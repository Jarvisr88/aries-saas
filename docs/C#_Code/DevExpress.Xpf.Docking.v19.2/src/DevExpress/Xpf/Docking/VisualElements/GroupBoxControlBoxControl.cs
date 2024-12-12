namespace DevExpress.Xpf.Docking.VisualElements
{
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

    [TemplatePart(Name="PART_ExpandButton", Type=typeof(ControlBoxButtonPresenter))]
    public class GroupBoxControlBoxControl : BaseControlBoxControl
    {
        public static readonly DependencyProperty ExpandButtonTemplateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty AllowExpandProperty;

        static GroupBoxControlBoxControl()
        {
            DevExpress.Xpf.Docking.DependencyPropertyRegistrator<GroupBoxControlBoxControl> registrator = new DevExpress.Xpf.Docking.DependencyPropertyRegistrator<GroupBoxControlBoxControl>();
            registrator.OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
            registrator.Register<DataTemplate>("ExpandButtonTemplate", ref ExpandButtonTemplateProperty, null, null, null);
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(GroupBoxControlBoxControl), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<GroupBoxControlBoxControl>.New().OverrideMetadata<Thickness>(FrameworkElement.MarginProperty, delegate (GroupBoxControlBoxControl d) {
            }, (d, value) => d.OnCoerceMargin(value)).Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<GroupBoxControlBoxControl, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(GroupBoxControlBoxControl.get_AllowExpand)), parameters), out AllowExpandProperty, false, (d, oldValue, newValue) => d.OnAllowExpandChanged(oldValue, newValue), frameworkOptions);
        }

        protected override void ClearControlBoxBindings()
        {
            if (this.PartExpandButton != null)
            {
                this.PartExpandButton.ClearValue(UIElement.VisibilityProperty);
            }
            base.ClearValue(AllowExpandProperty);
            base.ClearControlBoxBindings();
        }

        protected override void EnsureTemplateChildren()
        {
            base.EnsureTemplateChildren();
            this.PartExpandButton = base.EnsurePresenter<ControlBoxButtonPresenter>(this.PartExpandButton, "PART_ExpandButton");
        }

        protected virtual void OnAllowExpandChanged(bool oldValue, bool newValue)
        {
            base.CoerceIsActuallyVisible();
        }

        protected override bool OnCoerceIsActuallyVisible(bool value) => 
            base.OnCoerceIsActuallyVisible(value) || ((this.PartExpandButton != null) && this.AllowExpand);

        protected virtual Thickness OnCoerceMargin(Thickness value)
        {
            if (base.IsActuallyVisible)
            {
                return value;
            }
            return new Thickness();
        }

        protected override void OnDispose()
        {
            if (this.PartExpandButton != null)
            {
                this.PartExpandButton.Dispose();
                this.PartExpandButton = null;
            }
            base.OnDispose();
        }

        protected override void OnIsActuallyVisibleChanged(bool oldValue, bool newValue)
        {
            base.OnIsActuallyVisibleChanged(oldValue, newValue);
            base.CoerceValue(FrameworkElement.MarginProperty);
        }

        protected override void SetControlBoxBindings()
        {
            base.SetControlBoxBindings();
            BindingHelper.SetBinding(this, AllowExpandProperty, base.LayoutItem, LayoutGroup.AllowExpandProperty, BindingMode.OneWay);
            if (this.PartExpandButton != null)
            {
                BindingHelper.SetBinding(this.PartExpandButton, UIElement.VisibilityProperty, base.LayoutItem, LayoutGroup.AllowExpandProperty, new BooleanToVisibilityConverter());
            }
        }

        public DataTemplate ExpandButtonTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ExpandButtonTemplateProperty);
            set => 
                base.SetValue(ExpandButtonTemplateProperty, value);
        }

        public ControlBoxButtonPresenter PartExpandButton { get; private set; }

        internal bool AllowExpand =>
            (bool) base.GetValue(AllowExpandProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GroupBoxControlBoxControl.<>c <>9 = new GroupBoxControlBoxControl.<>c();

            internal void <.cctor>b__2_0(GroupBoxControlBoxControl d)
            {
            }

            internal Thickness <.cctor>b__2_1(GroupBoxControlBoxControl d, Thickness value) => 
                d.OnCoerceMargin(value);

            internal void <.cctor>b__2_2(GroupBoxControlBoxControl d, bool oldValue, bool newValue)
            {
                d.OnAllowExpandChanged(oldValue, newValue);
            }
        }
    }
}

