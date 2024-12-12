namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Windows;

    public class PixelSnapper : PixelSnapperBase
    {
        public PixelSnapper()
        {
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnIsVisibleChanged);
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ResetTopElement();
        }
    }
}

