namespace DevExpress.Utils.About
{
    public class DXReportsLicenseProvider : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.XtraReports;
    }
}

