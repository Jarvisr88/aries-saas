namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IGridBand : IBandedGridColumn, IColumn
    {
        IEnumerable<IBandedGridColumn> GetColumns();

        int VisibleChildrenBandsCount { get; }
    }
}

