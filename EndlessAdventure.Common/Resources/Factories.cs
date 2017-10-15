using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Equipments;
using EndlessAdventure.Common.Equipments.Effects;

namespace EndlessAdventure.Common.Resources
{
	/// <summary>
	/// Creates Combatants.
	/// </summary>
	public class CombatantFactory {

		public static Combatant CreateCombatant(string name = "",
																						int attack = -1,
																						int defense = -1,
																						int health = -1,
																						int energy = -1,
																						int strength = -1,
																						int dexterity = -1,
																						int vitality = -1,
																						int intelligence = -1,
																						int luck = -1) {

			return new Combatant(CharacterFactory.CreateCharacter(name, 
																														attack,
																														defense,
																														health,
																														energy,
																														strength,
																														dexterity,
																														vitality,
																														intelligence,
																														luck),
													 InventoryFactory.CreateInventory(),
													 Defaults.CombatantLevel,
													 Defaults.CombatantExpReward);

			if (attack == -1) attack = Defaults.CharacterAttack;
			if (defense == -1) defense = Defaults.CharacterDefense;
			if (health == -1) health = Defaults.CharacterHealth;
			if (energy == -1) energy = Defaults.CharacterEnergy;
			if (strength == -1) strength = Defaults.CharacterStrength;
			if (dexterity == -1) dexterity = Defaults.CharacterDexterity;
			if (vitality == -1) vitality = Defaults.CharacterVitality;
			if (intelligence == -1) intelligence = Defaults.CharacterIntelligence;
			if (luck == -1) luck = Defaults.CharacterLuck;
		}

