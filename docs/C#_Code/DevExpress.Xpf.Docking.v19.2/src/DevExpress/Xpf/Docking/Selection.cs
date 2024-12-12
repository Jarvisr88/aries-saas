namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core.Base;
    using System;

    public class Selection : BaseReadOnlyList<BaseLayoutItem>
    {
        protected internal void ProcessSelectionChanged(BaseLayoutItem item, bool selected)
        {
            if (!selected)
            {
                base.List.Remove(item);
            }
            else if (!base.List.Contains(item))
            {
                base.List.Add(item);
            }
        }

        public BaseLayoutItem[] ToArray()
        {
            BaseLayoutItem[] array = new BaseLayoutItem[base.List.Count];
            base.List.CopyTo(array, 0);
            return array;
        }
    }
}

