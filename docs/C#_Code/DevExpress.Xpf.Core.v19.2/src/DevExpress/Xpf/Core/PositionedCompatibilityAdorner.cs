namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PositionedCompatibilityAdorner : CompatibilityAdorner
    {
        public PositionedCompatibilityAdorner(UIElement child) : base(child)
        {
        }

        public void UpdateLocation(Point newPos)
        {
            if (!this.Position.Equals(newPos))
            {
                this.Position = newPos;
                base.Offset = newPos;
            }
        }

        public Point Position { get; private set; }
    }
}

