namespace DevExpress.Utils.About
{
    using System;
    using System.ComponentModel;

    public class DXLicense : License
    {
        private DXLicenseType licType;
        private bool exp;
        private ProductKind productKind;

        public DXLicense(DXLicenseType licType)
        {
            this.licType = licType;
        }

        internal DXLicense(DXLicenseType licType, bool exp)
        {
            if (licType == DXLicenseType.Trial)
            {
                this.exp = exp;
            }
            this.licType = licType;
        }

        internal DXLicense(DXLicenseType licType, bool exp, ProductKind kind) : this(licType, exp)
        {
            this.productKind = kind;
        }

        public override void Dispose()
        {
        }

        public bool IsExpired =>
            this.exp;

        public ProductKind Kind =>
            this.productKind;

        public DXLicenseType LicType =>
            this.licType;

        public override string LicenseKey =>
            "Key";
    }
}

