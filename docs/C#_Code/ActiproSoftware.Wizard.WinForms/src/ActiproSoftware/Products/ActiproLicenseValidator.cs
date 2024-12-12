namespace ActiproSoftware.Products
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Actipro")]
    public static class ActiproLicenseValidator
    {
        public static AssemblyLicenseType ValidateLicense(AssemblyInfo assemblyInfo, Type type, object instance) => 
            ActiproLicenseManager.ValidateLicense(assemblyInfo, type, instance);
    }
}

