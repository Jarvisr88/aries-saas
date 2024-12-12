namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;

    public class FieldMapData : ISupportObjectChanged
    {
        private int columnIndex;
        private bool dynamicAddress;
        private string mappedName;
        private string columnName;
        private MailMergeFieldType fieldType;
        private int mergeFieldNameLanguageId;
        private EventHandler changed;

        public event EventHandler Changed
        {
            add
            {
                this.changed += value;
            }
            remove
            {
                this.changed -= value;
            }
        }

        protected virtual void RaiseChanged()
        {
            if (this.changed != null)
            {
                this.changed(this, EventArgs.Empty);
            }
        }

        public int ColumnIndex
        {
            get => 
                this.columnIndex;
            set
            {
                this.columnIndex = value;
                this.RaiseChanged();
            }
        }

        public bool DynamicAddress
        {
            get => 
                this.dynamicAddress;
            set
            {
                this.dynamicAddress = value;
                this.RaiseChanged();
            }
        }

        public string MappedName
        {
            get => 
                this.mappedName;
            set
            {
                this.mappedName = value;
                this.RaiseChanged();
            }
        }

        public string ColumnName
        {
            get => 
                this.columnName;
            set
            {
                this.columnName = value;
                this.RaiseChanged();
            }
        }

        public MailMergeFieldType FieldType
        {
            get => 
                this.fieldType;
            set
            {
                this.fieldType = value;
                this.RaiseChanged();
            }
        }

        public int MergeFieldNameLanguageId
        {
            get => 
                this.mergeFieldNameLanguageId;
            set
            {
                this.mergeFieldNameLanguageId = value;
                this.RaiseChanged();
            }
        }
    }
}

