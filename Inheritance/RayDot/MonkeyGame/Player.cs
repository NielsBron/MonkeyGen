using System; // Math
using System.Numerics; // Vector2
using System.Collections.Generic; // List
using RayDot; // RayDot
using Raylib_cs; // Raylib


namespace UserLand
{
	class Player : MoverNode {

		private SpriteNode body;
		private float Speed;
		private float timer;

		public Player(string name) : base(name) {
			Position = new Vector2(200, 550);
			Pivot = new Vector2(0.5f , 0.5f);
			Scale = new Vector2(3f , 3f);

			Speed = 50;

			body = new SpriteNode("");
			body.Scale = new Vector2(1.05f , 1.05f);
			body.Pivot = new Vector2(2f , 2f);
			AddChild(this.body);
		}

		public override void Update(float deltaTime)
		{

			base.Update(deltaTime);
			timer += deltaTime;
			if (timer > 5.0f) {
			}

			BorderWrap();
		}
		public void Idle() {
			TextureName = "resources/PlayerIdle.png";
		}
		public void Walking1(float deltaTime) {
			Position.X += Speed * deltaTime;
			timer += deltaTime;
			if (timer > 0.0f) {
				TextureName = "resources/PlayerMove1.png";
			}
			if (timer > 0.5f) {
				TextureName = "resources/PlayerMove2.png";
            }
			if (timer > 1.0f) {
				timer = 0.0f;
            }
		}

		public void Walking2(float deltaTime) {
			Position.X -= Speed * deltaTime;
			timer += deltaTime;
			
			if (timer > 0.0f) {
				TextureName = "resources/PlayerMove1.png";
			}
			if (timer > 0.5f) {
				TextureName = "resources/PlayerMove2.png";
            }
			if (timer > 1.0f) {
				timer = 0.0f;
            }
		}


		private void BorderWrap()
		{
			int swidth = (int)Settings.ScreenSize.X;
			int sheight = (int)Settings.ScreenSize.Y;

			if (Position.X > swidth)  { Position.X = 0; }
			if (Position.X < 0)       { Position.X = swidth; }
			if (Position.Y > sheight) { Position.Y = 0; }
			if (Position.Y < 0)       { Position.Y = sheight; }
		}
	}
}

