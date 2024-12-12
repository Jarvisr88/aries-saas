namespace DevExpress.Xpf.Layout.Core.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using DevExpress.Xpf.Layout.Core.Base;
    using System;

    public class ViewCollection : BaseChangeableList<IView>
    {
        private IViewAdapter ownerCore;

        public ViewCollection(IViewAdapter owner)
        {
            this.ownerCore = owner;
        }

        protected override void OnDispose()
        {
            this.ownerCore = null;
            foreach (IView view in base.ToArray())
            {
                view.Disposed -= new EventHandler(this.OnLayoutItemDisposed);
                Ref.Dispose<IView>(ref view);
            }
            base.OnDispose();
        }

        protected override void OnElementAdded(IView element)
        {
            base.OnElementAdded(element);
            AffinityHelper.SetAffinity(this.ownerCore, element);
            element.Disposed += new EventHandler(this.OnLayoutItemDisposed);
        }

        protected override void OnElementRemoved(IView element)
        {
            element.Disposed -= new EventHandler(this.OnLayoutItemDisposed);
            AffinityHelper.SetAffinity(null, element);
            base.OnElementRemoved(element);
        }

        private void OnLayoutItemDisposed(object sender, EventArgs ea)
        {
            this.RaiseCollectionChanged(new CollectionChangedEventArgs<IView>(sender as IView, CollectionChangedType.ElementDisposed));
            if (base.List != null)
            {
                base.Remove(sender as IView);
            }
        }

        private static class AffinityHelper
        {
            public static void SetAffinity(IViewAdapter adapter, IView element)
            {
                if (AffinityHelperException.Assert(element))
                {
                    ((BaseView) element).adapterCore = adapter;
                }
            }
        }
    }
}

