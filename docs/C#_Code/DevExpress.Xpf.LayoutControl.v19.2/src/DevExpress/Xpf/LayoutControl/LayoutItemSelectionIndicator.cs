namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    [TemplatePart(Name="LeftSizingElement", Type=typeof(ElementSizer)), TemplatePart(Name="RightSizingElement", Type=typeof(ElementSizer)), TemplatePart(Name="TopSizingElement", Type=typeof(ElementSizer)), TemplatePart(Name="BottomSizingElement", Type=typeof(ElementSizer))]
    public class LayoutItemSelectionIndicator : ControlBase
    {
        public static readonly DependencyProperty AllowSizingProperty = DependencyProperty.Register("AllowSizing", typeof(bool), typeof(LayoutItemSelectionIndicator), null);
        public static readonly DependencyProperty ElementProperty;
        public static readonly DependencyProperty ElementSizeProperty;
        public static readonly DependencyProperty IsSizingProperty;
        public static readonly DependencyProperty IsHorizontalSizingProperty;
        public static readonly DependencyProperty IsVerticalSizingProperty;
        private ILayoutControl _LayoutControl;
        private const string LeftSizingElementName = "LeftSizingElement";
        private const string RightSizingElementName = "RightSizingElement";
        private const string TopSizingElementName = "TopSizingElement";
        private const string BottomSizingElementName = "BottomSizingElement";

        static LayoutItemSelectionIndicator()
        {
            ElementProperty = DependencyProperty.Register("Element", typeof(FrameworkElement), typeof(LayoutItemSelectionIndicator), new PropertyMetadata((o, e) => ((LayoutItemSelectionIndicator) o).OnElementChanged()));
            ElementSizeProperty = DependencyProperty.Register("ElementSize", typeof(object), typeof(LayoutItemSelectionIndicator), null);
            IsSizingProperty = DependencyProperty.Register("IsSizing", typeof(bool), typeof(LayoutItemSelectionIndicator), null);
            IsHorizontalSizingProperty = DependencyProperty.Register("IsHorizontalSizing", typeof(bool), typeof(LayoutItemSelectionIndicator), null);
            IsVerticalSizingProperty = DependencyProperty.Register("IsVerticalSizing", typeof(bool), typeof(LayoutItemSelectionIndicator), null);
        }

        public LayoutItemSelectionIndicator()
        {
            base.DefaultStyleKey = typeof(LayoutItemSelectionIndicator);
        }

        private void AttachEventHandlers(ElementSizer sizer)
        {
            if (sizer != null)
            {
                sizer.ElementSizeChanging += new EventHandler(this.OnElementSizeChanging);
                sizer.IsSizingChanged += new EventHandler(this.OnIsSizingChanged);
            }
        }

        protected virtual Rect CalculateBounds() => 
            !ReferenceEquals(this.Element, this.LayoutControl) ? this.Element.GetVisualBounds(((FrameworkElement) base.Parent), false) : this.Element.MapRect(((ILayoutControl) this.Element).ContentBounds, ((FrameworkElement) base.Parent));

        private void DetachEventHandlers(ElementSizer sizer)
        {
            if (sizer != null)
            {
                sizer.ElementSizeChanging -= new EventHandler(this.OnElementSizeChanging);
                sizer.IsSizingChanged -= new EventHandler(this.OnIsSizingChanged);
            }
        }

        public override void OnApplyTemplate()
        {
            this.DetachEventHandlers(this.LeftSizingElement);
            this.DetachEventHandlers(this.RightSizingElement);
            this.DetachEventHandlers(this.TopSizingElement);
            this.DetachEventHandlers(this.BottomSizingElement);
            base.OnApplyTemplate();
            this.LeftSizingElement = base.GetTemplateChild("LeftSizingElement") as ElementSizer;
            this.RightSizingElement = base.GetTemplateChild("RightSizingElement") as ElementSizer;
            this.TopSizingElement = base.GetTemplateChild("TopSizingElement") as ElementSizer;
            this.BottomSizingElement = base.GetTemplateChild("BottomSizingElement") as ElementSizer;
            this.AttachEventHandlers(this.LeftSizingElement);
            this.AttachEventHandlers(this.RightSizingElement);
            this.AttachEventHandlers(this.TopSizingElement);
            this.AttachEventHandlers(this.BottomSizingElement);
        }

        protected virtual void OnElementChanged()
        {
            this.SetAllowSizing();
            this.UpdateBounds();
        }

        protected virtual void OnElementSizeChanging(double elementSize)
        {
            this.ElementSize = elementSize;
        }

        private void OnElementSizeChanging(object sender, EventArgs e)
        {
            this.OnElementSizeChanging(((ElementSizer) sender).ElementSize);
        }

        private void OnIsSizingChanged(object sender, EventArgs e)
        {
            ElementSizer sizer = (ElementSizer) sender;
            this.OnIsSizingChanged(sizer.IsSizing, sizer.Side, sizer.ElementSize);
        }

        protected virtual void OnIsSizingChanged(bool isSizing, DevExpress.Xpf.Core.Side side, double elementSize)
        {
            this.IsSizing = isSizing;
            this.IsHorizontalSizing = isSizing && ((side == DevExpress.Xpf.Core.Side.Left) || (side == DevExpress.Xpf.Core.Side.LeftRight));
            this.IsVerticalSizing = isSizing && ((side == DevExpress.Xpf.Core.Side.Top) || (side == DevExpress.Xpf.Core.Side.Bottom));
            this.ElementSize = elementSize;
        }

        protected virtual void SetAllowSizing()
        {
            if ((this.LayoutControl == null) || ReferenceEquals(this.Element, this.LayoutControl))
            {
                base.ClearValue(AllowSizingProperty);
            }
            else
            {
                Binding binding = new Binding("AllowItemSizingDuringCustomization");
                binding.Source = this.LayoutControl;
                base.SetBinding(AllowSizingProperty, binding);
            }
        }

        public void UpdateBounds()
        {
            if ((this.Element != null) && (base.Parent != null))
            {
                this.SetBounds(this.CalculateBounds());
            }
        }

        public bool AllowSizing =>
            (bool) base.GetValue(AllowSizingProperty);

        public FrameworkElement Element
        {
            get => 
                (FrameworkElement) base.GetValue(ElementProperty);
            set => 
                base.SetValue(ElementProperty, value);
        }

        public double ElementSize
        {
            get => 
                (double) base.GetValue(ElementSizeProperty);
            protected set => 
                base.SetValue(ElementSizeProperty, value);
        }

        public bool IsSizing
        {
            get => 
                (bool) base.GetValue(IsSizingProperty);
            protected set => 
                base.SetValue(IsSizingProperty, value);
        }

        public bool IsHorizontalSizing
        {
            get => 
                (bool) base.GetValue(IsHorizontalSizingProperty);
            protected set => 
                base.SetValue(IsHorizontalSizingProperty, value);
        }

        public bool IsVerticalSizing
        {
            get => 
                (bool) base.GetValue(IsVerticalSizingProperty);
            protected set => 
                base.SetValue(IsVerticalSizingProperty, value);
        }

        public ILayoutControl LayoutControl
        {
            get => 
                this._LayoutControl;
            internal set
            {
                if (!ReferenceEquals(this.LayoutControl, value))
                {
                    this._LayoutControl = value;
                    this.SetAllowSizing();
                }
            }
        }

        protected ElementSizer LeftSizingElement { get; private set; }

        protected ElementSizer RightSizingElement { get; private set; }

        protected ElementSizer TopSizingElement { get; private set; }

        protected ElementSizer BottomSizingElement { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutItemSelectionIndicator.<>c <>9 = new LayoutItemSelectionIndicator.<>c();

            internal void <.cctor>b__59_0(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutItemSelectionIndicator) o).OnElementChanged();
            }
        }
    }
}

