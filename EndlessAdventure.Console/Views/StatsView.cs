using System;
using System.Linq;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Items;

namespace EndlessAdventure {
	public class StatsView : View {

		private const string ADD_STAT_COMMAND = "addstat";

		public StatsView(Game pGame) : base(pGame) {
			_commandDictionary.Add(ADD_STAT_COMMAND, " <stat> <points>: Add <points> amount to <stat>, if possible. Default is 1.");
		}

		public override void ProcessInput(string[] pArgs) {
			var protagonist = _game.Battlefield.Protagonists[0];
			switch (pArgs[0]) {

				case ADD_STAT_COMMAND:
					if (pArgs.Length == 1) {
						_parseMessage = "Missing argument.";
						break;
					}
					int numpoints = -1;
					if (pArgs.Length == 3) {
						if (Int32.TryParse(pArgs[2], out int parsed) && parsed > 0 && parsed <= protagonist.SkillPoints) {
							numpoints = parsed;
						}
						else {
							_parseMessage = "Invalid number of points to allocate.";
							break;
						}
					}
					else if (pArgs.Length == 2) {
						numpoints = 1;
					}

					var type = StatType.Fortune;
					switch (pArgs[1]) {
						case "body":
							type = StatType.Body;
							break;
						case "mind":
							type = StatType.Mind;
							break;
						case "soul":
							type = StatType.Soul;
							break;
					}
					if (type != StatType.Fortune) {
						for (int i = 0; i < numpoints; i++) {
							protagonist.AddSkillPoint(type);
						}
						_parseMessage = numpoints + " stat point(s) added to " + pArgs[1] + "!";
					}
					else {
						_parseMessage = "Invalid stat.";
					}
					break;

				default:
					_parseMessage = "Invalid command.";
					break;
			}
		}
	}
}