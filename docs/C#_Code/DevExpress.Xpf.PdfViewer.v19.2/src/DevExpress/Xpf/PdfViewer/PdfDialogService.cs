namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;

    public class PdfDialogService : DialogService
    {
        public static readonly DependencyProperty ContentProperty;

        static PdfDialogService()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PdfDialogService), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ContentProperty = DependencyPropertyRegistrator.Register<PdfDialogService, object>(System.Linq.Expressions.Expression.Lambda<Func<PdfDialogService, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PdfDialogService.get_Content)), parameters), null);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }
    }
}

