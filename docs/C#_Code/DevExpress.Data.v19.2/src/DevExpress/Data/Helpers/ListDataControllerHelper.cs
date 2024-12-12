namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class ListDataControllerHelper : BaseListDataControllerHelper, IRelationListEx, IRelationList
    {
        public static string UseFirstRowTypeWhenPopulatingColumnsTypeName;
        private const string DefaultUseFirstRowTypeWhenPopulatingColumnsTypeName = "System.ServiceModel.DomainServices.Client.Entity";
        private int beforeAddRowCount;
        private int detachedRowPosition;

        static ListDataControllerHelper();
        public ListDataControllerHelper(DataControllerBase controller);
        protected override object AddNewRowCore();
        protected virtual PropertyDescriptorCollection CreateSimplePropertyDescriptor();
        IList IRelationList.GetDetailList(int listSourceRow, int relationIndex);
        string IRelationList.GetRelationName(int listSourceRow, int relationIndex);
        bool IRelationList.IsMasterRowEmpty(int listSourceRow, int relationIndex);
        int IRelationListEx.GetRelationCount(int listSourceRow);
        string IRelationListEx.GetRelationDisplayName(int listSourceRow, int relationIndex);
        private DataColumnInfo GetDetailInfo(int relationIndex);
        public static PropertyDescriptorCollection GetExpandoObjectProperties(DataControllerBase controller, object row);
        private object GetFirstRow();
        public Type GetIndexerPropertyType();
        public static Type GetIndexerPropertyType(Type listType);
        private PropertyDescriptorCollection GetItemProperties();
        private Type GetListType();
        public static Type GetListType(object dataSource);
        protected override PropertyDescriptorCollection GetPropertyDescriptorCollection();
        private PropertyDescriptorCollection GetPropertyDescriptorCollectionCore();
        public Type GetRowType(out bool isGenericIListRowType);
        public static Type GetRowType(Type listType, out bool isGenericIListRowType);
        protected virtual PropertyDescriptorCollection GetTypeProperties(Type rowType);
        private bool IsEmptyDetail(IList list);
        private bool IsValidRelationIndex(int relationIndex);
        protected internal override void OnBindingListChanged(ListChangedEventArgs e);
        protected override void OnEndNewItemRow();
        protected internal override void RaiseOnStartNewItemRow();
        protected virtual PropertyDescriptorCollection TryGetItemProperties();

        int IRelationList.RelationCount { get; }
    }
}

