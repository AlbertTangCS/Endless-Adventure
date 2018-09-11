using System.Collections.Generic;
using System.Runtime.InteropServices;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;
using EndlessAdventure.Common.Characters;

namespace EndlessAdventure.Common.Interfaces
{
    public interface ICombatant
    {
        string Name { get; }
        string Description { get; }

        int Level { get; }
        int Experience { get; }
        int SkillPoints { get; }

        int BaseBody { get; }
        int Body { get; }
        int BaseMind { get; }
        int Mind { get; }
        int BaseSoul { get; }
        int Soul { get; }
        int BaseFortune { get; }
        int Fortune { get; }

        int BaseHealth { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        int BaseEnergy { get; }
        int CurrentEnergy { get; }
        int MaxEnergy { get; }
        
        int BasePhysicalAttack { get; }
        int PhysicalAttack { get; }
        int BaseEnergyAttack { get; }
        int EnergyAttack { get; }
        int BaseDefense { get; }
        int Defense { get; }
        
        int BaseAccuracy { get; }
        int Accuracy { get; }
        int BaseEvasion { get; }
        int Evasion { get; }

        bool Fallen { get; }
        
        IEnumerable<AEffect> ActiveEffects { get; }
        IEnumerable<AOnHitBuff> OnHitBuffs { get; }

        void ApplyDamage(int pHealth);
        void Heal(int pHealth);
        
        void AddExperience(int pExperience);
        bool AddSkillPoint(StatType pType, int pCount = 1);
        
        void AutoHeal();
        bool TryAttack(ICombatant pCombatant, out int pDamage);
        int ApplyPendingDamage();
        // TODO: remove this method
        void DefeatCombatant(ICombatant pCombatant);

        void AddEffect(AEffect pEffect);
        void RemoveEffect(AEffect pEffect);
    }
}