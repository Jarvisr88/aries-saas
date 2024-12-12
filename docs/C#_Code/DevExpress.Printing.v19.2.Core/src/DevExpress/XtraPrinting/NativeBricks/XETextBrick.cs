namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(XETextBrickExporter))]
    public class XETextBrick : TextBrick
    {
        public XETextBrick()
        {
        }

        public XETextBrick(BrickStyle style) : base(style)
        {
        }

        internal XETextBrick(XETextBrick brick) : base(brick)
        {
        }

        public override object Clone() => 
            new XETextBrick(this);

        public override string BrickType =>
            "XEText";

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public float FTabInterval { get; set; }
    }
}

