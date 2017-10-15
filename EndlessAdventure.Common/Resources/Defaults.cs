namespace EndlessAdventure.Common.Resources {

	public class Defaults {

		public static string CharacterName = "Character";

		public static int CharacterHealth = 3;
		public static int CharacterEnergy = 0;
		public static int CharacterAttack = 1;
		public static int CharacterDefense = 0;
		public static int CharacterStrength = 1;
		public static int CharacterDexterity = 1;
		public static int CharacterVitality = 1;
		public static int CharacterIntelligence = 1;
		public static int CharacterLuck = 0;

		public static int CombatantLevel = 1;
		public static int CombatantExpReward = 1;
		public static int CombatantBaseLevelExp = 3;
		public static int LevelExpFormula(int level) {
			int exp = (int)(CombatantBaseLevelExp + ((level-1) * 3));
			return exp;
		}

		public static string EquipmentName = "Equipment";
		public static int EquipmentCost = 0;
		public static string EquipmentDescription = "Default Equipment.";
	}
}