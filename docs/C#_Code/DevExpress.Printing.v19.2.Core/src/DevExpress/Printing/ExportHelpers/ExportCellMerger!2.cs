namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    public class ExportCellMerger<TCol, TRow> : ExportHelperBase<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private Dictionary<object, MergeInfo> mergeInfo;

        public ExportCellMerger(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        private bool CanMergeCurrentColumn(TCol col) => 
            base.ExportInfo.View.GetAllowMerge(col) && this.MergeInformation.ContainsKey(col);

        public void Clear()
        {
            this.MergeInformation.Clear();
        }

        public void CompleteMergingInGroup(TRow lastRow, int endGroupIndex)
        {
            this.MergedLastCells(lastRow, endGroupIndex);
            this.MergeInformation.Clear();
        }

        public override void Execute()
        {
        }

        public void Merge(XlCellRange range)
        {
            try
            {
                base.ExportInfo.Sheet.MergedCells.Add(range);
            }
            catch (ArgumentException)
            {
            }
        }

        private void MergeCellsCore(int endGroupIndex, TCol column)
        {
            if ((endGroupIndex - this.MergeInformation[column].ExportRowIndex) > 1)
            {
                int index = base.ExportInfo.ColumnsInfoColl.IndexOf(column.FieldName);
                base.ExportInfo.Sheet.MergedCells.Add(XlCellRange.FromLTRB(index, this.MergeInformation[column].ExportRowIndex, index, endGroupIndex - 1));
            }
        }

        public void MergedLastCells(TRow gridRow, int endGroupIndex)
        {
            if (gridRow != null)
            {
                foreach (TCol local in base.ExportInfo.ColumnsInfoColl)
                {
                    if (this.CanMergeCurrentColumn(local) && (this.MergeInformation.Count != 0))
                    {
                        this.MergeCellsCore(endGroupIndex, local);
                    }
                }
            }
        }

        public void ProcessVerticalMerging(int rowInd, int rowHandle, TCol col, IXlCell cell)
        {
            if (!this.MergeInformation.ContainsKey(col))
            {
                MergeInfo info1 = new MergeInfo();
                info1.RowHandle = rowHandle;
                info1.ExportRowIndex = rowInd;
                info1.Value = cell.Value;
                this.MergeInformation.Add(col, info1);
            }
            else
            {
                bool allowMerge = base.ExportInfo.View.GetAllowMerge(col);
                bool flag2 = Equals(cell.Value, this.MergeInformation[col].Value) & allowMerge;
                if (allowMerge)
                {
                    int num = base.ExportInfo.RaiseMergeEvent(this.MergeInformation[col].RowHandle, rowHandle, col);
                    if (num < 0)
                    {
                        flag2 = false;
                    }
                    if (num == 0)
                    {
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    this.MergeCellsCore(rowInd, col);
                    this.MergeInformation[col].Value = cell.Value;
                    this.MergeInformation[col].RowHandle = rowHandle;
                    this.MergeInformation[col].ExportRowIndex = rowInd;
                }
            }
        }

        private Dictionary<object, MergeInfo> MergeInformation
        {
            get
            {
                this.mergeInfo ??= new Dictionary<object, MergeInfo>();
                return this.mergeInfo;
            }
        }
    }
}

