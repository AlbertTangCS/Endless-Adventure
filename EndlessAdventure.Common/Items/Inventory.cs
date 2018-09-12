using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Statbuffs;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Items
{
	public class Inventory : IInventory
	{
		private List<Item> _equippables;
		private List<Item> _consumables;
		private List<Item> _miscellaneous;
		
		public Dictionary<ItemType, Item> Equipped { get; private set; }
		public List<Item> Equippables { get; private set; }
		public List<Item> Consumables { get; private set; }
		public List<Item> Miscellaneous { get; private set; }

		public Inventory(Dictionary<ItemType, Item> equipped,
		                 List<Item> equippables,
										 List<Item> consumables,
										 List<Item> miscellaneous) {
			Equipped = equipped ?? new Dictionary<ItemType, Item>();
			Equippables = equippables ?? new List<Item>();
			Consumables = consumables ?? new List<Item>();
			Miscellaneous = miscellaneous ?? new List<Item>();
		}

		public void Equip(Item item, Character character)
		{
			if (item.Type == ItemType.Consumable || item.Type == ItemType.Miscellaneous || !Equippables.Contains(item)) {
				throw new ArgumentException();
			}

			// add/remove buffs depending on what was equipped/unequipped
			Equippables.Remove(item);
			List<AStatBuff> buffsToAdd = null;
			List<AStatBuff> buffsToRemove = null;
			if (Equipped.TryGetValue(item.Type, out Item equipped)) {
				Equipped.Remove(item.Type);
				Equippables.Add(equipped);

				Equipped.Add(item.Type, item);
				buffsToAdd = item.Buffs;
				buffsToRemove = equipped.Buffs;
			}
			else {
				Equipped.Add(item.Type, item);
				buffsToAdd = item.Buffs;
			}

			if (buffsToAdd != null) {
				foreach (AStatBuff buff in buffsToAdd) {
					character.AddBuff(buff);
				}
			}
			if (buffsToRemove != null) {
				foreach (AStatBuff buff in buffsToRemove) {
					character.RemoveBuff(buff);
				}
			}
		}

		public void Unequip(Item equipment, Character character) {
			if (!Equipped.TryGetValue(equipment.Type, out Item equipped)) {
				throw new ArgumentException();
			}

			// remove buffs depending on what was unequipped
			List<AStatBuff> buffsToRemove = null;
			if (equipped == equipment) {
				Equipped.Remove(equipment.Type);
				Equippables.Add(equipped);
				buffsToRemove = equipped.Buffs;
			}
			if (buffsToRemove != null) {
				foreach (AStatBuff buff in buffsToRemove) {
					character.RemoveBuff(buff);
				}
			}
		}

		public void Consume(Item item, Character character) {
			if (item.Type != ItemType.Consumable) throw new ArgumentException();
			if (!Consumables.Contains(item)) return;

			Consumables.Remove(item);
			item.Consume(character);
		}

		public void AddItem(IItem item) {
			if (item.Type == ItemType.Consumable) {
				Consumables.Add(item);
			}
			else if (item.Type == ItemType.Miscellaneous) {
				Miscellaneous.Add(item);
			}
			else {
				Equippables.Add(item);
			}
		}

	}
}