namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;

    [BrickExporter(typeof(OfficeImageBrickExporter))]
    public class OfficeImageBrick : ImageBrick
    {
        internal readonly DocumentLayoutUnitConverter unitConverter;

        public OfficeImageBrick(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }
    }
}

