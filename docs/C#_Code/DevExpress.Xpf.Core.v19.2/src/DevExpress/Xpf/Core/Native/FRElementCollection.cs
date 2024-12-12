namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;

    public class FRElementCollection : ObservableCollection<FrameworkRenderElement>
    {
        private readonly FrameworkRenderElement parent;

        public FRElementCollection(FrameworkRenderElement parent);
        protected override void ClearItems();
        protected override void InsertItem(int index, FrameworkRenderElement item);
        protected override void RemoveItem(int index);
        protected override void SetItem(int index, FrameworkRenderElement item);
    }
}

