namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Runtime.Serialization;

    [Serializable, ToolboxItem(true), aa("DbDataTable_Description"), DesignerSerializer("Devart.Data.MySql.Design.MySqlDataTableSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), Designer("Devart.Data.MySql.Design.MySqlDataTableDesigner, Devart.Data.MySql.Design"), DesignTimeVisible(true)]
    public class MySqlDataTable : DbDataTable
    {
        private MySqlRowUpdatedEventHandler a;
        private MySqlRowUpdatingEventHandler b;
        private static bool c;
        private bool d;
        private MemberInfo e;
        private bool shouldSerializeConnection;
        private bool shouldSerializeSelectCommand;
        private bool shouldSerializeUpdateCommand;
        private bool shouldSerializeInsertCommand;
        private bool shouldSerializeDeleteCommand;

        [aa("DbDataAdapter_RowUpdated")]
        public event MySqlRowUpdatedEventHandler RowUpdated;

        [aa("DbDataAdapter_RowUpdating")]
        public event MySqlRowUpdatingEventHandler RowUpdating;

        static MySqlDataTable();
        public MySqlDataTable();
        public MySqlDataTable(MySqlCommand selectCommand);
        public MySqlDataTable(string tableName);
        public MySqlDataTable(MySqlCommand selectCommand, MySqlConnection connection);
        protected MySqlDataTable(SerializationInfo A_0, StreamingContext A_1);
        public MySqlDataTable(string selectCommandText, MySqlConnection connection);
        public MySqlDataTable(string selectCommandText, string connectionString);
        private void a(ref MemberInfo A_0);
        private void a(object A_0, MySqlRowUpdatedEventArgs A_1);
        private void a(object A_0, MySqlRowUpdatingEventArgs A_1);
        protected override string AddWhere(string commandText, string whereText);
        protected override void ColumnAdded(DataColumn column, DataRow schemaRow, int index);
        protected override void CreateDataAdapter();
        protected override void DecrementAutoIncrementCurrent(DataRow row);
        protected override void FetchCompleted(DataTable schemaTable);
        protected override int FillPage(DbCommand command, int startRecord, int maxRecords);
        protected override void OpenInternal(IDataReader reader);
        private void ResetTableMapping();
        protected override void SetOwner(object value);
        private bool ShouldSerializeConnection();
        private bool ShouldSerializeDeleteCommand();
        private bool ShouldSerializeInsertCommand();
        private bool ShouldSerializeSelectCommand();
        private bool ShouldSerializeTableMapping();
        private bool ShouldSerializeUpdateCommand();

        [aa("DbTable_Connection"), MergableProperty(false), Category("Behavior"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.Repaint)]
        public MySqlConnection Connection { get; set; }

        [aa("DbTable_SelectCommand"), Editor("Devart.Data.MySql.Design.MySqlDataTableMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.Repaint), Category("Fill"), MergableProperty(false), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design")]
        public MySqlCommand SelectCommand { get; set; }

        [Editor("Devart.Data.MySql.Design.MySqlDataTableMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("DbTable_InsertCommand"), RefreshProperties(RefreshProperties.Repaint), MergableProperty(false), Category("Update"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design")]
        public MySqlCommand InsertCommand { get; set; }

        [Category("Update"), MergableProperty(false), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.MySqlDataTableMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), aa("DbTable_UpdateCommand")]
        public MySqlCommand UpdateCommand { get; set; }

        [TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), RefreshProperties(RefreshProperties.Repaint), aa("DbTable_DeleteCommand"), Editor("Devart.Data.MySql.Design.MySqlDataTableMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false), Category("Update")]
        public MySqlCommand DeleteCommand { get; set; }

        [aa("DbTable_FetchAll"), DefaultValue(true)]
        public override bool FetchAll { get; set; }

        [Category("Mapping"), MergableProperty(false), aa("DbTable_TableMapping"), TypeConverter("Devart.Common.Design.DataTableMappingConverter, Devart.Data.Design"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Editor("Devart.Data.MySql.Design.MySqlDataTableMappingEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public DataTableMapping TableMapping { get; }

        [Category("Update"), MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlDataTableKeyFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string UpdatingKeyFields { get; set; }

        [Editor("Devart.Data.MySql.Design.MySqlDataTableUpdatingFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Update"), MergableProperty(false)]
        public string UpdatingFields { get; set; }

        [Editor("Devart.Data.MySql.Design.MySqlDataTableReturningFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("DbDataTable_RefreshingFields"), Category("Update")]
        public string RefreshingFields { get; set; }

        [Editor("Devart.Common.Design.DbDataTableUpdatingTableEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("DbDataTable_UpdatingTable"), MergableProperty(false), Category("Update"), DefaultValue("")]
        public string UpdatingTable { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DefaultValue(true)]
        public static bool StaticRetrieveAutoIncrementSeed { get; set; }
    }
}

