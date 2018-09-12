using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Battle
{
	public class Combatant : ICombatant
	{
		#region Private Fields

		private readonly Inventory _inventory;
		
		private int _pendingDamage;

		private readonly Dictionary<StatType, List<AStatBuff>> _statBonuses;
		private readonly List<AEffect> _activeEffects;
		private readonly List<AOnHitBuff> _onHitBuffs;
		
		#endregion Private Fields;
		
		public Combatant(CombatantData pCombatantData)
		{
			Name = pCombatantData.Name;
			Description = pCombatantData.Description;
			ExpReward = pCombatantData.ExpReward;
			
			BaseBody = pCombatantData.Body;
			BaseMind = pCombatantData.Mind;
			BaseSoul = pCombatantData.Soul;
			
			_inventory =  new Inventory(null, null, null, null);
			if (pCombatantData.Drops != null)
			{
				foreach (var key in pCombatantData.Drops.Keys)
				{
					var chance = Utilities.Random.NextDouble();
					if (!(pCombatantData.Drops[key] - chance > 0))
						continue;
					
					var item = new Item(Database.Items[key]);
					Inventory.AddItem(item);
				}
			}

			if (pCombatantData.Buffs != null)
			{
				foreach (var key in pCombatantData.Buffs.Keys)
				{
					var isBuff = Database.Buffs.TryGetValue(key, out var buffResult);
					if (isBuff)
					{
						AddBuff(buffResult(pCombatantData.Buffs[key], -1));
					}
					else
					{
						var isOnHit = Database.OnHits.TryGetValue(key, out var onHitResult);
						if (isOnHit)
						{
							AddOnHit(onHitResult(pCombatantData.Buffs[key], 5));
						}
					}
				}
			}

			Experience = pCombatantData.Experience;
			SkillPoints = pCombatantData.SkillPoints;

			_statBonuses = new Dictionary<StatType, List<AStatBuff>>();
			_activeEffects = new List<AEffect>();
			_onHitBuffs = new List<AOnHitBuff>();
		}

		#region Public Fields
		
		public string Name { get; }
		public string Description { get; }
		public int ExpReward { get; }
		
		public int Level { get; private set; }
		public int Experience { get; private set; }
		public int SkillPoints { get; private set; }

		public IInventory Inventory => _inventory;
		
		#region Base Stats

		public int BaseBody { get; private set; }
		public int Body => GetBuffedStat(StatType.Body, BaseBody);

		public int BaseMind { get; private set; }
		public int Mind => GetBuffedStat(StatType.Mind, BaseMind);

		public int BaseSoul { get; private set; }
		public int Soul => GetBuffedStat(StatType.Soul, BaseSoul);

		public int BaseFortune { get; private set; }
		public int Fortune => GetBuffedStat(StatType.Fortune, BaseFortune);

		#endregion Base Stats
		
		#region Derived Stats
		
		public int BaseHealth => Defaults.CalculateHealth(Body, Mind, Soul);
		public int CurrentHealth { get; private set; }
		public int MaxHealth => GetBuffedStat(StatType.Health, BaseHealth);

		public int BaseEnergy => Defaults.CalculateEnergy(Body, Mind, Soul);
		public int CurrentEnergy { get; private set; }
		public int MaxEnergy => GetBuffedStat(StatType.Energy, BaseEnergy);

		public int BasePhysicalAttack => Defaults.CalculatePhysicalAttack(Body, Mind, Soul);
		public int PhysicalAttack => GetBuffedStat(StatType.PhysicalAttack, BasePhysicalAttack);

		public int BaseEnergyAttack => Defaults.CalculateEnergyAttack(Body, Mind, Soul);
		public int EnergyAttack => GetBuffedStat(StatType.EnergyAttack, BaseEnergyAttack);

		public int BaseDefense => Defaults.CalculateDefense(Body, Mind, Soul);
		public int Defense => GetBuffedStat(StatType.Defense, BaseDefense);
		
		public int BaseAccuracy => Defaults.CalculateAccuracy(Body, Mind, Soul);
		public int Accuracy => GetBuffedStat(StatType.Accuracy, BaseAccuracy);

		public int BaseEvasion => Defaults.CalculateEvasion(Body, Mind, Soul);
		public int Evasion => GetBuffedStat(StatType.Evasion, BaseEvasion);
		
		#endregion Derived Stats
		
		public bool Fallen { get; private set; }
		
		public IEnumerable<AEffect> ActiveEffects => _activeEffects;
		public IEnumerable<AOnHitBuff> OnHitBuffs => _onHitBuffs;
		
		#endregion Public Fields

		#region Public Methods

		public void AddPendingDamage(int pDamage)
		{
			_pendingDamage += pDamage;
		}
		
		public int ApplyPendingDamage()
		{
			var pendingDamage = _pendingDamage;
			ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
			if (CurrentHealth == 0) {
				Fallen = true;
			}
			return pendingDamage;
		}
		
		public void AutoHeal()
		{
			Heal(1);
		}
		
		public void SpendEnergy(int pEnergy) {
			if (CurrentEnergy - pEnergy < 0) {
				CurrentEnergy = 0;
			}
			else {
				CurrentEnergy -= pEnergy;
			}
		}

		public void Recharge(int pEnergy) {
			if (CurrentEnergy + pEnergy > MaxEnergy) {
				CurrentEnergy = MaxEnergy;
			}
			else {
				CurrentEnergy += pEnergy;
			}
		}
		
		public void AddExperience(int pExperience)
		{
			Experience += pExperience;
			if (Experience < Defaults.NextLevelExpFormula(Level))
				return;
			
			Experience %= Defaults.NextLevelExpFormula(Level);
			Level++;
			SkillPoints += 3;
		}

		public bool AddSkillPoint(StatType pType, int pCount = 1)
		{
			if (SkillPoints == 0 || SkillPoints < pCount)
				return false;

			for (var i = 0; i < pCount; i++)
			{
				var validSkill = true;
				switch (pType)
				{
					case StatType.Body:
						BaseBody += 1;
						break;
					case StatType.Mind:
						BaseMind += 1;
						break;
					case StatType.Soul:
						BaseSoul += 1;
						break;
					default:
						validSkill = false;
						break;
				}

				if (validSkill) SkillPoints -= 1;
			}
			return true;
		}

		/*public bool TryAttack(ICombatant pCombatant, out int pDamage)
		{
			if (!Defaults.DidMiss(Accuracy, pCombatant.Evasion)) {
				int pendingDamage = PhysicalAttack - pCombatant.Defense;
				if (pendingDamage > 0) {
					pCombatant.AddPendingDamage(pendingDamage);
				}
				pDamage = pendingDamage;
				return true;
			}
			pDamage = 0;
			return false;
		}*/
		
		public void Equip(IItem item)
		{
			//Inventory.Equip(item, Character);
		}

		public void Unequip(IItem equipment)
		{
			//Inventory.Unequip(equipment, Character);
		}

		public void Consume(IItem item)
		{
			//Inventory.Consume(item, Character);
		}
		
		public void AddBuff(AStatBuff buff) {
			if (!_statBonuses.TryGetValue(buff.StatType, out var buffList)) {
				List<AStatBuff> buffs = new List<AStatBuff> { buff };
				_statBonuses.Add(buff.StatType, buffs);
			}
			else {
				buffList.Add(buff);
			}
		}

		public void RemoveBuff(AStatBuff buff) {
			if (_statBonuses.TryGetValue(buff.StatType, out var buffList)) {
				buffList.Remove(buff);
			}
		}

		public void AddOnHit(AOnHitBuff buff) {
			_onHitBuffs.Add(buff);
		}

		public void AddEffect(AEffect effect) {
			_activeEffects.Add(effect);
		}

		public void RemoveEffect(AEffect effect) {
			_activeEffects.Remove(effect);
		}
		
		#endregion Public Methods
		
		#region Private Implementation
		
		private int GetBuffedStat(StatType type, int baseValue) {
			if (!_statBonuses.TryGetValue(type, out var buffs))
				return baseValue;
			
			var buffedValue = baseValue;
			foreach (var buff in buffs)
			{
				buffedValue += buff.GetStatBonus(this);
			}
			return buffedValue;

		}
		
		private void ApplyDamage(int pHealth)
		{
			if (CurrentHealth - pHealth < 0)
			{
				CurrentHealth = 0;
			}
			else
			{
				CurrentHealth -= pHealth;
			}
		}

		private void Heal(int pHealth)
		{
			if (CurrentHealth + pHealth > MaxHealth)
			{
				CurrentHealth = MaxHealth;
				if (Fallen)
					Fallen = false;
			}
			else {
				CurrentHealth += pHealth;
			}
		}
		
		#endregion Private Implementation
	}
}