namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Windows;

    public class ListenerPropertyChangedEventArgs : EventArgs
    {
        private readonly DependencyObject owner;
        private readonly string path;

        public ListenerPropertyChangedEventArgs(DependencyObject owner, string path)
        {
            this.owner = owner;
            this.path = path;
        }

        public DependencyObject Owner =>
            this.owner;

        public string Path =>
            this.path;
    }
}

