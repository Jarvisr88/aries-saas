namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal sealed class DefaultDisplayTemplatesCustomizationServiceFactory : IDisplayTemplatesCustomizationServiceFactory
    {
        internal static readonly IDisplayTemplatesCustomizationServiceFactory Instance = new DefaultDisplayTemplatesCustomizationServiceFactory();

        private DefaultDisplayTemplatesCustomizationServiceFactory()
        {
        }

        IDisplayTemplatesCustomizationService IDisplayTemplatesCustomizationServiceFactory.Create(string path) => 
            DefaultDisplayTemplatesCustomizationService.Instance;

        private sealed class DefaultDisplayTemplatesCustomizationService : IDisplayTemplatesCustomizationService
        {
            internal static readonly IDisplayTemplatesCustomizationService Instance = new DefaultDisplayTemplatesCustomizationServiceFactory.DefaultDisplayTemplatesCustomizationService();

            private DefaultDisplayTemplatesCustomizationService()
            {
            }

            void IDisplayTemplatesCustomizationService.OnApplyTemplate(object template)
            {
            }

            object IDisplayTemplatesCustomizationService.PrepareTemplate(object template) => 
                template;
        }
    }
}

