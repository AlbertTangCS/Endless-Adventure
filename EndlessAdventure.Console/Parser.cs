using System;
using System.Linq;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure {
	public enum Mode {
		Game, Inventory, Buffs
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

				case "consume":

					break;

				case "buffs":
					Mode = Mode.Buffs;
					break;

				case "equip":
					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					Equipment equipment = protagonist.Inventory.Equippables.FirstOrDefault(x => x.Name == args[1]);
					if (equipment != null) {
						protagonist.Equip(equipment);
						_message = equipment.Name + " equipped.";
					}
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

				case "unequip":

					if (args.Length == 1) {
						_message = "Missing argument.";
						break;
					}
					switch (args[1]) {
						case "weapon":
							if (protagonist.Inventory.Equipped.TryGetValue(ItemType.Weapon, out Equipment weapon)) {
								protagonist.Unequip(weapon);
								_message = weapon.Name + " unequipped.";
							}
							break;

						default:
							Equipment unequipped = protagonist.Inventory.Equipped.Values.FirstOrDefault(x => x.Name == args[1]);
							if (unequipped != null) {
								protagonist.Unequip(unequipped);
							}
							break;
					}

					break;

				default:
					_message = "Invalid command.";
					break;
			}
		}
	}
}