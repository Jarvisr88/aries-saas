namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ClipboardSourceFactory
    {
        static ClipboardSourceFactory()
        {
            SourcePriority = new ClipboardSourceType[] { ClipboardSourceType.Excel, ClipboardSourceType.Csv, ClipboardSourceType.XMLSpreadsheet, ClipboardSourceType.UnicodeText, ClipboardSourceType.Text };
        }

        public static ClipboardSourceType[] SourcePriority { get; set; }
    }
}

