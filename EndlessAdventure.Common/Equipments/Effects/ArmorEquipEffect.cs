using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Equipments;

namespace EndlessAdventure.Common.Equipments.Effects {

	public class ArmorEquipEffect : IEquipmentEffect {

		public string Name { get; private set; }
		public int Value { get; private set; }

		public ArmorEquipEffect(string name, int value) {
			Name = name;
			Value = value;
		}

		public void Affect(Character character) {
			//character.Stats.TryGetValue(StatType.Defense, out Stat defense);
			//defense.Current += Value;
		}
	}
}