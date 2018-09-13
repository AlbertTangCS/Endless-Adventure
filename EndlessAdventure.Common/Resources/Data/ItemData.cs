using System.Collections.Generic;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure.Common.Resources
{
	public class ItemData
	{
		public ItemData(string pName, string pDescription, ItemType pType, int pCost, Dictionary<string, double> pEquipEffects)
		{
			Name = pName;
			Description = pDescription;
			Type = pType;
			Cost = pCost;

			EquipEffects = pEquipEffects;
		}

		public string Name { get; }
		public string Description { get; }
		public ItemType Type { get; }
		public int Cost { get; }

		public Dictionary<string, double> EquipEffects { get; }
	}
}