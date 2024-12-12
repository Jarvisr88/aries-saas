namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class DockBarContainerControlBase : BarContainerControl
    {
        internal DockBarContainerControlBase()
        {
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected virtual void OnLoaded()
        {
        }

        protected sealed override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            this.OnLoaded();
        }

        protected virtual void OnUnloaded()
        {
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.OnUnloaded();
        }

        [Obsolete("Use BarManager.Bars property instead.")]
        public ItemCollection Items =>
            null;
    }
}

