namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface ICustomUIFiltersProperties
    {
        void Assign(ICustomUIFiltersProperties properties);

        CustomUIFiltersType? FiltersType { get; }
    }
}

