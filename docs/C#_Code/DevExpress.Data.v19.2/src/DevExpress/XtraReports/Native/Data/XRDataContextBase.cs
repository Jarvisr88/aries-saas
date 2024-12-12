namespace DevExpress.XtraReports.Native.Data
{
    using DevExpress.Data.Browsing;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class XRDataContextBase : DataContext
    {
        private bool isCalculatedFieldsApplied;
        private bool isCalculatedFieldsApplying;
        protected IEnumerable<IParameter> parameters;
        protected IEnumerable<ICalculatedField> calculatedFields;

        public XRDataContextBase();
        public XRDataContextBase(IEnumerable<ICalculatedField> calculatedFields);
        public XRDataContextBase(IEnumerable<ICalculatedField> calculatedFields, bool suppressListFilling);
        public XRDataContextBase(IEnumerable<ICalculatedField> calculatedFields, IEnumerable<IParameter> parameters, bool suppressListFilling);
        public void ApplyCalculatedFields(IEnumerable<ICalculatedField> calculatedFields);
        public override void Clear();
        private static string ConcatStrings(string s1, string s2);
        protected virtual CalculatedPropertyDescriptorBase CreateCalculatedPropertyDescriptor(ICalculatedField calculatedField);
        protected override ListBrowser CreateListBrowser(DataPair data, ListControllerBase listController);
        protected override RelatedListBrowser CreateRelatedListBrowser(DataPair data, DataBrowser parent, PropertyDescriptor prop, ListControllerBase listController);
        protected override void EnsureCalculatedFields();
        public string GetActualDataMember(object dataSource, string dataMember);
        private string GetActualDataMember(object dataSource, string parentDataMember, string dataMember);
        public string GetDataMemberFromDisplayName(object dataSource, string dataMember, string displayName);
        public ObjectNameCollection GetItemDisplayNames(object dataSource, string dataMember);
        public ObjectNameCollection GetItemDisplayNames(object dataSource, string dataMember, bool forceList);
        public bool IsDataMemberValid(object dataSource, string dataMember);
        protected override bool IsIDisplayNameProviderSupported(PropertyDescriptor property);
        internal void ResetCalculatedFieldsApplied();
    }
}

