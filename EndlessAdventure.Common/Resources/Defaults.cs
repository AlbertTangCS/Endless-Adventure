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
			double defense = 0.5 * body + 0.4 * mind + 0.25 * soul;
			return (int)Math.Round(defense);
		}

		public static int CalculateAccuracy(int body, int mind, int soul) {
			double defense = 0.1 * body + 0.9 * mind + 0 * soul;
			return (int)Math.Round(defense);
		}

		public static int CalculateEvasion(int body, int mind, int soul) {
			double defense = 0.3 * body + 0.7 * mind + 0 * soul;
			return (int)Math.Round(defense);
		}

		private static readonly int CONST_HIT_CHANCE = 10;
		private static readonly int CONST_MISS_CHANCE = 5;
		private static readonly int AVERAGE_HIT_CHANCE = 80;
		private static readonly int DIFFERENCE_MULTIPLIER = 5;
		public static bool DidMiss(int pAccuracy, int pEvasion) {
			int chance = Utilities.Random.Next(100);
			if (chance < CONST_HIT_CHANCE)
				return false;
			else if (chance >= 100-CONST_MISS_CHANCE)
				return true;
			else {
				int difference = Math.Abs(pAccuracy - pEvasion);
				int total = 0;
				for (int i = 0; i < difference; i++) {
					total += i + 1;
				}
				int success = AVERAGE_HIT_CHANCE + (pAccuracy < pEvasion ? -1 * total : total);
				return chance >= success;
			}
		}
	}
}