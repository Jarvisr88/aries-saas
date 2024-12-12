namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IWorksheetCollection : IEnumerable<IWorksheet>, IEnumerable, ICollection
    {
        IWorksheet this[int index] { get; }

        IWorksheet this[string name] { get; }
    }
}

