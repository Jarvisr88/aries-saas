namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.InteropServices;

    public class AsyncCommand : AsyncCommand<object>
    {
        public AsyncCommand(Func<Task> executeMethod) : this(executeMethod, null, false, nullable)
        {
        }

        public AsyncCommand(Func<Task> executeMethod, bool useCommandManager) : this(executeMethod, null, false, new bool?(useCommandManager))
        {
        }

        public AsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod, bool? useCommandManager = new bool?()) : this(executeMethod, canExecuteMethod, false, useCommandManager)
        {
        }

        public AsyncCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod, bool allowMultipleExecution, bool? useCommandManager = new bool?()) : this((executeMethod != null) ? o => executeMethod() : null, (canExecuteMethod != null) ? o => canExecuteMethod() : null, allowMultipleExecution, useCommandManager)
        {
        }
    }
}

