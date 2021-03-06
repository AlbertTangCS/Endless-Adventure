using System.Collections.Generic;
using System.Runtime.InteropServices;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Buffs.Effects;
using EndlessAdventure.Common.Buffs.OnHitBuffs;

namespace EndlessAdventure.Common.Interfaces
{
    public interface ICombatant
    {
        string Name { get; }
        string Description { get; }
        int ExpReward { get; }

        IInventory Inventory { get; }
        
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
        
        IEnumerable<IActiveEffect> ActiveEffects { get; }
        IEnumerable<IOnHitEffect> OnHitBuffs { get; }
        IEnumerable<IStatBuff> StatBuffs { get; }
        
        void AddExperience(int pExperience);
        bool AddSkillPoint(StatType pType, int pCount = 1);
        
        void AutoHeal();
        void AddPendingDamage(int pDamage);
        int ApplyPendingDamage();

        void AddEffect(IEffect pEffect);
        void RemoveEffect(IEffect pEffect);
        
        void AddItem(IItem pItem);
        void RemoveItem(IItem pItem);
        bool TryEquipItem(IItem pItem, out IItem pUnequipped);
        void UnequipItem(IItem pItem);
        void ConsumeItem(IItem pItem);
    }
}