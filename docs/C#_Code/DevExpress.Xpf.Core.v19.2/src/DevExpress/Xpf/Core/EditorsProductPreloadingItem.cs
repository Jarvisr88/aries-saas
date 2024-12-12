namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;

    public class EditorsProductPreloadingItem : IProductPreloadingItem
    {
        public string AssemblyFullName =>
            "DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a";

        public IEnumerable<FrameworkElement> Controls
        {
            get
            {
                List<FrameworkElement> list1 = new List<FrameworkElement>();
                list1.Add(new ComboBoxEdit());
                return list1;
            }
        }
    }
}

