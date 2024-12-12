namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native.LayoutAdjustment;
    using System;

    [BrickExporter(typeof(TableBrickExporter))]
    public class TableBrick : PanelBrick
    {
        public TableBrick() : this(NullBrickOwner.Instance)
        {
        }

        public TableBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.NoClip = true;
        }

        public override ILayoutData CreateLayoutData(float dpi) => 
            new TableBrickLayoutData(this, dpi);

        [XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override bool RightToLeftLayout
        {
            get => 
                false;
            set
            {
            }
        }

        public override string BrickType =>
            "Table";
    }
}

