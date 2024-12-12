namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(XEToggleSwitchTextBrickExporter))]
    public class XEToggleSwitchTextBrick : ToggleSwitchTextBrick
    {
        public XEToggleSwitchTextBrick()
        {
        }

        public XEToggleSwitchTextBrick(BrickStyle style) : base(style)
        {
        }

        public override string BrickType =>
            "XEToggleSwitchText";
    }
}

