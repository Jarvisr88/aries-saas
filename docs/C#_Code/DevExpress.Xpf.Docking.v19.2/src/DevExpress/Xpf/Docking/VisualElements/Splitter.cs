namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class Splitter : BaseSplitterControl, IUIElement
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty DockLayoutManagerProperty;
        public static readonly DependencyProperty ViewStyleProperty;

        static Splitter()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(Splitter), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            expression = System.Linq.Expressions.Expression.Parameter(typeof(Splitter), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<Splitter>.New().OverrideDefaultStyleKey().AddOwner<DockLayoutManager>(System.Linq.Expressions.Expression.Lambda<Func<Splitter, DockLayoutManager>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DockLayoutManager.GetDockLayoutManager), arguments), parameters), out DockLayoutManagerProperty, DockLayoutManager.DockLayoutManagerProperty, null, (d, oldValue, newValue) => d.OnDockLayoutManagerChanged(oldValue, newValue)).Register<DockingViewStyle>(System.Linq.Expressions.Expression.Lambda<Func<Splitter, DockingViewStyle>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(Splitter.get_ViewStyle)), expressionArray3), out ViewStyleProperty, DockingViewStyle.Default, (Func<Splitter, DockingViewStyle, DockingViewStyle>) ((d, value) => d.OnCoerceDockingViewStyle(value)), frameworkOptions);
        }

        public Splitter(LayoutGroup group) : base(group)
        {
        }

        protected virtual DockingViewStyle OnCoerceDockingViewStyle(DockingViewStyle viewStyle) => 
            base.LayoutGroup.Items.ContainsLayoutControlItemOrGroup() ? DockingViewStyle.Default : viewStyle;

        protected virtual void OnDockLayoutManagerChanged(DockLayoutManager oldValue, DockLayoutManager newValue)
        {
            base.ClearValue(ViewStyleProperty);
            if (newValue != null)
            {
                BindingHelper.SetBinding(this, ViewStyleProperty, newValue, DockLayoutManager.ViewStyleProperty, BindingMode.OneWay);
            }
        }

        protected override IResizeCalculator ResolveResizeCalculator()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return base.ResolveResizeCalculator();
            }
            RecursiveResizeCalculator calculator1 = new RecursiveResizeCalculator();
            calculator1.Orientation = base.Orientation;
            return calculator1;
        }

        public DockingViewStyle ViewStyle
        {
            get => 
                (DockingViewStyle) base.GetValue(ViewStyleProperty);
            set => 
                base.SetValue(ViewStyleProperty, value);
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Splitter.<>c <>9 = new Splitter.<>c();

            internal void <.cctor>b__2_0(Splitter d, DockLayoutManager oldValue, DockLayoutManager newValue)
            {
                d.OnDockLayoutManagerChanged(oldValue, newValue);
            }

            internal DockingViewStyle <.cctor>b__2_1(Splitter d, DockingViewStyle value) => 
                d.OnCoerceDockingViewStyle(value);
        }
    }
}

