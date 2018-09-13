namespace EndlessAdventure.Common.Interfaces
{
    public interface IActiveEffect : IEffect
    {
        void ApplyEffect(ICombatant pCombatant);
    }
}