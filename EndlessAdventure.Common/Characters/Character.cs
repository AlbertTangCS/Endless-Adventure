using System.Collections.Generic;

namespace EndlessAdventure.Common.Characters {

	public class Character {

		public string Name { get; private set; }
		public Dictionary<StatType, Stat> Stats { get; private set; }

		/// <summary>
		/// DO NOT CALL DIRECTLY. Use CharacterFactory.
		/// </summary>
		public Character(string name, Dictionary<StatType, Stat> stats) {
			Name = name;
			Stats = stats;
		}

		public void ApplyDamage(int pDamage) {
			Stats.TryGetValue(StatType.Health, out Stat healthStat);

			if (healthStat.Current - pDamage < 0) {
				healthStat.Current = 0;
			}
			else {
				healthStat.Current -= pDamage;
			}
		}

		public int CurrentHealth {
			get {
				Stats.TryGetValue(StatType.Health, out Stat health);
				return health.Current;
			}
		}

		public int MaxHealth {
			get {
				Stats.TryGetValue(StatType.Health, out Stat health);
				return health.Max;
			}
		}

	}
}