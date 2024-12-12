namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class BrickPaintService : BrickPaintServiceBase
    {
        public BrickPaintService(EditingFieldCollection editingFields, IServiceProvider provider, float dpi = -1f) : base(editingFields, provider, dpi)
        {
        }

        protected override System.Drawing.Image CreateImage(bool isAscending)
        {
            SvgImage svgImage = ResourceImageHelper.CreateSvgImageFromResources(isAscending ? "DevExpress.Printing.Core.Images.SortAsc.svg" : "DevExpress.Printing.Core.Images.SortDes.svg", typeof(IBrickPaintService).Assembly);
            if (!base.UseDpi())
            {
                return CreateMetafileFromSvg(new SvgBitmap(svgImage), new Rectangle(0, 0, (int) svgImage.Width, (int) svgImage.Height));
            }
            SizeF ef = GraphicsUnitConverter.Convert(new SizeF((float) svgImage.Width, (float) svgImage.Height), (float) 96f, base.Dpi);
            Bitmap bitmap = (Bitmap) new SvgBitmap(svgImage).Render(ef.ToSize(), null, DefaultBoolean.Default, DefaultBoolean.Default);
            bitmap.SetResolution(base.Dpi, base.Dpi);
            return bitmap;
        }

        private static System.Drawing.Image CreateMetafileFromSvg(SvgBitmap bitmap, Rectangle bounds)
        {
            if ((bounds.Width == 0) || (bounds.Height == 0))
            {
                return null;
            }
            using (Graphics graphics = Graphics.FromHwndInternal(IntPtr.Zero))
            {
                System.Drawing.Image image = graphics.CreateMetafile(bounds, MetafileFrameUnit.Pixel, EmfType.EmfPlusOnly);
                using (Graphics graphics2 = Graphics.FromImage(image))
                {
                    bitmap.RenderToGraphics(graphics2, null, 1.0, DefaultBoolean.Default);
                }
                return image;
            }
        }
    }
}

