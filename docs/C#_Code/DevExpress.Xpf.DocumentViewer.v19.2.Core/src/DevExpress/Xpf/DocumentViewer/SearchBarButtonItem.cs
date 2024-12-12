namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchBarButtonItem : BarButtonItem
    {
        static SearchBarButtonItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(SearchBarButtonItem), typeof(SearchBarButtonItemLink), (CreateObjectMethod<BarItemLink>) (it => new SearchBarButtonItemLink()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarButtonItem.<>c <>9 = new SearchBarButtonItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object it) => 
                new SearchBarButtonItemLink();
        }
    }
}

