namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(OfficeTextBrickExporter))]
    public class OfficeTextBrick : TextBrick
    {
        internal readonly DocumentLayoutUnitConverter unitConverter;

        public OfficeTextBrick(DocumentLayoutUnitConverter unitConverter) : this(unitConverter, NullBrickOwner.Instance)
        {
        }

        public OfficeTextBrick(DocumentLayoutUnitConverter unitConverter, IBrickOwner brickOwner) : base(brickOwner)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public void SetBrickOwner(IBrickOwner brickOwner)
        {
            base.BrickOwner = brickOwner;
        }
    }
}

