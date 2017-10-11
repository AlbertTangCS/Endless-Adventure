using System.Collections.Generic;
using EndlessAdventure.Resources;

namespace EndlessAdventure.Characters {

	public class Character {

		public string Name { get; private set; }
		public Dictionary<StatType, Stat> Stats { get; private set; }

		public Character() : this(Defaults.CharacterName, null) { }

		public Character(Dictionary<StatType, Stat> stats) : this(Defaults.CharacterName, stats) { }

		public Character(string name, Dictionary<StatType, Stat> stats) {
			Name = name;
			if (stats == null) {
				Stats = StatsFactory.CreateStats();
			}
			else {
				Stats = stats;
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
		}

		public int CurrentHealth {
			get {
				Stat health;
				Stats.TryGetValue(StatType.Health, out health);
				return health.Current;
			}
		}

		public int MaxHealth {
			get {
				Stat health;
				Stats.TryGetValue(StatType.Health, out health);
				return health.Max;
			}
		}
	}
}