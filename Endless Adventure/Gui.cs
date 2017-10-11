using System;

namespace EndlessAdventure {

	public class Gui {

		private Game _game;

		public Gui(Game game) {
			_game = game;
		}
		
		public void Render(long frametime) {
			Console.Clear();
			Console.WriteLine("[Frame time: " + frametime + "ms]");
			Console.WriteLine("");

			foreach (Battle.Combatant p in _game.Battlefield.Protagonists) {
				Characters.Character protagonist = p.Character;
				Console.WriteLine(protagonist.Name + ":");
				Console.WriteLine(protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
			}

			Console.WriteLine("");

			foreach (Battle.Combatant a in _game.Battlefield.Antagonists) {
				Characters.Character antagonist = a.Character;
				Console.WriteLine(antagonist.Name + ":");
				Console.WriteLine(antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
			}
		}
	}
}