namespace EndlessAdventure.Common.Characters {
	public interface IBuff {

		StatType StatType { get; }
		int Apply(int stat);

	}
}