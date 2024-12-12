namespace DevExpress.XtraEditors
{
    using DevExpress.XtraEditors.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class FilterChangedEventArgs : EventArgs
    {
        public FilterChangedEventArgs(FilterChangedAction action, Node node);
        public override string ToString();

        public FilterChangedAction Action { get; private set; }

        public Node CurrentNode { get; private set; }
    }
}

