namespace EndlessAdventure.Common.Battle
{
	public class CombatantData {

		public string Name { get; private set; }
		public string Description { get; private set; }
		public int Weight { get; private set; }
		public string World { get; private set; }
		
		public int ExpReward { get; private set; }

		public int Body { get; private set; }
		public int Mind { get; private set; }
		public int Soul { get; private set; }
		public int Fortune { get; private set; }

		public CombatantData(string name, string description, int weight, string world, int expReward, int body, int mind, int soul, int fortune) {
			Name = name;
			Description = description;
			Weight = weight;
			World = world;

			ExpReward = expReward;

			Body = body;
			Mind = mind;
			Soul = soul;
			Fortune = fortune;
		}
	}
}