using System.Collections.Generic;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Characters {

	public class Character {

		public string Name { get; private set; }
		public string Description { get; private set; }

		public Dictionary<StatType, List<ABuff>> StatBonuses { get; private set; }

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

			StatBonuses = new Dictionary<StatType, List<ABuff>>();

			CurrentHealth = MaxHealth;
			CurrentEnergy = MaxEnergy;
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

		public void AddBody() {
			BaseBody += 1;
		}

		public void AddMind() {
			BaseMind += 1;
		}

		public void AddSoul() {
			BaseSoul += 1;
		}

		public void AddBuff(ABuff buff) {
			if (!StatBonuses.TryGetValue(buff.StatType, out List<ABuff> buffList)) {
				List<ABuff> buffs = new List<ABuff> { buff };
				StatBonuses.Add(buff.StatType, buffs);
			}
			else {
				buffList.Add(buff);
			}
		}

		public void RemoveBuff(ABuff buff) {
			if (StatBonuses.TryGetValue(buff.StatType, out List<ABuff> buffList)) {
				buffList.Remove(buff);
			}
		}

		#region Getters&Setters

		private int GetBuffedStat(StatType type, int baseValue) {
			if (StatBonuses.TryGetValue(type, out List<ABuff> buffs)) {
				int buffedValue = baseValue;
				foreach (ABuff buff in buffs) {
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

		public int Accuracy {
			get {
				return Defaults.CalculateAccuracy(Body, Mind, Soul);
			}
		}

		public int Evasion {
			get {
				return Defaults.CalculateEvasion(Body, Mind, Soul);
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