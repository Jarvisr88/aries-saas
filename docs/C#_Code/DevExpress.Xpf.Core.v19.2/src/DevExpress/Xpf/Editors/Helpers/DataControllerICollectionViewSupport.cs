namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataControllerICollectionViewSupport
    {
        public DataControllerICollectionViewSupport(IItemsProviderCollectionViewSupport itemsProvider)
        {
            this.ItemsProvider = itemsProvider;
            Action<DataControllerICollectionViewSupport, object, EventArgs> onEventAction = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Action<DataControllerICollectionViewSupport, object, EventArgs> local1 = <>c.<>9__15_0;
                onEventAction = <>c.<>9__15_0 = (owner, o, e) => owner.RaiseCurrentChanged();
            }
            this.CollectionViewMoveCurrentTo = new CollectionViewMoveCurrentEventHandler<DataControllerICollectionViewSupport>(this, onEventAction);
        }

        public virtual void Initialize()
        {
            if (this.HasCollectionView)
            {
                this.CollectionView.CurrentChanged += this.CollectionViewMoveCurrentTo.Handler;
            }
        }

        protected virtual void RaiseCurrentChanged()
        {
            if (this.HasCollectionView && this.ItemsProvider.IsSynchronizedWithCurrentItem)
            {
                this.ItemsProvider.RaiseCurrentChanged(this.CurrentItem);
            }
        }

        public virtual void Release()
        {
            if (this.HasCollectionView)
            {
                this.CollectionView.CurrentChanged -= this.CollectionViewMoveCurrentTo.Handler;
            }
        }

        public virtual void SetCurrentItem(object currentItem)
        {
            this.CurrentItem = currentItem;
        }

        public void SyncWithCurrent()
        {
            this.RaiseCurrentChanged();
        }

        public virtual void SyncWithData(IDataControllerVisualClient visual)
        {
            if (this.HasCollectionView && (this.ItemsProvider.DataSync != null))
            {
                visual.RequireSynchronization(this.ItemsProvider.DataSync);
            }
        }

        private CollectionViewMoveCurrentEventHandler<DataControllerICollectionViewSupport> CollectionViewMoveCurrentTo { get; set; }

        public ICollectionView CollectionView =>
            this.ItemsProvider.ListSource;

        public bool HasCollectionView =>
            this.CollectionView != null;

        private IItemsProviderCollectionViewSupport ItemsProvider { get; set; }

        private object CurrentItem
        {
            get => 
                (this.ItemsProvider.ListSource != null) ? this.CollectionView.CurrentItem : null;
            set
            {
                ICollectionView listSource = this.ItemsProvider.ListSource;
                if ((listSource != null) && !Equals(listSource.CurrentItem, value))
                {
                    listSource.MoveCurrentTo(value);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControllerICollectionViewSupport.<>c <>9 = new DataControllerICollectionViewSupport.<>c();
            public static Action<DataControllerICollectionViewSupport, object, EventArgs> <>9__15_0;

            internal void <.ctor>b__15_0(DataControllerICollectionViewSupport owner, object o, EventArgs e)
            {
                owner.RaiseCurrentChanged();
            }
        }
    }
}

