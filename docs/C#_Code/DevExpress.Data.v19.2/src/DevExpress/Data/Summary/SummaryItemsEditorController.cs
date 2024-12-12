namespace DevExpress.Data.Summary
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class SummaryItemsEditorController
    {
        private ISummaryItemsOwner itemsOwner;
        private List<ISummaryItem> items;
        private List<ISummaryItem> initialItems;
        private List<SummaryEditorUIItem> uiItems;

        public SummaryItemsEditorController(ISummaryItemsOwner itemsOwner);
        public virtual void AddSummary(string fieldName, SummaryItemType summaryType);
        public void Apply();
        public virtual bool CanApplySummary(SummaryItemType summaryType, string fieldName);
        protected internal virtual bool CanApplySummaryCore(SummaryItemType summaryType, Type objectType);
        protected virtual void CreateItemWithCountSummary();
        public List<SummaryEditorOrderUIItem> CreateOrderItems();
        private void CreateUIItems();
        protected ISummaryItem FindSummaryItem(string fieldName, SummaryItemType summaryType, List<ISummaryItem> list);
        protected virtual string GetSummaryItemCaption(ISummaryItem item);
        protected virtual string GetTextBySummaryType(SummaryItemType summaryType);
        public virtual bool HasSummary(SummaryItemType summaryType);
        public bool HasSummary(string fieldName);
        public bool HasSummary(string fieldName, SummaryItemType summaryType);
        protected virtual bool IsGroupSummaryItem(ISummaryItem item);
        public virtual void RemoveSummary(string fieldName, SummaryItemType summaryType);
        public void SetSummary(string fieldName, SummaryItemType summaryType, bool value);
        protected virtual bool TestItemAlignment(ISummaryItem item);
        private int UIItemCompare(SummaryEditorUIItem item1, SummaryEditorUIItem item2);

        public List<ISummaryItem> Items { get; }

        protected List<SummaryEditorUIItem> UIItems { get; }

        public int Count { get; }

        public SummaryEditorUIItem this[int index] { get; }

        public SummaryEditorUIItem this[string fieldName] { get; }

        public bool HasCountByEmptyField { get; set; }

        protected ISummaryItemsOwner ItemsOwner { get; }

        protected List<ISummaryItem> InitialItems { get; }
    }
}

