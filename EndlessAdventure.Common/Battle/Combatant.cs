using System;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Equipments;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Battle {
	public class Combatant {

		public Character Character { get; private set; }
		public int Level { get; private set; }
		public int Experience { get; private set; }
		private int _expReward;
		private Equipment Equipment;

		private int _pendingDamage;

		private bool _fallen;
		
		/// <summary>
		/// DO NOT CALL DIRECTLY. Use CombatantFactory.
		/// </summary>
		public Combatant(Character character, Equipment equipment, int level, int expReward) {

			Character = character ?? throw new ArgumentException();
			Equipment = equipment ?? throw new ArgumentException();
			if (level < 1) throw new ArgumentException();
			Level = level;
			_expReward = expReward;

			Fallen = false;
		}
		
		public void AutoHeal() {
			Character.Stats.TryGetValue(StatType.Health, out Stat healthStat);
			healthStat.Current += 1;
			if (healthStat.Current == healthStat.Max) {
				Fallen = false;
			}
		}

		public void AttackCombatant(Combatant antagonist) {
			Character.Stats.TryGetValue(StatType.Attack, out Stat proAttack);
			antagonist.Character.Stats.TryGetValue(StatType.Defense, out Stat antDefense);
			antagonist._pendingDamage = proAttack.Current - antDefense.Current;
			if (antagonist._pendingDamage < 0) {
				antagonist._pendingDamage = 0;
			}
		}

		public void DefeatCombatant(Combatant antagonist) {
			AddExperience(antagonist._expReward);
		}

		public void AddExperience(int experience) {
			Experience += experience;
			if (Experience >= Defaults.LevelExpFormula(Level)) {
				Experience %= Defaults.LevelExpFormula(Level);
				Level++;
			}
		}

		public void ApplyPendingDamage() {
			Character.ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
			if (Character.CurrentHealth == 0) {
				Fallen = true;
			}
		}

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

#endregion

	}
}