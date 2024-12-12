namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Data;
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.Utils.Controls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public abstract class ClipboardExportManagerBase<TCol, TRow> : IClipboardManager<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private static readonly DateTime minExcelDateTime;
        private IClipboardBandedGridView<TCol, TRow> bandedGridViewImplementer;
        private IClipboardGridView<TCol, TRow> gridViewImplementor;

        static ClipboardExportManagerBase()
        {
            ClipboardExportManagerBase<TCol, TRow>.minExcelDateTime = XlVariantValue.BaseDate.AddDays(1.0);
        }

        public ClipboardExportManagerBase(IClipboardGridView<TCol, TRow> gridView) : this(gridView, new ClipboardOptions(true))
        {
        }

        public ClipboardExportManagerBase(IClipboardGridView<TCol, TRow> gridView, ClipboardOptions exportOptions)
        {
            this.gridViewImplementor = gridView;
            this.<ExportOptions>k__BackingField = exportOptions;
        }

        public void AssignOptions(BaseOptions options)
        {
            if (options is ClipboardOptions)
            {
                this.ExportOptions.Assign(options);
            }
        }

        private void BeginExport(List<IClipboardExporter<TCol, TRow>> exporters)
        {
            foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
            {
                exporter.BeginExport();
            }
        }

        private List<ClipboardBandCellInfo>[] CreateAdvRowInfo(ClipboardBandLayoutInfo bandsInfo, TRow selectedRow)
        {
            List<ClipboardBandCellInfo>[] listArray = new List<ClipboardBandCellInfo>[bandsInfo.ColumnPanelInfo.Length];
            int index = 0;
            while (index < bandsInfo.ColumnPanelInfo.Length)
            {
                List<ClipboardBandCellInfo> list = bandsInfo.ColumnPanelInfo[index];
                int num2 = 0;
                while (true)
                {
                    if (num2 >= list.Count)
                    {
                        index++;
                        break;
                    }
                    TCol column = list[num2].Column as TCol;
                    object rowCellValue = string.Empty;
                    string displayValue = string.Empty;
                    XlCellFormatting xlCellFormatting = new XlCellFormatting();
                    if (column != null)
                    {
                        rowCellValue = this.gridViewImplementor.GetRowCellValue(selectedRow, column);
                        displayValue = this.gridViewImplementor.GetRowCellDisplayText(selectedRow, column.FieldName);
                        xlCellFormatting = this.GetXlCellFormatting(selectedRow, column);
                    }
                    if (displayValue == null)
                    {
                        displayValue = string.Empty;
                        this.ResetFormattingFill(xlCellFormatting);
                    }
                    ClipboardBandCellInfo info1 = new ClipboardBandCellInfo(list[num2].Width, list[num2].Height, rowCellValue, displayValue, xlCellFormatting, false);
                    info1.IsHyperlink = this.gridViewImplementor.GetRowCellHyperlink(selectedRow, column) != null;
                    info1.Column = column;
                    ClipboardBandCellInfo item = info1;
                    item.SpaceAfter = list[num2].SpaceAfter;
                    item.SpaceBefore = list[num2].SpaceBefore;
                    listArray[index] ??= new List<ClipboardBandCellInfo>();
                    listArray[index].Add(item);
                    num2++;
                }
            }
            return listArray;
        }

        protected virtual List<IClipboardExporter<TCol, TRow>> CreateExporters(int columnsCount)
        {
            int num2 = this.gridViewImplementor.GetSelectedCellsCount() / columnsCount;
            List<IClipboardExporter<TCol, TRow>> list = new List<IClipboardExporter<TCol, TRow>>();
            if (this.ExportOptions.AllowTxtFormat != DefaultBoolean.False)
            {
                list.Add(new ClipboardTxtExporter<TCol, TRow>());
            }
            if (this.ExportOptions.AllowRtfFormat != DefaultBoolean.False)
            {
                list.Add(new ClipboardRtfExporterNew<TCol, TRow>());
            }
            if (this.ExportOptions.AllowHtmlFormat != DefaultBoolean.False)
            {
                list.Add(new ClipboardHtmlExporter<TCol, TRow>());
            }
            if ((this.ExportOptions.AllowExcelFormat != DefaultBoolean.False) && (num2 < 0xffff))
            {
                list.Add(new ClipboardXlsExporter<TCol, TRow>());
            }
            if ((this.ExportOptions.AllowCsvFormat != DefaultBoolean.False) && (num2 < 0xffff))
            {
                list.Add(new ClipboardCsvExporter<TCol, TRow>());
            }
            return list;
        }

        private List<ClipboardCellInfo> CreateGroupRowSummaryInfo(TRow selectedRow, IEnumerable<TCol> selectedColumns)
        {
            object summaryValueByGroupId = null;
            string displayValue = string.Empty;
            List<ClipboardCellInfo> list = new List<ClipboardCellInfo>();
            Dictionary<string, ClipboardCellInfo> summaries = new Dictionary<string, ClipboardCellInfo>();
            foreach (ISummaryItemEx ex in this.gridViewImplementor.GridGroupSummaryItemCollection)
            {
                if (ex.SummaryType != SummaryItemType.None)
                {
                    summaryValueByGroupId = ex.GetSummaryValueByGroupId(selectedRow.LogicalPosition);
                    XlCellFormatting formatting = new XlCellFormatting();
                    formatting.NetFormatString = ex.DisplayFormat;
                    ClipboardCellInfo info = new ClipboardCellInfo(summaryValueByGroupId, string.Format(ex.DisplayFormat, summaryValueByGroupId), formatting, false);
                    summaries.Add(ex.FieldName, info);
                }
            }
            bool groupHeaderAdded = false;
            foreach (TCol local in selectedColumns)
            {
                XlCellFormatting formatting = !this.gridViewImplementor.GetShowGroupedColumns() ? this.CreateGroupRowSummaryInfoUnGroupped(selectedRow, summaries, local, out summaryValueByGroupId, out displayValue, ref groupHeaderAdded) : this.CreateGroupRowSummaryInfoGroupped(selectedRow, summaries, local, out summaryValueByGroupId, out displayValue);
                if (string.IsNullOrEmpty(displayValue))
                {
                    this.ResetFormattingFill(formatting);
                }
                bool flag2 = this.gridViewImplementor.GetRowCellHyperlink(selectedRow, local) != null;
                ClipboardCellInfo item = new ClipboardCellInfo(summaryValueByGroupId, displayValue, formatting, false);
                item.IsHyperlink = flag2;
                list.Add(item);
            }
            return list;
        }

        private XlCellFormatting CreateGroupRowSummaryInfoGroupped(TRow selectedRow, Dictionary<string, ClipboardCellInfo> summaries, TCol column, out object value, out string displayValue)
        {
            string groupedColumnFieldName = (selectedRow as IClipboardGroupRow<TRow>).GetGroupedColumnFieldName();
            XlCellFormatting xlCellFormatting = this.GetXlCellFormatting(selectedRow, column);
            if (column.FieldName == groupedColumnFieldName)
            {
                displayValue = (selectedRow as IGroupRow<TRow>).GetGroupRowHeader().Trim();
                value = displayValue;
            }
            else if (!summaries.ContainsKey(column.FieldName))
            {
                displayValue = string.Empty;
                value = displayValue;
            }
            else
            {
                value = summaries[column.FieldName].Value;
                displayValue = summaries[column.FieldName].DisplayValue;
                xlCellFormatting.NetFormatString = summaries[column.FieldName].Formatting.NetFormatString;
            }
            return xlCellFormatting;
        }

        private XlCellFormatting CreateGroupRowSummaryInfoUnGroupped(TRow selectedRow, Dictionary<string, ClipboardCellInfo> summaries, TCol column, out object value, out string displayValue, ref bool groupHeaderAdded)
        {
            XlCellFormatting xlCellFormatting = this.GetXlCellFormatting(selectedRow, column);
            if (!groupHeaderAdded)
            {
                groupHeaderAdded = true;
                displayValue = (selectedRow as IGroupRow<TRow>).GetGroupRowHeader();
                value = displayValue;
                if (summaries.ContainsKey(column.FieldName))
                {
                    value = value + " " + summaries[column.FieldName];
                    displayValue = displayValue + " " + summaries[column.FieldName];
                }
            }
            else if (!summaries.ContainsKey(column.FieldName))
            {
                displayValue = string.Empty;
                value = displayValue;
            }
            else
            {
                value = summaries[column.FieldName].Value;
                displayValue = summaries[column.FieldName].DisplayValue;
                xlCellFormatting.NetFormatString = summaries[column.FieldName].Formatting.NetFormatString;
            }
            return xlCellFormatting;
        }

        private List<ClipboardCellInfo> CreateHeaderRowInfo(IEnumerable<TCol> selectedColumns)
        {
            List<ClipboardCellInfo> list = new List<ClipboardCellInfo>();
            foreach (TCol local in selectedColumns)
            {
                TRow selectedRow = default(TRow);
                XlCellFormatting xlCellFormatting = this.GetXlCellFormatting(selectedRow, local);
                this.ResetFormattingFill(xlCellFormatting);
                list.Add(new ClipboardCellInfo(local.Header, local.Header, xlCellFormatting, false));
            }
            return list;
        }

        private List<ClipboardCellInfo> CreateRowInfo(TRow selectedRow, IEnumerable<TCol> selectedColumns)
        {
            List<ClipboardCellInfo> list = new List<ClipboardCellInfo>();
            foreach (TCol local in selectedColumns)
            {
                object rowCellValue = this.gridViewImplementor.GetRowCellValue(selectedRow, local);
                string rowCellDisplayText = this.gridViewImplementor.GetRowCellDisplayText(selectedRow, local.FieldName);
                if ((rowCellValue is Image) || (rowCellValue is Bitmap))
                {
                    rowCellDisplayText = string.Empty;
                }
                XlCellFormatting xlCellFormatting = this.GetXlCellFormatting(selectedRow, local);
                if (rowCellDisplayText == null)
                {
                    rowCellDisplayText = string.Empty;
                    this.ResetFormattingFill(xlCellFormatting);
                }
                bool flag = this.gridViewImplementor.GetRowCellHyperlink(selectedRow, local) != null;
                ClipboardCellInfo item = new ClipboardCellInfo(rowCellValue, rowCellDisplayText, xlCellFormatting, false);
                item.IsHyperlink = flag;
                list.Add(item);
            }
            return list;
        }

        protected virtual IClipboardExporter<TCol, TRow> CreateRTFExporter(bool exportRtf, bool exportHtml) => 
            null;

        private void EndExport(DataObject dataObject, List<IClipboardExporter<TCol, TRow>> exporters)
        {
            foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
            {
                exporter.EndExport();
                exporter.SetDataObject(dataObject);
            }
        }

        private void ExportAdvBandedViewDataRows(ClipboardBandLayoutInfo bandsInfo, List<IClipboardExporter<TCol, TRow>> exporters, IEnumerable<TCol> selectedColumns)
        {
            int rowIterator = 0;
            int rowsCount = this.gridViewImplementor.GetSelectedCellsCount() / selectedColumns.Count<TCol>();
            this.ForAllRows(this.gridViewImplementor, delegate (TRow selectedRow) {
                int num = rowIterator;
                rowIterator = num + 1;
                bool flag = false;
                ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.ProgressBarCallBack((int) Math.Round((double) ((rowIterator * 100.0) / ((double) rowsCount))));
                if (!selectedRow.IsGroupRow || (selectedRow as IClipboardGroupRow<TRow>).IsTreeListGroupRow())
                {
                    List<ClipboardBandCellInfo>[] panelInfo = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateAdvRowInfo(bandsInfo, selectedRow);
                    if (panelInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).bandedGridViewImplementer.RaiseClipboardAdvDataRowCopying(selectedRow.LogicalPosition, panelInfo);
                    }
                    if (!flag)
                    {
                        foreach (IClipboardExporter<TCol, TRow> exporter2 in exporters)
                        {
                            exporter2.AddRow(panelInfo);
                        }
                    }
                }
                else if (!((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.GetAlignGroupSummaryInGroupRow())
                {
                    TCol[] localArray = new TCol[] { ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.GetGroupedColumns().ElementAt<TCol>(selectedRow.GetRowLevel()) };
                    List<ClipboardCellInfo> source = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateGroupRowSummaryInfo(selectedRow, localArray);
                    ClipboardCellInfo headerInfo = source.FirstOrDefault<ClipboardCellInfo>();
                    if (headerInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.RaiseClipboardGroupRowCopying(selectedRow.LogicalPosition, localArray, source, ClipboardInfoType.Group);
                    }
                    if (!flag)
                    {
                        for (int i = 0; i < exporters.Count; i++)
                        {
                            exporters[i].AddGroupHeader(headerInfo, selectedColumns.Count<TCol>());
                        }
                    }
                }
                else
                {
                    List<ClipboardCellInfo> rowInfo = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateGroupRowSummaryInfo(selectedRow, selectedColumns);
                    if (rowInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.RaiseClipboardGroupRowCopying(selectedRow.LogicalPosition, selectedColumns, rowInfo, ClipboardInfoType.GroupSummary);
                    }
                    if (!flag)
                    {
                        Func<ClipboardCellInfo, bool> predicate = <>c<TCol, TRow>.<>9__14_1;
                        if (<>c<TCol, TRow>.<>9__14_1 == null)
                        {
                            Func<ClipboardCellInfo, bool> local1 = <>c<TCol, TRow>.<>9__14_1;
                            predicate = <>c<TCol, TRow>.<>9__14_1 = info => !string.IsNullOrEmpty(info.DisplayValue);
                        }
                        ClipboardCellInfo info = rowInfo.FirstOrDefault<ClipboardCellInfo>(predicate);
                        foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
                        {
                            exporter.AddGroupHeader(new ClipboardCellInfo(info.Value, info.DisplayValue, info.Formatting, false), bandsInfo.GetBandPanelRowWidth(0, false));
                        }
                    }
                }
            });
        }

        private void ExportAdvBandedViewHeaders(ClipboardBandLayoutInfo bandsInfo, List<IClipboardExporter<TCol, TRow>> exporters)
        {
            bool isWithoutBand = this.bandedGridViewImplementer.RaiseClipboardAdvBandRowCopying(bandsInfo.BandPanelInfo);
            bool isWithoutHeader = this.bandedGridViewImplementer.RaiseClipboardAdvHeaderRowCopying(bandsInfo.ColumnPanelInfo);
            bandsInfo = bandsInfo.Without(isWithoutBand, isWithoutHeader);
            if (!isWithoutBand || !isWithoutHeader)
            {
                foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
                {
                    exporter.AddBandedHeader(bandsInfo);
                }
            }
        }

        private void ExportBandedViewHeaders(ClipboardBandLayoutInfo bandsInfo, List<IClipboardExporter<TCol, TRow>> exporters)
        {
            if (this.ExportOptions.CopyColumnHeaders != DefaultBoolean.False)
            {
                Func<List<ClipboardBandCellInfo>, IEnumerable<ClipboardBandCellInfo>> selector = <>c<TCol, TRow>.<>9__15_0;
                if (<>c<TCol, TRow>.<>9__15_0 == null)
                {
                    Func<List<ClipboardBandCellInfo>, IEnumerable<ClipboardBandCellInfo>> local1 = <>c<TCol, TRow>.<>9__15_0;
                    selector = <>c<TCol, TRow>.<>9__15_0 = delegate (List<ClipboardBandCellInfo> x) {
                        Func<ClipboardBandCellInfo, ClipboardBandCellInfo> func1 = <>c<TCol, TRow>.<>9__15_1;
                        if (<>c<TCol, TRow>.<>9__15_1 == null)
                        {
                            Func<ClipboardBandCellInfo, ClipboardBandCellInfo> local1 = <>c<TCol, TRow>.<>9__15_1;
                            func1 = <>c<TCol, TRow>.<>9__15_1 = t => t;
                        }
                        return x.Select<ClipboardBandCellInfo, ClipboardBandCellInfo>(func1);
                    };
                }
                IEnumerable<ClipboardBandCellInfo> rowInfo = bandsInfo.BandPanelInfo.SelectMany<List<ClipboardBandCellInfo>, ClipboardBandCellInfo>(selector);
                if (bandsInfo.ColumnPanelInfo.Count<List<ClipboardBandCellInfo>>() > 1)
                {
                    throw new Exception("!!!");
                }
                bool isWithoutBand = this.bandedGridViewImplementer.RaiseClipboardBandRowCopying(rowInfo);
                Func<ClipboardBandCellInfo, TCol> func2 = <>c<TCol, TRow>.<>9__15_2;
                if (<>c<TCol, TRow>.<>9__15_2 == null)
                {
                    Func<ClipboardBandCellInfo, TCol> local2 = <>c<TCol, TRow>.<>9__15_2;
                    func2 = <>c<TCol, TRow>.<>9__15_2 = x => x.Column as TCol;
                }
                bool isWithoutHeader = this.bandedGridViewImplementer.RaiseClipboardHeaderRowCopying(bandsInfo.ColumnPanelInfo[0].Select<ClipboardBandCellInfo, TCol>(func2), (IEnumerable<ClipboardCellInfo>) bandsInfo.ColumnPanelInfo[0]);
                bandsInfo = bandsInfo.Without(isWithoutBand, isWithoutHeader);
                if (!isWithoutBand || !isWithoutHeader)
                {
                    foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
                    {
                        exporter.AddBandedHeader(bandsInfo);
                    }
                }
            }
        }

        private void ExportDataRows(IEnumerable<TCol> selectedColumns, List<IClipboardExporter<TCol, TRow>> exporters)
        {
            int rowIterator = 0;
            int rowsCount = this.gridViewImplementor.GetSelectedCellsCount() / selectedColumns.Count<TCol>();
            this.ForAllRows(this.gridViewImplementor, delegate (TRow selectedRow) {
                int num = rowIterator;
                rowIterator = num + 1;
                bool flag = false;
                ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.ProgressBarCallBack((int) Math.Round((double) ((rowIterator * 100.0) / ((double) rowsCount))));
                if (!selectedRow.IsGroupRow || (selectedRow as IClipboardGroupRow<TRow>).IsTreeListGroupRow())
                {
                    List<ClipboardCellInfo> rowInfo = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateRowInfo(selectedRow, selectedColumns);
                    if (rowInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.RaiseClipboardDataRowCopying(selectedRow.LogicalPosition, selectedColumns, rowInfo);
                    }
                    if (!flag)
                    {
                        for (int i = 0; i < exporters.Count; i++)
                        {
                            exporters[i].AddRow(rowInfo);
                        }
                    }
                }
                else if (((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.GetAlignGroupSummaryInGroupRow())
                {
                    List<ClipboardCellInfo> rowInfo = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateGroupRowSummaryInfo(selectedRow, selectedColumns);
                    if (rowInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.RaiseClipboardGroupRowCopying(selectedRow.LogicalPosition, selectedColumns, rowInfo, ClipboardInfoType.GroupSummary);
                    }
                    if (!flag)
                    {
                        for (int i = 0; i < exporters.Count; i++)
                        {
                            exporters[i].AddRow(rowInfo);
                        }
                    }
                }
                else
                {
                    TCol[] localArray = new TCol[] { ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.GetGroupedColumns().ElementAt<TCol>(selectedRow.GetRowLevel()) };
                    List<ClipboardCellInfo> source = ((ClipboardExportManagerBase<TCol, TRow>) this).CreateGroupRowSummaryInfo(selectedRow, localArray);
                    ClipboardCellInfo headerInfo = source.FirstOrDefault<ClipboardCellInfo>();
                    if (headerInfo != null)
                    {
                        flag = ((ClipboardExportManagerBase<TCol, TRow>) this).gridViewImplementor.RaiseClipboardGroupRowCopying(selectedRow.LogicalPosition, localArray, source, ClipboardInfoType.Group);
                    }
                    if (!flag)
                    {
                        for (int i = 0; i < exporters.Count; i++)
                        {
                            exporters[i].AddGroupHeader(headerInfo, selectedColumns.Count<TCol>());
                        }
                    }
                }
            });
        }

        private void ExportHeaders(IEnumerable<TCol> selectedColumns, List<IClipboardExporter<TCol, TRow>> exporters)
        {
            if (this.ExportOptions.CopyColumnHeaders != DefaultBoolean.False)
            {
                List<ClipboardCellInfo> rowInfo = this.CreateHeaderRowInfo(selectedColumns);
                if (!this.gridViewImplementor.RaiseClipboardHeaderRowCopying(selectedColumns, rowInfo))
                {
                    foreach (IClipboardExporter<TCol, TRow> exporter in exporters)
                    {
                        exporter.AddHeaders(rowInfo);
                    }
                }
            }
        }

        protected void ForAllRows(IClipboardGridView<TCol, TRow> view, Action<TRow> action)
        {
            this.ForAllRowsCore(view, action, null);
        }

        internal void ForAllRowsCore(IClipboardGridView<TCol, TRow> view, Action<TRow> action, IClipboardGroupRow<TRow> parent = null)
        {
            foreach (TRow local in (parent == null) ? view.GetSelectedRows() : ((!parent.IsCollapsed || (this.ExportOptions.CopyCollapsedData != DefaultBoolean.True)) ? parent.GetSelectedRows() : parent.GetAllRows()))
            {
                action(local);
                if (view.IsCancelPending)
                {
                    break;
                }
                IClipboardGroupRow<TRow> row = local as IClipboardGroupRow<TRow>;
                if (row != null)
                {
                    this.ForAllRowsCore(view, action, row);
                }
            }
        }

        private XlCellFormatting GetXlCellFormatting(TRow selectedRow, TCol column)
        {
            XlCellFormatting cellAppearance = this.gridViewImplementor.GetCellAppearance(selectedRow, column);
            cellAppearance.IsDateTimeFormatString = false;
            System.Type actualDataType = column.FormatSettings.ActualDataType;
            System.Type underlyingType = Nullable.GetUnderlyingType(actualDataType);
            if (underlyingType != null)
            {
                actualDataType = underlyingType;
            }
            if (selectedRow != null)
            {
                this.SetDateTimeOrTimeSpanFormat(selectedRow, column, cellAppearance, actualDataType);
            }
            if (cellAppearance.NumberFormat == null)
            {
                cellAppearance.NetFormatString = column.FormatSettings.FormatString;
            }
            if (this.gridViewImplementor.UseHierarchyIndent(selectedRow, column))
            {
                cellAppearance.Alignment.Indent = (byte) selectedRow.GetRowLevel();
            }
            return cellAppearance;
        }

        private void ResetFormattingFill(XlCellFormatting formatting)
        {
            if (formatting.Fill == null)
            {
                formatting.Fill = new XlFill();
                formatting.Fill.ForeColor = XlColor.FromArgb(0xffffff);
                formatting.Fill.BackColor = XlColor.FromArgb(0xffffff);
            }
        }

        public void SetClipboardData(DataObject dataObject)
        {
            try
            {
                Func<TCol, bool> predicate = <>c<TCol, TRow>.<>9__29_0;
                if (<>c<TCol, TRow>.<>9__29_0 == null)
                {
                    Func<TCol, bool> local1 = <>c<TCol, TRow>.<>9__29_0;
                    predicate = <>c<TCol, TRow>.<>9__29_0 = e => e.IsVisible;
                }
                Func<TCol, int> keySelector = <>c<TCol, TRow>.<>9__29_1;
                if (<>c<TCol, TRow>.<>9__29_1 == null)
                {
                    Func<TCol, int> local2 = <>c<TCol, TRow>.<>9__29_1;
                    keySelector = <>c<TCol, TRow>.<>9__29_1 = e => e.VisibleIndex;
                }
                IEnumerable<TCol> source = this.gridViewImplementor.GetSelectedColumns().Where<TCol>(predicate).OrderBy<TCol, int>(keySelector);
                if (source.Any<TCol>())
                {
                    this.bandedGridViewImplementer = this.gridViewImplementor as IClipboardBandedGridView<TCol, TRow>;
                    if (this.bandedGridViewImplementer != null)
                    {
                        this.SetClipboardDataCore(this.bandedGridViewImplementer, dataObject, source);
                    }
                    else
                    {
                        this.SetClipboardDataCore(dataObject, source);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void SetClipboardDataCore(DataObject dataObject, IEnumerable<TCol> selectedColumns)
        {
            List<IClipboardExporter<TCol, TRow>> exporters = this.CreateExporters(selectedColumns.Count<TCol>());
            if (exporters.Count != 0)
            {
                this.BeginExport(exporters);
                this.ExportHeaders(selectedColumns, exporters);
                this.ExportDataRows(selectedColumns, exporters);
                this.EndExport(dataObject, exporters);
            }
        }

        private void SetClipboardDataCore(IClipboardBandedGridView<TCol, TRow> bandedGridViewImplementer, DataObject dataObject, IEnumerable<TCol> selectedColumns)
        {
            List<IClipboardExporter<TCol, TRow>> exporters = this.CreateExporters(selectedColumns.Count<TCol>());
            if (exporters.Count != 0)
            {
                this.BeginExport(exporters);
                if (bandedGridViewImplementer.IsAdvBandedView())
                {
                    this.ExportAdvBandedViewHeaders(bandedGridViewImplementer.GetBandsInfo(), exporters);
                    this.ExportAdvBandedViewDataRows(bandedGridViewImplementer.GetBandsInfo(), exporters, selectedColumns);
                }
                else
                {
                    this.ExportBandedViewHeaders(bandedGridViewImplementer.GetBandsInfo(), exporters);
                    this.ExportDataRows(selectedColumns, exporters);
                }
                this.EndExport(dataObject, exporters);
            }
        }

        private bool SetDateTimeFormat(TRow selectedRow, TCol column, XlCellFormatting formatting, System.Type actualDataType)
        {
            if ((actualDataType != typeof(DateTime)) && (column.FormatSettings.FormatType != FormatType.DateTime))
            {
                return false;
            }
            object rowCellValue = this.gridViewImplementor.GetRowCellValue(selectedRow, column);
            if ((rowCellValue is DateTime) && (((DateTime) rowCellValue) > ClipboardExportManagerBase<TCol, TRow>.minExcelDateTime))
            {
                formatting.NumberFormat = XlNumberFormat.ShortDate;
                formatting.IsDateTimeFormatString = true;
            }
            if (!string.IsNullOrEmpty(column.FormatSettings.FormatString))
            {
                formatting.NetFormatString = column.FormatSettings.FormatString;
            }
            return true;
        }

        private bool SetDateTimeOrTimeSpanFormat(TRow selectedRow, TCol column, XlCellFormatting formatting, System.Type actualDataType) => 
            this.SetDateTimeFormat(selectedRow, column, formatting, actualDataType) || this.SetTimeSpanFormat(selectedRow, column, formatting, actualDataType);

        private bool SetTimeSpanFormat(TRow selectedRow, TCol column, XlCellFormatting formatting, System.Type actualDataType)
        {
            if (!(actualDataType == typeof(TimeSpan)))
            {
                return false;
            }
            object rowCellValue = this.gridViewImplementor.GetRowCellValue(selectedRow, column);
            if ((rowCellValue is TimeSpan) && (((TimeSpan) rowCellValue) > TimeSpan.Zero))
            {
                formatting.NumberFormat = XlNumberFormat.Span;
                formatting.IsDateTimeFormatString = true;
            }
            if (!string.IsNullOrEmpty(column.FormatSettings.FormatString))
            {
                formatting.NetFormatString = column.FormatSettings.FormatString;
            }
            return true;
        }

        public ClipboardOptions ExportOptions { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ClipboardExportManagerBase<TCol, TRow>.<>c <>9;
            public static Func<ClipboardCellInfo, bool> <>9__14_1;
            public static Func<ClipboardBandCellInfo, ClipboardBandCellInfo> <>9__15_1;
            public static Func<List<ClipboardBandCellInfo>, IEnumerable<ClipboardBandCellInfo>> <>9__15_0;
            public static Func<ClipboardBandCellInfo, TCol> <>9__15_2;
            public static Func<TCol, bool> <>9__29_0;
            public static Func<TCol, int> <>9__29_1;

            static <>c()
            {
                ClipboardExportManagerBase<TCol, TRow>.<>c.<>9 = new ClipboardExportManagerBase<TCol, TRow>.<>c();
            }

            internal bool <ExportAdvBandedViewDataRows>b__14_1(ClipboardCellInfo info) => 
                !string.IsNullOrEmpty(info.DisplayValue);

            internal IEnumerable<ClipboardBandCellInfo> <ExportBandedViewHeaders>b__15_0(List<ClipboardBandCellInfo> x)
            {
                Func<ClipboardBandCellInfo, ClipboardBandCellInfo> selector = ClipboardExportManagerBase<TCol, TRow>.<>c.<>9__15_1;
                if (ClipboardExportManagerBase<TCol, TRow>.<>c.<>9__15_1 == null)
                {
                    Func<ClipboardBandCellInfo, ClipboardBandCellInfo> local1 = ClipboardExportManagerBase<TCol, TRow>.<>c.<>9__15_1;
                    selector = ClipboardExportManagerBase<TCol, TRow>.<>c.<>9__15_1 = t => t;
                }
                return x.Select<ClipboardBandCellInfo, ClipboardBandCellInfo>(selector);
            }

            internal ClipboardBandCellInfo <ExportBandedViewHeaders>b__15_1(ClipboardBandCellInfo t) => 
                t;

            internal TCol <ExportBandedViewHeaders>b__15_2(ClipboardBandCellInfo x) => 
                x.Column as TCol;

            internal bool <SetClipboardData>b__29_0(TCol e) => 
                e.IsVisible;

            internal int <SetClipboardData>b__29_1(TCol e) => 
                e.VisibleIndex;
        }
    }
}

