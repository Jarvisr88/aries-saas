namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    public class OfficeWmfImageWinAzure : OfficeMetafileImageWin
    {
        public OfficeWmfImageWinAzure(Metafile image, MemoryStream imageStream, IUniqueImageId id) : base(image, imageStream, id)
        {
        }

        protected override OfficeImage CreateClone(IDocumentModel documentModel)
        {
            OfficeImage imageById = documentModel.GetImageById(base.Id);
            if (imageById != null)
            {
                return imageById;
            }
            MemoryStreamBasedImage image2 = ImageLoaderHelper.ImageFromStream(base.ImageStream);
            return new OfficeEmfImageWinAzure((Metafile) image2.Image, image2.ImageStream, base.Id);
        }

        public override byte[] GetWmfImageBytes() => 
            base.ImageStream.ToArray();

        protected internal override bool OverrideResolution =>
            false;
    }
}

