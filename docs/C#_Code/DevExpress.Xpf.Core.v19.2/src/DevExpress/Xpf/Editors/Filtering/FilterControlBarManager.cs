namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using System;

    [DXToolboxBrowsable(false)]
    public class FilterControlBarManager : BarManager
    {
        public FilterControlBarManager()
        {
            base.CreateStandardLayout = false;
        }
    }
}

