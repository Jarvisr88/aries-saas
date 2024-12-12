namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    public class OfficeEmfImageWinAzure : OfficeEmfImageWin
    {
        public OfficeEmfImageWinAzure(Metafile image, MemoryStream imageStream, IUniqueImageId id) : base(image, imageStream, id)
        {
        }

        protected override OfficeImage CreateClone(IDocumentModel targetModel)
        {
            OfficeImage imageById = targetModel.GetImageById(base.Id);
            if (imageById != null)
            {
                return imageById;
            }
            MemoryStreamBasedImage image2 = ImageLoaderHelper.ImageFromStream(base.ImageStream);
            return new OfficeEmfImageWinAzure((Metafile) image2.Image, image2.ImageStream, base.Id);
        }

        public override byte[] GetEmfImageBytes() => 
            base.ImageStream.ToArray();

        protected internal override bool OverrideResolution =>
            true;
    }
}

