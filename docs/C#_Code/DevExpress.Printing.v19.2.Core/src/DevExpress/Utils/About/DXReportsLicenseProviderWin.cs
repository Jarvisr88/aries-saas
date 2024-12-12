namespace DevExpress.Utils.About
{
    public class DXReportsLicenseProviderWin : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.XtraReportsWin;
    }
}

