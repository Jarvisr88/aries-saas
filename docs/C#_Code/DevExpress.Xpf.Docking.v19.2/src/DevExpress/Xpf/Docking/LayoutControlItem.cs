namespace DevExpress.Xpf.Docking
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class LayoutControlItem : ContentItem, IUIElement, ILayoutContent
    {
        public static readonly DependencyProperty CaptionToControlDistanceProperty;
        public static readonly DependencyProperty ControlProperty;
        private static readonly DependencyPropertyKey ControlPropertyKey;
        public static readonly DependencyProperty ShowControlProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty DesiredSizeInternalProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty HasDesiredSizeProperty;
        internal static readonly DependencyPropertyKey HasDesiredSizePropertyKey;
        public static readonly DependencyProperty ActualCaptionMarginProperty;
        private static readonly DependencyPropertyKey ActualCaptionMarginPropertyKey;
        public static readonly DependencyProperty HasControlProperty;
        internal static readonly DependencyPropertyKey HasControlPropertyKey;
        public static readonly DependencyProperty ControlHorizontalAlignmentProperty;
        public static readonly DependencyProperty ControlVerticalAlignmentProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty HasVisibleCaptionProperty;
        public static readonly DependencyProperty ContentPresenterProperty;
        private static readonly DependencyPropertyKey ContentPresenterPropertyKey;
        private Size desiredSizeCore;
        private System.Windows.Controls.ContentPresenter Presenter;

        static LayoutControlItem()
        {
            DependencyPropertyRegistrator<LayoutControlItem> registrator = new DependencyPropertyRegistrator<LayoutControlItem>();
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowFloatProperty, false, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowDockProperty, false, null, null);
            registrator.OverrideMetadata<bool>(BaseLayoutItem.AllowCloseProperty, false, null, null);
            registrator.OverrideMetadata<GridLength>(BaseLayoutItem.ItemHeightProperty, new GridLength(1.0, GridUnitType.Auto), null, null);
            registrator.OverrideMetadata<ActivateOnFocusing>(ContentItem.ActivateOnFocusingProperty, ActivateOnFocusing.Logical, null, null);
            registrator.RegisterReadonly<UIElement>("Control", ref ControlPropertyKey, ref ControlProperty, null, (dObj, e) => ((LayoutControlItem) dObj).OnControlChanged((UIElement) e.NewValue, (UIElement) e.OldValue), null);
            registrator.Register<bool>("ShowControl", ref ShowControlProperty, true, (dObj, e) => ((LayoutControlItem) dObj).OnShowControlChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<bool>("HasControl", ref HasControlPropertyKey, ref HasControlProperty, false, null, null);
            registrator.Register<double>("CaptionToControlDistance", ref CaptionToControlDistanceProperty, double.NaN, (dObj, e) => ((LayoutControlItem) dObj).OnCaptionToControlDistanceChanged((double) e.NewValue), (CoerceValueCallback) ((dObj, value) => ((LayoutControlItem) dObj).CoerceCaptionToControlDistance((double) value)));
            registrator.Register<Size>("DesiredSizeInternal", ref DesiredSizeInternalProperty, Size.Empty, (dObj, e) => ((LayoutControlItem) dObj).OnDesiredSizeChanged((Size) e.NewValue), (dObj, value) => ((LayoutControlItem) dObj).CoerceDesiredSize((Size) value));
            registrator.RegisterReadonly<bool>("HasDesiredSize", ref HasDesiredSizePropertyKey, ref HasDesiredSizeProperty, false, null, null);
            registrator.RegisterReadonly<Thickness>("ActualCaptionMargin", ref ActualCaptionMarginPropertyKey, ref ActualCaptionMarginProperty, new Thickness(double.NaN), null, (CoerceValueCallback) ((dObj, value) => ((LayoutControlItem) dObj).CoerceActualCaptionMargin((Thickness) value)));
            registrator.Register<HorizontalAlignment>("ControlHorizontalAlignment", ref ControlHorizontalAlignmentProperty, HorizontalAlignment.Stretch, null, null);
            registrator.Register<VerticalAlignment>("ControlVerticalAlignment", ref ControlVerticalAlignmentProperty, VerticalAlignment.Stretch, null, null);
            registrator.Register<bool>("HasVisibleCaption", ref HasVisibleCaptionProperty, false, (dObj, e) => ((LayoutControlItem) dObj).OnHasVisibleCaptionChanged((bool) e.NewValue), null);
            registrator.RegisterReadonly<UIElement>("ContentPresenter", ref ContentPresenterPropertyKey, ref ContentPresenterProperty, null, null, null);
        }

        public LayoutControlItem()
        {
            if (!base.IsInDesignTime)
            {
                base.CoerceValue(CaptionToControlDistanceProperty);
            }
            base.CoerceValue(ActualCaptionMarginProperty);
            this.Presenter = this.CreatePresenter();
        }

        protected virtual void ActivateItemCore()
        {
            if (!base.Manager.IsDisposing)
            {
                base.Manager.LayoutController.Activate(this, false);
            }
        }

        protected override Size CalcMinSizeValue(Size value)
        {
            if (!value.IsZero() || (!this.HasDesiredSize || !DesiredSizeHelper.CanUseDesiredSizeAsMinSize(this.Control)))
            {
                return base.CalcMinSizeValue(value);
            }
            Size[] minSizes = new Size[] { this.DesiredSizeInternal, value };
            return MathHelper.MeasureMinSize(minSizes);
        }

        protected virtual void CheckContent(object content)
        {
            if (content is UIElement)
            {
                base.SetValue(ControlPropertyKey, content);
            }
            else
            {
                base.SetValue(ContentItem.IsDataBoundPropertyKey, content != null);
            }
        }

        protected virtual void ClearContent(object oldContent)
        {
            base.ClearValue(ControlPropertyKey);
            base.ClearValue(ContentItem.IsDataBoundPropertyKey);
        }

        protected virtual Thickness CoerceActualCaptionMargin(Thickness value)
        {
            if (!this.HasVisibleCaption)
            {
                return new Thickness();
            }
            if (!MathHelper.AreEqual(value, new Thickness(double.NaN)))
            {
                return value;
            }
            double bottom = double.IsNaN(this.CaptionToControlDistance) ? 0.0 : this.CaptionToControlDistance;
            switch (base.CaptionLocation)
            {
                case CaptionLocation.Top:
                    return new Thickness(0.0, 0.0, 0.0, bottom);

                case CaptionLocation.Right:
                    return new Thickness(bottom, 0.0, 0.0, 0.0);

                case CaptionLocation.Bottom:
                    return new Thickness(0.0, bottom, 0.0, 0.0);
            }
            return new Thickness(0.0, 0.0, bottom, 0.0);
        }

        protected override double CoerceActualCaptionWidth(double value) => 
            base.HasDesiredCaptionWidth ? CaptionAlignHelper.GetActualCaptionWidth(this) : value;

        protected override string CoerceCaptionFormat(string captionFormat) => 
            string.IsNullOrEmpty(captionFormat) ? DockLayoutManagerParameters.LayoutControlItemCaptionFormat : captionFormat;

        protected virtual double CoerceCaptionToControlDistance(double value)
        {
            if (!double.IsNaN(value))
            {
                return value;
            }
            switch (base.CaptionLocation)
            {
                case CaptionLocation.Top:
                    return DockLayoutManagerParameters.CaptionToControlDistanceTop;

                case CaptionLocation.Right:
                    return DockLayoutManagerParameters.CaptionToControlDistanceRight;

                case CaptionLocation.Bottom:
                    return DockLayoutManagerParameters.CaptionToControlDistanceBottom;
            }
            return DockLayoutManagerParameters.CaptionToControlDistanceLeft;
        }

        protected virtual object CoerceDesiredSize(Size value) => 
            !this.HasDesiredSize ? value : this.desiredSizeCore;

        internal override void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            if ((base.Manager != null) && !base.IsActive)
            {
                this.ActivateItemCore();
            }
        }

        protected virtual System.Windows.Controls.ContentPresenter CreatePresenter()
        {
            System.Windows.Controls.ContentPresenter target = new System.Windows.Controls.ContentPresenter();
            BindingHelper.SetBinding(target, System.Windows.Controls.ContentPresenter.ContentTemplateProperty, this, ContentItem.ContentTemplateProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, System.Windows.Controls.ContentPresenter.ContentTemplateSelectorProperty, this, ContentItem.ContentTemplateSelectorProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, FrameworkElement.DataContextProperty, this, FrameworkElement.DataContextProperty, BindingMode.OneWay);
            BindingHelper.SetBinding(target, System.Windows.Controls.ContentPresenter.ContentProperty, this, ContentItem.ContentProperty, BindingMode.OneWay);
            base.SetValue(ContentPresenterPropertyKey, target);
            return target;
        }

        internal override UIChildren CreateUIChildren() => 
            new EmptyUIChildrenCollection();

        protected override LayoutItemType GetLayoutItemTypeCore() => 
            LayoutItemType.ControlItem;

        protected override void OnCaptionAlignModeChanged(CaptionAlignMode oldValue, CaptionAlignMode value)
        {
            base.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
            base.CoerceValue(ActualCaptionMarginProperty);
            CaptionAlignHelper.UpdateAffectedItems(this, oldValue, value);
        }

        protected override void OnCaptionChanged(object oldValue, object newValue)
        {
            base.OnCaptionChanged(oldValue, newValue);
            this.UpdateHasVisibleCaptionProperty();
        }

        protected override void OnCaptionImageChanged(ImageSource value)
        {
            base.OnCaptionImageChanged(value);
            this.UpdateHasVisibleCaptionProperty();
        }

        protected override void OnCaptionLocationChanged(CaptionLocation value)
        {
            base.CoerceValue(CaptionToControlDistanceProperty);
            base.CoerceValue(ActualCaptionMarginProperty);
            base.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
            base.ClearValue(HasDesiredSizePropertyKey);
        }

        protected override void OnCaptionTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            base.OnCaptionTemplateChanged(oldValue, newValue);
            this.UpdateHasVisibleCaptionProperty();
        }

        protected override void OnCaptionTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            base.OnCaptionTemplateSelectorChanged(oldValue, newValue);
            this.UpdateHasVisibleCaptionProperty();
        }

        protected virtual void OnCaptionToControlDistanceChanged(double value)
        {
            base.CoerceValue(ActualCaptionMarginProperty);
        }

        protected override void OnCaptionWidthChanged(double value)
        {
            base.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
            base.CoerceValue(ActualCaptionMarginProperty);
        }

        protected override void OnContentChanged(object content, object oldContent)
        {
            base.OnContentChanged(content, oldContent);
            this.ClearContent(oldContent);
            this.CheckContent(content);
        }

        protected virtual void OnControlChanged(UIElement control, UIElement oldControl)
        {
            base.SetValue(HasControlPropertyKey, control != null);
            if (oldControl != null)
            {
                base.RemoveLogicalChild(oldControl);
                oldControl.ClearValue(DockLayoutManager.LayoutItemProperty);
            }
            if (control != null)
            {
                base.AddLogicalChild(control);
                control.SetValue(DockLayoutManager.LayoutItemProperty, this);
            }
        }

        protected override void OnDesiredCaptionWidthChanged(double value)
        {
            base.OnDesiredCaptionWidthChanged(value);
            CaptionAlignHelper.UpdateAffectedItems(this, base.CaptionAlignMode);
        }

        protected virtual void OnDesiredSizeChanged(Size value)
        {
            this.SetDesiredSize(value);
            base.SetValue(HasDesiredSizePropertyKey, !value.IsEmpty);
            base.CoerceValue(BaseLayoutItem.ActualMinSizeProperty);
        }

        protected virtual void OnHasVisibleCaptionChanged(bool newValue)
        {
            base.CoerceValue(ActualCaptionMarginProperty);
        }

        protected override void OnIsDataBoundChanged(bool value)
        {
            base.OnIsDataBoundChanged(value);
            if (value)
            {
                base.SetValue(ControlPropertyKey, this.GetDataBoundContainer());
            }
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
            base.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
        }

        protected internal override void OnParentItemsChanged()
        {
            base.OnParentItemsChanged();
            base.CoerceValue(BaseLayoutItem.ActualCaptionWidthProperty);
        }

        protected override void OnShowCaptionChanged(bool value)
        {
            base.OnShowCaptionChanged(value);
            base.ClearValue(DesiredSizeInternalProperty);
        }

        protected virtual void OnShowControlChanged(bool showControl)
        {
            base.ClearValue(DesiredSizeInternalProperty);
        }

        private void SetDesiredSize(Size value)
        {
            if (this.desiredSizeCore != value)
            {
                this.desiredSizeCore = value;
            }
        }

        private void UpdateHasVisibleCaptionProperty()
        {
            bool flag = ((base.Caption != null) || ((base.CaptionTemplate != null) || (base.CaptionTemplateSelector != null))) || (base.CaptionImage != null);
            base.SetValue(HasVisibleCaptionProperty, flag);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Thickness ActualCaptionMargin =>
            (Thickness) base.GetValue(ActualCaptionMarginProperty);

        [Description("Gets or sets the distance between the item's caption and control.This is a dependency property."), XtraSerializableProperty]
        public double CaptionToControlDistance
        {
            get => 
                (double) base.GetValue(CaptionToControlDistanceProperty);
            set => 
                base.SetValue(CaptionToControlDistanceProperty, value);
        }

        public UIElement ContentPresenter =>
            (UIElement) base.GetValue(ContentPresenterProperty);

        [Description("Gets or sets the control displayed by the current item.This is a dependency property."), Category("Control")]
        public UIElement Control =>
            (UIElement) base.GetValue(ControlProperty);

        [Description("Gets or sets the horizontal alignment of the control within the current LayoutControlItem.This is a dependency property."), XtraSerializableProperty, Category("Control")]
        public HorizontalAlignment ControlHorizontalAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(ControlHorizontalAlignmentProperty);
            set => 
                base.SetValue(ControlHorizontalAlignmentProperty, value);
        }

        [Description("Gets or sets the vertical alignment of the control within the current LayoutControlItem.This is a dependency property."), XtraSerializableProperty, Category("Control")]
        public VerticalAlignment ControlVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(ControlVerticalAlignmentProperty);
            set => 
                base.SetValue(ControlVerticalAlignmentProperty, value);
        }

        [Description("Gets whether a control is assigned to the LayoutControlItem.Control property.This is a dependency property.")]
        public bool HasControl =>
            (bool) base.GetValue(HasControlProperty);

        [Description("Gets or sets whether the LayoutControlItem.Control is visible.This is a dependency property."), XtraSerializableProperty, Category("Control")]
        public bool ShowControl
        {
            get => 
                (bool) base.GetValue(ShowControlProperty);
            set => 
                base.SetValue(ShowControlProperty, value);
        }

        internal Size DesiredSizeInternal
        {
            get => 
                (Size) base.GetValue(DesiredSizeInternalProperty);
            set => 
                base.SetValue(DesiredSizeInternalProperty, value);
        }

        internal bool HasDesiredSize =>
            (bool) base.GetValue(HasDesiredSizeProperty);

        internal bool HasVisibleCaption =>
            (bool) base.GetValue(HasVisibleCaptionProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutControlItem.<>c <>9 = new LayoutControlItem.<>c();

            internal void <.cctor>b__16_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlItem) dObj).OnControlChanged((UIElement) e.NewValue, (UIElement) e.OldValue);
            }

            internal void <.cctor>b__16_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlItem) dObj).OnShowControlChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__16_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlItem) dObj).OnCaptionToControlDistanceChanged((double) e.NewValue);
            }

            internal object <.cctor>b__16_3(DependencyObject dObj, object value) => 
                ((LayoutControlItem) dObj).CoerceCaptionToControlDistance((double) value);

            internal void <.cctor>b__16_4(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlItem) dObj).OnDesiredSizeChanged((Size) e.NewValue);
            }

            internal object <.cctor>b__16_5(DependencyObject dObj, object value) => 
                ((LayoutControlItem) dObj).CoerceDesiredSize((Size) value);

            internal object <.cctor>b__16_6(DependencyObject dObj, object value) => 
                ((LayoutControlItem) dObj).CoerceActualCaptionMargin((Thickness) value);

            internal void <.cctor>b__16_7(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((LayoutControlItem) dObj).OnHasVisibleCaptionChanged((bool) e.NewValue);
            }
        }
    }
}

