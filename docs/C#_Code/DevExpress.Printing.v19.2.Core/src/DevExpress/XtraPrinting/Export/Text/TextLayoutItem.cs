namespace DevExpress.XtraPrinting.Export.Text
{
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Collections;

    public class TextLayoutItem
    {
        private string text;
        private TextBrickViewData owner;

        public TextLayoutItem(string text, TextBrickViewData owner)
        {
            this.text = text;
            this.owner = owner;
        }

        public string Text =>
            this.text;

        public TextBrickViewData Owner =>
            this.owner;

        public class XComparer : IComparer
        {
            public static readonly TextLayoutItem.XComparer Instance = new TextLayoutItem.XComparer();

            private XComparer()
            {
            }

            public int Compare(object x, object y)
            {
                TextLayoutItem item = (TextLayoutItem) x;
                TextLayoutItem item2 = (TextLayoutItem) y;
                return TextBrickViewData.XComparer.Instance.Compare(item.Owner, item2.Owner);
            }
        }

        public class YComparer : IComparer
        {
            public static readonly TextLayoutItem.YComparer Instance = new TextLayoutItem.YComparer();

            private YComparer()
            {
            }

            public int Compare(object x, object y)
            {
                TextLayoutItem item = (TextLayoutItem) x;
                TextLayoutItem item2 = (TextLayoutItem) y;
                return TextBrickViewData.YComparer.Instance.Compare(item.Owner, item2.Owner);
            }
        }
    }
}

