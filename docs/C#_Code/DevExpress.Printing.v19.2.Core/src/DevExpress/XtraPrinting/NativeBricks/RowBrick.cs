namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(RowBrickExporter))]
    public class RowBrick : PanelBrick
    {
        public RowBrick() : this(NullBrickOwner.Instance)
        {
        }

        public RowBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.NoClip = true;
        }

        public override string BrickType =>
            "Row";
    }
}

