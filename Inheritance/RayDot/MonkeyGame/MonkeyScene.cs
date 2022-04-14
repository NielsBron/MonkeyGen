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
		private Background background;
		private TextNode fpstext;
		private float timer;
		private int framecounter;

		public MonkeyScene() : base() {
			Reload();
		}

		public void Reload() {
			Children.Clear();
			State = State.Playing;
			background = new Background("");
			AddChild(background);
			player = new Player("resources/PlayerIdle.png");
			AddChild(player);
			fpstext = new TextNode("calculating FPS..." , 20);
			fpstext.Position = new Vector2(10 , 10);
			fpstext.Color = Color.YELLOW;
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

			HandleInput(deltaTime);
			//if (Raylib.IsKeyReleased(KeyboardKey.KEY_UP)) {
			//	player. ("resources/PlayerMove1.png");
			//}
			//else {
			//	player. ("resources/PlayerIdle.png")
            //}
		}
		private void HandleInput(float deltaTime) {
			// Reload Game
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_R)) {
				Reload();
			}
			// Player Rotate
			//if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {
			//	//player.RotateRight(deltaTime);
			//	Console.WriteLine("<");
			//	player.Walk2(deltaTime);
			//}
			//if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {
			//	//player.RotateLeft(deltaTime);
			//	Console.WriteLine(">");
			//	player.Walk1();
			//}
			//if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) {
			//	//player.RotateLeft(deltaTime);
			//	Console.WriteLine("Idle");
			//	player.Idle();
			//}

			if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) {
				//player.RotateLeft(deltaTime);
				Console.WriteLine("Left");
				player.Walking2(deltaTime);
			}
			else {
				player.Idle();
            }

			if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) {
				//player.RotateLeft(deltaTime);
				Console.WriteLine("Right");
				player.Walking1(deltaTime);
			}

			//if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {
			//	//player.RotateLeft(deltaTime);
			//	Console.WriteLine("Left");
			//	player.Walking2(deltaTime);
			//}
			//else {
			//	player.Idle();
            //}
		


		}
	}
}


