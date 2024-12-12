namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IDisplayTemplatesCustomizationService
    {
        void OnApplyTemplate(object template);
        object PrepareTemplate(object template);
    }
}

