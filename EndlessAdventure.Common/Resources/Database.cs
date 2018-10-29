using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace EndlessAdventure.Common.Resources
{
	public class Database
	{
		public static void Initialize()
		{
			InitializeWorlds();
			InitializeEnemies();
			InitializeEffects();
			InitializeItems();
		}

		#region Worlds

		public static Dictionary<string, WorldData> Worlds = new Dictionary<string, WorldData>();

		public const string KEY_WORLD_GREEN_PASTURES = "WorldGreenPastures";
		public const string KEY_WORLD_SHADY_WOODS = "WorldShadyWoods";

		private static void InitializeWorlds()
		{
			string file;
			var assembly = typeof(FileParser).GetTypeInfo().Assembly;
			
			var space = typeof(FileParser).Namespace;
			using (var stream = assembly.GetManifestResourceStream(space+".Worlds.json"))
			{
				using (var reader = new StreamReader(stream))
				{
					file = reader.ReadToEnd();
				}
			}
			Worlds = JsonConvert.DeserializeObject<Dictionary<string, WorldData>>(file);
		}

		#endregion

		#region Combatants

		public static Dictionary<string, CombatantData> Combatants = new Dictionary<string, CombatantData>();

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
			
			string file;
			var assembly = typeof(FileParser).GetTypeInfo().Assembly;
			
			var space = typeof(FileParser).Namespace;
			using (var stream = assembly.GetManifestResourceStream(space+".Enemies.json"))
			{
				using (var reader = new StreamReader(stream))
				{
					file = reader.ReadToEnd();
				}
			}
			Combatants = JsonConvert.DeserializeObject<Dictionary<string, CombatantData>>(file);
		}

		#endregion

		#region Effects

		public static Dictionary<string, EffectData> Effects = new Dictionary<string, EffectData>();

		public const string KEY_EFFECT_HEAL = "EffectHeal";
		public const string KEY_EFFECT_POISON = "EffectPoison";

		public const string KEY_STATBUFF_INC_PHYSICAL_ATTACK = "StatbuffIncPhysicalAttack";
		public const string KEY_STATBUFF_MULT_PHYSICAL_ATTACK = "StatbuffMultPhysicalAttack";
		public const string KEY_STATBUFF_INC_DEFENSE = "StatbuffIncDefense";
		public const string KEY_STATBUFF_INC_EVASION = "StatbuffIncEvasion";

		public const string KEY_ONHIT_POISON = "OnHitPoison";

		private static void InitializeEffects()
		{
			string file;
			var assembly = typeof(FileParser).GetTypeInfo().Assembly;
			
			var space = typeof(FileParser).Namespace;
			using (var stream = assembly.GetManifestResourceStream(space+".Effects.json"))
			{
				using (var reader = new StreamReader(stream))
				{
					file = reader.ReadToEnd();
				}
			}
			Effects = JsonConvert.DeserializeObject<Dictionary<string, EffectData>>(file);
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
			var assembly = typeof(FileParser).GetTypeInfo().Assembly;
			
			var space = typeof(FileParser).Namespace;
			using (var stream = assembly.GetManifestResourceStream(space+".Items.json"))
			{
				using (var reader = new StreamReader(stream))
				{
					file = reader.ReadToEnd();
				}
			}
			Items = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(file);
		}

		#endregion
	}
}