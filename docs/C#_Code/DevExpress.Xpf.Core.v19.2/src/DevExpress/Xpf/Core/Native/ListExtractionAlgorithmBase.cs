namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows.Data;

    public abstract class ListExtractionAlgorithmBase : IListExtractionAlgorithm
    {
        protected ListExtractionAlgorithmBase();
        protected static IList CreateEnumerableWrapper(IEnumerable enumerable);
        protected static IList CreateEnumerableWrapper(IEnumerable enumerable, Type genericType);
        protected static IList CreateGenericListWrapper(IEnumerable enumerable);
        public abstract IList Extract(object dataSource);
        protected static object ExtractDataSourceFromProvider(DataSourceProvider provider);
        protected static Type GetGenericType(IEnumerable enumerable);
        protected static bool IsCollectionOfDictionaries(IEnumerable enumerable);
        protected static BindingListAdapter WrapIEnumerable(IEnumerable enumerable, ItemPropertyNotificationMode itemPropertyNotificationMode = 1);
    }
}

