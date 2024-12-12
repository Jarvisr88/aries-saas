namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TemplatedTabCaptionControl : TemplatedCaptionControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ActualTabCaptionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TabCaptionTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TabCaptionTemplateSelectorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ShowTabCaptionImageProperty;

        static TemplatedTabCaptionControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TemplatedTabCaptionControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<TemplatedTabCaptionControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<object>("ActualTabCaption", ref ActualTabCaptionProperty, null, (dObj, e) => ((TemplatedCaptionControl) dObj).OnItemPropertyChanged(), null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedTabCaptionControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedTabCaptionControl> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedTabCaptionControl>.New().OverrideMetadata<bool>(TemplatedCaptionControl.ShowCaptionImageProperty, delegate (TemplatedTabCaptionControl d) {
            }, (d, value) => d.OnCoerceShowCaptionImageProperty(value)).Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedTabCaptionControl, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedTabCaptionControl.get_TabCaptionTemplate)), parameters), out TabCaptionTemplateProperty, null, d => d.OnItemPropertyChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedTabCaptionControl), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<TemplatedTabCaptionControl> registrator2 = registrator1.Register<DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedTabCaptionControl, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedTabCaptionControl.get_TabCaptionTemplateSelector)), expressionArray2), out TabCaptionTemplateSelectorProperty, null, d => d.OnItemPropertyChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(TemplatedTabCaptionControl), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<TemplatedTabCaptionControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(TemplatedTabCaptionControl.get_ShowTabCaptionProperty)), expressionArray3), out ShowTabCaptionImageProperty, false, d => d.CoerceValue(TemplatedCaptionControl.ShowCaptionImageProperty), frameworkOptions);
        }

        protected override object GetActualContent() => 
            base.LayoutItem.TabCaption ?? base.LayoutItem.DataContext;

        protected override DataTemplate GetActualContentTemplate() => 
            ((base.LayoutItem == null) || !base.LayoutItem.HasTabCaptionTemplate) ? base.GetActualContentTemplate() : base.LayoutItem.TabCaptionTemplate;

        protected override DataTemplateSelector GetActualContentTemplateSelector() => 
            ((base.LayoutItem == null) || !base.LayoutItem.HasTabCaptionTemplate) ? base.GetActualContentTemplateSelector() : base.LayoutItem.TabCaptionTemplateSelector;

        protected override DependencyProperty GetCaptionImageProperty() => 
            BaseLayoutItem.TabCaptionImageProperty;

        protected override bool GetHasCaptionTemplate(BaseLayoutItem item) => 
            base.GetHasCaptionTemplate(item) || item.HasTabCaptionTemplate;

        private bool OnCoerceShowCaptionImageProperty(bool value) => 
            ((base.LayoutItem == null) || !base.LayoutItem.IsPropertySet(BaseLayoutItem.ShowTabCaptionImageProperty)) ? value : this.ShowTabCaptionProperty;

        protected override void OnItemPropertyChangedOverride()
        {
            base.OnItemPropertyChangedOverride();
            base.InvalidateMeasure();
            Func<DependencyObject, bool> predicate = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<DependencyObject, bool> local1 = <>c.<>9__19_0;
                predicate = <>c.<>9__19_0 = x => x is ITabHeader;
            }
            DependencyObject tabHeader = LayoutTreeHelper.GetVisualParents(this, null).FirstOrDefault<DependencyObject>(predicate);
            if (tabHeader != null)
            {
                Action<UIElement> action = <>c.<>9__19_2;
                if (<>c.<>9__19_2 == null)
                {
                    Action<UIElement> local2 = <>c.<>9__19_2;
                    action = <>c.<>9__19_2 = x => x.InvalidateMeasure();
                }
                LayoutTreeHelper.GetVisualParents(this, null).TakeWhile<DependencyObject>(x => !ReferenceEquals(x, tabHeader)).Concat<DependencyObject>(tabHeader.Yield<DependencyObject>()).OfType<UIElement>().ForEach<UIElement>(action);
            }
        }

        protected override void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            base.OnLayoutItemChanged(oldValue, newValue);
            if (newValue != null)
            {
                BindingHelper.SetBinding(this, ActualTabCaptionProperty, newValue, BaseLayoutItem.TabCaptionProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, Control.HorizontalContentAlignmentProperty, newValue, BaseLayoutItem.TabCaptionHorizontalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, Control.VerticalContentAlignmentProperty, newValue, BaseLayoutItem.TabCaptionVerticalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, TabCaptionTemplateProperty, newValue, BaseLayoutItem.TabCaptionTemplateProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, TabCaptionTemplateSelectorProperty, newValue, BaseLayoutItem.TabCaptionTemplateSelectorProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, FrameworkElement.HorizontalAlignmentProperty, newValue, BaseLayoutItem.TabCaptionHorizontalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, FrameworkElement.VerticalAlignmentProperty, newValue, BaseLayoutItem.TabCaptionVerticalAlignmentProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, TemplatedCaptionControl.ActualCaptionWidthProperty, newValue, BaseLayoutItem.TabCaptionWidthProperty, BindingMode.OneWay);
                BindingHelper.SetBinding(this, ShowTabCaptionImageProperty, newValue, BaseLayoutItem.ShowTabCaptionImageProperty, BindingMode.OneWay);
            }
            else
            {
                BindingHelper.ClearBinding(this, TabCaptionTemplateProperty);
                BindingHelper.ClearBinding(this, TabCaptionTemplateSelectorProperty);
                BindingHelper.ClearBinding(this, ActualTabCaptionProperty);
                BindingHelper.ClearBinding(this, Control.HorizontalContentAlignmentProperty);
                BindingHelper.ClearBinding(this, Control.VerticalContentAlignmentProperty);
                BindingHelper.ClearBinding(this, FrameworkElement.HorizontalAlignmentProperty);
                BindingHelper.ClearBinding(this, FrameworkElement.VerticalAlignmentProperty);
                BindingHelper.ClearBinding(this, TemplatedCaptionControl.ActualCaptionWidthProperty);
                BindingHelper.ClearBinding(this, ShowTabCaptionImageProperty);
            }
        }

        public override bool CanUpdateDesiredCaptionWidth =>
            false;

        protected override bool ShouldUseCaptionTemplate =>
            !base.LayoutItem.ShouldUseTabCaptionTemplate;

        private bool ShowTabCaptionProperty =>
            (bool) base.GetValue(ShowTabCaptionImageProperty);

        private DataTemplate TabCaptionTemplate =>
            (DataTemplate) base.GetValue(TabCaptionTemplateProperty);

        private DataTemplateSelector TabCaptionTemplateSelector =>
            (DataTemplateSelector) base.GetValue(TabCaptionTemplateSelectorProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TemplatedTabCaptionControl.<>c <>9 = new TemplatedTabCaptionControl.<>c();
            public static Func<DependencyObject, bool> <>9__19_0;
            public static Action<UIElement> <>9__19_2;

            internal void <.cctor>b__4_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((TemplatedCaptionControl) dObj).OnItemPropertyChanged();
            }

            internal void <.cctor>b__4_1(TemplatedTabCaptionControl d)
            {
            }

            internal bool <.cctor>b__4_2(TemplatedTabCaptionControl d, bool value) => 
                d.OnCoerceShowCaptionImageProperty(value);

            internal void <.cctor>b__4_3(TemplatedTabCaptionControl d)
            {
                d.OnItemPropertyChanged();
            }

            internal void <.cctor>b__4_4(TemplatedTabCaptionControl d)
            {
                d.OnItemPropertyChanged();
            }

            internal void <.cctor>b__4_5(TemplatedTabCaptionControl d)
            {
                d.CoerceValue(TemplatedCaptionControl.ShowCaptionImageProperty);
            }

            internal bool <OnItemPropertyChangedOverride>b__19_0(DependencyObject x) => 
                x is ITabHeader;

            internal void <OnItemPropertyChangedOverride>b__19_2(UIElement x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

