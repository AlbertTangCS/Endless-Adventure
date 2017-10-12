using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Characters {

	public class CharacterFactory {
		public static Character CreateCharacter(StatType pType, int value) {
			return new Character(StatsFactory.CreateStats(pType, value));
		}
		public static Character Attack0 => new Character(StatsFactory.CreateStats(StatType.Attack, 0));
		public static Character Health2 => new Character(StatsFactory.CreateStats(StatType.Health, 2));
	}

	public class StatsFactory {

		public static Dictionary<StatType, Stat> CreateStats() {
			Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
			foreach (StatType stat in Enum.GetValues(typeof(StatType))) {
				stats.Add(stat, new Stat(stat, GetDefaultStatValue(stat)));
			}
			return stats;
		}

		public static Dictionary<StatType, Stat> CreateStats(StatType pType, int value) {
			Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
			foreach (StatType stat in Enum.GetValues(typeof(StatType))) {
				if (stat == pType) {
					stats.Add(stat, new Stat(stat, value));
				}
				else {
					stats.Add(stat, new Stat(stat, GetDefaultStatValue(stat)));
				}
			}
			return stats;
		}

		private static int GetDefaultStatValue(StatType pType) {
			switch (pType) {
				case StatType.Health:
					return Defaults.CharacterHealth;
				case StatType.Energy:
					return Defaults.CharacterEnergy;
				case StatType.Attack:
					return Defaults.CharacterAttack;
				case StatType.Defense:
					return Defaults.CharacterDefense;
				case StatType.Strength:
					return Defaults.CharacterStrength;
				case StatType.Dexterity:
					return Defaults.CharacterDexterity;
				case StatType.Vitality:
					return Defaults.CharacterVitality;
				case StatType.Intelligence:
					return Defaults.CharacterIntelligence;
				default:
					return 0;
			}
		}
	}
}
