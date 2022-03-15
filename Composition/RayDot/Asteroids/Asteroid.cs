using System.Numerics; // Vector2
using RayDot; // RayDot

namespace UserLand
{
	enum Generation
	{
		First,
		Second,
		Third
	}

	class Asteroid : Node
	{
		public float RotSpeed { get; set; }
		public Generation Generation { get; set; }

		public Asteroid(Generation gen) : base()
		{
			AddComponent("Sprite", new SpriteComponent(this, "resources/asteroid.png"));
			AddComponent("Mover", new MoverComponent(this));

			RotSpeed = 0.0f;
			Generation = gen;
		}

		public override void Update(float deltaTime)
		{
			// base.Update(deltaTime);

			Transform.Rotation += RotSpeed * deltaTime / 3;

			BorderWrap();
		}

		public void BorderWrap()
		{
			int swidth = (int)Settings.ScreenSize.X;
			int sheight = (int)Settings.ScreenSize.Y;

			Vector2 pos = Transform.Position;
			Vector2 scale = Transform.Scale;
			if (pos.X > swidth + 64 * scale.X)  { pos.X = 0 - 64 * scale.X; }
			if (pos.X < 0 - 64 * scale.X)       { pos.X = swidth + 64 * scale.X; }
			if (pos.Y > sheight + 64 * scale.Y) { pos.Y = 0 - 64 * scale.Y; }
			if (pos.Y < 0 - 64 * scale.Y)       { pos.Y = sheight + 64 * scale.Y; }
			Transform.Position = pos;
		}
	}
}
