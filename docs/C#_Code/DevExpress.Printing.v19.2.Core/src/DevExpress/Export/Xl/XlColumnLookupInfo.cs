namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlColumnLookupInfo
    {
        public XlCellRange Range { get; set; }

        public int ResultColumn { get; set; }

        public bool ApproximateMatch { get; set; }
    }
}

