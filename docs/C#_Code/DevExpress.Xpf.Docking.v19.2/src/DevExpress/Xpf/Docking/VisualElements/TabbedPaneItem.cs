namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.UIAutomation;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;

    [TemplatePart(Name="PART_ControlBox", Type=typeof(BaseControlBoxControl)), TemplatePart(Name="PART_CaptionControl", Type=typeof(CaptionControl)), DXToolboxBrowsable(false)]
    public class TabbedPaneItem : psvSelectorItem, IUIElement, ITabHeader
    {
        public static readonly DependencyProperty CaptionOrientationProperty;
        public static readonly DependencyProperty CaptionLocationProperty;
        public static readonly DependencyProperty PinnedProperty;
        public static readonly DependencyProperty PinLocationProperty;
        public static readonly DependencyProperty DragOffsetProperty;
        public static readonly DependencyProperty ActualBorderThicknessProperty;
        private static readonly DependencyPropertyKey ActualBorderThicknessPropertyKey;
        private Thickness controlBoxThemeMargin;

        static TabbedPaneItem()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TabbedPaneItem> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TabbedPaneItem>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<Orientation>("CaptionOrientation", ref CaptionOrientationProperty, Orientation.Horizontal, null, null);
            registrator.Register<DevExpress.Xpf.Docking.CaptionLocation>("CaptionLocation", ref CaptionLocationProperty, DevExpress.Xpf.Docking.CaptionLocation.Default, (dObj, ea) => ((TabbedPaneItem) dObj).OnCaptionLocationChanged((DevExpress.Xpf.Docking.CaptionLocation) ea.NewValue), null);
            registrator.Register<bool>("Pinned", ref PinnedProperty, false, null, null);
            registrator.Register<TabHeaderPinLocation>("PinLocation", ref PinLocationProperty, TabHeaderPinLocation.Default, null, null);
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("DragOffset", ref DragOffsetProperty, defValue, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TabbedPaneItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TabbedPaneItem>.New().OverrideMetadata<Thickness>(Control.BorderThicknessProperty, (d, oldValue, newValue) => d.OnBorderThicknessChanged(oldValue, newValue)).RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<TabbedPaneItem, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TabbedPaneItem.get_ActualBorderThickness)), parameters), out ActualBorderThicknessPropertyKey, out ActualBorderThicknessProperty, new Thickness(1.0, 1.0, 1.0, 0.0), (Func<TabbedPaneItem, Thickness, Thickness>) ((d, value) => d.OnCoerceActualBorderThickness(value)), frameworkOptions);
        }

        public TabbedPaneItem()
        {
            this.ScrollIndex = -1;
        }

        protected override void AfterArrange()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        public void Apply(ITabHeaderInfo info)
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(this);
            layoutItem.CoerceValue(BaseLayoutItem.IsCaptionVisibleProperty);
            layoutItem.CoerceValue(BaseLayoutItem.IsCaptionImageVisibleProperty);
            this.Visibility = info.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            this.RemeasureIfNeeded(info);
            Panel.SetZIndex(this, info.ZIndex);
            this.ArrangeRect = info.Rect;
            this.ScrollIndex = info.ScrollIndex;
            this.IsLastRow = (info.MultiLineResult == null) || (info.MultiLineResult.RowIndex == 0);
        }

        protected internal bool AutomationClick()
        {
            if ((base.LayoutItem == null) || (base.Container == null))
            {
                return false;
            }
            base.Container.DockController.Activate(base.LayoutItem);
            return ReferenceEquals(base.LayoutItem.Parent.SelectedItem, base.LayoutItem);
        }

        public ITabHeaderInfo CreateInfo(Size size)
        {
            base.Visibility = Visibility.Visible;
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(this);
            layoutItem.CoerceValue(BaseLayoutItem.IsCaptionVisibleProperty);
            layoutItem.CoerceValue(BaseLayoutItem.IsCaptionImageVisibleProperty);
            this.UpdateControlBoxMargins(layoutItem);
            base.Measure(size);
            FrameworkElement partCaption = this.PartCaption;
            return new BaseHeaderInfo(this, this.PartCaptionControlPresenter, this.CaptionOrientation == Orientation.Horizontal, this.PartControlBox, base.IsSelected, this.PinLocation, this.Pinned);
        }

        protected virtual bool IsControlBoxActuallyVisible(BaseLayoutItem item) => 
            item.IsCloseButtonVisible || (item.ControlBoxContent != null);

        public override void OnApplyTemplate()
        {
            Thickness margin;
            base.OnApplyTemplate();
            this.PartControlBox = base.GetTemplateChild("PART_ControlBox") as BaseControlBoxControl;
            this.PartCaptionControlPresenter = base.GetTemplateChild("PART_CaptionControlPresenter") as TemplatedCaptionControl;
            BaseControlBoxControl partControlBox = this.PartControlBox;
            if (partControlBox != null)
            {
                margin = partControlBox.Margin;
            }
            else
            {
                BaseControlBoxControl local1 = partControlBox;
                margin = new Thickness(0.0);
            }
            this.controlBoxThemeMargin = margin;
            this.UpdateVisualState();
        }

        protected virtual void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        protected virtual void OnCaptionLocationChanged(DevExpress.Xpf.Docking.CaptionLocation captionLocation)
        {
            this.UpdateVisualState();
            base.CoerceValue(ActualBorderThicknessProperty);
        }

        protected virtual Thickness OnCoerceActualBorderThickness(Thickness value)
        {
            bool flag;
            bool flag2;
            DevExpress.Xpf.Docking.CaptionLocation captionLocation = (this.CaptionLocation == DevExpress.Xpf.Docking.CaptionLocation.Default) ? this.DefaultCaptionLocation : this.CaptionLocation;
            base.GetItemAlignment<LayoutTabControl>(captionLocation.ToDock(Dock.Top).ToOrthogonalOrientation() == Orientation.Horizontal, out flag, out flag2);
            Thickness borderThickness = base.BorderThickness;
            if (base.ViewStyle == DockingViewStyle.Light)
            {
                if (flag)
                {
                    if ((captionLocation != DevExpress.Xpf.Docking.CaptionLocation.Bottom) && (captionLocation != DevExpress.Xpf.Docking.CaptionLocation.Right))
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
                    if ((captionLocation != DevExpress.Xpf.Docking.CaptionLocation.Bottom) && (captionLocation != DevExpress.Xpf.Docking.CaptionLocation.Right))
                    {
                        borderThickness.Right = 0.0;
                    }
                    else
                    {
                        borderThickness.Left = 0.0;
                    }
                }
                if (this.IsLastRow)
                {
                    borderThickness.Top = 0.0;
                }
            }
            return (this.TransformBorderThickness ? captionLocation.RotateThickness(borderThickness) : borderThickness);
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new TabbedPaneItemAutomationPeer(this);

        protected override void OnDispose()
        {
            if (this.PartCaptionControlPresenter != null)
            {
                this.PartCaptionControlPresenter.Dispose();
                this.PartCaptionControlPresenter = null;
            }
            if (this.PartControlBox != null)
            {
                this.PartControlBox.Dispose();
                this.PartControlBox = null;
            }
            base.OnDispose();
        }

        protected override void OnIsSelectedChanged(bool selected)
        {
            base.OnIsSelectedChanged(selected);
            this.UpdateVisualState();
        }

        protected virtual void OnItemVisualChanged(object sender, EventArgs e)
        {
            this.UpdateVisualState();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
            base.CoerceValue(ActualBorderThicknessProperty);
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

        private void RemeasureIfNeeded(ITabHeaderInfo info)
        {
            Size availableSize = info.Rect.Size;
            Size desiredSize = info.DesiredSize;
            double num = (this.CaptionOrientation == Orientation.Horizontal) ? availableSize.Width : availableSize.Height;
            if ((num < ((this.CaptionOrientation == Orientation.Horizontal) ? desiredSize.Width : desiredSize.Height)) || (num == 0.0))
            {
                base.Measure(availableSize);
            }
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            if (item != null)
            {
                item.VisualChanged += new EventHandler(this.OnItemVisualChanged);
                if (item is LayoutPanel)
                {
                    BindingHelper.SetBinding(this, PinLocationProperty, item, "TabPinLocation");
                    BindingHelper.SetBinding(this, PinnedProperty, item, "Pinned");
                }
            }
        }

        protected override void Unsubscribe(BaseLayoutItem item)
        {
            base.Unsubscribe(item);
            if (item != null)
            {
                item.VisualChanged -= new EventHandler(this.OnItemVisualChanged);
                if (item is LayoutPanel)
                {
                    BindingHelper.ClearBinding(this, PinLocationProperty);
                    BindingHelper.ClearBinding(this, PinnedProperty);
                }
            }
        }

        private void UpdateCommonState()
        {
            if (base.IsMouseOver)
            {
                VisualStateManager.GoToState(this, "MouseOver", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", false);
            }
        }

        private void UpdateControlBoxMargins(BaseLayoutItem item)
        {
            if (this.PartControlBox != null)
            {
                if (this.IsControlBoxActuallyVisible(item))
                {
                    this.PartControlBox.Margin = this.controlBoxThemeMargin;
                }
                else
                {
                    this.PartControlBox.Margin = new Thickness(0.0);
                }
            }
        }

        private void UpdateLocationState()
        {
            switch (this.CaptionLocation)
            {
                case DevExpress.Xpf.Docking.CaptionLocation.Left:
                    VisualStateManager.GoToState(this, "Left", false);
                    return;

                case DevExpress.Xpf.Docking.CaptionLocation.Right:
                    VisualStateManager.GoToState(this, "Right", false);
                    return;

                case DevExpress.Xpf.Docking.CaptionLocation.Bottom:
                    VisualStateManager.GoToState(this, "Bottom", false);
                    return;
            }
            VisualStateManager.GoToState(this, "Top", false);
        }

        protected virtual void UpdateSelectionState()
        {
            VisualStateManager.GoToState(this, "EmptySelectionState", false);
            if (base.IsSelected)
            {
                VisualStateManager.GoToState(this, "Selected", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unselected", false);
            }
        }

        protected virtual void UpdateVisualState()
        {
            this.UpdateSelectionState();
            if (!base.IsSelected)
            {
                this.UpdateCommonState();
            }
            this.UpdateLocationState();
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children =>
            null;

        public Thickness ActualBorderThickness =>
            (Thickness) base.GetValue(ActualBorderThicknessProperty);

        public Orientation CaptionOrientation
        {
            get => 
                (Orientation) base.GetValue(CaptionOrientationProperty);
            set => 
                base.SetValue(CaptionOrientationProperty, value);
        }

        public DevExpress.Xpf.Docking.CaptionLocation CaptionLocation
        {
            get => 
                (DevExpress.Xpf.Docking.CaptionLocation) base.GetValue(CaptionLocationProperty);
            set => 
                base.SetValue(CaptionLocationProperty, value);
        }

        public Thickness DragOffset
        {
            get => 
                (Thickness) base.GetValue(DragOffsetProperty);
            set => 
                base.SetValue(DragOffsetProperty, value);
        }

        public bool Pinned
        {
            get => 
                (bool) base.GetValue(PinnedProperty);
            set => 
                base.SetValue(PinnedProperty, value);
        }

        public TabHeaderPinLocation PinLocation
        {
            get => 
                (TabHeaderPinLocation) base.GetValue(PinLocationProperty);
            set => 
                base.SetValue(PinLocationProperty, value);
        }

        public BaseControlBoxControl PartControlBox { get; private set; }

        public TemplatedCaptionControl PartCaptionControlPresenter { get; private set; }

        protected virtual DevExpress.Xpf.Docking.CaptionLocation DefaultCaptionLocation =>
            DevExpress.Xpf.Docking.CaptionLocation.Bottom;

        protected virtual bool TransformBorderThickness =>
            true;

        private bool IsLastRow { get; set; }

        public bool IsPinned =>
            this.Pinned;

        public Rect ArrangeRect { get; private set; }

        public int ScrollIndex { get; private set; }

        public CaptionControl PartCaption =>
            this.PartCaptionControlPresenter?.PartCaption;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabbedPaneItem.<>c <>9 = new TabbedPaneItem.<>c();

            internal void <.cctor>b__7_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TabbedPaneItem) dObj).OnCaptionLocationChanged((CaptionLocation) ea.NewValue);
            }

            internal void <.cctor>b__7_1(TabbedPaneItem d, Thickness oldValue, Thickness newValue)
            {
                d.OnBorderThicknessChanged(oldValue, newValue);
            }

            internal Thickness <.cctor>b__7_2(TabbedPaneItem d, Thickness value) => 
                d.OnCoerceActualBorderThickness(value);
        }
    }
}

