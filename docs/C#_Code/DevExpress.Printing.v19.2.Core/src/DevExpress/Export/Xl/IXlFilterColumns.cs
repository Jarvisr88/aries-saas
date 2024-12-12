namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IXlFilterColumns : IEnumerable<XlFilterColumn>, IEnumerable, ICollection
    {
        void Add(XlFilterColumn column);
        void Clear();
        bool Contains(XlFilterColumn column);
        XlFilterColumn FindById(int columnId);
        int IndexOf(XlFilterColumn column);
        void Remove(XlFilterColumn column);
        void RemoveAt(int index);

        XlFilterColumn this[int index] { get; }
    }
}

