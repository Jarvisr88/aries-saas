namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Pdf;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Input;

    public class CommandSetPageLayout : CommandToggleButton
    {
        private bool isSeparator;
        private PdfPageLayout pageLayout;
        private System.Windows.Input.KeyGesture keyGesture;

        public bool IsSeparator
        {
            get => 
                this.isSeparator;
            set => 
                base.SetProperty<bool>(ref this.isSeparator, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CommandSetPageLayout)), (MethodInfo) methodof(CommandSetPageLayout.get_IsSeparator)), new ParameterExpression[0]));
        }

        public PdfPageLayout PageLayout
        {
            get => 
                this.pageLayout;
            set => 
                base.SetProperty<PdfPageLayout>(ref this.pageLayout, value, Expression.Lambda<Func<PdfPageLayout>>(Expression.Property(Expression.Constant(this, typeof(CommandSetPageLayout)), (MethodInfo) methodof(CommandSetPageLayout.get_PageLayout)), new ParameterExpression[0]));
        }

        public System.Windows.Input.KeyGesture KeyGesture
        {
            get => 
                this.keyGesture;
            set => 
                base.SetProperty<System.Windows.Input.KeyGesture>(ref this.keyGesture, value, Expression.Lambda<Func<System.Windows.Input.KeyGesture>>(Expression.Property(Expression.Constant(this, typeof(CommandSetPageLayout)), (MethodInfo) methodof(CommandSetPageLayout.get_KeyGesture)), new ParameterExpression[0]));
        }
    }
}

