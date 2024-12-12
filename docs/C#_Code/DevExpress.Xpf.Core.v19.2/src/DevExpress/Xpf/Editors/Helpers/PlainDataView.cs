namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;

    public abstract class PlainDataView : DataView
    {
        protected PlainDataView(bool selectNullValue, object view, string valueMember, string displayMember) : base(selectNullValue, view, valueMember, displayMember)
        {
        }

        protected override void InitializeView(object source)
        {
            base.SetView(this.CreateDataProxyViewCache((IList) source));
        }

        public override bool ProcessAddItem(int index)
        {
            DataProxy item = base.DataAccessor.CreateProxy(this.ListSource[index], -1);
            base.View.Add(index, item);
            base.ItemsCache.UpdateItemOnAdding(index);
            return true;
        }

        public override bool ProcessChangeItem(int index)
        {
            DataProxy item = base.DataAccessor.CreateProxy(this.ListSource[index], -1);
            base.View.Replace(index, item);
            base.ItemsCache.UpdateItem(index);
            return true;
        }

        public override bool ProcessDeleteItem(int index)
        {
            base.View.Remove(index);
            base.ItemsCache.UpdateItemOnDeleting(index);
            return true;
        }

        public override bool ProcessMoveItem(int oldIndex, int newIndex)
        {
            DataProxy item = base.View[oldIndex];
            base.View.Remove(oldIndex);
            base.View.Add(newIndex, item);
            base.ItemsCache.UpdateItemOnMoving(oldIndex, newIndex);
            return true;
        }

        public override bool ProcessReset()
        {
            base.ItemsCache.Reset();
            this.Initialize();
            return true;
        }

        private IList ListSource =>
            base.ListSource as IList;
    }
}

