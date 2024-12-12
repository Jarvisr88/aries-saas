namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    public class RowInfoCollection
    {
        private CardsPanelInfo panelInfo;
        private List<LineInfo> rowSeparators;
        private List<RowInfo> info;

        public RowInfoCollection(CardsPanelInfo panelInfo);
        public Rect[] Arrange(Size finalSize, IList<UIElement> sortedChildren);
        public double CalcCardSpace(RowInfo item, double finalSize, Alignment alignment, SizeHelperBase sizeHelper);
        public double CalcNearOffset(RowInfo item, double finalSize, Alignment alignment, SizeHelperBase sizeHelper);
        private static List<RowInfo> CalculateRowInfo(Size availableSize, CardsPanelInfo panelInfo, IList<UIElement> sortedChildren);
        private Rect CreateCardRect(double cardSpace, double currentHeight, double currentPosition, UIElement element);
        private static Size GetElementDesiredSize(Size size, Thickness margin);
        private static double GetSecondarySizeWithSeparators(Size size, CardsPanelInfo info);
        private Size GetUnboundInSecondaryDirectionSize(Size size);
        private static double GetValidSize(double normalSize, double fixedSize);
        private static Size GetValidSize(Size availableSize, SizeHelperBase sizeHelper, double fixedSize);
        private static bool IsEnoughItems(Size availableSize, CardsPanelInfo panelInfo, double currentWidth, int currentElementCount, Size elementDesiredSize);
        public Size Measure(Size availableSize, IList<UIElement> sortedChildren);

        public int Count { get; }

        public List<LineInfo> RowSeparators { get; }

        public RowInfo this[int index] { get; }
    }
}

