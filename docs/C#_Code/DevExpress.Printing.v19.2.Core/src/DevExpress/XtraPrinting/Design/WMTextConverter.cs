namespace DevExpress.XtraPrinting.Design
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.ComponentModel;

    public class WMTextConverter : StringConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => 
            new TypeConverter.StandardValuesCollection(StandardValues);

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => 
            false;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            true;

        public static string[] StandardValues
        {
            get
            {
                string[] textArray1 = new string[11];
                textArray1[0] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Asap);
                textArray1[1] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Confidential);
                textArray1[2] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Copy);
                textArray1[3] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_DoNotCopy);
                textArray1[4] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Draft);
                textArray1[5] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Evaluation);
                textArray1[6] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Original);
                textArray1[7] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Personal);
                textArray1[8] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Sample);
                textArray1[9] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_TopSecret);
                textArray1[10] = PreviewLocalizer.GetString(PreviewStringId.WMForm_Watermark_Urgent);
                return textArray1;
            }
        }
    }
}

