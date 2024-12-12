namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ITablesCollection : IEnumerable<ITable>, IEnumerable, ICollection
    {
        IList<ITable> GetTables(string sheetName);

        ITable this[int index] { get; }

        ITable this[string name] { get; }
    }
}

