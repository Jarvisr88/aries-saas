namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IGridViewFactoryBase
    {
        string GetDataMember();
        object GetDataSource();
        Type GetViewType();
    }
}

