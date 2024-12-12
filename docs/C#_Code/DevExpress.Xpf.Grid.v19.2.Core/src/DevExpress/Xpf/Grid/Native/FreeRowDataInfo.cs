namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;

    public class FreeRowDataInfo
    {
        public FreeRowDataInfo(RowsContainer dataContainer, RowDataBase rowData)
        {
            this.RowData = rowData;
            this.DataContainer = dataContainer;
        }

        public RowDataBase RowData { get; set; }

        public RowsContainer DataContainer { get; set; }
    }
}

