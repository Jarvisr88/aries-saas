namespace DevExpress.Data
{
    using DevExpress.Data.Helpers;
    using System;
    using System.ComponentModel;

    public abstract class BaseGridControllerEx : BaseGridController
    {
        protected BaseGridControllerEx();
        public override void AddNewRow();
        public virtual void CancelNewRowEdit();
        public virtual int EndNewRowEdit();
        public override int GetListSourceRowIndex(int controllerRow);
        public override bool IsControllerRowValid(int controllerRow);
        protected internal override void OnEndNewItemRow();
        protected override void OnItemChanged(ListChangedEventArgs e, DataControllerChangedItemCollection changedItems);
        protected internal override void OnStartNewItemRow();
        protected override void StopCurrentRowEditCore();
    }
}

