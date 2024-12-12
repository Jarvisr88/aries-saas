namespace DevExpress.Office.Commands.Internal
{
    using DevExpress.Office.Utils;
    using System;

    public class ClipboardPasteSource : PasteSource
    {
        public override bool ContainsData(string format, bool autoConvert) => 
            OfficeClipboard.ContainsData(format, autoConvert);

        public override object GetData(string format, bool autoConvert) => 
            OfficeClipboard.GetDataObject()?.GetData(format, autoConvert);
    }
}

