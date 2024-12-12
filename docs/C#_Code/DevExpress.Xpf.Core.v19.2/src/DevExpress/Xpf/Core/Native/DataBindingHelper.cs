namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public static class DataBindingHelper
    {
        public static IList ExtractDataSource(object dataSource, IListExtractionAlgorithm algorithm);
        public static IList ExtractDataSource(object dataSource, bool allowLiveDataShaping = true, bool wrapDataView = false, bool useSlidingSubscription = false);
        public static IList ExtractDataSource(object dataSource, ItemPropertyNotificationMode itemPropertyNotificationMode, bool wrapDataView = false, bool wrapRealList = false, bool listenToComplexProperties = false, bool useSlidingSubscription = false);
        private static IList ExtractDataSourceCore(object dataSource, IListExtractionAlgorithm algorithm);
        public static IList ExtractDataSourceFromCollectionView(IEnumerable source, ItemPropertyNotificationMode itemPropertyNotificationMode = 1);
    }
}

