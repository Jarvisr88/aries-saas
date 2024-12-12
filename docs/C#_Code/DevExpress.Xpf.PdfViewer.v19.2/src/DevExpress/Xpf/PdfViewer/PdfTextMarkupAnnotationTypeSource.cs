namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Linq;
    using System.Windows.Markup;

    public class PdfTextMarkupAnnotationTypeSource : MarkupExtension
    {
        private string GetLocalizedValue(object enumValue)
        {
            string str = "AnnotationProperties" + enumValue + "MarkupType";
            return PdfViewerLocalizer.GetString((PdfViewerStringId) Enum.Parse(typeof(PdfViewerStringId), str));
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            from x in Enum.GetValues(typeof(PdfTextMarkupAnnotationType)).Cast<object>() select new { 
                Value = x,
                DisplayName = this.GetLocalizedValue(x)
            };
    }
}

