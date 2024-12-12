namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    internal class CollectionViewSourceDesignDataManager : IDesignDataUpdater
    {
        private readonly DependencyPropertyChangedListener listener = new DependencyPropertyChangedListener();

        private void OnCollectionViewSourceChanged(object sender, ListenerPropertyChangedEventArgs e)
        {
            this.UpdateSubscriptions(e.Owner);
            if (DesignerProperties.GetIsInDesignMode(e.Owner))
            {
                this.UpdateCollectionViewDesignData((CollectionViewSource) e.Owner);
            }
        }

        private void UpdateCollectionViewDesignData(CollectionViewSource collectionView)
        {
            this.UpdateSubscriptions(collectionView);
            IDesignDataSettings designData = DesignDataManager.GetDesignData(collectionView);
            if ((designData != null) && DesignerProperties.GetIsInDesignMode(collectionView))
            {
                if (!this.listener.IsRegistered(collectionView))
                {
                    Binding binding = new Binding();
                    binding.Source = collectionView;
                    this.listener.Register(collectionView, binding);
                }
                Type dataObjectType = designData.DataObjectType ?? DataSourceHelper.ExtractEnumerableType(collectionView.Source as IEnumerable);
                if (dataObjectType != null)
                {
                    ISupportInitialize initialize = collectionView;
                    initialize.BeginInit();
                    collectionView.Source = new CollectionViewDesignTimeDataSource(dataObjectType, designData.RowCount, designData.UseDistinctValues, designData.FlattenHierarchy);
                    initialize.EndInit();
                }
            }
        }

        public void UpdateDesignData(DependencyObject element)
        {
            CollectionViewSource collectionView = element as CollectionViewSource;
            if (collectionView != null)
            {
                this.UpdateCollectionViewDesignData(collectionView);
            }
        }

        private void UpdateSubscriptions(DependencyObject d)
        {
            this.listener.DependencyPropertyChanged -= new EventHandler<ListenerPropertyChangedEventArgs>(this.OnCollectionViewSourceChanged);
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                this.listener.UnregisterAll();
            }
            else
            {
                this.listener.DependencyPropertyChanged += new EventHandler<ListenerPropertyChangedEventArgs>(this.OnCollectionViewSourceChanged);
            }
        }
    }
}

