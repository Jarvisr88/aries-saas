namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FilterColumn
    {
        private object columnCaption;
        private BaseEditSettings editSettings;

        public virtual ClauseType GetDefaultOperation() => 
            FilterControlHelper.GetDefaultOperation(this.ClauseClass);

        public virtual bool IsValidClause(ClauseType clause) => 
            FilterControlHelpers.IsValidClause(clause, this.ClauseClass);

        public virtual object ColumnCaption
        {
            get => 
                ((this.columnCaption == null) || ((this.columnCaption as string) == string.Empty)) ? this.FieldName : this.columnCaption;
            set => 
                this.columnCaption = value;
        }

        public virtual DataTemplate HeaderTemplate { get; set; }

        public virtual DataTemplateSelector HeaderTemplateSelector { get; set; }

        public virtual string FieldName { get; set; }

        public virtual BaseEditSettings EditSettings
        {
            get
            {
                this.editSettings ??= new TextEditSettings();
                return this.editSettings;
            }
            set => 
                this.editSettings = value;
        }

        public virtual FilterColumnClauseClass ClauseClass =>
            !(this.ColumnType == typeof(string)) ? (((this.ColumnType == typeof(DateTime)) || (this.ColumnType == typeof(DateTime?))) ? FilterColumnClauseClass.DateTime : FilterColumnClauseClass.Generic) : FilterColumnClauseClass.String;

        public virtual Type ColumnType { get; set; }
    }
}

