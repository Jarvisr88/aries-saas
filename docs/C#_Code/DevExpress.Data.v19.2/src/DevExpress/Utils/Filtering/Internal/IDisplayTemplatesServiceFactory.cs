namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IDisplayTemplatesServiceFactory
    {
        IDisplayTemplatesService Create(string path);
    }
}

