namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlDocumentView
    {
        private int tabRatio = 60;
        private int windowWidth;
        private int windowHeight;
        private int firstVisibleTabIndex;
        private int selectedTabIndex;

        private int GetFirstVisibleSheetIndex(IList<IXlSheet> sheets)
        {
            for (int i = 0; i < sheets.Count; i++)
            {
                if (sheets[i].VisibleState == XlSheetVisibleState.Visible)
                {
                    return i;
                }
            }
            return -1;
        }

        internal bool HasWindowBounds() => 
            (this.WindowX != 0) || ((this.WindowY != 0) || ((this.WindowWidth != 0) || (this.WindowHeight != 0)));

        internal bool IsDefault() => 
            this.GroupDatesInAutoFilterMenu && (this.ShowHorizontalScrollBar && (this.ShowVerticalScrollBar && (this.ShowSheetTabs && ((this.WindowX == 0) && ((this.WindowY == 0) && ((this.WindowWidth == 0) && ((this.WindowHeight == 0) && ((this.TabRatio == 60) && ((this.FirstVisibleTabIndex == 0) && (this.SelectedTabIndex == 0))))))))));

        internal void Validate(IList<IXlSheet> sheets)
        {
            int num = Math.Max(0, this.GetFirstVisibleSheetIndex(sheets));
            if ((this.SelectedTabIndex >= sheets.Count) || (sheets[this.SelectedTabIndex].VisibleState != XlSheetVisibleState.Visible))
            {
                this.SelectedTabIndex = num;
            }
            if ((this.FirstVisibleTabIndex >= sheets.Count) || (sheets[this.FirstVisibleTabIndex].VisibleState != XlSheetVisibleState.Visible))
            {
                this.FirstVisibleTabIndex = num;
            }
        }

        public bool GroupDatesInAutoFilterMenu { get; set; }

        public bool ShowHorizontalScrollBar { get; set; }

        public bool ShowVerticalScrollBar { get; set; }

        public bool ShowSheetTabs { get; set; }

        public int WindowX { get; set; }

        public int WindowY { get; set; }

        public int WindowWidth
        {
            get => 
                this.windowWidth;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("WindowWidth less than 0");
                }
                this.windowWidth = value;
            }
        }

        public int WindowHeight
        {
            get => 
                this.windowHeight;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("WindowHeight less than 0");
                }
                this.windowHeight = value;
            }
        }

        public int TabRatio
        {
            get => 
                this.tabRatio;
            set
            {
                if ((value < 0) || (value > 100))
                {
                    throw new ArgumentOutOfRangeException("TabRatio out of range 0...100");
                }
                this.tabRatio = value;
            }
        }

        public int FirstVisibleTabIndex
        {
            get => 
                this.firstVisibleTabIndex;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("FirstVisibleTabIndex less than 0");
                }
                this.firstVisibleTabIndex = value;
            }
        }

        public int SelectedTabIndex
        {
            get => 
                this.selectedTabIndex;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("SelectedTabIndex less than 0");
                }
                this.selectedTabIndex = value;
            }
        }
    }
}

