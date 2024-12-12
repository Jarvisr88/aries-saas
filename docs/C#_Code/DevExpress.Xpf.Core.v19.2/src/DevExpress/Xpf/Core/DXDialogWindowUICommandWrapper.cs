namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Input;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class DXDialogWindowUICommandWrapper
    {
        private int lockOnCommandCanExecuteChanged;

        public event CancelEventHandler CommandExecuted;

        public DXDialogWindowUICommandWrapper(DevExpress.Mvvm.UICommand uiCommand)
        {
            this.UICommand = uiCommand;
            bool? useCommandManager = null;
            this.RealCommand = new DelegateCommand(new Action(this.CommandWrapperExecute), new Func<bool>(this.CommandWrapperCanExecute), useCommandManager);
        }

        private bool CommandWrapperCanExecute()
        {
            this.lockOnCommandCanExecuteChanged++;
            Func<ICommand, bool> evaluator = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<ICommand, bool> local1 = <>c.<>9__18_0;
                evaluator = <>c.<>9__18_0 = x => x.CanExecute(null);
            }
            return this.UICommand.Command.Return<ICommand, bool>(evaluator, (<>c.<>9__18_1 ??= () => true));
        }

        private void CommandWrapperExecute()
        {
            CancelEventArgs parameter = new CancelEventArgs();
            if (this.UICommand.Command != null)
            {
                this.UICommand.Command.Execute(parameter);
            }
            if (this.CommandExecuted != null)
            {
                this.CommandExecuted(this, parameter);
            }
        }

        private void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            if (this.lockOnCommandCanExecuteChanged > 0)
            {
                this.lockOnCommandCanExecuteChanged = 0;
            }
            else
            {
                this.RealCommand.RaiseCanExecuteChanged();
            }
        }

        private void OnUICommandPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == BindableBase.GetPropertyName<ICommand>(Expression.Lambda<Func<ICommand>>(Expression.Property(Expression.Property(Expression.Constant(this, typeof(DXDialogWindowUICommandWrapper)), (MethodInfo) methodof(DXDialogWindowUICommandWrapper.get_UICommand)), (MethodInfo) methodof(DevExpress.Mvvm.UICommand.get_Command)), new ParameterExpression[0])))
            {
                this.Subscribe();
                this.RealCommand.RaiseCanExecuteChanged();
            }
        }

        public void Subscribe()
        {
            this.Unsubscribe();
            this.UICommand.PropertyChanged += new PropertyChangedEventHandler(this.OnUICommandPropertyChanged);
            if (this.UICommand.Command != null)
            {
                this.UICommand.Command.CanExecuteChanged += new EventHandler(this.OnCommandCanExecuteChanged);
            }
        }

        public void Unsubscribe()
        {
            if (this.UICommand.Command != null)
            {
                this.UICommand.Command.CanExecuteChanged -= new EventHandler(this.OnCommandCanExecuteChanged);
            }
            this.UICommand.PropertyChanged -= new PropertyChangedEventHandler(this.OnUICommandPropertyChanged);
        }

        public DevExpress.Mvvm.UICommand UICommand { get; private set; }

        public DelegateCommand RealCommand { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXDialogWindowUICommandWrapper.<>c <>9 = new DXDialogWindowUICommandWrapper.<>c();
            public static Func<ICommand, bool> <>9__18_0;
            public static Func<bool> <>9__18_1;

            internal bool <CommandWrapperCanExecute>b__18_0(ICommand x) => 
                x.CanExecute(null);

            internal bool <CommandWrapperCanExecute>b__18_1() => 
                true;
        }
    }
}

