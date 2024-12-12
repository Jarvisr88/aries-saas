namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PdfSplitItem : CommandBase
    {
        private ObservableCollection<ICommand> commands;

        public ObservableCollection<ICommand> Commands
        {
            get => 
                this.commands;
            set => 
                base.SetProperty<ObservableCollection<ICommand>>(ref this.commands, value, Expression.Lambda<Func<ObservableCollection<ICommand>>>(Expression.Property(Expression.Constant(this, typeof(PdfSplitItem)), (MethodInfo) methodof(PdfSplitItem.get_Commands)), new ParameterExpression[0]));
        }
    }
}

