namespace DevExpress.Data
{
    using System;

    public interface IDataColumnInfoProvider
    {
        IDataColumnInfo GetInfo(object arguments);
    }
}

