namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Input;

    public abstract class CommandBase<T> : CommandBase, ICommand<T>, ICommand, IDelegateCommand
    {
        protected Func<T, bool> canExecuteMethod;
        protected bool useCommandManager;
        [CompilerGenerated]
        private EventHandler canExecuteChanged;

        private event EventHandler canExecuteChanged
        {
            [CompilerGenerated] add
            {
                EventHandler canExecuteChanged = this.canExecuteChanged;
                while (true)
                {
                    EventHandler comparand = canExecuteChanged;
                    EventHandler handler3 = comparand + value;
                    canExecuteChanged = Interlocked.CompareExchange<EventHandler>(ref this.canExecuteChanged, handler3, comparand);
                    if (ReferenceEquals(canExecuteChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler canExecuteChanged = this.canExecuteChanged;
                while (true)
                {
                    EventHandler comparand = canExecuteChanged;
                    EventHandler handler3 = comparand - value;
                    canExecuteChanged = Interlocked.CompareExchange<EventHandler>(ref this.canExecuteChanged, handler3, comparand);
                    if (ReferenceEquals(canExecuteChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.useCommandManager)
                {
                    CommandManagerHelper.Subscribe(value);
                }
                else
                {
                    this.canExecuteChanged += value;
                }
            }
            remove
            {
                if (this.useCommandManager)
                {
                    CommandManagerHelper.Unsubscribe(value);
                }
                else
                {
                    this.canExecuteChanged -= value;
                }
            }
        }

        public CommandBase(bool? useCommandManager = new bool?())
        {
            bool? nullable = useCommandManager;
            this.useCommandManager = (nullable != null) ? nullable.GetValueOrDefault() : CommandBase.DefaultUseCommandManager;
        }

        public virtual bool CanExecute(T parameter) => 
            (this.canExecuteMethod != null) ? this.canExecuteMethod(parameter) : true;

        public abstract void Execute(T parameter);
        private static T GetGenericParameter(object parameter, bool suppressCastException = false)
        {
            parameter = TypeCastHelper.TryCast(parameter, typeof(T));
            if ((parameter == null) || (parameter is T))
            {
                return (T) parameter;
            }
            if (!suppressCastException)
            {
                throw new InvalidCastException($"CommandParameter: Unable to cast object of type '{parameter.GetType().FullName}' to type '{typeof(T).FullName}'");
            }
            return default(T);
        }

        protected virtual void OnCanExecuteChanged()
        {
            if (this.canExecuteChanged != null)
            {
                this.canExecuteChanged(this, EventArgs.Empty);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.useCommandManager)
            {
                CommandManagerHelper.InvalidateRequerySuggested();
            }
            else
            {
                this.OnCanExecuteChanged();
            }
        }

        bool ICommand.CanExecute(object parameter) => 
            this.CanExecute(CommandBase<T>.GetGenericParameter(parameter, true));

        void ICommand.Execute(object parameter)
        {
            this.Execute(CommandBase<T>.GetGenericParameter(parameter, false));
        }
    }
}

