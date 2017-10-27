using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items
{
	public class Inventory
	{
		public Dictionary<ItemType, Equipment> Equipped { get; private set; }
		public List<Equipment> Equippables { get; private set; }
		public List<Equipment> Consumables { get; private set; }
		public List<Item> Miscellaneous { get; private set; }

		public Inventory(List<Equipment> equippables,
										 List<Equipment> consumables,
										 List<Item> miscellaneous) {
			Equipped = new Dictionary<ItemType, Equipment>(); ;
			Equippables = equippables;
			Consumables = consumables;
			Miscellaneous = miscellaneous;
		}

		public void Equip(Equipment equipment, out List<ABuff> equip, out List<ABuff> unequip) {
			if (equipment.Type == ItemType.Consumable || equipment.Type == ItemType.Miscellaneous) {
				throw new ArgumentException();
			}

			if (Equipped.TryGetValue(equipment.Type, out Equipment equipped)) {
				Equipped.Remove(equipment.Type);
				Equipped.Add(equipment.Type, equipment);
				Equippables.Add(equipped);

				equip = equipment.Buffs;
				unequip = equipped.Buffs;
			}
			else {
				Equippables.Remove(equipment);

				Equipped.Add(equipment.Type, equipment);
				equip = equipment.Buffs;
				unequip = new List<ABuff>();
			}
		}

		public void Unequip(Equipment equipment, out List<ABuff> unequip) {
			Equipped.TryGetValue(equipment.Type, out Equipment equipped);
			if (equipped == equipment) {
				Equipped.Remove(equipment.Type);
				Equippables.Add(equipped);
				unequip = equipped.Buffs;
			}
			else {
				unequip = new List<ABuff>();
			}
		}

		public void Add(Equipment equipment) {
			if (equipment.Type == ItemType.Consumable) {
				Consumables.Add(equipment);
			}
			else if (equipment.Type == ItemType.Miscellaneous) {
				Miscellaneous.Add(equipment);
			}
			else {
				Equippables.Add(equipment);
			}
		}

	}
}