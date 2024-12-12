namespace DevExpress.Utils.About
{
    public class DXReportsLicenseProviderWeb : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.XtraReportsWeb;
    }
}

