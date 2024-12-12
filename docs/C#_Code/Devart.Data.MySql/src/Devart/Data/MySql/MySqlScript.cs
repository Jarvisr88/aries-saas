namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [DesignerSerializer("Devart.Data.MySql.Design.MySqlScriptSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), Designer("Devart.Data.MySql.Design.MySqlScriptDesigner, Devart.Data.MySql.Design"), ToolboxItem(true), aa("DbScript_Description")]
    public class MySqlScript : DbScript
    {
        private const string a = ";";
        private string b;
        private string c;
        private int d;
        private bool shouldSerializeConnection;

        public event MySqlStatementExecuteEventHandler MySqlStatementExecute;

        public MySqlScript();
        public MySqlScript(Stream stream);
        public MySqlScript(TextReader reader);
        public MySqlScript(string scriptText);
        public MySqlScript(Stream stream, MySqlConnection connection);
        public MySqlScript(TextReader reader, MySqlConnection connection);
        public MySqlScript(string scriptText, MySqlConnection connection);
        private static MySqlStatementType a(Token A_0);
        private bool a(string A_0);
        private object b(string A_0);
        private void c(string A_0);
        protected override bool CanExecuteStatement(SqlStatement sqlStatement);
        protected override SqlStatementCollection CreateStatementCollection();
        protected override void Dispose(bool disposing);
        protected override int GetDefaultCommandTimeout();
        protected override bool GetNextStatement(out SqlStatement stmt);
        protected override SqlStatementType GetStatementType(Token token);
        protected override void InitializeFromConnection();
        protected override void InternalReset();
        protected override SqlStatementStatus OnSqlStatementExecute(SqlStatement stmt, out IDataReader reader);
        public override void Reset();
        private bool ShouldSerializeConnection();

        [Category("Behavior"), RefreshProperties(RefreshProperties.Repaint), MergableProperty(false), aa("DbScript_Connection"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public MySqlConnection Connection { get; set; }

        [aa("MySqlScript_ScriptText"), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.MySqlScriptTextEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Data")]
        public override string ScriptText { get; set; }

        [aa("MySqlScript_Delimiter"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(";"), Category("Data")]
        public string Delimiter { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MySqlStatementCollection Statements { get; }
    }
}

