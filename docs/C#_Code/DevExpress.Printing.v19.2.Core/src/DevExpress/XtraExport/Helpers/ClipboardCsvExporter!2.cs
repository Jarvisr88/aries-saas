namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using DevExpress.XtraExport.Csv;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public class ClipboardCsvExporter<TCol, TRow> : IClipboardExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private MemoryStream ms;
        private IXlExport exporter;
        private IXlDocument document;
        private IXlSheet sheet;

        public void AddBandedHeader(ClipboardBandLayoutInfo info)
        {
            int index = 0;
            while (index < info.HeaderPanelInfo.Length)
            {
                List<ClipboardBandCellInfo> list = info.HeaderPanelInfo[index];
                IXlRow row = this.exporter.BeginRow();
                int num2 = 0;
                while (true)
                {
                    if (num2 >= list.Count)
                    {
                        this.exporter.EndRow();
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info2 = list[num2];
                    if (info2.SpaceBefore > 0)
                    {
                        this.exporter.SkipCells(info2.SpaceBefore);
                    }
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= info2.Width)
                        {
                            if (info2.SpaceAfter > 0)
                            {
                                this.exporter.SkipCells(info2.SpaceAfter);
                            }
                            num2++;
                            break;
                        }
                        IXlCell cell = this.exporter.BeginCell();
                        if (num3 == 0)
                        {
                            if (info2.AllowHtmlDraw)
                            {
                                cell.SetRichText(info2.DisplayValueFormatted);
                            }
                            else
                            {
                                cell.Value = info2.DisplayValue;
                            }
                        }
                        this.exporter.EndCell();
                        num3++;
                    }
                }
            }
        }

        public void AddGroupHeader(ClipboardCellInfo groupHeaderInfo, int columnCount)
        {
            IXlRow row = this.exporter.BeginRow();
            for (int i = 0; i < columnCount; i++)
            {
                IXlCell cell = this.exporter.BeginCell();
                if (i == 0)
                {
                    cell.Value = groupHeaderInfo.DisplayValue;
                }
                this.exporter.EndCell();
            }
            this.exporter.EndRow();
        }

        public void AddHeaders(IEnumerable<ClipboardCellInfo> headerInfo)
        {
            IXlRow row = this.exporter.BeginRow();
            foreach (ClipboardCellInfo info in headerInfo)
            {
                IXlCell cell = this.exporter.BeginCell();
                cell.Formatting = new XlCellFormatting();
                if (info.AllowHtmlDraw)
                {
                    cell.SetRichText(info.DisplayValueFormatted);
                }
                else
                {
                    cell.Value = info.DisplayValue;
                }
                this.exporter.EndCell();
            }
            this.exporter.EndRow();
        }

        public void AddRow(IEnumerable<ClipboardCellInfo> rowsInfo)
        {
            IXlRow row = this.exporter.BeginRow();
            for (int i = 0; i < rowsInfo.Count<ClipboardCellInfo>(); i++)
            {
                IXlCell cell = this.exporter.BeginCell();
                cell.Value = XlVariantValue.FromObject(rowsInfo.ElementAt<ClipboardCellInfo>(i).DisplayValue);
                this.exporter.EndCell();
            }
            this.exporter.EndRow();
        }

        public void AddRow(IEnumerable<ClipboardBandCellInfo>[] rowInfo)
        {
            int index = 0;
            while (index < rowInfo.Length)
            {
                List<ClipboardBandCellInfo> list = rowInfo[index].ToList<ClipboardBandCellInfo>();
                IXlRow row = this.exporter.BeginRow();
                int num2 = 0;
                while (true)
                {
                    if (num2 >= list.Count)
                    {
                        this.exporter.EndRow();
                        index++;
                        break;
                    }
                    ClipboardBandCellInfo info = list[num2];
                    if (info.SpaceBefore > 0)
                    {
                        this.exporter.SkipCells(info.SpaceBefore);
                    }
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= info.Width)
                        {
                            if (info.SpaceAfter > 0)
                            {
                                this.exporter.SkipCells(info.SpaceAfter);
                            }
                            num2++;
                            break;
                        }
                        IXlCell cell = this.exporter.BeginCell();
                        if (num3 == 0)
                        {
                            if (info.AllowHtmlDraw)
                            {
                                cell.SetRichText(info.DisplayValueFormatted);
                            }
                            else
                            {
                                cell.Value = info.DisplayValue;
                            }
                        }
                        this.exporter.EndCell();
                        num3++;
                    }
                }
            }
        }

        public void BeginExport()
        {
            this.exporter = new CsvDataAwareExporter();
            this.ms = new MemoryStream();
            this.document = this.exporter.BeginExport(this.ms);
            this.document.Options.Culture = CultureInfo.CurrentCulture;
            CsvDataAwareExporterOptions options = this.document.Options as CsvDataAwareExporterOptions;
            if (options != null)
            {
                options.Encoding = Encoding.Default;
            }
            this.sheet = this.exporter.BeginSheet();
        }

        public void EndExport()
        {
            this.exporter.EndSheet();
            this.exporter.EndExport();
        }

        public void SetDataObject(DataObject data)
        {
            data.SetData("Csv", this.ms);
        }
    }
}

