namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class FlowLayoutLayerInfo
    {
        public FlowLayoutItemSize Size;

        public FlowLayoutLayerInfo(int firstItemIndex, bool isHardFlowBreak, FlowLayoutItemPosition position)
        {
            this.FirstItemIndex = firstItemIndex;
            this.IsHardFlowBreak = isHardFlowBreak;
            this.Position = position;
            this.SlotFirstItemIndexes = new List<int>();
        }

        public int FirstItemIndex { get; private set; }

        public bool IsHardFlowBreak { get; private set; }

        public FlowLayoutItemPosition Position { get; private set; }

        public int SlotCount =>
            this.SlotFirstItemIndexes.Count;

        public List<int> SlotFirstItemIndexes { get; private set; }
    }
}

