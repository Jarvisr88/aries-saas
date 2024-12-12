namespace DevExpress.Office.Utils
{
    using DevExpress.Data.Helpers;
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class GraphicsToLayoutUnitsModifier : IDisposable
    {
        private readonly Graphics graphics;
        private readonly DocumentLayoutUnitConverter unitConverter;
        private GraphicsUnit oldUnit;
        private float oldScale;
        private HdcDpiModifier hdcDpiModifier;
        private Matrix oldMatrix;

        public GraphicsToLayoutUnitsModifier(Graphics graphics, DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(graphics, "graphics");
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.graphics = graphics;
            this.unitConverter = unitConverter;
            this.Apply();
        }

        protected void Apply()
        {
            this.oldUnit = this.graphics.PageUnit;
            this.oldScale = this.graphics.PageScale;
            this.oldMatrix = this.graphics.Transform;
            this.graphics.PageUnit = (GraphicsUnit) this.unitConverter.GraphicsPageUnit;
            this.graphics.PageScale = this.unitConverter.GraphicsPageScale;
            this.graphics.ResetTransform();
            if (SecurityHelper.IsUnmanagedCodeGrantedAndCanUseGetHdc)
            {
                this.hdcDpiModifier = new HdcDpiModifier(this.graphics, new Size(0x1000, 0x1000), (int) Math.Round((double) this.unitConverter.Dpi));
            }
        }

        public void Dispose()
        {
            this.Restore();
        }

        protected void Restore()
        {
            try
            {
                if ((this.hdcDpiModifier != null) && SecurityHelper.IsUnmanagedCodeGrantedAndCanUseGetHdc)
                {
                    this.hdcDpiModifier.Dispose();
                }
                this.graphics.PageUnit = this.oldUnit;
                this.graphics.PageScale = this.oldScale;
                this.graphics.ResetTransform();
                this.graphics.Transform = this.oldMatrix;
            }
            catch
            {
            }
        }
    }
}

