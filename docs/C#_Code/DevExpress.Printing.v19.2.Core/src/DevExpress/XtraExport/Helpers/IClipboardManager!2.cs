namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Utils.Controls;
    using System;
    using System.Windows.Forms;

    public interface IClipboardManager<TCol, TRow> where TCol: IColumn where TRow: IRowBase
    {
        void AssignOptions(BaseOptions options);
        void SetClipboardData(DataObject dataObject);
    }
}

