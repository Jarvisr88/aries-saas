namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Linq;
    using System;

    public class CollectionViewCurrentDataView : LocalCurrentDataView
    {
        public CollectionViewCurrentDataView(bool selectNullValue, object view, object handle, string valueMember, string displayMember) : base(selectNullValue, view, handle, valueMember, displayMember, false)
        {
        }

        protected override object CreateVisibleListWrapper() => 
            ((ICollectionViewHelper) this.ListSource.ListSource).Collection;

        private DefaultDataView ListSource =>
            base.ListSource as DefaultDataView;
    }
}

