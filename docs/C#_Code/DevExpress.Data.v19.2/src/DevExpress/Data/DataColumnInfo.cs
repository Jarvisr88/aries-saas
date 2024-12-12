namespace DevExpress.Data
{
    using DevExpress.Data.Access;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class DataColumnInfo : IDataColumnInfo
    {
        private System.Type type;
        private string name;
        private bool allowSort;
        private System.ComponentModel.PropertyDescriptor propertyDescriptor;
        private bool autoSplitCasing;
        private int columnIndex;
        private int dataIndex;
        private bool visible;
        private bool unbound;
        private bool allowErrorInfo;
        private object tag;
        internal DataColumnInfoCollection owner;
        private IComparer customComparer;
        private string caption;
        private bool isDataViewDescriptorCore;

        public DataColumnInfo(System.ComponentModel.PropertyDescriptor descriptor, bool autoSplitCasing = true);
        public object ConvertValue(object val);
        public object ConvertValue(object val, bool useCurrentCulture);
        public object ConvertValue(object val, bool useCurrentCulture, DataControllerBase controller);
        public System.Type GetDataType();
        protected void SetAsUnbound();
        internal void SetOwner(DataColumnInfoCollection owner);
        protected internal virtual void SetPropertyDescriptor(System.ComponentModel.PropertyDescriptor descriptor);
        public override string ToString();

        public virtual IComparer CustomComparer { get; set; }

        public bool Visible { get; set; }

        public int DataIndex { get; set; }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; }

        public string Caption { get; }

        public string Name { get; }

        public System.Type Type { get; }

        public bool ReadOnly { get; }

        public bool Browsable { get; }

        public bool AllowSort { get; set; }

        public bool Unbound { get; }

        public bool UnboundWithExpression { get; }

        public string UnboundExpression { get; }

        public bool IsDataViewDescriptor { get; }

        public object Tag { get; set; }

        protected internal int ColumnIndex { get; set; }

        public int Index { get; }

        protected internal bool AllowErrorInfo { get; }

        private UnboundPropertyDescriptor UnboundDescriptor { get; }

        List<IDataColumnInfo> IDataColumnInfo.Columns { get; }

        string IDataColumnInfo.UnboundExpression { get; }

        string IDataColumnInfo.Caption { get; }

        string IDataColumnInfo.FieldName { get; }

        string IDataColumnInfo.Name { get; }

        DataControllerBase IDataColumnInfo.Controller { get; }

        System.Type IDataColumnInfo.FieldType { get; }
    }
}

