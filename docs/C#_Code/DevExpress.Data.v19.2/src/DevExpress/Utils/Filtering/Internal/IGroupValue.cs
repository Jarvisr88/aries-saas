namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public interface IGroupValue : IGrouping<int, object>, IEnumerable<object>, IEnumerable
    {
        object Value { get; }

        int Level { get; }
    }
}

