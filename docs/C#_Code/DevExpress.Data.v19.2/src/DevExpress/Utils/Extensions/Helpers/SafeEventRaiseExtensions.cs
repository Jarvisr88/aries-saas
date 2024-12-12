namespace DevExpress.Utils.Extensions.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public static class SafeEventRaiseExtensions
    {
        public static void SafeRaise(this EventHandler eventHandler, object sender)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, EventArgs.Empty);
            }
        }

        public static void SafeRaise<THandler, TArgs>(this WeakEventHandler<TArgs, THandler> eventHandler, object sender, TArgs e) where TArgs: EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler.Raise(sender, e);
            }
        }

        public static void SafeRaise(this PropertyChangedEventHandler eventHandler, object sender, PropertyChangedEventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void SafeRaise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void SafeRaise<T>(this EventHandler<T> eventHandler, object sender, T e) where T: EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }
    }
}

