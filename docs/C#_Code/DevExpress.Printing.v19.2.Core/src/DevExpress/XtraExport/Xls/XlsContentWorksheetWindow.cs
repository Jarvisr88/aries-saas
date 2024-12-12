namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentWorksheetWindow : XlsContentBase
    {
        private int topRowIndex;
        private int leftColumnIndex;
        private int gridlinesColorIndex;
        private int zoomScalePageBreakPreview;
        private int zoomScaleNormalView;

        public override int GetSize() => 
            0x12;

        public override void Read(XlReader reader, int size)
        {
            ushort num = reader.ReadUInt16();
            this.ShowFormulas = Convert.ToBoolean((int) (num & 1));
            this.ShowGridlines = Convert.ToBoolean((int) (num & 2));
            this.ShowRowColumnHeadings = Convert.ToBoolean((int) (num & 4));
            this.Frozen = Convert.ToBoolean((int) (num & 8));
            this.ShowZeroValues = Convert.ToBoolean((int) (num & 0x10));
            this.RightToLeft = Convert.ToBoolean((int) (num & 0x40));
            this.ShowOutlineSymbols = Convert.ToBoolean((int) (num & 0x80));
            this.FrozenWithoutPaneSplit = Convert.ToBoolean((int) (num & 0x100));
            this.SheetTabIsSelected = Convert.ToBoolean((int) (num & 0x200));
            this.CurrentlyDisplayed = Convert.ToBoolean((int) (num & 0x400));
            this.InPageBreakPreview = Convert.ToBoolean((int) (num & 0x800));
            this.TopRowIndex = reader.ReadUInt16();
            this.LeftColumnIndex = reader.ReadUInt16();
            this.GridlinesColorIndex = reader.ReadUInt16();
            if ((num & 0x20) != 0)
            {
                this.GridlinesColorIndex = 0x40;
            }
            reader.ReadUInt16();
            if (size != 10)
            {
                this.zoomScalePageBreakPreview = reader.ReadUInt16();
                this.zoomScaleNormalView = reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            ushort num = 0;
            if (this.ShowFormulas)
            {
                num = (ushort) (num | 1);
            }
            if (this.ShowGridlines)
            {
                num = (ushort) (num | 2);
            }
            if (this.ShowRowColumnHeadings)
            {
                num = (ushort) (num | 4);
            }
            if (this.Frozen)
            {
                num = (ushort) (num | 8);
            }
            if (this.ShowZeroValues)
            {
                num = (ushort) (num | 0x10);
            }
            if (this.GridlinesInDefaultColor)
            {
                num = (ushort) (num | 0x20);
            }
            if (this.RightToLeft)
            {
                num = (ushort) (num | 0x40);
            }
            if (this.ShowOutlineSymbols)
            {
                num = (ushort) (num | 0x80);
            }
            if (this.FrozenWithoutPaneSplit)
            {
                num = (ushort) (num | 0x100);
            }
            if (this.SheetTabIsSelected)
            {
                num = (ushort) (num | 0x200);
            }
            if (this.CurrentlyDisplayed)
            {
                num = (ushort) (num | 0x400);
            }
            if (this.InPageBreakPreview)
            {
                num = (ushort) (num | 0x800);
            }
            writer.Write(num);
            writer.Write((ushort) this.TopRowIndex);
            writer.Write((ushort) this.LeftColumnIndex);
            writer.Write((ushort) this.GridlinesColorIndex);
            writer.Write((ushort) 0);
            writer.Write((ushort) this.ZoomScalePageBreakPreview);
            writer.Write((ushort) this.ZoomScaleNormalView);
            writer.Write((ushort) 0);
            writer.Write((ushort) 0);
        }

        public bool ShowFormulas { get; set; }

        public bool ShowGridlines { get; set; }

        public bool ShowRowColumnHeadings { get; set; }

        public bool Frozen { get; set; }

        public bool ShowZeroValues { get; set; }

        public bool GridlinesInDefaultColor =>
            this.gridlinesColorIndex == 0x40;

        public bool RightToLeft { get; set; }

        public bool ShowOutlineSymbols { get; set; }

        public bool FrozenWithoutPaneSplit { get; set; }

        public bool SheetTabIsSelected { get; set; }

        public bool CurrentlyDisplayed { get; set; }

        public bool InPageBreakPreview { get; set; }

        public int TopRowIndex
        {
            get => 
                this.topRowIndex;
            set
            {
                base.CheckValue(value, 0, 0xffff, "TopRowIndex");
                this.topRowIndex = value;
            }
        }

        public int LeftColumnIndex
        {
            get => 
                this.leftColumnIndex;
            set
            {
                base.CheckValue(value, 0, 0xff, "LeftColumnIndex");
                this.leftColumnIndex = value;
            }
        }

        public int GridlinesColorIndex
        {
            get => 
                this.gridlinesColorIndex;
            set
            {
                base.CheckValue(value, 0, 0x40, "GridlinesColorIndex");
                this.gridlinesColorIndex = value;
            }
        }

        public int ZoomScalePageBreakPreview
        {
            get => 
                this.zoomScalePageBreakPreview;
            set
            {
                base.CheckValue(value, 0, 0xffff, "ZoomScalePageBreakPreview");
                this.zoomScalePageBreakPreview = value;
            }
        }

        public int ZoomScaleNormalView
        {
            get => 
                this.zoomScaleNormalView;
            set
            {
                base.CheckValue(value, 0, 0xffff, "ZoomScaleNormalView");
                this.zoomScaleNormalView = value;
            }
        }
    }
}

