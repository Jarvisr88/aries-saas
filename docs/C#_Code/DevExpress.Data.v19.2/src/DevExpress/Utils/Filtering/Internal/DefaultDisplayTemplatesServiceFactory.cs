namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultDisplayTemplatesServiceFactory : IDisplayTemplatesServiceFactory
    {
        internal static readonly IDisplayTemplatesServiceFactory Instance = new DefaultDisplayTemplatesServiceFactory();

        private DefaultDisplayTemplatesServiceFactory()
        {
        }

        IDisplayTemplatesService IDisplayTemplatesServiceFactory.Create(string path) => 
            DefaultDisplayTemplatesService.Instance;

        private sealed class DefaultDisplayTemplatesService : IDisplayTemplatesService
        {
            internal static readonly IDisplayTemplatesService Instance = new DefaultDisplayTemplatesServiceFactory.DefaultDisplayTemplatesService();

            private DefaultDisplayTemplatesService()
            {
            }

            object IDisplayTemplatesService.GetCustomUIFiltersTemplateSelectorContainerProvider() => 
                null;

            object IDisplayTemplatesService.GetCustomUIFilterTemplateProvider() => 
                null;

            object IDisplayTemplatesService.GetTemplateProvider() => 
                null;
        }
    }
}

