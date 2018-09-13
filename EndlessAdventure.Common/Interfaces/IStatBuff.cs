using EndlessAdventure.Common.Battle;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IStatBuff : IEffect
    {
        StatType StatType { get; }
        int GetStatBonus(ICombatant pCombatant);
    }
}