using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs {
	public class OnHitPoisonBuff : AOnHitBuff {
		public OnHitPoisonBuff(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration) {
		}

		public override void ApplyToEnemy(Character enemy) {
			enemy.AddBuff(Database.Buffs[Database.KEY_EFFECT_POISON](Value, DurationRemaining));
		}
	}
}