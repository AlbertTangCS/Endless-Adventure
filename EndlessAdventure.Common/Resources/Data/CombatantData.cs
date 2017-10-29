using System.Collections.Generic;

namespace EndlessAdventure.Common.Battle
{
	public class CombatantData {

		public string Name { get; private set; }
		public string Description { get; private set; }

		public int Body { get; private set; }
		public int Mind { get; private set; }
		public int Soul { get; private set; }

		public int ExpReward { get; private set; }
		public Dictionary<string, double> Drops { get; private set; }

		public CombatantData(string name,
		                     string description,
												 int body,
												 int mind,
												 int soul,
												 int expReward,
												 Dictionary<string, double> drops) {
			Name = name;
			Description = description;

			Body = body;
			Mind = mind;
			Soul = soul;

			ExpReward = expReward;
			Drops = drops;
		}
	}
}