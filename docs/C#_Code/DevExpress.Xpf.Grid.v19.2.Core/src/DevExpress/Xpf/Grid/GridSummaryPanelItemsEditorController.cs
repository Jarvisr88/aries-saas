namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using System;
    using System.Collections.Generic;

    public class GridSummaryPanelItemsEditorController : GridSummaryItemsEditorController
    {
        public GridSummaryPanelItemsEditorController(ISummaryItemsOwner itemsOwner) : base(itemsOwner)
        {
        }

        public override void AddSummary(string fieldName, SummaryItemType summaryType)
        {
            if (summaryType != SummaryItemType.Count)
            {
                base.AddSummary(fieldName, summaryType);
            }
            else if (this.FindSummaryItems(summaryType, base.Items).Count == 0)
            {
                IList<ISummaryItem> list = this.FindSummaryItems(summaryType, base.InitialItems);
                foreach (ISummaryItem item in list)
                {
                    base.Items.Add(item);
                }
                if (list.Count == 0)
                {
                    base.Items.Add(base.ItemsOwner.CreateItem(fieldName, summaryType));
                }
            }
        }

        private IList<ISummaryItem> FindSummaryItems(SummaryItemType summaryType, List<ISummaryItem> list)
        {
            IList<ISummaryItem> list2 = new List<ISummaryItem>();
            foreach (IAlignmentItem item in list)
            {
                if (this.TestItemAlignment(item) && ((item.SummaryType == summaryType) && this.IsGroupSummaryItem(item)))
                {
                    list2.Add(item);
                }
            }
            return list2;
        }

        public override bool HasFixedCountSummary() => 
            this.HasSummary(SummaryItemType.Count);

        public override bool HasSummary(SummaryItemType summaryType)
        {
            bool flag;
            using (List<ISummaryItem>.Enumerator enumerator = base.Items.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IAlignmentItem current = (IAlignmentItem) enumerator.Current;
                        if ((current.SummaryType != summaryType) || (current.Alignment == GridSummaryItemAlignment.Default))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public override void RemoveSummary(string fieldName, SummaryItemType summaryType)
        {
            if (summaryType != SummaryItemType.Count)
            {
                base.RemoveSummary(fieldName, summaryType);
            }
            else
            {
                foreach (ISummaryItem item in this.FindSummaryItems(summaryType, base.Items))
                {
                    base.Items.Remove(item);
                }
            }
        }

        protected override bool TestItemAlignment(ISummaryItem item) => 
            ((IAlignmentItem) item).Alignment != GridSummaryItemAlignment.Default;
    }
}

