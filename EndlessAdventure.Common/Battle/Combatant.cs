using System;

using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Battle {
	public class Combatant {

		public Character Character { get; private set; }
		public int Level { get; private set; }
		public int Experience { get; private set; }
		private int _expReward;
		public Inventory Inventory { get; private set; }
		public int SkillPoints { get; private set; }

		private int _pendingDamage;
		private bool _fallen;

		private static Random _random = new Random();

		public Combatant(CombatantData combatantData, Inventory inventory = null) {
			Character = new Character(combatantData.Name, combatantData.Description, combatantData.Body, combatantData.Mind, combatantData.Soul, 0);
			_expReward = combatantData.ExpReward;

			Inventory = inventory ?? new Inventory(null, null, null, null);

			if (combatantData.Drops != null) {
				
				foreach (string key in combatantData.Drops.Keys) {
					double chance = _random.NextDouble();
					if (combatantData.Drops[key] - chance > 0) {
						Item item = new Item(Database.Items[key]);
						Inventory.Add(item);
					}
				}
			}

			Fallen = false;
		}
		
		public void AutoHeal() {
			Character.Heal(1);
			if (Character.CurrentHealth == Character.MaxHealth) {
				Fallen = false;
			}
		}

		public void AttackCombatant(Combatant antagonist) {
			int pendingDamage = Character.PhysicalAttack - antagonist.Character.Defense;
			if (pendingDamage > 0) {
				antagonist._pendingDamage += pendingDamage;
			}
		}

		public void DefeatCombatant(Combatant antagonist) {
			AddExperience(antagonist._expReward);
			Inventory.Equippables.AddRange(antagonist.Inventory.Equipped.Values);
			Inventory.Equippables.AddRange(antagonist.Inventory.Equippables);
			Inventory.Consumables.AddRange(antagonist.Inventory.Consumables);
			Inventory.Miscellaneous.AddRange(antagonist.Inventory.Miscellaneous);
		}

		public void ApplyPendingDamage() {
			Character.ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
			if (Character.CurrentHealth == 0) {
				Fallen = true;
			}
		}

		public void AddExperience(int experience) {
			Experience += experience;
			if (Experience >= Defaults.NextLevelExpFormula(Level)) {
				Experience %= Defaults.NextLevelExpFormula(Level);
				Level++;
				SkillPoints += 3;
			}
		}

		public void AddSkillPoint(StatType type) {
			if (SkillPoints == 0) return;
			else {
				bool validSkill = true;
				switch (type) {
					case StatType.Body:
						Character.AddBody();
						break;
					case StatType.Mind:
						Character.AddMind();
						break;
					case StatType.Soul:
						Character.AddSoul();
						break;
					default:
						validSkill = false;
						break;
				}
				if (validSkill) SkillPoints -= 1;
			}
		}

		#region Equipment

		public void Equip(Item item) {
			Inventory.Equip(item, Character);
		}

		public void Unequip(Item equipment) {
			Inventory.Unequip(equipment, Character);
		}

		public void Consume(Item item) {
			Inventory.Consume(item, Character);
		}

#endregion

#region Getters&Setters

		public bool Fallen {
			get {
				return _fallen;
			}
			private set {
				if (_fallen != value) {
					_fallen = value;
				}
			}
		}

		public int Luck { get; private set; }

#endregion

	}
}