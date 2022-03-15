using RayDot; // RayDot

namespace UserLand
{
	class Bullet : Node
	{
		public float Speed { get; set; }

		public Bullet(float speed) : base()
		{
			AddComponent("Sprite", new SpriteComponent(this, "resources/bullet.png"));
			AddComponent("Mover", new MoverComponent(this));

			Speed = speed;
		}

		public override void Update(float deltaTime)
		{
			// base.Update(deltaTime);
		}
	}
}
