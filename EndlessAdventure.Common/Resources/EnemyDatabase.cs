using System.Collections.Generic;

namespace EndlessAdventure.Common.Resources
{
	public class EnemyDatabase {

		public static readonly List<EnemyData> Enemies = new List<EnemyData> {
			new EnemyData("Enemy1", 10, WorldDatabase.GreenPastures),
			new EnemyData("Enemy2", 20, WorldDatabase.GreenPastures),
			new EnemyData("Enemy3", 30, WorldDatabase.GreenPastures)
		};
	}

	public class EnemyData {

		public string Name { get; private set; }
		public int Weight { get; private set; }
		public string World { get; private set; }

		public EnemyData(string name, int weight, string world) {
			Name = name;
			Weight = weight;
			World = world;
		}
	}
}