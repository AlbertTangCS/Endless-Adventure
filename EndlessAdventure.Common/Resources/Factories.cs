using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Items.Effects;

namespace EndlessAdventure.Common.Resources
{
	/// <summary>
	/// Creates Combatants.
	/// </summary>
	public class CombatantFactory {

		public static Combatant CreateCombatant(CombatantData data) {

			return CreateCombatant(name: data.Name,
														 description: data.Description,
														 expReward: data.ExpReward,
														 body: data.Body,
														 mind: data.Mind,
														 soul: data.Soul,
														 fortune: data.Fortune);
		}

		public static Combatant CreateCombatant(string name = "",
																						string description = "",
																						int level = -1,
																						int expReward = -1,
																						int body = -1,
																						int mind = -1,
																						int soul = -1,
																						int fortune = -1) {

			return new Combatant(CharacterFactory.CreateCharacter(name: name,
																														description: description,
																														body: body,
																														mind: mind,
																														soul: soul,
																														fortune: fortune),
													 level == -1 ? Defaults.CombatantLevel : level,
													 expReward == -1 ? Defaults.CombatantExpReward : expReward);
		}
	}

	/// <summary>
	/// Creates Characters.
	/// </summary>
	public class CharacterFactory {
		public static Character CreateCharacter(string name = "",
																						string description = "",
																						int body = -1,
																						int mind = -1,
																						int soul = -1,
																						int fortune = -1) {

			return new Character(name == "" ? "Combatant" : name,
													 description == "" ? "Combatant description." : description,
													 body == -1 ? 1 : body,
													 mind == -1 ? 1 : mind,
													 soul == -1 ? 0 : soul,
													 fortune == -1 ? 0 : fortune);
		}
	}

	public class InventoryFactory {
		public static Inventory CreateInventory() {

			Inventory inventory = new Inventory(
				new List<Equipment>(),
				new List<Equipment>(),
				new List<Item>());

			return inventory;
		}
	}

	public class EquipmentFactory {
		public static Equipment CreateEquipment() {

			Equipment equipment = new Equipment(
					Defaults.EquipmentName,
					ItemType.Miscellaneous,
					Defaults.EquipmentCost,
					Defaults.EquipmentDescription,
					new List<ABuff>());

			return equipment;
		}

		public static Equipment CreateEquipment(ItemType type, double value) {

			if (type == ItemType.Weapon) {
				return CreateWeapon(value);
			}
			else if (type == ItemType.Chestgear) {
				return CreateChestgear(value);
			}
			else {
				Equipment equipment = new Equipment(
					Defaults.EquipmentName,
					type,
					Defaults.EquipmentCost,
					Defaults.EquipmentDescription,
					new List<ABuff>());

				return equipment;
			}
		}

		public static Equipment CreateWeapon(double value) {
			Equipment equipment = new Equipment(
				"Weapon",
				ItemType.Weapon,
				Defaults.EquipmentCost,
				Defaults.EquipmentDescription,
				new List<ABuff>());

			equipment.Buffs.Add(new PhysicalWeaponBuff("WeaponEquipEffect", value));

			return equipment;
		}

		public static Equipment CreateChestgear(double value) {
			Equipment equipment = new Equipment(
				"Armor",
				ItemType.Chestgear,
				Defaults.EquipmentCost,
				Defaults.EquipmentDescription,
				new List<ABuff>());
			equipment.Buffs.Add(new ArmorBuff("WeaponEquipEffect", value));

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