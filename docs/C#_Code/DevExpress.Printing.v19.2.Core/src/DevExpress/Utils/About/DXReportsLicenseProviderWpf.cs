namespace DevExpress.Utils.About
{
    public class DXReportsLicenseProviderWpf : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.XtraReportsWpf;
    }
}

