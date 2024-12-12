namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class OverlayWindow : DXWindow
    {
        public OverlayWindow()
        {
            base.SizeToContent = SizeToContent.WidthAndHeight;
            base.ShowInTaskbar = false;
            base.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            base.ResizeMode = ResizeMode.NoResize;
        }

        private void HostContent_Resized(object sender, EventArgs e)
        {
            this.Stretch();
        }

        protected void OnClosed()
        {
        }

        protected void OnOpened()
        {
            base.Left = 0.0;
            base.Top = 0.0;
            this.Stretch();
        }

        private void Stretch()
        {
            base.Width = Application.Current.MainWindow.Width;
            base.Height = Application.Current.MainWindow.Height;
        }
    }
}

