namespace DevExpress.Printing.ExportHelpers.Helpers
{
    using DevExpress.Data.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class SparklineExportStrategy<TCol, TRow> : ISparklineExportStrategy where TCol: class, IColumn where TRow: class, IRowBase
    {
        protected HashSet<IEnumerable> sparklineData;
        protected IEnumerable<TCol> sparklineColumns;
        protected ExportInfo<TCol, TRow> exportInfo;
        private int dataSheetRow;

        public SparklineExportStrategy(IEnumerable<TCol> sparklineColumns, ExportInfo<TCol, TRow> exportInfo)
        {
            this.sparklineData = new HashSet<IEnumerable>();
            this.sparklineColumns = sparklineColumns;
            this.exportInfo = exportInfo;
        }

        private void AddSparklines(XlSparklineGroup spGroup, TCol col, int dataRowLen, int startGroup, int endGroup)
        {
            int location = startGroup;
            for (int i = 0; i < ((endGroup - startGroup) + 1); i++)
            {
                int index = this.exportInfo.ColumnsInfoColl.IndexOf(col);
                XlSparkline item = new XlSparkline(this.GetDataRange(dataRowLen, this.dataSheetRow), this.GetLocation(index, location));
                this.dataSheetRow++;
                location++;
                spGroup.Sparklines.Add(item);
            }
        }

        protected void AssignSettings(XlSparklineGroup spGroup, ISparklineInfo info)
        {
            if (info != null)
            {
                spGroup.SparklineType = (XlSparklineType) info.SparklineType;
                spGroup.ColorSeries = info.ColorSeries;
                spGroup.ColorFirst = info.ColorFirst;
                spGroup.ColorHigh = info.ColorHigh;
                spGroup.ColorLast = info.ColorLast;
                spGroup.ColorLow = info.ColorLow;
                spGroup.ColorMarker = info.ColorMarker;
                spGroup.ColorNegative = info.ColorNegative;
                spGroup.HighlightFirst = info.HighlightFirst;
                spGroup.HighlightHighest = info.HighlightHighest;
                spGroup.HighlightLast = info.HighlightLast;
                spGroup.HighlightLowest = info.HighlightLowest;
                spGroup.HighlightNegative = info.HighlightNegative;
                spGroup.LineWeight = info.LineWeight;
                spGroup.DisplayMarkers = info.DisplayMarkers;
            }
        }

        protected virtual void AssignSparklinesToSheet()
        {
            this.CacheValues();
        }

        private void CacheValues()
        {
            foreach (TCol col in this.sparklineColumns)
            {
                this.exportInfo.Helper.ForAllRows(this.exportInfo.View, delegate (TRow x) {
                    IEnumerable rowCellValue = ((SparklineExportStrategy<TCol, TRow>) this).exportInfo.View.GetRowCellValue(x, col) as IEnumerable;
                    if (rowCellValue != null)
                    {
                        IEnumerable item = ((SparklineExportStrategy<TCol, TRow>) this).CorrectValues(rowCellValue);
                        ((SparklineExportStrategy<TCol, TRow>) this).sparklineData.Add(item);
                    }
                });
            }
        }

        private IEnumerable CorrectValues(IEnumerable castedValue)
        {
            int num = 0;
            List<object> list = new List<object>();
            foreach (object obj2 in castedValue)
            {
                if (num >= this.exportInfo.Exporter.DocumentOptions.MaxColumnCount)
                {
                    break;
                }
                list.Add(obj2);
                num++;
            }
            return list;
        }

        private int CreateAdvBandedSparklineGroup(TCol col, ISparklineInfo sparklineInfo)
        {
            BandNodeDescriptor descriptor;
            AdvBandedExportInfo<TCol, TRow> exportInfo = this.exportInfo as AdvBandedExportInfo<TCol, TRow>;
            exportInfo.BandsLayoutInfo.TryGetValue(col, out descriptor);
            if (descriptor.Column == null)
            {
                return -1;
            }
            int num = exportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(descriptor.Column.FieldName);
            int count = exportInfo.BandedRowPattern.Count;
            if (count == 1)
            {
                return -1;
            }
            for (int i = 0; i < this.exportInfo.GroupsList.Count; i++)
            {
                Group objA = this.exportInfo.GroupsList[i];
                XlSparklineGroup spGroup = new XlSparklineGroup();
                this.AssignSettings(spGroup, sparklineInfo);
                if (objA.End != 0)
                {
                    int end = objA.End;
                    if (Equals(objA, this.exportInfo.GroupsList.Last<Group>()))
                    {
                        end--;
                    }
                    for (int j = objA.Start; j < end; j += count)
                    {
                        int startGroup = j + num;
                        this.AddSparklines(spGroup, col, this.sparklineData.ElementAt<IEnumerable>(this.dataSheetRow).Count() - 1, startGroup, startGroup);
                    }
                }
                this.exportInfo.Sheet.SparklineGroups.Add(spGroup);
            }
            return 1;
        }

        protected virtual void CreateSparklineGroup(TCol col, ISparklineInfo sparklineInfo)
        {
            int num = -1;
            if (this.exportInfo is AdvBandedExportInfo<TCol, TRow>)
            {
                num = this.CreateAdvBandedSparklineGroup(col, sparklineInfo);
            }
            if (num != 1)
            {
                for (int i = 0; i < this.exportInfo.GroupsList.Count; i++)
                {
                    Group objA = this.exportInfo.GroupsList[i];
                    XlSparklineGroup spGroup = new XlSparklineGroup();
                    this.AssignSettings(spGroup, sparklineInfo);
                    if (objA.End != 0)
                    {
                        int end = objA.End;
                        if (Equals(objA, this.exportInfo.GroupsList.Last<Group>()))
                        {
                            end--;
                        }
                        if (this.dataSheetRow < this.sparklineData.Count)
                        {
                            IEnumerable source = this.sparklineData.ElementAt<IEnumerable>(this.dataSheetRow);
                            if (source != null)
                            {
                                this.AddSparklines(spGroup, col, source.Count() - 1, objA.Start, end);
                            }
                        }
                    }
                    this.exportInfo.Sheet.SparklineGroups.Add(spGroup);
                }
            }
        }

        public void Export()
        {
            if (this.sparklineColumns.Any<TCol>())
            {
                this.AssignSparklinesToSheet();
                this.ExportSparklineData();
            }
        }

        private void ExportSparklineData()
        {
            this.exportInfo.Exporter.EndSheet();
            IXlSheet sheet = this.exportInfo.Exporter.BeginSheet();
            sheet.Name = this.exportInfo.View.AdditionalSheetInfo.Name;
            sheet.VisibleState = this.exportInfo.View.AdditionalSheetInfo.VisibleState;
            for (int i = 0; i < this.sparklineData.Count; i++)
            {
                this.exportInfo.Exporter.BeginRow();
                IEnumerable enumerable = this.sparklineData.ElementAt<IEnumerable>(i);
                foreach (object obj2 in enumerable)
                {
                    IXlCell cell = this.exportInfo.Exporter.BeginCell();
                    cell.Value = XlVariantValue.FromObject(obj2);
                    this.exportInfo.Exporter.EndCell();
                }
                this.exportInfo.Exporter.EndRow();
                this.exportInfo.ExportRowIndex++;
                this.exportInfo.ReportProgress(this.exportInfo.ExportRowIndex);
            }
        }

        private XlCellRange GetDataRange(int dataRowLen, int index)
        {
            XlCellRange range1 = new XlCellRange(new XlCellPosition(0, index, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(dataRowLen, index, XlPositionType.Absolute, XlPositionType.Absolute));
            range1.SheetName = this.exportInfo.View.AdditionalSheetInfo.Name;
            return range1;
        }

        private XlCellRange GetLocation(int colLPosition, int location) => 
            new XlCellRange(new XlCellPosition(colLPosition, location, XlPositionType.Absolute, XlPositionType.Absolute), new XlCellPosition(colLPosition, location, XlPositionType.Absolute, XlPositionType.Absolute));
    }
}

