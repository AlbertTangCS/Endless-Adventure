using System.Collections.Generic;
using System.Runtime.InteropServices;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {

	public class Game
	{

		private readonly ISaveFile _saveFile;

		public Game(ISaveFile pSaveFile)
		{
			_saveFile = pSaveFile;
			Database.Initialize();

			var protagonists = _saveFile.GetSavedProtagonists();//new List<Combatant> { new Combatant(Database.Combatants[Database.KEY_COMBATANT_PLAYER]) };
			var world = _saveFile.GetCurrentWorld();//new World(Database.Worlds[Database.KEY_WORLD_GREEN_PASTURES]);
			Battlefield = new Battlefield(protagonists, world);
		}
		
		public IBattlefield Battlefield { get; }

		public void TravelToWorld(string pWorldKey) {
			Battlefield.World = new World(Database.Worlds[pWorldKey]);
		}

		public void Update(int ticks = 1) {
			for (var i = 0; i < ticks; i++) {
				Battlefield.Tick();
			}
		}
	}
}