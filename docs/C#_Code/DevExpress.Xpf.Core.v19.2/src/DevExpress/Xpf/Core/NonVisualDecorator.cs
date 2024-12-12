namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Child")]
    public class NonVisualDecorator : FrameworkElement, IAddChild
    {
        private static readonly DependencyPropertyKey ActualChildPropertyKey;
        public static readonly DependencyProperty ActualChildProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsSelfInheritanceParentProperty = DependencyProperty.RegisterAttached("IsSelfInheritanceParent", typeof(object), typeof(NonVisualDecorator), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        static NonVisualDecorator()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(NonVisualDecorator), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<NonVisualDecorator>.New().RegisterReadOnly<UIElement>(System.Linq.Expressions.Expression.Lambda<Func<NonVisualDecorator, UIElement>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NonVisualDecorator.get_ActualChild)), parameters), out ActualChildPropertyKey, out ActualChildProperty, null, (d, e) => d.OnActualChildChanged(e), frameworkOptions);
        }

        protected override Size ArrangeOverride(Size arrangeSize) => 
            arrangeSize;

        protected override Visual GetVisualChild(int index)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        protected override Size MeasureOverride(Size constraint) => 
            new Size();

        protected virtual void OnActualChildChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        void IAddChild.AddChild(object value)
        {
            UIElement element = GuardHelper.ArgumentMatchType<UIElement>(value, "value");
            if (this.Child != null)
            {
                throw new ArgumentException("", "value");
            }
            this.Child = element;
        }

        void IAddChild.AddText(string text)
        {
        }

        public UIElement ActualChild
        {
            get => 
                (UIElement) base.GetValue(ActualChildProperty);
            private set => 
                base.SetValue(ActualChildPropertyKey, value);
        }

        [DefaultValue((string) null)]
        public virtual UIElement Child
        {
            get => 
                this.ActualChild;
            set
            {
                UIElement actualChild = this.ActualChild;
                if (!ReferenceEquals(actualChild, value))
                {
                    base.RemoveLogicalChild(actualChild);
                    base.AddLogicalChild(value);
                    this.ActualChild = value;
                    if (value != null)
                    {
                        value.SetValue(IsSelfInheritanceParentProperty, null);
                    }
                }
            }
        }

        protected override IEnumerator LogicalChildren =>
            new SingleLogicalChildEnumerator(this.ActualChild);

        protected override int VisualChildrenCount =>
            0;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NonVisualDecorator.<>c <>9 = new NonVisualDecorator.<>c();

            internal void <.cctor>b__3_0(NonVisualDecorator d, DependencyPropertyChangedEventArgs e)
            {
                d.OnActualChildChanged(e);
            }
        }
    }
}

