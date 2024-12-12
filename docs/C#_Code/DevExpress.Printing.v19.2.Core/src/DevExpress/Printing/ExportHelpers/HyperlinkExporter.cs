namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal class HyperlinkExporter
    {
        public static bool CanExportHyperlink(bool isEventHandled, string hyperlink, ColumnEditTypes columnEditType, DefaultBoolean allowHyperLinks) => 
            !isEventHandled ? ((allowHyperLinks != DefaultBoolean.False) && ((columnEditType == ColumnEditTypes.Hyperlink) && !string.IsNullOrEmpty(hyperlink))) : !string.IsNullOrEmpty(hyperlink);

        private static bool CheckCellValueEmpty(IXlCell cell) => 
            cell.Value.IsEmpty || (cell.Value.IsText && string.IsNullOrEmpty(cell.Value.TextValue));

        private static bool Find(IList<XlHyperlink> hyperlinks, IXlCell cell, string displayValue, string targetUri, bool isCellCustomDisplayText)
        {
            bool flag = false;
            int num = 0;
            while (true)
            {
                if (num < hyperlinks.Count)
                {
                    XlHyperlink hyperlink = hyperlinks[num];
                    if (!IsCoincidence(hyperlink.Reference, cell))
                    {
                        num++;
                        continue;
                    }
                    SetData(cell, hyperlink, displayValue, targetUri, isCellCustomDisplayText);
                    flag = true;
                }
                return flag;
            }
        }

        private static object GetCellValue(IXlCell cell)
        {
            switch (cell.Value.Type)
            {
                case XlVariantValueType.Boolean:
                    return cell.Value.BooleanValue;

                case XlVariantValueType.Text:
                    return cell.Value.TextValue;

                case XlVariantValueType.Numeric:
                    return cell.Value.NumericValue;

                case XlVariantValueType.DateTime:
                    return cell.Value.DateTimeValue;
            }
            return string.Empty;
        }

        private static void GetHyperlinkCellText(IXlCell cell, string textFormat)
        {
            if (!string.IsNullOrEmpty(textFormat))
            {
                if (!string.IsNullOrEmpty(cell.Formatting.NetFormatString))
                {
                    textFormat = cell.Formatting.NetFormatString;
                }
                string str = string.Format(textFormat, GetCellValue(cell));
                if (str != textFormat)
                {
                    cell.Value = XlVariantValue.FromObject(str);
                    cell.Formatting.NetFormatString = string.Empty;
                }
            }
        }

        private static bool IsCoincidence(XlCellRange range, IXlCell cell) => 
            (range.TopLeft.Column == cell.ColumnIndex) && ((range.TopLeft.Row == cell.RowIndex) && ((range.BottomRight.Column == cell.ColumnIndex) && (range.BottomRight.Row == cell.RowIndex)));

        private static string ReplaceInvalidChars(string input)
        {
            if (input.IndexOf('\0') >= 0)
            {
                input = input.Replace("\0", " ");
            }
            return input;
        }

        private static void SetData(IXlCell cell, XlHyperlink hyperlink, string displayValue, string targetUri, bool isCellCustomDisplayText)
        {
            string str = ReplaceInvalidChars(!string.IsNullOrEmpty(displayValue) ? displayValue : targetUri);
            hyperlink.DisplayText = str;
            hyperlink.TargetUri = targetUri;
            hyperlink.Tooltip = str;
            if (isCellCustomDisplayText || (CheckCellValueEmpty(cell) && !string.IsNullOrEmpty(str)))
            {
                cell.Value = XlVariantValue.FromObject(str);
            }
        }

        public static void SetHyperlink(string targetUri, string displayText, string textFormat, IXlCell cell, IXlSheet sheet, bool isCellCustomDisplayText = false)
        {
            if (!string.IsNullOrEmpty(targetUri) && !Find(sheet.Hyperlinks, cell, displayText, targetUri, isCellCustomDisplayText))
            {
                targetUri = ReplaceInvalidChars(targetUri);
                XlHyperlink hyperlink1 = new XlHyperlink();
                hyperlink1.Reference = new XlCellRange(new XlCellPosition(cell.ColumnIndex, cell.RowIndex));
                XlHyperlink hyperlink = hyperlink1;
                GetHyperlinkCellText(cell, textFormat);
                SetData(cell, hyperlink, displayText, targetUri, isCellCustomDisplayText);
                FormattingUtils.SetHyperlinkFormat(cell.Formatting);
                sheet.Hyperlinks.Add(hyperlink);
            }
        }
    }
}

