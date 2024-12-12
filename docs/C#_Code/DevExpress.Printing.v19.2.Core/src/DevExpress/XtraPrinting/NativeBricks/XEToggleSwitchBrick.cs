namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(XEToggleSwitchBrickExporter))]
    public class XEToggleSwitchBrick : ToggleSwitchBrick
    {
        public XEToggleSwitchBrick()
        {
        }

        public XEToggleSwitchBrick(BrickStyle style) : base(style)
        {
        }

        public override string BrickType =>
            "XEToggleSwitch";
    }
}

