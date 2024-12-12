namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class ContinuousExportInfo : IXtraSortableProperties
    {
        public static readonly ContinuousExportInfo Empty;
        private Margins margins;
        private Rectangle pageBounds;
        private float bottomMarginOffset;
        private ICollection multiColumnInfo;
        private List<PageBreakData> pageBreaks;

        static ContinuousExportInfo();
        protected ContinuousExportInfo();
        protected ContinuousExportInfo(ContinuousExportInfo info);
        protected ContinuousExportInfo(Margins margins, Rectangle pageBounds, float bottomMarginOffset, ICollection multiColumnInfo);
        bool IXtraSortableProperties.ShouldSortProperties();
        public virtual void ExecuteExport(IBrickExportVisitor brickVisitor, IPrintingSystemContext context);
        protected void ExportBrick(Brick brick, RectangleDF rect, IBrickExportVisitor brickVisitor, IPrintingSystemContext context);
        protected virtual void PreprocessBrick(Brick brick, IPrintingSystemContext context);

        [XtraSerializableProperty(0)]
        public Margins PageMargins { get; set; }

        [XtraSerializableProperty(1)]
        public Rectangle PageBounds { get; set; }

        [XtraSerializableProperty(2)]
        public float BottomMarginOffset { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.SimpleCollection, true, false, false, 3)]
        public ICollection PageBreakPositions { get; protected set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 3)]
        public IList<PageBreakData> PageBreaks { get; }

        internal List<PageBreakData> PageBreaksInternal { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 4)]
        public ICollection MultiColumnInfo { get; }

        [XtraSerializableProperty(XtraSerializationVisibility.Collection, true, false, false, 5)]
        public ICollection Bricks { get; protected set; }

        internal bool IsEmpty { get; }
    }
}

