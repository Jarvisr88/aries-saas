namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    public class ClipboardSourceFactory<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public static IClipboardSource CreateSource(IClipboardPasteHelper<TCol, TRow> helper)
        {
            foreach (ClipboardSourceType type in ClipboardSourceFactory<TCol, TRow>.SourcePriority)
            {
                switch (type)
                {
                    case ClipboardSourceType.Excel:
                        if (!Clipboard.ContainsData("Biff8"))
                        {
                            break;
                        }
                        return new ExcelClipboardSource<TCol, TRow>("Biff8", helper);

                    case ClipboardSourceType.Csv:
                        if (!Clipboard.ContainsData(DataFormats.CommaSeparatedValue) || !(Clipboard.GetData(DataFormats.CommaSeparatedValue) is MemoryStream))
                        {
                            break;
                        }
                        return new ExcelClipboardSource<TCol, TRow>(DataFormats.CommaSeparatedValue, helper);

                    case ClipboardSourceType.XMLSpreadsheet:
                        if (!Clipboard.ContainsData("XML Spreadsheet"))
                        {
                            break;
                        }
                        return new XmlClipboardSource();

                    case ClipboardSourceType.UnicodeText:
                        if (!Clipboard.ContainsText(TextDataFormat.UnicodeText))
                        {
                            break;
                        }
                        return new ExcelClipboardSource<TCol, TRow>("UnicodeText", helper);

                    case ClipboardSourceType.Text:
                        if (!Clipboard.ContainsText(TextDataFormat.Text))
                        {
                            break;
                        }
                        return new ExcelClipboardSource<TCol, TRow>("Text", helper);

                    default:
                        break;
                }
            }
            return null;
        }

        [Browsable(false)]
        public static ClipboardSourceType[] SourcePriority
        {
            get => 
                ClipboardSourceFactory.SourcePriority;
            set => 
                ClipboardSourceFactory.SourcePriority = value;
        }
    }
}

