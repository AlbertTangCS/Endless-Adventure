using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Equipments.Effects;

namespace EndlessAdventure.Common.Equipments {

	public class Equipment {

		public string Name { get; private set; }
		public EquipmentType Type { get; private set; }
		public int Cost { get; private set; }
		public string Description { get; private set; }
		public List<IEquipmentEffect> EquipEffects { get; private set; }
		public List<IEquipmentEffect> UnequipEffects { get; private set; }

		public Equipment(string name,
										 EquipmentType type,
										 int cost,
										 string description,
										 List<IEquipmentEffect> equipEffects,
										 List<IEquipmentEffect> unequipEffects) {

			if (name == null || description == null ||  equipEffects == null || unequipEffects == null) throw new ArgumentException();

			Name = name;
			Type = type;
			Cost = cost;
			Description = description;
			EquipEffects = equipEffects;
			UnequipEffects = unequipEffects;
		}

	}
}