namespace DevExpress.Data.XtraReports.Labels
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current Report Wizard implementation.")]
    public static class LabelWizardHelper
    {
        private static int ConvertFloatToInt(float num, float epsilon);
        public static string GetFormattedValueInUnits(float value, GraphicsUnit targetUnit);
        private static string GetFormattedValueInUnits(float value, GraphicsUnit targetUnit, bool includeUnitText);
        public static int GetLabelsCount(float labelPitch, float labelWidth, float margin, float oppositeMargin, GraphicsUnit labelUnit, float paperDimension, GraphicsUnit paperUnit);
        public static int GetLabelsCount(float labelPitch, float labelWidth, float margin, float oppositeMargin, float labelDpi, float paperDimension, float paperDpi);
        public static string GetPaperKindFormattedString(PaperKindData paperKindData, GraphicsUnit targetUnit);
    }
}

