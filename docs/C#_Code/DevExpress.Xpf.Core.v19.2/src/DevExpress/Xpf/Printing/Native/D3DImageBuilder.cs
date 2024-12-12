namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Drawing;
    using System.Windows.Interop;

    public class D3DImageBuilder : ImagesSourceBuilder<D3DImage>
    {
        internal D3DImageBuilder(D3DImage source) : base(source)
        {
        }

        public override Bitmap CreateImage()
        {
            throw new NotImplementedException();
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            throw new NotImplementedException();
        }
    }
}

