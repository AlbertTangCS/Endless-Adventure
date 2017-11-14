using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {

	public class Game {

		public World World { get; private set; }
		public Battlefield Battlefield { get; private set; }

		public Game() {
			Database.Initialize();
			World = new World(Database.Worlds[Database.KEY_WORLD_GREEN_PASTURES]);
			List<Combatant> protagonists = new List<Combatant> { new Combatant(Database.Combatants[Database.KEY_COMBATANT_PLAYER]) };
			Battlefield = new Battlefield(protagonists, World.SpawnEnemy);
		}

		public string Message => Battlefield.Message;

		public void TravelToWorld(string pWorldKey) {
			World = new World(Database.Worlds[pWorldKey]);
			Battlefield.SwitchWorlds(World.SpawnEnemy);
		}

		public void Update(int ticks = 1) {
			for (int i = 0; i < ticks; i++) {
				Battlefield.Update();
			}
		}

		public void ProcessInput() {

		}
	}
}