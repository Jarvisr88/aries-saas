namespace ActiproSoftware.Products
{
    using ActiproSoftware.Products.Shared;
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro")]
    public sealed class ActiproLicenseProvider : LicenseProvider
    {
        public sealed override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            ActiproLicense license = ActiproLicenseManager.GetLicense(ActiproSoftware.Products.Shared.AssemblyInfo.Instance, context);
            if ((license != null) && (!license.IsValid || (license.LicenseType != AssemblyLicenseType.Full)))
            {
                ActiproLicenseManager.#KFj(license, null, instance);
            }
            return license;
        }
    }
}

