using System;
using System.Diagnostics;
using EndlessAdventure.Common;
using EndlessAdventure.Common.Resources;
using EndlessAdventure.ConsoleApp.Views;

namespace EndlessAdventure.ConsoleApp
{
	/// <summary>
	/// Main loop of the program that continually runs and determines whether
	/// to update the game and the GUI.
	/// </summary>
	public static class Driver
	{
		private const bool AUTO = false;

		private static Game _game;
		private static MainView _parser;
		private static Gui _gui;
		
		public static void Main()
		{
			_game = new Game(new SaveFile());
			_parser = new MainView(_game);
			_gui = new Gui(_game, _parser);

			if (AUTO)
			{
				RunAutomatically();
			}
			else
			{
				RunManually();
			}

			_gui.DisplayQuit();
		}

		private static void RunManually()
		{
			var input = string.Empty;
			while (true)
			{
				if (input == "quit" || input == "q")
					break;

				var args = input.Split(' ');
				_parser.ProcessInput(args);
				_gui.Render(-1);

				input = Console.ReadLine();
			}
		}

		private static void RunAutomatically()
		{
			var frequency = Stopwatch.Frequency/1000;
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			long tickLag = 0;
			long frameLag = 0;

			while (true)
			{
				var current = stopwatch.ElapsedTicks;
				stopwatch.Restart();
				tickLag += current;
				frameLag += current;

				//_game.ProcessInput();

				// logic update, update for every elapsed tick
				while (tickLag/frequency >= Preferences.LENGTH_TICK)
				{
					_game.Update();
					// Console.WriteLine("\n[Tick time: " + tick_lag / frequency + "ms]");
					tickLag -= Preferences.LENGTH_TICK * frequency;
				}

				// GUI update, only update once
				if (frameLag/frequency >= Preferences.LENGTH_FRAME)
				{
					_gui.Render(frameLag/frequency);
					frameLag = 0;
				}
			}
		}
	}
}