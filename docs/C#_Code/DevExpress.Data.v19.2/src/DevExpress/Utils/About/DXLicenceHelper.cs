namespace DevExpress.Utils.About
{
    using System;
    using System.ComponentModel;

    public class DXLicenceHelper
    {
        private Type type;
        private bool validated;

        public DXLicenceHelper(Type type)
        {
            this.type = type;
        }

        public bool Validate()
        {
            if (this.validated)
            {
                return true;
            }
            this.validated = true;
            return this.ValidateCore();
        }

        private bool ValidateCore()
        {
            DXLicense license = LicenseManager.Validate(this.type, 0) as DXLicense;
            return ((license != null) && (license.LicType != DXLicenseType.Trial));
        }
    }
}

