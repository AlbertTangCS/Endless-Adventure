using System;
using System.Diagnostics;

namespace EndlessAdventure {
	public class Driver {

		public static void Main() {
			Game game = new Game();
			Gui gui = new Gui();

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

				while (tick_lag/frequency >= Config.LENGTH_TICK) {
					game.Update();
					Console.WriteLine("Tick time: " + tick_lag / frequency + "ms");
					tick_lag -= Config.LENGTH_TICK*frequency;
				}

				if (frame_lag/frequency >= Config.LENGTH_FRAME) {
					gui.Render();
					Console.WriteLine("Frame time: " + frame_lag / frequency + "ms");
					frame_lag = 0;
				}
			}
		}

	}
}