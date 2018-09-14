using System.Collections.Generic;

namespace EndlessAdventure.Common.Interfaces
{
    public interface ISaveFile
    {
        IEnumerable<ICombatant> GetSavedProtagonists();
        IEnumerable<ICombatant> GetSavedAntagonists();
        IWorld GetCurrentWorld();

        void SaveToDisk();
    }
}