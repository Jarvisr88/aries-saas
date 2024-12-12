namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class TextLinePathInfo : TextRunPathInfo
    {
        public TextLinePathInfo(GraphicsPath graphicsPath, Brush fill, PenInfo penInfo) : base(graphicsPath, fill, penInfo)
        {
        }

        public override void Draw(Graphics graphics, PenInfo penInfo, Matrix shapeTransform)
        {
            SmoothingMode smoothingMode = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.None;
            base.Draw(graphics, penInfo, shapeTransform);
            graphics.SmoothingMode = smoothingMode;
        }
    }
}

