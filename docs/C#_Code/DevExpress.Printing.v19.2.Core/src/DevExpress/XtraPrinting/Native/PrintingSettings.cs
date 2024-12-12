namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class PrintingSettings
    {
        private static bool verticalContentSplittingNewBehavior;
        private static bool passPdfDrawingExceptions;
        private static bool useNewPdfExport;
        private static bool allowMapiModelessDialog;
        private static bool useRichTextFontSubstitution;
        private static DevExpress.XtraPrinting.Native.ParameterPanelResetMode parameterPanelResetMode;
        private static bool useGdiPlusLineBreakAlgorithm;

        static PrintingSettings();

        [Obsolete("This member has become obsolete.")]
        public static bool EnablePageBreakForRollPaper { get; set; }

        public static bool VerticalContentSplittingNewBehavior { get; set; }

        public static bool UseNewPdfExport { get; set; }

        public static bool PassPdfDrawingExceptions { get; set; }

        public static bool AllowMapiModelessDialog { get; set; }

        public static bool UseRichTextFontSubstitution { get; set; }

        public static DevExpress.XtraPrinting.Native.ParameterPanelResetMode ParameterPanelResetMode { get; set; }

        public static bool UseGdiPlusLineBreakAlgorithm { get; set; }
    }
}

