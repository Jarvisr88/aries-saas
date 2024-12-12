namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public abstract class BaseCustomizationItem : ContentControl, IUIElement
    {
        public static readonly DependencyProperty LayoutItemProperty;

        static BaseCustomizationItem()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(BaseCustomizationItem), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<BaseCustomizationItem>.New().Register<BaseLayoutItem>(System.Linq.Expressions.Expression.Lambda<Func<BaseCustomizationItem, BaseLayoutItem>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(BaseCustomizationItem.get_LayoutItem)), parameters), out LayoutItemProperty, null, (d, oldValue, newValue) => d.OnLayoutItemChanged(oldValue, newValue), frameworkOptions);
        }

        protected BaseCustomizationItem()
        {
        }

        protected virtual void OnLayoutItemChanged(BaseLayoutItem oldValue, BaseLayoutItem newValue)
        {
            if (newValue != null)
            {
                newValue.Forward(this, ContentControl.ContentProperty, BaseLayoutItem.ActualCustomizationCaptionProperty, BindingMode.OneWay);
                newValue.Forward(this, ContentControl.ContentTemplateProperty, BaseLayoutItem.ActualCustomizationCaptionTemplateProperty, BindingMode.OneWay);
                newValue.Forward(this, ContentControl.ContentTemplateSelectorProperty, BaseLayoutItem.ActualCustomizationCaptionTemplateSelectorProperty, BindingMode.OneWay);
            }
            else
            {
                base.ClearValue(ContentControl.ContentProperty);
                base.ClearValue(ContentControl.ContentTemplateProperty);
                base.ClearValue(ContentControl.ContentTemplateSelectorProperty);
            }
        }

        public BaseLayoutItem LayoutItem
        {
            get => 
                (BaseLayoutItem) base.GetValue(LayoutItemProperty);
            set => 
                base.SetValue(LayoutItemProperty, value);
        }

        IUIElement IUIElement.Scope =>
            DockLayoutManager.GetUIScope(this);

        UIChildren IUIElement.Children { get; } = new UIChildren()

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseCustomizationItem.<>c <>9 = new BaseCustomizationItem.<>c();

            internal void <.cctor>b__1_0(BaseCustomizationItem d, BaseLayoutItem oldValue, BaseLayoutItem newValue)
            {
                d.OnLayoutItemChanged(oldValue, newValue);
            }
        }
    }
}

