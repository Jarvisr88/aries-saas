namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class RenderViewBoxContext : RenderDecoratorContext
    {
        private System.Windows.Media.Stretch? _stretch;
        private System.Windows.Controls.StretchDirection? _stretchDirection;

        public RenderViewBoxContext(RenderViewBox factory);

        public System.Windows.Media.Stretch? Stretch { get; set; }

        public System.Windows.Controls.StretchDirection? StretchDirection { get; set; }

        public RenderViewBox Factory { get; }
    }
}

