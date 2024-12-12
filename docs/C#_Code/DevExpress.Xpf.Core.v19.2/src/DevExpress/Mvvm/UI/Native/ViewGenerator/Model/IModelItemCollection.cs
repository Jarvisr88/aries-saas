namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IModelItemCollection : IEnumerable<IModelItem>, IEnumerable
    {
        void Add(IModelItem value);
        IModelItem Add(object value);
        void Clear();
        int IndexOf(IModelItem item);
        void Insert(int index, IModelItem valueItem);
        void Insert(int index, object value);
        bool Remove(IModelItem item);
        bool Remove(object item);
        void RemoveAt(int index);

        IModelItem this[int index] { get; set; }
    }
}

