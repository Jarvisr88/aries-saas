namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlsTableRow : XlRow
    {
        private readonly List<XlCell> cells;

        public XlsTableRow(IXlExport exporter) : base(exporter)
        {
            this.cells = new List<XlCell>();
            this.AutomaticHeightInPixels = -1;
        }

        public bool IsDefault() => 
            ((base.Formatting == null) && ((base.HeightInPixels < 0) && (!base.IsCollapsed && !base.IsHidden))) && (this.Cells.Count <= 0);

        public IList<XlCell> Cells =>
            this.cells;

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex { get; set; }

        public int AutomaticHeightInPixels { get; set; }
    }
}

