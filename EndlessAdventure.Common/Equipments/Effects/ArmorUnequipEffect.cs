using System;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Equipments.Effects {
	public class ArmorUnequipEffect : IEquipmentEffect {

		public string Name { get; private set; }
		public int Value { get; private set; }

		public ArmorUnequipEffect(string name, int value) {
			Name = name ?? throw new ArgumentException();
			Value = value;
		}

		public void Affect(Character character) {
			character.Stats.TryGetValue(StatType.Defense, out Stat defense);
			defense.Current -= Value;
		}
	}
}