namespace DevExpress.DataAccess
{
    using DevExpress.Data;
    using DevExpress.Data.Controls.DataAccess;
    using DevExpress.Data.Localization;
    using DevExpress.XtraEditors.Filtering;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Xml.Linq;

    [TypeConverter("DevExpress.XtraReports.Design.ParameterValueEditorChangingConverter,DevExpress.Utils.v19.2.UI")]
    public class DataSourceParameterBase : IParameter, IFilterParameter
    {
        private System.Type type;
        private object value;

        public DataSourceParameterBase() : this(null, typeof(string), null)
        {
        }

        public DataSourceParameterBase(string name, System.Type type, object value)
        {
            this.Name = name;
            this.type = type;
            this.value = value;
        }

        internal static DataSourceParameterBase FromIParameter(IParameter value)
        {
            object obj1;
            if (value == null)
            {
                obj1 = null;
            }
            else
            {
                DataSourceParameterBase base1 = value as DataSourceParameterBase;
                obj1 = base1;
                if (base1 == null)
                {
                    DataSourceParameterBase local1 = base1;
                    return new DataSourceParameterBase(value.Name, value.Type, value.Value);
                }
            }
            return (DataSourceParameterBase) obj1;
        }

        internal void LoadFromXMl(XElement element)
        {
            ParameterSerializer.DeserializeParameter(this, element, null);
        }

        internal XElement SaveToXml() => 
            ParameterSerializer.SerializeParameter(null, this);

        private bool ShouldSerializeValue() => 
            this.Type == typeof(Expression);

        private bool ShouldSerializeValueInfo()
        {
            if (this.Type == typeof(Expression))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.ValueInfo))
            {
                this.ValueInfo = ParameterHelper.ConvertValueToString(this.Value);
            }
            return !string.IsNullOrEmpty(this.ValueInfo);
        }

        [LocalizableCategory(CommonStringId.PropertyGridDesignCategoryName)]
        public string Name { get; set; }

        [DefaultValue((string) null), TypeConverter("DevExpress.DataAccess.UI.Native.Sql.QueryParameterTypeConverter,DevExpress.DataAccess.v19.2.UI"), RefreshProperties(RefreshProperties.All), LocalizableCategory(CommonStringId.PropertyGridDesignCategoryName)]
        public System.Type Type
        {
            get => 
                this.type;
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    if (((this.type != null) && (this.type != typeof(Expression))) && ParameterHelper.ShouldConvertValue(this.value, this.type))
                    {
                        this.value = ParameterHelper.ConvertFrom(this.value, this.type, ParameterHelper.GetDefaultValue(this.type));
                    }
                }
            }
        }

        [LocalizableCategory(CommonStringId.PropertyGridDesignCategoryName)]
        public object Value
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ValueInfo))
                {
                    this.value = ParameterHelper.ConvertFrom(this.ValueInfo, this.Type, null);
                }
                return this.value;
            }
            set
            {
                this.value = value;
                this.ValueInfo = null;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public string ValueInfo { get; set; }

        public class BaseEqualityComparer : IEqualityComparer<DataSourceParameterBase>
        {
            public static bool Equals(IParameter x, IParameter y) => 
                string.Equals(x.Name, y.Name, StringComparison.Ordinal) && ((x.Type == y.Type) && Equals(x.Value, y.Value));

            bool IEqualityComparer<DataSourceParameterBase>.Equals(DataSourceParameterBase x, DataSourceParameterBase y) => 
                Equals(x, y);

            int IEqualityComparer<DataSourceParameterBase>.GetHashCode(DataSourceParameterBase obj) => 
                0;
        }
    }
}

