namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PdfPrintEditorAttachedBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty HasValidationErrorProperty;

        static PdfPrintEditorAttachedBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfPrintEditorAttachedBehavior), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            HasValidationErrorProperty = DependencyPropertyRegistrator.Register<PdfPrintEditorAttachedBehavior, bool>(System.Linq.Expressions.Expression.Lambda<Func<PdfPrintEditorAttachedBehavior, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfPrintEditorAttachedBehavior.get_HasValidationError)), parameters), false, (d, oldValue, newValue) => d.OnHasValidationErrorChanged(newValue));
        }

        private void OnHasValidationErrorChanged(bool newValue)
        {
            Func<FrameworkElement, PrintDialogViewModel> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<FrameworkElement, PrintDialogViewModel> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.DataContext as PrintDialogViewModel;
            }
            base.AssociatedObject.With<FrameworkElement, PrintDialogViewModel>(evaluator).Do<PrintDialogViewModel>(x => x.HasValidationError = newValue);
            CommandManager.InvalidateRequerySuggested();
        }

        public bool HasValidationError
        {
            get => 
                (bool) base.GetValue(HasValidationErrorProperty);
            set => 
                base.SetValue(HasValidationErrorProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfPrintEditorAttachedBehavior.<>c <>9 = new PdfPrintEditorAttachedBehavior.<>c();
            public static Func<FrameworkElement, PrintDialogViewModel> <>9__2_0;

            internal void <.cctor>b__1_0(PdfPrintEditorAttachedBehavior d, bool oldValue, bool newValue)
            {
                d.OnHasValidationErrorChanged(newValue);
            }

            internal PrintDialogViewModel <OnHasValidationErrorChanged>b__2_0(FrameworkElement x) => 
                x.DataContext as PrintDialogViewModel;
        }
    }
}

