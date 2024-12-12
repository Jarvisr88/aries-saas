namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class RenderViewBox : RenderDecorator
    {
        private static readonly Func<Size, Size, System.Windows.Media.Stretch, System.Windows.Controls.StretchDirection, Size> ComputeScaleFactor;
        private System.Windows.Media.Stretch _stretch;
        private System.Windows.Controls.StretchDirection _stretchDirection;

        static RenderViewBox();
        public RenderViewBox();
        protected override Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected override FrameworkRenderElementContext CreateContextInstance();
        protected override Size MeasureOverride(Size constraint, IFrameworkRenderElementContext context);

        public System.Windows.Media.Stretch Stretch { get; set; }

        public System.Windows.Controls.StretchDirection StretchDirection { get; set; }
    }
}

