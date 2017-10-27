using System;
using System.Collections.Generic;
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
				Console.WriteLine("Level " + p.Level + ": " + protagonist.Name);
				Console.WriteLine("Exp: " + p.Experience + " / " + Defaults.NextLevelExpFormula(p.Level));
				Console.WriteLine("Attack: " + protagonist.PhysicalAttack + ", Defense: " + protagonist.Defense);
				Console.WriteLine("Health: " + protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
			}

			Console.WriteLine("");

			foreach (Combatant a in _game.Battlefield.Antagonists) {
				Character antagonist = a.Character;
				Console.WriteLine("Level " + a.Level + ": " + antagonist.Name);
				Console.WriteLine("Attack: " + antagonist.PhysicalAttack + ", Defense: " + antagonist.Defense);
				Console.WriteLine("Health: " + antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
			}

			Console.WriteLine("");
			Console.WriteLine(Parser.Message);
			Console.Write("> ");
		}

		private void DisplayInventory() {
			Combatant protagonist = _game.Battlefield.Protagonists[0];

			Console.WriteLine("Equipped");
			foreach (Equipment equipped in protagonist.Inventory.Equipped.Values) {
				Console.WriteLine(equipped.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Equippables");
			foreach (Equipment equippable in protagonist.Inventory.Equippables) {
				Console.WriteLine(equippable.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Consumables");
			foreach (Equipment consumable in protagonist.Inventory.Consumables) {
				Console.WriteLine(consumable.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("Miscellaneous");
			foreach (Equipment equipment in protagonist.Inventory.Miscellaneous) {
				Console.WriteLine(equipment.Name);
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
	}
}