using System; // Random, Math
using System.Numerics; // Vector2
using System.Collections.Generic; // List
using RayDot; // RayDot
using Raylib_cs; // Raylib

namespace UserLand
{
	class MonkeyScene : SceneNode
	{
		private Player player;
		private TextNode fpstext;
		private float timer;
		private int framecounter;

		public MonkeyScene() : base() {
			Reload();
		}

		public void Reload() {
			Children.Clear();
			State = State.Playing;

			player = new Player("resources/player.png");
			AddChild(player);

			fpstext = new TextNode("calculating FPS..." , 20);
			fpstext.Position = new Vector2(10 , 10);
			fpstext.Color = Color.GREEN;
			AddChild(fpstext);
			timer = 0.0f;
			framecounter = 0;


		}

		public override void Update(float deltaTime) {
			// Calculate framerate
			framecounter++;
			timer += deltaTime;
			if (timer > 1.0f) {
				fpstext.Text = framecounter.ToString() + " FPS";
				framecounter = 0;
				timer = 0.0f;
			}
		}
	}
}


