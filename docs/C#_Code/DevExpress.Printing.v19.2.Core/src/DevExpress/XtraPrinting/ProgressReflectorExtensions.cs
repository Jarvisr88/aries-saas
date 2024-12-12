namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class ProgressReflectorExtensions
    {
        public static void Do(this ProgressReflector me, int rangeMaximum, Action action)
        {
            me.InitializeRange(rangeMaximum);
            try
            {
                action();
            }
            finally
            {
                me.MaximizeRange();
            }
        }
    }
}

