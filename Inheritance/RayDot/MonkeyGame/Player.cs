using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib

namespace UserLand
{
	class Player : MoverNode
	{
		private float speed;
		public Player(string name) : base(name)
		{
			Position = new Vector2(200, Settings.ScreenSize.Y / 2);
			Pivot = new Vector2(0.45f, 0.5f);
			Scale = new Vector2(2f, 2f);
			
			speed = 500;
		}

		public void Speed()
		{
			float x = (float)Math.Cos(Rotation);
			float y = (float)Math.Sin(Rotation);
			AddForce(new Vector2(x, y) * speed);
		}
	}
}
