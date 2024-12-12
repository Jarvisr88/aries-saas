namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action executeMethod) : this(executeMethod, null, nullable)
        {
        }

        public DelegateCommand(Action executeMethod, bool useCommandManager) : this(executeMethod, null, new bool?(useCommandManager))
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod, bool? useCommandManager = new bool?()) : this((executeMethod != null) ? o => executeMethod() : null, (canExecuteMethod != null) ? o => canExecuteMethod() : null, useCommandManager)
        {
        }
    }
}

