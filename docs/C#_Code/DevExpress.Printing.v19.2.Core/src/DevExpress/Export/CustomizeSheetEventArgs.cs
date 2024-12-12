namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeSheetEventArgs
    {
        public ISheetCustomizationContext ExportContext { get; set; }

        public IXlSheet Sheet { get; internal set; }
    }
}

