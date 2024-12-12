namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Threading;

    internal static class WpfNotifyIcon
    {
        private const int WH_MOUSE_LL = 14;
        private const int WM_MOUSEMOVE = 0x200;
        private const int WM_MOUSEWHEEL = 0x20a;
        private const int WM_MOUSEHWHEEL = 0x20e;
        private const int WM_RBUTTONUP = 0x205;
        private static NotifyIcon notifyIcon = new NotifyIcon();
        private static NotifyIconHooks notifyIconHooks = new NotifyIconHooks();
        private static System.Drawing.Icon icon;
        private static DispatcherTimer clickTimer;
        private static Window attachedWindow;
        private static PopupMenu contextMenu;
        private static List<int> services = new List<int>();
        private static Dictionary<int, System.Drawing.Icon> icons = new Dictionary<int, System.Drawing.Icon>();

        static WpfNotifyIcon()
        {
            if (Icon != null)
            {
                notifyIcon.Icon = Icon;
            }
            notifyIcon.Visible = true;
            ClickTimerInitialize();
            notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(WpfNotifyIcon.OnMouseClickNotifyIcon);
        }

        private static void ClickTimerInitialize()
        {
            DispatcherTimer timer1 = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, SystemInformation.DoubleClickTime), DispatcherPriority.Background, new EventHandler(WpfNotifyIcon.OnClickTimerTick), Dispatcher.CurrentDispatcher);
            timer1.IsEnabled = false;
            clickTimer = timer1;
        }

        public static void CloseContextMenu()
        {
            ContextMenu.IsOpen = false;
        }

        private static void ExecuteCommand()
        {
            if (!clickTimer.IsEnabled)
            {
                clickTimer.Start();
            }
            else
            {
                clickTimer.Stop();
                ExecuteLeftDoubleClickCommand();
            }
        }

        private static void ExecuteLeftClickCommand()
        {
            if (LeftClickCommand != null)
            {
                if (LeftClickCommand is RoutedCommand)
                {
                    if ((LeftClickCommand as RoutedCommand).CanExecute(null, attachedWindow))
                    {
                        (LeftClickCommand as RoutedCommand).Execute(null, attachedWindow);
                    }
                }
                else if (LeftClickCommand.CanExecute(null))
                {
                    LeftClickCommand.Execute(null);
                }
            }
        }

        private static void ExecuteLeftDoubleClickCommand()
        {
            if (LeftDoubleClickCommand != null)
            {
                if (LeftDoubleClickCommand is RoutedCommand)
                {
                    if ((LeftDoubleClickCommand as RoutedCommand).CanExecute(null, attachedWindow))
                    {
                        (LeftDoubleClickCommand as RoutedCommand).Execute(null, attachedWindow);
                    }
                }
                else if (LeftDoubleClickCommand.CanExecute(null))
                {
                    LeftDoubleClickCommand.Execute(null);
                }
            }
        }

        private static System.Drawing.Icon GetDefaultWindowsAppIcon() => 
            SystemIcons.Application;

        private static void OnClickTimerTick(object sender, EventArgs e)
        {
            clickTimer.Stop();
            ExecuteLeftClickCommand();
        }

        internal static void OnClosingParentWindow()
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Icon = null;
                notifyIcon.Text = string.Empty;
                ContextMenu = null;
                LeftClickCommand = null;
                Icon = null;
            }
        }

        private static void OnContextMenuChanged(PopupMenu oldValue, PopupMenu newValue)
        {
            if (oldValue != null)
            {
                oldValue.Opened -= new EventHandler(WpfNotifyIcon.OnMenuOpened);
                oldValue.Closed -= new EventHandler(WpfNotifyIcon.OnMenuClosed);
            }
            if (newValue != null)
            {
                newValue.Opened += new EventHandler(WpfNotifyIcon.OnMenuOpened);
                newValue.Closed += new EventHandler(WpfNotifyIcon.OnMenuClosed);
            }
        }

        private static void OnMenuClosed(object sender, EventArgs e)
        {
            notifyIconHooks.UnHook();
        }

        private static void OnMenuOpened(object sender, EventArgs e)
        {
            notifyIconHooks.SetHook();
        }

        private static void OnMouseClickNotifyIcon(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseButtons button = e.Button;
            if (button == MouseButtons.Left)
            {
                ExecuteCommand();
            }
            else if (button == MouseButtons.Right)
            {
                OnRightClickNotifyIcon();
            }
        }

        internal static void OnRightClickNotifyIcon()
        {
            if ((ContextMenu != null) && !ContextMenu.IsOpen)
            {
                ContextMenu.Placement = PlacementMode.Mouse;
                ContextMenu.IsOpen = true;
            }
        }

        internal static void PushAttachedWindow(Window window)
        {
            attachedWindow = window;
        }

        public static void PushIcon(NotifyIconService notifyIconService, System.Drawing.Icon newValue)
        {
            int hashCode = notifyIconService.GetHashCode();
            bool isInDesignMode = false;
            if (notifyIconService != null)
            {
                isInDesignMode = DesignerProperties.GetIsInDesignMode(notifyIconService);
            }
            if (!(ReferenceEquals(icons, null) | isInDesignMode))
            {
                if (newValue == null)
                {
                    services.Remove(hashCode);
                    icons.Remove(hashCode);
                }
                else
                {
                    icons[hashCode] = newValue;
                    if (!services.Contains(hashCode))
                    {
                        services.Add(hashCode);
                    }
                }
                if (services.Count == 0)
                {
                    Icon = GetDefaultWindowsAppIcon();
                }
                else
                {
                    Icon = icons[services.Last<int>()];
                }
            }
        }

        public static void PushTooltip(NotifyIconService notifyIconService, string tooltip)
        {
            notifyIcon.Text = tooltip;
        }

        public static PopupMenu ContextMenu
        {
            get => 
                contextMenu;
            set
            {
                if (!ReferenceEquals(WpfNotifyIcon.contextMenu, value))
                {
                    PopupMenu contextMenu = WpfNotifyIcon.contextMenu;
                    WpfNotifyIcon.contextMenu = value;
                    OnContextMenuChanged(contextMenu, value);
                }
            }
        }

        public static ICommand LeftClickCommand { get; set; }

        public static ICommand LeftDoubleClickCommand { get; set; }

        private static System.Drawing.Icon Icon
        {
            get => 
                icon;
            set
            {
                icon = value;
                if (notifyIcon != null)
                {
                    notifyIcon.Icon = value;
                    notifyIcon.Visible = true;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Windows.Point pt) : this((int) pt.X, (int) pt.Y)
            {
            }

            public static implicit operator System.Windows.Point(WpfNotifyIcon.POINT p) => 
                new System.Windows.Point((double) p.X, (double) p.Y);

            public static implicit operator WpfNotifyIcon.POINT(System.Windows.Point p) => 
                new WpfNotifyIcon.POINT(p);
        }
    }
}

