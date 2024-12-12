namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class TextRunPathInfo : PathInfoBase
    {
        public TextRunPathInfo(GraphicsPath graphicsPath, Brush fill, DevExpress.Office.Drawing.PenInfo penInfo) : base(graphicsPath, fill, penInfo != null)
        {
            this.PenInfo = penInfo;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.PenInfo != null))
            {
                this.PenInfo.Dispose();
                this.PenInfo = null;
            }
            base.Dispose(disposing);
        }

        public override void Draw(Graphics graphics, DevExpress.Office.Drawing.PenInfo penInfo, Matrix shapeTransform)
        {
            base.Draw(graphics, this.PenInfo, shapeTransform);
        }

        protected internal override bool ShouldDrawGlowPath() => 
            true;

        public DevExpress.Office.Drawing.PenInfo PenInfo { get; private set; }
    }
}

