using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs {
	public class ABuff {

		public string Name { get; private set; }
		public string Description { get; private set; }
		public double Value { get; private set; }

		public int DurationTotal { get; private set; }
		public int DurationRemaining { get; private set; }

		protected ABuff(string pName, string pDescription, double pValue, int pDuration) {
			Name = pName;
			Description = pDescription;
			Value = pValue;

			DurationTotal = pDuration;
			DurationRemaining = pDuration;
		}

		public void Decay() {
			if (DurationRemaining != 0)
				DurationRemaining--;
		}

		public virtual void Equip(Character c) { }
		public virtual void Unequip(Character c) { }
	}
}