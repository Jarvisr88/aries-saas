namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IEndUserFilteringSettingsFactory
    {
        IEndUserFilteringSettings Create(Type type, IEnumerable<IEndUserFilteringMetricAttributes> customAttributes = null);
    }
}

