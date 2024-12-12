namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IXlTableColumnCollection : IXlReadonlyCollection<IXlTableColumn>, IEnumerable<IXlTableColumn>, IEnumerable
    {
        int IndexOf(string name);
    }
}

