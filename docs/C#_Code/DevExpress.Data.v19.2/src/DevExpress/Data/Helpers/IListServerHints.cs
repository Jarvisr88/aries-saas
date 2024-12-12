namespace DevExpress.Data.Helpers
{
    using System;

    public interface IListServerHints
    {
        void HintGridIsPaged(int pageSize);
        void HintMaxVisibleRowsInGrid(int rowsInGrid);
    }
}

