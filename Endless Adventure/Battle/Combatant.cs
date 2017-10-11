using System;
using EndlessAdventure.Characters;
using EndlessAdventure.Equipments;

namespace EndlessAdventure.Battle {
	public class Combatant {

		public Character Character { get; private set; }
		public int Level { get; private set; }
		public int Experience { get; private set; }
		private Equipment Equipment;
		private int _pendingDamage;

		private bool _fallen;
		public event EventHandler StateChanged;
		public bool Fallen {
			get {
				return _fallen;
			}
			private set {
				if (_fallen != value) {
					_fallen = value;
					OnStateChanged(EventArgs.Empty);
				}
			}
		}
		private void OnStateChanged(EventArgs e) {
			StateChanged?.Invoke(this, e);
		}

		public Combatant() : this (new Character(), new Equipment()) { }

		public Combatant(Character character) : this (character, new Equipment()) { }

		public Combatant(Character character, Equipment equipment) {
			Character = character;
			Equipment = equipment;

			Fallen = false;
		}

		public void Heal() {
			Stat healthStat;
			Character.Stats.TryGetValue(StatType.Health, out healthStat);
			healthStat.Current += 1;
			if (healthStat.Current == healthStat.Max) {
				Fallen = false;
			}
		}

		public void AttackCombatant(Combatant antagonist) {
			Stat attackStat;
			Character.Stats.TryGetValue(StatType.Attack, out attackStat);
			antagonist._pendingDamage = attackStat.Current;
		}

		public void ApplyPendingDamage() {
			Character.ApplyDamage(_pendingDamage);
			_pendingDamage = 0;
			if (Character.CurrentHealth == 0) {
				Fallen = true;
			}
		}
	}
}