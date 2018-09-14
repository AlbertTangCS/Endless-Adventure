using System;
using EndlessAdventure.Common;

namespace EndlessAdventure.ConsoleApp.Views
{
	public enum Mode
	{
		Battle, Inventory, Stats, Buffs
	}

	public class MainView : View
	{
		private const string BATTLE_VIEW_COMMAND = "battle";
		private const string BUFFS_VIEW_COMMAND = "buffs";
		private const string INVENTORY_VIEW_COMMAND = "inventory";
		private const string STATS_VIEW_COMMAND = "stats";

		private const int BATTLE_VIEW_INDEX = 0;
		private const int BUFFS_VIEW_INDEX = 1;
		private const int INVENTORY_VIEW_INDEX = 2;
		private const int STATS_VIEW_INDEX = 3;
		
		private readonly View[] _subviews;
		private View _currentSubview;

		public MainView(Game pGame) : base(pGame)
		{
			_commandDictionary.Add(BATTLE_VIEW_COMMAND, ": Switches to the battle view.");
			_commandDictionary.Add(BUFFS_VIEW_COMMAND, ": Switches to the buffs view.");
			_commandDictionary.Add(INVENTORY_VIEW_COMMAND, ": Switches to the inventory view.");
			_commandDictionary.Add(STATS_VIEW_COMMAND, ": Switches to the stats view.");

			_subviews = new View[] { new BattleView(pGame), new BuffsView(pGame), new InventoryView(pGame), new StatsView(pGame) };
			_currentSubview = _subviews[0];
		}

		public Mode Mode { get
			{
				var index = Array.IndexOf(_subviews, _currentSubview);
				switch (index) {
					case BATTLE_VIEW_INDEX:
						return Mode.Battle;
					case BUFFS_VIEW_INDEX:
						return Mode.Buffs;
					case INVENTORY_VIEW_INDEX:
						return Mode.Inventory;
					case STATS_VIEW_INDEX:
						return Mode.Stats;
					default:
						return Mode.Battle;
				}
			}
		}

		private string _message;
		public string FetchMessage()
		{
			var message = _message;
			_message = null;
			return message;
		}

		public override void ProcessInput(string[] pArgs)
		{
			pArgs[0] = pArgs[0].ToLower();
			switch (pArgs[0]) {
				case "":
					if (_currentSubview == _subviews[BATTLE_VIEW_INDEX]) {
						_game.Update();
					}
					else {
						_message = "Invalid command.";
					}
					break;

				case BATTLE_VIEW_COMMAND:
					_currentSubview = _subviews[BATTLE_VIEW_INDEX];
					break;

				case BUFFS_VIEW_COMMAND:
					_currentSubview = _subviews[BUFFS_VIEW_INDEX];
					break;

				case INVENTORY_VIEW_COMMAND:
					_currentSubview = _subviews[INVENTORY_VIEW_INDEX];
					break;

				case STATS_VIEW_COMMAND:
					_currentSubview = _subviews[STATS_VIEW_INDEX];
					break;
					
				case "help":
					var helpMessage = ">>> COMMANDS:\n";
					helpMessage += _currentSubview.GetHelpMessage() + "\n";
					helpMessage += GetHelpMessage();
					_message = helpMessage;
					break;

				default:
					_currentSubview.ProcessInput(pArgs);
					_message = _currentSubview.FetchParseMessage();
					break;
			}
		}
	}
}