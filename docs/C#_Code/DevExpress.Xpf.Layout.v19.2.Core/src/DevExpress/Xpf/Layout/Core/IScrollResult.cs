namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface IScrollResult
    {
        int Index { get; }

        int MaxIndex { get; }

        bool CanScrollPrev { get; }

        bool CanScrollNext { get; }

        double ScrollOffset { get; }
    }
}

