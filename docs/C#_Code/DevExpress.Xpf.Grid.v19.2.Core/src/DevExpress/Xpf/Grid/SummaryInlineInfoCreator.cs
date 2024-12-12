namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class SummaryInlineInfoCreator
    {
        public Func<GridSummaryData, string> GetItemDisplayText;
        public Func<SummaryItemBase, Style> GetSummaryStyle;

        private bool CalcHasStyle(IList<InlineInfo> inlineSource)
        {
            Func<InlineInfo, bool> predicate = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<InlineInfo, bool> local1 = <>c.<>9__12_0;
                predicate = <>c.<>9__12_0 = x => x.Style.ReturnSuccess<Style>();
            }
            return inlineSource.Any<InlineInfo>(predicate);
        }

        public InlineCollectionInfo Create(IEnumerable<GridSummaryData> data)
        {
            if (data == null)
            {
                return null;
            }
            IList<InlineInfo> inlineSource = new List<InlineInfo>();
            using (IEnumerator<GridSummaryData> enumerator = data.GetEnumerator())
            {
                for (bool flag = false; enumerator.MoveNext(); flag = true)
                {
                    if (flag)
                    {
                        inlineSource.Add(this.CreateSeparator());
                    }
                    inlineSource.Add(this.CreateItem(enumerator.Current));
                }
            }
            return new InlineCollectionInfo(this.CreateTextSource(inlineSource), inlineSource) { HasStyle = this.CalcHasStyle(inlineSource) };
        }

        public virtual InlineInfo CreateItem(GridSummaryData data)
        {
            string displayText = null;
            if (this.GetItemDisplayText != null)
            {
                displayText = this.GetItemDisplayText(data);
            }
            Style style = null;
            if ((this.GetSummaryStyle != null) && (data.Item != null))
            {
                style = this.GetSummaryStyle(data.Item);
            }
            InlineInfo info1 = new InlineInfo();
            info1.DisplayText = displayText;
            InlineInfo info2 = info1;
            Style defaultStyle = style;
            if (style == null)
            {
                Style local1 = style;
                defaultStyle = this.DefaultStyle;
            }
            info1.Style = defaultStyle;
            InlineInfo local2 = info1;
            local2.Data = new GridSummaryDisplayData(data, displayText);
            return local2;
        }

        public virtual InlineInfo CreateSeparator()
        {
            InlineInfo info1 = new InlineInfo();
            info1.DisplayText = this.SeparatorText;
            info1.Style = this.DefaultStyle;
            info1.Data = new GridSummaryDisplayData(null, null, null, this.SeparatorText);
            return info1;
        }

        private string CreateTextSource(IList<InlineInfo> inlineSource)
        {
            Func<InlineInfo, string> selector = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<InlineInfo, string> local1 = <>c.<>9__11_0;
                selector = <>c.<>9__11_0 = x => x.DisplayText;
            }
            return string.Join(null, inlineSource.Select<InlineInfo, string>(selector));
        }

        public Style DefaultStyle { get; set; }

        public string SeparatorText { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SummaryInlineInfoCreator.<>c <>9 = new SummaryInlineInfoCreator.<>c();
            public static Func<InlineInfo, string> <>9__11_0;
            public static Func<InlineInfo, bool> <>9__12_0;

            internal bool <CalcHasStyle>b__12_0(InlineInfo x) => 
                x.Style.ReturnSuccess<Style>();

            internal string <CreateTextSource>b__11_0(InlineInfo x) => 
                x.DisplayText;
        }
    }
}

