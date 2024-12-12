namespace DevExpress.SpreadsheetSource
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IDefinedNamesCollection : IEnumerable<IDefinedName>, IEnumerable, ICollection
    {
        IDefinedName FindBy(string name, string scope);

        IDefinedName this[int index] { get; }
    }
}

