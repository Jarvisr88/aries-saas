namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal interface IEndUserFilteringMetadataProvider : IEnumerable<IEndUserFilteringMetric>, IEnumerable
    {
        void SetMetadataStorage(IMetadataStorage metadataStorage);
    }
}

