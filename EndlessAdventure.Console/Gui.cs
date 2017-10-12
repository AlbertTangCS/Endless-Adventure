using System;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.ConsoleApp {

	public class Gui : IGui {

		private Game _game;

		public Gui(Game game) {
			_game = game;
		}
		
		public void Render(long frametime) {
			Console.Clear();
			Console.WriteLine("[Frame time: " + frametime + "ms]");
			Console.WriteLine("");

			foreach (Combatant p in _game.Battlefield.Protagonists) {
				Character protagonist = p.Character;
				Console.WriteLine(protagonist.Name + ":");
				Console.WriteLine(protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
			}

			Console.WriteLine("");

			foreach (Combatant a in _game.Battlefield.Antagonists) {
				Character antagonist = a.Character;
				Console.WriteLine(antagonist.Name + ":");
				Console.WriteLine(antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
			}
		}
	}
}