namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Xpf.Editors.Settings;
    using System;

    public class DataColumnInfoFilterColumn : FilterColumn
    {
        public readonly IDataColumnInfo Column;

        public DataColumnInfoFilterColumn(IDataColumnInfo column)
        {
            this.Column = column;
            this.HeaderTemplate = null;
            this.HeaderTemplateSelector = null;
            this.EditSettings = this.CreateEditSettings();
        }

        private BaseEditSettings CreateEditSettings() => 
            ((this.ColumnType == typeof(bool)) || (this.ColumnType == typeof(bool?))) ? ((BaseEditSettings) new CheckEditSettings()) : (((this.ColumnType == typeof(DateTime)) || (this.ColumnType == typeof(DateTime?))) ? ((BaseEditSettings) new DateEditSettings()) : ((BaseEditSettings) new TextEditSettings()));

        public override Type ColumnType =>
            this.Column.FieldType;

        public override string FieldName =>
            this.Column.Name;

        public override object ColumnCaption =>
            this.Column.Caption;
    }
}

