namespace DevExpress.Xpf.Core.Internal
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;

    public static class InputManagerHelper
    {
        private static readonly Func<System.Windows.Point, PresentationSource, System.Windows.Point> ScreenToClient;
        private static readonly System.Type tRawMouseInputReport = typeof(Mouse).Assembly.GetType("System.Windows.Input.RawMouseInputReport");
        private static readonly System.Type tRawKeyboardInputReport = typeof(Mouse).Assembly.GetType("System.Windows.Input.RawKeyboardInputReport");
        private static readonly System.Type tPointUtil = typeof(Colors).Assembly.GetType("MS.Internal.PointUtil");
        private static readonly System.Type tInputReportEventArgs = typeof(Mouse).Assembly.GetType("System.Windows.Input.InputReportEventArgs");
        private static readonly object eRawMouseActions_Activate = Enum.Parse(typeof(Mouse).Assembly.GetType("System.Windows.Input.RawMouseActions"), "Activate");
        private static readonly object eRawKeyboardActions_Activate = Enum.Parse(typeof(Mouse).Assembly.GetType("System.Windows.Input.RawKeyboardActions"), "Activate");
        private static readonly RoutedEvent PreviewInputReportEvent;

        static InputManagerHelper()
        {
            int? parametersCount = null;
            ScreenToClient = ReflectionHelper.CreateInstanceMethodHandler<Func<System.Windows.Point, PresentationSource, System.Windows.Point>>(null, "ScreenToClient", BindingFlags.NonPublic | BindingFlags.Static, tPointUtil, parametersCount, null, true);
            PreviewInputReportEvent = (RoutedEvent) typeof(InputManager).GetField("PreviewInputReportEvent", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
        }

        public static void UpdateKeyboardActiveSource(Func<PresentationSource> get_source)
        {
            PresentationSource source = get_source();
            if ((source != null) && !source.IsDisposed)
            {
                object[] objArray1 = new object[9];
                objArray1[0] = source;
                objArray1[1] = InputMode.Foreground;
                objArray1[2] = Environment.TickCount;
                objArray1[3] = eRawKeyboardActions_Activate;
                objArray1[4] = 0;
                objArray1[5] = false;
                objArray1[6] = false;
                objArray1[7] = 0;
                objArray1[8] = IntPtr.Zero;
                object obj2 = Activator.CreateInstance(tRawKeyboardInputReport, objArray1);
                object[] objArray2 = new object[] { Keyboard.PrimaryDevice, obj2 };
                InputEventArgs input = (InputEventArgs) Activator.CreateInstance(tInputReportEventArgs, objArray2);
                input.RoutedEvent = PreviewInputReportEvent;
                InputManager.Current.ProcessInput(input);
            }
        }

        public static void UpdateMouseActiveSource(Func<PresentationSource> get_source, System.Windows.Point? position)
        {
            Func<MouseDevice, PresentationSource> evaluator = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<MouseDevice, PresentationSource> local1 = <>c.<>9__10_0;
                evaluator = <>c.<>9__10_0 = x => x.ActiveSource;
            }
            PresentationSource source = Mouse.PrimaryDevice.With<MouseDevice, PresentationSource>(evaluator);
            if ((source != null) && source.IsDisposed)
            {
                PresentationSource source2 = get_source();
                if ((source2 != null) && !source2.IsDisposed)
                {
                    System.Drawing.Point point = System.Windows.Forms.Cursor.Position;
                    System.Windows.Point? nullable = position;
                    System.Windows.Point point2 = (nullable != null) ? nullable.GetValueOrDefault() : ScreenToClient(new System.Windows.Point((double) point.X, (double) point.Y), source2);
                    object[] objArray1 = new object[] { InputMode.Foreground, Environment.TickCount, source2, eRawMouseActions_Activate, (int) point2.X, (int) point2.Y, 0, IntPtr.Zero };
                    object obj2 = Activator.CreateInstance(tRawMouseInputReport, objArray1);
                    object[] objArray2 = new object[] { Mouse.PrimaryDevice, obj2 };
                    InputEventArgs input = (InputEventArgs) Activator.CreateInstance(tInputReportEventArgs, objArray2);
                    input.RoutedEvent = PreviewInputReportEvent;
                    InputManager.Current.ProcessInput(input);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InputManagerHelper.<>c <>9 = new InputManagerHelper.<>c();
            public static Func<MouseDevice, PresentationSource> <>9__10_0;

            internal PresentationSource <UpdateMouseActiveSource>b__10_0(MouseDevice x) => 
                x.ActiveSource;
        }
    }
}

