using System; // Console
using RayDot; // RayDot

namespace UserLand
{
	class MonkeyGame
	{
		private Core core;
		private MonkeyScene currentScene;

		public MonkeyGame()
		{
			core = new Core("MonkeyGame");

			currentScene = new MonkeyScene();
		}

		public void Play()
		{
			while (core.Run(currentScene))
			{
				;
			}
			Console.WriteLine("Thank you for playing!");
		}
	}
}
