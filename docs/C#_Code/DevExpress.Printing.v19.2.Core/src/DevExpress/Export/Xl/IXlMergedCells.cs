namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IXlMergedCells : IEnumerable<XlCellRange>, IEnumerable, ICollection
    {
        void Add(XlCellRange range);
        void Add(XlCellRange range, bool checkOverlap);
        void Clear();
        bool Contains(XlCellRange range);
        int IndexOf(XlCellRange range);
        void Remove(XlCellRange range);
        void RemoveAt(int index);

        XlCellRange this[int index] { get; }
    }
}

