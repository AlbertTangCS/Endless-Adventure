using System;
using System.Collections.Generic;
using System.Linq;

using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common
{
	public class World : IWorld
	{
		public string Name { get; }
		public string Description { get; }

		// cumulative spawn weights - if enemy1 has weight 10 and enemy2 has weight 5, dictionary has <15, enemy2>
		private readonly Dictionary<int, string> _enemySpawns;
		private readonly int _totalWeight;

		public World(WorldData data)
		{
			Name = data.Name;
			Description = data.Description;

			if (data.EnemySpawns == null)
				return;
			
			_enemySpawns = new Dictionary<int, string>();
			_totalWeight = 0;
			foreach (string key in data.EnemySpawns.Keys) {
				_totalWeight += data.EnemySpawns[key];
				_enemySpawns.Add(_totalWeight, key);
			}
		}

		public Combatant SpawnEnemy()
		{
			if (_enemySpawns == null) return null;

			// randomly generate an integer between 0 and the total weight
			// round up to the nearest cumulative weight, and 
			var result = Utilities.Random.Next(1, _totalWeight+1);

			// get the key that is the random value rounded up
			var key = _enemySpawns.Keys.FirstOrDefault( x => x >= result);
			if (!_enemySpawns.TryGetValue(key, out string combatantKey)) {
				throw new InvalidOperationException(); // no idea how we would get here
			}

			return new Combatant(Database.Combatants[combatantKey]);
		}
	}
}