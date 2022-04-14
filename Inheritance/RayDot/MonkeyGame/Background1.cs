using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib


namespace UserLand
{
	class Background1 : MoverNode {

		private SpriteNode body;

		public Background1(string name) : base(name) {
			Position = new Vector2(1280 , 720);
			Pivot = new Vector2(1f , 1f);
			Scale = new Vector2(1f , 1f);
			body = new SpriteNode("resources/bg1.png");
			body.Scale = new Vector2(1f , 1f);
			body.Pivot = new Vector2(1f , 1f);
			AddChild(this.body);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}
	}
}

