namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Children")]
    public class RenderInfoGroup : BaseRenderInfo
    {
        public RenderInfoGroup();
        protected override void RenderOverride(DrawingContext dc, Rect bounds);

        public List<IRenderInfo> Children { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderInfoGroup.<>c <>9;
            public static Func<IRenderInfo, int> <>9__5_0;

            static <>c();
            internal int <RenderOverride>b__5_0(IRenderInfo x);
        }
    }
}

