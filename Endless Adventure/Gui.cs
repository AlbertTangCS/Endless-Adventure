using System;

namespace EndlessAdventure {

	public class Gui {

		private Game _game;

		public Gui(Game game) {
			_game = game;
		}

		private bool _tickOrTock = true;
		public void Render() {
			Console.Clear();

			Characters.Character protagonist = _game.Battlefield.Protagonists[0].Character;
			Console.WriteLine(protagonist.Name+":");
			Console.WriteLine(protagonist.Stats[Characters.StatType.Health].Current + " / " + protagonist.Stats[Characters.StatType.Health].Max);

			Console.WriteLine("");

			Characters.Character antagonist = _game.Battlefield.Antagonists[0].Character;
			Console.WriteLine(antagonist.Name + ":");
			Console.WriteLine(antagonist.Stats[Characters.StatType.Health].Current + " / " + antagonist.Stats[Characters.StatType.Health].Max);
			/*
			Console.Clear();
			Console.WriteLine(_tickOrTock ? "tick" : "tock");
			_tickOrTock = !_tickOrTock;
			*/
		}
	}
}