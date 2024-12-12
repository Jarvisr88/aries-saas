namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public abstract class BaseLayoutContainer : BaseLayoutElement, ILayoutContainer, ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>
    {
        private LayoutElementCollection itemsCore;

        protected BaseLayoutContainer()
        {
        }

        internal void AddInternal(ILayoutElement element)
        {
            base.BeginUpdate();
            this.Items.Add(element);
            base.CancelUpdate();
        }

        protected virtual LayoutElementCollection CreateItems() => 
            new LayoutElementCollection(this);

        protected override ILayoutElement[] GetNodesCore() => 
            this.Items.ToArray();

        protected override void OnCreate()
        {
            base.OnCreate();
            this.itemsCore = this.CreateItems();
        }

        protected override void OnDispose()
        {
            ILayoutElement[] elementArray = this.itemsCore.ToArray();
            Ref.Dispose<LayoutElementCollection>(ref this.itemsCore);
            for (int i = 0; i < elementArray.Length; i++)
            {
                ILayoutElement refToDispose = elementArray[i];
                Ref.Dispose<ILayoutElement>(ref refToDispose);
            }
            base.OnDispose();
        }

        public LayoutElementCollection Items =>
            this.itemsCore;
    }
}

