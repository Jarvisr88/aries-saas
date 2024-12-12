namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class TemplatedCaptionControl : psvControl
    {
        public static readonly DependencyProperty CaptionControlTemplateProperty;
        public static readonly DependencyProperty ContentPresenterTemplateProperty;
        public static readonly DependencyProperty LayoutItemProperty;
        public static readonly DependencyProperty CaptionMarginProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualCaptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualDataContextProperty;
        private static readonly DependencyPropertyKey ActualContentPropertyKey;
        public static readonly DependencyProperty ActualContentProperty;
        public static readonly DependencyProperty ActualCaptionWidthProperty;
        public static readonly DependencyProperty ActualContentTemplateProperty;
        private static readonly DependencyPropertyKey ActualContentTemplatePropertyKey;
        public static readonly DependencyProperty ActualContentTemplateSelectorProperty;
        private static readonly DependencyPropertyKey ActualContentTemplateSelectorPropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CaptionTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CaptionTemplateSelectorProperty;
        public static readonly DependencyProperty CaptionImageProperty;
        public static readonly DependencyProperty ShowCaptionImageProperty;
        public static readonly DependencyProperty IsCaptionImageVisibleProperty;
        private static readonly DependencyPropertyKey IsCaptionImageVisiblePropertyKey;
        private bool IsParentMeasureValid = true;

        static TemplatedCaptionControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TemplatedCaptionControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<ControlTemplate>("CaptionControlTemplate", ref CaptionControlTemplateProperty, null, null, null);
            registrator.Register<ControlTemplate>("ContentPresenterTemplate", ref ContentPresenterTemplateProperty, null, null, null);
            registrator.Register<BaseLayoutItem>("LayoutItem", ref LayoutItemProperty, null, (dObj, ea) => ((TemplatedCaptionControl) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.OldValue, (BaseLayoutItem) ea.NewValue), null);
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("CaptionMargin", ref CaptionMarginProperty, defValue, null, null);
            registrator.Register<object>("ActualCaption", ref ActualCaptionProperty, null, (dObj, e) => ((TemplatedCaptionControl) dObj).OnItemPropertyChanged(), null);
            registrator.Register<object>("ActualDataContext", ref ActualDataContextProperty, null, (dObj, e) => ((TemplatedCaptionControl) dObj).OnItemPropertyChanged(), null);
            registrator.RegisterReadonly<object>("ActualContent", ref ActualContentPropertyKey, ref ActualContentProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl>.New().OverrideMetadata(FrameworkElement.WidthProperty, d => d.IsParentMeasureValid = false).Register<double>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_ActualCaptionWidth)), parameters), out ActualCaptionWidthProperty, double.NaN, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator2 = registrator1.Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_CaptionTemplate)), expressionArray2), out CaptionTemplateProperty, null, d => d.OnItemPropertyChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator3 = registrator2.Register<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_CaptionTemplateSelector)), expressionArray3), out CaptionTemplateSelectorProperty, null, d => d.OnItemPropertyChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator4 = registrator3.Register<ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_CaptionImage)), expressionArray4), out CaptionImageProperty, null, d => d.CoerceValue(IsCaptionImageVisibleProperty), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator5 = registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_ShowCaptionImage)), expressionArray5), out ShowCaptionImageProperty, true, d => d.CoerceValue(IsCaptionImageVisibleProperty), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator6 = registrator5.RegisterReadOnly<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_ActualContentTemplate)), expressionArray6), out ActualContentTemplatePropertyKey, out ActualContentTemplateProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedCaptionControl> registrator7 = registrator6.RegisterReadOnly<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_ActualContentTemplateSelector)), expressionArray7), out ActualContentTemplateSelectorPropertyKey, out ActualContentTemplateSelectorProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedCaptionControl), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator7.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedCaptionControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedCaptionControl.get_IsCaptionImageVisible)), expressionArray8), out IsCaptionImageVisiblePropertyKey, out IsCaptionImageVisibleProperty, false, (Func<TemplatedCaptionControl, bool, bool>) ((d, value) => d.OnCoerceIsCaptionImageVisible(value)), frameworkOptions);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.UpdateDesiredCaptionWidth(base.DesiredSize.Width);
            return base.ArrangeOverride(arrangeBounds);
        }

        internal virtual bool CanSetDesiredCaptionWidth() => 
            (this.LayoutItem.CaptionLocation != CaptionLocation.Bottom) && ((this.LayoutItem.CaptionLocation != CaptionLocation.Top) && (!this.LayoutItem.IsLogicalTreeLocked && VisibilityHelper.GetIsVisible(this)));

        protected virtual object GetActualContent() => 
            this.LayoutItem.Caption ?? this.LayoutItem.DataContext;

        protected virtual DataTemplate GetActualContentTemplate()
        {
            BaseLayoutItem layoutItem = this.LayoutItem;
            if (layoutItem != null)
            {
                return layoutItem.CaptionTemplate;
            }
            BaseLayoutItem local1 = layoutItem;
            return null;
        }

        protected virtual DataTemplateSelector GetActualContentTemplateSelector()
        {
            BaseLayoutItem layoutItem = this.LayoutItem;
            if (layoutItem != null)
            {
                return layoutItem.CaptionTemplateSelector;
            }
            BaseLayoutItem local1 = layoutItem;
            return null;
        }

        protected virtual DependencyProperty GetCaptionImageProperty() => 
            BaseLayoutItem.CaptionImageProperty;

        protected virtual bool GetHasCaptionTemplate(BaseLayoutItem item) => 
            item.HasCaptionTemplate;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartCaption = LayoutItemsHelper.GetTemplateChild<CaptionControl>(this);
        }

        protected virtual bool OnCoerceIsCaptionImageVisible(bool value) => 
            (this.CaptionImage != null) && this.ShowCaptionImage;

        protected override void OnDispose()
        {
            this.Unsubscribe();
            if (this.PartCaption != null)
            {
                this.PartCaption.Dispose();
                this.PartCaption = null;
            }
            base.ClearValue(CaptionControlTemplateProperty);
            base.ClearValue(ContentPresenterTemplateProperty);
            base.ClearValue(LayoutItemProperty);
            base.OnDispose();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.SelectTemplate();
        }

        public void OnItemPropertyChanged()
        {
            this.SelectTemplate();
            this.OnItemPropertyChangedOverride();
        }

        protected virtual void OnItemPropertyChangedOverride()
        {
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            if (newValue == null)
            {
                this.Unsubscribe();
                BindingHelper.ClearBinding(this, ActualCaptionProperty);
                BindingHelper.ClearBinding(this, ActualDataContextProperty);
                BindingHelper.ClearBinding(this, CaptionTemplateProperty);
                BindingHelper.ClearBinding(this, CaptionTemplateSelectorProperty);
                BindingHelper.ClearBinding(this, Control.HorizontalContentAlignmentProperty);
                BindingHelper.ClearBinding(this, Control.VerticalContentAlignmentProperty);
                BindingHelper.ClearBinding(this, FrameworkElement.HorizontalAlignmentProperty);
                BindingHelper.ClearBinding(this, FrameworkElement.VerticalAlignmentProperty);
                BindingHelper.ClearBinding(this, CaptionImageProperty);
                BindingHelper.ClearBinding(this, ShowCaptionImageProperty);
            }
            else
            {
                if (this.CanUpdateDesiredCaptionWidth)
                {
                    this.Subscribe();
                }
                BindingHelper.SetBinding(this, ActualCaptionProperty, newValue, BaseLayoutItem.CaptionProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, ActualDataContextProperty, newValue, FrameworkElement.DataContextProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, CaptionTemplateProperty, newValue, BaseLayoutItem.CaptionTemplateProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, CaptionTemplateSelectorProperty, newValue, BaseLayoutItem.CaptionTemplateSelectorProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, Control.HorizontalContentAlignmentProperty, newValue, BaseLayoutItem.CaptionHorizontalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, Control.VerticalContentAlignmentProperty, newValue, BaseLayoutItem.CaptionVerticalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, FrameworkElement.HorizontalAlignmentProperty, newValue, BaseLayoutItem.CaptionHorizontalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, FrameworkElement.VerticalAlignmentProperty, newValue, BaseLayoutItem.CaptionVerticalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, CaptionImageProperty, newValue, this.GetCaptionImageProperty(), BindingMode.OneWay);
                BindingHelper.SetBinding(this, ShowCaptionImageProperty, newValue, BaseLayoutItem.ShowCaptionImageProperty, BindingMode.OneWay);
            }
            this.OnItemPropertyChanged();
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.UpdateDesiredCaptionWidth(base.DesiredSize.Width);
        }

        private void SelectTemplate()
        {
            if (this.LayoutItem != null)
            {
                bool shouldUseCaptionTemplate = this.ShouldUseCaptionTemplate;
                this.Template = shouldUseCaptionTemplate ? this.CaptionControlTemplate : this.ContentPresenterTemplate;
                if (!shouldUseCaptionTemplate)
                {
                    this.ActualContent = this.GetActualContent();
                    this.ActualContentTemplate = this.GetActualContentTemplate();
                    this.ActualContentTemplateSelector = this.GetActualContentTemplateSelector();
                }
            }
        }

        protected void Subscribe()
        {
            this.Unsubscribe();
            base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
        }

        protected void Unsubscribe()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
        }

        private void UpdateDesiredCaptionWidth(double desiredWidth)
        {
            if (this.CanUpdateDesiredCaptionWidth)
            {
                LayoutControlItem layoutItem = (LayoutControlItem) this.LayoutItem;
                if (!layoutItem.HasDesiredCaptionWidth && this.CanSetDesiredCaptionWidth())
                {
                    layoutItem.DesiredCaptionWidth = layoutItem.HasVisibleCaption ? desiredWidth : 0.0;
                    base.InvalidateMeasure();
                }
                if (!this.IsParentMeasureValid)
                {
                    Action<FrameworkElement> action = <>c.<>9__84_0;
                    if (<>c.<>9__84_0 == null)
                    {
                        Action<FrameworkElement> local1 = <>c.<>9__84_0;
                        action = <>c.<>9__84_0 = x => x.InvalidateMeasure();
                    }
                    (base.VisualParent as FrameworkElement).Do<FrameworkElement>(action);
                    this.IsParentMeasureValid = true;
                }
            }
        }

        public double ActualCaptionWidth
        {
            get => 
                (double) base.GetValue(ActualCaptionWidthProperty);
            set => 
                base.SetValue(ActualCaptionWidthProperty, value);
        }

        public object ActualContent
        {
            get => 
                base.GetValue(ActualContentProperty);
            protected set => 
                base.SetValue(ActualContentPropertyKey, value);
        }

        public DataTemplate ActualContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ActualContentTemplateProperty);
            internal set => 
                base.SetValue(ActualContentTemplatePropertyKey, value);
        }

        public DataTemplateSelector ActualContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ActualContentTemplateSelectorProperty);
            internal set => 
                base.SetValue(ActualContentTemplateSelectorPropertyKey, value);
        }

        public virtual bool CanUpdateDesiredCaptionWidth =>
            this.LayoutItem is LayoutControlItem;

        public ControlTemplate CaptionControlTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(CaptionControlTemplateProperty);
            set => 
                base.SetValue(CaptionControlTemplateProperty, value);
        }

        public ImageSource CaptionImage
        {
            get => 
                (ImageSource) base.GetValue(CaptionImageProperty);
            set => 
                base.SetValue(CaptionImageProperty, value);
        }

        public Thickness CaptionMargin
        {
            get => 
                (Thickness) base.GetValue(CaptionMarginProperty);
            set => 
                base.SetValue(CaptionMarginProperty, value);
        }

        public ControlTemplate ContentPresenterTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(ContentPresenterTemplateProperty);
            set => 
                base.SetValue(ContentPresenterTemplateProperty, value);
        }

        public bool IsCaptionImageVisible
        {
            get => 
                (bool) base.GetValue(IsCaptionImageVisibleProperty);
            private set => 
                base.SetValue(IsCaptionImageVisiblePropertyKey, value);
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        public CaptionControl PartCaption { get; private set; }

        public bool ShowCaptionImage
        {
            get => 
                (bool) base.GetValue(ShowCaptionImageProperty);
            set => 
                base.SetValue(ShowCaptionImageProperty, value);
        }

        protected virtual bool ShouldUseCaptionTemplate =>
            !this.LayoutItem.ShouldUseCaptionTemplate;

        private DataTemplate CaptionTemplate =>
            (DataTemplate) base.GetValue(CaptionTemplateProperty);

        private DataTemplateSelector CaptionTemplateSelector =>
            (DataTemplateSelector) base.GetValue(CaptionTemplateSelectorProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TemplatedCaptionControl.<>c <>9 = new TemplatedCaptionControl.<>c();
            public static Action<FrameworkElement> <>9__84_0;

            internal void <.cctor>b__19_0(DependencyObject dObj, DependencyPropertyChangedEventArgs ea)
            {
                ((TemplatedCaptionControl) dObj).OnLayoutItemChanged((BaseLayoutItem) ea.OldValue, (BaseLayoutItem) ea.NewValue);
            }

            internal void <.cctor>b__19_1(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TemplatedCaptionControl) dObj).OnItemPropertyChanged();
            }

            internal void <.cctor>b__19_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TemplatedCaptionControl) dObj).OnItemPropertyChanged();
            }

            internal void <.cctor>b__19_3(TemplatedCaptionControl d)
            {
                d.IsParentMeasureValid = false;
            }

            internal void <.cctor>b__19_4(TemplatedCaptionControl d)
            {
                d.OnItemPropertyChanged();
            }

            internal void <.cctor>b__19_5(TemplatedCaptionControl d)
            {
                d.OnItemPropertyChanged();
            }

            internal void <.cctor>b__19_6(TemplatedCaptionControl d)
            {
                d.CoerceValue(TemplatedCaptionControl.IsCaptionImageVisibleProperty);
            }

            internal void <.cctor>b__19_7(TemplatedCaptionControl d)
            {
                d.CoerceValue(TemplatedCaptionControl.IsCaptionImageVisibleProperty);
            }

            internal bool <.cctor>b__19_8(TemplatedCaptionControl d, bool value) => 
                d.OnCoerceIsCaptionImageVisible(value);

            internal void <UpdateDesiredCaptionWidth>b__84_0(FrameworkElement x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

