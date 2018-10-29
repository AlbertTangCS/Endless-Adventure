using System.Collections.Generic;

namespace EndlessAdventure.Common.Resources
{
    public interface IDataProvider
    {
        IDictionary<string, WorldData> GetWorldData();
        IDictionary<string, CombatantData> GetEnemyData();
        IDictionary<string, ItemData> GetItemData();
        IDictionary<string, EffectData> GetEffectData();
    }
}