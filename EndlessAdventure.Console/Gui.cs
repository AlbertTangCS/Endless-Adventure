using System;
using System.Collections.Generic;

using EndlessAdventure.Common;
using EndlessAdventure.Common.Resources;
using EndlessAdventure.ConsoleApp.Views;

namespace EndlessAdventure.ConsoleApp
{
	public class Gui
	{
		private readonly Game _game;
		private readonly MainView _parser;

		public Gui(Game pGame, MainView pParser)
		{
			_game = pGame;
			_parser = pParser;
		}
		
		public void Render(long frameTime)
		{
			Console.Clear();
			if (frameTime >= 0)
			{
				Console.WriteLine("[Frame time: " + frameTime + "ms]");
			}

			switch (_parser.Mode) {
				case Mode.Battle:
					PrintGame();
					break;

				case Mode.Inventory:
					DisplayInventory();
					break;

				case Mode.Stats:
					DisplaySkills();
					break;

				case Mode.Buffs:
					DisplayBuffs();
					break;
			}

			DisplayMessages();
		}

		private void PrintGame()
		{
			Console.WriteLine("<========== " + _game.Battlefield.World.Name.ToUpper() + " ==========>");
			Console.WriteLine("");

			var hitChance = -1;
			foreach (var protagonist in _game.Battlefield.Protagonists) {
				if (hitChance == -1)
					hitChance = protagonist.Accuracy;
				Console.Write("<-- Lvl. " + protagonist.Level + " " + protagonist.Name);
				Console.WriteLine(" (" + protagonist.Experience + "/" + Defaults.NextLevelExpFormula(protagonist.Level) + ") -->");
				Console.WriteLine("Health: " + protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
				Console.WriteLine("Energy: " + protagonist.CurrentEnergy + " / " + protagonist.MaxEnergy);
				Console.Write("PA:" + protagonist.PhysicalAttack + "(+" + (protagonist.PhysicalAttack - protagonist.BasePhysicalAttack) + "), ");
				Console.Write("D:" + protagonist.Defense + "(+" + (protagonist.Defense - protagonist.BaseDefense) + "), ");
				Console.Write("ACC:" + protagonist.Accuracy + ", ");
				Console.Write("EVA:" + protagonist.Evasion+"\n");
			}

			Console.WriteLine("");

			foreach (var antagonist in _game.Battlefield.Antagonists) {
				Console.WriteLine("<-- " + antagonist.Name +" ("+Defaults.HitChance(hitChance, antagonist.Evasion)+"%) -->");
				Console.WriteLine("Health: " + antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
				Console.WriteLine("Energy: " + antagonist.CurrentEnergy + " / " + antagonist.MaxEnergy);
				Console.Write("PA:" + antagonist.PhysicalAttack + "(+" + (antagonist.PhysicalAttack - antagonist.BasePhysicalAttack) + "), ");
				Console.Write("D:" + antagonist.Defense + "(+" + (antagonist.Defense - antagonist.BaseDefense) + "), ");
				Console.Write("ACC:" + antagonist.Accuracy + ", ");
				Console.Write("EVA:" + antagonist.Evasion + "\n");

			}
		}

		private void DisplayInventory()
		{
			Console.WriteLine("<========== INVENTORY ==========>");
			Console.WriteLine("");

			var protagonist = _game.Battlefield.Protagonists[0];

			Console.WriteLine("> Equipped");
			foreach (var equipped in protagonist.Inventory.Equipped.Values) {
				Console.WriteLine(equipped.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("> Equippables");
			var equippableCount = new Dictionary<string, int>();
			foreach (var equippable in protagonist.Inventory.Equippables) {
				if (!equippableCount.ContainsKey(equippable.Name)) {
					equippableCount.Add(equippable.Name, 1);
				}
				else {
					equippableCount[equippable.Name] += 1;
				}
			}
			foreach (string key in equippableCount.Keys) {
				Console.WriteLine(key + " x" + equippableCount[key]);
			}

			Console.WriteLine("");
			Console.WriteLine("> Consumables");
			var consumableCount = new Dictionary<string, int>();
			foreach (var consumable in protagonist.Inventory.Consumables) {
				if (!consumableCount.ContainsKey(consumable.Name)) {
					consumableCount.Add(consumable.Name, 1);
				}
				else {
					consumableCount[consumable.Name] += 1;
				}
			}
			foreach (var key in consumableCount.Keys) {
				Console.WriteLine(key + " x" + consumableCount[key]);
			}

			Console.WriteLine("");
			Console.WriteLine("> Miscellaneous");
			var miscellaneousCount = new Dictionary<string, int>();
			foreach (var miscellaneous in protagonist.Inventory.Miscellaneous) {
				if (!miscellaneousCount.ContainsKey(miscellaneous.Name)) {
					miscellaneousCount.Add(miscellaneous.Name, 1);
				}
				else {
					miscellaneousCount[miscellaneous.Name] += 1;
				}
			}
			foreach (var key in miscellaneousCount.Keys) {
				Console.WriteLine(key + " x" + miscellaneousCount[key]);
			}
		}

		private void DisplaySkills()
		{
			Console.WriteLine("<========== SKILLS ==========>");
			Console.WriteLine("");

			var protagonist = _game.Battlefield.Protagonists[0];
			Console.WriteLine("POINTS TO ALLOCATE: " + protagonist.SkillPoints);
			Console.WriteLine("Body: " + protagonist.BaseBody + " (+" + (protagonist.Body - protagonist.BaseBody) + ")");
			Console.WriteLine("Mind: " + protagonist.BaseMind + " (+" + (protagonist.Mind - protagonist.BaseMind) + ")");
			Console.WriteLine("Soul: " + protagonist.BaseSoul + " (+" + (protagonist.Soul - protagonist.BaseSoul) + ")");
			Console.WriteLine("Fortune: " + protagonist.Fortune);
		}

		private void DisplayBuffs()
		{
			Console.WriteLine("<========== EFFECTS ==========>");
			Console.WriteLine("");

			Console.WriteLine("Buffs:");
			var protagonist = _game.Battlefield.Protagonists[0];
			
			/*foreach (StatType type in protagonist.Character.StatBonuses.Keys) {
				Console.WriteLine("Buffs to " + type + ":");
				foreach (AStatBuff buff in protagonist.Character.StatBonuses[type]) {
					Console.WriteLine(buff.Name + " (+" + buff.Value + ")");
				}
			}*/
			
			Console.WriteLine("");
			Console.WriteLine("Active Effects:");
			foreach (var effect in protagonist.ActiveEffects) {
				Console.WriteLine(effect.Name + " (" + effect.Value+"): " + effect.Description + " (" + effect.DurationRemaining + "t)");
			}
			
			var titlePrinted = false;
			foreach (var onHit in protagonist.OnHitBuffs) {
				if (!titlePrinted)
				{
					Console.WriteLine("On Hit Effects:");
					titlePrinted = true;
				}
				Console.WriteLine(onHit.Name + " (" + onHit.Value + "): " + onHit.Description);
			}
		}

		private void DisplayMessages() {
			Console.WriteLine("");
			Console.Write(_game.Battlefield.Message);
			Console.WriteLine(_parser.FetchMessage());
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
			Console.WriteLine("");
			Console.WriteLine("Press ENTER to quit.");
			Console.ReadLine();
		}
	}
}