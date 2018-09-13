namespace EndlessAdventure.Common.Interfaces
{
    public interface IOnHitEffect : IEffect
    {
        void ApplyToEnemy(ICombatant pCombatant);
    }
}