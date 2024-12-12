namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class GraphicsModifier
    {
        protected GraphicsModifier();
        public abstract void Dispose();
        public abstract void DrawImage(Graphics gr, Image image, Point position);
        public abstract void DrawImage(Graphics gr, Image image, RectangleF bounds);
        public abstract void DrawString(Graphics gr, string s, Font font, Brush brush, RectangleF bounds, StringFormat format);
        public abstract void OnGraphicsDispose();
        public abstract void ScaleTransform(Graphics gr, float sx, float sy, MatrixOrder order);
        public abstract void SetPageUnit(Graphics gr, GraphicsUnit value);
    }
}

