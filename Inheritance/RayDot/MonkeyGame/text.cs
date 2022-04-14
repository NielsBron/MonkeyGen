using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib


namespace UserLand
{
	class Text : MoverNode
	{

		private SpriteNode body;
		private float timer;

		public Text(string name) : base(name) {
			Position = new Vector2(100 , 550);
			Pivot = new Vector2(0.15f , 0.8f);
			Scale = new Vector2(3f , 3f);
			body = new SpriteNode("");
			body.Scale = new Vector2(1.05f , 1.05f);
			body.Pivot = new Vector2(2f , 2f);
			AddChild(this.body);
		}

		public override void Update(float deltaTime)
		{
			base.Update(deltaTime);
		}
		public void Text1(float deltaTime) {
				TextureName = "resources/sus.png";
		}
		public void Text2(float deltaTime) {
				TextureName = "resources/sus2.png";
		}
	}
}

