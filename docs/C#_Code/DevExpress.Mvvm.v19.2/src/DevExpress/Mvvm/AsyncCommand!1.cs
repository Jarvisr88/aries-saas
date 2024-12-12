namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class AsyncCommand<T> : AsyncCommandBase<T>, IAsyncCommand, IDelegateCommand, ICommand
    {
        private bool allowMultipleExecution;
        private bool isExecuting;
        private System.Threading.CancellationTokenSource cancellationTokenSource;
        private bool shouldCancel;
        internal Task executeTask;
        private DispatcherOperation completeTaskOperation;

        public AsyncCommand(Func<T, Task> executeMethod) : this(executeMethod, null, false, nullable)
        {
        }

        public AsyncCommand(Func<T, Task> executeMethod, bool useCommandManager) : this(executeMethod, null, false, new bool?(useCommandManager))
        {
        }

        public AsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool? useCommandManager = new bool?()) : this(executeMethod, canExecuteMethod, false, useCommandManager)
        {
        }

        public AsyncCommand(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool allowMultipleExecution, bool? useCommandManager = new bool?()) : base(executeMethod, canExecuteMethod, useCommandManager)
        {
            this.CancelCommand = new DelegateCommand(new Action(this.Cancel), new Func<bool>(this.CanCancel), false);
            this.AllowMultipleExecution = allowMultipleExecution;
        }

        private bool CanCancel() => 
            this.IsExecuting;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void Cancel()
        {
            if (this.CanCancel())
            {
                this.ShouldCancel = true;
                this.CancellationTokenSource.Cancel();
            }
        }

        public override bool CanExecute(T parameter) => 
            (this.AllowMultipleExecution || !this.IsExecuting) ? base.CanExecute(parameter) : false;

        public override void Execute(T parameter)
        {
            if (this.CanExecute(parameter) && (base.executeMethod != null))
            {
                <>c__DisplayClass31_0<T> class_;
                this.IsExecuting = true;
                Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
                this.CancellationTokenSource = new System.Threading.CancellationTokenSource();
                this.executeTask = base.executeMethod(parameter).ContinueWith(x => ((AsyncCommand<T>) this).completeTaskOperation = dispatcher.BeginInvoke(delegate {
                    class_.IsExecuting = false;
                    class_.ShouldCancel = false;
                    class_.completeTaskOperation = null;
                }, new object[0]));
            }
        }

        private void OnIsExecutingChanged()
        {
            this.CancelCommand.RaiseCanExecuteChanged();
            base.RaiseCanExecuteChanged();
        }

        public void Wait(TimeSpan timeout)
        {
            if ((this.executeTask != null) && this.IsExecuting)
            {
                this.executeTask.Wait(timeout);
                this.completeTaskOperation.Do<DispatcherOperation>(x => x.Wait(timeout));
            }
        }

        public bool AllowMultipleExecution
        {
            get => 
                this.allowMultipleExecution;
            set => 
                this.allowMultipleExecution = value;
        }

        public bool IsExecuting
        {
            get => 
                this.isExecuting;
            private set
            {
                if (this.isExecuting != value)
                {
                    this.isExecuting = value;
                    base.RaisePropertyChanged(BindableBase.GetPropertyName<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(AsyncCommand<T>)), (MethodInfo) methodof(AsyncCommand<T>.get_IsExecuting, AsyncCommand<T>)), new ParameterExpression[0])));
                    this.OnIsExecutingChanged();
                }
            }
        }

        public System.Threading.CancellationTokenSource CancellationTokenSource
        {
            get => 
                this.cancellationTokenSource;
            private set
            {
                if (!ReferenceEquals(this.cancellationTokenSource, value))
                {
                    this.cancellationTokenSource = value;
                    base.RaisePropertyChanged(BindableBase.GetPropertyName<System.Threading.CancellationTokenSource>(Expression.Lambda<Func<System.Threading.CancellationTokenSource>>(Expression.Property(Expression.Constant(this, typeof(AsyncCommand<T>)), (MethodInfo) methodof(AsyncCommand<T>.get_CancellationTokenSource, AsyncCommand<T>)), new ParameterExpression[0])));
                }
            }
        }

        [Obsolete("Use the IsCancellationRequested property instead."), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldCancel
        {
            get => 
                this.shouldCancel;
            private set
            {
                if (this.shouldCancel != value)
                {
                    this.shouldCancel = value;
                    base.RaisePropertyChanged(BindableBase.GetPropertyName<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(AsyncCommand<T>)), (MethodInfo) methodof(AsyncCommand<T>.get_ShouldCancel, AsyncCommand<T>)), new ParameterExpression[0])));
                }
            }
        }

        public bool IsCancellationRequested =>
            (this.CancellationTokenSource != null) ? this.CancellationTokenSource.IsCancellationRequested : false;

        public DelegateCommand CancelCommand { get; private set; }

        ICommand IAsyncCommand.CancelCommand =>
            this.CancelCommand;
    }
}

