namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class CopyingToClipboardEventArgsBase : RoutedEventArgs
    {
        private readonly IEnumerable<int> rowHandles;
        private readonly bool copyHeader;
        private readonly DataViewBase view;

        public CopyingToClipboardEventArgsBase(DataViewBase view, IEnumerable<int> rowHandles, bool copyHeader)
        {
            this.rowHandles = rowHandles;
            this.copyHeader = copyHeader;
            this.view = view;
        }

        public IEnumerable<int> RowHandles =>
            this.rowHandles;

        public bool CopyHeader =>
            this.copyHeader;

        public DataViewBase Source =>
            this.view;
    }
}

