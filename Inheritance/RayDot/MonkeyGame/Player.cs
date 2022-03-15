using System; // Math
using System.Numerics; // Vector2
using RayDot; // RayDot
using Raylib_cs; // Raylib

namespace UserLand
{
	class Player : MoverNode
	{

		public Player(string name) : base(name)
		{
			Position = new Vector2(200, Settings.ScreenSize.Y / 2);
			Pivot = new Vector2(0.45f, 0.5f);
			Scale = new Vector2(0.9f, 0.9f);
		}
	}
}
