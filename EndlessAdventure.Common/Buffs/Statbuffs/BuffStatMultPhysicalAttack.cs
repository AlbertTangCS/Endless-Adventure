using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Statbuffs
{
	public class BuffStatMultPhysicalAttack : AStatBuff
	{
		public BuffStatMultPhysicalAttack(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration, StatType.PhysicalAttack)
		{
		}

		public override int GetStatBonus(ICombatant pCombatant)
		{
			return (int)(Value * pCombatant.BasePhysicalAttack);
		}
	}
}