using EndlessAdventure.Battle;

namespace EndlessAdventure {

	public class Game {

		public Battlefield Battlefield { get; private set; }

		public Game() {
			Battlefield = new Battlefield();
		}

		public void Update() {
			Battlefield.Update();
		}

		public void ProcessInput() {

		}
	}
}