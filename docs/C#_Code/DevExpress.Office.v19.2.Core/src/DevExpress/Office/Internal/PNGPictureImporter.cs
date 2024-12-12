namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;

    public class PNGPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_PNGFiles), "png");

        public override IImporterOptions SetupLoading() => 
            new PNGPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Png;
    }
}

