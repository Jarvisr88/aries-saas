namespace DevExpress.XtraPrinting.Export.Imaging
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public class OnePageImageGraphics : ImageGraphics
    {
        public OnePageImageGraphics(System.Drawing.Image img, PrintingSystemBase ps) : base(img, ps)
        {
        }

        public override int GetPageCount(int basePageNumber, DefaultBoolean continuousPageNumbering) => 
            basePageNumber + 1;
    }
}

