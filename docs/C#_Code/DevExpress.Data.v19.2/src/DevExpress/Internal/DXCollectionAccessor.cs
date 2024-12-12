namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public static class DXCollectionAccessor
    {
        public static IList<T> GetInnerList<T>(DXCollectionBase<T> collection) => 
            collection.GetInnerList();

        public static T GetItem<T>(DXCollectionBase<T> collection, int index) => 
            collection.InvokeGetItem(index);
    }
}

