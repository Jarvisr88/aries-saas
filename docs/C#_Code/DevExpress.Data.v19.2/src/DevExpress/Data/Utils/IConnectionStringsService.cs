namespace DevExpress.Data.Utils
{
    using System;

    public interface IConnectionStringsService
    {
        void PatchConnection();
        void RestoreConnection();
    }
}

