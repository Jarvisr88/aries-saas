namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;

    public interface IBestFitColumn
    {
        DevExpress.Xpf.Core.BestFitMode BestFitMode { get; }

        int BestFitMaxRowCount { get; }
    }
}

