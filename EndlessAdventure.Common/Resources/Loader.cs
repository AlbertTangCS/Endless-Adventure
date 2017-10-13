using System.Collections.Generic;
using System.Linq;

namespace EndlessAdventure.Common.Resources
{
	public class Loader
	{
		public static List<EnemyData> GetEnemyData(string world) {
			return new List<EnemyData>(EnemyDatabase.Enemies.Where(data => data.World == world));
		}
	}
}
