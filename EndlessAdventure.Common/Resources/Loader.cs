using System.Collections.Generic;
using System.Linq;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Resources {

	public class Loader {

		public static List<CombatantData> GetEnemyData(string world) {
			return new List<CombatantData>(EnemyDatabase.Enemies.Where(data => data.World == world));
		}
	}

}