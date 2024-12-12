namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Office.PInvoke;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public abstract class OfficeMetafileImageWin : OfficeImageWin
    {
        private Size metafileSizeInHundredthsOfMillimeter;

        protected OfficeMetafileImageWin(Metafile image, IUniqueImageId id) : this(image, null, id)
        {
        }

        protected OfficeMetafileImageWin(Metafile image, MemoryStream imageStream, IUniqueImageId id) : base(image, imageStream, id)
        {
            this.metafileSizeInHundredthsOfMillimeter = base.EnsureNonZeroSize(Size.Round(image.PhysicalDimension));
        }

        protected internal override Size CalculateImageSizeInModelUnits(DocumentModelUnitConverter unitConverter) => 
            base.EnsureNonZeroSize(unitConverter.HundredthsOfMillimeterToModelUnits(this.SizeInOriginalUnits));

        public override byte[] GetWmfImageBytes()
        {
            byte[] buffer;
            using (Metafile metafile = (Metafile) this.NativeImage.Clone())
            {
                IntPtr henhmetafile = metafile.GetHenhmetafile();
                try
                {
                    buffer = Win32.GdipEmfToWmfBits(henhmetafile, Win32.MapMode.Anisotropic, Win32.EmfToWmfBitsFlags.EmfToWmfBitsFlagsDefault);
                }
                finally
                {
                    MetafileHelper.DeleteMetafileHandle(henhmetafile);
                }
            }
            return buffer;
        }

        protected internal override void OnNativeImageChanged(Size desiredSize)
        {
            this.metafileSizeInHundredthsOfMillimeter = Size.Empty;
            base.OnNativeImageChanged(desiredSize);
        }

        protected override void ReplaceInvalidImage()
        {
        }

        protected override bool TryRepairImage() => 
            false;

        protected internal override Size UnitsToDocuments(Size sizeInUnits) => 
            base.EnsureNonZeroSize(Units.HundredthsOfMillimeterToDocuments(sizeInUnits));

        protected internal override Size UnitsToHundredthsOfMillimeter(Size sizeInUnits) => 
            sizeInUnits;

        protected internal override Size UnitsToPixels(Size sizeInUnits) => 
            base.EnsureNonZeroSize(Units.HundredthsOfMillimeterToPixels(sizeInUnits, this.VerticalResolution, this.HorizontalResolution));

        protected internal override Size UnitsToTwips(Size sizeInUnits) => 
            base.EnsureNonZeroSize(Units.HundredthsOfMillimeterToTwips(sizeInUnits));

        public Size MetafileSizeInHundredthsOfMillimeter
        {
            get => 
                this.metafileSizeInHundredthsOfMillimeter;
            set => 
                this.metafileSizeInHundredthsOfMillimeter = value;
        }

        protected internal abstract bool OverrideResolution { get; }

        public override float HorizontalResolution =>
            96f;

        public override float VerticalResolution =>
            96f;

        public override Size SizeInOriginalUnits =>
            this.MetafileSizeInHundredthsOfMillimeter;
    }
}

