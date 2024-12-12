namespace DMEWorks.Data.Common
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IValidationResult : IReadOnlyDictionary<string, IEnumerable<IError>>, IReadOnlyCollection<KeyValuePair<string, IEnumerable<IError>>>, IEnumerable<KeyValuePair<string, IEnumerable<IError>>>, IEnumerable
    {
    }
}

