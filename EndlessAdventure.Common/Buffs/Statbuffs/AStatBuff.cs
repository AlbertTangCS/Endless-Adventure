using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;

namespace EndlessAdventure.Common.Buffs.Statbuffs
{
    public abstract class AStatBuff : AEffect, IStatBuff
    {
        protected AStatBuff(string pName, string pDescription, double pValue, int pDuration, StatType pType) : base(pName, pDescription, pValue, pDuration)
        {
            StatType = pType;
        }
        
        public StatType StatType { get; }
        
        public abstract int GetStatBonus(ICombatant pCombatant);
    }
}