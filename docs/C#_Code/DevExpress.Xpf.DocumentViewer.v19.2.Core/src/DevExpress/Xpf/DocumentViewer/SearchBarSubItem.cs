namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchBarSubItem : BarSubItem
    {
        static SearchBarSubItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(SearchBarSubItem), typeof(SearchBarSubItemLink), (CreateObjectMethod<BarItemLink>) (it => new SearchBarSubItemLink()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarSubItem.<>c <>9 = new SearchBarSubItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object it) => 
                new SearchBarSubItemLink();
        }
    }
}

