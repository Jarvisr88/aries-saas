namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class DocumentLoadingViewModel : BindableBase
    {
        private long currentProgress;
        private long totalProgress;
        private string message;

        public DocumentLoadingViewModel()
        {
            this.TotalProgress = 1L;
            this.CurrentProgress = 0L;
        }

        public long CurrentProgress
        {
            get => 
                this.currentProgress;
            set => 
                base.SetProperty<long>(ref this.currentProgress, value, Expression.Lambda<Func<long>>(Expression.Property(Expression.Constant(this, typeof(DocumentLoadingViewModel)), (MethodInfo) methodof(DocumentLoadingViewModel.get_CurrentProgress)), new ParameterExpression[0]));
        }

        public long TotalProgress
        {
            get => 
                this.totalProgress;
            set => 
                base.SetProperty<long>(ref this.totalProgress, value, Expression.Lambda<Func<long>>(Expression.Property(Expression.Constant(this, typeof(DocumentLoadingViewModel)), (MethodInfo) methodof(DocumentLoadingViewModel.get_TotalProgress)), new ParameterExpression[0]));
        }

        public string Message
        {
            get => 
                this.message;
            set => 
                base.SetProperty<string>(ref this.message, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(DocumentLoadingViewModel)), (MethodInfo) methodof(DocumentLoadingViewModel.get_Message)), new ParameterExpression[0]));
        }
    }
}

