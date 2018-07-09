using System;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Resources;
using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;

namespace EndlessAdventure.ConsoleApp {

	public class Gui {

		private readonly Game _game;
		private readonly MainView _parser;

		public Gui(Game pGame, MainView pParser) {
			_game = pGame;
			_parser = pParser;
		}
		
		public void Render(long frametime) {
			Console.Clear();
			//Console.WriteLine("[Frame time: " + frametime + "ms]");
			//Console.WriteLine("");

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

		private void PrintGame() {
			Console.WriteLine("<========== " + _game.World.Name.ToUpper() + " ==========>");
			Console.WriteLine("");

			int hitchance = -1;
			foreach (Combatant p in _game.Battlefield.Protagonists) {
				if (hitchance == -1)
					hitchance = p.Character.Accuracy;
				Character protagonist = p.Character;
				Console.Write("<-- Lvl. " + p.Level + " " + protagonist.Name);
				Console.WriteLine(" (" + p.Experience + "/" + Defaults.NextLevelExpFormula(p.Level) + ") -->");
				Console.WriteLine("Health: " + protagonist.CurrentHealth + " / " + protagonist.MaxHealth);
				Console.WriteLine("Energy: " + protagonist.CurrentEnergy + " / " + protagonist.MaxEnergy);
				Console.Write("PA:" + protagonist.PhysicalAttack + "(+" + (protagonist.PhysicalAttack - protagonist.BasePhysicalAttack) + "), ");
				Console.Write("D:" + protagonist.Defense + "(+" + (protagonist.Defense - protagonist.BaseDefense) + "), ");
				Console.Write("ACC:" + protagonist.Accuracy + ", ");
				Console.Write("EVA:" + protagonist.Evasion+"\n");
			}

			Console.WriteLine("");

			foreach (Combatant a in _game.Battlefield.Antagonists) {
				Character antagonist = a.Character;
				Console.WriteLine("<-- " + antagonist.Name +" ("+Defaults.HitChance(hitchance, a.Character.Evasion)+"%) -->");
				Console.WriteLine("Health: " + antagonist.CurrentHealth + " / " + antagonist.MaxHealth);
				Console.WriteLine("Energy: " + antagonist.CurrentEnergy + " / " + antagonist.MaxEnergy);
				Console.Write("PA:" + antagonist.PhysicalAttack + "(+" + (antagonist.PhysicalAttack - antagonist.BasePhysicalAttack) + "), ");
				Console.Write("D:" + antagonist.Defense + "(+" + (antagonist.Defense - antagonist.BaseDefense) + "), ");
				Console.Write("ACC:" + antagonist.Accuracy + ", ");
				Console.Write("EVA:" + antagonist.Evasion + "\n");

			}
		}

		private void DisplayInventory() {
			Console.WriteLine("<========== INVENTORY ==========>");
			Console.WriteLine("");

			Combatant protagonist = _game.Battlefield.Protagonists[0];

			Console.WriteLine("> Equipped");
			foreach (Item equipped in protagonist.Inventory.Equipped.Values) {
				Console.WriteLine(equipped.Name);
			}

			Console.WriteLine("");
			Console.WriteLine("> Equippables");
			Dictionary<string, int> equippableCount = new Dictionary<string, int>();
			foreach (Item equippable in protagonist.Inventory.Equippables) {
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
			Dictionary<string, int> consumableCount = new Dictionary<string, int>();
			foreach (Item consumable in protagonist.Inventory.Consumables) {
				if (!consumableCount.ContainsKey(consumable.Name)) {
					consumableCount.Add(consumable.Name, 1);
				}
				else {
					consumableCount[consumable.Name] += 1;
				}
			}
			foreach (string key in consumableCount.Keys) {
				Console.WriteLine(key + " x" + consumableCount[key]);
			}

			Console.WriteLine("");
			Console.WriteLine("> Miscellaneous");
			Dictionary<string, int> miscellaneousCount = new Dictionary<string, int>();
			foreach (Item miscellaneous in protagonist.Inventory.Miscellaneous) {
				if (!miscellaneousCount.ContainsKey(miscellaneous.Name)) {
					miscellaneousCount.Add(miscellaneous.Name, 1);
				}
				else {
					miscellaneousCount[miscellaneous.Name] += 1;
				}
			}
			foreach (string key in miscellaneousCount.Keys) {
				Console.WriteLine(key + " x" + miscellaneousCount[key]);
			}
		}

		private void DisplaySkills() {
			Console.WriteLine("<========== SKILLS ==========>");
			Console.WriteLine("");

			Combatant protagonist = _game.Battlefield.Protagonists[0];
			Console.WriteLine("POINTS TO ALLOCATE: " + protagonist.SkillPoints);
			Console.WriteLine("Body: " + protagonist.Character.BaseBody + " (+" + (protagonist.Character.Body - protagonist.Character.BaseBody) + ")");
			Console.WriteLine("Mind: " + protagonist.Character.BaseMind + " (+" + (protagonist.Character.Mind - protagonist.Character.BaseMind) + ")");
			Console.WriteLine("Soul: " + protagonist.Character.BaseSoul + " (+" + (protagonist.Character.Soul - protagonist.Character.BaseSoul) + ")");
			Console.WriteLine("Luck: " + protagonist.Luck);
		}

		private void DisplayBuffs() {
			Console.WriteLine("<========== EFFECTS ==========>");
			Console.WriteLine("");

			Console.WriteLine("Buffs:");
			Combatant protagonist = _game.Battlefield.Protagonists[0];
			foreach (StatType type in protagonist.Character.StatBonuses.Keys) {
				Console.WriteLine("Buffs to " + type + ":");
				foreach (AStatBuff buff in protagonist.Character.StatBonuses[type]) {
					Console.WriteLine(buff.Name + " (+" + buff.Value + ")");
				}
			}
			if (protagonist.Character.OnHitBuffs.Count > 0)
				Console.WriteLine("On Hit Effects:");
			foreach (AOnHitBuff onhit in protagonist.Character.OnHitBuffs) {
				Console.WriteLine(onhit.Name + " (" + onhit.Value + "): " + onhit.Description);
			}

			Console.WriteLine("");
			Console.WriteLine("Active Effects:");
			foreach (AEffect effect in protagonist.Character.ActiveEffects) {
				Console.WriteLine(effect.Name + " (" + effect.Value+"): " + effect.Description);
			}
		}

		public void DisplayMessages() {
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