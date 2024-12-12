namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchBarStaticItemLink : BarStaticItemLink
    {
        static SearchBarStaticItemLink()
        {
            BarItemLinkControlCreator.Default.RegisterObject(typeof(SearchBarStaticItemLink), typeof(SearchBarStaticItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (li => new SearchBarStaticItemLinkControl()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarStaticItemLink.<>c <>9 = new SearchBarStaticItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__0_0(object li) => 
                new SearchBarStaticItemLinkControl();
        }
    }
}

