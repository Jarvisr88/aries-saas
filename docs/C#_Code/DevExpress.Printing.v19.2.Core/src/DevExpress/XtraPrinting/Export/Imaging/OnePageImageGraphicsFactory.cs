namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using System.Drawing;

    public class OnePageImageGraphicsFactory : ImageGraphicsFactory
    {
        public override IGraphics CreateGraphics(Image img, PrintingSystemBase ps) => 
            new OnePageImageGraphics(img, ps);
    }
}

