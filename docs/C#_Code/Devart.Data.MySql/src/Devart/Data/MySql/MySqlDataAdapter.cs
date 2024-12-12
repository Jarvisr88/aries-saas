namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.InteropServices;

    [Designer("Devart.Data.MySql.Design.MySqlDataAdapterDesigner, Devart.Data.MySql.Design"), DesignerSerializer("Devart.Data.MySql.Design.MySqlDataAdapterSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), aa("DbDataAdapter_Description"), DesignTimeVisible(true), DesignerSerializer("Devart.Data.MySql.Design.MySqlDataAdapterSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), ToolboxItem(true), DefaultEvent("RowUpdated")]
    public class MySqlDataAdapter : DbDataAdapter, IDbDataAdapter
    {
        private MySqlCommand a;
        private MySqlCommand b;
        private MySqlCommand c;
        private MySqlCommand d;
        private DataColumn e;
        private static bool f;
        private bool shouldSerializeSelectCommand;
        private bool shouldSerializeInsertCommand;
        private bool shouldSerializeUpdateCommand;
        private bool shouldSerializeDeleteCommand;
        private ArrayList g;
        private int h;
        private static readonly object i;
        private static readonly object j;
        private static readonly object k;
        private static readonly object l;

        [aa("DbDataAdapter_RowUpdated")]
        public event MySqlRowUpdatedEventHandler RowUpdated;

        internal event MySqlRowUpdatedEventHandler RowUpdatedInternal;

        [aa("DbDataAdapter_RowUpdating")]
        public event MySqlRowUpdatingEventHandler RowUpdating;

        internal event MySqlRowUpdatingEventHandler RowUpdatingInternal;

        static MySqlDataAdapter();
        public MySqlDataAdapter();
        public MySqlDataAdapter(MySqlCommand selectCommand);
        public MySqlDataAdapter(string selectCommandText, MySqlConnection selectConnection);
        public MySqlDataAdapter(string selectCommandText, string selectConnectionString);
        private MySqlConnection a();
        private string a(MySqlDataReader A_0, out string A_1, out string A_2);
        private int a(DataTable A_0, IDataReader A_1, bool A_2);
        private int a(DataSet A_0, string A_1, IDataReader A_2, int A_3, int A_4);
        internal static void a(DataTable A_0, MySqlDataReader A_1, DataTableMapping A_2, MissingSchemaAction A_3, bool A_4);
        private int a(DataTable[] A_0, MySqlDataReader A_1, DataSet A_2, string A_3, int A_4, int A_5);
        private int a(DataTable[] A_0, IDataReader A_1, DataSet A_2, string A_3, int A_4, int A_5);
        protected override int AddToBatch(IDbCommand command);
        internal int b(DataTable A_0, IDataReader A_1, bool A_2);
        protected override void ClearBatch();
        protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);
        protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping);
        protected override void Dispose(bool disposing);
        protected override int ExecuteBatch();
        protected override int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords);
        protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords);
        protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex);
        protected override void InitializeBatching();
        protected override void OnRowUpdated(RowUpdatedEventArgs value);
        protected override void OnRowUpdating(RowUpdatingEventArgs value);
        private void ResetTableMappings();
        private bool ShouldSerializeDeleteCommand();
        private bool ShouldSerializeInsertCommand();
        private bool ShouldSerializeSelectCommand();
        private bool ShouldSerializeTableMappings();
        private bool ShouldSerializeUpdateCommand();
        protected override void TerminateBatching();

        [DefaultValue(1), Browsable(true)]
        public override int UpdateBatchSize { get; set; }

        IDbCommand IDbDataAdapter.SelectCommand { get; set; }

        [aa("DbDataAdapter_SelectCommand"), RefreshProperties(RefreshProperties.Repaint), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Editor("Devart.Data.MySql.Design.MySqlDataAdapterMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false), Category("Fill")]
        public MySqlCommand SelectCommand { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), MergableProperty(false), Editor("Devart.Common.Design.DataTableMappingCollectionEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public DataTableMappingCollection TableMappings { get; }

        IDbCommand IDbDataAdapter.InsertCommand { get; set; }

        [Editor("Devart.Data.MySql.Design.MySqlDataAdapterMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), RefreshProperties(RefreshProperties.Repaint), aa("DbDataAdapter_InsertCommand"), MergableProperty(false), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Category("Update")]
        public MySqlCommand InsertCommand { get; set; }

        IDbCommand IDbDataAdapter.UpdateCommand { get; set; }

        [aa("DbDataAdapter_UpdateCommand"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Editor("Devart.Data.MySql.Design.MySqlDataAdapterMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false), Category("Update"), RefreshProperties(RefreshProperties.Repaint)]
        public MySqlCommand UpdateCommand { get; set; }

        IDbCommand IDbDataAdapter.DeleteCommand { get; set; }

        [aa("DbDataAdapter_DeleteCommand"), Editor("Devart.Data.MySql.Design.MySqlDataAdapterMySqlCommandEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), MergableProperty(false), Category("Update"), RefreshProperties(RefreshProperties.Repaint), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design")]
        public MySqlCommand DeleteCommand { get; set; }

        [DefaultValue(true), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public static bool RetrieveAutoIncrementSeed { get; set; }
    }
}

