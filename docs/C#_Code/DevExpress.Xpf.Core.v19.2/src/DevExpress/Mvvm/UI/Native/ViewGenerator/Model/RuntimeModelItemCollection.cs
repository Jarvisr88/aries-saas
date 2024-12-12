namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections;

    public class RuntimeModelItemCollection : ModelItemCollectionBase
    {
        public RuntimeModelItemCollection(EditingContextBase context, IEnumerable computedValue, IModelItem parent) : base(context, computedValue, parent)
        {
        }
    }
}

