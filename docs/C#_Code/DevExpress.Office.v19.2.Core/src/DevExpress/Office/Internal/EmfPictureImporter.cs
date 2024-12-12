namespace DevExpress.Office.Internal
{
    using DevExpress.Office.Import;
    using DevExpress.Office.Utils;

    public class EmfPictureImporter : PictureImporter
    {
        private static readonly FileDialogFilter filter = new FileDialogFilter(OfficeLocalizer.GetString(OfficeStringId.FileFilterDescription_EmfFiles), "emf");

        public override IImporterOptions SetupLoading() => 
            new EmfPictureImporterOptions();

        public override FileDialogFilter Filter =>
            filter;

        public override OfficeImageFormat Format =>
            OfficeImageFormat.Emf;
    }
}

