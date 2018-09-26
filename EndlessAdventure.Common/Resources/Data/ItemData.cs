using System.Collections.Generic;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Resources
{
	public class ItemData
	{
		public ItemData(string name, string description, ItemType type, int cost, Dictionary<string, double> equipEffects)
		{
			Name = name;
			Description = description;
			Type = type;
			Cost = cost;

			EquipEffects = equipEffects;
		}

		public string Name { get; }
		public string Description { get; }
		public ItemType Type { get; }
		public int Cost { get; }

		public Dictionary<string, double> EquipEffects { get; }
	}
}