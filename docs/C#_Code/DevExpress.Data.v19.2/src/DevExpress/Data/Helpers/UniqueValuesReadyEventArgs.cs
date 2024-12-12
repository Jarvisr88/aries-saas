namespace DevExpress.Data.Helpers
{
    using System;

    public class UniqueValuesReadyEventArgs : EventArgs
    {
        public readonly object[] UniqueValues;

        public UniqueValuesReadyEventArgs(object[] result);
    }
}

