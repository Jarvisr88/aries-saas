namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Collections;

    [BrickExporter(typeof(ContainerBrickBaseExporter))]
    public abstract class ContainerBrickBase : PanelBrick
    {
        protected ContainerBrickBase(BrickStyle style) : base(style)
        {
        }

        protected ContainerBrickBase(IBrickOwner brickOwner) : base(brickOwner)
        {
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override BrickCollectionBase Bricks =>
            base.Bricks;

        internal override IList InnerBrickList =>
            EmptyBrickCollection.Instance;
    }
}

