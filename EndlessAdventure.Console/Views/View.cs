using System.Collections.Generic;
using EndlessAdventure.Common;

namespace EndlessAdventure.ConsoleApp.Views
{
	public abstract class View
	{
		protected readonly Game _game;
		protected readonly Dictionary<string, string> _commandDictionary = new Dictionary<string, string>();

		protected View(Game pGame)
		{
			_game = pGame;
		}

		public string GetHelpMessage()
		{
			var helpMessage = string.Empty;
			foreach (var command in _commandDictionary.Keys) {
				helpMessage += command;
				helpMessage += _commandDictionary[command];
				helpMessage += "\n";
			}
			return helpMessage;
		}
		
		protected string _parseMessage;
		public string FetchParseMessage()
		{
			var message = _parseMessage;
			_parseMessage = string.Empty;
			return message;
		}

		public abstract void ProcessInput(string[] pArgs);
	}
}