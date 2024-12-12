namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentWorkbookWindow : XlsContentBase
    {
        private int horizontalPosition;
        private int verticalPosition;
        private int widthInTwips = 1;
        private int heightInTwips = 1;
        private int selectedTabIndex;
        private int firstDisplayedTabIndex;
        private int selectedTabsCount;
        private int tabRatio = 600;

        public override int GetSize() => 
            0x12;

        public override void Read(XlReader reader, int size)
        {
            this.horizontalPosition = reader.ReadInt16();
            this.verticalPosition = reader.ReadInt16();
            this.widthInTwips = base.ValueInRange(reader.ReadUInt16(), 1, 0x7fff);
            this.heightInTwips = base.ValueInRange(reader.ReadUInt16(), 1, 0x7fff);
            ushort num = reader.ReadUInt16();
            this.IsHidden = (num & 1) != 0;
            this.IsMinimized = (num & 2) != 0;
            this.IsVeryHidden = (num & 4) != 0;
            this.HorizontalScrollDisplayed = (num & 8) != 0;
            this.VerticalScrollDisplayed = (num & 0x10) != 0;
            this.SheetTabsDisplayed = (num & 0x20) != 0;
            this.NoAutoFilterDateGrouping = (num & 0x40) != 0;
            this.selectedTabIndex = reader.ReadInt16();
            this.firstDisplayedTabIndex = reader.ReadInt16();
            this.selectedTabsCount = reader.ReadInt16();
            this.tabRatio = reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((short) this.horizontalPosition);
            writer.Write((short) this.verticalPosition);
            writer.Write((short) this.widthInTwips);
            writer.Write((short) this.heightInTwips);
            ushort num = 0;
            if (this.IsHidden)
            {
                num = (ushort) (num | 1);
            }
            if (this.IsMinimized)
            {
                num = (ushort) (num | 2);
            }
            if (this.IsVeryHidden)
            {
                num = (ushort) (num | 4);
            }
            if (this.HorizontalScrollDisplayed)
            {
                num = (ushort) (num | 8);
            }
            if (this.VerticalScrollDisplayed)
            {
                num = (ushort) (num | 0x10);
            }
            if (this.SheetTabsDisplayed)
            {
                num = (ushort) (num | 0x20);
            }
            if (this.NoAutoFilterDateGrouping)
            {
                num = (ushort) (num | 0x40);
            }
            writer.Write(num);
            writer.Write((short) this.selectedTabIndex);
            writer.Write((short) this.firstDisplayedTabIndex);
            writer.Write((short) this.selectedTabsCount);
            writer.Write((short) this.tabRatio);
        }

        public int HorizontalPosition
        {
            get => 
                this.horizontalPosition;
            set => 
                this.horizontalPosition = value;
        }

        public int VerticalPosition
        {
            get => 
                this.verticalPosition;
            set => 
                this.verticalPosition = value;
        }

        public int WidthInTwips
        {
            get => 
                this.widthInTwips;
            set => 
                this.widthInTwips = base.ValueInRange(value, 1, 0x7fff);
        }

        public int HeightInTwips
        {
            get => 
                this.heightInTwips;
            set => 
                this.heightInTwips = base.ValueInRange(value, 1, 0x7fff);
        }

        public bool IsHidden { get; set; }

        public bool IsMinimized { get; set; }

        public bool IsVeryHidden { get; set; }

        public bool HorizontalScrollDisplayed { get; set; }

        public bool VerticalScrollDisplayed { get; set; }

        public bool SheetTabsDisplayed { get; set; }

        public bool NoAutoFilterDateGrouping { get; set; }

        public int SelectedTabIndex
        {
            get => 
                this.selectedTabIndex;
            set
            {
                base.CheckValue(value, 0, 0xffff, "SelectedTabIndex");
                this.selectedTabIndex = value;
            }
        }

        public int FirstDisplayedTabIndex
        {
            get => 
                this.firstDisplayedTabIndex;
            set
            {
                base.CheckValue(value, 0, 0xffff, "FirstDisplayedTabIndex");
                this.firstDisplayedTabIndex = value;
            }
        }

        public int SelectedTabsCount
        {
            get => 
                this.selectedTabsCount;
            set
            {
                base.CheckValue(value, 0, 0xffff, "SelectedTabsCount");
                this.selectedTabsCount = value;
            }
        }

        public int TabRatio
        {
            get => 
                this.tabRatio;
            set
            {
                base.CheckValue(value, 0, 0x3e8, "TabRatio");
                this.tabRatio = value;
            }
        }
    }
}

