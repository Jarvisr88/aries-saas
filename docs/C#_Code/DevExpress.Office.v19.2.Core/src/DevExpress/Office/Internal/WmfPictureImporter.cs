namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;

    public class WmfPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_WmfFiles), "wmf");

        public override IImporterOptions SetupLoading() => 
            new WmfPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Wmf;
    }
}

