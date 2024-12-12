namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public class DictionaryContinuousExportInfo : ContinuousExportInfo
    {
        private Dictionary<Brick, RectangleDF> innerBricks;

        public DictionaryContinuousExportInfo(Dictionary<Brick, RectangleDF> bricks, Margins margins, Rectangle pageBounds, float bottomMarginOffset, ICollection multiColumnInfo);
        public override void ExecuteExport(IBrickExportVisitor brickVisitor, IPrintingSystemContext context);
    }
}

