namespace DevExpress.Data.TreeList.DataHelpers
{
    using DevExpress.Data;
    using DevExpress.Data.TreeList;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class TreeListBoundDataHelper : TreeListDataHelperBase
    {
        public TreeListBoundDataHelper(TreeListDataControllerBase controller, object dataSource);
        public override TreeListNodeBase AddNewNode(TreeListNodeBase parentNode);
        public override bool AllowNew(TreeListNodeBase parentNode);
        protected internal override void CancelNewNode(TreeListNodeBase node);
        public override void Dispose();
        protected internal override void EndNewNode(TreeListNodeBase node);
        protected override PropertyDescriptor GetActualComplexPropertyDescriptor(ComplexColumnInfo info);
        protected virtual PropertyDescriptor GetActualPropertyDescriptor(PropertyDescriptor descriptor);
        public override IEnumerable<IBindingList> GetBindingLists();
        private ICancelAddNew GetCancelAddNewObject(TreeListNodeBase node);
        protected virtual PropertyDescriptorCollection GetExpandoObjectProperties(object item);
        protected object GetFirstItem();
        protected PropertyDescriptorCollection GetIItemProperties(object source);
        protected virtual Type GetItemType(IList list);
        protected Type GetListItemPropertyType(IList list);
        protected virtual IList GetListSource(TreeListNodeBase node);
        protected virtual PropertyDescriptorCollection GetPropertyDescriptorCollection();
        protected virtual void InitNewNodeId(TreeListNodeBase node);
        protected override bool IsColumnVisible(DataColumnInfo column);
        protected virtual bool IsServiceColumnName(string fieldName);
        protected virtual bool IsValidColumnName(string fieldName);
        public override void LoadData();
        protected virtual void PatchColumnCollection(PropertyDescriptorCollection properties);
        public override void PopulateColumns();
        protected virtual bool UseFirstRowTypeWhenPopulatingColumns(Type itemType);

        public override Type ItemType { get; }

        public override bool IsReady { get; }

        public override bool IsLoaded { get; }

        public override bool AllowEdit { get; }

        public override bool AllowRemove { get; }

        public IList ListSource { get; private set; }

        protected ICollectionView CollectionViewSource { get; private set; }

        protected ITypedList TypedList { get; }

        protected virtual IBindingList BindingList { get; set; }

        protected internal override bool SupportNotifications { get; }
    }
}

