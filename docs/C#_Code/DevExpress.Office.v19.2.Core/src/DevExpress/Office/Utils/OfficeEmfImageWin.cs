namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing.Imaging;
    using System.IO;

    public class OfficeEmfImageWin : OfficeMetafileImageWin
    {
        public OfficeEmfImageWin(Metafile image, IUniqueImageId id) : base(image, id)
        {
        }

        protected OfficeEmfImageWin(Metafile image, MemoryStream imageStream, IUniqueImageId id) : base(image, imageStream, id)
        {
        }

        protected override OfficeImage CreateClone(IDocumentModel targetModel)
        {
            OfficeImage imageById = targetModel.GetImageById(base.Id);
            return ((imageById == null) ? new OfficeEmfImageWin((Metafile) this.NativeImage.Clone(), base.Id) : imageById);
        }

        protected internal override bool OverrideResolution =>
            true;
    }
}

