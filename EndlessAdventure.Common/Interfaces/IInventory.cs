using System.Collections.Generic;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IInventory
    {
        IReadOnlyDictionary<ItemType, IItem> Equipped { get; }
        IEnumerable<IItem> Equippables { get; }
        IEnumerable<IItem> Consumables { get; }
        IEnumerable<IItem> Miscellaneous { get; }
    }
}