namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using System;
    using System.Collections.Generic;

    public class GridSummaryItemsEditorController : SummaryItemsEditorController
    {
        public GridSummaryItemsEditorController(ISummaryItemsOwner itemsOwner) : base(itemsOwner)
        {
        }

        public override void AddSummary(string fieldName, SummaryItemType summaryType)
        {
            if (this.CanApplySummary(summaryType, fieldName))
            {
                base.AddSummary(fieldName, summaryType);
            }
        }

        protected override bool CanApplySummaryCore(SummaryItemType summaryType, Type objectType)
        {
            if ((((summaryType != SummaryItemType.Average) && (summaryType != SummaryItemType.Sum)) || (objectType == null)) || ((objectType != typeof(TimeSpan)) && (objectType != typeof(TimeSpan?))))
            {
                return base.CanApplySummaryCore(summaryType, objectType);
            }
            return true;
        }

        protected override void CreateItemWithCountSummary()
        {
        }

        public static string GetGlobalCountSummaryName() => 
            GridControlLocalizer.GetString(GridControlStringId.MenuFooterRowCount);

        public static string GetNameBySummaryType(SummaryItemType summaryType)
        {
            switch (summaryType)
            {
                case SummaryItemType.Sum:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterSum);

                case SummaryItemType.Min:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterMin);

                case SummaryItemType.Max:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterMax);

                case SummaryItemType.Count:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterCount);

                case SummaryItemType.Average:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterAverage);

                case SummaryItemType.Custom:
                    return GridControlLocalizer.GetString(GridControlStringId.MenuFooterCustom);
            }
            return string.Empty;
        }

        protected override string GetSummaryItemCaption(ISummaryItem item) => 
            ((IGridSummaryItemsOwner) base.ItemsOwner).FormatSummaryItemCaption(item, base.GetSummaryItemCaption(item));

        protected override string GetTextBySummaryType(SummaryItemType summaryType) => 
            GetNameBySummaryType(summaryType);

        public virtual bool HasFixedCountSummary() => 
            base.FindSummaryItem("", SummaryItemType.Count, base.Items) != null;

        protected override bool IsGroupSummaryItem(ISummaryItem item) => 
            ((IGridSummaryItemsOwner) base.ItemsOwner).IsSummaryItemExists(item) ? this.TestItemAlignment(item) : false;

        protected override bool TestItemAlignment(ISummaryItem item) => 
            ((IAlignmentItem) item).Alignment == GridSummaryItemAlignment.Default;

        internal ISummaryItemsOwner Owner =>
            base.ItemsOwner;

        public List<SummaryEditorUIItem> UIItems =>
            base.UIItems;
    }
}

