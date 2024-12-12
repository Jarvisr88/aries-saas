namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class CommandSplitItem : CommandBase
    {
        private ObservableCollection<ICommand> commands = new ObservableCollection<ICommand>();

        public ObservableCollection<ICommand> Commands
        {
            get => 
                this.commands;
            set => 
                base.SetProperty<ObservableCollection<ICommand>>(ref this.commands, value, Expression.Lambda<Func<ObservableCollection<ICommand>>>(Expression.Property(Expression.Constant(this, typeof(CommandSplitItem)), (MethodInfo) methodof(CommandSplitItem.get_Commands)), new ParameterExpression[0]));
        }
    }
}

