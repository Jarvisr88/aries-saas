namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraReports.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;

    public abstract class LookUpSettings : ValueSourceSettings, IDataContainer, IDataContainerBase, IDataContainerBase2
    {
        protected LookUpSettings()
        {
            this.FilterString = string.Empty;
        }

        string IDataContainerBase2.GetEffectiveDataMember()
        {
            throw new NotImplementedException();
        }

        object IDataContainerBase2.GetEffectiveDataSource() => 
            this.DataSource;

        object IDataContainer.GetSerializableDataSource() => 
            this.DataSource;

        [XtraSerializableProperty, DefaultValue(""), Editor("DevExpress.XtraReports.Design.LookUpSettingsFilterStringEditor,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a", typeof(UITypeEditor)), TypeConverter("DevExpress.XtraReports.Design.TextPropertyTypeConverter,DevExpress.XtraReports.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"), Description("Specifies the filter criteria applied to the LookUpSettings object."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.LookUpSettings.FilterString")]
        public string FilterString { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Description("Specifies the data source that is used to provide parameter values to the lookup editor."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.LookUpSettings.DataSource")]
        public abstract object DataSource { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Description("Specifies the data member that is used to provide parameter values to the lookup editor."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.LookUpSettings.DataMember")]
        public abstract string DataMember { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Description("Specifies the data adapter that is used to provide parameter values to the lookup editor."), DXDisplayName(typeof(DevExpress.Printing.ResFinder), "DevExpress.XtraReports.Parameters.LookUpSettings.DataAdapter")]
        public abstract object DataAdapter { get; set; }
    }
}

