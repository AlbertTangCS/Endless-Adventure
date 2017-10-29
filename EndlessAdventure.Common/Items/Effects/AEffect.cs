using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Items.Consumables.Effects
{
	public abstract class AEffect
	{
		public string Name { get; private set; }
		public StatType Type { get; private set; }
		public double Value { get; private set; }

		public AEffect(string name, StatType type, double value) {
			Name = name;
			Type = type;
			Value = value;
		}

		public abstract void ApplyEffect(Character character);
	}
}