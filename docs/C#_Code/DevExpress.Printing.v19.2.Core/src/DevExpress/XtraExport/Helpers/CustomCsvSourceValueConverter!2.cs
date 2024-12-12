namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.SpreadsheetSource.Csv;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.InteropServices;

    internal class CustomCsvSourceValueConverter<TCol, TRow> : ICsvSourceValueConverter where TCol: class, IColumn where TRow: class, IRowBase
    {
        private CultureInfo cultureInfo;
        private IClipboardPasteHelper<TCol, TRow> clipboardPasteHelper;
        private IEnumerable<IColumn> targetColumns;

        public CustomCsvSourceValueConverter(IClipboardPasteHelper<TCol, TRow> clipboardPasteHelper, CultureInfo cultureInfo = null)
        {
            this.clipboardPasteHelper = clipboardPasteHelper;
            this.cultureInfo = (cultureInfo == null) ? CultureInfo.CurrentCulture : cultureInfo;
        }

        public object Convert(string text, int columnIndex)
        {
            double num;
            bool flag2;
            DateTime time;
            if (text == null)
            {
                return null;
            }
            if ((this.TargetColumns != null) && (columnIndex < this.TargetColumns.Count<IColumn>()))
            {
                IColumn column = this.TargetColumns.ElementAt<IColumn>(columnIndex);
                if ((column != null) && (column.ColumnType == typeof(string)))
                {
                    return text;
                }
            }
            bool flag = text.Contains("%");
            string s = text.Replace("%", string.Empty);
            return (!double.TryParse(s, NumberStyles.AllowCurrencySymbol | NumberStyles.Float | NumberStyles.AllowThousands, this.cultureInfo, out num) ? (!bool.TryParse(s, out flag2) ? ((object) text) : ((object) flag2)) : (!DateTime.TryParse(text, out time) ? (flag ? ((object) (num / 100.0)) : ((object) num)) : ((object) text)));
        }

        private IEnumerable<IColumn> TargetColumns
        {
            get
            {
                if ((this.clipboardPasteHelper != null) && (this.targetColumns == null))
                {
                    TRow startRow = default(TRow);
                    this.targetColumns = (IEnumerable<IColumn>) this.clipboardPasteHelper.GetTargetColumns(-1, startRow);
                }
                return this.targetColumns;
            }
        }
    }
}

