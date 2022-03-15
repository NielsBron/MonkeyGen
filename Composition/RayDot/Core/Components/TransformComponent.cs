using System.Numerics; // Vector2

namespace RayDot
{
	class TransformComponent : Component
	{
		// Transform
		private Vector2 position;
		private float rotation;
		private Vector2 scale;

		public Vector2 Position {
			get { return position; }
			set { position = value; }
		}
		public float Rotation {
			get { return rotation; }
			set { rotation = value; }
		}
		public Vector2 Scale {
			get { return scale; }
			set { scale = value; }
		}

		// Node.TransformNode() sets these values after transform.
		private Vector2 worldPosition;
		private float worldRotation;
		private Vector2 worldScale;

		public Vector2 WorldPosition {
			get { return worldPosition; }
			set { worldPosition = value; }
		}
		public float WorldRotation {
			get { return worldRotation; }
			set { worldRotation = value; }
		}
		public Vector2 WorldScale {
			get { return worldScale; }
			set { worldScale = value; }
		}

		public TransformComponent(Node n) : base(n)
		{
			
		}

		/* We don't need a Tick(). Nothing happens here.*/
		// public override void Tick(float deltaTime)
		// {
		// }

	}
}
