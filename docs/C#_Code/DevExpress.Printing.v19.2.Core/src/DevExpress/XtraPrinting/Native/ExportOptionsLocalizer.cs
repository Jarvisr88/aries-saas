namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections.Generic;

    public static class ExportOptionsLocalizer
    {
        private static Dictionary<Enum, PreviewStringId> localizationAssociations;

        static ExportOptionsLocalizer();
        private static void AddExportOptionKindLocalizationAssociation(ExportOptionKind optionKind, PreviewStringId previewStringId);
        private static void AddLocalizationAssociation(Enum enumValue, PreviewStringId previewStringId);
        internal static object GetDelocalizedOption(Type enumType, string option);
        internal static string GetLocalizedOption(Enum enumValue);
    }
}

