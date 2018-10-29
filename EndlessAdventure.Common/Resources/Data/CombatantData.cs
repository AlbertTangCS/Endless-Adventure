using System.Collections.Generic;

namespace EndlessAdventure.Common.Resources
{
	public class CombatantData
	{
		public string Name { get; }
		public string Description { get; }
		public int ExpReward { get; }
		public Dictionary<string, double> Drops { get; }
		
		public int Level { get; }
		
		public int Body { get; }
		public int Mind { get; }
		public int Soul { get; }
		public Dictionary<string, double> Buffs { get; }

		// specific to player
		public int Experience { get; }
		public int SkillPoints { get; }
		
		public CombatantData(
			string name,
		    string description,
			int expReward,
			Dictionary<string, double> drops,
			int level,
			int body,
			int mind,
			int soul,
			Dictionary<string, double> buffs,
			int experience = 0,
			int skillpoints = 0)
		{
			Name = name;
			Description = description;
			ExpReward = expReward;

			Body = body;
			Mind = mind;
			Soul = soul;
			Buffs = buffs;

			Drops = drops;

			Experience = experience;
			SkillPoints = skillpoints;
		}
	}
}