namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export.Text;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Drawing;

    public class TextBrickViewData : BrickViewData
    {
        private ITextLayoutTable texts;

        public TextBrickViewData(BrickStyle style, RectangleF bounds, ITableCell tableCell) : base(style, bounds, tableCell)
        {
        }

        public TextLayoutItem GetLayoutItem(int row) => 
            new TextLayoutItem(this.Texts[row], this);

        public ITextLayoutTable Texts
        {
            get => 
                this.texts;
            set => 
                this.texts = value;
        }

        public bool IsCompoundControl =>
            (this.Texts != null) && (this.Texts.Count > 1);

        public class XComparer : IComparer
        {
            public static readonly TextBrickViewData.XComparer Instance = new TextBrickViewData.XComparer();

            private XComparer()
            {
            }

            public int Compare(object x, object y)
            {
                TextBrickViewData data2 = (TextBrickViewData) y;
                return Math.Sign((int) (((TextBrickViewData) x).Bounds.X - data2.Bounds.X));
            }
        }

        public class YComparer : IComparer
        {
            public static readonly TextBrickViewData.YComparer Instance = new TextBrickViewData.YComparer();

            private YComparer()
            {
            }

            public int Compare(object x, object y)
            {
                TextBrickViewData data2 = (TextBrickViewData) y;
                return Math.Sign((int) (((TextBrickViewData) x).Bounds.Y - data2.Bounds.Y));
            }
        }
    }
}

