namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    internal sealed class NullRenderInfo : BaseRenderInfo
    {
        private static NullRenderInfo instance;

        private NullRenderInfo();
        protected override void RenderOverride(DrawingContext dc, Rect bounds);

        public static NullRenderInfo Instance { get; }
    }
}

