namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class ImageGraphicsFactory
    {
        public static readonly DevExpress.XtraPrinting.Export.Imaging.MultiplePageImageGraphicsFactory MultiplePageImageGraphicsFactory = new DevExpress.XtraPrinting.Export.Imaging.MultiplePageImageGraphicsFactory();
        public static readonly DevExpress.XtraPrinting.Export.Imaging.OnePageImageGraphicsFactory OnePageImageGraphicsFactory = new DevExpress.XtraPrinting.Export.Imaging.OnePageImageGraphicsFactory();

        protected ImageGraphicsFactory()
        {
        }

        public abstract IGraphics CreateGraphics(Image img, PrintingSystemBase ps);
    }
}

