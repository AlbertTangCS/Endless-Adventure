using System;
using System.Collections.Generic;
using System.Linq;

using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {
	public class World {

		public string Name { get; private set; }
		public string Description { get; private set; }

		// cumulative spawn weights - if enemy1 has weight 10 and enemy2 has weight 5, dictionary has <15, enemy2>
		private Dictionary<int, string> _enemySpawns;
		private int _totalWeight;
		private Random _random;

		public World(WorldData data) {
			Name = data.Name;
			Description = data.Description;

			if (data.EnemySpawns != null) {
				_enemySpawns = new Dictionary<int, string>();
				_totalWeight = 0;
				foreach (string key in data.EnemySpawns.Keys) {
					_totalWeight += data.EnemySpawns[key];
					_enemySpawns.Add(_totalWeight, key);
				}
			}

			_random = new Random();
		}

		public Combatant SpawnEnemy() {
			if (_enemySpawns == null) return null;

			// randomly generate an integer between 0 and the total weight
			// round up to the nearest cumulative weight, and 
			int result = _random.Next(1, _totalWeight+1);

			// get the key that is the random value rounded up
			int key = _enemySpawns.Keys.FirstOrDefault( x => x >= result);
			if (!_enemySpawns.TryGetValue(key, out string combatantKey)) {
				throw new InvalidOperationException(); // no idea how we would get here
			}

			return new Combatant(Database.Combatants[combatantKey]);
		}
	}

}