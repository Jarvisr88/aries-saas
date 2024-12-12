namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Windows;

    public interface IElementHost
    {
        FrameworkElement GetChild(int index);
        void InvalidateArrange();
        void InvalidateMeasure();
        void InvalidateVisual();

        double DpiScale { get; }

        int ChildrenCount { get; }

        IEnumerator LogicalChildren { get; }

        FrameworkElement TemplatedParent { get; }

        FrameworkElement Parent { get; }

        bool IsMeasureValid { get; }

        bool MeasureInProgress { get; }

        bool MeasureDuringArrange { get; }

        bool IsArrangeValid { get; }

        bool ArrangeInProgress { get; }
    }
}

