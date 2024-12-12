namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal class XlExpressionContext : IXlExpressionContextEx, IXlExpressionContext
    {
        private readonly Dictionary<string, XlColumnLookupInfo> lookupColumns;

        public XlExpressionContext()
        {
            this.ReferenceMode = XlCellReferenceMode.Offset;
            this.MaxColumnCount = 0x4000;
            this.MaxRowCount = 0x100000;
            this.lookupColumns = new Dictionary<string, XlColumnLookupInfo>();
        }

        public XlCellPosition CurrentCell { get; set; }

        public string CurrentSheetName { get; set; }

        public XlCellReferenceMode ReferenceMode { get; set; }

        public XlCellReferenceStyle ReferenceStyle =>
            XlCellReferenceStyle.A1;

        public int MaxColumnCount { get; set; }

        public int MaxRowCount { get; set; }

        public XlExpressionStyle ExpressionStyle { get; set; }

        public CultureInfo Culture =>
            CultureInfo.InvariantCulture;

        public int RowOffset { get; set; }

        public IXlTable CurrentTable { get; set; }

        public Dictionary<string, XlColumnLookupInfo> LookupColumns =>
            this.lookupColumns;
    }
}

