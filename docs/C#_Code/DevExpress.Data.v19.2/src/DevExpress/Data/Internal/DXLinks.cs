namespace DevExpress.Data.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    internal static class DXLinks
    {
        private static readonly HashSet<string> links;

        static DXLinks();
        public static bool IsDXLink(ProcessStartInfo startInfo);
        public static bool IsDXLink(string fileName, string arguments);
    }
}

