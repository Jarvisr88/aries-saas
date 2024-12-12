namespace DevExpress.Data.Export
{
    using System;

    public interface ILineSparklineAdditionalInfo : ISparklineInfo
    {
        int MarkerSize { get; }

        int MinPointMarkerSize { get; }

        int EndPointMarkerSize { get; }

        int MaxPointMarkerSize { get; }

        int NegativePointMarkerSize { get; }

        int StartPointMarkerSize { get; }

        bool EnableAntialiasing { get; }
    }
}

