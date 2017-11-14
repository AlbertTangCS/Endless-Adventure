using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs {
	public abstract class AOnHitBuff : ABuff {

		public AOnHitBuff(string pName, string pDescription, double pValue, int pDuration) :
				base(pName, pDescription, pValue, pDuration) { }

		public abstract void ApplyToEnemy(Character enemy);
	}
}