namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class PanelTabContainer : LayoutTabControl
    {
        public static readonly DependencyProperty TabHeaderBorderThicknessProperty;
        public static readonly DependencyProperty ActualTabHeaderBorderThicknessProperty;
        private static readonly DependencyPropertyKey ActualTabHeaderBorderThicknessPropertyKey;
        public static readonly DependencyProperty IsFloatingRootItemProperty;

        static PanelTabContainer()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelTabContainer), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            Thickness defaultValue = new Thickness();
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelTabContainer> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelTabContainer>.New().OverrideDefaultStyleKey().Register<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<PanelTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelTabContainer.get_TabHeaderBorderThickness)), parameters), out TabHeaderBorderThicknessProperty, defaultValue, (d, oldValue, newValue) => d.OnTabHeaderBorderThicknessChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelTabContainer), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            defaultValue = new Thickness();
            frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<PanelTabContainer> registrator2 = registrator1.RegisterReadOnly<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<PanelTabContainer, Thickness>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelTabContainer.get_ActualTabHeaderBorderThickness)), expressionArray2), out ActualTabHeaderBorderThicknessPropertyKey, out ActualTabHeaderBorderThicknessProperty, defaultValue, (Func<PanelTabContainer, Thickness, Thickness>) ((d, value) => d.OnCoerceActualTabHeaderBorderThickness(value)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PanelTabContainer), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<PanelTabContainer, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PanelTabContainer.get_IsFloatingRootItem)), expressionArray3), out IsFloatingRootItemProperty, false, d => d.CoerceValue(ActualTabHeaderBorderThicknessProperty), frameworkOptions);
        }

        protected override void ClearGroupBindings(LayoutGroup oldValue)
        {
            base.ClearValue(IsFloatingRootItemProperty);
            base.ClearGroupBindings(oldValue);
        }

        protected override psvSelectorItem CreateSelectorItem() => 
            new TabbedPaneItem();

        protected override IView GetView(DockLayoutManager container)
        {
            TabbedPane owner = null;
            TabbedPaneContentPresenter templatedParent = base.TemplatedParent as TabbedPaneContentPresenter;
            if (templatedParent != null)
            {
                owner = templatedParent.Owner;
            }
            return ((owner != null) ? container.GetView(owner.GetRootUIScope()) : null);
        }

        protected virtual Thickness OnCoerceActualTabHeaderBorderThickness(Thickness value)
        {
            Thickness thickness = base.CaptionLocation.ToThickness(this.TabHeaderBorderThickness);
            return (((base.ViewStyle != DockingViewStyle.Light) || LayoutItemsHelper.IsFloatingRootItem(base.LayoutItem)) ? this.TabHeaderBorderThickness : thickness);
        }

        protected virtual void OnTabHeaderBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            base.CoerceValue(ActualTabHeaderBorderThicknessProperty);
        }

        protected override void OnViewStyleChanged(DockingViewStyle oldValue, DockingViewStyle newValue)
        {
            base.CoerceValue(ActualTabHeaderBorderThicknessProperty);
        }

        protected override void SetGroupBindings(LayoutGroup group)
        {
            base.SetGroupBindings(group);
            group.Forward(this, IsFloatingRootItemProperty, BaseLayoutItem.IsFloatingRootItemProperty, BindingMode.OneWay);
        }

        public Thickness ActualTabHeaderBorderThickness
        {
            get => 
                (Thickness) base.GetValue(ActualTabHeaderBorderThicknessProperty);
            private set => 
                base.SetValue(ActualTabHeaderBorderThicknessPropertyKey, value);
        }

        public Thickness TabHeaderBorderThickness
        {
            get => 
                (Thickness) base.GetValue(TabHeaderBorderThicknessProperty);
            set => 
                base.SetValue(TabHeaderBorderThicknessProperty, value);
        }

        public bool IsFloatingRootItem
        {
            get => 
                (bool) base.GetValue(IsFloatingRootItemProperty);
            set => 
                base.SetValue(IsFloatingRootItemProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PanelTabContainer.<>c <>9 = new PanelTabContainer.<>c();

            internal void <.cctor>b__4_0(PanelTabContainer d, Thickness oldValue, Thickness newValue)
            {
                d.OnTabHeaderBorderThicknessChanged(oldValue, newValue);
            }

            internal Thickness <.cctor>b__4_1(PanelTabContainer d, Thickness value) => 
                d.OnCoerceActualTabHeaderBorderThickness(value);

            internal void <.cctor>b__4_2(PanelTabContainer d)
            {
                d.CoerceValue(PanelTabContainer.ActualTabHeaderBorderThicknessProperty);
            }
        }
    }
}

