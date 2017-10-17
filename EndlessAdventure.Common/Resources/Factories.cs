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

		public static Combatant CreateCombatant(CombatantData data) {

			return CreateCombatant(name: data.Name,
														 attack: data.Attack,
														 defense: data.Defense,
														 health: data.Health,
														 energy: data.Energy,
														 expReward: data.ExpReward,
														 strength: data.Strength,
														 dexterity: data.Dexterity,
														 vitality: data.Vitality,
														 intelligence: data.Intelligence,
														 luck: data.Luck);
		}

		public static Combatant CreateCombatant(string name = "",
																						int attack = -1,
																						int defense = -1,
																						int health = -1,
																						int energy = -1,
																						int expReward = -1,
																						int strength = -1,
																						int dexterity = -1,
																						int vitality = -1,
																						int intelligence = -1,
																						int luck = -1) {

			return new Combatant(CharacterFactory.CreateCharacter(name, 
																														attack: attack,
																														defense: defense,
																														health: health,
																														energy: energy,
																														strength: strength,
																														dexterity: dexterity,
																														vitality: vitality,
																														intelligence: intelligence,
																														luck: luck),
													 InventoryFactory.CreateInventory(),
													 Defaults.CombatantLevel,
													 expReward == -1 ? Defaults.CombatantExpReward : expReward);
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
			List<CombatantData> enemyData = Loader.GetEnemyData(name);
			World world = new World(name, enemyData);
			return world;
		}
	}

}