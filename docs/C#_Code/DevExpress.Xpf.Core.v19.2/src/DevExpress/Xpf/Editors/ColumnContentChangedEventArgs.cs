namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class ColumnContentChangedEventArgs : EventArgs
    {
        private readonly DependencyProperty property;

        public ColumnContentChangedEventArgs(DependencyProperty property)
        {
            this.property = property;
        }

        public DependencyProperty Property =>
            this.property;
    }
}

