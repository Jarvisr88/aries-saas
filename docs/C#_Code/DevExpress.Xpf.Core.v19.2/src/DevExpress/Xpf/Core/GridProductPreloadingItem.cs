namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class GridProductPreloadingItem : IProductPreloadingItem
    {
        public string AssemblyFullName =>
            "DevExpress.Xpf.Grid.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a";

        public IEnumerable<FrameworkElement> Controls
        {
            get
            {
                List<FrameworkElement> list1 = new List<FrameworkElement>();
                list1.Add((FrameworkElement) Activator.CreateInstance(this.AssemblyFullName, "DevExpress.Xpf.Grid.GridControl").Unwrap());
                return list1;
            }
        }
    }
}

