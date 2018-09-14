using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.ConsoleApp
{
    public class SaveFile : ISaveFile
    {
        public IEnumerable<ICombatant> GetSavedProtagonists()
        {
            var protagonist = new Combatant("Player", "main player", 0, 1, 3, 3, 3, 0, 0);
            return new List<ICombatant> { protagonist };
        }

        public IEnumerable<ICombatant> GetSavedAntagonists()
        {
            return new List<ICombatant>();
        }
        
        public IWorld GetCurrentWorld()
        {
            return Factory.CreateWorld(Database.KEY_WORLD_GREEN_PASTURES);
        }

        public void SaveToDisk()
        {
            throw new System.NotImplementedException();
        }
    }
}