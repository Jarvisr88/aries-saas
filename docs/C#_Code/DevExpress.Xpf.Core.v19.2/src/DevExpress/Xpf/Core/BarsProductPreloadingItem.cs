namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.Generic;

    public class BarsProductPreloadingItem : IProductPreloadingItem
    {
        public string AssemblyFullName =>
            "DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a";

        public IEnumerable<FrameworkElement> Controls
        {
            get
            {
                List<FrameworkElement> list1 = new List<FrameworkElement>();
                list1.Add(new ToolBarControl());
                return list1;
            }
        }
    }
}

