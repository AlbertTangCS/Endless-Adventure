
using System;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Resources
{
	public static class Factory
	{
		public static IWorld CreateWorld(string pWorldKey)
		{
			
		}
		
		public static ICombatant CreateCombatant(string pCombatantKey)
		{
			
		}
		
		public static IItem CreateItem(string pItemKey)
		{
			
		}
		
		public static IEffect CreateEffect(string pEffectKey)
		{
			switch (pEffectKey)
			{
				// active effects
				case Database.KEY_EFFECT_HEAL:
					break;
				case Database.KEY_EFFECT_POISON:
					break;
				
				// stat buffs
				case Database.KEY_STATBUFF_INC_PHYSICAL_ATTACK:
					break;
				case Database.KEY_STATBUFF_MULT_PHYSICAL_ATTACK:
					break;
				case Database.KEY_STATBUFF_INC_DEFENSE:
					break;
				case Database.KEY_STATBUFF_INC_EVASION:
					break;
				
				// on hits
				
				default:
					throw new ArgumentException();
			}
		}
	}
}