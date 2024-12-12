namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Data;
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraExport.Xls;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class SummaryExportHelper<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private IEnumerable<ISummaryItemEx> groupSummaryItems;
        private int groupFooterRowsCnt;
        private bool allowExportGroupSummary;
        private Dictionary<string, int> customSummaryByColumnsTable;

        public SummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.IgnoreHiddenGroupValues = false;
            if (!exportInfo.AllowExportSummaryItemsAlignByColumnsInFooter || !exportInfo.AlignGroupSummaryInGroupRow)
            {
                this.groupSummaryItems = exportInfo.View.GridGroupSummaryItemCollection;
            }
            else
            {
                Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__4_0;
                if (<>c<TCol, TRow>.<>9__4_0 == null)
                {
                    Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__4_0;
                    predicate = <>c<TCol, TRow>.<>9__4_0 = x => x.AlignByColumnInFooter;
                }
                this.groupSummaryItems = exportInfo.View.GridGroupSummaryItemCollection.Where<ISummaryItemEx>(predicate);
            }
            this.groupFooterRowsCnt = this.FooterRowsCount(this.groupSummaryItems);
            this.allowExportGroupSummary = this.CheckFooterForValues();
        }

        protected virtual List<XlCellRange> CalcSummaryRange(SheetAreaType areaType, IList<Group> gRanges, int colPosition, string fieldName, XlSummary summaryType, bool isRecursive, bool alignByColumnsInFooter)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            SheetAreaType type = (!base.ExportInfo.AlignGroupSummaryInGroupRow || alignByColumnsInFooter) ? SheetAreaType.GroupFooter : SheetAreaType.GroupHeader;
            if (areaType == type)
            {
                list = this.GetRangeList(gRanges, colPosition, fieldName);
            }
            return ((areaType != SheetAreaType.TotalFooter) ? list : (!this.CheckConstuctCompositeRange(colPosition, fieldName, summaryType) ? this.GetFullSheetRange(colPosition, base.ExportInfo.ExportRowIndex, fieldName) : this.GetRangeList(colPosition, fieldName, base.ExportInfo.GroupsList)));
        }

        private bool CheckConstuctCompositeRange(int colPosition, string fieldName, XlSummary summaryType) => 
            ((summaryType != XlSummary.CountA) || !this.ColumnHasSpecificConditions(colPosition, fieldName)) ? base.ExportInfo.Options.CalcTotalSummaryOnCompositeRange : true;

        private bool CheckFooterForValues()
        {
            bool flag;
            using (IEnumerator<TCol> enumerator = base.ExportInfo.GridColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TCol current = enumerator.Current;
                        ISummaryItemEx itemByKey = this.GetItemByKey(this.groupSummaryItems, current.FieldName);
                        if (!base.ExportInfo.CanExportCurrentColumn(current) || (itemByKey == null))
                        {
                            continue;
                        }
                        flag = this.CheckGroupSummaryOptions();
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

        private IXlFormulaParameter CheckFormulaLength(string itemFieldName, string itemDisplayFormat, XlSummary summaryType, IXlCell cell, TCol gridColumn, List<XlCellRange> ranges, bool eventHandled, IXlFormulaParameter formula, IXlFormulaEngine formulaEngine, bool concatenate = false)
        {
            int num;
            bool flag = false;
            if (this.CustomSummaryByColumnsTable.TryGetValue(gridColumn.FieldName, out num))
            {
                flag = num > 0;
            }
            if (!flag && this.FormulaGetInvalidLength(formula))
            {
                List<XlCellRange> list = new List<XlCellRange>();
                XlCellRange range = ranges.FirstOrDefault<XlCellRange>();
                XlCellRange range2 = ranges.LastOrDefault<XlCellRange>();
                if ((range != null) && (range2 != null))
                {
                    list.Add(new XlCellRange(new XlCellPosition(range.LastColumn, range.FirstRow), new XlCellPosition(range2.LastColumn, range2.LastRow)));
                }
                formula = this.GetCellFormula(formulaEngine, itemFieldName, itemDisplayFormat, summaryType, cell, gridColumn, list, eventHandled, concatenate, false);
            }
            return formula;
        }

        private bool CheckGroupSummaryOptions() => 
            (base.ExportInfo.View.GridGroupSummaryItemCollection != null) && base.ExportInfo.View.OptionsView.ShowGroupFooter;

        private bool CheckItemFormatString(FormatStringParser formatStringEx) => 
            !string.IsNullOrEmpty(formatStringEx.Prefix) || !string.IsNullOrEmpty(formatStringEx.Postfix);

        protected virtual bool CheckRecursive(ISummaryItemEx item) => 
            true;

        private int CheckRowWithCustomSummaryExists(int groupLevel, int groupRowHandle, List<ISummaryItemEx> tempCollection)
        {
            int num = 0;
            foreach (TCol local in base.ExportInfo.GridColumns)
            {
                if (base.ExportInfo.CanExportCurrentColumn(local) && (local != null))
                {
                    ISummaryItemEx itemByKey = this.GetItemByKey(tempCollection, local.FieldName);
                    if ((itemByKey != null) && base.ExportInfo.View.RaiseCustomSummaryExists(itemByKey, groupLevel, groupRowHandle, true))
                    {
                        num++;
                        continue;
                    }
                    tempCollection.Remove(itemByKey);
                }
            }
            return num;
        }

        private bool CheckTotalSummaryOptions() => 
            (base.ExportInfo.View.GridTotalSummaryItemCollection != null) && base.ExportInfo.View.OptionsView.ShowFooter;

        protected bool ColumnHasSpecificConditions(int colPosition, string fieldName) => 
            (colPosition == 0) || this.CountBlankCells(fieldName);

        private IXlFormulaParameter ConstructParamSubtotal(TCol gridColumn, List<XlCellRange> ranges, XlSummary summaryType)
        {
            IColumnExportProvider<TRow> columnProvider = this.GetColumnProvider(gridColumn.FieldName);
            IXlFormulaParameter item = XlFunc.Subtotal(ranges, summaryType, this.IgnoreHiddenGroupValues);
            if ((summaryType == XlSummary.CountA) && this.CountBlankCells(gridColumn.FieldName))
            {
                List<IXlFormulaParameter> list = new List<IXlFormulaParameter>();
                if (ranges.Count <= 1)
                {
                    if (ranges.Count > 0)
                    {
                        IXlFormulaParameter[] parameters = new IXlFormulaParameter[] { ranges[0] };
                        list.Add(XlFunc.CountA(parameters));
                        list.Add(XlFunc.CountBlank(ranges[0]));
                    }
                }
                else
                {
                    list.Add(item);
                    for (int i = 0; i < ranges.Count; i++)
                    {
                        XlCellRange range = ranges[i];
                        if (this.RangeContainsEmptyCells(range, columnProvider.EmptyCellsPositions))
                        {
                            list.Add(XlFunc.CountBlank(range));
                        }
                    }
                }
                item = XlFunc.Sum(list.ToArray());
            }
            return item;
        }

        private IXlFormulaParameter ConstructSpecificText(IXlFormulaParameter paramSubtotal, bool isDateTime, FormatStringParser formatStringEx)
        {
            IXlFormulaParameter thenParam = XlFunc.Text(paramSubtotal, StandardFormats.IntegerNumber, isDateTime);
            return XlFunc.If(XlOper.Equal(paramSubtotal, XlFunc.Trunc(paramSubtotal)), thenParam, XlFunc.Text(paramSubtotal, formatStringEx.ValueFormat, isDateTime));
        }

        protected virtual void CorrectExportRowIndex(bool correct)
        {
            if (correct)
            {
                ExportInfo<TCol, TRow> exportInfo = base.ExportInfo;
                exportInfo.ExportRowIndex--;
            }
        }

        private bool CountBlankCells(string fieldName)
        {
            IColumnExportProvider<TRow> columnProvider = this.GetColumnProvider(fieldName);
            return ((columnProvider != null) && (columnProvider.HasEmptyCells && base.ExportInfo.Options.SummaryCountBlankCells));
        }

        public override void Execute()
        {
        }

        protected virtual void ExecuteCore(FooterAreaType areaType, int index, Action<ISummaryItemEx, TCol, IXlCell> action, List<ISummaryItemEx> tempCollection)
        {
            base.ExportInfo.Exporter.BeginRow();
            foreach (TCol local in base.ExportInfo.GridColumns)
            {
                if (base.ExportInfo.CanExportCurrentColumn(local))
                {
                    IXlCell cell = base.ExportInfo.Exporter.BeginCell();
                    if (local != null)
                    {
                        ISummaryItemEx itemByKey = this.GetItemByKey(tempCollection, local.FieldName);
                        if (itemByKey != null)
                        {
                            action(itemByKey, local, cell);
                        }
                        else if (base.ExportInfo.Options.CanRaiseCustomizeCellEvent)
                        {
                            CustomizeSummaryCellInfo<TCol, TRow> info = new CustomizeSummaryCellInfo<TCol, TRow> {
                                AreaType = (areaType == FooterAreaType.GroupFooter) ? SheetAreaType.GroupFooter : SheetAreaType.TotalFooter,
                                Column = local,
                                Cell = cell,
                                View = base.ExportInfo.View,
                                Options = base.ExportInfo.Options,
                                ExportRowIndex = base.ExportInfo.ExportRowIndex
                            };
                            CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(info, base.ExportInfo);
                        }
                        tempCollection.Remove(itemByKey);
                    }
                    base.ExportInfo.Exporter.EndCell();
                }
            }
            base.ExportInfo.Exporter.EndRow();
        }

        private void ExportCustomSummaryValue(SheetAreaType type, IXlCell cell, object displayValue, string itemDisplayFormat, string itemFieldName, bool eventHandled)
        {
            this.SetFormulaCellFormat(type, itemFieldName, cell, eventHandled);
            if (!string.IsNullOrEmpty(new FormatStringParser(itemDisplayFormat, itemFieldName).Prefix))
            {
                string str = string.Format(itemDisplayFormat, new object[] { displayValue, itemFieldName });
                cell.Value = XlVariantValue.FromObject(str);
            }
            else
            {
                cell.Value = XlVariantValue.FromObject(displayValue);
                if (cell.Formatting != null)
                {
                    cell.Formatting.NetFormatString = itemDisplayFormat;
                }
            }
        }

        public void ExportGroupSummary(DevExpress.Printing.ExportHelpers.XlGroup group)
        {
            if (this.AllowExportGroupSummary && group.ShowFooter)
            {
                ExportInfo<TCol, TRow> exportInfo = base.ExportInfo;
                exportInfo.ExportRowIndex += this.groupFooterRowsCnt;
                this.SummaryExportCore(FooterAreaType.GroupFooter, group, this.groupSummaryItems, this.groupFooterRowsCnt, delegate (ISummaryItemEx item, TCol gridColumn, IXlCell cell) {
                    object summaryValueByGroupId = item.GetSummaryValueByGroupId(group.GroupId);
                    XlSummary summaryType = SummaryExportUtils.ConvertSummaryItemTypeToExcel(item.SummaryType);
                    IList<Group> groupSummaryDataRanges = ((SummaryExportHelper<TCol, TRow>) this).GetGroupSummaryDataRanges(group, item);
                    if (!((SummaryExportHelper<TCol, TRow>) this).ExportInfo.Options.CanRaiseCustomizeCellEvent)
                    {
                        ((SummaryExportHelper<TCol, TRow>) this).ExportItem(SheetAreaType.GroupFooter, groupSummaryDataRanges, summaryValueByGroupId, item.FieldName, item.DisplayFormat, summaryType, gridColumn, cell, false, ((SummaryExportHelper<TCol, TRow>) this).CheckRecursive(item), item.AlignByColumnInFooter);
                    }
                    else
                    {
                        CustomizeSummaryCellInfo<TCol, TRow> info = new CustomizeSummaryCellInfo<TCol, TRow> {
                            AreaType = SheetAreaType.GroupFooter,
                            GroupRanges = groupSummaryDataRanges,
                            SummaryValue = summaryValueByGroupId,
                            Column = gridColumn,
                            Cell = cell,
                            RowHandle = group.GroupRowHandle,
                            View = ((SummaryExportHelper<TCol, TRow>) this).ExportInfo.View,
                            Item = item
                        };
                        CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(info, ((SummaryExportHelper<TCol, TRow>) this).ExportInfo);
                    }
                });
            }
        }

        internal void ExportItem(SheetAreaType areaType, IList<Group> gRanges, object summaryValue, string itemFieldName, string itemDisplayFormat, XlSummary summaryType, TCol gridColumn, IXlCell cell, bool eventHandled, bool isRecursive, bool alignByColumnInFooter)
        {
            int columnPosition = this.GetColumnPosition(itemFieldName);
            if (columnPosition == -3)
            {
                columnPosition = gridColumn.LogicalPosition;
            }
            TCol local1 = base.ExportInfo.ColumnsInfoColl[itemFieldName];
            TCol local3 = local1;
            if (local1 == null)
            {
                TCol local2 = local1;
                local3 = gridColumn;
            }
            TCol sourceColumn = local3;
            bool isCount = summaryType == XlSummary.CountA;
            List<XlCellRange> ranges = this.CalcSummaryRange(areaType, gRanges, columnPosition, this.GetCorrectFieldName(itemFieldName, gridColumn, isCount), summaryType, isRecursive, alignByColumnInFooter);
            if (this.SetLiveSummaries(summaryType, columnPosition, sourceColumn))
            {
                this.ExportSummaryItem(areaType, itemFieldName, itemDisplayFormat, summaryType, cell, sourceColumn, ranges, eventHandled);
            }
            else
            {
                this.ExportCustomSummaryValue(areaType, cell, summaryValue, itemDisplayFormat, itemFieldName, eventHandled);
            }
        }

        internal void ExportItemsAlignedByColumn(ISummaryItemEx[] items, SheetAreaType areaType, TCol gridColumn, IXlCell cell, DevExpress.Printing.ExportHelpers.XlGroup group, bool eventHandled, bool isRecursive)
        {
            this.SetFormulaCellFormat(SheetAreaType.GroupFooter, gridColumn.FieldName, cell, eventHandled);
            if ((items != null) && (items.Length != 0))
            {
                IXlFormulaEngine formulaEngine = base.ExportInfo.Exporter.FormulaEngine;
                IXlFormulaParameter[] parameters = new IXlFormulaParameter[items.Length];
                for (int i = 0; i < items.Length; i++)
                {
                    ISummaryItemEx ex = items[i];
                    int columnPosition = this.GetColumnPosition(ex.FieldName);
                    if (columnPosition == -3)
                    {
                        columnPosition = gridColumn.LogicalPosition;
                    }
                    TCol local1 = base.ExportInfo.ColumnsInfoColl[ex.FieldName];
                    TCol sourceColumn = local1;
                    if (local1 == null)
                    {
                        TCol local2 = local1;
                        sourceColumn = gridColumn;
                    }
                    XlSummary summaryType = SummaryExportUtils.ConvertSummaryItemTypeToExcel(ex.SummaryType);
                    bool isCount = summaryType == XlSummary.CountA;
                    List<XlCellRange> ranges = this.CalcSummaryRange(areaType, group.DataRanges, columnPosition, this.GetCorrectFieldName(ex.FieldName, gridColumn, isCount), summaryType, isRecursive, false);
                    if (this.SetLiveSummaries(summaryType, columnPosition, sourceColumn))
                    {
                        bool insertConcatenateSeparator = i != (items.Length - 1);
                        IXlFormulaParameter formula = this.GetCellFormula(formulaEngine, ex.FieldName, ex.DisplayFormat, summaryType, cell, gridColumn, ranges, eventHandled, true, insertConcatenateSeparator);
                        parameters[i] = this.CheckFormulaLength(ex.FieldName, ex.DisplayFormat, summaryType, cell, gridColumn, ranges, eventHandled, formula, base.ExportInfo.Exporter.FormulaEngine, true);
                    }
                    else if (!string.IsNullOrEmpty(new FormatStringParser(ex.DisplayFormat, ex.FieldName).Prefix))
                    {
                        string str2 = string.Format(ex.DisplayFormat, ex.GetSummaryValueByGroupId(group.GroupId));
                        if (i != (items.Length - 1))
                        {
                            str2 = str2 + ", ";
                        }
                        parameters[i] = formulaEngine.Param(str2);
                    }
                }
                cell.SetFormula((parameters.Length == 1) ? parameters[0] : formulaEngine.Concatenate(parameters));
            }
        }

        private void ExportSummaryItem(SheetAreaType areaType, string itemFieldName, string itemDisplayFormat, XlSummary summaryType, IXlCell cell, TCol gridColumn, List<XlCellRange> ranges, bool eventHandled)
        {
            this.SetFormulaCellFormat(areaType, itemFieldName, cell, eventHandled);
            IXlFormulaEngine formulaEngine = base.ExportInfo.Exporter.FormulaEngine;
            IXlFormulaParameter formula = this.GetCellFormula(formulaEngine, itemFieldName, itemDisplayFormat, summaryType, cell, gridColumn, ranges, eventHandled, false, false);
            if (areaType != SheetAreaType.TotalFooter)
            {
                formula = this.CheckFormulaLength(itemFieldName, itemDisplayFormat, summaryType, cell, gridColumn, ranges, eventHandled, formula, formulaEngine, false);
            }
            cell.SetFormula(formula);
        }

        public virtual void ExportTotalSummary()
        {
            if (this.AllowExportTotalSummary && (base.ExportInfo.ExportRowIndex != this.GetStartRangePosition()))
            {
                List<ISummaryItemEx> totalSummaryItems = base.ExportInfo.View.GridTotalSummaryItemCollection.ToList<ISummaryItemEx>();
                this.ExportTotalSummaryFooterCore(totalSummaryItems);
            }
        }

        protected void ExportTotalSummaryFooterCore(List<ISummaryItemEx> totalSummaryItems)
        {
            int totalFooterRowsCnt = this.FooterRowsCount(totalSummaryItems);
            this.ModifyRowIndex(totalFooterRowsCnt);
            DevExpress.Printing.ExportHelpers.XlGroup group = new DevExpress.Printing.ExportHelpers.XlGroup();
            this.SummaryExportCore(FooterAreaType.TotalFooter, group, totalSummaryItems, totalFooterRowsCnt, delegate (ISummaryItemEx item, TCol gridColumn, IXlCell cell) {
                object summaryValue = item.SummaryValue;
                XlSummary summaryType = SummaryExportUtils.ConvertSummaryItemTypeToExcel(item.SummaryType);
                if (!base.ExportInfo.Options.CanRaiseCustomizeCellEvent)
                {
                    base.ExportItem(SheetAreaType.TotalFooter, null, summaryValue, item.FieldName, item.DisplayFormat, summaryType, gridColumn, cell, false, this.CheckRecursive(item), item.AlignByColumnInFooter);
                }
                else
                {
                    CustomizeSummaryCellInfo<TCol, TRow> info = new CustomizeSummaryCellInfo<TCol, TRow> {
                        AreaType = SheetAreaType.TotalFooter,
                        Item = item,
                        SummaryValue = summaryValue,
                        Column = gridColumn,
                        Cell = cell,
                        View = base.ExportInfo.View
                    };
                    if ((summaryValue != null) && (summaryType == ((XlSummary) 0)))
                    {
                        info.CellValue = summaryValue;
                    }
                    CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(info, base.ExportInfo);
                }
            });
        }

        internal int FooterRowCountForGroup(IEnumerable<ISummaryItemEx> collection, int groupLevel, int groupRowHandle)
        {
            IEnumerable<ISummaryItemEx> enumerable = from x in collection
                where ((SummaryExportHelper<TCol, TRow>) this).ExportInfo.View.RaiseCustomSummaryExists(x, groupLevel, groupRowHandle, true)
                select x;
            return this.FooterRowsCount(enumerable);
        }

        protected internal virtual int FooterRowsCount(IEnumerable<ISummaryItemEx> collection)
        {
            List<ISummaryItemEx> items = collection.ToList<ISummaryItemEx>();
            int num = 0;
            int num2 = 0;
            foreach (TCol local in base.ExportInfo.GridColumns)
            {
                while (true)
                {
                    if ((local == null) || (this.GetItemByKey(items, local.FieldName) == null))
                    {
                        if (num <= num2)
                        {
                            num = 0;
                        }
                        else
                        {
                            num2 = num;
                            num = 0;
                        }
                        break;
                    }
                    num++;
                    items.Remove(this.GetItemByKey(items, local.FieldName));
                }
            }
            return num2;
        }

        private bool FormulaGetInvalidLength(IXlFormulaParameter formula)
        {
            string str = formula.ToString(base.ExportInfo.Exporter.CurrentCulture);
            if (base.ExportInfo.Options.ExportTarget == ExportTarget.Xlsx)
            {
                return (str.Length > 0x2000);
            }
            if ((base.ExportInfo.Options.ExportTarget != ExportTarget.Xls) || (str.Length <= 900))
            {
                return false;
            }
            XlsDataAwareExporter exporter = base.ExportInfo.Exporter as XlsDataAwareExporter;
            return (new XlFormulaConverter(new XlsDataAwareExporterOptions()).Convert(formula).GetBytes(exporter).Length > 0x708);
        }

        private IXlFormulaParameter GetCellFormula(IXlFormulaEngine engine, string itemFieldName, string itemDisplayFormat, XlSummary summaryType, IXlCell cell, TCol gridColumn, List<XlCellRange> ranges, bool eventHandled, bool concatenate = false, bool insertConcatenateSeparator = false)
        {
            FormatStringParser formatStringEx = new FormatStringParser(itemDisplayFormat, itemFieldName);
            IXlFormulaParameter parameter = this.ConstructParamSubtotal(gridColumn, ranges, summaryType);
            IXlFormulaParameter parameter2 = parameter;
            bool isDateTimeFormatString = (cell.Formatting != null) && cell.Formatting.IsDateTimeFormatString;
            string str = ", ";
            if (this.CheckItemFormatString(formatStringEx))
            {
                IXlFormulaParameter parameter3 = engine.Text(parameter, formatStringEx.ValueFormat, isDateTimeFormatString);
                if (this.IsFormatExceptional(formatStringEx.UnionString))
                {
                    parameter3 = this.ConstructSpecificText(parameter, isDateTimeFormatString, formatStringEx);
                }
                IXlFormulaParameter parameter4 = engine.Param(formatStringEx.Prefix);
                IXlFormulaParameter parameter5 = engine.Param(formatStringEx.Postfix);
                if (insertConcatenateSeparator)
                {
                    parameter5 = string.IsNullOrEmpty(formatStringEx.Postfix) ? engine.Param(str) : engine.Param(formatStringEx.Postfix + str);
                }
                IXlFormulaParameter[] parameters = new IXlFormulaParameter[] { parameter4, parameter3, parameter5 };
                parameter2 = engine.Concatenate(parameters);
            }
            else
            {
                if (concatenate && !string.IsNullOrEmpty(itemDisplayFormat))
                {
                    IXlFormulaParameter parameter7 = engine.Text(parameter2, itemDisplayFormat, isDateTimeFormatString);
                    if (insertConcatenateSeparator)
                    {
                        IXlFormulaParameter[] parameters = new IXlFormulaParameter[] { parameter7, engine.Param(str) };
                        parameter7 = engine.Concatenate(parameters);
                    }
                    parameter2 = parameter7;
                }
                if (!eventHandled)
                {
                    this.SetSummaryCellFormat(cell.Formatting, summaryType, gridColumn, formatStringEx);
                }
            }
            return parameter2;
        }

        protected virtual int GetColumnPosition(string fieldName) => 
            !string.IsNullOrEmpty(fieldName) ? base.ExportInfo.ColumnsInfoColl.IndexOf(fieldName) : -3;

        protected IColumnExportProvider<TRow> GetColumnProvider(string fieldName) => 
            base.ExportInfo.ExportProviders.FirstOrDefault<IColumnExportProvider<TRow>>(x => x.FieldName == fieldName);

        protected virtual string GetCorrectFieldName(string itemFieldName, TCol gridColumn, bool isCount) => 
            !string.IsNullOrEmpty(itemFieldName) ? itemFieldName : gridColumn.FieldName;

        protected virtual List<XlCellRange> GetFullSheetRange(int columnPosition, int endRangeRow, string fieldName) => 
            this.GetUnionRange(columnPosition, this.GetStartRangePosition(), endRangeRow, fieldName);

        protected virtual IList<Group> GetGroupSummaryDataRanges(DevExpress.Printing.ExportHelpers.XlGroup group, ISummaryItemEx item) => 
            group.DataRanges;

        protected ISummaryItemEx GetItemByKey(IEnumerable<ISummaryItemEx> items, string fieldName) => 
            string.IsNullOrEmpty(fieldName) ? null : items.FirstOrDefault<ISummaryItemEx>(item => (item.ShowInColumnFooterName == fieldName));

        protected virtual List<XlCellRange> GetRangeList(IList<Group> dataRanges, int columnPosition, string fieldName)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            if (dataRanges != null)
            {
                for (int i = 0; i < dataRanges.Count; i++)
                {
                    Group group = dataRanges[i];
                    list.Add(new XlCellRange(new XlCellPosition(columnPosition, dataRanges[i].Start), new XlCellPosition(columnPosition, group.End - 1)));
                }
            }
            return list;
        }

        protected virtual List<XlCellRange> GetRangeList(int columnPosition, string fieldName, List<Group> groupsList)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            for (int i = 0; i < groupsList.Count; i++)
            {
                Group objA = groupsList[i];
                int end = objA.End;
                if (Equals(objA, base.ExportInfo.GroupsList.Last<Group>()))
                {
                    end--;
                }
                list.Add(new XlCellRange(new XlCellPosition(columnPosition, objA.Start), new XlCellPosition(columnPosition, end)));
            }
            return list;
        }

        protected virtual int GetStartRangePosition() => 
            ((base.ExportInfo.GroupsList == null) || (base.ExportInfo.GroupsList.Count <= 0)) ? ((base.ExportInfo.Options.ShowColumnHeaders != DefaultBoolean.True) ? 0 : 1) : base.ExportInfo.GroupsList[0].Start;

        protected virtual List<XlCellRange> GetUnionRange(int columnPosition, int startRangeRow, int endRangeRow, string fieldName) => 
            new List<XlCellRange> { new XlCellRange(new XlCellPosition(columnPosition, startRangeRow), new XlCellPosition(columnPosition, endRangeRow - 1)) };

        private bool IsFormatExceptional(string valueToCheck) => 
            valueToCheck.Contains(StandardFormats.OneDecimalPlace) || valueToCheck.Contains(StandardFormats.OneDecimalPlaceDP);

        protected virtual void ModifyRowIndex(int totalFooterRowsCnt)
        {
        }

        private bool RangeContainsEmptyCells(XlCellRange range, List<XlCellPosition> emptyCellsPositions)
        {
            for (int i = 0; i < emptyCellsPositions.Count; i++)
            {
                if (range.Contains(emptyCellsPositions[i]))
                {
                    return true;
                }
            }
            return false;
        }

        protected virtual void RemoveSkippedRowItems(FooterAreaType areaType, int ind, List<ISummaryItemEx> tempCollection)
        {
            foreach (TCol local in base.ExportInfo.GridColumns)
            {
                if ((local != null) && base.ExportInfo.CanExportCurrentColumn(local))
                {
                    tempCollection.Remove(this.GetItemByKey(tempCollection, local.FieldName));
                }
            }
        }

        private void SetFormulaCellFormat(SheetAreaType type, string itemFieldName, IXlCell cell, bool eventHandled)
        {
            TCol gridColumn = base.ExportInfo.ColumnsInfoColl[itemFieldName];
            if (!eventHandled)
            {
                cell.Formatting ??= new XlCellFormatting();
                XlCellFormatting appearanceFooter = (type == SheetAreaType.GroupFooter) ? base.ExportInfo.View.Appearance.AppearanceGroupFooter : base.ExportInfo.View.Appearance.AppearanceFooter;
                FormattingUtils.SetCellFormatting(cell.Formatting, gridColumn, appearanceFooter, base.ExportInfo.AllowHorzLines, base.ExportInfo.AllowVertLines, !base.ExportInfo.RawDataMode);
            }
        }

        private bool SetLiveSummaries(XlSummary summaryType, int columnIndex, IColumn sourceColumn)
        {
            bool flag = (sourceColumn != null) && (!(sourceColumn.FormatSettings.ActualDataType == typeof(string)) || (summaryType == XlSummary.CountA));
            return ((((summaryType != ((XlSummary) 0)) && (columnIndex != -1)) & flag) && (base.ExportInfo.Options.TextExportMode == TextExportMode.Value));
        }

        protected void SetSummaryCellFormat(XlCellFormatting format, XlSummary summaryType, TCol gridColumn, FormatStringParser formatStringEx)
        {
            bool flag = summaryType == XlSummary.CountA;
            string str = !flag ? gridColumn.FormatSettings.FormatString : string.Empty;
            string formatString = !string.IsNullOrEmpty(formatStringEx.UnionString) ? formatStringEx.UnionString : str;
            FormattingUtils.SetCellFormatting(format, base.ExportInfo.AllowHorzLines, base.ExportInfo.AllowVertLines, formatString, gridColumn, !base.ExportInfo.RawDataMode, !flag);
        }

        protected virtual void SummaryExportCore(FooterAreaType areaType, DevExpress.Printing.ExportHelpers.XlGroup group, IEnumerable<ISummaryItemEx> summaryCollection, int numOfRows, Action<ISummaryItemEx, TCol, IXlCell> action)
        {
            List<ISummaryItemEx> tempCollection = summaryCollection.ToList<ISummaryItemEx>();
            for (int i = 0; i < numOfRows; i++)
            {
                int areaRowHandle = (numOfRows == 1) ? -1 : i;
                if (base.ExportInfo.Options.CanRaiseSkipFooterRowEvent)
                {
                    base.ExportInfo.RaiseSkipRowEvent(areaType, areaRowHandle, group);
                }
                if (base.ExportInfo.CanSkipRow())
                {
                    base.ExportInfo.ResetSkipRowIndex();
                    this.CorrectExportRowIndex(areaType == FooterAreaType.GroupFooter);
                    this.RemoveSkippedRowItems(areaType, i, tempCollection);
                }
                else
                {
                    int groupLevel = (areaType == FooterAreaType.GroupFooter) ? (group.Group.OutlineLevel - 1) : 0;
                    if (this.CheckRowWithCustomSummaryExists(groupLevel, group.GroupRowHandle, tempCollection) > 0)
                    {
                        this.ExecuteCore(areaType, i, action, tempCollection);
                    }
                    else
                    {
                        this.CorrectExportRowIndex(areaType == FooterAreaType.GroupFooter);
                    }
                }
            }
        }

        private Dictionary<string, int> CustomSummaryByColumnsTable
        {
            get
            {
                if (this.customSummaryByColumnsTable == null)
                {
                    this.customSummaryByColumnsTable = new Dictionary<string, int>();
                    for (int i = 0; i < base.ExportInfo.ColumnsInfoColl.Count; i++)
                    {
                        TCol column = base.ExportInfo.ColumnsInfoColl[i];
                        if (!this.customSummaryByColumnsTable.ContainsKey(column.FieldName))
                        {
                            Func<ISummaryItemEx, bool> predicate = <>c<TCol, TRow>.<>9__6_1;
                            if (<>c<TCol, TRow>.<>9__6_1 == null)
                            {
                                Func<ISummaryItemEx, bool> local1 = <>c<TCol, TRow>.<>9__6_1;
                                predicate = <>c<TCol, TRow>.<>9__6_1 = x => x.SummaryType == SummaryItemType.Custom;
                            }
                            int num2 = (from x in this.groupSummaryItems
                                where !string.IsNullOrEmpty(x.ShowInColumnFooterName) ? ((IEnumerable<ISummaryItemEx>) (x.ShowInColumnFooterName == column.FieldName)) : ((IEnumerable<ISummaryItemEx>) (x.FieldName == column.FieldName))
                                select x).Count<ISummaryItemEx>(predicate);
                            this.customSummaryByColumnsTable.Add(column.FieldName, num2);
                        }
                    }
                }
                return this.customSummaryByColumnsTable;
            }
        }

        [DefaultValue(false)]
        public bool IgnoreHiddenGroupValues { get; set; }

        internal bool AllowExportGroupSummary =>
            this.allowExportGroupSummary;

        protected bool AllowExportTotalSummary =>
            this.CheckTotalSummaryOptions();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SummaryExportHelper<TCol, TRow>.<>c <>9;
            public static Func<ISummaryItemEx, bool> <>9__4_0;
            public static Func<ISummaryItemEx, bool> <>9__6_1;

            static <>c()
            {
                SummaryExportHelper<TCol, TRow>.<>c.<>9 = new SummaryExportHelper<TCol, TRow>.<>c();
            }

            internal bool <.ctor>b__4_0(ISummaryItemEx x) => 
                x.AlignByColumnInFooter;

            internal bool <get_CustomSummaryByColumnsTable>b__6_1(ISummaryItemEx x) => 
                x.SummaryType == SummaryItemType.Custom;
        }
    }
}

