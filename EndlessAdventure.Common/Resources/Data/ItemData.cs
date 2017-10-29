using System.Collections.Generic;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Resources {
	public class ItemData {
		public string Name { get; private set; }
		public string Description { get; private set; }
		public ItemType Type { get; private set; }
		public int Cost { get; private set; }

		public Dictionary<string, double> BuffValueDict { get; private set; }
		public Dictionary<string, double> EffectValueDict { get; private set; }

		public ItemData(string name, string description, ItemType type, int cost, Dictionary<string, double> buffValueDict, Dictionary<string, double> effectValueDict) {
			Name = name;
			Description = description;
			Type = type;
			Cost = cost;

			BuffValueDict = buffValueDict;
			EffectValueDict = effectValueDict;
		}
	}
}