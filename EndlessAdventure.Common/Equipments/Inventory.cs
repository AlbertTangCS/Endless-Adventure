using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Equipments.Effects;

namespace EndlessAdventure.Common.Equipments
{
	public class Inventory
	{
		public Dictionary<EquipmentType, Equipment> Equipped { get; private set; }
		public List<Equipment> Equippables { get; private set; }
		public List<Equipment> Consumables { get; private set; }
		public List<Equipment> Miscellaneous { get; private set; }

		public Inventory(List<Equipment> equippables,
										 List<Equipment> consumables,
										 List<Equipment> miscellaneous) {
			Equipped = new Dictionary<EquipmentType, Equipment>(); ;
			Equippables = equippables;
			Consumables = consumables;
			Miscellaneous = miscellaneous;
		}

		public List<IEquipmentEffect> Equip(Equipment equipment) {
			if (equipment.Type == EquipmentType.Consumable || equipment.Type == EquipmentType.Miscellaneous) {
				throw new ArgumentException();
			}

			if (Equipped.TryGetValue(equipment.Type, out Equipment equipped)) {
				Equipped.Remove(equipment.Type);
				Equipped.Add(equipment.Type, equipment);
				Equippables.Add(equipped);

				List<IEquipmentEffect> totalEffects = new List<IEquipmentEffect>();
				totalEffects.AddRange(equipment.EquipEffects);
				totalEffects.AddRange(equipped.UnequipEffects);
				return totalEffects;
			}
			else {
				Equipped.Add(equipment.Type, equipment);
				return equipment.EquipEffects;
			}
		}

		public List<IEquipmentEffect> Unequip(Equipment equipment) {
			Equipped.TryGetValue(equipment.Type, out Equipment equipped);
			if (equipped == equipment) {
				Equipped.Remove(equipment.Type);
				Equippables.Add(equipped);
				return equipped.UnequipEffects;
			}
			else {
				return new List<IEquipmentEffect>();
			}
		}

		public void Add(Equipment equipment) {
			if (equipment.Type == EquipmentType.Consumable) {
				Consumables.Add(equipment);
			}
			else if (equipment.Type == EquipmentType.Miscellaneous) {
				Miscellaneous.Add(equipment);
			}
			else {
				Equippables.Add(equipment);
			}
		}

	}
}