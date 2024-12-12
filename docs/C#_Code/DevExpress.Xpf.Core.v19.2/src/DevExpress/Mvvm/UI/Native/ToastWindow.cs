namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Markup;

    public class ToastWindow : Window, IComponentConnector
    {
        private const int WS_EX_TOOLWINDOW = 0x80;
        private bool _contentLoaded;

        public ToastWindow()
        {
            base.Loaded += new RoutedEventHandler(this.ToastWindow_Loaded);
            this.InitializeComponent();
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/mvvm.ui/services/notificationservice/customtoastnotifications/toastwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        [SecuritySafeCritical]
        private void ToastWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            int dwNewLong = GetWindowLong(helper.Handle, -20) | 0x80;
            SetWindowLong(helper.Handle, -20, dwNewLong);
        }
    }
}

