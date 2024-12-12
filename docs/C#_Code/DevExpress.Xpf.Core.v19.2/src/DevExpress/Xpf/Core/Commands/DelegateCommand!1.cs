namespace DevExpress.Xpf.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Windows.Threading;

    [Obsolete("This class is obsolete. Use the DevExpress.Mvvm.DelegateCommand instead.")]
    public class DelegateCommand<T> : IDispatcherInfo, ICommand
    {
        private readonly System.Windows.Threading.Dispatcher dispatcher;
        private readonly Action<T> executeMethod;
        private readonly Func<T, bool> canExecuteMethod;
        private List<WeakReference> _canExecuteChangedHandlers;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                WeakEventHandlerManager.AddWeakReferenceHandler(ref this._canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                WeakEventHandlerManager.RemoveWeakReferenceHandler(this._canExecuteChangedHandlers, value);
            }
        }

        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            this.dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod");
            }
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(T parameter) => 
            (this.canExecuteMethod != null) ? this.canExecuteMethod(parameter) : true;

        public void Execute(T parameter)
        {
            if (this.executeMethod != null)
            {
                this.executeMethod(parameter);
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, this._canExecuteChangedHandlers);
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }

        bool ICommand.CanExecute(object parameter) => 
            this.CanExecute((T) parameter);

        void ICommand.Execute(object parameter)
        {
            this.Execute((T) parameter);
        }

        public System.Windows.Threading.Dispatcher Dispatcher =>
            this.dispatcher;
    }
}

