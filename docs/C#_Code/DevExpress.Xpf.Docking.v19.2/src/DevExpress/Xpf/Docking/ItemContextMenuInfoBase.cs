namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ItemContextMenuInfoBase : BaseLayoutElementMenuInfo
    {
        public ItemContextMenuInfoBase(BaseLayoutElementMenu menu, BaseLayoutItem item) : base(menu)
        {
            this.Item = item;
        }

        public override void Uninitialize()
        {
            base.Uninitialize();
            this.Item = null;
        }

        public BaseLayoutItem Item { get; private set; }
    }
}

