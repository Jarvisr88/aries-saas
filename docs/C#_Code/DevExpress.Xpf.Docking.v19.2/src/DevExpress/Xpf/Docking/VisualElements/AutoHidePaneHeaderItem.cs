namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TemplatePart(Name="PART_ControlBox", Type=typeof(BaseControlBoxControl)), TemplatePart(Name="PART_CaptionControl", Type=typeof(CaptionControl))]
    public class AutoHidePaneHeaderItem : psvSelectorItem, IUIElement, ITabHeader
    {
        public static readonly DependencyProperty LocationProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ItemIsVisibleProperty;
        public static readonly DependencyProperty ActualBorderThicknessProperty;
        private static readonly DependencyPropertyKey ActualBorderThicknessPropertyKey;

        static AutoHidePaneHeaderItem()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHidePaneHeaderItem> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<AutoHidePaneHeaderItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Dock>("Location", ref LocationProperty, Dock.Left, (dObj, ea) => ((AutoHidePaneHeaderItem) dObj).OnLocationChanged((Dock) ea.NewValue), null);
            registrator.Register<bool>("ItemIsVisible", ref ItemIsVisibleProperty, true, (dObj, ea) => ((AutoHidePaneHeaderItem) dObj).OnItemIsVisibleChanged((bool) ea.NewValue), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(AutoHidePaneHeaderItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            Thickness defaultValue = new Thickness();
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<AutoHidePaneHeaderItem>.New().OverrideMetadata<Thickness>(Control.BorderThicknessProperty, (d, oldValue, newValue) => d.OnBorderThicknessChanged(oldValue, newValue)).RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<AutoHidePaneHeaderItem, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(AutoHidePaneHeaderItem.get_ActualBorderThickness)), parameters), out ActualBorderThicknessPropertyKey, out ActualBorderThicknessProperty, defaultValue, (Func<AutoHidePaneHeaderItem, Thickness, Thickness>) ((d, value) => d.OnCoerceActualBorderThickness(value)), frameworkOptions);
        }

        public AutoHidePaneHeaderItem(AutoHideTrayHeadersGroup headersGroup)
        {
            this.HeadersGroup = headersGroup;
        }

        protected override void AfterArrange()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        public void Apply(ITabHeaderInfo info)
        {
            this.EnsureItemCaptionElementsVisibility();
            this.Visibility = VisibilityHelper.Convert(this.ItemIsVisible && info.IsVisible, Visibility.Collapsed);
            if (this.PartCaptionControl != null)
            {
                base.Measure(info.Rect.GetSize());
            }
            this.ArrangeRect = info.Rect;
        }

        public ITabHeaderInfo CreateInfo(Size size)
        {
            this.EnsureItemCaptionElementsVisibility();
            base.Visibility = VisibilityHelper.Convert(this.ItemIsVisible, Visibility.Collapsed);
            base.Measure(size);
            return new BaseHeaderInfo(this, this.PartCaptionControl, this.PartControlBox, base.IsSelected);
        }

        private void EnsureItemCaptionElementsVisibility()
        {
            if (base.LayoutItem != null)
            {
                base.LayoutItem.CoerceValue(BaseLayoutItem.IsCaptionVisibleProperty);
                base.LayoutItem.CoerceValue(BaseLayoutItem.IsCaptionImageVisibleProperty);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartControlBox = base.GetTemplateChild("PART_ControlBox") as BaseControlBoxControl;
            this.PartCaptionControlPresenter = base.GetTemplateChild("PART_CaptionControlPresenter") as TemplatedCaptionControl;
            this.UpdateVisualState();
        }

        protected virtual void OnBorderThicknessChanged(object oldValue, object newValue)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        protected virtual Thickness OnCoerceActualBorderThickness(Thickness value)
        {
            bool flag;
            bool flag2;
            base.GetItemAlignment<AutoHideTray>(this.Location.ToOrthogonalOrientation() == Orientation.Horizontal, out flag, out flag2);
            Thickness borderThickness = base.BorderThickness;
            if (base.ViewStyle == DockingViewStyle.Light)
            {
                if (flag)
                {
                    if ((this.Location != Dock.Bottom) && (this.Location != Dock.Right))
                    {
                        borderThickness.Left = 0.0;
                    }
                    else
                    {
                        borderThickness.Right = 0.0;
                    }
                }
                if (flag2)
                {
                    if ((this.Location != Dock.Bottom) && (this.Location != Dock.Right))
                    {
                        borderThickness.Right = 0.0;
                    }
                    else
                    {
                        borderThickness.Left = 0.0;
                    }
                }
                borderThickness.Bottom = 0.0;
            }
            return this.Location.RotateThickness(borderThickness);
        }

        protected override void OnIsSelectedChanged(bool selected)
        {
            base.OnIsSelectedChanged(selected);
            this.UpdateVisualState();
        }

        protected void OnItemIsVisibleChanged(bool newValue)
        {
            this.Recalc();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        protected virtual void OnLocationChanged(Dock location)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
            this.UpdateVisualState();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.UpdateVisualState();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.UpdateVisualState();
        }

        protected override void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        private void Recalc()
        {
            BaseHeadersPanel.Invalidate(this);
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            BindingHelper.SetBinding(this, ItemIsVisibleProperty, base.LayoutItem, "IsVisibleCore");
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.ClearValue(ItemIsVisibleProperty);
            base.Unsubscribe(item);
        }

        private void UpdateVisualState()
        {
            if (base.IsSelected)
            {
                VisualStateManager.GoToState(this, "Selected", false);
            }
            else if (base.IsMouseOver)
            {
                VisualStateManager.GoToState(this, "MouseOver", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", false);
            }
            switch (this.Location)
            {
                case Dock.Top:
                    VisualStateManager.GoToState(this, "Top", false);
                    return;

                case Dock.Right:
                    VisualStateManager.GoToState(this, "Right", false);
                    return;

                case Dock.Bottom:
                    VisualStateManager.GoToState(this, "Bottom", false);
                    return;
            }
            VisualStateManager.GoToState(this, "Left", false);
        }

        public Thickness ActualBorderThickness =>
            (Thickness) base.GetValue(ActualBorderThicknessProperty);

        public AutoHideTrayHeadersGroup HeadersGroup { get; private set; }

        public bool ItemIsVisible =>
            (bool) base.GetValue(ItemIsVisibleProperty);

        public Dock Location
        {
            get => 
                (Dock) base.GetValue(LocationProperty);
            set => 
                base.SetValue(LocationProperty, value);
        }

        public TemplatedCaptionControl PartCaptionControlPresenter { get; private set; }

        public BaseControlBoxControl PartControlBox { get; private set; }

        public TabHeaderPinLocation PinLocation =>
            TabHeaderPinLocation.Default;

        public bool IsPinned =>
            false;

        public Rect ArrangeRect { get; private set; }

        public CaptionControl PartCaptionControl =>
            this.PartCaptionControlPresenter?.PartCaption;

        public int ScrollIndex { get; private set; }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHidePaneHeaderItem.<>c <>9 = new AutoHidePaneHeaderItem.<>c();

            internal void <.cctor>b__4_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AutoHidePaneHeaderItem) dObj).OnLocationChanged((Dock) ea.NewValue);
            }

            internal void <.cctor>b__4_1(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((AutoHidePaneHeaderItem) dObj).OnItemIsVisibleChanged((bool) ea.NewValue);
            }

            internal void <.cctor>b__4_2(AutoHidePaneHeaderItem d, Thickness oldValue, Thickness newValue)
            {
                d.OnBorderThicknessChanged(oldValue, newValue);
            }

            internal Thickness <.cctor>b__4_3(AutoHidePaneHeaderItem d, Thickness value) => 
                d.OnCoerceActualBorderThickness(value);
        }
    }
}

