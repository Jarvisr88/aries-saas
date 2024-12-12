namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IXtraMoreRows
    {
        event EventHandler RowsLoaded;

        void MoreRows();

        bool IsMoreRowsSupported { get; }

        bool IsMoreRowsAvailable { get; }
    }
}

