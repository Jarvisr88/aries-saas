namespace DevExpress.Mvvm
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    internal class CommandManagerHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void InvalidateRequerySuggested()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Subscribe(EventHandler canExecuteChangedHandler)
        {
            CommandManager.RequerySuggested += canExecuteChangedHandler;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Unsubscribe(EventHandler canExecuteChangedHandler)
        {
            CommandManager.RequerySuggested -= canExecuteChangedHandler;
        }
    }
}

