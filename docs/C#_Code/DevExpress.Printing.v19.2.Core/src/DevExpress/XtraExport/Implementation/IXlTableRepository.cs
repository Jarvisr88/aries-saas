namespace DevExpress.XtraExport.Implementation
{
    using System;

    public interface IXlTableRepository
    {
        string GetUniqueTableName();
        void RegisterTableName(string name);
        void UnregisterTableName(string name);
    }
}

