namespace RayDot
{
	abstract class Component
	{
		private Node owner;
		public Node Owner {
			get { return owner; }
		}

		public Component(Node n)
		{
			owner = n;
		}

		public virtual void Tick (float deltaTime)
		{
			// virtual (override in subclass)
			// or don't, then this will be called
		}

	}
	
}
