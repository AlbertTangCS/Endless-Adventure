using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common {

	public class Game {

		public World World { get; private set; }
		public Battlefield Battlefield { get; private set; }

		public Game() {
			World = WorldFactory.CreateWorld(WorldDatabase.GreenPastures);
			Battlefield = new Battlefield(Loader.GetProtagonists(), World.SpawnEnemy);
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