using System.Collections.Generic;

namespace EndlessAdventure.Common.Interfaces
{
    public interface ISaveFile
    {
        IEnumerable<ICombatant> GetSavedProtagonists();
        IWorld GetCurrentWorld();

        void SaveToDisk();
    }
}