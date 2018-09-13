namespace EndlessAdventure.Common.Interfaces
{
    public interface IEffect
    {
        string Name { get; }
        string Description { get; }
        double Value { get; }
        int MaxStacks { get; }
		
        int DurationTotal { get; }
        int DurationRemaining { get; }

        void Decay();
    }
}