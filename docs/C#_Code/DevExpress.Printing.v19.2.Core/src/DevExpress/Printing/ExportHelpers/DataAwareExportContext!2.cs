namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class DataAwareExportContext<TCol, TRow> : ISheetHeaderFooterExportContext, IExportContext, ISheetCustomizationContext where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected ExportInfo<TCol, TRow> exportInfo;

        public DataAwareExportContext(ExportInfo<TCol, TRow> exportInfo)
        {
            this.exportInfo = exportInfo;
        }

        public virtual void AddAutoFilter()
        {
            if (!this.exportInfo.Options.CanRaiseCustomizeSheetSettingsEvent)
            {
                this.AddAutoFilter(0, this.DataCellsRangeTop - 1, this.exportInfo.Exporter.CurrentColumnIndex - 1, this.exportInfo.Exporter.CurrentRowIndex - 1);
            }
        }

        public void AddAutoFilter(XlCellRange range)
        {
            this.AddAutoFilter(range.TopLeft.Column, range.TopLeft.Row, range.BottomRight.Column, range.BottomRight.Row);
        }

        protected virtual void AddAutoFilter(int left, int top, int right, int bottom)
        {
            if (this.exportInfo.Exporter.CurrentColumnIndex != 0)
            {
                this.exportInfo.Sheet.AutoFilterRange = XlCellRange.FromLTRB(left, top, right, bottom);
            }
        }

        public virtual void AddRow()
        {
            this.AddRowCore(new List<object>(), null);
        }

        public void AddRow(TRow gridRow)
        {
            this.CreateRow(gridRow, null, false);
            if (this.exportInfo.Options.CanRaiseAfterAddRow)
            {
                this.exportInfo.RaiseAfterAddRowEvent(gridRow, (DataAwareExportContext<TCol, TRow>) this);
            }
        }

        public void AddRow(CellObject[] values)
        {
            this.AddRowCore(values, delegate (IXlRow row, IXlCell cell, int i) {
                CellObject obj2 = (i < values.Length) ? values[i] : null;
                if (obj2 != null)
                {
                    if (obj2.Value is Image)
                    {
                        Image picture = obj2.Value as Image;
                        ((DataAwareExportContext<TCol, TRow>) this).SetPictureCore(picture, cell.ColumnIndex, cell.RowIndex, picture.Width, picture.Height, XlAnchorType.Absolute);
                        row.HeightInPixels = Math.Max(picture.Height, row.HeightInPixels);
                    }
                    else
                    {
                        cell.Value = XlVariantValue.FromObject(obj2.Value);
                        cell.Formatting = obj2.Formatting.ConvertWith(((DataAwareExportContext<TCol, TRow>) this).exportInfo.AllowHorzLines, ((DataAwareExportContext<TCol, TRow>) this).exportInfo.AllowVertLines);
                        if (!string.IsNullOrEmpty(obj2.Hyperlink))
                        {
                            string displayText = (obj2.Value != null) ? obj2.Value.ToString() : string.Empty;
                            HyperlinkExporter.SetHyperlink(obj2.Hyperlink, displayText, string.Empty, cell, ((DataAwareExportContext<TCol, TRow>) this).exportInfo.Sheet, false);
                        }
                    }
                }
            });
        }

        public void AddRow(object[] values)
        {
            this.AddRowCore(values, delegate (IXlRow row, IXlCell cell, int i) {
                object obj2 = (i < values.Length) ? values[i] : null;
                if (!(obj2 is Image))
                {
                    cell.Value = XlVariantValue.FromObject(obj2);
                    cell.Formatting = new XlFormattingObject().ConvertWith(((DataAwareExportContext<TCol, TRow>) this).exportInfo.AllowHorzLines, ((DataAwareExportContext<TCol, TRow>) this).exportInfo.AllowVertLines);
                }
                else
                {
                    Image picture = obj2 as Image;
                    ((DataAwareExportContext<TCol, TRow>) this).SetPictureCore(picture, cell.ColumnIndex, cell.RowIndex, picture.Width, picture.Height, XlAnchorType.Absolute);
                    row.HeightInPixels = Math.Max(picture.Height, row.HeightInPixels);
                }
            });
        }

        private void AddRowCore(ICollection<object> values, Action<IXlRow, IXlCell, int> value)
        {
            if (values != null)
            {
                IXlRow row = this.exportInfo.Exporter.BeginRow();
                for (int i = 0; (i < values.Count) && this.exportInfo.ComplyWithFormatLimits(i); i++)
                {
                    value(row, this.exportInfo.Exporter.BeginCell(), i);
                    this.exportInfo.Exporter.EndCell();
                }
                this.exportInfo.Exporter.EndRow();
                this.exportInfo.ExportRowIndex++;
            }
        }

        public void ConfigureHeader()
        {
            if (!this.exportInfo.Options.CanRaiseCustomizeSheetSettingsEvent && !this.exportInfo.Options.CanRaiseCustomizeHeaderEvent)
            {
                this.SetFixedHeader(this.GetIndexByRowCount());
            }
        }

        public virtual void CreateColumn(TCol gridColumn)
        {
            if (this.exportInfo.ComplyWithFormatLimits(gridColumn.LogicalPosition))
            {
                IXlColumn column = this.exportInfo.Exporter.BeginColumn();
                column.Formatting = !this.exportInfo.ApplyFormattingToEntireColumn ? FormattingUtils.GetDefault() : this.GetFormattingFromColumn(gridColumn);
                DataAwareExportContext<TCol, TRow>.SetColumnWidth(gridColumn, column);
                if (this.exportInfo.AllowFixedColumns)
                {
                    DataAwareExportContext<TCol, TRow>.SetColumnAsFixed(this.exportInfo.Sheet, gridColumn);
                }
                DataAwareExportContext<TCol, TRow>.SetColumnVisibilityState(gridColumn, column);
                if (this.exportInfo.Options.CanRaiseCustomizeDocumentColumnEvent)
                {
                    CustomizeDocumentColumnEventArgs ea = new CustomizeDocumentColumnEventArgs();
                    ea.DocumentColumn = column;
                    ea.ColumnFieldName = gridColumn.FieldName;
                    this.exportInfo.Options.RaiseCustomizeDocumentColumn(ea);
                }
                this.exportInfo.Exporter.EndColumn();
            }
        }

        public void CreateExportDataGroup(int exportEssenceLevel, int groupRowHandle, int groupId, bool isCollapsed)
        {
            this.CreateExportDataGroup(exportEssenceLevel, groupRowHandle, this.exportInfo.ExportRowIndex, groupId, isCollapsed, true);
        }

        internal void CreateExportDataGroup(int exportEssenceLevel, int groupRowHandle, int groupId, bool isCollapsed, bool showFooter)
        {
            this.CreateExportDataGroup(exportEssenceLevel, groupRowHandle, this.exportInfo.ExportRowIndex, groupId, isCollapsed, showFooter);
        }

        public void CreateExportDataGroup(int exportEssenceLevel, int groupRowHandle, int startGroup, int groupId, bool isCollapsed, bool showFooter = true)
        {
            if (this.exportInfo.GroupsStack.Count < (this.exportInfo.MaxGroupingLevel - 1))
            {
                XlGroup item = new XlGroup {
                    Group = this.exportInfo.Exporter.BeginGroup()
                };
                item.Group.IsCollapsed = isCollapsed;
                item.Group.OutlineLevel = exportEssenceLevel + 1;
                item.StartGroup = startGroup;
                item.GroupId = groupId;
                item.GroupRowHandle = groupRowHandle;
                item.ShowFooter = showFooter;
                if (this.exportInfo.GridGroupedColumns.Count > 0)
                {
                    item.GroupFieldName = this.exportInfo.GridGroupedColumns[exportEssenceLevel].FieldName;
                }
                item.DataRanges = new List<Group>();
                this.exportInfo.GroupsStack.Push(item);
            }
        }

        public virtual void CreateFooter()
        {
            if (this.exportInfo.ShowTotalSummary)
            {
                this.ExecuteExportSummary();
            }
            if (this.exportInfo.Options.CanRaiseCustomizeFooterEvent)
            {
                this.exportInfo.RaiseContextCustomizationEvent(EventType.Footer, (DataAwareExportContext<TCol, TRow>) this);
            }
        }

        public virtual void CreateHeader()
        {
            TRow gridRow = default(TRow);
            this.CreateRowCore(SheetAreaType.Header, gridRow, null, -1);
            if (this.exportInfo.Options.CanRaiseAfterAddRow)
            {
                this.exportInfo.RaiseAfterAddRowEvent(null, (DataAwareExportContext<TCol, TRow>) this);
            }
            this.DataCellsRangeTop += this.exportInfo.ExportRowIndex;
        }

        private void CreateRow(TRow gridRow, Action<IXlRow> rsAction, bool emptyRow)
        {
            if (!emptyRow)
            {
                this.CreateRowCore(SheetAreaType.DataArea, gridRow, rsAction, -1);
            }
            else
            {
                this.AddRow();
            }
        }

        protected virtual void CreateRowCore(SheetAreaType areaType, TRow gridRow, Action<IXlRow> rsAction, int colLimit = -1)
        {
            if (colLimit == -1)
            {
                colLimit = this.exportInfo.Exporter.DocumentOptions.MaxColumnCount;
            }
            IXlRow row = this.exportInfo.Exporter.BeginRow();
            if (rsAction != null)
            {
                rsAction(row);
            }
            for (int i = 0; (i < this.exportInfo.ExportProviders.Count) && (i < colLimit); i++)
            {
                IColumnExportProvider<TRow> provider = this.exportInfo.ExportProviders[i];
                if (provider.CanExportValue(areaType))
                {
                    IXlCell cell = this.exportInfo.Exporter.BeginCell();
                    provider.ExportValue(areaType, cell, gridRow, this.exportInfo.ExportRowIndex);
                    this.exportInfo.Exporter.EndCell();
                }
            }
            this.exportInfo.Exporter.EndRow();
            this.exportInfo.ExportRowIndex++;
        }

        private Action<IXlRow> CustomizeRow(Image image, int widthInPixels, int heightInPixels) => 
            delegate (IXlRow xlrow) {
                ((DataAwareExportContext<TCol, TRow>) this).SetPictureCore(image, 0, ((DataAwareExportContext<TCol, TRow>) this).exportInfo.ExportRowIndex, widthInPixels, heightInPixels, XlAnchorType.OneCell);
                xlrow.HeightInPixels = heightInPixels;
            };

        protected void ExecuteExportSummary()
        {
            this.exportInfo.HelpersProvider.SummaryExporter.ExportTotalSummary();
        }

        protected XlCellFormatting GetFormattingFromColumn(TCol gridColumn)
        {
            XlCellFormatting columnAppearanceFormGridColumn = FormattingUtils.GetColumnAppearanceFormGridColumn(gridColumn, !this.exportInfo.RawDataMode);
            FormattingUtils.PrimaryFormatColumn(gridColumn, columnAppearanceFormGridColumn);
            FormattingUtils.SetBorder(columnAppearanceFormGridColumn, this.exportInfo.AllowHorzLines, this.exportInfo.AllowVertLines);
            return columnAppearanceFormGridColumn;
        }

        protected virtual int GetIndexByRowCount() => 
            (this.exportInfo.View.FixedRowsCount <= 0) ? 0 : (!this.exportInfo.ShowColumnHeaders ? (this.exportInfo.View.FixedRowsCount - 1) : this.exportInfo.View.FixedRowsCount);

        public void InsertImage(Image image, XlCellRange range)
        {
            this.SetPictureCore(image, range.TopLeft.Column, range.TopLeft.Row, range.BottomRight.Column + 1, range.BottomRight.Row + 1, XlAnchorType.TwoCell);
        }

        public void InsertImage(Image image, Size s)
        {
            TRow gridRow = default(TRow);
            this.CreateRow(gridRow, this.CustomizeRow(image, s.Width, s.Height), false);
        }

        public void MergeCells(XlCellRange range)
        {
            this.exportInfo.HelpersProvider.CellMerger.Merge(range);
        }

        public virtual void PrintGroupRowHeader(TRow groupRow)
        {
            if (this.exportInfo.GroupsStack.Count < (this.exportInfo.MaxGroupingLevel - 1))
            {
                IGroupRow<TRow> groupRowCore = groupRow as IGroupRow<TRow>;
                this.CreateRowCore(SheetAreaType.GroupHeader, groupRow, this.SetGroupRowState(groupRowCore), -1);
                if (this.exportInfo.Options.CanRaiseAfterAddRow)
                {
                    this.exportInfo.RaiseAfterAddRowEvent(groupRow, (DataAwareExportContext<TCol, TRow>) this);
                }
            }
        }

        protected static void SetColumnAsFixed(IXlSheet sheet, TCol column)
        {
            if (column.IsFixedLeft)
            {
                sheet.SplitPosition = new XlCellPosition(column.LogicalPosition + 1, sheet.SplitPosition.Row);
            }
        }

        protected static void SetColumnVisibilityState(TCol col, IXlColumn column)
        {
            column.IsHidden = !col.IsVisible;
        }

        protected static void SetColumnWidth(TCol col, IXlColumn column)
        {
            column.WidthInPixels = col.Width;
        }

        public void SetExportSheetSettings()
        {
            this.exportInfo.Sheet.ViewOptions.ShowGridLines = this.exportInfo.ShowGridLines;
            this.exportInfo.Sheet.HeaderFooter.ScaleWithDoc = true;
            string viewCaption = this.exportInfo.View.ViewCaption;
            this.exportInfo.Sheet.HeaderFooter.EvenFooter = viewCaption;
            this.exportInfo.Sheet.HeaderFooter.FirstHeader = viewCaption;
            this.exportInfo.Sheet.HeaderFooter.AlignWithMargins = true;
            if (!this.exportInfo.Sheet.HeaderFooter.DifferentOddEven)
            {
                this.exportInfo.Sheet.HeaderFooter.OddHeader = viewCaption;
            }
            if (this.exportInfo.Options.CanRaiseCustomizeSheetSettingsEvent)
            {
                this.exportInfo.RaiseCustomizeSheetSettingsEvent((DataAwareExportContext<TCol, TRow>) this);
            }
            if (this.exportInfo.Options.CanRaiseCustomizeHeaderEvent)
            {
                this.exportInfo.RaiseContextCustomizationEvent(EventType.Header, (DataAwareExportContext<TCol, TRow>) this);
            }
        }

        public void SetFixedHeader(int bottomRowIndex)
        {
            this.exportInfo.Sheet.SplitPosition = new XlCellPosition(this.exportInfo.Sheet.SplitPosition.Column, bottomRowIndex + 1);
        }

        protected void SetFormatting(IXlCell cell, XlCellFormatting format, bool setBoldFont)
        {
            if (!this.exportInfo.RawDataMode)
            {
                if (format != null)
                {
                    cell.Formatting = format;
                }
                if (setBoldFont)
                {
                    FormattingUtils.SetBoldFont(cell.Formatting);
                }
                FormattingUtils.SetBorder(cell.Formatting, this.exportInfo.AllowHorzLines, this.exportInfo.AllowVertLines);
            }
        }

        protected Action<IXlRow> SetGroupRowState(IGroupRow<TRow> groupRowCore) => 
            delegate (IXlRow row) {
                row.IsCollapsed = groupRowCore.IsCollapsed;
            };

        public void SetGroupSummary(XlGroup group)
        {
            bool flag = (this.exportInfo.PrecalculatedGroupsList.Count == 0) || this.exportInfo.AllowExportSummaryItemsAlignByColumnsInFooter;
            if ((this.exportInfo.Options.ShowGroupSummaries == DefaultBoolean.True) & flag)
            {
                this.exportInfo.HelpersProvider.SummaryExporter.ExportGroupSummary(group);
            }
        }

        private void SetPictureCore(Image picture, int column, int row, int widthInPixels, int heightInPixels, XlAnchorType type)
        {
            if (picture != null)
            {
                new PictureExporter<TCol, TRow>(this.exportInfo).SetPicture(picture, column, row, widthInPixels, heightInPixels, type);
            }
        }

        public void SetPrintTitles()
        {
            this.exportInfo.Sheet.PrintTitles.SetRows(0, 0);
        }

        internal int DataCellsRangeTop { get; set; }
    }
}

