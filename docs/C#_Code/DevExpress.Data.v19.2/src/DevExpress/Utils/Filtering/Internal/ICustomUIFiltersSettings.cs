namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface ICustomUIFiltersSettings : IEnumerable<ICustomUIFiltersBox>, IEnumerable
    {
        void EnsureFiltersType(string path);
        bool HasFilters(string path);

        ICustomUIFilters this[string path] { get; }
    }
}

