using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace EndlessAdventure.Common.Resources
{
	public static class Parser
	{
		public static void Parse()
		{
			string file;
			var assembly = typeof(Parser).GetTypeInfo().Assembly;

			foreach (var wat in assembly.GetManifestResourceNames())
			{
				Console.WriteLine(wat);
			}
			Console.ReadLine();

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

			var deserialized = JsonConvert.DeserializeObject<Test>(file);
			Console.WriteLine(deserialized.Bar);
		}

		private class Test
		{
			public string Bar { get; }

			public Test(string bar)
			{
				Bar = bar;
			}
		}
	}
}