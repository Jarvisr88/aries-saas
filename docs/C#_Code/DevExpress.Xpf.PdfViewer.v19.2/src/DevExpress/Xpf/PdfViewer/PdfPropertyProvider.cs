namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfPropertyProvider : PropertyProvider
    {
        private bool isFormDataPageVisible;
        private bool isStartScreenVisible;

        public bool IsFormDataPageVisible
        {
            get => 
                this.isFormDataPageVisible;
            protected internal set => 
                base.SetProperty<bool>(ref this.isFormDataPageVisible, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PdfPropertyProvider)), (MethodInfo) methodof(PdfPropertyProvider.get_IsFormDataPageVisible)), new ParameterExpression[0]));
        }

        public bool IsStartScreenVisible
        {
            get => 
                this.isStartScreenVisible;
            protected internal set => 
                base.SetProperty<bool>(ref this.isStartScreenVisible, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PdfPropertyProvider)), (MethodInfo) methodof(PdfPropertyProvider.get_IsStartScreenVisible)), new ParameterExpression[0]));
        }
    }
}

