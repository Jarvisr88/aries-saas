namespace DevExpress.XtraExport.Xls
{
    using System;

    public enum XlsSubstreamType
    {
        WorkbookGlobals = 5,
        VisualBasicModule = 6,
        Sheet = 0x10,
        Chart = 0x20,
        MacroSheet = 0x40,
        Workspace = 0x100
    }
}

