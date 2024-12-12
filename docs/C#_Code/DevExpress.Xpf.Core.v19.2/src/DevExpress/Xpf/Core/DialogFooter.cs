namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DialogFooter : NonVisualDecorator
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ActualDialogWindowProperty;

        static DialogFooter()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogFooter), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<DialogFooter>.New().AddOwner<DXDialogWindow>(System.Linq.Expressions.Expression.Lambda<Func<DialogFooter, DXDialogWindow>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogFooter.get_ActualDialogWindow)), parameters), out ActualDialogWindowProperty, DXDialogWindow.ActualDialogWindowProperty, null, (d, e) => d.OnActualDialogWindowChanged(e));
        }

        protected override void OnActualChildChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnActualChildChanged(e);
            UIElement oldValue = (UIElement) e.OldValue;
            UIElement newValue = (UIElement) e.NewValue;
            if ((oldValue != null) && (this.ActualDialogWindow != null))
            {
                this.ActualDialogWindow.RemoveCustomFooter(oldValue);
            }
            if ((newValue != null) && (this.ActualDialogWindow != null))
            {
                this.ActualDialogWindow.AddCustomFooter(newValue);
            }
        }

        protected virtual void OnActualDialogWindowChanged(DependencyPropertyChangedEventArgs e)
        {
            DXDialogWindow oldValue = (DXDialogWindow) e.OldValue;
            if ((oldValue != null) && (base.ActualChild != null))
            {
                oldValue.RemoveCustomFooter(base.ActualChild);
            }
            DXDialogWindow newValue = (DXDialogWindow) e.NewValue;
            if ((newValue != null) && (base.ActualChild != null))
            {
                newValue.AddCustomFooter(base.ActualChild);
            }
        }

        public DXDialogWindow ActualDialogWindow =>
            (DXDialogWindow) base.GetValue(ActualDialogWindowProperty);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DialogFooter.<>c <>9 = new DialogFooter.<>c();

            internal void <.cctor>b__1_0(DialogFooter d, DependencyPropertyChangedEventArgs e)
            {
                d.OnActualDialogWindowChanged(e);
            }
        }
    }
}

