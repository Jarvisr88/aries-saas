namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using System;

    public class JPEGPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter;

        static JPEGPictureImporter()
        {
            string[] extensions = new string[] { "jpg", "jpeg" };
            filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_JPEGFiles), extensions);
        }

        public override IImporterOptions SetupLoading() => 
            new JPEGPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Jpeg;
    }
}

