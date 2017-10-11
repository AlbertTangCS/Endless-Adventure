using System;
using System.Collections.Generic;
using EndlessAdventure.Resources;

namespace EndlessAdventure.Characters {

	public class Character {

		public string Name { get; private set; }
		public Dictionary<StatType, Stat> Stats { get; private set; }

		private CharacterState _state;
		public event EventHandler StateChanged;
		public CharacterState State {
			get {
				return _state;
			}
			private set {
				if (_state != value) {
					_state = value;
					OnStateChanged(EventArgs.Empty);
				}
			}
		}
		private void OnStateChanged(EventArgs e) {
			StateChanged?.Invoke(this, e);
		}

		public Character() : this(Defaults.CharacterName) { }

		public Character(string name) {
			Name = name;
			RegisterStats();
			State = CharacterState.Fighting;
		}

		public void RegisterStats() {
			Stats = new Dictionary<StatType, Stat>();
			foreach (StatType stat in Enum.GetValues(typeof(StatType))) {
				Stats.Add(stat, new Stat(stat));
			}
		}

		public void ApplyDamage(int pDamage) {
			Stat healthStat;
			Stats.TryGetValue(StatType.Health, out healthStat);

			if (healthStat.Current - pDamage < 0) {
				healthStat.Current = 0;
			}
			else {
				healthStat.Current -= pDamage;
			}

			if (healthStat.Current == 0) {
				State = CharacterState.Fallen;
			}
		}
	}
}