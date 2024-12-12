namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;

    [ToolboxItem(true), aa("DbCommandBuilder_Description"), Designer("Devart.Data.MySql.Design.MySqlCommandBuilderDesigner, Devart.Data.MySql.Design")]
    public class MySqlCommandBuilder : DbCommandBuilder
    {
        private MySqlRowUpdatingEventHandler a;
        private bool[] b;

        public MySqlCommandBuilder();
        public MySqlCommandBuilder(MySqlDataAdapter adapter);
        private void a(RowUpdatingEventArgs A_0);
        private void a(object A_0, MySqlRowUpdatingEventArgs A_1);
        protected override void AddRefreshSql(IDbCommand command, StatementType statementType);
        protected override void AddRefreshSql(IDbCommand command, bool useColumnsForParameterNames, StatementType statementType);
        protected override void AddRefreshSql(IDbCommand command, string[] fields, bool useColumnsForParameterNames, StatementType statementType);
        protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause);
        public static void DeriveParameters(MySqlCommand command);
        protected override void Dispose(bool disposing);
        public MySqlCommand GetDeleteCommand();
        public MySqlCommand GetDeleteCommand(bool useColumnsForParameterNames);
        public MySqlCommand GetInsertCommand();
        public MySqlCommand GetInsertCommand(bool useColumnsForParameterNames);
        public MySqlCommand GetInsertCommand(string[] fields, bool useColumnsForParameterNames);
        protected override string GetLastInsertIdKeyword();
        protected override string GetParameterName(int parameterOrdinal);
        protected override string GetParameterName(string parameterName);
        protected override string GetParameterPlaceholder(int parameterOrdinal);
        public MySqlCommand GetRefreshCommand();
        public MySqlCommand GetRefreshCommand(bool useColumnsForParameterNames);
        public MySqlCommand GetRefreshCommand(string[] fields, bool useColumnsForParameterNames);
        protected override DataTable GetSchemaTable(DbCommand srcCommand);
        public MySqlCommand GetUpdateCommand();
        public MySqlCommand GetUpdateCommand(bool useColumnsForParameterNames);
        public MySqlCommand GetUpdateCommand(string[] fields, bool useColumnsForParameterNames);
        protected override bool IsValidQuote(string quote, bool prefix);
        public override string QuoteIdentifier(string unquotedIdentifier);
        protected override void SetRowUpdatingHandler(DbDataAdapter adapter);
        public override string UnquoteIdentifier(string quotedIdentifier);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override string CatalogSeparator { get; set; }

        [DefaultValue((string) null), aa("DbCommandBuilder_DataAdapter"), MergableProperty(false), Category("Update")]
        public MySqlDataAdapter DataAdapter { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override string SchemaSeparator { get; set; }

        [aa("DbCommandBuilder_KeyFields"), Category("Update"), MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlCommandBuilderKeyFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string KeyFields { get; set; }

        [aa("DbCommandBuilder_UpdatingFields"), Editor("Devart.Data.MySql.Design.MySqlCommandBuilderUpdatingFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Update"), MergableProperty(false)]
        public override string UpdatingFields { get; set; }

        [aa("DbCommandBuilder_UpdatingTable"), MergableProperty(false), Editor("Devart.Common.Design.CommandBuilderUpdatingTableEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Update")]
        public string UpdatingTable { get; set; }

        protected override char[] QuoteSymbols { get; }

        [MergableProperty(false), aa("DbCommandBuilder_RefreshingFields"), Category("Update"), Editor("Devart.Data.MySql.Design.MySqlCommandBuilderReturningFieldsEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public override string RefreshingFields { get; set; }
    }
}

