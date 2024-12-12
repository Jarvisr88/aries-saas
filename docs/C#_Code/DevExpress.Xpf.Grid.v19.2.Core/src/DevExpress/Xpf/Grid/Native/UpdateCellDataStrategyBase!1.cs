namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public abstract class UpdateCellDataStrategyBase<TColumnData> where TColumnData: GridColumnData
    {
        protected UpdateCellDataStrategyBase()
        {
        }

        public abstract TColumnData CreateNewData();
        public abstract void UpdateData(ColumnBase column, TColumnData columnData);

        public abstract bool CanReuseCellData { get; }

        public abstract Dictionary<ColumnBase, TColumnData> DataCache { get; }
    }
}

