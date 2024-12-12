namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public abstract class FrameworkRenderElement : FreezableRenderObject, IFrameworkRenderElement
    {
        private static readonly object unsetValue;
        private bool? useLayoutRounding;
        private double width;
        private double height;
        private double minWidth;
        private double maxWidth;
        private double minHeight;
        private double maxHeight;
        private Thickness margin;
        private string name;
        private bool allowMouseCapturing;
        private System.Windows.HorizontalAlignment ha;
        private System.Windows.VerticalAlignment va;
        private System.Windows.Visibility vi;
        private System.Windows.Controls.Dock? dock;
        private System.Windows.FlowDirection? fd;
        private double opacity;
        private bool showBounds;
        private bool shouldCalcDpiAwareThickness;
        private Brush foreground;
        private RenderFontSettings fontSettings;
        private bool clipToBounds;
        private bool contentSpecificClipToBounds;
        private bool bypassLayoutPolicies;
        private WpfSvgPalette svgPalette;
        private string svgState;

        static FrameworkRenderElement();
        protected FrameworkRenderElement();
        private bool ApplyMirrorTransform(System.Windows.FlowDirection parentFD, System.Windows.FlowDirection thisFD);
        protected virtual bool ApplyProperty(string propertyName, object value);
        public void ApplyTemplate(FrameworkRenderElementContext context);
        public void Arrange(Rect finalRect, FrameworkRenderElementContext context);
        private void ArrangeCore(Rect finalRect, FrameworkRenderElementContext context);
        protected virtual Size ArrangeOverride(Size finalSize, IFrameworkRenderElementContext context);
        protected Thickness CalcDpiAwareThickness(FrameworkRenderElementContext context, Thickness thickness);
        protected virtual bool CalcNeedToClipSlot(Size inkSize, FrameworkRenderElementContext context);
        private System.Windows.Visibility CalcParentVisibility(FrameworkRenderElementContext context);
        private System.Windows.Visibility CalcRenderVisibility(FrameworkRenderElementContext context);
        private System.Windows.Visibility CalcVisibility(FrameworkRenderElementContext context);
        private Vector ComputeAlignmentOffset(Size clientSize, Size inkSize, System.Windows.HorizontalAlignment horizontalAlignment, System.Windows.VerticalAlignment verticalAlignment);
        protected internal FrameworkRenderElementContext CreateContext(Namescope namescope);
        protected virtual FrameworkRenderElementContext CreateContextInstance();
        bool IFrameworkRenderElement.ApplySetter(RenderStyleSetter setter);
        private static void EnsureInvisible(FrameworkRenderElementContext context, bool collapsed);
        private static void EnsureVisible(FrameworkRenderElementContext context);
        protected override void FreezeOverride();
        protected abstract IEnumerable<IFrameworkRenderElement> GetChildren();
        private Transform GetFlowDirectionTransform(FrameworkRenderElementContext context);
        protected virtual Geometry GetLayoutClip(Size layoutSlotSize, FrameworkRenderElementContext context);
        protected virtual Geometry GetLayoutClipBase(Size layoutSlotSize, FrameworkRenderElementContext context);
        protected virtual void InitializeContext(FrameworkRenderElementContext context);
        public void Measure(Size availableSize, FrameworkRenderElementContext context);
        private Size MeasureCore(Size availableSize, FrameworkRenderElementContext context);
        protected virtual Size MeasureOverride(Size availableSize, IFrameworkRenderElementContext context);
        protected virtual void OnApplyTemplate(FrameworkRenderElementContext context);
        protected virtual void PostApplyTemplate(FrameworkRenderElementContext context);
        protected virtual void PreApplyTemplate(FrameworkRenderElementContext context);
        public static void PropagateResumeLayout(FrameworkRenderElementContext parent, FrameworkRenderElementContext context);
        public static void PropagateSuspendLayout(FrameworkRenderElementContext context);
        public void Render(DrawingContext dc, FrameworkRenderElementContext context);
        private void RenderCore(DrawingContext dc, FrameworkRenderElementContext context);
        protected virtual void RenderOverride(DrawingContext dc, IFrameworkRenderElementContext context);
        protected static Rect RoundLayoutRect(FrameworkRenderElementContext context, Rect rect);
        protected static Size RoundLayoutSize(FrameworkRenderElementContext context, Size size);
        protected static double RoundLayoutValue(IFrameworkRenderElementContext context, double value);
        private bool ShouldApplyMirrorTransform(FrameworkRenderElementContext context);
        private bool ShouldUseLayoutClip(FrameworkRenderElementContext context);
        private static void SignalDesiredSizeChange(FrameworkRenderElementContext context);
        public static void SwitchVisibilityIfNeeded(FrameworkRenderElementContext context, System.Windows.Visibility visibility);

        public static object UnsetValue { [DebuggerStepThrough] get; }

        public FrameworkRenderElement Parent { get; internal set; }

        public WpfSvgPalette SvgPalette { get; set; }

        public string SvgState { get; set; }

        public Brush Foreground { get; set; }

        public RenderFontSettings FontSettings { get; set; }

        public System.Windows.FlowDirection? FlowDirection { get; set; }

        public bool ShowBounds { get; set; }

        public bool? UseLayoutRounding { get; set; }

        public bool ClipToBounds { get; set; }

        public bool ContentSpecificClipToBounds { get; set; }

        public double Opacity { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double MinWidth { get; set; }

        public double MaxWidth { get; set; }

        public double MinHeight { get; set; }

        public double MaxHeight { get; set; }

        public Thickness Margin { get; set; }

        public string Name { get; set; }

        public bool AllowMouseCapturing { get; set; }

        public System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        public System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        public System.Windows.Visibility Visibility { get; set; }

        public System.Windows.Controls.Dock? Dock { get; set; }

        public bool ShouldCalcDpiAwareThickness { get; set; }

        public bool BypassLayoutPolicies { get; set; }

        public string ThemeInfo { get; set; }

        IEnumerable<IFrameworkRenderElement> IFrameworkRenderElement.Children { get; }

        [StructLayout(LayoutKind.Sequential)]
        private struct MinMax
        {
            public readonly double MinWidth;
            public readonly double MaxWidth;
            public readonly double MinHeight;
            public readonly double MaxHeight;
            internal MinMax(FrameworkRenderElementContext context);
        }
    }
}

