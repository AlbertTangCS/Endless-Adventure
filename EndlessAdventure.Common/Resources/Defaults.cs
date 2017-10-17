using System;

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
		public static int CombatantBaseLevelExp = 10;

		public static string EquipmentName = "Equipment";
		public static int EquipmentCost = 0;
		public static string EquipmentDescription = "Default Equipment.";

		public static int NextLevelExpFormula(int level) {
			int exp = (int)(CombatantBaseLevelExp + ((level - 1) * 5));
			return exp;
		}

		private static int HealthMultiplier = 5;
		public static int CalculateHealth(int body, int mind, int soul) {
			double health = 0.50 * body + 0.50 * mind * HealthMultiplier;
			return (int)Math.Round(health);
		}

		private static int EnergyMultiplier = 3;
		public static int CalculateEnergy(int body, int mind, int soul) {
			double energy = 0.50 * soul + 0.50 * mind * EnergyMultiplier*3;
			return (int)Math.Round(energy);
		}

		public static int CalculatePhysicalAttack(int body, int mind, int soul) {
			double physicalAttack = 0.75 * body + 0.25 * mind;
			return (int)Math.Round(physicalAttack);
		}

		public static int CalculateEnergyAttack(int body, int mind, int soul) {
			double energyAttack = 0.75 * soul + 0.25 * mind;
			return (int)Math.Round(energyAttack);
		}

		public static int CalculateDefense(int body, int mind, int soul) {
			double defense = 0.5 * body + 0.25 * mind + 0.25 * soul;
			return (int)Math.Round(defense);
		}

	}
}