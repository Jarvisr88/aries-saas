namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TextureContent
    {
        public TextureContent(double zoomFactor, double angle, Bitmap texture)
        {
            this.Angle = angle;
            this.ZoomFactor = zoomFactor;
            this.Texture = texture;
        }

        public virtual bool Match(double zoomFactor, double angle) => 
            this.ZoomFactor.AreClose(zoomFactor) && this.Angle.AreClose(angle);

        public double Angle { get; private set; }

        public double ZoomFactor { get; private set; }

        public Bitmap Texture { get; private set; }
    }
}

