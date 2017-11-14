using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.Statbuffs {
	public class BuffStatMultPhysicalAttack : AStatBuff {

		public BuffStatMultPhysicalAttack(string pName, string pDescription, double pValue, int pDuration) :
				base(pName, pDescription, pValue, pDuration, StatType.PhysicalAttack) { }

		public override int GetStatBonus(Character c) {
			return (int)(Value * c.BasePhysicalAttack);
		}
	}
}