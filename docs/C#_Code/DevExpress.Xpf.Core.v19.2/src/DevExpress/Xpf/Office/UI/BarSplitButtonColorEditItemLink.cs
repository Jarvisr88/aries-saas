namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    [DXToolboxBrowsable(false)]
    public class BarSplitButtonColorEditItemLink : BarSplitButtonItemLink
    {
        static BarSplitButtonColorEditItemLink()
        {
            BarItemLinkControlCreator.Default.RegisterObject(typeof(BarSplitButtonColorEditItemLink), typeof(BarSplitButtonColorEditItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (arg => new BarSplitButtonColorEditItemLinkControl((BarSplitButtonColorEditItemLink) arg)));
        }

        protected internal virtual void UpdateLinkControl()
        {
            BarItemLinkInfoReferenceCollection linkInfos = base.LinkInfos;
            int count = linkInfos.Count;
            for (int i = 0; i < count; i++)
            {
                BarSplitButtonColorEditItemLinkControl linkControl = linkInfos[i].LinkControl as BarSplitButtonColorEditItemLinkControl;
                if (linkControl != null)
                {
                    linkControl.UpdateColorIndicator();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarSplitButtonColorEditItemLink.<>c <>9 = new BarSplitButtonColorEditItemLink.<>c();

            internal BarItemLinkControlBase <.cctor>b__1_0(object arg) => 
                new BarSplitButtonColorEditItemLinkControl((BarSplitButtonColorEditItemLink) arg);
        }
    }
}

