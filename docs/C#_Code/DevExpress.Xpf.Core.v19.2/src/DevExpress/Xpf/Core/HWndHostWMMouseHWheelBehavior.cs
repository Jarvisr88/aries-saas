namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;

    public class HWndHostWMMouseHWheelBehavior : Behavior<FrameworkElement>
    {
        public static readonly RoutedEvent PreviewInputReportEvent = EventManager.RegisterRoutedEvent("PreviewInputReport", RoutingStrategy.Tunnel, typeof(EventHandler), typeof(HWndHostWMMouseHWheelBehavior));
        private HwndSource source;

        static HWndHostWMMouseHWheelBehavior()
        {
            InputManager.Current.PreProcessInput += new PreProcessInputEventHandler(HWndHostWMMouseHWheelBehavior.CurrentPreProcessInput);
        }

        private static void CurrentPreProcessInput(object sender, PreProcessInputEventArgs e)
        {
            if ((e.StagingItem.Input.Device != null) && (e.StagingItem.Input.Device is MouseDevice))
            {
                StagingAreaInputItem stagingItem = e.StagingItem;
                DXInputReportEventArgs input = e.StagingItem.Input as DXInputReportEventArgs;
                if ((input != null) && !input.Handled)
                {
                    e.Cancel();
                    MouseWheelEventArgsEx ex = new MouseWheelEventArgsEx((MouseDevice) stagingItem.Input.Device, stagingItem.Input.Timestamp, input.Report.Wheel, 0) {
                        RoutedEvent = Mouse.PreviewMouseWheelEvent
                    };
                    e.PushInput(ex, e.StagingItem);
                }
            }
        }

        private IntPtr Filter(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr zero = IntPtr.Zero;
            if (msg == 0x20e)
            {
                int wheel = DevExpress.Xpf.Core.NativeMethods.SignedHIWORD(DevExpress.Xpf.Core.NativeMethods.IntPtrToInt32(wParam));
                DevExpress.Xpf.Core.NativeMethods.POINT pt = new DevExpress.Xpf.Core.NativeMethods.POINT(DevExpress.Xpf.Core.NativeMethods.SignedLOWORD(DevExpress.Xpf.Core.NativeMethods.IntPtrToInt32(lParam)), DevExpress.Xpf.Core.NativeMethods.SignedHIWORD(DevExpress.Xpf.Core.NativeMethods.IntPtrToInt32(lParam)));
                if (!this.ScreenToClient(hwnd, ref pt))
                {
                    throw new Win32Exception();
                }
                int messageTime = this.GetMessageTime();
                handled = this.ReportInput(InputMode.Foreground, messageTime, pt.x, pt.y, wheel);
            }
            return zero;
        }

        [SecuritySafeCritical]
        private int GetMessageTime() => 
            DevExpress.Xpf.Core.NativeMethods.GetMessageTime();

        protected override void OnAttached()
        {
            base.OnAttached();
            this.source = (HwndSource) PresentationSource.FromVisual(base.AssociatedObject);
            this.source.Do<HwndSource>(x => x.AddHook(new HwndSourceHook(this.Filter)));
        }

        protected override void OnDetaching()
        {
            this.source.Do<HwndSource>(x => x.RemoveHook(new HwndSourceHook(this.Filter)));
            this.source = null;
            base.OnDetaching();
        }

        private bool ReportInput(InputMode inputMode, int timeStamp, int x, int y, int wheel)
        {
            MouseDevice primaryDevice = Mouse.PrimaryDevice;
            if ((primaryDevice == null) || (primaryDevice.ActiveSource == null))
            {
                return false;
            }
            PresentationSource activeSource = primaryDevice.ActiveSource;
            if ((activeSource == null) || activeSource.IsDisposed)
            {
                return false;
            }
            InputEventArgs input = new DXInputReportEventArgs(primaryDevice, new DXInputReport(activeSource, inputMode, timeStamp, x, y, wheel)) {
                RoutedEvent = PreviewInputReportEvent
            };
            return InputManager.Current.ProcessInput(input);
        }

        [SecuritySafeCritical]
        private bool ScreenToClient(IntPtr hwnd, ref DevExpress.Xpf.Core.NativeMethods.POINT pt) => 
            DevExpress.Xpf.Core.NativeMethods.ScreenToClient(new HandleRef(this, hwnd).Handle, ref pt);

        public class DXInputReport
        {
            private readonly int x;
            private readonly int y;
            private readonly int wheel;
            private readonly PresentationSource inputSource;
            private readonly InputType type;
            private readonly InputMode mode;
            private readonly int timestamp;

            public DXInputReport(PresentationSource inputSource, InputMode mode, int timestamp, int x, int y, int wheel)
            {
                this.mode = mode;
                this.timestamp = timestamp;
                this.x = x;
                this.y = y;
                this.type = InputType.Mouse;
                this.wheel = wheel;
                this.inputSource = inputSource;
            }

            public int X =>
                this.x;

            public int Y =>
                this.y;

            public int Wheel =>
                this.wheel;

            public Visual RootVisual
            {
                get
                {
                    Func<PresentationSource, Visual> evaluator = <>c.<>9__14_0;
                    if (<>c.<>9__14_0 == null)
                    {
                        Func<PresentationSource, Visual> local1 = <>c.<>9__14_0;
                        evaluator = <>c.<>9__14_0 = i => i.RootVisual;
                    }
                    return this.inputSource.With<PresentationSource, Visual>(evaluator);
                }
            }

            public InputType Type =>
                this.type;

            public InputMode Mode =>
                this.mode;

            public int Timestamp =>
                this.timestamp;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly HWndHostWMMouseHWheelBehavior.DXInputReport.<>c <>9 = new HWndHostWMMouseHWheelBehavior.DXInputReport.<>c();
                public static Func<PresentationSource, Visual> <>9__14_0;

                internal Visual <get_RootVisual>b__14_0(PresentationSource i) => 
                    i.RootVisual;
            }
        }

        public class DXInputReportEventArgs : InputEventArgs
        {
            public DXInputReportEventArgs(MouseDevice inputDevice, HWndHostWMMouseHWheelBehavior.DXInputReport report) : base(inputDevice, report.Timestamp)
            {
                this.Report = report;
            }

            public HWndHostWMMouseHWheelBehavior.DXInputReport Report { get; private set; }
        }
    }
}

