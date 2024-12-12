namespace DevExpress.Utils.Filtering.Internal
{
    using System.Collections.Generic;

    public interface ILookupValuesViewModel
    {
        IEnumerable<KeyValuePair<object, string>> LookupDataSource { get; }
    }
}

