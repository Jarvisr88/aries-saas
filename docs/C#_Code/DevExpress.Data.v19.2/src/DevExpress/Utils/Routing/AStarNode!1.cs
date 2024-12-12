namespace DevExpress.Utils.Routing
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerDisplay("P={Position} (F={F}; G={G}; H={H})")]
    public sealed class AStarNode<T>
    {
        private static readonly EqualityComparer<T> Comparer;
        public readonly T Position;
        public readonly double H;

        static AStarNode()
        {
            AStarNode<T>.Comparer = EqualityComparer<T>.Default;
        }

        public AStarNode(T position, double h)
        {
            this.Position = position;
            this.H = h;
            this.F = this.H;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (obj.GetType() != typeof(AStarNode<T>)))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            AStarNode<T> node = (AStarNode<T>) obj;
            return (AStarNode<T>.Comparer.Equals(this.Position, node.Position) && (ReferenceEquals(this.Parent, node.Parent) && (Math.Abs((double) (this.F - node.F)) <= double.Epsilon)));
        }

        public override int GetHashCode() => 
            HashCodeHelper.Calculate(this.F.GetHashCode(), this.Position.GetHashCode(), this.Parent.GetHashCode());

        public IEnumerable<T> GetPath()
        {
            LinkedList<T> list = new LinkedList<T>();
            while (true)
            {
                list.AddFirst(parent.Position);
                parent = parent.Parent;
                if (parent == null)
                {
                    return list;
                }
            }
        }

        public void UpdateParent(AStarNode<T> parent, double g)
        {
            this.Parent = parent;
            this.G = g;
            this.F = this.G + this.H;
        }

        public double F { get; private set; }

        public double G { get; private set; }

        public AStarNode<T> Parent { get; private set; }
    }
}

