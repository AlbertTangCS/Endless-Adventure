using System;

namespace EndlessAdventure.Common.Resources
{
	public static class Defaults
	{
		public static string CharacterName = "Character";

		private const int COMBATANT_BASE_LEVEL_EXP = 10;
		public static int NextLevelExpFormula(int level)
		{
			var exp = (COMBATANT_BASE_LEVEL_EXP + ((level - 1) * 5));
			return exp;
		}

		private const int HEALTH_MULTIPLIER = 5;
		public static int CalculateHealth(int body, int mind, int soul)
		{
			var health = (0.60 * body + 0.25 * mind + 0.15 * soul) * HEALTH_MULTIPLIER;
			return (int)Math.Round(health);
		}

		private const int ENERGY_MULTIPLIER = 3;
		public static int CalculateEnergy(int body, int mind, int soul)
		{
			var energy = (0.60 * soul + 0.40 * mind) * ENERGY_MULTIPLIER;
			return (int)Math.Round(energy);
		}

		public static int CalculatePhysicalAttack(int body, int mind, int soul)
		{
			var physicalAttack = 0.8 * body + 0.2 * mind;
			return (int)Math.Round(physicalAttack);
		}

		public static int CalculateEnergyAttack(int body, int mind, int soul)
		{
			var energyAttack = 0.9 * soul + 0.1 * mind;
			return (int)Math.Round(energyAttack);
		}

		public static int CalculateDefense(int body, int mind, int soul)
		{
			var defense = 0.4 * body + 0.5 * mind + 0.25 * soul;
			return (int)Math.Round(defense);
		}

		public static int CalculateAccuracy(int body, int mind, int soul)
		{
			var defense = 0.1 * body + 0.9 * mind;
			return (int)Math.Round(defense);
		}

		public static int CalculateEvasion(int body, int mind, int soul)
		{
			var defense = 0.3 * body + 0.7 * mind;
			return (int)Math.Round(defense);
		}

		private const int CONST_HIT_CHANCE = 10;
		private const int CONST_MISS_CHANCE = 5;
		private const int AVERAGE_HIT_CHANCE = 75;
		private const int DIFFERENCE_MULTIPLIER = 2;

		public static int HitChance(int pAccuracy, int pEvasion)
		{
			var difference = Math.Abs(pAccuracy - pEvasion);
			var total = 0;
			for (var i = 0; i < difference; i++)
			{
				total += DIFFERENCE_MULTIPLIER;
			}
			return AVERAGE_HIT_CHANCE + (pAccuracy < pEvasion ? -1 * total : total);
		}

		public static bool DidMiss(int pAccuracy, int pEvasion)
		{
			var chance = Utilities.Random.Next(100);
			
			if (chance < CONST_HIT_CHANCE)
				return false;
			
			if (chance >= 100-CONST_MISS_CHANCE)
				return true;
			
			return chance >= HitChance(pAccuracy, pEvasion);
		}
	}
}