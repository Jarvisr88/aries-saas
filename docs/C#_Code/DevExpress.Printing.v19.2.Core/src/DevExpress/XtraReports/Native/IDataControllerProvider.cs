namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data;

    public interface IDataControllerProvider
    {
        DevExpress.Data.DataController DataController { get; }
    }
}

