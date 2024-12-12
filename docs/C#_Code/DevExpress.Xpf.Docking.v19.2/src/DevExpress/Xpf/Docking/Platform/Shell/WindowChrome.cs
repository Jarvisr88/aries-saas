namespace DevExpress.Xpf.Docking.Platform.Shell
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    internal class WindowChrome : Freezable
    {
        public static readonly DependencyProperty WindowChromeProperty = DependencyProperty.RegisterAttached("WindowChrome", typeof(WindowChrome), typeof(WindowChrome), new PropertyMetadata(null, new PropertyChangedCallback(WindowChrome.OnChromeChanged)));
        public static readonly DependencyProperty OverlapTaskbarProperty = DependencyProperty.RegisterAttached("OverlapTaskbar", typeof(bool), typeof(WindowChrome), new PropertyMetadata(false));

        protected override Freezable CreateInstanceCore() => 
            new WindowChrome();

        public static bool GetOverlapTaskbar(Window window) => 
            (bool) window.GetValue(OverlapTaskbarProperty);

        public static WindowChrome GetWindowChrome(Window window) => 
            (WindowChrome) window.GetValue(WindowChromeProperty);

        private static void OnChromeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                Window window = (Window) d;
                WindowChrome newValue = (WindowChrome) e.NewValue;
                WindowChromeWorker windowChromeWorker = WindowChromeWorker.GetWindowChromeWorker(window);
                if (windowChromeWorker == null)
                {
                    windowChromeWorker = new WindowChromeWorker();
                    WindowChromeWorker.SetWindowChromeWorker(window, windowChromeWorker);
                }
                windowChromeWorker.SetWindowChrome(newValue);
            }
        }

        public static void SetOverlapTaskbar(Window window, bool overlapTaskbar)
        {
            window.SetValue(OverlapTaskbarProperty, overlapTaskbar);
        }

        public static void SetWindowChrome(Window window, WindowChrome chrome)
        {
            window.SetValue(WindowChromeProperty, chrome);
        }
    }
}

