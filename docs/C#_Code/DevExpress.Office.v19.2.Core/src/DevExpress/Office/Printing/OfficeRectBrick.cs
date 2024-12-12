namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(OfficeRectBrickExporter))]
    public class OfficeRectBrick : VisualBrick
    {
        internal readonly DocumentLayoutUnitConverter unitConverter;

        public OfficeRectBrick(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }
    }
}

