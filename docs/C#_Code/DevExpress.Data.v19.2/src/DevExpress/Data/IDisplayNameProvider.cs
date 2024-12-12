namespace DevExpress.Data
{
    using System;

    public interface IDisplayNameProvider
    {
        string GetDataSourceDisplayName();
        string GetFieldDisplayName(string[] fieldAccessors);
    }
}

