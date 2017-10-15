using System;
using System.Collections.Generic;

namespace EndlessAdventure.Common.Equipments {

	public class Equipment {
		public string Name { get; private set; }
		public int Cost { get; private set; }
		public string Description { get; private set; }
		public List<EquipmentEffect> Effects { get; private set; }

		public Equipment(string name, int cost, string description, List<EquipmentEffect> effects) {
			if (name == null || description == null || effects == null) throw new ArgumentException();
			Name = name;
			Cost = cost;
			Description = description;
			Effects = effects;
		}

	}
}