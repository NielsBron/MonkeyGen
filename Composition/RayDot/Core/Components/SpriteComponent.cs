using System; // Math.PI
using System.Numerics; // Vector2
using Raylib_cs; // Raylib

namespace RayDot
{
	class SpriteComponent : Component
	{
		// Sprite
		private string textureName;
		private Vector2 textureSize;
		private Vector2 pivot;
		private Color color;

		public string TextureName {
			get { return textureName; }
			set { textureName = value; }
		}
		public Vector2 TextureSize {
			get { return textureSize; }
			set { textureSize = value; }
		}
		public Vector2 Pivot {
			get { return pivot; }
			set { pivot = value; }
		}
		public Color Color {
			get { return color; }
			set { color = value; }
		}

		public SpriteComponent(Node n, string name) : base(n)
		{
			TextureName = name;
			TextureSize = new Vector2(0, 0); // Draw() updates this if necessary
			Pivot = new Vector2(0.5f, 0.5f);
			Color = Color.WHITE;
		}

		public override void Tick(float deltaTime)
		{
			// base.Tick(deltaTime); // Component.Tick()

			Draw();
		}

		private void Draw()
		{
			ResourceManager resman = ResourceManager.Instance;
			Texture2D texture = resman.GetTexture(TextureName);
			float width = texture.width;
			float height = texture.height;
			// this Entity might not know its Size yet...
			if (TextureSize.X == 0)
			{
				Vector2 size = new Vector2(width, height);
				TextureSize = size;
			}
			// draw the Texture
			Rectangle sourceRec = new Rectangle(0.0f, 0.0f, width, height);
			Rectangle destRec = new Rectangle(Owner.Transform.WorldPosition.X, Owner.Transform.WorldPosition.Y, width * Owner.Transform.WorldScale.X, height * Owner.Transform.WorldScale.Y);
			Vector2 pivot = new Vector2(width * Pivot.X * Owner.Transform.WorldScale.X, height * Pivot.Y * Owner.Transform.WorldScale.Y);
			float rot = Owner.Transform.WorldRotation * 180 / (float) Math.PI;
			Raylib.DrawTexturePro(texture, sourceRec, destRec, pivot, rot, Color);
		}

	}
}
