using System;
using System.Collections.Generic;
using System.Linq;

using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common
{
	public class World : IWorld
	{
		#region Private Fields
		
		// cumulative spawn weights - if enemy1 has weight 10 and enemy2 has weight 5, dictionary has <15, enemy2>
		private readonly SortedDictionary<int, string> _enemySpawns;
		private readonly int _totalWeight;
		private readonly Func<ICombatant> _spawnFunction;

		#endregion Private Fields
		
		public World(WorldData data)
		{
			Name = data.Name;
			Description = data.Description;

			if (data.EnemySpawns == null)
				return;
			
			_enemySpawns = new SortedDictionary<int, string>();
			_totalWeight = 0;
			foreach (string key in data.EnemySpawns.Keys) {
				_totalWeight += data.EnemySpawns[key];
				_enemySpawns.Add(_totalWeight, key);
			}
		}

		public World(string pName, string pDescription, Func<ICombatant> pSpawnFunction)
		{
			Name = pName;
			Description = pDescription;
			_spawnFunction = pSpawnFunction;
		}

		#region Public Fields
		
		public string Name { get; }
		public string Description { get; }

		#endregion Public Fields

		#region Public Methods

		public ICombatant SpawnEnemy() => _spawnFunction();
		/*{
			return _spawnFunction();
			if (_enemySpawns == null) return null;

			// randomly generate an integer between 0 and the total weight
			// round up to the nearest cumulative weight, and 
			var result = Utilities.Random.Next(1, _totalWeight+1);

			// get the key that is the random value rounded up
			var key = _enemySpawns.Keys.FirstOrDefault(x => x >= result);
			if (!_enemySpawns.TryGetValue(key, out string combatantKey)) {
				throw new InvalidOperationException(); // no idea how we would get here
			}

			return Factory.CreateCombatant(combatantKey);
		}*/
		
		#endregion Public Methods
	}
}