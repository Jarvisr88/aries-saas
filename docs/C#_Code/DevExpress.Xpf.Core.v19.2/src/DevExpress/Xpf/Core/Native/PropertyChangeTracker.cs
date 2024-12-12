namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class PropertyChangeTracker
    {
        private static readonly Func<DependencyObject, DependencyProperty, object> createTracker;
        private static readonly Func<object, bool> getCanClose;
        private static readonly Action<object> close;
        private static readonly Action<object, EventHandler> setChanged;
        private object tracker;
        private EventHandler changed;

        public event EventHandler Changed;

        static PropertyChangeTracker();
        public PropertyChangeTracker(DependencyObject obj, DependencyProperty property);
        public void Close();
        private void OnChanged(object sender, EventArgs e);

        public bool CanClose { get; }
    }
}

