namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IEndUserFilteringViewModelProperties : IEnumerable<KeyValuePair<string, Type>>, IEnumerable
    {
        IEndUserFilteringViewModelProperties GetNestedProperties(string rootPath);
    }
}

