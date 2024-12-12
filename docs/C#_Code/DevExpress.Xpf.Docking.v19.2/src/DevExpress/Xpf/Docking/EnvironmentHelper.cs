namespace DevExpress.Xpf.Docking
{
    using System;

    internal static class EnvironmentHelper
    {
        private static readonly Version version = Environment.OSVersion.Version;

        public static bool IsWinSevenOrLater =>
            (version.Major > 6) || ((version.Major == 6) && (version.Minor > 0));

        public static bool IsWinXP =>
            (version.Major == 5) && (version.Minor == 1);

        public static bool IsNet45OrNewer =>
            Type.GetType("System.Reflection.ReflectionContext", false) != null;

        public static bool IsEightOneOrNewer =>
            ((version.Major != 6) || (version.Minor <= 3)) ? (version.Major > 7) : true;

        public static bool IsRedstoneOneOrNewer =>
            version > new Version(10, 0, 0x3839);
    }
}

