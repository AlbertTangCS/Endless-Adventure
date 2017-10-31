using System;
using System.Linq;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure {
	public enum Mode {
		Game, Inventory, Skills, Buffs
	}

	public class Parser {

		public static Game Game;
		public static Mode Mode = Mode.Game;

		private static string _message;
		public static string Message {
			get {
				string message = _message;
				_message = null;
				return message;
			}
		}

		public static void Parse(string input) {
			if (Game == null) {
				_message = "Game not set in Parser.";
				return;
			}

			string[] args = input.Split(' ');

			Combatant protagonist = Game.Battlefield.Protagonists[0];
			args[0] = args[0].ToLower();
			switch (args[0]) {
				case "":
					if (Mode == Mode.Game) {
						Game.Update();
					}
					else {
						_message = "Invalid command.";
					}
					break;

				case "addskill":
					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					int numpoints = -1;
					if (args.Length == 3) {
						if (Int32.TryParse(args[2], out int parsed) && parsed>0 && parsed<=protagonist.SkillPoints) {
							numpoints = parsed;
						}
						else {
							_message = "Invalid number of points to allocate.";
							break;
						}
					}

					StatType type = StatType.Fortune;
					switch (args[1]) {
						case "body":
							type = StatType.Body;
							break;
						case "mind":
							type = StatType.Mind;
							break;
						case "soul":
							type = StatType.Soul;
							break;
					}
					if (type != StatType.Fortune) {
						for (int i = 0; i < numpoints; i++) {
							protagonist.AddSkillPoint(type);
						}
						_message = numpoints + " skill point(s) added to " + args[1] + "!";
					}
					else {
						_message = "Invalid skill.";
					}
					break;

				case "buffs":
					Mode = Mode.Buffs;
					break;

				case "equip":
					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					Item equipment = protagonist.Inventory.Equippables.FirstOrDefault(x => x.Name == args[1]);
					if (equipment != null) {
						protagonist.Equip(equipment);
						_message = equipment.Name + " equipped.";
					}
					else {
						_message = "Invalid equipment.";
					}
					break;

				case "flee":
					Game.Battlefield.Flee();
					_message = "Fled from enemy!";
					break;

				case "game":
					Mode = Mode.Game;
					break;

				case "inventory":
					Mode = Mode.Inventory;
					break;

				case "run":
					if (Mode == Mode.Game) {
						if (args.Length == 1) {
							_message = "Missing argument.";
							break;
						}
						if (Int32.TryParse(args[1], out int parsed) && parsed > 0) {
							Game.Update(parsed);
						}
					}
					else {
						_message = "Invalid command.";
					}
					break;

				case "skills":
					Mode = Mode.Skills;
					break;

				case "unequip":

					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					switch (args[1]) {
						case "weapon":
							if (protagonist.Inventory.Equipped.TryGetValue(ItemType.Weapon, out Item weapon)) {
								protagonist.Unequip(weapon);
								_message = weapon.Name + " unequipped.";
							}
							break;

						default:
							Item unequipped = protagonist.Inventory.Equipped.Values.FirstOrDefault(x => x.Name == args[1]);
							if (unequipped != null) {
								protagonist.Unequip(unequipped);
							}
							else {
								_message = "Invalid equipment.";
							}
							break;
					}

					break;

				case "use":
					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					Item consumable = protagonist.Inventory.Consumables.FirstOrDefault(x => x.Name == args[1]);
					if (consumable != null) {
						protagonist.Consume(consumable);
						_message = consumable.Name + " consumed.";
					}
					else {
						_message = "Invalid consumable.";
					}
					break;

				default:
					_message = "Invalid command.";
					break;
			}
		}
	}
}