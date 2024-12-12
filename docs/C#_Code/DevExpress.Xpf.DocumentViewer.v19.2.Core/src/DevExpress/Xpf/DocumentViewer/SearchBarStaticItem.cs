namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SearchBarStaticItem : BarStaticItem
    {
        static SearchBarStaticItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(SearchBarStaticItem), typeof(SearchBarStaticItemLink), (CreateObjectMethod<BarItemLink>) (it => new SearchBarStaticItemLink()));
            BarStaticItem.ShowBorderProperty.OverrideMetadata(typeof(SearchBarStaticItem), new FrameworkPropertyMetadata(false));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarStaticItem.<>c <>9 = new SearchBarStaticItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object it) => 
                new SearchBarStaticItemLink();
        }
    }
}

