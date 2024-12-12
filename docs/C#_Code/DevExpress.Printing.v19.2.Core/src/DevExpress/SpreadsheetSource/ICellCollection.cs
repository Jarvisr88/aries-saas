namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ICellCollection : IEnumerable<ICell>, IEnumerable, ICollection
    {
        ICell this[int index] { get; }
    }
}

