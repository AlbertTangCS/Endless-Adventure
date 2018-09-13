using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Effects
{
	public class HealEffect : AEffect, IActiveEffect
	{
		public HealEffect(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration)
		{
		}

		public void ApplyEffect(ICombatant pCombatant)
		{
			pCombatant.AddPendingDamage((int)(Value * -1));
		}
	}
}