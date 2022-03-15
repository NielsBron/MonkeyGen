using System.Numerics; // Vector2, Matrix4x4
using System.Collections.Generic; // List<>

namespace RayDot
{
	interface ITransformable
	{
		// Data structure
		ITransformable Parent { get; set; }
		List<ITransformable> Children { get; }

		bool AddChild(ITransformable child);
		bool RemoveChild(ITransformable child, bool keepAlive);
		void TransformNode(Matrix4x4 parentMatrix);
	
		// Transform
		Vector2 Position { get; set; }
		float Rotation { get; set; }
		Vector2 Scale { get; set; }

		Vector2 WorldPosition { get; set; }
		float WorldRotation { get; set; }
		Vector2 WorldScale { get; set; }
	}
}
