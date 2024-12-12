namespace DevExpress.Xpf.Docking
{
    using System;

    internal class LayoutAdapter : ILayoutAdapter
    {
        private static ILayoutAdapter _Instance;

        string ILayoutAdapter.Resolve(DockLayoutManager owner, object item) => 
            MVVMHelper.GetTargetNameForItem(item);

        public static ILayoutAdapter Instance
        {
            get
            {
                _Instance ??= new LayoutAdapter();
                return _Instance;
            }
        }
    }
}

