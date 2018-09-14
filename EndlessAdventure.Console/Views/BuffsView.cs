using EndlessAdventure.Common;

namespace EndlessAdventure.ConsoleApp.Views
{
	public class BuffsView : View
	{
		public BuffsView(Game pGame) : base(pGame)
		{
		}

		public override void ProcessInput(string[] pArgs)
		{
			switch (pArgs[0]) {

				default:
					_parseMessage = "Invalid command.";
					break;
			}
		}
	}
}