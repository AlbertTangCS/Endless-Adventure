using System.Collections.Generic;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IBattlefield
    {
        IReadOnlyList<ICombatant> Protagonists { get; }
        IReadOnlyList<ICombatant> Antagonists { get; }
        string Message { get; }
        IWorld World { get; }

        void Tick();
        void Flee();
        void TravelToWorld(IWorld pWorld);
    }
}