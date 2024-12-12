namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing;

    public class SvgGraphicsWrapper : IDisposable
    {
        private ISvgGraphics graphicsCore;
        private ISvgGraphics tempGraphicsCore;

        public SvgGraphicsWrapper(ISvgGraphics graphics)
        {
            this.graphicsCore = graphics;
            if (this.graphicsCore == null)
            {
                this.tempGraphicsCore = new DevExpress.Utils.Svg.SvgGraphics();
            }
        }

        public void Dispose()
        {
            this.graphicsCore = null;
            if (this.tempGraphicsCore != null)
            {
                this.tempGraphicsCore.Dispose();
            }
            this.tempGraphicsCore = null;
        }

        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat) => 
            this.SvgGraphics.MeasureString(text, font, origin, stringFormat);

        private ISvgGraphics SvgGraphics =>
            this.graphicsCore ?? this.tempGraphicsCore;
    }
}

