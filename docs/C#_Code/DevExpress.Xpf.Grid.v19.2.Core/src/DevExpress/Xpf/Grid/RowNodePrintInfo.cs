namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RowNodePrintInfo
    {
        public int NextNodeLevel { get; set; }

        public DevExpress.Xpf.Grid.RowPosition RowPosition { get; set; }

        public int ListIndex { get; set; }

        public int PrevRowHandle { get; set; }

        public int NextRowHandle { get; set; }

        public DevExpress.Xpf.Grid.RowPosition PrevRowPosition { get; set; }

        public bool IsSelected { get; set; }

        public Dictionary<ColumnBase, int> MergeValues { get; set; }

        public bool IsLast { get; set; }

        internal int Index { get; set; }
    }
}

