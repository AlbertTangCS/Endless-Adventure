using System;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.ConsoleApp {

	public class Gui : IGui {

		private Game _game;

		public Gui(Game game) {
			_game = game;
		}
		
		public void Render(long frametime) {
			Console.Clear();
			Console.WriteLine("[Frame time: " + frametime + "ms]");
			Console.WriteLine("Current World: " + _game.world.Name);
			Console.WriteLine("");

			foreach (Combatant p in _game.Battlefield.Protagonists) {
				Character protagonist = p.Character;
				Console.WriteLine("Level "+p.Level+": "+protagonist.Name);
				Console.WriteLine("Exp: " + p.Experience + " / " + Defaults.NextLevelExpFormula(p.Level));
				Console.WriteLine("Attack: " + protagonist.PhysicalAttack + ", Defense: " + protagonist.Defense);
				Console.WriteLine("Health: "+protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
			}

			Console.WriteLine("");

			foreach (Combatant a in _game.Battlefield.Antagonists) {
				Character antagonist = a.Character;
				Console.WriteLine("Level "+a.Level+": "+antagonist.Name);
				Console.WriteLine("Attack: " + antagonist.PhysicalAttack + ", Defense: " + antagonist.Defense);
				Console.WriteLine("Health: "+antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
			}
		}
	}
}