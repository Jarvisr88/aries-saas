namespace DevExpress.Xpf.Ribbon
{
    using DevExpress.Xpf.Bars;
    using System;

    public interface IRibbonStatusBarControl
    {
        MDIMergeStyle GetMDIMergeStyle();
        void Merge(object child);
        void UnMerge(object child);

        bool IsMerged { get; }

        bool IsChild { get; }
    }
}

