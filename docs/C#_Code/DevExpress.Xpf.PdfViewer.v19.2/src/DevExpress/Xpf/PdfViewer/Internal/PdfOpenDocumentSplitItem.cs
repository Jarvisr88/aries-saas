namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfOpenDocumentSplitItem : CommandBase
    {
        private ObservableCollection<RecentFileViewModel> recentFiles;

        public ObservableCollection<RecentFileViewModel> RecentFiles
        {
            get => 
                this.recentFiles;
            set => 
                base.SetProperty<ObservableCollection<RecentFileViewModel>>(ref this.recentFiles, value, Expression.Lambda<Func<ObservableCollection<RecentFileViewModel>>>(Expression.Property(Expression.Constant(this, typeof(PdfOpenDocumentSplitItem)), (MethodInfo) methodof(PdfOpenDocumentSplitItem.get_RecentFiles)), new ParameterExpression[0]));
        }
    }
}

