using System;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
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
			Console.WriteLine("");

			switch (Parser.Mode) {
				case Mode.Game:
					PrintGame();
					break;

				case Mode.Inventory:
					DisplayInventory();
					break;

				case Mode.Buffs:
					DisplayBuffs();
					break;
			}
		}

		private void PrintGame() {

			foreach (Combatant p in _game.Battlefield.Protagonists) {
				Character protagonist = p.Character;
				Console.Write("<-- Lvl. " + p.Level + " " + protagonist.Name);
				Console.WriteLine(" (" + p.Experience + "/" + Defaults.NextLevelExpFormula(p.Level) + ") -->");
				Console.WriteLine("Health: " + protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
				Console.WriteLine("Energy: " + protagonist.CurrentEnergy + " / " + protagonist.MaxEnergy);
				Console.Write("PA: " + protagonist.BasePhysicalAttack + " (+" + (protagonist.PhysicalAttack - protagonist.BasePhysicalAttack) + "), ");
				Console.WriteLine("D: " + protagonist.BaseDefense + " (+" + (protagonist.Defense - protagonist.BaseDefense) + ")");
			}

			Console.WriteLine("");

			foreach (Combatant a in _game.Battlefield.Antagonists) {
				Character antagonist = a.Character;
				Console.WriteLine("<-- " + antagonist.Name +" -->");
				Console.WriteLine("Health: " + antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
				Console.WriteLine("Energy: " + antagonist.CurrentEnergy + " / " + antagonist.MaxEnergy);
				Console.Write("PA: " + antagonist.BasePhysicalAttack + " (+" + (antagonist.PhysicalAttack - antagonist.BasePhysicalAttack) + "), ");
				Console.WriteLine("D: " + antagonist.BaseDefense + " (+" + (antagonist.Defense - antagonist.BaseDefense) + ")");
			}

			Console.WriteLine("");
			Console.WriteLine(Parser.Message);
			Console.Write("> ");
		}

		private void DisplayInventory() {
			Combatant protagonist = _game.Battlefield.Protagonists[0];

			Console.WriteLine("Equipped");
			foreach (Item equipped in protagonist.Inventory.Equipped.Values) {
				Console.WriteLine(equipped.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Equippables");
			foreach (Item equippable in protagonist.Inventory.Equippables) {
				Console.WriteLine(equippable.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Consumables");
			foreach (Item consumable in protagonist.Inventory.Consumables) {
				Console.WriteLine(consumable.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Miscellaneous");
			foreach (Item miscellaneous in protagonist.Inventory.Miscellaneous) {
				Console.WriteLine(miscellaneous.Name);
			}

			Console.WriteLine("");
			Console.WriteLine(Parser.Message);
			Console.Write("> ");
		}

		private void DisplayBuffs() {
			Combatant protagonist = _game.Battlefield.Protagonists[0];
			foreach (StatType type in protagonist.Character.StatBonuses.Keys) {
				Console.WriteLine("Type: " + type);
				foreach (ABuff buff in protagonist.Character.StatBonuses[type]) {
					Console.WriteLine(buff.Name + " (" + buff.Value + ")");
				}
			}

			Console.WriteLine("");
			Console.WriteLine(Parser.Message);
			Console.Write("> ");
		}

		public void DisplayQuit() {
			Console.Clear();
			Console.WriteLine("<-----|===========================|----->");
			Console.WriteLine("<-----|                           |----->");
			Console.WriteLine("<-----|   THANK YOU FOR PLAYING   |----->");
			Console.WriteLine("<-----|     ENDLESS ADVENTURE!    |----->");
			Console.WriteLine("<-----|                           |----->");
			Console.WriteLine("<-----|===========================|----->");
			Console.ReadLine();
		}
	}
}