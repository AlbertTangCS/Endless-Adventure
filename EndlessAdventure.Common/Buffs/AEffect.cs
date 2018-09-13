using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs
{
	public abstract class AEffect : IEffect
	{
		protected AEffect(string pName, string pDescription, double pValue, int pDuration)
		{
			Name = pName;
			Description = pDescription;
			Value = pValue;

			DurationTotal = pDuration;
			DurationRemaining = pDuration;
			MaxStacks = 1;
		}

		public string Name { get; }
		public string Description { get; }
		public double Value { get; }
		public int MaxStacks { get; }
		
		public int DurationTotal { get; }
		public int DurationRemaining { get; private set; }
		
		public void Decay()
		{
			if (DurationRemaining > 0)
				DurationRemaining--;
		}
	}
}