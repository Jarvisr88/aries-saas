namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using DevExpress.Utils.Text;
    using DevExpress.Utils.Text.Internal;
    using DevExpress.XtraExport.Helpers;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class ColumnExportProvider<TCol, TRow> : IColumnExportProvider<TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected readonly TCol targetLocalColumn;
        protected ColumnExportInfo<TCol, TRow> columnInfo;
        protected int index;
        private bool groupColumnHeaderCellHasBeenExported;
        private XlCellFormatting colFormatting;
        private FormatSettings columnFormatSettings;
        private ColumnEditTypes? colEditType;
        private List<XlCellPosition> emptyCellsPositions;
        private readonly HashSet<Type> explicitValueTypes;
        private readonly HashSet<ColumnEditTypes> explicitColumnTypes;
        private XlCellFormatting gHRCFormatting;
        private bool? canExecuteExport;
        protected bool handled;

        public ColumnExportProvider(TCol target, ColumnExportInfo<TCol, TRow> columnInfo, int cIndex)
        {
            HashSet<Type> set = new HashSet<Type> {
                typeof(byte[])
            };
            this.explicitValueTypes = set;
            HashSet<ColumnEditTypes> set1 = new HashSet<ColumnEditTypes>();
            set1.Add(ColumnEditTypes.Sparkline);
            set1.Add(ColumnEditTypes.Image);
            this.explicitColumnTypes = set1;
            this.targetLocalColumn = target;
            this.columnInfo = columnInfo;
            this.index = cIndex;
            Guard.ArgumentNotNull(this.targetLocalColumn, "TargetColumn");
            Guard.ArgumentNotNull(columnInfo, "OptionsProvider");
            Guard.ArgumentNotNull(columnInfo.ExportInfo.Sheet, "Sheet");
            Guard.ArgumentNotNull(columnInfo.ExportInfo.HelpersProvider, "HelpersProvider");
            Guard.ArgumentNotNull(columnInfo.ExpressionConverter, "ExpressionConverter");
            this.RaiseDocumentColumnFiltering(columnInfo);
        }

        private Func<ISummaryItemEx, bool> AlignGroupSummaryExportCondition(XlGroup group)
        {
            int groupLevel = (group.Group != null) ? (group.Group.OutlineLevel - 1) : 0;
            return x => ((x.ShowInColumnFooterName == ((ColumnExportProvider<TCol, TRow>) this).targetLocalColumn.FieldName) && (!x.AlignByColumnInFooter && ((ColumnExportProvider<TCol, TRow>) this).columnInfo.View.RaiseCustomSummaryExists(x, groupLevel, group.GroupRowHandle, true)));
        }

        protected virtual bool AllowExportCustomTypes(IXlCell cell, object cellValue)
        {
            if (!(cell.Value == XlVariantValue.Empty) || (cellValue == null))
            {
                return ((cellValue is TimeSpan) && (((TimeSpan) cellValue) < TimeSpan.Zero));
            }
            bool flag2 = this.explicitValueTypes.Contains(cellValue.GetType());
            return (!this.explicitColumnTypes.Contains(this.ColEditType) && !flag2);
        }

        public bool CanExportValue(SheetAreaType area) => 
            (this.columnInfo.ExportInfo.ComplyWithFormatLimits(this.columnInfo.Exporter.CurrentColumnIndex) && (!this.IsGridBand || (area == SheetAreaType.Header))) && this.CanExecuteExport;

        private object CheckExceptionalValueTypes(object value)
        {
            if (!(value is DateTimeOffset))
            {
                return value;
            }
            if (this.ColumnFormatSettings.FormatType != FormatType.DateTime)
            {
                return value.ToString();
            }
            CultureInfo culture = this.columnInfo.Exporter.CurrentDocument.Options.Culture;
            return ((DateTimeOffset) value).ToString(this.ColumnFormatSettings.FormatString, culture);
        }

        protected virtual bool CheckOptions(SheetAreaType areaType, bool isHandled, string hyperlink)
        {
            bool flag = HyperlinkExporter.CanExportHyperlink(isHandled, hyperlink, this.ColEditType, this.columnInfo.Options.AllowHyperLinks);
            bool suppressHyperlinkMaxCountWarning = this.columnInfo.Options.SuppressHyperlinkMaxCountWarning;
            bool flag3 = (this.columnInfo.Sheet.Hyperlinks.Count < 0xfffa) || !suppressHyperlinkMaxCountWarning;
            return (((flag && !this.columnInfo.RawDataMode) && (areaType == SheetAreaType.DataArea)) & flag3);
        }

        private bool CheckSettings(FormatSettings settings) => 
            (settings != null) && ((settings.ActualDataType != null) || !string.IsNullOrEmpty(settings.FormatString));

        protected virtual void CombineFormatSettings(TRow gridRow, TCol col, IXlCell cell)
        {
            FormatSettings rowCellFormatting = this.columnInfo.View.GetRowCellFormatting(gridRow, col);
            bool flag = this.CheckSettings(gridRow.FormatSettings);
            if (this.CheckSettings(rowCellFormatting))
            {
                cell.Formatting.GetActual(rowCellFormatting);
            }
            else if (flag)
            {
                cell.Formatting.GetActual(gridRow.FormatSettings);
            }
            else
            {
                cell.Formatting.GetActual(this.ColumnFormatSettings);
            }
        }

        private void ConcatenateAlignByColumnItems(IXlCell cell, TRow gridRow, int exportRowIndex, XlGroup group)
        {
            ISummaryItemEx[] items = this.columnInfo.View.GridGroupSummaryItemCollection.Where<ISummaryItemEx>(this.AlignGroupSummaryExportCondition(group)).ToArray<ISummaryItemEx>();
            if (items.Length == 0)
            {
                this.SetValue(cell, gridRow, null, exportRowIndex, SheetAreaType.GroupHeader);
            }
            else if (!this.columnInfo.Options.CanRaiseCustomizeCellEvent || (items.Length != 1))
            {
                this.columnInfo.HelpersProvider.SummaryExporter.ExportItemsAlignedByColumn(items, SheetAreaType.GroupHeader, this.targetLocalColumn, cell, group, false, false);
            }
            else
            {
                ISummaryItemEx item = items[0];
                CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(this.CreateCustomizeSummaryCellInfo(cell, gridRow, group.DataRanges, item, item.GetSummaryValueByGroupId(group.GroupId)), this.columnInfo.ExportInfo);
            }
        }

        protected virtual CellObject CreateCellObject(object value, string hyperlink)
        {
            CellObject obj1 = new CellObject();
            obj1.Value = value;
            obj1.Hyperlink = hyperlink;
            return obj1;
        }

        private CustomizeCellInfo<TCol, TRow> CreateCustomizeCellInfo(IXlCell cell, TRow gridRow, object value, int exportRowIndex, SheetAreaType areaType, string hyperlink) => 
            new CustomizeCellInfo<TCol, TRow> { 
                AreaType = areaType,
                Cell = cell,
                CellValue = value,
                Column = this.targetLocalColumn,
                Row = gridRow,
                ExportRowIndex = exportRowIndex,
                Hyperlink = hyperlink,
                Cellobj = this.CreateCellObject(value, hyperlink),
                View = this.columnInfo.View
            };

        private CustomizeSummaryCellInfo<TCol, TRow> CreateCustomizeSummaryCellInfo(IXlCell cell, TRow gridRow, List<Group> groupDataRanges, ISummaryItemEx item, object summaryValue) => 
            new CustomizeSummaryCellInfo<TCol, TRow> { 
                AreaType = SheetAreaType.GroupHeader,
                Column = this.targetLocalColumn,
                GroupRanges = groupDataRanges,
                Item = item,
                SummaryValue = summaryValue,
                Row = gridRow,
                Cell = cell,
                View = this.columnInfo.View
            };

        protected virtual void CreateDataCell(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            if (!this.GroupColumnHeaderCell() || this.groupColumnHeaderCellHasBeenExported)
            {
                this.ExportFormattedValue(cell, gridRow, exportRowIndex);
            }
            else
            {
                this.ExportGroupColumnHeaderCell(cell, gridRow, exportRowIndex);
                this.groupColumnHeaderCellHasBeenExported = true;
            }
        }

        void IColumnExportProvider<TRow>.ClearCaches()
        {
            this.columnFormatSettings = null;
            this.colEditType = null;
            if (this.emptyCellsPositions != null)
            {
                this.emptyCellsPositions.Clear();
                this.emptyCellsPositions = null;
            }
        }

        private void ExportAlignGroupSummaryInGroupRowHeaderValue(IXlCell cell, TRow gridRow, int exportRowIndex, IGroupRow<TRow> groupRow)
        {
            if (this.targetLocalColumn.LogicalPosition == 0)
            {
                this.SetValue(cell, gridRow, groupRow.GetGroupRowHeader(), exportRowIndex, SheetAreaType.GroupHeader);
            }
            else if ((this.columnInfo.PrecalculatedGroupsList != null) && (this.columnInfo.PrecalculatedGroupsList.Count > 0))
            {
                XlGroup group = this.columnInfo.PrecalculatedGroupsList.FirstOrDefault<XlGroup>(x => x.StartGroup == (exportRowIndex + 1));
                if (this.columnInfo.View.ConcatenateAlignedByColumnsSummaryItems)
                {
                    this.ConcatenateAlignByColumnItems(cell, gridRow, exportRowIndex, group);
                }
                else
                {
                    ISummaryItemEx item = this.columnInfo.View.GridGroupSummaryItemCollection.LastOrDefault<ISummaryItemEx>(this.AlignGroupSummaryExportCondition(group));
                    if (item == null)
                    {
                        this.SetValue(cell, gridRow, null, exportRowIndex, SheetAreaType.GroupHeader);
                    }
                    else
                    {
                        object itemSummaryValue = this.GetItemSummaryValue(item, group);
                        if (this.columnInfo.Options.CanRaiseCustomizeCellEvent)
                        {
                            CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(this.CreateCustomizeSummaryCellInfo(cell, gridRow, group.DataRanges, item, itemSummaryValue), this.columnInfo.ExportInfo);
                        }
                        else
                        {
                            this.columnInfo.HelpersProvider.SummaryExporter.ExportItem(SheetAreaType.GroupHeader, group.DataRanges, itemSummaryValue, item.FieldName, item.DisplayFormat, SummaryExportUtils.ConvertSummaryItemTypeToExcel(item.SummaryType), this.targetLocalColumn, cell, false, false, item.AlignByColumnInFooter);
                        }
                    }
                }
            }
        }

        protected virtual void ExportDataText(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            object obj2 = this.GetValue(gridRow, true);
            obj2 ??= this.GetValue(gridRow, false);
            this.ExportDataValueCore(cell, gridRow, exportRowIndex, obj2);
        }

        protected virtual void ExportDataValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            object rowCellValueCore = this.GetRowCellValueCore(gridRow);
            this.ExportDataValueCore(cell, gridRow, exportRowIndex, rowCellValueCore);
        }

        private void ExportDataValueCore(IXlCell cell, TRow gridRow, int exportRowIndex, object value)
        {
            this.SetValue(cell, gridRow, value, exportRowIndex, SheetAreaType.DataArea);
            if (this.columnInfo.AllowCellMerge)
            {
                this.columnInfo.HelpersProvider.CellMerger.ProcessVerticalMerging(exportRowIndex, gridRow.LogicalPosition, this.targetLocalColumn, cell);
            }
        }

        private void ExportFormattedValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            if (!this.columnInfo.ApplyFormattingToEntireColumn)
            {
                cell.Formatting = this.ColumnFormatting;
            }
            if (gridRow != null)
            {
                this.CombineFormatSettings(gridRow, this.targetLocalColumn, cell);
            }
            this.ExportValue(cell, gridRow, exportRowIndex);
        }

        private void ExportGroupColumnHeaderCell(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            string groupColumnHeader = this.targetLocalColumn.GetGroupColumnHeader();
            cell.Value = XlVariantValue.FromObject(groupColumnHeader);
            this.SetFormatting(cell, null, true);
            this.SetValue(cell, gridRow, groupColumnHeader, exportRowIndex, SheetAreaType.DataArea);
        }

        protected virtual void ExportGroupHeaderValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            IGroupRow<TRow> groupRow = gridRow as IGroupRow<TRow>;
            if (groupRow != null)
            {
                if (this.columnInfo.ExportInfo.AlignGroupSummaryInGroupRow)
                {
                    this.ExportAlignGroupSummaryInGroupRowHeaderValue(cell, gridRow, exportRowIndex, groupRow);
                }
                else
                {
                    object groupHeaderValue = this.GetGroupHeaderValue(groupRow);
                    this.SetValue(cell, gridRow, groupHeaderValue, exportRowIndex, SheetAreaType.GroupHeader);
                }
            }
        }

        protected virtual void ExportHeaderValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            this.SetValue(cell, gridRow, this.targetLocalColumn.Header, exportRowIndex, SheetAreaType.Header);
            this.SetRichText(cell, cell.Value.TextValue, cell.Formatting);
        }

        protected virtual void ExportLink(IXlCell cell, TRow gridRow, string hyperlink)
        {
            string hyperlinkTextFormatString = this.targetLocalColumn.HyperlinkTextFormatString;
            string hyperlinkEditorCaption = this.targetLocalColumn.HyperlinkEditorCaption;
            bool isCellCustomDisplayText = false;
            if (string.IsNullOrEmpty(hyperlinkEditorCaption))
            {
                hyperlinkEditorCaption = this.columnInfo.View.GetRowCellHyperlinkDisplayText(gridRow, this.targetLocalColumn);
                isCellCustomDisplayText = true;
            }
            if (string.IsNullOrEmpty(hyperlinkEditorCaption))
            {
                hyperlinkEditorCaption = cell.Value.ToText().TextValue;
                isCellCustomDisplayText = false;
            }
            HyperlinkExporter.SetHyperlink(hyperlink, hyperlinkEditorCaption, hyperlinkTextFormatString, cell, this.columnInfo.Sheet, isCellCustomDisplayText);
        }

        private void ExportTextValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            if (!this.targetLocalColumn.ExportModeValue)
            {
                this.ExportDataText(cell, gridRow, exportRowIndex);
            }
            else
            {
                this.ExportDataValue(cell, gridRow, exportRowIndex);
            }
        }

        private void ExportValue(IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            if (this.columnInfo.ExportInfo.IsCsvExport)
            {
                this.ExportDataValue(cell, gridRow, exportRowIndex);
            }
            else if (this.columnInfo.Options.TextExportMode == TextExportMode.Text)
            {
                this.ExportTextValue(cell, gridRow, exportRowIndex);
            }
            else
            {
                this.ExportDataValue(cell, gridRow, exportRowIndex);
            }
        }

        public virtual void ExportValue(SheetAreaType area, IXlCell cell, TRow gridRow, int exportRowIndex)
        {
            switch (area)
            {
                case SheetAreaType.DataArea:
                    this.CreateDataCell(cell, gridRow, exportRowIndex);
                    return;

                case SheetAreaType.Header:
                    this.SetFormatting(cell, this.targetLocalColumn.AppearanceHeader, false);
                    this.ExportHeaderValue(cell, gridRow, exportRowIndex);
                    return;

                case SheetAreaType.GroupHeader:
                    this.SetGroupHeaderCellFormatting(gridRow, cell);
                    this.ExportGroupHeaderValue(cell, gridRow, exportRowIndex);
                    return;
            }
        }

        private string GetActualHyperlinkValue(object cellValue, TRow row, TCol col)
        {
            string hyperlinkEditorCaption = string.Empty;
            if (this.ColEditType == ColumnEditTypes.Hyperlink)
            {
                if (!string.IsNullOrEmpty(col.HyperlinkEditorCaption))
                {
                    hyperlinkEditorCaption = col.HyperlinkEditorCaption;
                }
                else
                {
                    string rowCellHyperlink = this.columnInfo.View.GetRowCellHyperlink(row, col);
                    if (string.IsNullOrEmpty(rowCellHyperlink) && (cellValue != null))
                    {
                        rowCellHyperlink = cellValue.ToString();
                    }
                    hyperlinkEditorCaption = rowCellHyperlink;
                }
            }
            return hyperlinkEditorCaption;
        }

        private XlCellFormatting GetFormattingFromColumn(TCol gridColumn)
        {
            XlCellFormatting columnAppearanceFormGridColumn = FormattingUtils.GetColumnAppearanceFormGridColumn(gridColumn, !this.columnInfo.RawDataMode);
            FormattingUtils.PrimaryFormatColumn(gridColumn, columnAppearanceFormGridColumn);
            FormattingUtils.SetBorder(columnAppearanceFormGridColumn, this.columnInfo.AllowHorzLines, this.columnInfo.AllowVertLines);
            return columnAppearanceFormGridColumn;
        }

        protected virtual object GetGroupHeaderValue(IGroupRow<TRow> groupRow) => 
            (this.targetLocalColumn.LogicalPosition == 0) ? groupRow.GetGroupRowHeader() : null;

        protected virtual string GetHyperlink() => 
            string.Empty;

        private object GetItemSummaryValue(ISummaryItemEx item, XlGroup group) => 
            (this.columnInfo.Options.TextExportMode == TextExportMode.Text) ? item.SummaryText : item.GetSummaryValueByGroupId(group.GroupId);

        protected object GetRowCellValueCore(TRow gridRow) => 
            (this.targetLocalColumn.IsGroupColumn || (gridRow == null)) ? null : this.columnInfo.View.GetRowCellValue(gridRow, this.targetLocalColumn);

        private object GetValue(TRow gridRow, bool getText) => 
            !this.targetLocalColumn.IsGroupColumn ? (!getText ? this.GetRowCellValueCore(gridRow) : this.columnInfo.View.GetRowCellDisplayText(gridRow, this.targetLocalColumn)) : null;

        private bool GroupColumnHeaderCell() => 
            this.targetLocalColumn.IsGroupColumn;

        protected virtual bool IgnoreOverride(SheetAreaType area) => 
            false;

        private void RaiseDocumentColumnFiltering(ColumnExportInfo<TCol, TRow> columnInfo)
        {
            if (columnInfo.Options.CanRaiseDocumentColumnFilteringEvent)
            {
                columnInfo.RaiseDocumentColumnFilteringEventArgs(this.targetLocalColumn);
            }
        }

        protected void SetFormatting(IXlCell cell, XlCellFormatting format, bool setBoldFont)
        {
            if (!this.columnInfo.RawDataMode && (cell != null))
            {
                if (format != null)
                {
                    cell.Formatting = format;
                }
                if (setBoldFont)
                {
                    FormattingUtils.SetBoldFont(cell.Formatting);
                }
                FormattingUtils.SetBorder(cell.Formatting, this.columnInfo.AllowHorzLines, this.columnInfo.AllowVertLines);
            }
        }

        protected virtual void SetGroupHeaderCellFormatting(TRow row, IXlCell cell)
        {
            if (this.gHRCFormatting == null)
            {
                if (this.columnInfo.AppearanceView.AppearanceGroupRow != null)
                {
                    this.gHRCFormatting = new XlCellFormatting();
                    this.gHRCFormatting.CopyFrom(this.columnInfo.AppearanceView.AppearanceGroupRow);
                }
                if (this.columnInfo.Exporter.CurrentColumnIndex == 0)
                {
                    this.gHRCFormatting ??= new XlCellFormatting();
                    FormattingUtils.SetBoldFont(this.gHRCFormatting);
                }
            }
            if (XlCellFormatting.Equals(this.gHRCFormatting, FormattingUtils.SpecialAreaDefaultFormatting) && this.columnInfo.ApplyFormattingToEntireColumn)
            {
                FormattingUtils.SetBoldFont(this.gHRCFormatting);
            }
            else
            {
                FormattingUtils.SetBorder(this.gHRCFormatting, this.columnInfo.AllowHorzLines, this.columnInfo.AllowVertLines);
                if ((cell != null) && !this.columnInfo.RawDataMode)
                {
                    cell.Formatting = this.gHRCFormatting;
                }
            }
        }

        protected virtual void SetRichText(IXlCell cell, string text, XlCellFormatting format)
        {
            List<StringBlock> list = DevExpress.Utils.Text.StringParser.Parse((format.Font != null) ? ((float) format.Font.Size) : 11f, text, false);
            XlRichTextString str = new XlRichTextString();
            for (int i = 0; i < list.Count; i++)
            {
                StringBlock block = list[i];
                str.Runs.Add(new XlRichTextRun(block.Text, FormattingUtils.CreateFont(block.FontSettings)));
            }
            bool flag = str.Runs.Any<XlRichTextRun>(new Func<XlRichTextRun, bool>(FormattingUtils.IsRunContainsNewLineChars));
            if (!str.IsPlainText)
            {
                cell.SetRichText(str);
            }
            if (flag)
            {
                FormattingUtils.SetRichTextCellWrap(cell);
            }
        }

        protected virtual void SetRichTextFromRuns(IXlCell cell, IList<XlRichTextRun> runs)
        {
            if (runs != null)
            {
                XlRichTextString str = new XlRichTextString();
                bool flag = false;
                for (int i = 0; i < runs.Count; i++)
                {
                    XlRichTextRun run = runs[i];
                    FormattingUtils.ValidateTextRun(run);
                    flag = FormattingUtils.IsRunContainsNewLineChars(run);
                    str.Runs.Add(run);
                }
                cell.SetRichText(str);
                if (flag)
                {
                    FormattingUtils.SetRichTextCellWrap(cell);
                }
            }
        }

        protected void SetValue(IXlCell cell, TRow gridRow, object value, int exportRowIndex, SheetAreaType areaType)
        {
            string hyperlink = this.GetActualHyperlinkValue(value, gridRow, this.targetLocalColumn);
            object cellValue = value;
            if (this.columnInfo.Options.CanRaiseCustomizeCellEvent)
            {
                CustomizeCellInfo<TCol, TRow> info = this.CreateCustomizeCellInfo(cell, gridRow, value, exportRowIndex, areaType, hyperlink);
                this.handled = CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeCellEvent(info, this.columnInfo.ExportInfo);
                cellValue = info.CellValue;
                hyperlink = info.Hyperlink;
            }
            this.SetValueCore(cell, cellValue, exportRowIndex, this.IgnoreOverride(areaType));
            if (this.CheckOptions(areaType, this.handled, hyperlink))
            {
                this.ExportLink(cell, gridRow, hyperlink);
            }
            if (cell.Value.IsEmpty && ((areaType == SheetAreaType.DataArea) && (exportRowIndex < XlCellPosition.MaxRowCount)))
            {
                this.EmptyCellsPositions.Add(cell.Position);
                this.HasEmptyCells = true;
            }
        }

        protected virtual void SetValueCore(IXlCell cell, object value, int rowIndex, bool ignoreOverride = false)
        {
            cell.Value = XlVariantValue.FromObject(this.CheckExceptionalValueTypes(value));
            if (this.AllowExportCustomTypes(cell, value))
            {
                cell.Value = XlVariantValue.FromObject(value.ToString());
            }
        }

        protected XlCellFormatting ColumnFormatting
        {
            get
            {
                this.colFormatting ??= this.GetFormattingFromColumn(this.targetLocalColumn);
                return this.colFormatting;
            }
        }

        protected FormatSettings ColumnFormatSettings
        {
            get
            {
                this.columnFormatSettings ??= this.targetLocalColumn.FormatSettings;
                return this.columnFormatSettings;
            }
        }

        protected ColumnEditTypes ColEditType
        {
            get
            {
                if (this.colEditType == null)
                {
                    this.colEditType = new ColumnEditTypes?(this.targetLocalColumn.ColEditType);
                }
                return this.colEditType.Value;
            }
        }

        public bool Used { get; set; }

        public List<XlCellPosition> EmptyCellsPositions
        {
            get
            {
                this.emptyCellsPositions ??= new List<XlCellPosition>();
                return this.emptyCellsPositions;
            }
        }

        public int Position =>
            this.index;

        public string FieldName =>
            this.targetLocalColumn.FieldName;

        public bool HasEmptyCells { get; protected set; }

        private bool IsGridBand =>
            this.targetLocalColumn is IGridBand;

        public bool CanExecuteExport
        {
            get
            {
                if (this.canExecuteExport == null)
                {
                    this.canExecuteExport = (this.columnInfo.View.OptionsView.ShowGroupedColumns || this.columnInfo.ExportInfo.AlignGroupSummaryInGroupRow) ? true : new bool?((this.targetLocalColumn.GroupIndex == -1) || this.IsGridBand);
                }
                return this.canExecuteExport.Value;
            }
        }
    }
}

