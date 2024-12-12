namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class ExportEditingFieldsToAcroFormsConverter : BooleanTypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !IsUnsupported(context) ? base.CanConvertFrom(context, sourceType) : false;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            !IsUnsupported(context) ? base.GetStandardValuesSupported(context) : false;

        private static bool IsUnsupported(ITypeDescriptorContext context) => 
            (context != null) && ((context.Instance != null) && ((context.Instance is PdfExportOptions) && (((PdfExportOptions) context.Instance).PdfACompatibility == PdfACompatibility.PdfA1b)));
    }
}

