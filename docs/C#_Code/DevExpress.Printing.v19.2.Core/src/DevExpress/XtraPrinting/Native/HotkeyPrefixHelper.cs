namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing.Text;

    public static class HotkeyPrefixHelper
    {
        public static string PreprocessHotkeyPrefixesInString(string original, BrickStyle style);
        public static string PreprocessHotkeyPrefixesInString(string original, HotkeyPrefix hotkeyPrefix);
        public static void PreprocessHotkeyPrefixesInTextLayoutTable(ITextLayoutTable table, BrickStyle style);
    }
}

