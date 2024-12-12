namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Collections.Generic;

    public class ModelItemCollectionWrapperHelper
    {
        private readonly Dictionary<IModelItem, ModelItemCollectionWrapper> columnCollections = new Dictionary<IModelItem, ModelItemCollectionWrapper>();
        private readonly Dictionary<IModelItem, BandModelItemCollectionWrapper> bandCollections = new Dictionary<IModelItem, BandModelItemCollectionWrapper>();

        public BandModelItemCollectionWrapper GetBandCollection(IModelItem bandCollectionOwnerModel)
        {
            BandModelItemCollectionWrapper wrapper = null;
            if (!this.bandCollections.TryGetValue(bandCollectionOwnerModel, out wrapper))
            {
                wrapper = new BandModelItemCollectionWrapper(bandCollectionOwnerModel.Properties["Bands"].Collection);
                this.bandCollections.Add(bandCollectionOwnerModel, wrapper);
            }
            return wrapper;
        }

        public ModelItemCollectionWrapper GetColumnCollection(IModelItem columnCollectionOwnerModel)
        {
            ModelItemCollectionWrapper wrapper = null;
            if (!this.columnCollections.TryGetValue(columnCollectionOwnerModel, out wrapper))
            {
                wrapper = new ModelItemCollectionWrapper(columnCollectionOwnerModel.Properties["Columns"].Collection);
                this.columnCollections.Add(columnCollectionOwnerModel, wrapper);
            }
            return wrapper;
        }
    }
}

