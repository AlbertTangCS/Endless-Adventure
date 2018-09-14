using System.Collections.Generic;
using System.Linq;
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

		private readonly Dictionary<StatType, List<IStatBuff>> _statBuffs;
		private readonly List<IActiveEffect> _activeEffects;
		private readonly List<IOnHitEffect> _onHitBuffs;
		
		#endregion Private Fields;
		
		public Combatant(string pName, string pDescription, int pExpReward, int pLevel, int pBody, int pMind, int pSoul, int pExperience, int pSkillPoints)
		{
			Name = pName;
			Description = pDescription;
			ExpReward = pExpReward;

			Level = pLevel;
			
			BaseBody = pBody;
			BaseMind = pMind;
			BaseSoul = pSoul;
			
			// player specific
			Experience = pExperience;
			SkillPoints = pSkillPoints;

			_inventory =  new Inventory();
			
			_statBuffs = new Dictionary<StatType, List<IStatBuff>>();
			_activeEffects = new List<IActiveEffect>();
			_onHitBuffs = new List<IOnHitEffect>();
			
			CurrentHealth = MaxHealth;
			CurrentEnergy = MaxEnergy;
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
		
		public IEnumerable<IActiveEffect> ActiveEffects => _activeEffects;
		public IEnumerable<IOnHitEffect> OnHitBuffs => _onHitBuffs;
		
		// for easy consumption for Battlefield
		public IEnumerable<IStatBuff> StatBuffs
		{
			get
			{
				IEnumerable<IStatBuff> statBuffs = new List<IStatBuff>();
				foreach (var subStatBuffs in _statBuffs.Values)
				{
					statBuffs = statBuffs.Concat(subStatBuffs);
				}
				return statBuffs;
			}
		}
		
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
			// TODO: make this heal a dynamic amount
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

		public void AddEffect(IEffect effect)
		{
			switch (effect)
			{
				case IActiveEffect activeEffect:
					_activeEffects.Add(activeEffect);
					break;
				case IOnHitEffect onHitEffect:
					_onHitBuffs.Add(onHitEffect);
					break;
				case IStatBuff statBuff:
				{
					if (!_statBuffs.TryGetValue(statBuff.StatType, out var buffList))
					{
						var buffs = new List<IStatBuff> { statBuff };
						_statBuffs.Add(statBuff.StatType, buffs);
					}
					else
					{
						buffList.Add(statBuff);
					}
					break;
				}
			}
		}

		public void RemoveEffect(IEffect effect)
		{
			switch (effect)
			{
				case IActiveEffect activeEffect:
					_activeEffects.Remove(activeEffect);
					break;
				case IOnHitEffect onHitEffect:
					_onHitBuffs.Remove(onHitEffect);
					break;
				case IStatBuff statBuff:
				{
					if (_statBuffs.TryGetValue(statBuff.StatType, out var buffList))
					{
						buffList.Remove(statBuff);
					}
					break;
				}
			}
		}

		public void AddItem(IItem pItem) => _inventory.AddItem(pItem);
		public void RemoveItem(IItem pItem) => _inventory.RemoveItem(pItem);

		public bool TryEquipItem(IItem pItem, out IItem pUnequipped)
		{
			if (!_inventory.TryEquipItem(pItem, out var unequipped))
			{
				pUnequipped = null;
				return false;
			}

			if (unequipped != null)
			{
				foreach (var effect in unequipped.EquipEffects)
				{
					RemoveEffect(effect);
				}
			}

			foreach (var effect in pItem.EquipEffects)
			{
				AddEffect(effect);
			}
			
			pUnequipped = unequipped;
			return true;
		}

		public void UnequipItem(IItem pItem)
		{
			if (!_inventory.UnequipItem(pItem))
				return;

			foreach (var effect in pItem.EquipEffects)
			{
				RemoveEffect(effect);
			}
		}

		public void ConsumeItem(IItem pItem)
		{
			if (pItem.Type != ItemType.Consumable || _inventory.Contains(pItem))
				return;

			foreach (var effect in pItem.EquipEffects)
			{
				AddEffect(effect);
			}
			_inventory.RemoveItem(pItem);
		}
		
		#endregion Public Methods
		
		#region Private Implementation
		
		private int GetBuffedStat(StatType type, int baseValue) {
			if (!_statBuffs.TryGetValue(type, out var buffs))
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