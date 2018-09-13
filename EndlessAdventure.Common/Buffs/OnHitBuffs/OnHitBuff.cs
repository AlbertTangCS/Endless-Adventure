using System.Collections.Generic;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs
{
  public class OnHitBuff : AEffect, IOnHitEffect
  {
		private readonly List<IEffect> _effectsToApply;

		public OnHitBuff(string pName, string pDescription, double pValue, int pDuration, List<IEffect> pEffectsToApply) : base(pName, pDescription, pValue, pDuration)
		{
			_effectsToApply = pEffectsToApply;
		}

		public void ApplyToEnemy(ICombatant pCombatant)
		{
			foreach (var effect in _effectsToApply)
			{
				pCombatant.AddEffect(effect);
			}
		}
  }
}