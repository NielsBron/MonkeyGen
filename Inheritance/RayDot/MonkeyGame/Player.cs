using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib


namespace UserLand
{
	class Player : MoverNode {

		private SpriteNode body;
		private float Speed;

		public Player(string name) : base(name) {
			Position = new Vector2(100, 550);
			Pivot = new Vector2(0.45f , 0.5f);
			Scale = new Vector2(3f , 3f);

			Speed= 500;

			body = new SpriteNode("resources/PlayerIdle.png");
			body.Scale = new Vector2(1.05f , 1.05f);
			body.Pivot = new Vector2(2f , 2f);
			body.Color = Color.YELLOW;
			AddChild(this.body);
		}

		public override void Update(float deltaTime) // override implementation of MoverNode.Update()
		{
			// MoverNode (IMovable)
			base.Update(deltaTime);
			// Or do:
			// Move(deltaTime);

			BorderWrap();
		}
		public void Walk1() {
			TextureName = "resources/PlayerMove1.png";
			//float x = (float)Math.Cos(Rotation);
			//float y = (float)Math.Sin(Rotation);
			//AddForce(new Vector2(x, y) * Speed);
		}
		public void Walk2() {
			TextureName = "resources/PlayerMove2.png";
		}
		public void Idle() {
			TextureName = "resources/PlayerIdle.png";
		}
		public void Walk() {
		
		}


		private void BorderWrap()
		{
			int swidth = (int)Settings.ScreenSize.X;
			int sheight = (int)Settings.ScreenSize.Y;

			// access protected fields in Node
			if (position.X > swidth)  { position.X = 0; }
			if (position.X < 0)       { position.X = swidth; }
			if (position.Y > sheight) { position.Y = 0; }
			if (position.Y < 0)       { position.Y = sheight; }
		}
	}
}

