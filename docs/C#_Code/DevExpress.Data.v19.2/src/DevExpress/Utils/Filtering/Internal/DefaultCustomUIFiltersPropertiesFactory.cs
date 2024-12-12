namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class DefaultCustomUIFiltersPropertiesFactory : ICustomUIFiltersPropertiesFactory
    {
        public static readonly ICustomUIFiltersPropertiesFactory Instance = new DefaultCustomUIFiltersPropertiesFactory();

        private DefaultCustomUIFiltersPropertiesFactory()
        {
        }

        public ICustomUIFiltersProperties Create(ICustomUIFilters filters) => 
            new CustomUIFiltersProperties(filters);

        private sealed class CustomUIFiltersProperties : ICustomUIFiltersProperties
        {
            internal CustomUIFiltersProperties(ICustomUIFilters filters)
            {
                this.FiltersType = new CustomUIFiltersType?(filters.GetID());
            }

            void ICustomUIFiltersProperties.Assign(ICustomUIFiltersProperties properties)
            {
                this.FiltersType = properties.FiltersType;
            }

            public CustomUIFiltersType? FiltersType { get; private set; }
        }
    }
}

