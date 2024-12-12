namespace DevExpress.XtraPrinting.Native.Interaction
{
    using System;
    using System.Runtime.InteropServices;

    public static class InteractionServiceMethods
    {
        internal static string CombineData(string path, string fieldName);
        internal static bool TryParseData(string s, out string[] values);
    }
}

