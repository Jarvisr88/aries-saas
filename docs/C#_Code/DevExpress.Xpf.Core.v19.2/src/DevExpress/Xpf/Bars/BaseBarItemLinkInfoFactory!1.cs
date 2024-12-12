namespace DevExpress.Xpf.Bars
{
    using System;

    public abstract class BaseBarItemLinkInfoFactory<TElement>
    {
        protected BaseBarItemLinkInfoFactory();
        public abstract TElement CreateLinkInfo(BarItemLinkBase link);
    }
}

