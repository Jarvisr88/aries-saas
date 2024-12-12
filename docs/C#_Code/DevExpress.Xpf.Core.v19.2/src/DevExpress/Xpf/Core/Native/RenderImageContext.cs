namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class RenderImageContext : FrameworkRenderElementContext
    {
        private ImageSource imageSource;
        private System.Windows.Media.Stretch? stretch;
        private System.Windows.Controls.StretchDirection? stretchDirection;

        public RenderImageContext(RenderImage factory);
        protected override void SvgPaletteChanged(WpfSvgPalette oldValue, WpfSvgPalette newValue);
        protected override void SvgStateChanged(string oldValue, string newValue);

        public ImageSource Source { get; set; }

        public System.Windows.Media.Stretch? Stretch { get; set; }

        public System.Windows.Controls.StretchDirection? StretchDirection { get; set; }

        public WpfSvgRenderer SvgRenderer { get; internal set; }

        public bool UseSvgRenderer { get; internal set; }
    }
}

