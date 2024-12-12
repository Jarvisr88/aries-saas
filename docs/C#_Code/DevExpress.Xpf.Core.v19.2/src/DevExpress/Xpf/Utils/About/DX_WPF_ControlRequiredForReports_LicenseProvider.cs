namespace DevExpress.Xpf.Utils.About
{
    using DevExpress.Utils.About;

    public class DX_WPF_ControlRequiredForReports_LicenseProvider : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.DXperienceWPF;

        protected override ProductKind[] Kinds =>
            new ProductKind[] { ProductKind.Default | ProductKind.DXperienceWPF, ProductKind.Default | ProductKind.XtraReportsWpf };
    }
}

