namespace DevExpress.Xpf.Layout.Core
{
    using DevExpress.Xpf.Layout.Core.Base;
    using System;

    public class LayoutElementCollection : BaseChangeableList<ILayoutElement>
    {
        private ILayoutContainer ownerCore;

        public LayoutElementCollection(ILayoutContainer owner)
        {
            this.ownerCore = owner;
        }

        protected override void OnElementAdded(ILayoutElement element)
        {
            base.OnElementAdded(element);
            AffinityHelper.SetAffinity(this.ownerCore, element);
            element.Disposed += new EventHandler(this.OnLayoutItemDisposed);
        }

        protected override void OnElementRemoved(ILayoutElement element)
        {
            element.Disposed -= new EventHandler(this.OnLayoutItemDisposed);
            AffinityHelper.SetAffinity(null, element);
            base.OnElementRemoved(element);
        }

        private void OnLayoutItemDisposed(object sender, EventArgs ea)
        {
            this.RaiseCollectionChanged(new CollectionChangedEventArgs<ILayoutElement>(sender as ILayoutElement, CollectionChangedType.ElementDisposed));
            if (base.List != null)
            {
                base.Remove(sender as ILayoutElement);
            }
        }

        private static class AffinityHelper
        {
            public static void SetAffinity(ILayoutContainer parent, ILayoutElement element)
            {
                if (AffinityHelperException.Assert(element))
                {
                    ((BaseLayoutElement) element).parentCore = parent;
                }
            }
        }
    }
}

