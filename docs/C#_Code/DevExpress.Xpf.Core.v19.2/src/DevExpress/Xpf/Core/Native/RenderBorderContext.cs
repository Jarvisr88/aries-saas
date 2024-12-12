namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class RenderBorderContext : RenderDecoratorContext
    {
        private Brush background;
        private Brush borderBrush;
        private Brush frozenBackground;
        private Brush frozenBorderBrush;
        private Thickness? borderThickness;
        private Thickness? padding;
        private System.Windows.CornerRadius? cornerRadius;
        private WeakEventHandler<RenderBorderContext, EventArgs, EventHandler> weakOnBrushAnimated;

        public RenderBorderContext(RenderBorder factory);
        private void OnBrushAnimated(object sender, EventArgs e);
        protected void OnBrushChanged(Brush oldValue, Brush newValue);
        public override void Release();
        protected override void RenderSizeChanged();
        protected override void ResetRenderCachesInternal();
        protected internal override void ResetSizeSpecificCaches();

        protected internal Geometry Clip { get; set; }

        public Brush Background { get; set; }

        public Brush BorderBrush { get; set; }

        public Thickness? BorderThickness { get; set; }

        public Thickness? Padding { get; set; }

        public System.Windows.CornerRadius? CornerRadius { get; set; }

        public Pen LeftPenCache { get; internal set; }

        public Pen RightPenCache { get; internal set; }

        public Pen TopPenCache { get; internal set; }

        public Pen BottomPenCache { get; internal set; }

        public bool UseComplexRenderCodePath { get; internal set; }

        public StreamGeometry BackgroundGeometryCache { get; internal set; }

        public StreamGeometry BorderGeometryCache { get; internal set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderBorderContext.<>c <>9;
            public static Action<RenderBorderContext, object, EventArgs> <>9__55_0;
            public static Action<WeakEventHandler<RenderBorderContext, EventArgs, EventHandler>, object> <>9__55_1;
            public static Func<WeakEventHandler<RenderBorderContext, EventArgs, EventHandler>, EventHandler> <>9__55_2;

            static <>c();
            internal void <.ctor>b__55_0(RenderBorderContext owner, object sender, EventArgs args);
            internal void <.ctor>b__55_1(WeakEventHandler<RenderBorderContext, EventArgs, EventHandler> weakHandler, object target);
            internal EventHandler <.ctor>b__55_2(WeakEventHandler<RenderBorderContext, EventArgs, EventHandler> weakHandler);
        }
    }
}

