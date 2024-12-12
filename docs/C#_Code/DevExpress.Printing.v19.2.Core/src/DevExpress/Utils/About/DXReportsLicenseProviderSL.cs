namespace DevExpress.Utils.About
{
    public class DXReportsLicenseProviderSL : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.XtraReportsSL;
    }
}

