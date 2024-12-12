namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(XECheckBoxBrickExporter))]
    public class XECheckBoxBrick : CheckBoxBrick
    {
        public XECheckBoxBrick()
        {
        }

        public XECheckBoxBrick(BrickStyle style) : base(style)
        {
        }

        internal XECheckBoxBrick(XECheckBoxBrick brick) : base(brick)
        {
        }

        public override object Clone() => 
            new XECheckBoxBrick(this);

        public override string BrickType =>
            "XECheckBox";
    }
}

