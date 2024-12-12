namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Diagnostics;

    public class BarCheckItemLinkControlHelperWorker
    {
        private readonly IBarCheckItemLinkControl instance;

        public BarCheckItemLinkControlHelperWorker(IBarCheckItemLinkControl instance);
        public virtual void UpdateCheckBorders();

        public IBarCheckItemLinkControl CheckInstance { [DebuggerStepThrough] get; }

        public BarCheckItemLink CheckLink { get; }

        private BarCheckItem CheckItem { get; }
    }
}

