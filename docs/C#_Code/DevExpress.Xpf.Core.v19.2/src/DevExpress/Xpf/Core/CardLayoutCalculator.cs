namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class CardLayoutCalculator
    {
        private RowInfoCollection rowInfo;

        public IList<Rect> ArrangeElements(Size finalSize, IList<UIElement> sortedChildren) => 
            (this.rowInfo != null) ? ((IList<Rect>) this.rowInfo.Arrange(finalSize, sortedChildren)) : ((IList<Rect>) new List<Rect>());

        public Size MeasureElements(Size availableSize, CardsPanelInfo panelInfo, IList<UIElement> sortedChildren)
        {
            this.rowInfo = new RowInfoCollection(panelInfo);
            return this.rowInfo.Measure(availableSize, sortedChildren);
        }

        public List<LineInfo> RowSeparators =>
            this.rowInfo.RowSeparators;

        public int RowsCount =>
            (this.rowInfo != null) ? this.rowInfo.Count : 1;

        public RowInfoCollection Rows =>
            this.rowInfo;
    }
}

