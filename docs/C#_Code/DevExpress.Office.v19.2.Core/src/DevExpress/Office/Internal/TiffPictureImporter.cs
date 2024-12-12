namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Localization;
    using DevExpress.Office.Utils;
    using System;

    public class TiffPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter;

        static TiffPictureImporter()
        {
            string[] extensions = new string[] { "tif", "tiff" };
            filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_TiffFiles), extensions);
        }

        public override IImporterOptions SetupLoading() => 
            new TiffPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Tiff;
    }
}

