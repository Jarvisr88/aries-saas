namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    internal class WindowsVersionInfo
    {
        public int CurrentBuild { get; set; }

        public int CurrentMajorVersionNumber { get; set; }

        public int CurrentMinorVersionNumber { get; set; }

        public Version CurrentVersion { get; set; }

        public string ProductName { get; set; }

        public int ReleaseID { get; set; }
    }
}

