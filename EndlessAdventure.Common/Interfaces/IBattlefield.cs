using System.Collections.Generic;

namespace EndlessAdventure.Common.Interfaces
{
    public interface IBattlefield
    {
        IReadOnlyList<ICombatant> Protagonists { get; }
        IReadOnlyList<ICombatant> Antagonists { get; }
        string Message { get; }
        IWorld World { set; }

        void Tick();
        void Flee();
    }
}