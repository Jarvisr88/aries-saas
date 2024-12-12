namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(XECheckBoxTextBrickExporter))]
    public class XECheckBoxTextBrick : CheckBoxTextBrick
    {
        public XECheckBoxTextBrick()
        {
        }

        public XECheckBoxTextBrick(BrickStyle style) : base(style)
        {
        }

        public override string BrickType =>
            "XECheckBoxText";
    }
}

