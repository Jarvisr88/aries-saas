namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class TextureContent
    {
        public TextureContent(double zoomFactor, double angle, bool restored, Bitmap texture)
        {
            this.Angle = angle;
            this.ZoomFactor = zoomFactor;
            this.Texture = texture;
            this.Restored = restored;
        }

        public virtual bool Match(double zoomFactor, double angle, bool restored) => 
            this.ZoomFactor.AreClose(zoomFactor) && (this.Angle.AreClose(angle) && (this.Restored == restored));

        public double Angle { get; private set; }

        public bool Restored { get; private set; }

        public double ZoomFactor { get; private set; }

        public Bitmap Texture { get; private set; }
    }
}

