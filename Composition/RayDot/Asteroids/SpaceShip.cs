using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib

namespace UserLand
{
	class SpaceShip : Node
	{
		private float rotSpeed;
		private float thrustForce;

		private Node body;
		private Node engineLeft;
		private Node engineRight;
		public Node Body {
			get { return body; }
		}

		private float bulletCoolDown;

		public SpaceShip() : base()
		{
			Transform.Position = new Vector2(200, Settings.ScreenSize.Y / 2);
			Transform.Scale = new Vector2(0.9f, 0.9f);

			// Body and engines
			body = new Node();
			body.AddComponent("Sprite", new SpriteComponent(body, "resources/player.png"));
			body.Transform.Scale = new Vector2(1.05f, 1.05f);
			body.Sprite.Pivot = new Vector2(0.4f, 0.5f);
			body.Sprite.Color = Color.YELLOW;
			AddChild(body);

			engineLeft = new Node();
			engineLeft.AddComponent("Sprite", new SpriteComponent(engineLeft, "resources/player.png"));
			engineLeft.Transform.Position = new Vector2(0, -12);
			engineLeft.Transform.Scale = new Vector2(0.5f, 0.5f);
			engineLeft.Transform.Rotation = (float) Math.PI / 6;
			engineLeft.Sprite.Color = Color.YELLOW;
			AddChild(engineLeft);

			engineRight = new Node();
			engineRight.AddComponent("Sprite", new SpriteComponent(engineRight, "resources/player.png"));
			engineRight.Transform.Position = new Vector2(0, 12);
			engineRight.Transform.Scale = new Vector2(0.5f, 0.5f);
			engineRight.Transform.Rotation = (float) - Math.PI / 6;
			engineRight.Sprite.Color = Color.YELLOW;
			AddChild(engineRight);

			AddComponent("Mover", new MoverComponent(this));

			Mover.Velocity = new Vector2(0.0f, 0.0f);
			Mover.Acceleration = new Vector2(0.0f, 0.0f);
			Mover.Mass = 1.0f;

			rotSpeed = (float)Math.PI; // rad/second
			thrustForce = 500;

			bulletCoolDown = 0.0f;

			NoThrust();
		}

		public override void Update(float deltaTime)
		{
			// base.Update(deltaTime);

			BorderWrap();
			// BorderBounce();
		}

		public Bullet Shoot(float deltaTime)
		{
			bulletCoolDown += deltaTime;
			if (bulletCoolDown >= 0.1f)
			{
				bulletCoolDown = 0.0f;
				Bullet b = new Bullet(500.0f);
				b.Transform.Rotation = this.Transform.WorldRotation;
				float vel_x = (float)Math.Cos(b.Transform.Rotation);
				float vel_y = (float)Math.Sin(b.Transform.Rotation);
				Vector2 direction = new Vector2(vel_x, vel_y);
				b.Transform.Position = this.Transform.WorldPosition + (direction * this.Body.Sprite.TextureSize.X / 2);
				b.Mover.Velocity = direction * b.Speed;
				return b;
			}
			return null;
		}

		public void RotateRight(float deltaTime)
		{
			Transform.Rotation += rotSpeed * deltaTime;
			engineRight.Sprite.Color = Color.ORANGE;
		}

		public void RotateLeft(float deltaTime)
		{
			Transform.Rotation -= rotSpeed * deltaTime;
			engineLeft.Sprite.Color = Color.ORANGE;
		}

		public void StopRotating()
		{
			engineLeft.Sprite.Color = Color.YELLOW;
			engineRight.Sprite.Color = Color.YELLOW;
		}

		public void Thrust()
		{
			// body.Sprite.Color = Color.ORANGE;
			body.Sprite.TextureName = "resources/playerthrust.png";
			float x = (float)Math.Cos(Transform.Rotation);
			float y = (float)Math.Sin(Transform.Rotation);
			Mover.AddForce(new Vector2(x, y) * thrustForce);
		}

		public void NoThrust()
		{
			body.Sprite.Color = Color.YELLOW;
			body.Sprite.TextureName = "resources/player.png";
		}

		private void BorderWrap()
		{
			int swidth = (int)Settings.ScreenSize.X;
			int sheight = (int)Settings.ScreenSize.Y;

			Vector2 pos = Transform.Position;
			if (pos.X > swidth)  { pos.X = 0; }
			if (pos.X < 0)       { pos.X = swidth; }
			if (pos.Y > sheight) { pos.Y = 0; }
			if (pos.Y < 0)       { pos.Y = sheight; }
			Transform.Position = pos;
		}

		private void BorderBounce()
		{
			int swidth = (int)Settings.ScreenSize.X;
			int sheight = (int)Settings.ScreenSize.Y;
			int halfwidth = (int)Body.Sprite.TextureSize.X / 2;
			int halfheight = (int)Body.Sprite.TextureSize.Y / 2;

			Vector2 pos = Transform.Position;
			Vector2 vel = Mover.Velocity;
			if (pos.X > swidth - halfwidth)   { pos.X = swidth - halfwidth;   vel.X *= -1; }
			if (pos.X < 0 + halfwidth)        { pos.X = 0 + halfwidth;        vel.X *= -1; }
			if (pos.Y > sheight - halfheight) { pos.Y = sheight - halfheight; vel.Y *= -1; }
			if (pos.Y < 0 + halfheight)       { pos.Y = 0 + halfheight;       vel.Y *= -1; }
			Transform.Position = pos;
			Mover.Velocity = vel;
		}
	}
}
