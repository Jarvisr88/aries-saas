namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class MouseWheelEventArgsHelper
    {
        public static void SetHandled(this MouseWheelEventArgsEx args, bool value)
        {
            if (args != null)
            {
                MouseWheelEventArgsWrapper wrapper = args as MouseWheelEventArgsWrapper;
                if (wrapper != null)
                {
                    wrapper.Handled = value;
                }
                else
                {
                    args.Handled = value;
                }
            }
        }
    }
}

