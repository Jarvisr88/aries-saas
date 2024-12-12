namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class AsyncCommandBase<T> : CommandBase<T>, INotifyPropertyChanged
    {
        protected Func<T, Task> executeMethod;
        [CompilerGenerated]
        private PropertyChangedEventHandler propertyChanged;

        private event PropertyChangedEventHandler propertyChanged
        {
            [CompilerGenerated] add
            {
                PropertyChangedEventHandler propertyChanged = this.propertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand + value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                PropertyChangedEventHandler propertyChanged = this.propertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand - value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.propertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.propertyChanged += value;
            }
            remove
            {
                this.propertyChanged -= value;
            }
        }

        public AsyncCommandBase(Func<T, Task> executeMethod) : this(executeMethod, null, nullable)
        {
        }

        public AsyncCommandBase(Func<T, Task> executeMethod, bool useCommandManager) : this(executeMethod, null, new bool?(useCommandManager))
        {
        }

        public AsyncCommandBase(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool? useCommandManager = new bool?()) : base(useCommandManager)
        {
            this.Init(executeMethod, canExecuteMethod);
        }

        private void Init(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod");
            }
            this.executeMethod = executeMethod;
            base.canExecuteMethod = canExecuteMethod;
        }

        protected void RaisePropertyChanged(string propName)
        {
            if (this.propertyChanged != null)
            {
                this.propertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}

