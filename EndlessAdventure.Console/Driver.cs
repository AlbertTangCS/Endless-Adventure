using System;
using System.Diagnostics;

using EndlessAdventure.Common;

namespace EndlessAdventure.ConsoleApp {
	/// <summary>
	/// Main loop of the program that continually runs and determines whether
	/// to update the game and the GUI.
	/// </summary>
	public class Driver {
		
		private static Game _game;
		private static MainView _parser;
		private static Gui _gui;

		public static void Main() {

			_game = new Game();
			_parser = new MainView(_game);
			_gui = new Gui(_game, _parser);

			string input = string.Empty;
			while (true) {
				if (input == "quit" || input == "q") break;
				else {
					var args = input.Split(' ');
					_parser.ProcessInput(args);
				}
				_gui.Render(-1);

				input = Console.ReadLine();
			}

			_gui.DisplayQuit();
		}

			/*
			long frequency = Stopwatch.Frequency/1000;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			long tick_lag = 0;
			long frame_lag = 0;

			while (true) {
				long current = stopwatch.ElapsedTicks;
				stopwatch.Restart();
				tick_lag += current;
				frame_lag += current;

				game.ProcessInput();

				// logic update, update for every elapsed tick
				while (tick_lag/frequency >= Preferences.LENGTH_TICK) {
					game.Update();
					// Console.WriteLine("\n[Tick time: " + tick_lag / frequency + "ms]");
					tick_lag -= Preferences.LENGTH_TICK*frequency;
				}

				// GUI update, only update once
				if (frame_lag/frequency >= Preferences.LENGTH_FRAME) {
					gui.Render(frame_lag/frequency);
					frame_lag = 0;
				}
			}
		}*/
	}
}