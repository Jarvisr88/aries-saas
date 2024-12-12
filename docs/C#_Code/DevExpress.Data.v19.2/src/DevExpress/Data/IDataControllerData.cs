namespace DevExpress.Data
{
    using System;

    public interface IDataControllerData
    {
        UnboundColumnInfoCollection GetUnboundColumns();
        object GetUnboundData(int listSourceRow1, DataColumnInfo column, object value);
        void SetUnboundData(int listSourceRow1, DataColumnInfo column, object value);
    }
}

