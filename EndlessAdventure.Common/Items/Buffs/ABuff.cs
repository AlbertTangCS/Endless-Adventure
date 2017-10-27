using System;

namespace EndlessAdventure.Common.Characters {
	public abstract class ABuff {

		public string Name { get; private set; }
		public StatType StatType { get; private set; }
		public double Value { get; private set; }

		public ABuff(string name, StatType type, double value) {
			Name = name ?? throw new ArgumentException();
			StatType = type;
			Value = value;
		}

		public abstract int Apply(int pBaseValue);
		public abstract int Unapply(int pBaseValue);
	}
}