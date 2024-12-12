namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    [DXToolboxBrowsable(false)]
    public class BarButtonColorEditItemLink : BarSplitButtonItemLink
    {
        static BarButtonColorEditItemLink()
        {
            BarItemLinkControlCreator.Default.RegisterObject(typeof(BarButtonColorEditItemLink), typeof(BarButtonColorEditItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (arg => new BarButtonColorEditItemLinkControl((BarButtonColorEditItemLink) arg)));
        }

        protected internal override void OnClick(IBarItemLinkControl linkControl)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarButtonColorEditItemLink.<>c <>9 = new BarButtonColorEditItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__1_0(object arg) => 
                new BarButtonColorEditItemLinkControl((BarButtonColorEditItemLink) arg);
        }
    }
}

