namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(OfficeTextBrickWithOffsetExporter))]
    public class OfficeTextBrickWithOffset : TextBrick
    {
        internal readonly DocumentLayoutUnitConverter unitConverter;

        public OfficeTextBrickWithOffset(DocumentLayoutUnitConverter unitConverter) : this(unitConverter, NullBrickOwner.Instance)
        {
        }

        public OfficeTextBrickWithOffset(DocumentLayoutUnitConverter unitConverter, IBrickOwner brickOwner) : base(brickOwner)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public void SetBrickOwner(IBrickOwner brickOwner)
        {
            base.BrickOwner = brickOwner;
        }

        public bool RightToLeft { get; set; }

        public int VerticalTextOffset { get; set; }
    }
}

