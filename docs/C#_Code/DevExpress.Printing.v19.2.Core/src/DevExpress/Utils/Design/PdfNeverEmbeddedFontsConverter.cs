namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    public class PdfNeverEmbeddedFontsConverter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            ((context == null) || ((context.Instance == null) || (!(context.Instance is PdfExportOptions) || (((PdfExportOptions) context.Instance).PdfACompatibility == PdfACompatibility.None)))) ? base.CanConvertFrom(context, sourceType) : false;
    }
}

