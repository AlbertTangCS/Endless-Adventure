using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.Statbuffs {
	public class BuffStatAdditive : AStatBuff {

		public BuffStatAdditive(string pName, string pDescription, double pValue, int pDuration, StatType pType) :
				base(pName, pDescription, pValue, pDuration, pType) { }

		public override int GetStatBonus(Character c) {
			return (int)Value;
		}
	}
}