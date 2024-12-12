namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class ThemeElementInfo
    {
        public ThemeElementInfo(HandleDecoratorWindowTypes windowType, Rectangle bounds, bool isActive, int imgIndex)
        {
            this.WindowType = windowType;
            this.Bounds = bounds;
            this.Active = isActive;
            this.ImageIndex = imgIndex;
        }

        public HandleDecoratorWindowTypes WindowType { get; private set; }

        public bool Active { get; private set; }

        public Rectangle Bounds { get; private set; }

        public int Width =>
            this.Bounds.Width;

        public int Height =>
            this.Bounds.Height;

        public int ImageIndex { get; private set; }
    }
}

