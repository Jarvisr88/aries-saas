namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;

    public class DataSourceObjectProperties : IDisposable
    {
        private Dictionary<string, FieldMapData> mapColumnName;
        private Dictionary<string, FieldMapData> mapFieldMappedName;
        private char columnDelimiter;
        private bool firstRowHeader;
        private NotificationCollection<FieldMapData> fieldsMapData = new NotificationCollection<FieldMapData>();
        private string tableName;
        private MailMergeSourceType dataSourceType;
        private string udlConnectionString;
        private NotificationCollectionChangedListener<FieldMapData> fieldsMapDataListener;
        private EventHandler columnDelimiterChanged;
        private EventHandler firstRowHeaderChanged;
        private EventHandler tableNameChanged;
        private EventHandler dataSourceTypeChanged;
        private EventHandler udlConnectionStringChanged;
        private EventHandler fieldsMapDataChanged;

        public event EventHandler ColumnDelimiterChanged
        {
            add
            {
                this.columnDelimiterChanged += value;
            }
            remove
            {
                this.columnDelimiterChanged -= value;
            }
        }

        public event EventHandler DataSourceTypeChanged
        {
            add
            {
                this.dataSourceTypeChanged += value;
            }
            remove
            {
                this.dataSourceTypeChanged -= value;
            }
        }

        public event EventHandler FieldsMapDataChanged
        {
            add
            {
                this.fieldsMapDataChanged += value;
            }
            remove
            {
                this.fieldsMapDataChanged -= value;
            }
        }

        public event EventHandler FirstRowHeaderChanged
        {
            add
            {
                this.firstRowHeaderChanged += value;
            }
            remove
            {
                this.firstRowHeaderChanged -= value;
            }
        }

        public event EventHandler TableNameChanged
        {
            add
            {
                this.tableNameChanged += value;
            }
            remove
            {
                this.tableNameChanged -= value;
            }
        }

        public event EventHandler UdlConnectionStringChanged
        {
            add
            {
                this.udlConnectionStringChanged += value;
            }
            remove
            {
                this.udlConnectionStringChanged -= value;
            }
        }

        public DataSourceObjectProperties()
        {
            this.fieldsMapDataListener = new NotificationCollectionChangedListener<FieldMapData>(this.fieldsMapData);
            this.mapColumnName = new Dictionary<string, FieldMapData>();
            this.mapFieldMappedName = new Dictionary<string, FieldMapData>();
            this.SubscribeFieldsMapDataEvents();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.fieldsMapDataListener != null)
                {
                    this.UnsubscribeFieldsMapDataEvents();
                    this.fieldsMapDataListener.Dispose();
                }
                this.fieldsMapDataListener = null;
                this.fieldsMapData = null;
            }
        }

        public virtual FieldMapData FindMapDataByColumnName(string columnName)
        {
            FieldMapData data;
            return (!this.mapColumnName.TryGetValue(columnName, out data) ? null : data);
        }

        public virtual FieldMapData FindMapDataByMapName(string mappedFieldName)
        {
            FieldMapData data;
            return (!this.mapFieldMappedName.TryGetValue(mappedFieldName, out data) ? null : data);
        }

        protected virtual void OnFieldsMapDataChanged(object sender, EventArgs e)
        {
            int count = this.FieldsMapData.Count;
            for (int i = 0; i < count; i++)
            {
                FieldMapData data = this.FieldsMapData[i];
                if (!string.IsNullOrEmpty(data.MappedName))
                {
                    this.mapFieldMappedName.Add(data.MappedName, data);
                }
                if (!string.IsNullOrEmpty(data.ColumnName))
                {
                    this.mapColumnName.Add(data.ColumnName, data);
                }
            }
            this.RaiseFieldsMapDataChanged();
        }

        protected virtual void RaiseColumnDelimiterChanged()
        {
            if (this.columnDelimiterChanged != null)
            {
                this.columnDelimiterChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseDataSourceTypeChanged()
        {
            if (this.dataSourceTypeChanged != null)
            {
                this.dataSourceTypeChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseFieldsMapDataChanged()
        {
            if (this.fieldsMapDataChanged != null)
            {
                this.fieldsMapDataChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseFirstRowHeaderChanged()
        {
            if (this.firstRowHeaderChanged != null)
            {
                this.firstRowHeaderChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseTableNameChanged()
        {
            if (this.tableNameChanged != null)
            {
                this.tableNameChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseUdlConnectionStringChanged()
        {
            if (this.udlConnectionStringChanged != null)
            {
                this.udlConnectionStringChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void SubscribeFieldsMapDataEvents()
        {
            this.fieldsMapDataListener.Changed += new EventHandler(this.OnFieldsMapDataChanged);
        }

        protected virtual void UnsubscribeFieldsMapDataEvents()
        {
            this.fieldsMapDataListener.Changed -= new EventHandler(this.OnFieldsMapDataChanged);
        }

        public char ColumnDelimiter
        {
            get => 
                this.columnDelimiter;
            set
            {
                if (this.columnDelimiter != value)
                {
                    this.columnDelimiter = value;
                    this.RaiseColumnDelimiterChanged();
                }
            }
        }

        public bool FirstRowHeader
        {
            get => 
                this.firstRowHeader;
            set
            {
                if (this.firstRowHeader != value)
                {
                    this.firstRowHeader = value;
                    this.RaiseFirstRowHeaderChanged();
                }
            }
        }

        public string TableName
        {
            get => 
                this.tableName;
            set
            {
                if (this.tableName != value)
                {
                    this.tableName = value;
                    this.RaiseTableNameChanged();
                }
            }
        }

        public MailMergeSourceType DataSourceType
        {
            get => 
                this.dataSourceType;
            set
            {
                if (this.dataSourceType != value)
                {
                    this.dataSourceType = value;
                    this.RaiseDataSourceTypeChanged();
                }
            }
        }

        public string UdlConnectionString
        {
            get => 
                this.udlConnectionString;
            set
            {
                if (this.udlConnectionString != value)
                {
                    this.udlConnectionString = value;
                    this.RaiseUdlConnectionStringChanged();
                }
            }
        }

        public NotificationCollection<FieldMapData> FieldsMapData =>
            this.fieldsMapData;
    }
}

