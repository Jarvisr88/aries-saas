namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class DXBorder : Border, IChrome, IElementOwner
    {
        private static readonly DevExpress.Xpf.Core.Native.RenderTemplate RenderTemplate;
        private ChromeSlave slave;
        private bool useLayoutRoundingCache;
        private bool? enableDpiCorrection;

        static DXBorder()
        {
            Type forType = typeof(DXBorder);
            RenderTemplate = CreateRenderTemplate();
            Border.BackgroundProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            Border.BorderBrushProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            Border.BorderThicknessProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            Border.CornerRadiusProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.MarginProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            Border.PaddingProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.MinWidthProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.MaxWidthProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.WidthProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.MinHeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.HeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.MaxHeightProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.HorizontalAlignmentProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.VerticalAlignmentProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            UIElement.VisibilityProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(new PropertyChangedCallback(DXBorder.UpdateContextPropertyValue)));
            FrameworkElement.FlowDirectionProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(FlowDirection.LeftToRight, ChromeSlave.GetDefaultOptions(FrameworkElement.FlowDirectionProperty), (o, args) => ChromeSlave.CoerceValue((IChrome) o, FrameworkRenderElementContext.FlowDirectionPropertyKey)));
            FrameworkElement.UseLayoutRoundingProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(false, ChromeSlave.GetDefaultOptions(FrameworkElement.UseLayoutRoundingProperty), new PropertyChangedCallback(DXBorder.UseLayoutRoundingPropertyChanged)));
            ThemeManager.TreeWalkerProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(null, ChromeSlave.GetDefaultOptions(ThemeManager.TreeWalkerProperty), new PropertyChangedCallback(DXBorder.OnTreeWalkerPropertyChanged)));
        }

        public DXBorder()
        {
            this.NeverMeasuredWithRenderTree = true;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (!this.ActualEnableDPICorrection)
            {
                return base.ArrangeOverride(finalSize);
            }
            this.EnsureSlave();
            return this.slave.ArrangeOverride(finalSize);
        }

        private void AssignProperties(FrameworkRenderElementContext context)
        {
            context.SetValue(Border.BackgroundProperty.Name, base.Background);
            context.SetValue(Border.BorderBrushProperty.Name, base.BorderBrush);
            context.SetValue(Border.BorderThicknessProperty.Name, base.BorderThickness);
            context.SetValue(Border.CornerRadiusProperty.Name, base.CornerRadius);
            context.SetValue(FrameworkElement.HorizontalAlignmentProperty.Name, base.HorizontalAlignment);
            context.SetValue(FrameworkElement.MarginProperty.Name, base.Margin);
            context.SetValue(FrameworkElement.MaxHeightProperty.Name, base.MaxHeight);
            context.SetValue(FrameworkElement.MaxWidthProperty.Name, base.MaxWidth);
            context.SetValue(FrameworkElement.MinHeightProperty.Name, base.MinHeight);
            context.SetValue(FrameworkElement.MinWidthProperty.Name, base.MinWidth);
            context.SetValue(FrameworkElement.HeightProperty.Name, base.Height);
            context.SetValue(FrameworkElement.WidthProperty.Name, base.Width);
            context.SetValue(Border.PaddingProperty.Name, base.Padding);
            context.SetValue(FrameworkElement.VerticalAlignmentProperty.Name, base.VerticalAlignment);
            context.SetValue(UIElement.VisibilityProperty.Name, base.Visibility);
            context.SetValue(FrameworkElement.UseLayoutRoundingProperty.Name, base.UseLayoutRounding);
        }

        private static DevExpress.Xpf.Core.Native.RenderTemplate CreateRenderTemplate()
        {
            DevExpress.Xpf.Core.Native.RenderTemplate template = new DevExpress.Xpf.Core.Native.RenderTemplate();
            RenderBorder border1 = new RenderBorder();
            border1.Child = new DXBorderRenderElementStub();
            template.RenderTree = border1;
            return template;
        }

        private void DestroySlave()
        {
            if (this.slave != null)
            {
                this.slave.DestroyContext();
            }
            this.slave = null;
        }

        void IChrome.AddChild(FrameworkElement element)
        {
            base.AddLogicalChild(element);
            base.AddVisualChild(element);
        }

        bool IChrome.CaptureMouse(FrameworkRenderElementContext context)
        {
            this.EnsureSlave();
            return this.slave.CaptureMouse(context);
        }

        void IChrome.GoToState(string stateName)
        {
            if (this.ActualEnableDPICorrection)
            {
                this.EnsureSlave();
                this.slave.GoToState(stateName);
            }
        }

        void IChrome.InvalidateArrange()
        {
            this.EnsureSlave();
            this.slave.InvalidateArrange();
        }

        void IChrome.InvalidateMeasure()
        {
            this.EnsureSlave();
            this.slave.InvalidateMeasure();
        }

        void IChrome.InvalidateVisual()
        {
            base.InvalidateVisual();
        }

        void IChrome.ReleaseMouseCapture(FrameworkRenderElementContext context)
        {
            this.EnsureSlave();
            this.slave.ReleaseMouseCapture(context);
        }

        void IChrome.RemoveChild(FrameworkElement element)
        {
            base.RemoveLogicalChild(element);
            base.RemoveVisualChild(element);
        }

        private void EnsureSlave()
        {
            if (this.slave == null)
            {
                this.InitializeSlave();
            }
        }

        private FrameworkRenderElementContext InitializeContext() => 
            this.slave.CreateContext(RenderTemplate, new Action<FrameworkRenderElementContext>(this.AssignProperties));

        private void InitializeSlave()
        {
            this.slave = new ChromeSlave(this, false, new Func<FrameworkRenderElementContext>(this.InitializeContext), new Action<FrameworkRenderElementContext>(this.ReleaseContext), null, null, null, true, null);
            this.NeverMeasuredWithRenderTree = true;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (!this.ActualEnableDPICorrection)
            {
                this.DestroySlave();
                return base.MeasureOverride(availableSize);
            }
            this.EnsureSlave();
            Size size = this.slave.MeasureOverride(availableSize);
            this.NeverMeasuredWithRenderTree = false;
            return size;
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            if (this.ActualEnableDPICorrection)
            {
                this.EnsureSlave();
                this.slave.ChildDesiredSizeChanged(child);
            }
            base.OnChildDesiredSizeChanged(child);
        }

        protected virtual void OnEnableDPICorrectionChanged()
        {
            this.SetBypassLayoutPolicies(this.ActualEnableDPICorrection);
            if (this.ActualEnableDPICorrection)
            {
                this.EnsureSlave();
            }
            base.InvalidateMeasure();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (!this.ActualEnableDPICorrection)
            {
                base.OnRender(dc);
            }
            else
            {
                this.EnsureSlave();
                this.slave.OnRender(dc);
            }
        }

        protected virtual void OnTreeWalkerChanged(ThemeTreeWalker oldValue, ThemeTreeWalker newValue)
        {
            ChromeSlave.CoerceValue(this, FrameworkRenderElementContext.IsTouchEnabledPropertyKey);
        }

        private static void OnTreeWalkerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DXBorder) d).OnTreeWalkerChanged((ThemeTreeWalker) e.OldValue, (ThemeTreeWalker) e.NewValue);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (!this.ActualEnableDPICorrection)
            {
                base.OnVisualChildrenChanged(visualAdded, visualRemoved);
            }
            else
            {
                this.EnsureSlave();
                this.slave.VisualChildrenChanged(visualAdded, visualRemoved);
            }
        }

        private void ReleaseContext(FrameworkRenderElementContext context)
        {
            this.slave.ReleaseContext(context);
        }

        private static void UpdateContextPropertyValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DXBorder border = (DXBorder) d;
            if (border.Root != null)
            {
                border.Root.SetValue(e.Property.Name, e.NewValue);
            }
        }

        private void UseLayoutRoundingChanged(bool value)
        {
            this.useLayoutRoundingCache = value;
            this.NeverMeasuredWithRenderTree = true;
            this.OnEnableDPICorrectionChanged();
            ChromeSlave.CoerceValue(this, FrameworkRenderElementContext.UseLayoutRoundingPropertyKey);
        }

        private static void UseLayoutRoundingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DXBorder) d).UseLayoutRoundingChanged((bool) e.NewValue);
        }

        internal bool NeverMeasuredWithRenderTree { get; private set; }

        public bool? EnableDPICorrection
        {
            get => 
                this.enableDpiCorrection;
            set
            {
                bool? enableDpiCorrection = this.enableDpiCorrection;
                bool? nullable2 = value;
                if (!((enableDpiCorrection.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((enableDpiCorrection != null) == (nullable2 != null)) : false))
                {
                    this.enableDpiCorrection = value;
                    this.OnEnableDPICorrectionChanged();
                }
            }
        }

        private bool ActualEnableDPICorrection
        {
            get
            {
                if (!this.useLayoutRoundingCache)
                {
                    return false;
                }
                bool? enableDpiCorrection = this.enableDpiCorrection;
                return ((enableDpiCorrection != null) ? enableDpiCorrection.GetValueOrDefault() : CompatibilitySettings.EnableDPICorrection);
            }
        }

        private FrameworkRenderElementContext Root =>
            this.slave?.Root;

        FrameworkElement IElementOwner.Child =>
            (FrameworkElement) this.Child;

        FrameworkRenderElementContext IChrome.Root =>
            this.Root;

        double IChrome.DpiScale =>
            ScreenHelper.GetScaleX(this);

        bool IChrome.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXBorder.<>c <>9 = new DXBorder.<>c();

            internal void <.cctor>b__1_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ChromeSlave.CoerceValue((IChrome) o, FrameworkRenderElementContext.FlowDirectionPropertyKey);
            }
        }
    }
}

