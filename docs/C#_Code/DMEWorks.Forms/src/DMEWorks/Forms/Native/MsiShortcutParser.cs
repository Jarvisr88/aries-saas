namespace DMEWorks.Forms.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class MsiShortcutParser
    {
        public const int MaxFeatureLength = 0x26;
        public const int MaxGuidLength = 0x26;
        public const int MaxPathLength = 0x400;

        [DllImport("msi.dll", CharSet=CharSet.Auto)]
        private static extern InstallState MsiGetComponentPath(string productCode, string componentCode, StringBuilder componentPath, ref int componentPathBufferSize);
        [DllImport("msi.dll", CharSet=CharSet.Auto)]
        private static extern int MsiGetShortcutTarget(string targetFile, StringBuilder productCode, StringBuilder featureID, StringBuilder componentCode);
        public static string ParseShortcut(string file)
        {
            StringBuilder productCode = new StringBuilder(0x27);
            StringBuilder componentCode = new StringBuilder(0x27);
            MsiGetShortcutTarget(file, productCode, new StringBuilder(0x27), componentCode);
            int capacity = 0x400;
            StringBuilder componentPath = new StringBuilder(capacity);
            return ((MsiGetComponentPath(productCode.ToString(), componentCode.ToString(), componentPath, ref capacity) != InstallState.Local) ? null : componentPath.ToString());
        }

        public enum InstallState
        {
            NotUsed = -7,
            BadConfig = -6,
            Incomplete = -5,
            SourceAbsent = -4,
            MoreData = -3,
            InvalidArg = -2,
            Unknown = -1,
            Broken = 0,
            Advertised = 1,
            Removed = 1,
            Absent = 2,
            Local = 3,
            Source = 4,
            Default = 5
        }
    }
}

