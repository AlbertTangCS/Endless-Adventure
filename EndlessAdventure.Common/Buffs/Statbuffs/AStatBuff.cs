using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.Statbuffs {
	public abstract class AStatBuff : ABuff {

		public StatType StatType { get; private set; }

		public AStatBuff(string pName, string pDescription, double pValue, int pDuration, StatType pStat) :
				base(pName, pDescription, pValue, pDuration) {

			StatType = pStat;
		}
		
		public abstract int GetStatBonus(Character character);
	}
}