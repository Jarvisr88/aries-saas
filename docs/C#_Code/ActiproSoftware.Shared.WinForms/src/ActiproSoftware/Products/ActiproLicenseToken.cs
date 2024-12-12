namespace ActiproSoftware.Products
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro"), EditorBrowsable(EditorBrowsableState.Never), LicenseProvider(typeof(ActiproLicenseProvider))]
    public sealed class ActiproLicenseToken
    {
        public ActiproLicenseToken()
        {
            LicenseManager.Validate(typeof(ActiproLicenseToken), this);
        }
    }
}

