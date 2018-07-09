using System;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure {
	public class BattleView : View {

		private const string FLEE_COMMAND = "flee";
		private const string RUN_COMMAND = "run";
		private const string TRAVEL_COMMAND = "travelto";

		public BattleView(Game pGame) : base(pGame) {
			_commandDictionary.Add(FLEE_COMMAND, ": Flee from current enemy.");
			_commandDictionary.Add(RUN_COMMAND, "; run <ticks>: Elapses the game by <ticks>. Default is 1.");
			_commandDictionary.Add(TRAVEL_COMMAND, " <world>: Travels to the given world.");
		}

		public override void ProcessInput(string[] pArgs) {
			switch (pArgs[0]) {

				case FLEE_COMMAND:
					_game.Battlefield.Flee();
					break;

				case RUN_COMMAND:
					if (pArgs.Length == 1) {
						_game.Update(1);
					}
					else if (Int32.TryParse(pArgs[1], out int parsed) && parsed > 0) {
						_game.Update(parsed);
					}
					else {
						_parseMessage = "Invalid argument.";
					}
					break;

				case TRAVEL_COMMAND:
					if (pArgs.Length == 1) {
						_parseMessage = "Missing argument.";
						break;
					}
					switch (pArgs[1]) {
						case "greenpastures":
							_game.TravelToWorld(Database.KEY_WORLD_GREEN_PASTURES);
							break;
						case "shadywoods":
							_game.TravelToWorld(Database.KEY_WORLD_SHADY_WOODS);
							break;
						default:
							_parseMessage = "Invalid argument.";
							break;
					}
					break;

				default:
					_parseMessage = "Invalid command.";
					break;
			}
		}
	}
}