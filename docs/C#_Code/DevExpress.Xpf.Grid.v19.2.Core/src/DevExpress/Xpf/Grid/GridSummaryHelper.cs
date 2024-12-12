namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public abstract class GridSummaryHelper : IGridSummaryItemsOwner, ISummaryItemsOwner
    {
        private GridSummaryItemsEditorController summaryController;
        internal readonly DataViewBase view;

        protected GridSummaryHelper(DataViewBase view)
        {
            this.view = view;
        }

        protected virtual bool AreItemsEqual(DevExpress.Xpf.Grid.SummaryItemBase item1, DevExpress.Xpf.Grid.SummaryItemBase item2) => 
            (item1.FieldName == item2.FieldName) && ((item1.SummaryType == item2.SummaryType) && ((item1.Alignment == item2.Alignment) && (item1.ShowInColumn == item2.ShowInColumn)));

        protected virtual bool CanBeUsedInSummary(ColumnBase column) => 
            this.view.CanShowColumnInSummaryEditor(column);

        protected internal virtual bool CanUseSummaryItem(ISummaryItem item) => 
            !string.IsNullOrEmpty(item.FieldName);

        protected virtual DevExpress.Xpf.Grid.SummaryItemBase CreateItemCore(string fieldName, SummaryItemType summaryType)
        {
            DevExpress.Xpf.Grid.SummaryItemBase base2 = this.view.DataControl.CreateSummaryItem();
            base2.FieldName = fieldName;
            base2.SummaryType = summaryType;
            return base2;
        }

        protected virtual GridSummaryItemsEditorController CreateSummaryItemController() => 
            new GridSummaryItemsEditorController(this);

        ISummaryItem ISummaryItemsOwner.CreateItem(string fieldName, SummaryItemType summaryType) => 
            this.CreateItemCore(fieldName, summaryType);

        string ISummaryItemsOwner.GetCaptionByFieldName(string fieldName) => 
            ColumnBase.GetDisplayName(this.view.DataControl.ColumnsCore[fieldName], fieldName);

        List<string> ISummaryItemsOwner.GetFieldNames()
        {
            List<string> list = new List<string>();
            foreach (ColumnBase base2 in this.view.DataControl.ColumnsCore)
            {
                if (this.PassesSummaryLimitations(base2) && this.CanBeUsedInSummary(base2))
                {
                    list.Add(base2.FieldName);
                }
            }
            return list;
        }

        List<ISummaryItem> ISummaryItemsOwner.GetItems()
        {
            List<ISummaryItem> list = new List<ISummaryItem>();
            foreach (DevExpress.Xpf.Grid.SummaryItemBase base2 in this.SummaryItems)
            {
                if (this.CanUseSummaryItem(base2))
                {
                    list.Add(base2);
                }
            }
            return list;
        }

        Type ISummaryItemsOwner.GetTypeByFieldName(string fieldName) => 
            this.view.GetColumnType(this.view.DataControl.ColumnsCore[fieldName], null);

        void ISummaryItemsOwner.SetItems(List<ISummaryItem> items)
        {
            this.SetItemsCore(items);
        }

        string IGridSummaryItemsOwner.FormatSummaryItemCaption(ISummaryItem item, string defaultCaption) => 
            this.view.FormatSummaryItemCaptionInSummaryEditor(item, defaultCaption);

        private DevExpress.Xpf.Grid.SummaryItemBase FindSummaryItem(DevExpress.Xpf.Grid.SummaryItemBase item, ISummaryItemOwner list)
        {
            DevExpress.Xpf.Grid.SummaryItemBase base3;
            using (IEnumerator<DevExpress.Xpf.Grid.SummaryItemBase> enumerator = list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DevExpress.Xpf.Grid.SummaryItemBase current = enumerator.Current;
                        if (!this.AreItemsEqual(current, item))
                        {
                            continue;
                        }
                        base3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return base3;
        }

        protected abstract string GetEditorTitle();
        public abstract bool IsSummaryItemExists(ISummaryItem item);
        internal virtual bool PassesSummaryLimitations(ColumnBase column) => 
            true;

        protected virtual void SetItemsCore(List<ISummaryItem> items)
        {
            try
            {
                this.SummaryItems.BeginUpdate();
                foreach (DevExpress.Xpf.Grid.SummaryItemBase base2 in this.SummaryItems)
                {
                    if (this.CanUseSummaryItem(base2))
                    {
                        base2.Visible = false;
                    }
                }
                foreach (DevExpress.Xpf.Grid.SummaryItemBase base3 in items)
                {
                    DevExpress.Xpf.Grid.SummaryItemBase item = this.FindSummaryItem(base3, this.SummaryItems);
                    if (item == null)
                    {
                        this.SummaryItems.Add(base3);
                        continue;
                    }
                    this.SummaryItems.Remove(item);
                    this.SummaryItems.Add(item);
                    item.Visible = true;
                }
            }
            finally
            {
                this.SummaryItems.EndUpdate();
            }
        }

        public virtual FloatingContainer ShowSummaryEditor() => 
            this.ShowSummaryEditor(SummaryEditorType.Default);

        public virtual FloatingContainer ShowSummaryEditor(SummaryEditorType summaryEditorType)
        {
            FloatingContainer container;
            FloatingContainerParameters parameters = new FloatingContainerParameters();
            parameters.Title = this.GetEditorTitle();
            parameters.AllowSizing = true;
            parameters.CloseOnEscape = true;
            this.view.SummaryEditorContainer = container = this.view.ShowDialogContent(new SummaryEditorControl(this.Controller, summaryEditorType), Size.Empty, parameters);
            return container;
        }

        internal GridSummaryItemsEditorController Controller
        {
            get
            {
                if (this.summaryController == null)
                {
                    this.summaryController = this.CreateSummaryItemController();
                    int num = 0;
                    while (num < this.summaryController.Items.Count)
                    {
                        if (!((DevExpress.Xpf.Grid.SummaryItemBase) this.summaryController.Items[num]).Visible)
                        {
                            this.summaryController.Items.Remove(this.summaryController.Items[num]);
                            continue;
                        }
                        num++;
                    }
                }
                return this.summaryController;
            }
        }

        protected abstract ISummaryItemOwner SummaryItems { get; }
    }
}

