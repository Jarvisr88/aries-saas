namespace DevExpress.Data.Helpers
{
    using System;

    public interface IXtraRefreshable
    {
        void Refresh();

        bool RefreshSupported { get; }
    }
}

