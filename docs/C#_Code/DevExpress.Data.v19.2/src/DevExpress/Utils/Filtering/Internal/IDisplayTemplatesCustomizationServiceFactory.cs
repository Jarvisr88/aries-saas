namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IDisplayTemplatesCustomizationServiceFactory
    {
        IDisplayTemplatesCustomizationService Create(string path);
    }
}

