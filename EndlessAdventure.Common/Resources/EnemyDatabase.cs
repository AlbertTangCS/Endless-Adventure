using System.Collections.Generic;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Resources
{
	public class EnemyDatabase {

		public static readonly List<CombatantData> Enemies = new List<CombatantData> {
			new CombatantData("Chicken", 50, WorldDatabase.GreenPastures, 2, 0, 0, 0, 0),
			new CombatantData("Scared Sheep", 20, WorldDatabase.GreenPastures, 3, 0, 0, 1, 1),
			new CombatantData("Small Pig", 20, WorldDatabase.GreenPastures, 3, 0, 1, 0, 1),
			new CombatantData("Pony", 5, WorldDatabase.GreenPastures, 5, 0, 1, 1, 2),
			new CombatantData("Goblin Deserter", 5, WorldDatabase.GreenPastures, 3, 0, 1, 1, 2),
			new CombatantData("Unicorn", 1, WorldDatabase.GreenPastures, 5, 0, 2, 1, 5),

			new CombatantData("Rabbit", 20, WorldDatabase.ShadyWoods, 2, 0, 0, 0, 0),
			new CombatantData("Crow", 5, WorldDatabase.ShadyWoods, 2, 0, 1, 0, 1),
			new CombatantData("Young Deer", 1, WorldDatabase.ShadyWoods, 3, 0, 1, 1, 2),
			new CombatantData("Young Wolf", 20, WorldDatabase.ShadyWoods, 4, 0, 2, 0, 2),
			new CombatantData("Goblin Scout", 5, WorldDatabase.ShadyWoods, 5, 0, 1, 1, 3),
			new CombatantData("Shroomling", 1, WorldDatabase.ShadyWoods, 3, 1, 2, 1, 5)
		};
	}

}