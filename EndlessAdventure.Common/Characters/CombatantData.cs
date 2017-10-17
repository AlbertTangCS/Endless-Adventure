using System;
using System.Collections.Generic;
using System.Text;

namespace EndlessAdventure.Common.Characters
{
	public class CombatantData {

		public string Name { get; private set; }
		public int Weight { get; private set; }
		public string World { get; private set; }

		public int Strength { get; private set; }
		public int Dexterity { get; private set; }
		public int Vitality { get; private set; }
		public int Intelligence { get; private set; }
		public int Luck { get; private set; }

		public int Health { get; private set; }
		public int Energy { get; private set; }
		public int Attack { get; private set; }
		public int Defense { get; private set; }
		public int ExpReward { get; private set; }

		public CombatantData(string name, int weight, string world, int health, int energy, int attack, int defense, int expReward) {
			Name = name;
			Weight = weight;
			World = world;
			Health = health;
			Energy = energy;
			Attack = attack;
			Defense = defense;
			ExpReward = expReward;
		}
	}
}
