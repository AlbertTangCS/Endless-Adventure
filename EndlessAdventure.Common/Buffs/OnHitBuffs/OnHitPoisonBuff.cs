using System.Linq;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs
{
	public class OnHitPoisonBuff : AEffect, IOnHitEffect
	{
		public OnHitPoisonBuff(string pName, string pDescription, double pValue, int pDuration) : base(pName, pDescription, pValue, pDuration)
		{
		}

		public void ApplyToEnemy(ICombatant pCombatant)
		{
			var isEffect = Database.Effects.TryGetValue(Database.KEY_EFFECT_POISON, out var effectResult);
			if (!isEffect)
				return;
			
			var newEffect = effectResult(Value, DurationRemaining);
			var stackCount = pCombatant.ActiveEffects.Count(x => x.Name == newEffect.Name);
			if (stackCount < newEffect.StackCount)
				pCombatant.AddEffect(effectResult(Value, DurationRemaining));
		}
	}
}