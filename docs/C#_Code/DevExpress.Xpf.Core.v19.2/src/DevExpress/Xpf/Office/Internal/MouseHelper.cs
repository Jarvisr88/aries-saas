namespace DevExpress.Xpf.Office.Internal
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    public static class MouseHelper
    {
        private static int Timeout = 500;
        private static bool clicked = false;
        private static Point position;

        public static bool IsDoubleClick(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(null);
            if (clicked)
            {
                clicked = false;
                return MouseHelper.position.Equals(position);
            }
            clicked = true;
            MouseHelper.position = position;
            new Thread(new ParameterizedThreadStart(MouseHelper.ResetThread)).Start();
            return false;
        }

        private static void ResetThread(object state)
        {
            Thread.Sleep(Timeout);
            clicked = false;
        }
    }
}

