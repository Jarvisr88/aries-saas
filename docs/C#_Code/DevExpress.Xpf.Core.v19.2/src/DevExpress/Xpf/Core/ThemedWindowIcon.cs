namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class ThemedWindowIcon : ContentControl
    {
        private DispatcherTimer waitTimer;

        static ThemedWindowIcon()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowIcon), new FrameworkPropertyMetadata(typeof(ThemedWindowIcon)));
        }

        private void DoubleClick(ThemedWindowIcon icon)
        {
            if (icon != null)
            {
                icon.ForWindowFromTemplate(new Action<Window>(WindowSystemCommands.CloseWindow));
            }
        }

        private Point GetMenuScreenLocation(Window window, ThemedWindowIcon icon)
        {
            Rect bounds = icon.GetBounds();
            DevExpress.Xpf.Core.Native.DpiScale dpi = WindowUtility.GetDpi(window);
            return DevExpress.Xpf.Core.Native.DpiHelper.DevicePixelsToLogical(icon.PointToScreen(new Point(bounds.CenterX(), bounds.Top)), dpi.DpiScaleX, dpi.DpiScaleY);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DispatcherTimer timer1 = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, SystemInformation.DoubleClickTime), DispatcherPriority.Background, new EventHandler(this.WaitTimerTick), Dispatcher.CurrentDispatcher);
            timer1.IsEnabled = false;
            this.waitTimer = timer1;
            this.SubscribeWindowIconClicks();
        }

        private void OnIconLeftButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.waitTimer.IsEnabled)
            {
                this.waitTimer.Start();
            }
            else
            {
                this.waitTimer.Stop();
                this.DoubleClick(sender as ThemedWindowIcon);
            }
        }

        private void SingleClick(ThemedWindowIcon icon)
        {
            if (icon == null)
            {
                ThemedWindowIcon local1 = icon;
            }
            else
            {
                icon.ForWindowFromTemplate(window => WindowSystemCommands.ShowSystemMenu(window, this.GetMenuScreenLocation(window, icon)));
            }
        }

        private void SubscribeWindowIconClicks()
        {
            base.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnIconLeftButtonClick);
        }

        private void WaitTimerTick(object sender, EventArgs e)
        {
            this.waitTimer.Stop();
            this.SingleClick(this);
        }
    }
}

