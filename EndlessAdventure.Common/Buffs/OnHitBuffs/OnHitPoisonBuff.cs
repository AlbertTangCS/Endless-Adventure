using System.Linq;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs {
	public class OnHitPoisonBuff : AOnHitBuff {
		public OnHitPoisonBuff(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration) {
		}

		public override void ApplyToEnemy(Character enemy) {
			var isEffect = Database.Effects.TryGetValue(Database.KEY_EFFECT_POISON, out var effectResult);
			if (isEffect) {
				var newEffect = effectResult(Value, DurationRemaining);
				var stackCount = enemy.ActiveEffects.Count(x => x.Name == newEffect.Name);
				if (stackCount < newEffect.StackCount)
					enemy.AddEffect(effectResult(Value, DurationRemaining));
			}
		}
	}
}