namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public abstract class PictureImporter : IImporter<OfficeImageFormat, OfficeImage>
    {
        protected PictureImporter()
        {
        }

        public virtual OfficeImage LoadDocument(IDocumentModel documentModel, Stream stream, IImporterOptions options) => 
            this.LoadDocument(documentModel, stream, options, true);

        public virtual OfficeImage LoadDocument(IDocumentModel documentModel, Stream stream, IImporterOptions options, bool leaveOpen) => 
            documentModel.CreateImage(stream);

        public abstract IImporterOptions SetupLoading();

        public abstract FileDialogFilter Filter { get; }

        public abstract OfficeImageFormat Format { get; }
    }
}

