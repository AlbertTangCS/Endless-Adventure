namespace EndlessAdventure.Characters {

	public enum StatType {
		Health,
		Energy,
		Attack,
		Defense,
		Strength,
		Dexterity,
		Vitality,
		Intelligence,
		Luck
	}

	public class Stat {

		public StatType Type { get; private set; }
		public int Base { get; set; }
		public int Current { get; set; }
		public int Max { get; set; }

		/// <summary>
		/// DO NOT CALL DIRECTLY. Use StatsFactory instead.
		/// Creates a new Stat instance given a type and a base value.
		/// </summary>
		/// <param name="pType">The type of the Stat.</param>
		/// <param name="pBase">The base value of the Stat.</param>
		public Stat(StatType pType, int pBase) {
			Type = pType;
			Base = pBase;
			Current = pBase;
			Max = pBase;
		}
	}
}