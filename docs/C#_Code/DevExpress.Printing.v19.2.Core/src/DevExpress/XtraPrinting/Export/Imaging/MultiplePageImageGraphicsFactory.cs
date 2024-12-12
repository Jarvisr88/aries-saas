namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System.Drawing;

    public class MultiplePageImageGraphicsFactory : ImageGraphicsFactory
    {
        public override IGraphics CreateGraphics(System.Drawing.Image img, PrintingSystemBase ps) => 
            new ImageGraphics(img, ps);
    }
}

