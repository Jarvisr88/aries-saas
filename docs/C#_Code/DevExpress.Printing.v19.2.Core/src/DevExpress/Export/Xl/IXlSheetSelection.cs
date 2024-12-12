namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public interface IXlSheetSelection
    {
        IList<XlCellRange> SelectedRanges { get; }

        XlCellPosition ActiveCell { get; set; }
    }
}

