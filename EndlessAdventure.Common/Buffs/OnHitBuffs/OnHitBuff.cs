using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.OnHitBuffs
{
	public class OnHitBuff : AEffect, IOnHitEffect
	{
		private readonly Func<IEnumerable<IEffect>> _getApplyEffects;

		public OnHitBuff(string pName, string pDescription, double pValue, int pDuration, Func<IEnumerable<IEffect>> pGetApplyEffects) : base(pName, pDescription, pValue, pDuration)
		{
			_getApplyEffects = pGetApplyEffects;
		}
		
		public void ApplyToEnemy(ICombatant pCombatant)
		{
			var effects = _getApplyEffects();
			foreach (var effect in effects)
			{
				pCombatant.AddEffect(effect);
			}
		}
	}
}