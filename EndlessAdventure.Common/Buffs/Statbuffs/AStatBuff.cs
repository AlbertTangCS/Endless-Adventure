using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Statbuffs {
	public abstract class AStatBuff : ABuff {

		public StatType StatType { get; private set; }

		protected AStatBuff(string pName, string pDescription, double pValue, int pDuration, StatType pStat) :
				base(pName, pDescription, pValue, pDuration) {

			StatType = pStat;
		}
		
		public abstract int GetStatBonus(ICombatant pCombatant);
	}
}