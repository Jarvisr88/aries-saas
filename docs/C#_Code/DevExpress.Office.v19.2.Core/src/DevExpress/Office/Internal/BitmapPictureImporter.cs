namespace DevExpress.Office.Internal
{
    using DevExpress.Office;
    using DevExpress.Office.Import;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class BitmapPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter;

        static BitmapPictureImporter()
        {
            string[] extensions = new string[] { "bmp", "dib" };
            filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_BitmapFiles), extensions);
        }

        public override OfficeImage LoadDocument(IDocumentModel documentModel, Stream stream, IImporterOptions options) => 
            documentModel.CreateImage(stream);

        public override IImporterOptions SetupLoading() => 
            new BitmapPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Bmp;
    }
}

