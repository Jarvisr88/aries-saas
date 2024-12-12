namespace ActiproSoftware.Products
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro")]
    public enum ActiproLicenseSourceLocation
    {
        None,
        Fixed,
        Registry,
        AssemblySavedContext
    }
}

