using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IItem
    {
        ItemType Type { get; }
    }
}