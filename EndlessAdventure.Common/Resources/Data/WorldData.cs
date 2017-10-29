using System.Collections.Generic;

namespace EndlessAdventure.Common.Resources
{
	public class WorldData
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public Dictionary<string, int> EnemySpawns { get; private set; }

		public WorldData(string name, string description, Dictionary<string, int> enemySpawns) {
			Name = name;
			Description = description;
			EnemySpawns = enemySpawns;
		}
	}
}