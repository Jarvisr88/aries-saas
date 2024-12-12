namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IDisplayTemplatesService
    {
        object GetCustomUIFiltersTemplateSelectorContainerProvider();
        object GetCustomUIFilterTemplateProvider();
        object GetTemplateProvider();
    }
}

