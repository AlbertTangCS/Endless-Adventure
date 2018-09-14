using System;
using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Resources
{
	public static class Factory
	{
		public static IWorld CreateWorld(string pWorldKey)
		{
			var data = Database.Worlds[pWorldKey];

			var enemySpawns = new SortedDictionary<int, string>();
			var totalWeight = 0;
			foreach (var key in data.EnemySpawns.Keys)
			{
				totalWeight += data.EnemySpawns[key];
				enemySpawns.Add(totalWeight, key);
			}

			ICombatant spawnFunction()
			{
				// randomly generate an integer between 0 and the total weight
				// round up to the nearest cumulative weight, and 
				var result = Utilities.Random.Next(1, totalWeight + 1);

				// get the key that is the random value rounded up
				var key = enemySpawns.Keys.FirstOrDefault(x => x >= result);
				return key == 0 ? null : CreateCombatant(enemySpawns[key]);
			}

			return new World(data.Name, data.Description, spawnFunction);
		}
		
		private static ICombatant CreateCombatant(string pCombatantKey)
		{
			var data = Database.Combatants[pCombatantKey];
			var combatant = new Combatant(data.Name, data.Description, data.ExpReward, data.Level, data.Body, data.Mind, data.Soul, data.Experience, data.SkillPoints);

			foreach (var itemKey in data.Drops.Keys)
			{
				var chance = Utilities.Random.NextDouble();
				if (!(data.Drops[itemKey] - chance > 0))
					continue;
		
				var item = CreateItem(itemKey);
				combatant.AddItem(item);
			}

			foreach (var buffKey in data.Buffs.Keys)
			{
				var buff = CreateEffect(buffKey, data.Buffs[buffKey], -1);
				combatant.AddEffect(buff);
			}
			
			return combatant;
		}
		
		private static IItem CreateItem(string pItemKey)
		{
			var data = Database.Items[pItemKey];

			var equipEffects = new List<IEffect>();
			// add instant effects for consumables
			if (data.Type == ItemType.Consumable)
			{
				foreach (var effect in data.EquipEffects.Keys)
				{
					equipEffects.Add(CreateEffect(effect, data.EquipEffects[effect], 1));
				}
			}
			// add permanent effects for equippables
			else if (data.Type != ItemType.Miscellaneous)
			{
				foreach (var effect in data.EquipEffects.Keys)
				{
					equipEffects.Add(CreateEffect(effect, data.EquipEffects[effect], -1));
				}
			}

			var item = new Item(data.Name, data.Description, data.Type, data.Cost, equipEffects);
			return item;
		}

		private static IEffect CreateEffect(string pEffectKey, double pValue, int pDuration = -1)
		{
			var data = Database.Effects[pEffectKey];

			switch (pEffectKey)
			{
				// active effects
				case Database.KEY_EFFECT_HEAL:
					return new HealEffect(data.Name, data.Description, pValue, pDuration);
				case Database.KEY_EFFECT_POISON:
					return new PoisonEffect(data.Name, data.Description, pValue, pDuration);
				
				// stat buffs
				case Database.KEY_STATBUFF_INC_PHYSICAL_ATTACK:
					return new BuffStatAdditive(data.Name, data.Description, pValue, pDuration, StatType.PhysicalAttack);
				case Database.KEY_STATBUFF_MULT_PHYSICAL_ATTACK:
					return new BuffStatMultPhysicalAttack(data.Name, data.Description, pValue, pDuration);
				case Database.KEY_STATBUFF_INC_DEFENSE:
					return new BuffStatAdditive(data.Name, data.Description, pValue, pDuration, StatType.Defense);
				case Database.KEY_STATBUFF_INC_EVASION:
					return new BuffStatAdditive(data.Name, data.Description, pValue, pDuration, StatType.Evasion);

				// on hits
				case Database.KEY_ONHIT_POISON:
				{
					IEnumerable<IEffect> getApplyEffects()
					{
						var effects = new List<IEffect> {
							CreateEffect(Database.KEY_EFFECT_POISON, 1, 5)
						};
						return effects;
					};
					return new OnHitBuff(data.Name, data.Description, pValue, pDuration, getApplyEffects);
				}

				default:
					throw new ArgumentException();
			}
		}
	}
}