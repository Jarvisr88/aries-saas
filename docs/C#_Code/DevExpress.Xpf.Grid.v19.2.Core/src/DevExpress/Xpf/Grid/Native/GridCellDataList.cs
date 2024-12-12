namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public class GridCellDataList : BridgeList<GridColumnData, ColumnBase>
    {
        private ColumnsRowDataBase row;

        public GridCellDataList(ColumnsRowDataBase row, IList<ColumnBase> visibleColumns) : base(visibleColumns)
        {
            this.row = row;
        }

        protected override GridColumnData GetItemByKey(ColumnBase key, int index) => 
            this.row.GetCellDataByColumn(key);
    }
}

