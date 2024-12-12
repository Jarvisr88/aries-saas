namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;

    public class GifPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_GifFiles), "gif");

        public override IImporterOptions SetupLoading() => 
            new GifPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Gif;
    }
}

