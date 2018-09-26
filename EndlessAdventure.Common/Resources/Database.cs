using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Resources.Data;
using Newtonsoft.Json;

namespace EndlessAdventure.Common.Resources
{
	public class Database
	{
		public static void Initialize()
		{
			//Parser.Parse();
			//Console.ReadLine();

			InitializeWorlds();
			InitializeEnemies();
			InitializeEffects();
			InitializeItems();

			Console.ReadLine();
		}

		#region Worlds

		public static readonly Dictionary<string, WorldData> Worlds = new Dictionary<string, WorldData>();

		public const string KEY_WORLD_GREEN_PASTURES = "WorldGreenPastures";
		public const string KEY_WORLD_SHADY_WOODS = "WorldShadyWoods";

		private static void InitializeWorlds()
		{
			Worlds.Add(KEY_WORLD_GREEN_PASTURES,
				new WorldData(
					Localization.WORLD_GREEN_PASTURES_NAME,
					Localization.WORLD_GREEN_PASTURES_DESCRIPTION,
					new Dictionary<string, int> {
						{ KEY_COMBATANT_CHICKEN, 50 },
						{ KEY_COMBATANT_SHEEP, 10 },
						{ KEY_COMBATANT_PIG, 10 },
						{ KEY_COMBATANT_PONY, 5 },
						{ KEY_COMBATANT_GOBLIN_DESERTER, 5 },
						{ KEY_COMBATANT_UNICORN, 1 }
					}));
			Worlds.Add(KEY_WORLD_SHADY_WOODS,
				new WorldData(
					Localization.WORLD_SHADY_WOODS_NAME,
					Localization.WORLD_SHADY_WOODS_DESCRIPTION,
					new Dictionary<string, int> {
						{ KEY_COMBATANT_SQUIRREL, 50 },
						{ KEY_COMBATANT_RACCOON, 20 },
						{ KEY_COMBATANT_DEER, 10 },
						{ KEY_COMBATANT_OWL, 10 },
						{ KEY_COMBATANT_WOLF, 5 },
						{ KEY_COMBATANT_SHROOMLING, 50 }
					}));
		}

		#endregion

		#region Combatants

		public static readonly Dictionary<string, CombatantData> Combatants = new Dictionary<string, CombatantData>();

		private const string KEY_COMBATANT_PLAYER = "Player";

		private const string KEY_COMBATANT_CHICKEN = "EnemyChicken";
		private const string KEY_COMBATANT_SHEEP = "EnemySheep";
		private const string KEY_COMBATANT_PIG = "EnemyPig";
		private const string KEY_COMBATANT_PONY = "EnemyPony";
		private const string KEY_COMBATANT_GOBLIN_DESERTER = "EnemyGoblinDeserter";
		private const string KEY_COMBATANT_UNICORN = "EnemyUnicorn";

		private const string KEY_COMBATANT_SQUIRREL = "EnemySquirrel";
		private const string KEY_COMBATANT_RACCOON = "EnemyRacoon";
		private const string KEY_COMBATANT_DEER = "EnemyDeer";
		private const string KEY_COMBATANT_OWL = "EnemyOwl";
		private const string KEY_COMBATANT_WOLF = "EnemyWolf";
		private const string KEY_COMBATANT_SHROOMLING = "EnemyShroomling";

