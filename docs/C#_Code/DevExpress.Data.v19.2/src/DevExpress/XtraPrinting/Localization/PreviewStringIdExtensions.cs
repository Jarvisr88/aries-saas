namespace DevExpress.XtraPrinting.Localization
{
    using System;
    using System.Runtime.CompilerServices;

    public static class PreviewStringIdExtensions
    {
        public static string GetString(this PreviewStringId id) => 
            PreviewLocalizer.GetString(id);

        public static string GetString(this PreviewStringId id, params object[] args) => 
            string.Format(PreviewLocalizer.GetString(id), args);
    }
}

