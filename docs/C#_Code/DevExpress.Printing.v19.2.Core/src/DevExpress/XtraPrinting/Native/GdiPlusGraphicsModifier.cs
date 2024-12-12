namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class GdiPlusGraphicsModifier : GraphicsModifier
    {
        public override void Dispose();
        public override void DrawImage(Graphics gr, Image image, Point position);
        public override void DrawImage(Graphics gr, Image image, RectangleF bounds);
        public override void DrawString(Graphics gr, string s, Font font, Brush brush, RectangleF bounds, StringFormat format);
        public override void OnGraphicsDispose();
        public override void ScaleTransform(Graphics gr, float sx, float sy, MatrixOrder order);
        public override void SetPageUnit(Graphics gr, GraphicsUnit value);
        private object ValidateObject(object obj);
    }
}

