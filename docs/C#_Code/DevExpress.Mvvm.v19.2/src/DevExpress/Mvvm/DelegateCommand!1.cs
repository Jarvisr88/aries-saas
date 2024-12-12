namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public class DelegateCommand<T> : DelegateCommandBase<T>
    {
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, null, nullable)
        {
        }

        public DelegateCommand(Action<T> executeMethod, bool useCommandManager) : this(executeMethod, null, new bool?(useCommandManager))
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool? useCommandManager = new bool?()) : base(executeMethod, canExecuteMethod, useCommandManager)
        {
        }

        public override void Execute(T parameter)
        {
            if (this.CanExecute(parameter) && (base.executeMethod != null))
            {
                base.executeMethod(parameter);
            }
        }
    }
}

