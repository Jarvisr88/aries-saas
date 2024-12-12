namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ExportBand
    {
        private List<Brick> bricks;
        private LinkedListNode<ExportBand> selfNode;
        private DevExpress.XtraPrinting.Native.DocumentBand documentBand;
        private PointF pageOffset;
        private int pageRowIndex;
        private Page page;

        public ExportBand(DevExpress.XtraPrinting.Native.DocumentBand documentBand, PointF location, Page page, int pageRowIndex)
        {
            this.documentBand = documentBand;
            this.pageOffset = location;
            this.page = page;
            this.pageRowIndex = pageRowIndex;
        }

        private void AddExportBandBricks(IBrickExportVisitor visitor, float verticalOffset, Brick brick)
        {
            visitor.ExportBrick((double) this.PageOffsetWithoutMargins.X, (double) verticalOffset, brick);
        }

        internal float AddExportBandElements(IBrickExportVisitor visitor, float verticalOffset)
        {
            foreach (Brick brick in this.Bricks)
            {
                this.AddExportBandBricks(visitor, verticalOffset, brick);
            }
            return (verticalOffset + this.UpriseVerticalOffset());
        }

        private bool EndOfRow(ExportBand exportBand)
        {
            if (exportBand.IsLast)
            {
                return true;
            }
            PointF pageOffsetWithoutMargins = exportBand.NextBand.PageOffsetWithoutMargins;
            return ((exportBand.PageOffsetWithoutMargins.X >= pageOffsetWithoutMargins.X) || !(exportBand.PageOffsetWithoutMargins.Y == pageOffsetWithoutMargins.Y));
        }

        private float ExactOffsetToNextBand(float upOffset) => 
            this.LastInRowOfPages ? ((!this.NextParentDifferent || !this.NextAncestorDifferent()) ? upOffset : (upOffset + this.GetParentBottomSpans(this.DocumentBand))) : Math.Max(upOffset, this.NextBand.PageOffsetWithoutMargins.Y - this.PageOffsetWithoutMargins.Y);

        internal float FirstOffset() => 
            !this.DocumentBand.Kind.IsFooter() ? this.PageOffsetWithoutMargins.Y : 0f;

        private DevExpress.XtraPrinting.Native.DocumentBand GetAncestor(DevExpress.XtraPrinting.Native.DocumentBand docBand)
        {
            DevExpress.XtraPrinting.Native.DocumentBand parent = docBand;
            if (parent.Parent != null)
            {
                while (parent.Parent.Parent != null)
                {
                    parent = parent.Parent;
                }
            }
            return parent;
        }

        private float GetMaxRowHeight()
        {
            float selfHeight = this.DocumentBand.SelfHeight;
            ExportBand exportBand = this.selfNode.Value;
            while (!this.IsFirstColumn(exportBand))
            {
                exportBand = exportBand.PreviousBand;
                selfHeight = Math.Max(selfHeight, exportBand.DocumentBand.SelfHeight);
            }
            return selfHeight;
        }

        private float GetParentBottomSpans(DevExpress.XtraPrinting.Native.DocumentBand docBand)
        {
            float num = 0f;
            DevExpress.XtraPrinting.Native.DocumentBand parent = docBand;
            while (parent.Parent != null)
            {
                parent = parent.Parent;
                num += parent.BottomSpan;
            }
            return num;
        }

        private bool IsFirstColumn(ExportBand exportBand) => 
            exportBand.IsFirst || this.EndOfRow(exportBand.PreviousBand);

        private bool IsHeaderOrNextFooter() => 
            this.NextBand.DocumentBand.Kind.IsFooter() || this.DocumentBand.Kind.IsHeader();

        private bool NextAncestorDifferent() => 
            !ReferenceEquals(this.GetAncestor(this.documentBand), this.GetAncestor(this.NextBand.documentBand));

        private float OffsetPreviousLastBand() => 
            !this.NextBand.IsLast ? 0f : this.GetParentBottomSpans(this.DocumentBand);

        private float OffsetToNextBand()
        {
            float upOffset = 0f;
            if (!this.EndOfRow(this))
            {
                return 0f;
            }
            upOffset = this.GetMaxRowHeight();
            return (!this.IsHeaderOrNextFooter() ? this.ExactOffsetToNextBand(upOffset) : (upOffset + this.OffsetPreviousLastBand()));
        }

        private float UpriseVerticalOffset() => 
            this.IsLast ? 0f : this.OffsetToNextBand();

        private PointF PageOffsetWithoutMargins =>
            new PointF(this.pageOffset.X, Math.Max((float) (this.pageOffset.Y - this.page.MarginsF.Top), (float) 0f));

        public List<Brick> Bricks
        {
            get
            {
                this.bricks ??= new List<Brick>();
                return this.bricks;
            }
        }

        public DevExpress.XtraPrinting.Native.DocumentBand DocumentBand =>
            this.documentBand;

        internal LinkedListNode<ExportBand> SelfNode
        {
            get => 
                this.selfNode;
            set => 
                this.selfNode = value;
        }

        internal PointF PageOffset =>
            this.pageOffset;

        internal int PageRowIndex =>
            this.pageRowIndex;

        internal bool IsFirst =>
            (this.selfNode == null) || ReferenceEquals(this.selfNode.Previous, null);

        internal bool IsLast =>
            (this.selfNode == null) || ReferenceEquals(this.selfNode.Next, null);

        internal ExportBand PreviousBand =>
            !this.IsFirst ? this.selfNode.Previous.Value : null;

        internal ExportBand NextBand =>
            !this.IsLast ? this.selfNode.Next.Value : null;

        internal bool LastInRowOfPages =>
            this.pageRowIndex != this.NextBand.pageRowIndex;

        internal bool NextParentDifferent =>
            !ReferenceEquals(this.documentBand.Parent, this.NextBand.documentBand.Parent);
    }
}

