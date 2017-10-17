using System.Collections.Generic;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Characters {

	public class Character {

		public string Name { get; private set; }
		public string Description { get; private set; }
		
		private Dictionary<StatType, List<IBuff>> _statBonuses;

		/// <summary>
		/// DO NOT CALL DIRECTLY. Use CharacterFactory.
		/// </summary>
		public Character(string name, string description, int body, int mind, int soul, int fortune) {
			Name = name;
			Description = description;
			BaseBody = body;
			BaseMind = mind;
			BaseSoul = soul;
			BaseFortune = fortune;

			_statBonuses = new Dictionary<StatType, List<IBuff>>();
		}

		public void ApplyDamage(int pHealth) {
			if (CurrentHealth - pHealth < 0) {
				CurrentHealth = 0;
			}
			else {
				CurrentHealth -= pHealth;
			}
		}

		public void Heal(int pHealth) {
			if (CurrentHealth + pHealth > MaxHealth) {
				CurrentHealth = MaxHealth;
			}
			else {
				CurrentHealth += pHealth;
			}
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

		public void AddBuff(IBuff buff) {
			if (!_statBonuses.TryGetValue(buff.StatType, out List<IBuff> buffList)) {
				List<IBuff> buffs = new List<IBuff> { buff };
				_statBonuses.Add(buff.StatType, buffs);
			}
			else {
				buffList.Add(buff);
			}
		}

		public void RemoveBuff(IBuff buff) {
			if (_statBonuses.TryGetValue(buff.StatType, out List<IBuff> buffList)) {
				buffList.Remove(buff);
			}
		}

		#region Getters&Setters

		private int GetBuffedStat(StatType type, int baseValue) {
			if (_statBonuses.TryGetValue(type, out List<IBuff> buffs)) {
				int buffedValue = baseValue;
				foreach (IBuff buff in buffs) {
					buffedValue = buff.Apply(buffedValue);
				}
				return buffedValue;
			}
			else {
				return baseValue;
			}
		}

		public int BaseHealth {
			get {
				return Defaults.CalculateHealth(Body, Mind, Soul);
			}
		}

		public int CurrentHealth { get; private set; }

		public int MaxHealth {
			get {
				return GetBuffedStat(StatType.Health, BaseHealth);
			}
		}

		public int BaseEnergy {
			get {
				return Defaults.CalculateEnergy(Body, Mind, Soul);
			}
		}

		public int CurrentEnergy { get; private set; }

		public int MaxEnergy {
			get {
				return GetBuffedStat(StatType.Energy, BaseEnergy);
			}
		}

		public int BasePhysicalAttack {
			get {
				return Defaults.CalculatePhysicalAttack(Body, Mind, Soul);
			}
		}

		public int PhysicalAttack {
			get {
				return GetBuffedStat(StatType.PhysicalAttack, BasePhysicalAttack);
			}
		}

		public int BaseEnergyAttack {
			get {
				return Defaults.CalculateEnergyAttack(Body, Mind, Soul);
			}
		}

		public int EnergyAttack {
			get {
				return GetBuffedStat(StatType.EnergyAttack, BaseEnergyAttack);
			}
		}

		public int BaseDefense {
			get {
				return Defaults.CalculateDefense(Body, Mind, Soul);
			}
		}

		public int Defense {
			get {
				return GetBuffedStat(StatType.Defense, BaseDefense);
			}
		}

		public int BaseBody { get; private set; }

		public int Body {
			get {
				return GetBuffedStat(StatType.Body, BaseBody);
			}
		}

		public int BaseMind { get; private set; }

		public int Mind {
			get {
				return GetBuffedStat(StatType.Mind, BaseMind);
			}
		}

		public int BaseSoul { get; private set; }

		public int Soul {
			get {
				return GetBuffedStat(StatType.Soul, BaseSoul);
			}
		}

		public int BaseFortune { get; private set; }

		public int Fortune {
			get {
				return GetBuffedStat(StatType.Fortune, BaseFortune);
			}
		}

		#endregion

	}
}