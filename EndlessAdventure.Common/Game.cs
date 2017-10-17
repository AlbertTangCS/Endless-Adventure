using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {

	public class Game {

		public World world { get; private set; }
		public Battlefield Battlefield { get; private set; }

		public Game() {
			world = WorldFactory.CreateWorld(WorldDatabase.GreenPastures);
			Battlefield = new Battlefield(world.SpawnEnemy);
		}

		public void Update() {
			Battlefield.Update();
		}

		public void ProcessInput() {

		}
	}
}