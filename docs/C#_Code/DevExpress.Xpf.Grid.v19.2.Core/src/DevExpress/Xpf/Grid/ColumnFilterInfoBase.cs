namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Summary;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public abstract class ColumnFilterInfoBase
    {
        private PopupBaseEdit currentPopup;

        public ColumnFilterInfoBase(ColumnBase column)
        {
            this.Column = column;
        }

        protected virtual void AfterPopupOpening(PopupBaseEdit popup)
        {
        }

        internal virtual void ApplyDelayedFilter()
        {
            this.ApplyFilter(this.GetFilterCriteriaCore(), false);
        }

        private void ApplyFilter(CriteriaOperator op, bool updateImmediately)
        {
            if (updateImmediately)
            {
                this.Column.ApplyColumnFilter(op);
            }
            else if (this.View != null)
            {
                this.View.EnqueueImmediateAction(() => this.Column.ApplyColumnFilter(op));
            }
        }

        public abstract bool CanShowFilterPopup();
        protected abstract void ClearPopupData(PopupBaseEdit popup);
        internal abstract PopupBaseEdit CreateColumnFilterPopup();
        protected virtual ExcelColumnFilterSettings GetExcelColumnCustomFilterSettings() => 
            null;

        protected internal abstract CriteriaOperator GetFilterCriteria();
        protected virtual CriteriaOperator GetFilterCriteriaCore() => 
            this.GetFilterCriteria();

        protected virtual bool GetIsNullableColumn()
        {
            Type fieldType = this.Column.FieldType;
            return ((Nullable.GetUnderlyingType(fieldType) == null) ? !fieldType.IsValueType : true);
        }

        protected virtual bool GetIsVirtualSource() => 
            (this.View != null) && ((this.View.DataControl != null) && this.View.DataControl.DataProviderBase.IsVirtualSource);

        protected internal virtual IEnumerable GetSelectedItems(IEnumerable items, CriteriaOperator op) => 
            null;

        protected virtual bool IsDateTimeColumn() => 
            SummaryItemTypeHelper.IsDateTime(this.Column.FieldType);

        internal virtual void OnPopupClosed(PopupBaseEdit popup, bool applyFilter)
        {
            this.View.IsColumnFilterOpened = false;
            if (applyFilter && !this.ImmediateUpdateFilter)
            {
                this.ApplyDelayedFilter();
            }
            this.ClearPopupData(popup);
            this.currentPopup = null;
            this.View.KeyboardLocker.Unlock();
            if (this.View.DataControl.GetRootDataControl().IsKeyboardFocusWithin)
            {
                this.View.SetFocusToRowCell();
            }
        }

        internal void OnPopupOpened(PopupBaseEdit popup)
        {
            this.View.KeyboardLocker.Lock();
            this.OnPopupOpenedCore(popup);
        }

        protected virtual void OnPopupOpenedCore(PopupBaseEdit popup)
        {
        }

        internal void OnPopupOpening(PopupBaseEdit popup)
        {
            this.View.IsColumnFilterLoaded = false;
            this.View.IsColumnFilterOpened = true;
            this.currentPopup = popup;
            this.UpdatePopupData(popup);
            this.UpdatePopupButtonsVisibility(popup);
            this.UpdateSizeGripVisibility(popup);
            this.RaiseFilterPopupEvent(popup);
            this.AfterPopupOpening(popup);
            this.View.IsColumnFilterLoaded = true;
        }

        protected virtual void RaiseFilterPopupEvent(PopupBaseEdit popup)
        {
            if (this.View != null)
            {
                this.View.RaiseFilterPopupEvent(this.Column, popup, new Lazy<ExcelColumnFilterSettings>(new Func<ExcelColumnFilterSettings>(this.GetExcelColumnCustomFilterSettings)));
            }
        }

        protected virtual PopupFooterButtons ShowButtons() => 
            !this.ImmediateUpdateFilter ? PopupFooterButtons.OkCancel : PopupFooterButtons.None;

        protected virtual void UpdateColumnFilterIfNeeded(Func<CriteriaOperator> getOperator)
        {
            if (this.ImmediateUpdateFilter && this.View.KeyboardLocker.IsLocked)
            {
                this.ApplyFilter(getOperator(), true);
            }
        }

        internal void UpdateCurrentPopupData(object[] values)
        {
            if (this.currentPopup != null)
            {
                this.UpdatePopupData(this.currentPopup, values);
            }
        }

        protected virtual void UpdatePopupButtonsVisibility(PopupBaseEdit popup)
        {
            popup.PopupFooterButtons = new PopupFooterButtons?(this.ShowButtons());
        }

        protected abstract void UpdatePopupData(PopupBaseEdit popup);
        protected virtual void UpdatePopupData(PopupBaseEdit popup, object[] values)
        {
        }

        protected virtual void UpdateSizeGripVisibility(PopupBaseEdit popup)
        {
            popup.ShowSizeGrip = true;
        }

        public ColumnBase Column { get; private set; }

        protected DataViewBase View =>
            this.Column.Owner as DataViewBase;

        protected virtual bool ImmediateUpdateFilter =>
            this.Column.ImmediateUpdateColumnFilter;

        protected virtual bool AddNullItem =>
            false;

        protected virtual bool ImplyNullLikeEmptyStringWhenFiltering =>
            this.View.DataControl.ImplyNullLikeEmptyStringWhenFiltering;

        protected virtual bool RoundDateTimeValues =>
            this.Column.RoundDateTimeForColumnFilter;

        protected bool IsVirtualSource =>
            this.GetIsVirtualSource();
    }
}

