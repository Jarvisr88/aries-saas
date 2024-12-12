namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    public interface IDisplayTextServiceFactory
    {
        IDisplayTextService Create(string path);
        bool TryCreate(string path, out IDisplayTextService service);
    }
}

