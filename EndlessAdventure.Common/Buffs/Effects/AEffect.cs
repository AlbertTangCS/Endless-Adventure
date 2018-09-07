using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Effects
{
	public abstract class AEffect : ABuff
	{
		protected AEffect(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration)
		{
			StackCount = 1;
		}

		public int StackCount { get; }
		
		public override void Equip(ICombatant character)
		{
			if (DurationTotal != 0)
				return;
			
			ApplyEffect(character);
			character.RemoveEffect(this);
		}

		public abstract void ApplyEffect(ICombatant pCombatant);
	}
}