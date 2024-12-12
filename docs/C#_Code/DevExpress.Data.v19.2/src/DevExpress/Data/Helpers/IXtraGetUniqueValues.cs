namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IXtraGetUniqueValues
    {
        event EventHandler<UniqueValuesReadyEventArgs> UniqueValuesReady;

        void CancelGetUniqueValues();
        void GetUniqueValues(GetUniqueValuesEventArgs e);

        bool UniqueValuesSupported { get; }
    }
}

