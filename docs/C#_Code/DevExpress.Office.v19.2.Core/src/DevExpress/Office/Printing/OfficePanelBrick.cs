namespace DevExpress.Office.Printing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [BrickExporter(typeof(OfficePanelBrickExporter))]
    public class OfficePanelBrick : PanelBrick
    {
        internal readonly DocumentLayoutUnitConverter unitConverter;

        public OfficePanelBrick(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
        }

        public PointF AbsoluteLocation { get; set; }
    }
}

