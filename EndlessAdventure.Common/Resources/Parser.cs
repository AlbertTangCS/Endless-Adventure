using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EndlessAdventure.Common.Items;
using Newtonsoft.Json;

namespace EndlessAdventure.Common.Resources
{
	public static class Parser
	{
		public static void Parse()
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

			//var streamreader = new StreamReader("Items.json");
			//string file = "{ \"bar\": \"Stack Overflow\" }";

			var deserialized = JsonConvert.DeserializeObject<ItemData>(file);
			var test = 1 + 1; //Console.WriteLine(deserialized.Bar);
		}
	}
}