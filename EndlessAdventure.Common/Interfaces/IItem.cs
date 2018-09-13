using System.Collections.Generic;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IItem
    {
        string Name { get; }
        string Description { get; }
        ItemType Type { get; }
        int Cost { get; }

        IEnumerable<IEffect> EquipEffects { get; }
    }
}