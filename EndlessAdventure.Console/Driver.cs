using System;
using System.Diagnostics;

using EndlessAdventure.Common;

namespace EndlessAdventure.ConsoleApp {
	/// <summary>
	/// Main loop of the program that continually runs and determines whether
	/// to update the game and the GUI.
	/// </summary>
	public class Driver {

		private static Game game = new Game();
		private static Gui gui = new Gui(game);

		public static void Main() {
			Parser.Game = game;

			string input = string.Empty;
			while (true) {
				if (input == "quit") break;
				else {
					Parser.Parse(input);
				}
				gui.Render(-1);

				input = Console.ReadLine();
			}
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