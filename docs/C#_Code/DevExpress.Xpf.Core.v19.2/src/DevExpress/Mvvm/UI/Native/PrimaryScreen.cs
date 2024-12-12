namespace DevExpress.Mvvm.UI.Native
{
    using Microsoft.Win32;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;

    public class PrimaryScreen : IScreen
    {
        private static event EventHandler<EventArgs> DisplaySettingsChanged;

        public event Action WorkingAreaChanged;

        static PrimaryScreen()
        {
            SystemEvents.DisplaySettingsChanged += new EventHandler(PrimaryScreen.SystemEvents_DisplaySettingsChanged);
        }

        public PrimaryScreen()
        {
            UnregisterCallback<EventArgs> unregister = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                UnregisterCallback<EventArgs> local1 = <>c.<>9__5_1;
                unregister = <>c.<>9__5_1 = delegate (EventHandler<EventArgs> handler) {
                    DisplaySettingsChanged -= handler;
                };
            }
            DisplaySettingsChanged += AnotherEventHandlerUtils.MakeWeak<EventArgs>(delegate (object s, EventArgs e) {
                if (this.WorkingAreaChanged != null)
                {
                    this.WorkingAreaChanged();
                }
            }, unregister);
        }

        public static System.Windows.Point GetDpi()
        {
            System.Windows.Point point;
            using (TextBox box = new TextBox())
            {
                using (Graphics graphics = box.CreateGraphics())
                {
                    point = new System.Windows.Point((double) (graphics.DpiX / 96f), (double) (graphics.DpiY / 96f));
                }
            }
            return point;
        }

        public Rect GetWorkingArea(System.Windows.Point point)
        {
            System.Windows.Point dpi = GetDpi();
            Rectangle workingArea = Screen.GetWorkingArea(new System.Drawing.Point(((int) point.X) + 1, ((int) point.Y) + 1));
            return new Rect(((double) workingArea.X) / dpi.X, ((double) workingArea.Y) / dpi.Y, ((double) workingArea.Width) / dpi.X, ((double) workingArea.Height) / dpi.Y);
        }

        private static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            if (DisplaySettingsChanged != null)
            {
                DisplaySettingsChanged(sender, e);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrimaryScreen.<>c <>9 = new PrimaryScreen.<>c();
            public static UnregisterCallback<EventArgs> <>9__5_1;

            internal void <.ctor>b__5_1(EventHandler<EventArgs> handler)
            {
                PrimaryScreen.DisplaySettingsChanged -= handler;
            }
        }
    }
}

