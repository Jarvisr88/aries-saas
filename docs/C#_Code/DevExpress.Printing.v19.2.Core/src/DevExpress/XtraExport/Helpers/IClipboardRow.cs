namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IClipboardRow
    {
        Type[] GetCellTypes();

        object[] Cells { get; }

        int Indent { get; }
    }
}

