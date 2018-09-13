using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Statbuffs
{
	public class BuffStatAdditive : AStatBuff
	{
		public BuffStatAdditive(string pName, string pDescription, double pValue, int pDuration, StatType pType) : base(pName, pDescription, pValue, pDuration, pType)
		{
		}
		
		public override int GetStatBonus(ICombatant pCombatant)
		{
			return (int)Value;
		}
	}
}