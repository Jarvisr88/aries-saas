namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Data.Design;
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    [DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.DynamicListLookUpSettings")]
    public class DynamicListLookUpSettings : LookUpSettings
    {
        public DynamicListLookUpSettings()
        {
            this.DataMember = string.Empty;
            this.ValueMember = string.Empty;
            this.SortMember = string.Empty;
            this.SortOrder = ColumnSortOrder.None;
        }

        [XtraSerializableProperty(XtraSerializationVisibility.Reference), Editor("DevExpress.XtraReports.Design.DataSourceEditor,DevExpress.XtraReports.v19.2.Extensions", typeof(UITypeEditor)), TypeConverter(typeof(DataSourceConverter))]
        public override object DataSource { get; set; }

        [XtraSerializableProperty(XtraSerializationVisibility.Reference), Editor("DevExpress.XtraReports.Design.DataAdapterEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.DataAdapterConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), DefaultValue(null)]
        public override object DataAdapter { get; set; }

        [XtraSerializableProperty, DefaultValue(""), Editor("DevExpress.XtraReports.Design.DataContainerDataMemberEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.DataMemberTypeConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
        public override string DataMember { get; set; }

        [XtraSerializableProperty, DefaultValue(""), Editor("DevExpress.XtraReports.Design.DataContainerFieldNameEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.FieldNameConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Description("The name of the field that stores the report parameter's values."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.DynamicListLookUpSettings.ValueMember")]
        public string ValueMember { get; set; }

        [XtraSerializableProperty, DefaultValue(""), Editor("DevExpress.XtraReports.Design.DataContainerFieldNameEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.FieldNameConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Description("Specifies the data member for the storage that contains the report parameter's display name."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.DynamicListLookUpSettings.DisplayMember")]
        public string DisplayMember { get; set; }

        [XtraSerializableProperty, DefaultValue(""), Editor("DevExpress.XtraReports.Design.DataContainerFieldNameEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.FieldNameConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Description("Specifies the name of the field that is used to sort parameter values in the lookup editor."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.DynamicListLookUpSettings.SortMember")]
        public string SortMember { get; set; }

        [XtraSerializableProperty, DefaultValue(0), Description("Specifies the sort order for the parameter values in the lookup editor."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.DynamicListLookUpSettings.SortOrder")]
        public ColumnSortOrder SortOrder { get; set; }
    }
}

