namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.LayoutControl;
    using System;

    public class TileLayoutControlWrapper : FlowLayoutControlWrapper, IItemsControlWrapper<TileLayoutControl>, ITargetWrapper<TileLayoutControl>
    {
        public TileLayoutControl Target
        {
            get => 
                (TileLayoutControl) base.Target;
            set => 
                base.Target = value;
        }
    }
}

