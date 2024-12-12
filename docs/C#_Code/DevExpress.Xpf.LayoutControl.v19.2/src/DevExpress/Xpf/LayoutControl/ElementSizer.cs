namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    [TemplatePart(Name="HorizontalRootElement", Type=typeof(FrameworkElement)), TemplatePart(Name="VerticalRootElement", Type=typeof(FrameworkElement))]
    public class ElementSizer : ControlBase, IElementSizer, IControl
    {
        public static int SizingStep = 5;
        public static readonly DependencyProperty CollapseOnDoubleClickProperty = DependencyProperty.Register("CollapseOnDoubleClick", typeof(bool), typeof(ElementSizer), new PropertyMetadata(true));
        public static readonly DependencyProperty ElementProperty = DependencyProperty.Register("Element", typeof(FrameworkElement), typeof(ElementSizer), null);
        public static readonly DependencyProperty UseSizingStepProperty = DependencyProperty.Register("UseSizingStep", typeof(bool), typeof(ElementSizer), null);
        public static readonly DependencyProperty ShowResizingHandleProperty = DependencyProperty.Register("ShowResizingHandle", typeof(bool), typeof(ElementSizer), new PropertyMetadata(true));
        private DevExpress.Xpf.Core.Side _Side;
        private double _SizingAreaWidth;
        private const string HorizontalRootElementName = "HorizontalRootElement";
        private const string VerticalRootElementName = "VerticalRootElement";

        public event EventHandler ElementSizeChanging
        {
            add
            {
                this.Controller.ElementSizeChanging += value;
            }
            remove
            {
                this.Controller.ElementSizeChanging -= value;
            }
        }

        public event EventHandler IsSizingChanged
        {
            add
            {
                this.Controller.IsSizingChanged += value;
            }
            remove
            {
                this.Controller.IsSizingChanged -= value;
            }
        }

        public ElementSizer()
        {
            base.DefaultStyleKey = typeof(ElementSizer);
        }

        protected virtual Rect CalculateBounds()
        {
            if (this.Element == null)
            {
                return base.Bounds;
            }
            Rect layoutSlot = LayoutInformation.GetLayoutSlot(this.Element);
            switch (this.Side)
            {
                case DevExpress.Xpf.Core.Side.Left:
                    layoutSlot.X = layoutSlot.Left - this.SizingAreaWidth;
                    break;

                case DevExpress.Xpf.Core.Side.Top:
                    layoutSlot.Y = layoutSlot.Top - this.SizingAreaWidth;
                    break;

                case DevExpress.Xpf.Core.Side.LeftRight:
                    layoutSlot.X = layoutSlot.Right;
                    break;

                case DevExpress.Xpf.Core.Side.Bottom:
                    layoutSlot.Y = layoutSlot.Bottom;
                    break;

                default:
                    break;
            }
            if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                layoutSlot.Height = this.SizingAreaWidth;
            }
            else
            {
                layoutSlot.Width = this.SizingAreaWidth;
            }
            return layoutSlot;
        }

        protected override ControlControllerBase CreateController() => 
            new ElementSizerController(this);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.HorizontalRootElement = base.GetTemplateChild("HorizontalRootElement") as FrameworkElement;
            this.VerticalRootElement = base.GetTemplateChild("VerticalRootElement") as FrameworkElement;
            this.UpdateTemplate();
        }

        public void UpdateBounds()
        {
            if (this.VisualChildrenCount > 0)
            {
                Action<FrameworkElement> action = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Action<FrameworkElement> local1 = <>c.<>9__8_0;
                    action = <>c.<>9__8_0 = x => x.InvalidateMeasure();
                }
                (this.GetVisualChild(0) as FrameworkElement).Do<FrameworkElement>(action);
            }
            Container local2 = LayoutTreeHelper.GetVisualChildren(this).OfType<Container>().FirstOrDefault<Container>();
            if (local2 == null)
            {
                Container local3 = local2;
            }
            else
            {
                local2.InvalidateMeasure();
            }
            base.Measure(SizeHelper.Infinite);
            base.Arrange(this.CalculateBounds());
        }

        protected virtual void UpdateTemplate()
        {
            if (this.HorizontalRootElement != null)
            {
                this.HorizontalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Horizontal);
                if (this.HorizontalRootElement.GetVisible())
                {
                    base.Cursor = this.HorizontalRootElement.Cursor;
                }
            }
            if (this.VerticalRootElement != null)
            {
                this.VerticalRootElement.SetVisible(this.Orientation == System.Windows.Controls.Orientation.Vertical);
                if (this.VerticalRootElement.GetVisible())
                {
                    base.Cursor = this.VerticalRootElement.Cursor;
                }
            }
            base.InvalidateMeasure();
        }

        public bool CollapseOnDoubleClick
        {
            get => 
                (bool) base.GetValue(CollapseOnDoubleClickProperty);
            set => 
                base.SetValue(CollapseOnDoubleClickProperty, value);
        }

        public ElementSizerController Controller =>
            (ElementSizerController) base.Controller;

        public FrameworkElement Element
        {
            get => 
                (FrameworkElement) base.GetValue(ElementProperty);
            set => 
                base.SetValue(ElementProperty, value);
        }

        public double ElementSize =>
            this.Controller.ElementSize;

        public bool IsSizing =>
            this.Controller.IsSizing;

        public bool ShowResizingHandle
        {
            get => 
                (bool) base.GetValue(ShowResizingHandleProperty);
            set => 
                base.SetValue(ShowResizingHandleProperty, value);
        }

        public DevExpress.Xpf.Core.Side Side
        {
            get => 
                this._Side;
            set
            {
                if (this._Side != value)
                {
                    this._Side = value;
                    this.UpdateTemplate();
                }
            }
        }

        public double SizingAreaWidth
        {
            get => 
                this._SizingAreaWidth;
            set => 
                this._SizingAreaWidth = Math.Max(0.0, value);
        }

        public bool UseSizingStep
        {
            get => 
                (bool) base.GetValue(UseSizingStepProperty);
            set => 
                base.SetValue(UseSizingStepProperty, value);
        }

        protected FrameworkElement HorizontalRootElement { get; set; }

        protected FrameworkElement VerticalRootElement { get; set; }

        protected System.Windows.Controls.Orientation Orientation =>
            this.Side.GetOrientation();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ElementSizer.<>c <>9 = new ElementSizer.<>c();
            public static Action<FrameworkElement> <>9__8_0;

            internal void <UpdateBounds>b__8_0(FrameworkElement x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

