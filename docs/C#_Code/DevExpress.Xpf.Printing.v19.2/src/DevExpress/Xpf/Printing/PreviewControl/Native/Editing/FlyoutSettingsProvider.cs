namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Editors.Flyout;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;

    public class FlyoutSettingsProvider : Behavior<FlyoutControl>
    {
        public static readonly DependencyProperty TargetProperty;

        static FlyoutSettingsProvider()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(FlyoutSettingsProvider), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<FlyoutSettingsProvider>.New().Register<UIElement>(System.Linq.Expressions.Expression.Lambda<Func<FlyoutSettingsProvider, UIElement>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(FlyoutSettingsProvider.get_Target)), parameters), out TargetProperty, null, frameworkOptions);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            FlyoutSettings settings1 = new FlyoutSettings();
            settings1.ShowIndicator = true;
            settings1.IndicatorVerticalAlignment = 1;
            settings1.Placement = FlyoutPlacement.Right;
            FlyoutSettings target = settings1;
            Binding binding = new Binding("Target");
            binding.Source = this;
            BindingOperations.SetBinding(target, FlyoutSettings.IndicatorTargetProperty, binding);
            base.AssociatedObject.Settings = target;
        }

        public UIElement Target
        {
            get => 
                (UIElement) base.GetValue(TargetProperty);
            set => 
                base.SetValue(TargetProperty, value);
        }
    }
}

