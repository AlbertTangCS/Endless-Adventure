using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace EndlessAdventure.Common.Resources
{
	public class FileParser : IDataProvider
	{
		public IDictionary<string, WorldData> GetWorldData()
		{
			throw new System.NotImplementedException();
		}

		public IDictionary<string, CombatantData> GetEnemyData()
		{
			throw new System.NotImplementedException();
		}

		public IDictionary<string, ItemData> GetItemData()
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
			return JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(file);
		}

		public IDictionary<string, EffectData> GetEffectData()
		{
			throw new System.NotImplementedException();
		}
	}
}