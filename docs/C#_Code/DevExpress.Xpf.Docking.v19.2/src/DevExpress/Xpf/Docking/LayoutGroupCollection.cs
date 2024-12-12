namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Collections.ObjectModel;

    public class LayoutGroupCollection : ObservableCollection<LayoutGroup>
    {
        public void AddOnce(LayoutGroup item)
        {
            if (!base.Contains(item))
            {
                base.Add(item);
            }
        }
    }
}

