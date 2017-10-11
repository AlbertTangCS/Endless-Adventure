using EndlessAdventure.Resources;

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

		public Stat(StatType pType) : this (pType, GetDefaultStatValue(pType)) { }

		public Stat(StatType pType, int pBase) {
			Type = pType;
			Base = pBase;
			Current = pBase;
			Max = pBase;
		}

		private static int GetDefaultStatValue(StatType pType) {
			switch (pType) {
				case StatType.Health:
					return Defaults.CharacterHealth;
				case StatType.Energy:
					return Defaults.CharacterEnergy;
				case StatType.Attack:
					return Defaults.CharacterAttack;
				case StatType.Defense:
					return Defaults.CharacterDefense;
				case StatType.Strength:
					return Defaults.CharacterStrength;
				case StatType.Dexterity:
					return Defaults.CharacterDexterity;
				case StatType.Vitality:
					return Defaults.CharacterVitality;
				case StatType.Intelligence:
					return Defaults.CharacterIntelligence;
				default:
					return 0;
			}
		}
	}
}