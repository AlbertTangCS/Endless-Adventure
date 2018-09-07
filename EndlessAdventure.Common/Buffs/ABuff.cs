using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs
{
	public abstract class ABuff
	{

		protected ABuff(string pName, string pDescription, double pValue, int pDuration)
		{
			Name = pName;
			Description = pDescription;
			Value = pValue;

			DurationTotal = pDuration;
			DurationRemaining = pDuration;
		}

		public string Name { get; }
		public string Description { get; }
		public double Value { get; }

		public int DurationTotal { get; }
		public int DurationRemaining { get; private set; }
		
		public void Decay()
		{
			if (DurationRemaining > 0)
				DurationRemaining--;
		}

		public abstract void Equip(ICombatant c);
		public abstract void Unequip(ICombatant c);
	}
}