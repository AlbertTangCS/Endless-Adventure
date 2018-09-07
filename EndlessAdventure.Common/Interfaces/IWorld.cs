using EndlessAdventure.Common.Battle;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IWorld
    {
        string Name { get; }
        string Description { get; }

        Combatant SpawnEnemy();
    }
}