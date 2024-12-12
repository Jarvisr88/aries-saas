namespace DevExpress.Xpf.Core.HandleDecorator
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class CompositeBitmapAttributes
    {
        public CompositeBitmapAttributes(Rectangle compositeBmp, Rectangle leftBorder, Rectangle topBorder, Rectangle rightBorder, Rectangle bottomBorder, ThemeElementPainter painter, bool isActive)
        {
            this.CompositeBmpSize = compositeBmp.Size;
            this.LeftBorderSize = leftBorder.Size;
            this.TopBorderSize = topBorder.Size;
            this.RightBorderSize = rightBorder.Size;
            this.BottomBorderSize = bottomBorder.Size;
            this.SkinPainter = painter;
            this.IsActive = isActive;
        }

        public Size CompositeBmpSize { get; private set; }

        public Size LeftBorderSize { get; private set; }

        public Size TopBorderSize { get; private set; }

        public Size RightBorderSize { get; private set; }

        public Size BottomBorderSize { get; private set; }

        public ThemeElementPainter SkinPainter { get; private set; }

        public bool IsActive { get; private set; }
    }
}

