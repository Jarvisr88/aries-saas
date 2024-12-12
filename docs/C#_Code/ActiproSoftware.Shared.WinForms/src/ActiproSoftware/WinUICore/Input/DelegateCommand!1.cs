namespace ActiproSoftware.WinUICore.Input
{
    using #H;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class DelegateCommand<T> : ICommand
    {
        private Func<T, bool> #Jui;
        private Action<T> #Kui;
        [CompilerGenerated]
        private EventHandler #3Gd;

        public event EventHandler CanExecuteChanged
        {
            [CompilerGenerated] add
            {
                EventHandler objA = this.#3Gd;
                while (true)
                {
                    EventHandler comparand = objA;
                    EventHandler handler3 = comparand + value;
                    objA = Interlocked.CompareExchange<EventHandler>(ref this.#3Gd, handler3, comparand);
                    if (ReferenceEquals(objA, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler objA = this.#3Gd;
                while (true)
                {
                    EventHandler comparand = objA;
                    EventHandler handler3 = comparand - value;
                    objA = Interlocked.CompareExchange<EventHandler>(ref this.#3Gd, handler3, comparand);
                    if (ReferenceEquals(objA, comparand))
                    {
                        return;
                    }
                }
            }
        }

        private bool #Oqk(object #kec) => 
            this.CanExecute((T) #kec);

        private void #Pqk(object #kec)
        {
            this.Execute((T) #kec);
        }

        public DelegateCommand(Action<T> executeAction) : this(executeAction, null)
        {
        }

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecuteFunc)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException(#G.#eg(0xe0d));
            }
            this.#Kui = executeAction;
            this.#Jui = canExecuteFunc;
        }

        public bool CanExecute(T parameter) => 
            (this.#Jui != null) ? this.#Jui(parameter) : true;

        public void Execute(T parameter)
        {
            if (this.CanExecute(parameter))
            {
                this.#Kui(parameter);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = this.#3Gd;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

