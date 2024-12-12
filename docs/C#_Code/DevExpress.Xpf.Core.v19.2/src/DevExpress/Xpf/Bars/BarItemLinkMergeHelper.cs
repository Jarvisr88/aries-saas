namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;

    public class BarItemLinkMergeHelper
    {
        private BarItemLink FindLinkByContent(object content, List<BarItemLinkMergeInfo> res);
        private object GetName(BarItemLink dObj);
        public void Merge(ILinksHolder coll, List<BarItemLinkMergeInfo> res);
        public void Merge(BarItemLinkCollection mainColl, ObservableCollection<ILinksHolder> collArray, BarItemLinkCollection resultColl);
        public void MergeLink_AddType(BarItemLinkBase link, List<BarItemLinkMergeInfo> res);
        public void MergeLink_MergeItemsType(BarItemLinkBase link, List<BarItemLinkMergeInfo> res);
        public void MergeLink_ReplaceType(BarItemLinkBase link, List<BarItemLinkMergeInfo> res);
        private void PerformMerge(List<BarItemLinkMergeInfo> res, BarItemLinkBase link);
        public void UnMerge(BarItemLinkCollection coll);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkMergeHelper.<>c <>9;
            public static Func<string, string> <>9__6_0;
            public static Func<string, string> <>9__6_1;
            public static Func<string, string> <>9__6_2;

            static <>c();
            internal string <GetName>b__6_0(string x);
            internal string <GetName>b__6_1(string x);
            internal string <GetName>b__6_2(string x);
        }

        private class MergeLinksComparer : IComparer<BarItemLinkMergeInfo>
        {
            int IComparer<BarItemLinkMergeInfo>.Compare(BarItemLinkMergeInfo link1, BarItemLinkMergeInfo link2);
        }
    }
}

