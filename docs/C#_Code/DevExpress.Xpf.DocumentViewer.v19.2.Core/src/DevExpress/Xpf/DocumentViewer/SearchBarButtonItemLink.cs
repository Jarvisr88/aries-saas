namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;

    public class SearchBarButtonItemLink : BarButtonItemLink
    {
        static SearchBarButtonItemLink()
        {
            BarItemLinkControlCreator.Default.RegisterObject(typeof(SearchBarButtonItemLink), typeof(SearchBarButtonItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (li => new SearchBarButtonItemLinkControl()));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SearchBarButtonItemLink.<>c <>9 = new SearchBarButtonItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__0_0(object li) => 
                new SearchBarButtonItemLinkControl();
        }
    }
}

