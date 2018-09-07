using System.Collections.Generic;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;

namespace EndlessAdventure.Common.Interfaces
{
    public interface ICombatant
    {
        string Name { get; }
        bool Fallen { get; }

        IEnumerable<AEffect> ActiveEffects { get; }
        IEnumerable<AOnHitBuff> OnHitBuffs { get; }
        
        void AutoHeal();
        bool TryAttack(ICombatant pCombatant, out int pDamage);
        int ApplyPendingDamage();
        // TODO: remove this method
        void DefeatCombatant(ICombatant pCombatant);

        void AddEffect(AEffect pEffect);
        void RemoveEffect(AEffect pEffect);
    }
}