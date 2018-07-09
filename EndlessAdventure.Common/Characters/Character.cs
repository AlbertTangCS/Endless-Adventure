using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Characters {

	public class Character {

		public string Name { get; private set; }
		public string Description { get; private set; }

		public Dictionary<StatType, List<AStatBuff>> StatBonuses { get; private set; }
		public List<AEffect> ActiveEffects { get; private set; }
		public List<AOnHitBuff> OnHitBuffs { get; private set; }

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

			StatBonuses = new Dictionary<StatType, List<AStatBuff>>();
			ActiveEffects = new List<AEffect>();
			OnHitBuffs = new List<AOnHitBuff>();

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

		public void AddBuff(AStatBuff buff) {
			if (!StatBonuses.TryGetValue(buff.StatType, out List<AStatBuff> buffList)) {
				List<AStatBuff> buffs = new List<AStatBuff> { buff };
				StatBonuses.Add(buff.StatType, buffs);
			}
			else {
				buffList.Add(buff);
			}
		}

		public void RemoveBuff(AStatBuff buff) {
			if (StatBonuses.TryGetValue(buff.StatType, out List<AStatBuff> buffList)) {
				buffList.Remove(buff);
			}
		}

		public void AddOnHit(AOnHitBuff buff) {
			OnHitBuffs.Add(buff);
		}

		public void AddEffect(AEffect effect) {
			ActiveEffects.Add(effect);
		}

		public void RemoveEffect(AEffect effect) {
			ActiveEffects.Remove(effect);
		}

		#region Getters&Setters

		private int GetBuffedStat(StatType type, int baseValue) {
			if (StatBonuses.TryGetValue(type, out List<AStatBuff> buffs)) {
				int buffedValue = baseValue;
				foreach (AStatBuff buff in buffs) {
					buffedValue += buff.GetStatBonus(this);
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

		public int BaseAccuracy {
			get {
				return Defaults.CalculateAccuracy(Body, Mind, Soul);
			}
		}

		public int Accuracy {
			get {
				return GetBuffedStat(StatType.Accuracy, BaseAccuracy);
			}
		}

		public int BaseEvasion {
			get {
				return Defaults.CalculateEvasion(Body, Mind, Soul);
			}
		}

		public int Evasion {
			get {
				return GetBuffedStat(StatType.Evasion, BaseEvasion);
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