using System;

namespace EndlessAdventure.Common.Items
{
    public class Item
    {
			public string Name { get; private set; }
			public ItemType Type { get; private set; }
			public int Cost { get; private set; }
			public string Description { get; private set; }

			public Item(string name,
											 ItemType type,
											 int cost,
											 string description) {

				if (name == null || description == null) throw new ArgumentException();

				Name = name;
				Type = type;
				Cost = cost;
				Description = description;
			}
	}
}