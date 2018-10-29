namespace EndlessAdventure.Common.Resources
{
    public enum EffectType
    {
        Active,
        Stat,
        OnHit
    }
    
    public struct EffectData
    {
        public EffectData(string pName, string pDescription, EffectType pEffectType, int pMaxStacks = 1)
        {
            Name = pName;
            Description = pDescription;
            EffectType = pEffectType;
            MaxStacks = pMaxStacks;
        }
        
        public string Name { get; }
        public string Description { get; }
        public EffectType EffectType { get; }
        public int MaxStacks { get; }
    }
}