using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs
{
	public abstract class AOnHitBuff : ABuff
	{
		protected AOnHitBuff(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription,pValue, pDuration)
		{
		}

		public abstract void ApplyToEnemy(ICombatant enemy);
	}
}