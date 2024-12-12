namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public class ClipboardXlsExporter<TCol, TRow> : IClipboardExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private MemoryStream ms;
        private IXlExport exporter;
        private IXlDocument document;
        private IXlSheet sheet;
        private static readonly DateTime minExcelDateTime;

        static ClipboardXlsExporter()
        {
            ClipboardXlsExporter<TCol, TRow>.minExcelDateTime = XlVariantValue.BaseDate.AddDays(1.0);
        }

        public void AddBandedHeader(ClipboardBandLayoutInfo info)
        {
            Dictionary<XlCellRange, XlCellFormatting> dictionary = new Dictionary<XlCellRange, XlCellFormatting>();
            List<XlCellRange> merged = new List<XlCellRange>();
            int num = 0;
            int index = 0;
            while (index < info.HeaderPanelInfo.Length)
            {
                List<ClipboardBandCellInfo> list2 = info.HeaderPanelInfo[index];
                IXlRow row = this.exporter.BeginRow();
                num = 0;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= list2.Count)
                    {
                        this.exporter.EndRow();
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info2 = list2[num3];
                    int num4 = (info2.Height == 1) ? 0 : (info2.Height - 1);
                    dictionary.Add(XlCellRange.FromLTRB(num + info2.SpaceBefore, row.RowIndex, ((num + info2.SpaceBefore) + info2.Width) - 1, row.RowIndex + num4), info2.Formatting);
                    if ((info2.Height > 0) && ((info2.Width > 1) || (info2.Height > 1)))
                    {
                        int num5 = (info2.Height == 1) ? 0 : (info2.Height - 1);
                        merged.Add(XlCellRange.FromLTRB(num + info2.SpaceBefore, row.RowIndex, ((num + info2.SpaceBefore) + info2.Width) - 1, row.RowIndex + num5));
                    }
                    num += info2.Width + info2.SpaceBefore;
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= info2.SpaceBefore)
                        {
                            int num7 = 0;
                            while (true)
                            {
                                if (num7 >= info2.Width)
                                {
                                    int num8 = 0;
                                    while (true)
                                    {
                                        if (num8 >= info2.SpaceAfter)
                                        {
                                            num3++;
                                            break;
                                        }
                                        this.exporter.BeginCell();
                                        IXlCell cell = this.exporter.BeginCell();
                                        if (this.MergedCell(merged, cell))
                                        {
                                            cell.Formatting = new XlCellFormatting();
                                            XlCellRange range3 = dictionary.Keys.FirstOrDefault<XlCellRange>(range => range.Contains(cell.Position));
                                            if (range3 != null)
                                            {
                                                cell.Formatting.MergeWith(dictionary[range3]);
                                            }
                                        }
                                        this.exporter.EndCell();
                                        num8++;
                                    }
                                    break;
                                }
                                IXlCell cell2 = this.exporter.BeginCell();
                                if (num7 == 0)
                                {
                                    if (info2.AllowHtmlDraw)
                                    {
                                        cell2.SetRichText(info2.DisplayValueFormatted);
                                    }
                                    else
                                    {
                                        cell2.Value = info2.DisplayValue;
                                    }
                                }
                                cell2.Formatting = XlCellFormatting.FromNetFormat(info2.Formatting.NetFormatString, info2.Formatting.IsDateTimeFormatString);
                                cell2.Formatting.MergeWith(info2.Formatting);
                                cell2.Formatting.Alignment ??= new XlCellAlignment();
                                this.exporter.EndCell();
                                num7++;
                            }
                            break;
                        }
                        IXlCell cell = this.exporter.BeginCell();
                        if (this.MergedCell(merged, cell))
                        {
                            cell.Formatting = new XlCellFormatting();
                            cell.Formatting.MergeWith(info2.Formatting);
                        }
                        this.exporter.EndCell();
                        num6++;
                    }
                }
            }
            merged.ForEach(range => base.sheet.MergedCells.Add(range, true));
        }

        public void AddGroupHeader(ClipboardCellInfo groupHeaderInfo, int columnCount)
        {
            IXlRow row = this.exporter.BeginRow();
            for (int i = 0; i < columnCount; i++)
            {
                IXlCell cell = this.exporter.BeginCell();
                cell.ApplyFormatting(groupHeaderInfo.Formatting);
                if (i == 0)
                {
                    cell.Value = XlVariantValue.FromObject(groupHeaderInfo.DisplayValue);
                }
                this.exporter.EndCell();
            }
            try
            {
                this.sheet.MergedCells.Add(XlCellRange.FromLTRB(0, row.RowIndex, columnCount - 1, row.RowIndex));
            }
            catch
            {
            }
            this.exporter.EndRow();
        }

        public void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo)
        {
            IXlRow row = this.exporter.BeginRow();
            for (int i = 0; i < headerInfo.Count<ClipboardCellInfo>(); i++)
            {
                ClipboardCellInfo info = headerInfo.ElementAt<ClipboardCellInfo>(i);
                IXlCell cell = this.exporter.BeginCell();
                cell.ApplyFormatting(info.Formatting);
                if (info.AllowHtmlDraw)
                {
                    cell.SetRichText(info.DisplayValueFormatted);
                }
                else
                {
                    cell.Value = XlVariantValue.FromObject(info.Value);
                }
                this.exporter.EndCell();
            }
            this.exporter.EndRow();
        }

        public void AddRow(IEnumerable<ClipboardCellInfo> rowInfo)
        {
            IXlRow row = this.exporter.BeginRow();
            for (int i = 0; i < rowInfo.Count<ClipboardCellInfo>(); i++)
            {
                ClipboardCellInfo info = rowInfo.ElementAt<ClipboardCellInfo>(i);
                XlCellFormatting formatting = info.Formatting;
                IXlCell cell = this.exporter.BeginCell();
                if (info.Value != null)
                {
                    if (info.IsHyperlink)
                    {
                        cell.Value = info.DisplayValue;
                        cell.Formatting = XlCellFormatting.Hyperlink;
                        cell.Formatting.Font.Size = formatting.Font.Size;
                        XlHyperlink item = new XlHyperlink {
                            Reference = new XlCellRange(new XlCellPosition(cell.ColumnIndex, cell.RowIndex)),
                            TargetUri = info.Value.ToString()
                        };
                        this.sheet.Hyperlinks.Add(item);
                    }
                    else
                    {
                        switch (info.Value)
                        {
                            case (string _):
                                cell.Value = XlVariantValue.FromObject(info.DisplayValue);
                                break;

                            case ((Image _) || (Bitmap _)):
                                cell.Value = string.Empty;
                                break;

                            case (XlVariantValue.FromObject(info.Value) == XlVariantValue.Empty):
                                cell.Value = XlVariantValue.FromObject(info.Value.ToString());
                                break;

                            default:
                            {
                                object displayValue = info.Value;
                                if ((displayValue is DateTime) && (((DateTime) displayValue) < ClipboardXlsExporter<TCol, TRow>.minExcelDateTime))
                                {
                                    displayValue = info.DisplayValue;
                                }
                                if ((displayValue is TimeSpan) && (((TimeSpan) displayValue) < TimeSpan.Zero))
                                {
                                    displayValue = info.DisplayValue;
                                }
                                cell.Value = XlVariantValue.FromObject(displayValue);
                                break;
                            }
                        }
                    }
                }
                if (cell.Formatting == null)
                {
                    cell.Formatting = new XlCellFormatting();
                    cell.Formatting.MergeWith(info.Formatting);
                }
                this.exporter.EndCell();
            }
            this.exporter.EndRow();
        }

        public void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo)
        {
            List<XlCellRange> merged = new List<XlCellRange>();
            int num = 0;
            int index = 0;
            while (index < rowInfo.Length)
            {
                List<ClipboardBandCellInfo> list2 = rowInfo[index].ToList<ClipboardBandCellInfo>();
                IXlRow row = this.exporter.BeginRow();
                num = 0;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= list2.Count)
                    {
                        this.exporter.EndRow();
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info = list2[num3];
                    if ((info.Height > 0) && ((info.Width > 1) || (info.Height > 1)))
                    {
                        int num4 = (info.Height == 1) ? 0 : (info.Height - 1);
                        merged.Add(XlCellRange.FromLTRB(num + info.SpaceBefore, row.RowIndex, ((num + info.SpaceBefore) + info.Width) - 1, row.RowIndex + num4));
                    }
                    num += info.Width + info.SpaceBefore;
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 >= info.SpaceBefore)
                        {
                            int num6 = 0;
                            while (true)
                            {
                                if (num6 >= info.Width)
                                {
                                    int num7 = 0;
                                    while (true)
                                    {
                                        if (num7 >= info.SpaceAfter)
                                        {
                                            num3++;
                                            break;
                                        }
                                        this.exporter.BeginCell();
                                        IXlCell cell3 = this.exporter.BeginCell();
                                        if (this.MergedCell(merged, cell3))
                                        {
                                            cell3.Formatting = new XlCellFormatting();
                                            cell3.Formatting.MergeWith(info.Formatting);
                                        }
                                        this.exporter.EndCell();
                                        num7++;
                                    }
                                    break;
                                }
                                IXlCell cell2 = this.exporter.BeginCell();
                                if (num6 == 0)
                                {
                                    if (info.IsHyperlink)
                                    {
                                        cell2.Value = info.DisplayValue;
                                        cell2.Formatting = XlCellFormatting.Hyperlink;
                                        cell2.Formatting.Font.Size = info.Formatting.Font.Size;
                                        XlHyperlink item = new XlHyperlink {
                                            Reference = new XlCellRange(new XlCellPosition(cell2.ColumnIndex, cell2.RowIndex)),
                                            TargetUri = info.Value.ToString()
                                        };
                                        this.sheet.Hyperlinks.Add(item);
                                    }
                                    else if (info.Value != null)
                                    {
                                        if (info.Value is string)
                                        {
                                            cell2.Value = XlVariantValue.FromObject(info.DisplayValue);
                                        }
                                        else if ((info.Value is Image) || ((info.Value is Bitmap) || (info.Value is byte[])))
                                        {
                                            cell2.Value = string.Empty;
                                        }
                                        else if (XlVariantValue.FromObject(info.Value) == XlVariantValue.Empty)
                                        {
                                            cell2.Value = XlVariantValue.FromObject(info.Value.ToString());
                                        }
                                        else
                                        {
                                            object displayValue = info.Value;
                                            if ((displayValue is DateTime) && (((DateTime) displayValue) < ClipboardXlsExporter<TCol, TRow>.minExcelDateTime))
                                            {
                                                displayValue = info.DisplayValue;
                                            }
                                            if ((displayValue is TimeSpan) && (((TimeSpan) displayValue) < TimeSpan.Zero))
                                            {
                                                displayValue = info.DisplayValue;
                                            }
                                            cell2.Value = XlVariantValue.FromObject(displayValue);
                                        }
                                    }
                                }
                                cell2.Formatting = XlCellFormatting.FromNetFormat(info.Formatting.NetFormatString, info.Formatting.IsDateTimeFormatString);
                                cell2.Formatting.MergeWith(info.Formatting);
                                cell2.Formatting.Alignment ??= new XlCellAlignment();
                                this.exporter.EndCell();
                                num6++;
                            }
                            break;
                        }
                        IXlCell cell = this.exporter.BeginCell();
                        if (this.MergedCell(merged, cell))
                        {
                            cell.Formatting = new XlCellFormatting();
                            cell.Formatting.MergeWith(info.Formatting);
                        }
                        this.exporter.EndCell();
                        num5++;
                    }
                }
            }
            merged.ForEach(range => base.sheet.MergedCells.Add(range, true));
        }

        public void BeginExport()
        {
            this.exporter = new XlsDataAwareExporter();
            (this.exporter as XlsDataAwareExporter).ClipboardMode = true;
            this.ms = new MemoryStream();
            this.document = this.exporter.BeginExport(this.ms);
            this.document.Options.Culture = CultureInfo.CurrentCulture;
            this.sheet = this.exporter.BeginSheet();
        }

        public void EndExport()
        {
            this.exporter.EndSheet();
            this.exporter.EndExport();
        }

        private bool MergedCell(List<XlCellRange> merged, IXlCell cell)
        {
            bool flag;
            using (List<XlCellRange>.Enumerator enumerator = merged.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlCellRange current = enumerator.Current;
                        if (!current.HasCommonCells(new XlCellRange(cell.Position)))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public void SetDataObject(DataObject data)
        {
            data.SetData("Biff8", this.ms);
        }
    }
}

