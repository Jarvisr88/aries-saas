namespace DevExpress.Xpf.Utils.About
{
    using DevExpress.Utils.About;

    public class DX_WPF_LicenseProvider : DXLicenseProvider
    {
        protected override ProductKind Kind =>
            ProductKind.Default | ProductKind.DXperienceWPF;
    }
}

