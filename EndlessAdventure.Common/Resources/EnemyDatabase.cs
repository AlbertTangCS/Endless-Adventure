using System.Collections.Generic;
using EndlessAdventure.Common.Battle;

namespace EndlessAdventure.Common.Resources
{
	public class EnemyDatabase {

		public static readonly List<CombatantData> Enemies = new List<CombatantData> {
			new CombatantData("Chicken", "chicken", 50, WorldDatabase.GreenPastures, 0, 1, 0, 0, 0),
			new CombatantData("Scared Sheep", "sheep", 20, WorldDatabase.GreenPastures, 1, 1, 1, 0, 0),
			new CombatantData("Small Pig", "pig", 20, WorldDatabase.GreenPastures, 1, 1, 1, 0, 0),
			new CombatantData("Pony", "horse", 5, WorldDatabase.GreenPastures, 2, 2, 1, 0, 0),
			new CombatantData("Goblin Deserter", "goblin", 5, WorldDatabase.GreenPastures, 2, 2, 1, 0, 0),
			new CombatantData("Unicorn", "unicorn", 1, WorldDatabase.GreenPastures, 2, 2, 2, 1, 0),

			new CombatantData("Rabbit", "rabbit", 20, WorldDatabase.ShadyWoods, 2, 0, 0, 0, 0),
			new CombatantData("Crow", "crow", 5, WorldDatabase.ShadyWoods, 2, 0, 1, 0, 1),
			new CombatantData("Young Deer", "deer", 1, WorldDatabase.ShadyWoods, 3, 0, 1, 1, 2),
			new CombatantData("Young Wolf", "wolf", 20, WorldDatabase.ShadyWoods, 4, 0, 2, 0, 2),
			new CombatantData("Goblin Scout", "goblin", 5, WorldDatabase.ShadyWoods, 5, 0, 1, 1, 3),
			new CombatantData("Shroomling", "shroom", 1, WorldDatabase.ShadyWoods, 3, 1, 2, 1, 5)
		};
	}

}