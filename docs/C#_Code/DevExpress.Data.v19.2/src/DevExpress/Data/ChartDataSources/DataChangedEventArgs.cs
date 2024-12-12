namespace DevExpress.Data.ChartDataSources
{
    using System;

    public class DataChangedEventArgs : EventArgs
    {
        private readonly DevExpress.Data.ChartDataSources.DataChangedType dataChangedType;

        public DataChangedEventArgs(DevExpress.Data.ChartDataSources.DataChangedType dataChangedType);

        public DevExpress.Data.ChartDataSources.DataChangedType DataChangedType { get; }
    }
}

