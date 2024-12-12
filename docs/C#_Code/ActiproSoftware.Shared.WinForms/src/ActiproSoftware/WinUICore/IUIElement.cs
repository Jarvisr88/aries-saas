namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public interface IUIElement : IDisposable, ILogicalTreeNode
    {
        void Arrange(Rectangle newBounds);
        bool ContainsLocation(Point location);
        Graphics CreateGraphics();
        UIElementDrawState GetDrawState();
        PointHitTestResult HitTest(PointHitTestParameters hitTestParams);
        PointHitTestResult HitTestRecursive(PointHitTestParameters hitTestParams);
        void Invalidate(InvalidationLevels levels, InvalidationTypes types);
        void InvalidateArrange();
        void InvalidateMeasure();
        void Measure(Graphics g, System.Drawing.Size availableSize);
        void NotifyChildDesiredSizeChanged();
        void NotifyMouseLeaveEvent();
        void Render(PaintEventArgs e);
        GeneralTransform TransformToAncestor(IUIElement ancestor);
        GeneralTransform TransformToDescendant(IUIElement descendant);

        int ActualHeight { get; }

        int ActualWidth { get; }

        Rectangle Bounds { get; }

        Rectangle ClipBounds { get; }

        System.Drawing.Size DesiredSize { get; }

        bool IsArrangeValid { get; }

        bool IsMeasureValid { get; }

        bool IsRightToLeft { get; }

        System.Drawing.Size Size { get; }

        ActiproSoftware.WinUICore.Visibility Visibility { get; set; }

        Point VisualOffset { get; }
    }
}

