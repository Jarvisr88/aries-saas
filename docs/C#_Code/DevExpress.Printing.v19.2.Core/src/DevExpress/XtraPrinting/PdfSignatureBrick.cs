namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(PdfSignatureBrickExporter))]
    public class PdfSignatureBrick : Brick
    {
        public override string BrickType =>
            "PdfSignature";
    }
}

