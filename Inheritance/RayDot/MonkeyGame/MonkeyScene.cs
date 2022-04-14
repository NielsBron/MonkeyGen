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
		private Background1 background1;
		private Background2 background2;
		private Background3 background3;
		private Text text;
		private TextNode fpstext;
		private TextNode controls;
		private float timer;
		private int framecounter;

		public MonkeyScene() : base() {
			Reload();
		}

		public void Reload() {
			Children.Clear();
			State = State.Playing;
			
			background1 = new Background1("");
			AddChild(background1);
			
			background2 = new Background2("");
			AddChild(background2);
			
			background3 = new Background3("");
			AddChild(background3);

			//for (int i = 0; i == background3.Position.X; i++) {
			//	background3 = new Background3("");
			//	background3.Position.X = 1280;
			//	AddChild(background3);
			//}
			
			player = new Player("resources/PlayerIdle.png");
			AddChild(player);
			
			text = new Text("");
			AddChild(text);
			
			fpstext = new TextNode("calculating FPS..." , 20);
			fpstext.Position = new Vector2(10 , 10);
			fpstext.Color = Color.YELLOW;
			AddChild(fpstext);
			
			controls = new TextNode("Controls: A,D,SPACE" , 20);
			controls.Position = new Vector2(1060 , 10);
			controls.Color = Color.YELLOW;
			AddChild(controls);
			timer = 0.0f;
			framecounter = 0;
		}

		public override void Update(float deltaTime) {
            //Console.WriteLine("Background Position = " + background3.Position.X);
            //for (int i = 0; i < background3.Position.X; i++) {
			//	Console.WriteLine("position");
            //}
			if (background3.Position.X < 0) {
				//Console.WriteLine("background position is lower then 0");
            }
			else {
				//Console.WriteLine("background position is higher then 0");
            }


			framecounter++;
			timer += deltaTime;
			if (timer > 1.0f) {
				fpstext.Text = framecounter.ToString() + " AMOGUS";
				framecounter = 0;
				timer = 0.0f;
			}
			if (timer > 1.0f) {
				controls.Text = framecounter.ToString();
				framecounter = 0;
			}

			text.Position = player.Position;

			HandleInput(deltaTime);
		}
		private void HandleInput(float deltaTime) {
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_R)) {
				Reload();
			}

			if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) {
				//Console.WriteLine("Left");
				player.Walking2(deltaTime);
				background2.Left(deltaTime);
				background3.Left(deltaTime);
			}
			else {
				player.Idle();
            }

			if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) {
				//Console.WriteLine("Right");
				player.Walking1(deltaTime);
				background2.Right(deltaTime);
				background3.Right(deltaTime);
			}
			
			if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) {
				//Console.WriteLine("SUS");
				text.Text1(deltaTime);
				Console.WriteLine("SUS");
			}
			if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE)) {
				text.Text2(deltaTime);
			}


		}
	}
}


