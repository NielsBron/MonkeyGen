using System.Numerics; // Vector2

namespace RayDot
{
	class MoverComponent : Component
	{
		// Velocity, Acceleration, Mass
		private Vector2 velocity;
		private Vector2 acceleration;
		private float mass;

		public Vector2 Velocity {
			get { return velocity; }
			set { velocity = value; }
		}
		public Vector2 Acceleration {
			get { return acceleration; }
			set { acceleration = value; }
		}
		public float Mass {
			get { return mass; }
			set { mass = value; }
		}

		public MoverComponent(Node n) : base(n)
		{
			Velocity = new Vector2(0.0f, 0.0f);
			Acceleration = new Vector2(0.0f, 0.0f);
			Mass = 1.0f;
		}

		public override void Tick (float deltaTime)
		{
			// base.Tick(deltaTime); // Component.Tick()

			Move(deltaTime);
		}

		// Move(float deltaTime), AddForce(Vector2 force)
		private void Move(float deltaTime)
		{
			// apply motion 101
			Velocity += Acceleration * deltaTime;
			Owner.Transform.Position += Velocity * deltaTime;
			// reset acceleration
			Acceleration *= 0.0f;
		}

		public void AddForce(Vector2 force)
		{
			Acceleration += force / Mass;
		}
	}
}
