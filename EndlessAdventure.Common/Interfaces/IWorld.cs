namespace EndlessAdventure.Common.Interfaces
{
    public interface IWorld
    {
        string Name { get; }
        string Description { get; }

        ICombatant SpawnEnemy();
    }
}