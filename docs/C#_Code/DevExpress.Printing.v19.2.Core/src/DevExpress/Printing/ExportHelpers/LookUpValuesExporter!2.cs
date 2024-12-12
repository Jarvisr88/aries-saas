namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class LookUpValuesExporter<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private readonly HashSet<string> uniqueSheetNames;

        public LookUpValuesExporter(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.uniqueSheetNames = new HashSet<string>();
            LookUpValuesExporter<TCol, TRow>.AuxiliarySheetRowsCount = this.AuxiliarySheetRows(exportInfo.ColumnsInfoColl);
        }

        private int AuxiliarySheetRows(ExportColumnsCollection<TCol> gridColumns)
        {
            int num = 0;
            foreach (TCol local in gridColumns)
            {
                if (local == null)
                {
                    continue;
                }
                List<object> list = gridColumns.ColumnLookupItemsByFieldName(local.FieldName);
                if (list != null)
                {
                    int count = list.Count;
                    if (count > num)
                    {
                        num = count;
                    }
                }
            }
            return num;
        }

        private void BeginAuxiliarySheet(IXlExport exporter)
        {
            if (base.ExportInfo.Exporter.CurrentSheet != null)
            {
                this.EndSheet();
            }
            IXlSheet sheet = exporter.BeginSheet();
            sheet.SplitPosition = new XlCellPosition(0, 0);
            sheet.VisibleState = XlSheetVisibleState.Hidden;
        }

        public void CompleteDataValidation()
        {
            if (LookUpValuesExporter<TCol, TRow>.AuxiliarySheetRowsCount == 0)
            {
                this.EndSheet();
            }
            else
            {
                this.ExportItems(LookUpValuesExporter<TCol, TRow>.AuxiliarySheetRowsCount, base.ExportInfo.ExportRowIndex, new Func<string, List<object>>(base.ExportInfo.ColumnsInfoColl.ColumnLookupItemsByFieldName));
                this.ExportLookupKeyValueTables(base.ExportInfo.ExportRowIndex);
            }
        }

        private void CreateCell(object value)
        {
            base.ExportInfo.Exporter.BeginCell().Value = XlVariantValue.FromObject(value);
            base.ExportInfo.Exporter.EndCell();
        }

        private void EndSheet()
        {
            base.ExportInfo.Exporter.EndSheet();
            base.ExportInfo.EndSheetFlag = false;
        }

        public override void Execute()
        {
            this.ProcessDataValidation(AdditionalSheetNames.LookUpItemsDefaultSheetName);
            this.CompleteDataValidation();
        }

        private void ExportItems(int columnValuesMaxCount, int exporterRowsIndex, Func<string, List<object>> getItems)
        {
            this.BeginAuxiliarySheet(base.ExportInfo.Exporter);
            for (int i = 0; i < columnValuesMaxCount; i++)
            {
                base.ExportInfo.Exporter.BeginRow();
                this.ExportLookupValues(i, getItems);
                base.ExportInfo.Exporter.EndRow();
                exporterRowsIndex++;
                base.ExportInfo.ReportProgress(exporterRowsIndex);
            }
            this.EndSheet();
        }

        private void ExportLookupKeyValueTables(int exporterRowsIndex)
        {
            int count = base.ExportInfo.ColumnsInfoColl.Count;
            for (int i = 0; i < count; i++)
            {
                TCol local = base.ExportInfo.ColumnsInfoColl[i];
                bool flag = base.ExportInfo.ColumnsInfoColl.ColumnHasLookupItems(local.FieldName);
                bool flag2 = base.ExportInfo.ColumnIsSourceForCondFmtRule(local.FieldName);
                if (flag & flag2)
                {
                    string fieldName = local.FieldName;
                    if (fieldName.Length > 0x1f)
                    {
                        fieldName = fieldName.Substring(0, 0x1f);
                    }
                    if (!this.uniqueSheetNames.Contains(fieldName))
                    {
                        this.BeginAuxiliarySheet(base.ExportInfo.Exporter);
                        base.ExportInfo.Exporter.CurrentSheet.Name = fieldName;
                        IDictionary<object, object> items = local.DataValidationItems;
                        TCol column = local;
                        base.ExportInfo.Helper.ForAllRows(base.ExportInfo.View, delegate (TRow row) {
                            if (!row.IsGroupRow)
                            {
                                ((LookUpValuesExporter<TCol, TRow>) this).ExportInfo.Exporter.BeginRow();
                                object obj2 = !column.IsGroupColumn ? ((LookUpValuesExporter<TCol, TRow>) this).ExportInfo.View.GetRowCellValue(row, column) : string.Empty;
                                ((LookUpValuesExporter<TCol, TRow>) this).CreateCell(obj2);
                                ((LookUpValuesExporter<TCol, TRow>) this).CreateCell(((LookUpValuesExporter<TCol, TRow>) this).FindKey(items, obj2));
                                int num = exporterRowsIndex;
                                exporterRowsIndex = num + 1;
                                ((LookUpValuesExporter<TCol, TRow>) this).ExportInfo.ReportProgress(exporterRowsIndex);
                                ((LookUpValuesExporter<TCol, TRow>) this).ExportInfo.Exporter.EndRow();
                            }
                        });
                        this.EndSheet();
                        this.uniqueSheetNames.Add(fieldName);
                    }
                }
            }
        }

        private void ExportLookupValues(int dvItemRow, Func<string, List<object>> getitems)
        {
            foreach (TCol local in base.ExportInfo.ColumnsInfoColl)
            {
                IXlCell cell = base.ExportInfo.Exporter.BeginCell();
                List<object> itemsList = getitems(local.FieldName);
                object itemByKey = LookUpValuesExporter<TCol, TRow>.GetItemByKey(itemsList, dvItemRow);
                cell.Value = XlVariantValue.FromObject(itemByKey);
                XlVariantValue value2 = cell.Value;
                if (value2.IsEmpty && (itemByKey != null))
                {
                    cell.Value = XlVariantValue.FromObject(itemByKey.ToString());
                }
                base.ExportInfo.Exporter.EndCell();
            }
        }

        private object FindKey(IDictionary<object, object> items, object value)
        {
            object key;
            using (IEnumerator<KeyValuePair<object, object>> enumerator = items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<object, object> current = enumerator.Current;
                        if (current.Value == null)
                        {
                            continue;
                        }
                        string a = (current.Value != null) ? current.Value.ToString() : string.Empty;
                        if (!string.Equals(a, (value != null) ? value.ToString() : string.Empty))
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return key;
        }

        private static object GetItemByKey(IList<object> itemsList, int index) => 
            ((itemsList == null) || (itemsList.Count <= index)) ? null : itemsList[index];

        private XlCellRange GetSourceRange(string auxiliarySheetName, IColumn gridColumn, int endRange)
        {
            int index = base.ExportInfo.ColumnsInfoColl.IndexOf(gridColumn.FieldName);
            XlCellRange range1 = new XlCellRange(new XlCellPosition(index, 0, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(index, endRange, XlPositionType.Absolute, XlPositionType.Absolute));
            range1.SheetName = auxiliarySheetName;
            return range1;
        }

        public void ProcessDataValidation(string itemsSheetName)
        {
            if (base.ExportInfo.GroupsList.Count != 0)
            {
                foreach (TCol local in base.ExportInfo.GridColumns)
                {
                    if (local != null)
                    {
                        List<object> list = base.ExportInfo.ColumnsInfoColl.ColumnLookupItemsByFieldName(local.FieldName);
                        XlCellRange sourcerangeitem = null;
                        if ((list != null) && (list.Count > 0))
                        {
                            sourcerangeitem = this.GetSourceRange(itemsSheetName, local, list.Count - 1);
                        }
                        this.SetRanges(sourcerangeitem, local);
                    }
                }
            }
        }

        protected void SetDataValidationOnCellsGroup(XlCellRange rangeItem, int column, int startrow, int endrow)
        {
            XlDataValidation validation1 = new XlDataValidation();
            validation1.Type = XlDataValidationType.List;
            validation1.AllowBlank = true;
            validation1.Operator = XlDataValidationOperator.Between;
            validation1.ListRange = rangeItem;
            validation1.ShowErrorMessage = base.ExportInfo.Options.ShowDataValidationErrorMessage;
            XlDataValidation item = validation1;
            item.Ranges.Add(new XlCellRange(new XlCellPosition(column, startrow), new XlCellPosition(column, endrow)));
            base.ExportInfo.Sheet.DataValidations.Add(item);
        }

        protected virtual void SetRanges(XlCellRange sourcerangeitem, IColumn gridColumn)
        {
            int index = base.ExportInfo.ColumnsInfoColl.IndexOf(gridColumn.FieldName);
            for (int i = 0; i < base.ExportInfo.GroupsList.Count; i++)
            {
                Group objA = base.ExportInfo.GroupsList[i];
                if ((sourcerangeitem != null) && (objA.End != 0))
                {
                    int end = objA.End;
                    if (Equals(objA, base.ExportInfo.GroupsList.Last<Group>()))
                    {
                        end--;
                    }
                    this.SetDataValidationOnCellsGroup(sourcerangeitem, index, objA.Start, end);
                }
            }
        }

        internal static int AuxiliarySheetRowsCount
        {
            get => 
                LookUpValuesExporter<TCol, TRow>.<AuxiliarySheetRowsCount>k__BackingField;
            private set => 
                LookUpValuesExporter<TCol, TRow>.<AuxiliarySheetRowsCount>k__BackingField = value;
        }
    }
}

