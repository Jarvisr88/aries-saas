namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class PdfShowPrintDialogOnOpenConverter : BooleanTypeConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            ((context == null) || ((context.Instance == null) || (!(context.Instance is PdfExportOptions) || (((PdfExportOptions) context.Instance).PdfACompatibility == PdfACompatibility.None)))) ? base.GetStandardValuesSupported(context) : false;
    }
}

