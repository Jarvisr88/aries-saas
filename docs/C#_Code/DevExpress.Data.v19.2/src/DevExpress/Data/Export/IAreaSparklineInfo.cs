namespace DevExpress.Data.Export
{
    using System;

    public interface IAreaSparklineInfo : ILineSparklineAdditionalInfo, ISparklineInfo
    {
        byte AreaOpacity { get; }
    }
}

