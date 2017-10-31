using System;
using System.Collections.Generic;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Characters;
using EndlessAdventure.Common.Items;
using EndlessAdventure.Common.Items.Consumables.Effects;
using EndlessAdventure.Common.Items.Effects;

namespace EndlessAdventure.Common.Resources {
	public class Database {

		public static void Initialize() {
			InitializeWorlds();
			InitializeEnemies();
			InitializeItems();
			InitializeBuffs();
			InitializeEffects();
		}

		#region Worlds

		public static readonly Dictionary<string, WorldData> Worlds = new Dictionary<string, WorldData>();

		public static readonly string GREEN_PASTURES_KEY = "WorldGreenPastures";
		public static readonly string SHADY_WOODS_KEY = "WorldShadyWoods";

		private static void InitializeWorlds() {
			Dictionary<string, int> green_pastures_spawns = new Dictionary<string, int>();
			green_pastures_spawns.Add(CHICKEN_KEY, 50);
			green_pastures_spawns.Add(SHEEP_KEY, 10);
			green_pastures_spawns.Add(PONY_KEY, 5);
			green_pastures_spawns.Add(GOBLIN_DESERTER_KEY, 5);
			green_pastures_spawns.Add(UNICORN_KEY, 1);
			Worlds.Add(GREEN_PASTURES_KEY, new WorldData("Green Pastures", "green pastures", green_pastures_spawns));
			Worlds.Add(SHADY_WOODS_KEY, new WorldData("ShadyWoods", "shady woods", null));
		}

		#endregion

		#region Combatants

		public static readonly Dictionary<string, CombatantData> Combatants = new Dictionary<string, CombatantData>();

		public static readonly string PLAYER_KEY = "Player";

		public static readonly string CHICKEN_KEY = "EnemyChicken";
		public static readonly string SHEEP_KEY = "EnemySheep";
		public static readonly string PONY_KEY = "EnemyPony";
		public static readonly string GOBLIN_DESERTER_KEY = "EnemyGoblinDeserter";
		public static readonly string UNICORN_KEY = "EnemyUnicorn";

		private static void InitializeEnemies() {
			Combatants.Add(PLAYER_KEY, new CombatantData("Player", "player", /*stats*/ 3, 3, 3, /*rewards*/ 0, null));

			Combatants.Add(CHICKEN_KEY, new CombatantData("Chicken", "chicken", /*stats*/ 1, 0, 0, /*rewards*/ 1, null));
			Combatants.Add(SHEEP_KEY, new CombatantData("Sheep", "sheep", /*stats*/ 3, 0, 0, /*rewards*/ 2, null));
			Combatants.Add(PONY_KEY, new CombatantData("Pony", "pony", /*stats*/ 5, 1, 0, /*rewards*/ 3, null));
			Dictionary<string, double> goblin_drops = new Dictionary<string, double>();
			goblin_drops.Add(POTION_KEY, 1);
			Combatants.Add(GOBLIN_DESERTER_KEY, new CombatantData("Goblin Deserter", "goblin deserter", /*stats*/ 5, 2, 0, /*rewards*/ 0, goblin_drops));
			Dictionary<string, double> unicorn_drops = new Dictionary<string, double>();
			unicorn_drops.Add(UNICORN_HORN_KEY, 1);
			Combatants.Add(UNICORN_KEY, new CombatantData("Unicorn", "unicorn", /*stats*/ 5, 5, 1, /*rewards*/ 3, unicorn_drops));
		}

		#endregion

		#region Items

		public static readonly Dictionary<string, ItemData> Items = new Dictionary<string, ItemData>();
		
		private static readonly string STICK_KEY = "ItemStick";
		private static readonly string SHIRT_KEY = "ItemShirt";
		
		private static readonly string POTION_KEY = "ItemPotion";

		private static readonly string UNICORN_HORN_KEY = "ItemUnicornHorn";

		private static void InitializeItems() {
			Dictionary<string, double> stick_key_dict = new Dictionary<string, double> { { PHYSICAL_WEAPON_BUFF_KEY, 1 } };
			Items.Add(STICK_KEY, new ItemData("Stick", "stick", ItemType.Weapon, 1, stick_key_dict, null));
			Dictionary<string, double> shirt_key_dict = new Dictionary<string, double> { { ARMOR_BUFF_KEY, 1 } };
			Items.Add(SHIRT_KEY, new ItemData("Shirt", "shirt", ItemType.Chestgear, 1, shirt_key_dict, null));

			Dictionary<string, double> potion_key_dict = new Dictionary<string, double> { { HEAL_EFFECT_KEY, 3 } };
			Items.Add(POTION_KEY, new ItemData("Potion", "potion", ItemType.Consumable, 5, null, potion_key_dict));

			Items.Add(UNICORN_HORN_KEY, new ItemData("Unicorn Horn", "unicorn horn", ItemType.Miscellaneous, 10, null, null));
		}

		#endregion

		#region Buffs

		public static readonly Dictionary<string, Func<double, ABuff>> Buffs = new Dictionary<string, Func<double, ABuff>>();

		public static readonly string PHYSICAL_WEAPON_BUFF_KEY = "BuffPhysicalWeapon";
		public static readonly string ARMOR_BUFF_KEY = "BuffArmor";

		private static void InitializeBuffs() {
			Buffs.Add(PHYSICAL_WEAPON_BUFF_KEY, (value) => new PhysicalWeaponBuff(value));
			Buffs.Add(ARMOR_BUFF_KEY, (value) => new ArmorBuff(value));
		}

		#endregion Buffs

		#region Effects

		public static readonly Dictionary<string, Func<double, AEffect>> Effects = new Dictionary<string, Func<double, AEffect>>();

		public static readonly string HEAL_EFFECT_KEY = "EffectHeal";

		private static void InitializeEffects() {
			Effects.Add(HEAL_EFFECT_KEY, (value) => new HealEffect(value));
		}

		#endregion Effects

	}
}