		public static Combatant CreateCombatant(string name, StatType pType, int value) {

			Combatant created = new Combatant(CharacterFactory.CreateCharacter(name, pType, value),
																				InventoryFactory.CreateInventory(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}

		public static Combatant CreateCombatant(StatType pType, int value) {

			Combatant created = new Combatant(CharacterFactory.CreateCharacter(pType, value),
																				InventoryFactory.CreateInventory(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}

		public static Combatant CreateCombatant(string name) {

			Combatant created = new Combatant(CharacterFactory.CreateCharacter(name),
																				InventoryFactory.CreateInventory(),
																				Defaults.CombatantLevel,
																				Defaults.CombatantExpReward);
			return created;
		}
	}

	/// <summary>
	/// Creates Characters.
	/// </summary>
	public class CharacterFactory {
		public static Character CreateCharacter(string name = "",
																						int attack = -1,
																						int defense = -1,
																						int health = -1,
																						int energy = -1,
																						int strength = -1,
																						int dexterity = -1,
																						int vitality = -1,
																						int intelligence = -1,
																						int luck = -1) {

			return new Character(name, StatsFactory.CreateStats(attack,
																													defense,
																													health,
																													energy,
																													strength,
																													dexterity,
																													vitality,
																													intelligence,
																													luck));
		}

		public static Character CreateCharacter(string name, StatType pType, int value) {
			return new Character(name, StatsFactory.CreateStats(pType, value));
		}

		public static Character CreateCharacter(StatType pType, int value) {
			return CreateCharacter(Defaults.CharacterName, pType, value);
		}

		public static Character CreateCharacter(string name) {
			return new Character(name, StatsFactory.CreateStats());
		}
	}

	/// <summary>
	/// Creates Stats.
	/// </summary>
	public class StatsFactory {
		public static Dictionary<StatType, Stat> CreateStats(int attack = -1,
																													int defense = -1,
																													int health = -1,
																													int energy = -1,
																													int strength = -1,
																													int dexterity = -1,
																													int vitality = -1,
																													int intelligence = -1,
																													int luck = -1) {

			Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
			stats.Add(StatType.Attack, new Stat(StatType.Attack, attack == -1 ? GetDefaultStatValue(StatType.Attack) : attack));
			stats.Add(StatType.Defense, new Stat(StatType.Defense, defense == -1 ? GetDefaultStatValue(StatType.Defense) : defense));
			stats.Add(StatType.Health, new Stat(StatType.Health, health == -1 ? GetDefaultStatValue(StatType.Health) : health));
			stats.Add(StatType.Energy, new Stat(StatType.Energy, energy == -1 ? GetDefaultStatValue(StatType.Energy) : energy));
			stats.Add(StatType.Strength, new Stat(StatType.Strength, strength == -1 ? GetDefaultStatValue(StatType.Strength) : strength));
			stats.Add(StatType.Dexterity, new Stat(StatType.Dexterity, dexterity == -1 ? GetDefaultStatValue(StatType.Dexterity) : dexterity));
			stats.Add(StatType.Vitality, new Stat(StatType.Vitality, vitality == -1 ? GetDefaultStatValue(StatType.Vitality) : vitality));
			stats.Add(StatType.Intelligence, new Stat(StatType.Intelligence, intelligence == -1 ? GetDefaultStatValue(StatType.Intelligence) : intelligence));
			stats.Add(StatType.Luck, new Stat(StatType.Luck, luck == -1 ? GetDefaultStatValue(StatType.Luck) : luck));

			return stats;
		}

		public static Dictionary<StatType, Stat> CreateStats() {
			Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
			foreach (StatType stat in Enum.GetValues(typeof(StatType))) {
				stats.Add(stat, new Stat(stat, GetDefaultStatValue(stat)));
			}
			return stats;
		}

		public static Dictionary<StatType, Stat> CreateStats(StatType pType, int value) {
			Dictionary<StatType, Stat> stats = new Dictionary<StatType, Stat>();
			foreach (StatType stat in Enum.GetValues(typeof(StatType))) {
				if (stat == pType) {
					stats.Add(stat, new Stat(stat, value));
				}
				else {
					stats.Add(stat, new Stat(stat, GetDefaultStatValue(stat)));
				}
			}
			return stats;
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

	public class InventoryFactory {
		public static Inventory CreateInventory() {

			Inventory inventory = new Inventory(
				new List<Equipment>(),
				new List<Equipment>(),
				new List<Equipment>());

			return inventory;
		}
	}

	public class EquipmentFactory {
		public static Equipment CreateEquipment() {

			Equipment equipment = new Equipment(
					Defaults.EquipmentName,
					EquipmentType.Miscellaneous,
					Defaults.EquipmentCost,
					Defaults.EquipmentDescription,
					new List<IEquipmentEffect>(),
					new List<IEquipmentEffect>());

			return equipment;
		}

		public static Equipment CreateEquipment(EquipmentType type, int value) {

			if (type == EquipmentType.Weapon) {
				return CreateWeapon(value);
			}
			else if (type == EquipmentType.Chestgear) {
				return CreateChestgear(value);
			}
			else {
				Equipment equipment = new Equipment(
					Defaults.EquipmentName,
					type,
					Defaults.EquipmentCost,
					Defaults.EquipmentDescription,
					new List<IEquipmentEffect>(),
					new List<IEquipmentEffect>());

				return equipment;
			}
		}

		public static Equipment CreateWeapon(int value) {
			Equipment equipment = new Equipment(
				"Weapon",
				EquipmentType.Weapon,
				Defaults.EquipmentCost,
				Defaults.EquipmentDescription,
				new List<IEquipmentEffect>(),
				new List<IEquipmentEffect>());

			equipment.EquipEffects.Add(new WeaponEquipEffect("WeaponEquipEffect", value));
			equipment.UnequipEffects.Add(new WeaponUnequipEffect("WeaponUnequipEffect", value));

			return equipment;
		}

		public static Equipment CreateChestgear(int value) {
			Equipment equipment = new Equipment(
				"Armor",
				EquipmentType.Chestgear,
				Defaults.EquipmentCost,
				Defaults.EquipmentDescription,
				new List<IEquipmentEffect>(),
				new List<IEquipmentEffect>());
			equipment.EquipEffects.Add(new ArmorEquipEffect("WeaponEquipEffect", value));
			equipment.UnequipEffects.Add(new ArmorUnequipEffect("WeaponUnequipEffect", value));

			return equipment;
		}
	}

	public class WorldFactory {
		public static World CreateWorld(string name) {
			List<EnemyData> enemyData = Loader.GetEnemyData(name);
			World world = new World(name, enemyData);
			return world;
		}
	}

}