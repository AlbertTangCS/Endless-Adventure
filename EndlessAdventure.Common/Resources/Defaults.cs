using System;

namespace EndlessAdventure.Common.Resources {

	public class Defaults {

		public static string CharacterName = "Character";
		
		public static int CombatantBaseLevelExp = 10;
		public static int NextLevelExpFormula(int level) {
			int exp = (int)(CombatantBaseLevelExp + ((level - 1) * 5));
			return exp;
		}

		private static int HealthMultiplier = 5;
		public static int CalculateHealth(int body, int mind, int soul) {
			double health = (0.60 * body + 0.25 * mind + 0.15 * soul) * HealthMultiplier;
			return (int)Math.Round(health);
		}

		private static int EnergyMultiplier = 3;
		public static int CalculateEnergy(int body, int mind, int soul) {
			double energy = (0.60 * soul + 0.40 * mind) * EnergyMultiplier;
			return (int)Math.Round(energy);
		}

		public static int CalculatePhysicalAttack(int body, int mind, int soul) {
			double physicalAttack = 0.7 * body + 0.3 * mind;
			return (int)Math.Round(physicalAttack);
		}

		public static int CalculateEnergyAttack(int body, int mind, int soul) {
			double energyAttack = 0.85 * soul + 0.15 * mind;
			return (int)Math.Round(energyAttack);
		}

		public static int CalculateDefense(int body, int mind, int soul) {
			double defense = 0.35 * body + 0.5 * mind + 0.25 * soul;
			return (int)Math.Round(defense);
		}

	}
}