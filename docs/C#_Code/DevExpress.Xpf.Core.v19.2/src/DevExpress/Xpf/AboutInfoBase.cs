namespace DevExpress.Xpf
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class AboutInfoBase
    {
        public DevExpress.Xpf.LicenseState LicenseState { get; set; }

        public string ProductName { get; set; }

        public string ProductKind { get; set; }

        public string Version { get; set; }
    }
}

