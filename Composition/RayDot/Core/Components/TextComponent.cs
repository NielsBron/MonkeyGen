using System; // String
using Raylib_cs; // Raylib

namespace RayDot
{
	class TextComponent : Component
	{
		private String text;
		private int fontsize;
		private Color color;

		public String PrintText {
			get { return text; }
			set { text = value; }
		}
		public int FontSize {
			get { return fontsize; }
			set { fontsize = value; }
		}
		public Color Color {
			get { return color; }
			set { color = value; }
		}

		public TextComponent(Node n, string txt, int size) : base(n)
		{
			PrintText = txt;
			FontSize = size;
			Color = Color.WHITE;
		}

		public override void Tick(float deltaTime)
		{
			// base.Tick(deltaTime); // Component.Tick()

			Draw();
		}

		private void Draw()
		{
			int posx = (int) Owner.Transform.Position.X;
			int posy = (int) Owner.Transform.Position.Y;
			Raylib.DrawText(PrintText, posx, posy, FontSize, Color);
		}

	}
}
