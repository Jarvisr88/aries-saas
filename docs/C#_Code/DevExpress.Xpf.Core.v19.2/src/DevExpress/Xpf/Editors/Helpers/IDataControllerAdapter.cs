namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public interface IDataControllerAdapter
    {
        int GetListSourceIndex(object value);
        object GetRow(int listSourceIndex);
        object GetRowValue(int listSourceIndex);
        object GetRowValue(object item);

        int VisibleRowCount { get; }

        bool IsOwnSearchProcessing { get; }
    }
}

