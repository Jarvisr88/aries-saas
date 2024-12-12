namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DragWidgetWindow : Window, IDisposable
    {
        public DragWidgetWindow();
        public void Dispose();
        public void Initialize(FrameworkElement dragObject);
        protected override void OnSourceInitialized(EventArgs e);
        public void UpdateWindowLocation();

        public FrameworkElement DragObject { get; private set; }

        public Point OffsetDifference { get; set; }
    }
}

