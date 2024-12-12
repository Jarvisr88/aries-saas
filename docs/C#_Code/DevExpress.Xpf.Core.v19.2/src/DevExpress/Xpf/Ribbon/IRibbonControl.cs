namespace DevExpress.Xpf.Ribbon
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IRibbonControl
    {
        MDIMergeStyle GetMDIMergeStyle();
        void Merge(object child, ILinksHolder extraItems);
        void ReMerge();
        void UnMerge(object child, ILinksHolder extraItems);
        void UpdateIsInRibbonWindow();
        void UpdateTitleAlignment();

        bool IsMerged { get; }

        bool IsChild { get; }

        bool IsBackStageViewOpen { get; }

        Thickness Margin { get; set; }

        ContentControl TabBackground { get; }

        Grid HeaderBorder { get; }

        double CategoriesHeight { get; }

        ContentControl BackgroundBorder { get; }

        DevExpress.Xpf.Ribbon.RibbonHeaderVisibility RibbonHeaderVisibility { get; }
    }
}

