namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XlGroup
    {
        public int GroupRowHandle { get; set; }
        public string GroupFieldName { get; set; }
        public IXlGroup Group { get; set; }
        public int GroupId { get; set; }
        public int StartGroup { get; set; }
        public List<DevExpress.Printing.ExportHelpers.Group> DataRanges { get; set; }
        public bool ShowFooter { get; set; }
        public int GroupRowLevel { get; set; }
    }
}

