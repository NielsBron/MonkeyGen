using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib


namespace UserLand
{
	class Background2 : MoverNode {

		private SpriteNode body;
		private float Speed;
		private float timer;

		public Background2(string name) : base(name) {
			Position = new Vector2(1280 , 720);
			Pivot = new Vector2(1f , 1f);
			Scale = new Vector2(1f , 1f);

			Speed = 30;

			body = new SpriteNode("resources/bg2.png");
			body.Scale = new Vector2(1f , 1f);
			body.Pivot = new Vector2(1f , 1f);
			AddChild(this.body);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}

		public void Right(float deltaTime) {
			Position.X += Speed * deltaTime;
		}

		public void Left(float deltaTime) {
			Position.X -= Speed * deltaTime;
		}
	}
}

