namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IModelItemDictionary
    {
        void Add(object key, object value);

        IEnumerable<IModelItem> Keys { get; }

        IEnumerable<IModelItem> Values { get; }

        IModelItem this[IModelItem key] { get; set; }

        IModelItem this[object key] { get; set; }
    }
}