		private static void InitializeEnemies()
		{
			// player
			Combatants.Add(KEY_COMBATANT_PLAYER,
				new CombatantData(
					Localization.COMBATANT_PLAYER_NAME,
					Localization.COMBATANT_PLAYER_DESCRIPTION,
					0,
					new Dictionary<string, double>(),
					1,
					3,
					3,
					3,
					new Dictionary<string, double>()));
			
			// green pastures
			Combatants.Add(KEY_COMBATANT_CHICKEN,
				new CombatantData(
					Localization.COMBATANT_CHICKEN_NAME,
					Localization.COMBATANT_CHICKEN_DESCRIPTION,
					1,
					new Dictionary<string, double>(),
					1,
					1,
					0,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_SHEEP,
				new CombatantData(
					Localization.COMBATANT_SHEEP_NAME,
					Localization.COMBATANT_SHEEP_DESCRIPTION,
					2,
					new Dictionary<string, double>(),
					1,
					3,
					0,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_PIG,
				new CombatantData(
					Localization.COMBATANT_PIG_NAME,
					Localization.COMBATANT_PIG_DESCRIPTION,
					2,
					new Dictionary<string, double>(),
					1,
					3,
					1,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_PONY,
				new CombatantData(
					Localization.COMBATANT_PONY_NAME,
					Localization.COMBATANT_PONY_DESCRIPTION,
					3,
					new Dictionary<string, double>(),
					1,
					5,
					1,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_GOBLIN_DESERTER,
				new CombatantData(
					Localization.COMBATANT_GOBLIN_DESERTER_NAME,
					Localization.COMBATANT_GOBLIN_DESERTER_DESCRIPTION,
					5,
					new Dictionary<string, double>
					{
						{KEY_ITEM_POTION, 1},
						{KEY_ITEM_STICK, 0.5},
						{KEY_ITEM_SHIRT, 0.5}
					},
					1,
					5,
					2,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_UNICORN,
				new CombatantData(
					Localization.COMBATANT_UNICORN_NAME,
					Localization.COMBATANT_UNICORN_DESCRIPTION,
					3,
					new Dictionary<string, double>
					{
						{KEY_ITEM_UNICORN_HORN, 1}
					},
					1,
					5,
					5,
					1,
					new Dictionary<string, double>()));

			// shady woods
			Combatants.Add(KEY_COMBATANT_SQUIRREL,
				new CombatantData(
					Localization.COMBATANT_SQUIRREL_NAME,
					Localization.COMBATANT_SQUIRREL_DESCRIPTION,
					1,
					new Dictionary<string, double>(),
					1,
					1,
					0,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_RACCOON,
				new CombatantData(
					Localization.COMBATANT_RACOON_NAME,
					Localization.COMBATANT_RACOON_DESCRIPTION,
					2,
					new Dictionary<string, double>(),
					1,
					2,
					2,
					0,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_DEER,
				new CombatantData(
					Localization.COMBATANT_DEER_NAME,
					Localization.COMBATANT_DEER_DESCRIPTION,
					3,
					new Dictionary<string, double>(),
					1,
					5,
					1,
					1,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_OWL,
				new CombatantData(
					Localization.COMBATANT_OWL_NAME,
					Localization.COMBATANT_OWL_DESCRIPTION,
					3,
					new Dictionary<string, double>(),
					1,
					1,
					7,
					0,
					new Dictionary<string, double>
					{
						{KEY_STATBUFF_INC_EVASION, 7}
					}));
			Combatants.Add(KEY_COMBATANT_WOLF,
				new CombatantData(
					Localization.COMBATANT_WOLF_NAME,
					Localization.COMBATANT_WOLF_DESCRIPTION,
					5,
					new Dictionary<string, double>(),
					1,
					7,
					5,
					2,
					new Dictionary<string, double>()));
			Combatants.Add(KEY_COMBATANT_SHROOMLING,
				new CombatantData(
					Localization.COMBATANT_SHROOMLING_NAME,
					Localization.COMBATANT_SHROOMLING_DESCRIPTION,
					10,
					new Dictionary<string, double>(),
					1,
					10,
					5,
					5,
					new Dictionary<string, double>
					{
						{KEY_ONHIT_POISON, 1}
					}));
		}

		#endregion

		#region Effects

		public static readonly Dictionary<string, EffectData> Effects = new Dictionary<string, EffectData>();//Func<double, int, IEffect>> Effects = new Dictionary<string, Func<double, int, IEffect>>();

		public const string KEY_EFFECT_HEAL = "EffectHeal";
		public const string KEY_EFFECT_POISON = "EffectPoison";

		public const string KEY_STATBUFF_INC_PHYSICAL_ATTACK = "StatbuffIncPhysicalAttack";
		public const string KEY_STATBUFF_MULT_PHYSICAL_ATTACK = "StatbuffMultPhysicalAttack";
		public const string KEY_STATBUFF_INC_DEFENSE = "StatbuffIncDefense";
		public const string KEY_STATBUFF_INC_EVASION = "StatbuffIncEvasion";

		public const string KEY_ONHIT_POISON = "OnHitPoison";

		private static void InitializeEffects() {
			// active effects
			Effects.Add(KEY_EFFECT_HEAL,
				new EffectData(
					Localization.EFFECT_HEAL_NAME,
					Localization.EFFECT_HEAL_DESCRIPTION,
					EffectType.Active));
			Effects.Add(KEY_EFFECT_POISON,
				new EffectData(
					Localization.EFFECT_POISON_NAME,
					Localization.EFFECT_POISON_DESCRIPTION,
					EffectType.Active));
			
			// stat buffs
			Effects.Add(KEY_STATBUFF_INC_PHYSICAL_ATTACK,
				new EffectData(
					Localization.STATBUFF_PHYSICAL_ATTACK_NAME,
					Localization.STATBUFF_INC_PHYSICAL_ATTACK_DESCRIPTION,
					EffectType.Stat));
			Effects.Add(KEY_STATBUFF_MULT_PHYSICAL_ATTACK,
				new EffectData(
					Localization.STATBUFF_PHYSICAL_ATTACK_NAME,
					Localization.STATBUFF_MULT_PHYSICAL_ATTACK_DESCRIPTION,
					EffectType.Stat));
			Effects.Add(KEY_STATBUFF_INC_DEFENSE,
				new EffectData(
					Localization.STATBUFF_DEFENSE_NAME,
					Localization.STATBUFF_INC_DEFENSE_DESCRIPTION,
					EffectType.Stat));
			Effects.Add(KEY_STATBUFF_INC_EVASION,
				new EffectData(
					Localization.STATBUFF_EVASION_NAME,
					Localization.STATBUFF_INC_EVASION_DESCRIPTION,
					EffectType.Stat));

			// on hits
			Effects.Add(KEY_ONHIT_POISON,
				new EffectData(
					Localization.ONHIT_POISON_NAME,
					Localization.ONHIT_POISON_DESCRIPTION,
					EffectType.OnHit));
		}

		#endregion Effects

		#region Items

		public static Dictionary<string, ItemData> Items = new Dictionary<string, ItemData>();

		private static readonly string KEY_ITEM_STICK = "ItemStick";
		private static readonly string KEY_ITEM_SHIRT = "ItemShirt";

		private static readonly string KEY_ITEM_POTION = "ItemPotion";

		private static readonly string KEY_ITEM_UNICORN_HORN = "ItemUnicornHorn";

		private static void InitializeItems()
		{
			string file;
			var assembly = typeof(Parser).GetTypeInfo().Assembly;
			
			var space = typeof(Parser).Namespace;
			using (var stream = assembly.GetManifestResourceStream(space+".Items.json"))
			{
				using (var reader = new StreamReader(stream))
				{
					file = reader.ReadToEnd();
				}
			}
			Items = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(file);
			/*//equippables
			Items.Add(KEY_ITEM_STICK,
				new ItemData(
					Localization.ITEM_STICK_NAME,
					Localization.ITEM_STICK_DESCRIPTION,
					ItemType.Weapon,
					1,
					new Dictionary<string, double> {
						{ KEY_STATBUFF_INC_PHYSICAL_ATTACK, 1 }
					}));
			Items.Add(KEY_ITEM_SHIRT,
				new ItemData(
					Localization.ITEM_SHIRT_NAME,
					Localization.ITEM_SHIRT_DESCRIPTION,
					ItemType.Chestgear,
					1,
					new Dictionary<string, double> {
						{ KEY_STATBUFF_INC_DEFENSE, 1 },
						{ KEY_STATBUFF_MULT_PHYSICAL_ATTACK, 0.5 }
					}));

			// consumables
			Items.Add(KEY_ITEM_POTION,
				new ItemData(
					Localization.ITEM_POTION_NAME,
					Localization.ITEM_POTION_DESCRIPTION,
					ItemType.Consumable,
					5,
					new Dictionary<string, double> {
						{ KEY_EFFECT_HEAL, 3 }
					}));

			// miscellaneous
			Items.Add(KEY_ITEM_UNICORN_HORN,
				new ItemData(
					Localization.ITEM_UNICORN_HORN_NAME,
					Localization.ITEM_UNICORN_HORN_DESCRIPTION,
					ItemType.Miscellaneous,
					100,
					null));*/
		}

		#endregion
	}
}