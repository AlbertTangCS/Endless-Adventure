using System.Linq;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.ConsoleApp.Views
{
	public class InventoryView : View
	{
		private const string EQUIP_COMMAND = "equip";
		private const string UNEQUIP_COMMAND = "unequip";
		private const string USE_COMMAND = "use";

		public InventoryView(Game pGame) : base(pGame)
		{
			_commandDictionary.Add(EQUIP_COMMAND, " <equipment>: Equip given equipment.");
			_commandDictionary.Add(UNEQUIP_COMMAND, " <equipment>: Unequips given equipment. \"weapon\" unequips current weapon.");
			_commandDictionary.Add(USE_COMMAND, " <consumable>: Uses the given item.");
		}

		public override void ProcessInput(string[] pArgs)
		{
			var protagonist = _game.Battlefield.Protagonists[0];
			switch (pArgs[0])
			{
				case EQUIP_COMMAND:
					if (pArgs.Length == 1) {
						_parseMessage = "Missing argument.";
						break;
					}
					var equipment = protagonist.Inventory.Equippables.FirstOrDefault(x => x.Name == pArgs[1]);
					if (equipment != null) {
						protagonist.TryEquipItem(equipment, out var unequipped);
						_parseMessage = equipment.Name + " equipped.";
					}
					else {
						_parseMessage = "Invalid argument.";
					}
					break;

				case UNEQUIP_COMMAND:
					if (pArgs.Length == 1) {
						_parseMessage = "Missing argument.";
						break;
					}
					switch (pArgs[1]) {
						case "weapon":
							if (protagonist.Inventory.Equipped.TryGetValue(ItemType.Weapon, out var weapon)) {
								protagonist.UnequipItem(weapon);
								_parseMessage = weapon.Name + " unequipped.";
							}
							break;

						default:
							var unequipped = protagonist.Inventory.Equipped.Values.FirstOrDefault(x => x.Name == pArgs[1]);
							if (unequipped != null) {
								protagonist.UnequipItem(unequipped);
							}
							else {
								_parseMessage = "Invalid argument.";
							}
							break;
					}
					break;
					
				case USE_COMMAND:
					if (pArgs.Length == 1) {
						_parseMessage = "Missing argument.";
						break;
					}
					var consumable = protagonist.Inventory.Consumables.FirstOrDefault(x => x.Name == pArgs[1]);
					if (consumable != null) {
						protagonist.ConsumeItem(consumable);
						_parseMessage = consumable.Name + " consumed.";
					}
					else {
						_parseMessage = "Invalid consumable.";
					}
					break;

				default:
					_parseMessage = "Invalid command.";
					break;
			}
		}
	}
}