using System.Collections.Generic; // List<>
using System.Numerics; // Vector2

namespace RayDot
{
	class Node
	{
		// Data structure
		private List<Node> children;

		private Node parent;
		public Node Parent {
			get { return parent; }
			set { parent = value; }
		}

		// List of Components
		private Dictionary<string, Component> components;
		public Dictionary<string, Component> Components {
			get { return components; }
		}

		// Getters for individual components
		public TransformComponent Transform {
			get { return (TransformComponent) Components["Transform"]; }
		}
		public SpriteComponent Sprite {
			get { return (SpriteComponent) Components["Sprite"]; }
		}
		public MoverComponent Mover {
			get { return (MoverComponent) Components["Mover"]; }
		}
		public TextComponent Text {
			get { return (TextComponent) Components["Text"]; }
		}

		// Constructor
		public Node()
		{
			// Datastructure
			this.children = new List<Node>();
			Parent = null;

			// Components
			this.components = new Dictionary<string, Component>();

			// Every Node has a Transform Component
			TransformComponent transform = new TransformComponent(this);
			AddComponent("Transform", transform);
			Transform.Position = new Vector2(0.0f, 0.0f);
			Transform.Rotation = 0.0f;
			Transform.Scale = new Vector2(1.0f, 1.0f);
		}

		public void AddComponent(string key, Component value)
		{
			components.Add(key, value);
		}

		public virtual void Update(float deltaTime)
		{
			// virtual (override in subclass)
			// or don't, then this will be called
		}

		public bool AddChild(Node child)
		{
			if (children.Contains(child))
			{
				// this is already our child
				return false;
			}
			if (child == this)
			{
				// this is us! we can't be our own child.
				return false;
			}
			if (child.Parent != null) // handle previous owner
			{
				// "kidnap" the child from previous parent
				child.Parent.RemoveChild(child, false);
			}
			child.Parent = this;
			children.Add(child);
			return true;
		}

		public bool RemoveChild(Node child, bool keepAlive = false)
		{
			// we don't know this child
			if (!children.Contains(child))
			{
				return false;
			}

			// do we need to keep this child alive?
			if (keepAlive)
			{
				// pass back up to our parent
				if (this.parent == null)
				{
					// we're the scene, we have no parents
					return false;
				}
				child.Parent = this.parent;
				child.Parent.AddChild(child);
			}

			// remove from our children
			children.Remove(child);
			return true;
		}

		public bool HasChild(Node child)
		{
			return children.Contains(child);
		}

		public void ClearChildren()
		{
			children.Clear();
		}

		public void TransformNode(Matrix4x4 parentMatrix)
		{
			// ========== Transform all nodes ==========
			// locals (we need Vec3 to use with Mat4 in System.Numerics)
			Vector3 position = new Vector3(Transform.Position, 0.0f);
			Vector3 rotation = new Vector3(0.0f, 0.0f, Transform.Rotation);
			Vector3 scale = new Vector3(Transform.Scale, 1.0f);

			// build individual translation, rotation and scale Matrices
			Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(position);
			Matrix4x4 rotMatZ = Matrix4x4.CreateRotationZ(rotation.Z);
			// Matrix4x4 rotMatX = Matrix4x4.CreateRotationX(rotation.X);
			// Matrix4x4 rotMatY = Matrix4x4.CreateRotationX(rotation.Y);
			// Matrix4x4 rotationMatrix = rotMatX * rotMatY * rotMatZ;
			Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);

			// build modelMatrix for this Entity
			// Matrix4x4 modelMatrix = scaleMatrix * rotationMatrix * translationMatrix;
			Matrix4x4 modelMatrix = scaleMatrix * rotMatZ * translationMatrix;

			// multiply with parent
			modelMatrix *= parentMatrix;

			// extract world coords
			Vector3 worldpos;
			Quaternion worldrotQ;
			Vector3 worldscl;
			Matrix4x4.Decompose(modelMatrix, out worldscl, out worldrotQ, out worldpos);

			// set World coords
			Transform.WorldPosition = new Vector2(worldpos.X, worldpos.Y);
			Transform.WorldRotation = Vector3.Transform(rotation, worldrotQ).Z; // TODO check
			// Rotation is not inherited from parent. For now, we hack it in.
			if (Parent != null) {
				Transform.WorldRotation = Parent.Transform.WorldRotation + this.Transform.Rotation;
			}
			Transform.WorldScale = new Vector2(worldscl.X, worldscl.Y);

			// transform all children
			for (int i=0; i<children.Count; i++)
			{
				children[i].TransformNode(modelMatrix);
			}
		}

		// "internal" methods to be called from Core
		// Updates all children recursively
		public void UpdateNode(float deltaTime)
		{
			Update(deltaTime);

			// Update all children
			for (int i=0; i<children.Count; i++)
			{
				((Node)children[i]).UpdateNode(deltaTime);
			}
		}

		public void TickComponents(float deltaTime)
		{
			foreach (var component in Components) {
				component.Value.Tick(deltaTime);
			}

			// Tick components for all children
			for (int i=0; i<children.Count; i++)
			{
				((Node)children[i]).TickComponents(deltaTime);
			}
		}

	}
}
