namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchBarSubItemLink : BarSubItemLink
    {
        static SearchBarSubItemLink()
        {
            BarItemLinkControlCreator.Default.RegisterObject(typeof(SearchBarSubItemLink), typeof(SearchBarSubItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (li => new SearchBarSubItemLinkControl()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarSubItemLink.<>c <>9 = new SearchBarSubItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__0_0(object li) => 
                new SearchBarSubItemLinkControl();
        }
    }
}

