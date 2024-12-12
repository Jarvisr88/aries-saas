namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class LayoutItemInsertionPoints : List<LayoutItemInsertionPoint>
    {
        public LayoutItemInsertionPoint Find(FrameworkElement element)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (ReferenceEquals(base[i].Element, element))
                {
                    return base[i];
                }
            }
            return null;
        }
    }
}

