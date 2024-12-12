namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class DelegateCommandBase<T> : CommandBase<T>
    {
        protected Action<T> executeMethod;

        public DelegateCommandBase(Action<T> executeMethod) : this(executeMethod, null, nullable)
        {
        }

        public DelegateCommandBase(Action<T> executeMethod, bool useCommandManager) : this(executeMethod, null, new bool?(useCommandManager))
        {
        }

        public DelegateCommandBase(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool? useCommandManager = new bool?()) : base(useCommandManager)
        {
            this.Init(executeMethod, canExecuteMethod);
        }

        private void Init(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod");
            }
            this.executeMethod = executeMethod;
            base.canExecuteMethod = canExecuteMethod;
        }
    }
}

