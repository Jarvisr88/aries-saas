namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    [TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), ToolboxItem(true), aa("DbCommand_Description"), DesignerSerializer("Devart.Data.MySql.Design.MySqlCommandSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), Designer("Devart.Data.MySql.Design.MySqlCommandDesigner, Devart.Data.MySql.Design")]
    public class MySqlCommand : DbCommandBase, ICloneable
    {
        private MySqlConnection a;
        private MySqlParameterCollection b;
        private bool c;
        private MySqlParameter d;
        private long e;
        private bool f;
        private bool g;
        private bool shouldSerializeConnection;
        private bool h;
        private bool z;

        public MySqlCommand();
        public MySqlCommand(string commandText);
        public MySqlCommand(string commandText, MySqlConnection connection);
        public MySqlCommand(string commandText, MySqlTransaction transaction);
        public MySqlCommand(string commandText, MySqlConnection connection, MySqlTransaction transaction);
        internal void a();
        private void a(MySqlDataReader A_0);
        internal void a(MySqlParameterCollection A_0);
        private void a(bool A_0);
        private string a(string A_0);
        protected override void AddCommand();
        protected override void AddDataReader(DbDataReader reader);
        internal void b();
        internal string c();
        public override void Cancel();
        protected override void ClearParameters();
        public MySqlCommand Clone();
        protected override DbParameter CreateDbParameter();
        public MySqlParameter CreateParameter();
        protected override string CreateStoredProcSql(string name);
        internal bool d();
        protected override void DescribeProcedure(string name);
        public MySqlDataReader EndExecuteReader(IAsyncResult result);
        public MySqlDataReader ExecutePageReader(CommandBehavior behavior, int startRecord, int maxRecords);
        protected override DbDataReader ExecutePageReaderInternal(CommandBehavior behavior, int startRecord, int maxRecords);
        public MySqlDataReader ExecuteReader();
        public MySqlDataReader ExecuteReader(CommandBehavior behavior);
        protected override DbDataReader InternalExecute(CommandBehavior behavior, IDisposable stmt, int startRecord, int maxRecords);
        protected override IDisposable InternalPrepare(bool implicitPrepare, int startRecord, int maxRecords);
        protected override bool IsReadOnlyOperation(IDisposable stmt);
        protected override void ParseSqlParameters(string sql);
        public override void Prepare();
        protected override void RemoveCommand();
        private bool ShouldSerializeConnection();
        internal bool ShouldSerializeParameters();
        object ICloneable.Clone();
        protected override void Unprepare();
        protected override void UseLoadBalancing(bool ignoreBalancing);

        [aa("DbCommand_Parameters"), Editor("Devart.Data.MySql.Design.MySqlCommandParametersEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), MergableProperty(false)]
        public MySqlParameterCollection Parameters { get; }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue(false), aa("MySqlCommand_FetchAll"), Category("Behavior")]
        public bool FetchAll { get; set; }

        [Browsable(false)]
        public long InsertId { get; }

        [Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), aa("DbCommand_Connection"), RefreshProperties(RefreshProperties.Repaint), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Category("Behavior"), MergableProperty(false)]
        public MySqlConnection Connection { get; set; }

        [aa("DbCommand_CommandText"), DefaultValue(""), Category("Data"), MergableProperty(false), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.MySqlCommandTextEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public override string CommandText { get; set; }

        [aa("DbCommand_CommandType"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(1), Category("Data")]
        public override System.Data.CommandType CommandType { get; set; }

        public override int CommandTimeout { get; set; }

        protected override System.Data.Common.DbConnection DbConnection { get; set; }

        protected override System.Data.Common.DbParameterCollection DbParameterCollection { get; }

        protected override System.Data.Common.DbTransaction DbTransaction { get; set; }

        internal System.Text.Encoding Encoding { get; }

        internal aw Session { get; }

        internal bool MultiExecution { get; }

        protected override ILocalFailoverManager LocalFailoverManager { get; }

        internal class a
        {
            public static Hashtable a;
            public static Hashtable b;
            public const int c = 0x3e9;
            public const int d = 0x3ea;
            public const int e = 0x3eb;

            static a();
        }
    }
}

