namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;
    using System;
    using System.Text;

    public abstract class ImageImportHelper : ImportHelper<OfficeImageFormat, OfficeImage>
    {
        protected ImageImportHelper(IDocumentModel documentModel) : base(documentModel)
        {
        }

        protected internal override void ApplyEncoding(IImporterOptions options, Encoding encoding)
        {
        }

        protected internal override IImporterOptions GetPredefinedOptions(OfficeImageFormat format) => 
            null;

        protected internal override OfficeImageFormat UndefinedFormat =>
            OfficeImageFormat.None;

        protected internal override OfficeImageFormat FallbackFormat =>
            this.UndefinedFormat;
    }
}

