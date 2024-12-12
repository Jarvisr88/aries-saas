namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Summary;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SummaryEditorOrderListHelper
    {
        private Dictionary<IAlignmentItem, GridSummaryItemAlignment> alignmentListSource = new Dictionary<IAlignmentItem, GridSummaryItemAlignment>();

        public SummaryEditorOrderListHelper(GridSummaryItemsEditorController controller)
        {
            this.Controller = controller;
        }

        public void ApplyAlignments()
        {
            foreach (ISummaryItem item in this.Controller.Items)
            {
                IAlignmentItem key = item as IAlignmentItem;
                if ((key != null) && this.alignmentListSource.ContainsKey(key))
                {
                    key.Alignment = this.alignmentListSource[key];
                }
            }
        }

        public List<AlignmentSummaryEditorOrderUIItem> GetOrderListSource(List<SummaryEditorOrderUIItem> orderItems)
        {
            Dictionary<IAlignmentItem, GridSummaryItemAlignment> dictionary = new Dictionary<IAlignmentItem, GridSummaryItemAlignment>();
            List<AlignmentSummaryEditorOrderUIItem> list = new List<AlignmentSummaryEditorOrderUIItem>();
            foreach (SummaryEditorOrderUIItem item in orderItems)
            {
                GridSummaryItemAlignment alignment;
                if (!this.alignmentListSource.TryGetValue((IAlignmentItem) item.Item, out alignment))
                {
                    alignment = ((IAlignmentItem) item.Item).Alignment;
                }
                list.Add(new AlignmentSummaryEditorOrderUIItem(this.Controller, this, (IAlignmentItem) item.Item, item.Caption));
                dictionary.Add((IAlignmentItem) item.Item, alignment);
            }
            this.alignmentListSource = dictionary;
            return list;
        }

        public List<AlignmentSummaryEditorOrderUIItem> GetOrderListSource(List<SummaryEditorOrderUIItem> orderItems, GridSummaryItemAlignment alignment) => 
            this.GetOrderListSource(orderItems).Where<AlignmentSummaryEditorOrderUIItem>(delegate (AlignmentSummaryEditorOrderUIItem item) {
                GridSummaryItemAlignment? summaryItemAlignment = this.GetSummaryItemAlignment(item.Item);
                GridSummaryItemAlignment alignment = alignment;
                return ((((GridSummaryItemAlignment) summaryItemAlignment.GetValueOrDefault()) == alignment) ? (summaryItemAlignment != null) : false);
            }).ToList<AlignmentSummaryEditorOrderUIItem>();

        public GridSummaryItemAlignment? GetSummaryItemAlignment(ISummaryItem item)
        {
            GridSummaryItemAlignment? nullable;
            IAlignmentItem objB = item as IAlignmentItem;
            if (objB == null)
            {
                return null;
            }
            using (Dictionary<IAlignmentItem, GridSummaryItemAlignment>.KeyCollection.Enumerator enumerator = this.alignmentListSource.Keys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IAlignmentItem current = enumerator.Current;
                        if (!ReferenceEquals(current, objB))
                        {
                            continue;
                        }
                        nullable = new GridSummaryItemAlignment?(this.alignmentListSource[current]);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return nullable;
        }

        public void SetSummaryItemAlignment(ISummaryItem item, GridSummaryItemAlignment alignment)
        {
            IAlignmentItem objB = item as IAlignmentItem;
            if (objB != null)
            {
                foreach (IAlignmentItem item3 in this.alignmentListSource.Keys)
                {
                    if (ReferenceEquals(item3, objB))
                    {
                        this.alignmentListSource[item3] = alignment;
                        break;
                    }
                }
            }
        }

        public bool TryGetAlignment(IAlignmentItem alignmentItem, out GridSummaryItemAlignment actualItemAlignment)
        {
            if (!this.alignmentListSource.TryGetValue(alignmentItem, out actualItemAlignment))
            {
                actualItemAlignment = alignmentItem.Alignment;
                return true;
            }
            actualItemAlignment = GridSummaryItemAlignment.Default;
            return false;
        }

        private GridSummaryItemsEditorController Controller { get; set; }
    }
}

