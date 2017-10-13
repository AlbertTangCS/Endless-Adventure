using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Equipments;

namespace EndlessAdventure.Common.Resources
{
	/// <summary>
	/// Creates Combatants.
	/// </summary>
	public class CombatantFactory {

		public static Combatant CreateCombatant(string name, StatType pType, int value) {
			Combatant created = new Combatant(CharacterFactory.CreateCharacter(name, pType, value),
																				new Equipment(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}

		public static Combatant CreateCombatant(StatType pType, int value) {
			Combatant created = new Combatant(CharacterFactory.CreateCharacter(pType, value),
																				new Equipment(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}

		public static Combatant CreateCombatant(string name) {
			Combatant created = new Combatant(CharacterFactory.CreateCharacter(name),
																				new Equipment(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}
	}

	/// <summary>
	/// Creates Characters.
	/// </summary>
	public class CharacterFactory {

		public static Character CreateCharacter(string name, StatType pType, int value) {
			return new Character(name, StatsFactory.CreateStats(pType, value));
		}

		public static Character CreateCharacter(StatType pType, int value) {
			return CreateCharacter(Defaults.CharacterName, pType, value);
		}

		public static Character CreateCharacter(string name) {
			return new Character(name, StatsFactory.CreateStats());
		}
	}

	/// <summary>
	/// Creates Stats.
	/// </summary>
	internal class StatsFactory {

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

	public class WorldFactory {
		public static World CreateWorld(string name) {
			List<EnemyData> enemyData = Loader.GetEnemyData(name);
			World world = new World(name, enemyData);
			return world;
		}
	}

}