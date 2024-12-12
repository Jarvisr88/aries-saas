namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public abstract class LayoutBuilder : ILayoutBuilder, IBrickExportVisitor
    {
        private Document document;
        private LayoutControlCollection layoutControls;
        private ContinuousExportInfo exportInfo;
        private int offsetYMult;

        protected LayoutBuilder(Document document)
        {
            this.document = document;
        }

        public LayoutControlCollection BuildLayoutControls()
        {
            this.layoutControls = new LayoutControlCollection();
            this.exportInfo = this.document.GetContinuousExportInfo();
            this.exportInfo.ExecuteExport(this, this.document.PrintingSystem);
            return this.layoutControls;
        }

        protected virtual ILayoutControl CreateLayoutControl(BrickViewData data, Brick brick) => 
            LayoutControl.Validate(data);

        void IBrickExportVisitor.ExportBrick(double horizontalOffset, double verticalOffset, Brick brick)
        {
            ILayoutControl[] values = this.GetLayoutControlInPixels(brick, horizontalOffset, verticalOffset);
            if (values != null)
            {
                this.layoutControls.AddRange(values);
            }
        }

        protected abstract ILayoutControl[] GetBrickLayoutControls(Brick brick, RectangleF rect);
        private unsafe ILayoutControl[] GetLayoutControlInPixels(Brick brick, double horizontalOffset, double verticalOffset)
        {
            RectangleDF val = RectangleDF.FromRectangleF(brick.InitialRect);
            if (val.IsEmpty)
            {
                return null;
            }
            val.X = horizontalOffset;
            val.Y = verticalOffset;
            val = GraphicsUnitConverter.Convert(val, (float) 300f, (float) 96f);
            this.offsetYMult = (int) (((long) val.Y) >> 13);
            RectangleDF* edfPtr1 = &val;
            edfPtr1.Y -= this.offsetYMult << 13;
            ILayoutControl[] brickLayoutControls = this.GetBrickLayoutControls(brick, val.ToRectangleF());
            this.offsetYMult = 0;
            return brickLayoutControls;
        }

        protected ILayoutControl[] ToLayoutControls(BrickViewData[] data, Brick brick)
        {
            if (data == null)
            {
                return null;
            }
            ILayoutControl[] controlArray = new ILayoutControl[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i].SetOffsetY(this.offsetYMult);
                controlArray[i] = this.CreateLayoutControl(data[i], brick);
            }
            return controlArray;
        }

        internal Margins PageMargins =>
            this.exportInfo.PageMargins;

        internal Rectangle PageBounds =>
            this.exportInfo.PageBounds;

        internal float BottomMarginOffset =>
            this.exportInfo.BottomMarginOffset;

        internal IList<PageBreakData> PageBreaks =>
            this.exportInfo.PageBreaks;

        internal ICollection MultiColumnInfo =>
            this.exportInfo.MultiColumnInfo;
    }
}

