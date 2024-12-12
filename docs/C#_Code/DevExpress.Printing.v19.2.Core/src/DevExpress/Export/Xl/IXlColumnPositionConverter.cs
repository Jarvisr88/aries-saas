namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlColumnPositionConverter
    {
        int GetColumnIndex(string name);
        int GetRowOffset(string columnName);
    }
}

