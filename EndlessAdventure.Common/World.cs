using System;

using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common
{
	public class World : IWorld
	{
		#region Private Fields
		
		private readonly Func<ICombatant> _spawnFunction;

		#endregion Private Fields

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
		
		#endregion Public Methods
	}
}