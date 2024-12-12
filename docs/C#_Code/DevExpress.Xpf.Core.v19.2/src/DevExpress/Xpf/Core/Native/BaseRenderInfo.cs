namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class BaseRenderInfo : IRenderInfo
    {
        public BaseRenderInfo();
        public void Render(DrawingContext dc, Rect bounds);
        protected abstract void RenderOverride(DrawingContext dc, Rect bounds);

        public Thickness Margin { get; set; }

        public double RelativeHeight { get; set; }

        public double RelativeWidth { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        public System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        public int ZIndex { get; set; }
    }
}

