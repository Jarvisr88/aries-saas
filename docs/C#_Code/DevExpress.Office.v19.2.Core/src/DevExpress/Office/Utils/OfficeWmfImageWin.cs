namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;
    using System.Drawing.Imaging;

    public class OfficeWmfImageWin : OfficeMetafileImageWin
    {
        public OfficeWmfImageWin(Metafile image, IUniqueImageId id) : base(image, id)
        {
        }

        protected override OfficeImage CreateClone(IDocumentModel documentModel)
        {
            OfficeImage imageById = documentModel.GetImageById(base.Id);
            return ((imageById == null) ? new OfficeWmfImageWin((Metafile) this.NativeImage.Clone(), base.Id) : imageById);
        }

        public override byte[] GetWmfImageBytes()
        {
            byte[] emfMetaFileBits;
            using (Metafile metafile = (Metafile) this.NativeImage.Clone())
            {
                IntPtr henhmetafile = metafile.GetHenhmetafile();
                try
                {
                    emfMetaFileBits = MetafileHelper.GetEmfMetaFileBits(henhmetafile, this.NativeImage.PhysicalDimension);
                }
                finally
                {
                    MetafileHelper.DeleteMetafileHandle(henhmetafile);
                }
            }
            return emfMetaFileBits;
        }

        protected internal override bool OverrideResolution =>
            false;
    }
}

