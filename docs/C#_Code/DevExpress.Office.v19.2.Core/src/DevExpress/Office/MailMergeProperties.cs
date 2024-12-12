namespace DevExpress.Office
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MailMergeProperties : IDisposable
    {
        public static readonly string DefaultFieldNameFormatString = "\x00ab{0}\x00bb";
        private int activeRecord;
        private DevExpress.Office.DataSourceObjectProperties dataSourceObjectProperties = new DevExpress.Office.DataSourceObjectProperties();
        private string eMailAddressColumnName;
        private string connectionString;
        private string dataSource;
        private MailMergeDataType dataType;
        private MailMergeDestination destination;
        private bool leaveBlankLines;
        private string query;
        private bool viewMergedData;
        private string fieldNameFormatString = DefaultFieldNameFormatString;
        private EventHandler activeRecordChanged;
        private EventHandler eMailAddressColumnNameChanged;
        private EventHandler connectionStringChanged;
        private EventHandler dataSourceChanged;
        private EventHandler dataTypeChanged;
        private EventHandler destinationChanged;
        private EventHandler leaveBlankLinesChanged;
        private EventHandler queryChanged;
        private EventHandler viewMergedDataChanged;

        public event EventHandler ActiveRecordChanged
        {
            add
            {
                this.activeRecordChanged += value;
            }
            remove
            {
                this.activeRecordChanged -= value;
            }
        }

        public event EventHandler ConnectionStringChanged
        {
            add
            {
                this.connectionStringChanged += value;
            }
            remove
            {
                this.connectionStringChanged -= value;
            }
        }

        public event EventHandler DataSourceChanged
        {
            add
            {
                this.dataSourceChanged += value;
            }
            remove
            {
                this.dataSourceChanged -= value;
            }
        }

        public event EventHandler DataTypeChanged
        {
            add
            {
                this.dataTypeChanged += value;
            }
            remove
            {
                this.dataTypeChanged -= value;
            }
        }

        public event EventHandler DestinationChanged
        {
            add
            {
                this.destinationChanged += value;
            }
            remove
            {
                this.destinationChanged -= value;
            }
        }

        public event EventHandler EMailAddressColumnNameChanged
        {
            add
            {
                this.eMailAddressColumnNameChanged += value;
            }
            remove
            {
                this.eMailAddressColumnNameChanged -= value;
            }
        }

        public event EventHandler FieldNameFormatStringChanged;

        public event EventHandler LeaveBlankLinesChanged
        {
            add
            {
                this.leaveBlankLinesChanged += value;
            }
            remove
            {
                this.leaveBlankLinesChanged -= value;
            }
        }

        public event EventHandler QueryChanged
        {
            add
            {
                this.queryChanged += value;
            }
            remove
            {
                this.queryChanged -= value;
            }
        }

        public event EventHandler ViewMergedDataChanged
        {
            add
            {
                this.viewMergedDataChanged += value;
            }
            remove
            {
                this.viewMergedDataChanged -= value;
            }
        }

        public void Dispose()
        {
            if (this.DataSourceObjectProperties != null)
            {
                this.dataSourceObjectProperties.Dispose();
                this.dataSourceObjectProperties = null;
            }
        }

        protected virtual void RaiseActiveRecordChanged()
        {
            if (this.activeRecordChanged != null)
            {
                this.activeRecordChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseConnectionStringChanged()
        {
            if (this.connectionStringChanged != null)
            {
                this.connectionStringChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseDataSourceChanged()
        {
            if (this.dataSourceChanged != null)
            {
                this.dataSourceChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseDataTypeChanged()
        {
            if (this.dataTypeChanged != null)
            {
                this.dataTypeChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseDestinationChanged()
        {
            if (this.destinationChanged != null)
            {
                this.destinationChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseEMailAddressColumnNameChanged()
        {
            if (this.eMailAddressColumnNameChanged != null)
            {
                this.eMailAddressColumnNameChanged(this, EventArgs.Empty);
            }
        }

        protected void RaiseFieldNameFormatStringChanged()
        {
            if (this.FieldNameFormatStringChanged == null)
            {
                EventHandler fieldNameFormatStringChanged = this.FieldNameFormatStringChanged;
            }
            else
            {
                this.FieldNameFormatStringChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseLeaveBlankLinesChanged()
        {
            if (this.leaveBlankLinesChanged != null)
            {
                this.leaveBlankLinesChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseQueryChanged()
        {
            if (this.queryChanged != null)
            {
                this.queryChanged(this, EventArgs.Empty);
            }
        }

        protected virtual void RaiseViewMergedDataChanged()
        {
            if (this.viewMergedDataChanged != null)
            {
                this.viewMergedDataChanged(this, EventArgs.Empty);
            }
        }

        public DevExpress.Office.DataSourceObjectProperties DataSourceObjectProperties =>
            this.dataSourceObjectProperties;

        public int ActiveRecord
        {
            get => 
                this.activeRecord;
            set
            {
                if (this.activeRecord != value)
                {
                    this.activeRecord = value;
                    this.RaiseActiveRecordChanged();
                }
            }
        }

        public string EMailAddressColumnName
        {
            get => 
                this.eMailAddressColumnName;
            set
            {
                if (this.eMailAddressColumnName != value)
                {
                    this.eMailAddressColumnName = value;
                    this.RaiseEMailAddressColumnNameChanged();
                }
            }
        }

        public string ConnectionString
        {
            get => 
                this.connectionString;
            set
            {
                if (this.connectionString != value)
                {
                    this.connectionString = value;
                    this.RaiseConnectionStringChanged();
                }
            }
        }

        public string DataSource
        {
            get => 
                this.dataSource;
            set
            {
                if (this.dataSource != value)
                {
                    this.dataSource = value;
                    this.RaiseDataSourceChanged();
                }
            }
        }

        public MailMergeDataType DataType
        {
            get => 
                this.dataType;
            set
            {
                if (this.dataType != value)
                {
                    this.dataType = value;
                    this.RaiseDataTypeChanged();
                }
            }
        }

        public MailMergeDestination Destination
        {
            get => 
                this.destination;
            set
            {
                if (this.destination != value)
                {
                    this.destination = value;
                    this.RaiseDestinationChanged();
                }
            }
        }

        public bool LeaveBlankLines
        {
            get => 
                this.leaveBlankLines;
            set
            {
                if (this.leaveBlankLines != value)
                {
                    this.leaveBlankLines = value;
                    this.RaiseLeaveBlankLinesChanged();
                }
            }
        }

        public string Query
        {
            get => 
                this.query;
            set
            {
                if (this.query != value)
                {
                    this.query = value;
                    this.RaiseQueryChanged();
                }
            }
        }

        public bool ViewMergedData
        {
            get => 
                this.viewMergedData;
            set
            {
                if (this.viewMergedData != value)
                {
                    this.viewMergedData = value;
                    this.RaiseViewMergedDataChanged();
                }
            }
        }

        public string FieldNameFormatString
        {
            get => 
                this.fieldNameFormatString;
            set
            {
                if (this.fieldNameFormatString != value)
                {
                    this.fieldNameFormatString = value;
                }
            }
        }
    }
}

