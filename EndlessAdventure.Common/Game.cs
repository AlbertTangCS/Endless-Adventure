using EndlessAdventure.Common.Battle;
using EndlessAdventure.Common.Interfaces;
using EndlessAdventure.Common.Resources;

namespace EndlessAdventure.Common
{
	public class Game
	{
		private readonly ISaveFile _saveFile;

		public Game(ISaveFile pSaveFile)
		{
			_saveFile = pSaveFile;
			Database.Initialize();

			var protagonists = _saveFile.GetSavedProtagonists();
			var world = _saveFile.GetCurrentWorld();
			Battlefield = new Battlefield(protagonists, world);
		}
		
		public IBattlefield Battlefield { get; }

		public void Update(int ticks = 1) {
			for (var i = 0; i < ticks; i++) {
				Battlefield.Tick();
			}
		}
	}
}