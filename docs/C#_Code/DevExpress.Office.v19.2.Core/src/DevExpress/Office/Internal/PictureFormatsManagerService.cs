namespace DevExpress.Office.Internal
{
    using System;

    public class PictureFormatsManagerService : ImportManagerService<OfficeImageFormat, OfficeImage>, IPictureImportManagerService, IImportManagerService<OfficeImageFormat, OfficeImage>
    {
        protected internal override void RegisterNativeFormats()
        {
            this.RegisterImporter(new BitmapPictureImporter());
            this.RegisterImporter(new JPEGPictureImporter());
            this.RegisterImporter(new PNGPictureImporter());
            this.RegisterImporter(new GifPictureImporter());
            this.RegisterImporter(new TiffPictureImporter());
            this.RegisterImporter(new EmfPictureImporter());
            this.RegisterImporter(new WmfPictureImporter());
        }
    }
}

