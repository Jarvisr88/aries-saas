namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    [aa("DbDump_Description"), ToolboxItem(true), DesignerSerializer("Devart.Data.MySql.Design.MySqlDumpSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), Designer("Devart.Data.MySql.Design.MySqlDumpDesigner, Devart.Data.MySql.Design")]
    public sealed class MySqlDump : DbDump
    {
        private bool a;
        private bool b;
        private bool c;
        private bool d;
        private bool e;
        private bool f;
        private int g;
        private int h;
        private bool i;
        private ArrayList j;
        private int k;
        private int l;
        private bool m;
        private int n;
        private bool o;
        private StringCollection p;
        private DataTable q;
        private DataTable r;
        private DataTable s;
        private DataTable t;
        private DataTable u;
        private DataTable v;
        private string w;
        private MySqlDumpObjects x;
        private const int y = 0xc3ba;
        private const int z = 0x4000;
        private const string aa = ";\r\n";
        private const string ab = ";\r\n/\r\n";
        private const string ac = "\r\n-- \r\n-- Definition for view `{0}`\r\n-- \r\n\r\n";
        private const string ad = "\r\n-- \r\n-- Definition for stored procedure `{0}`\r\n-- \r\n\r\n";
        private const string ae = "\r\n-- \r\n-- Definition for stored function `{0}`\r\n-- \r\n\r\n";
        private const string af = "\r\n-- \r\n-- User-defined function `{0}`\r\n-- \r\n\r\n";
        private const string ag = "\r\n-- \r\n-- Definition for trigger `{0}`\r\n-- \r\n\r\n";
        private const string ah = "\r\n-- \r\n-- Definition for event `{0}`\r\n-- \r\n\r\n";

        [aa("MySqlDump_Error")]
        public event ScriptErrorEventHandler Error;

        [aa("MySqlDump_Progress")]
        public event MySqlDumpProgressEventHandler Progress;

        public MySqlDump();
        public MySqlDump(MySqlConnection connection);
        private StringCollection a();
        private string a(MySqlDataReader A_0);
        private bool a(MySqlDumpObjects A_0);
        private bool a(int A_0);
        private int a(TextWriter A_0);
        private bool a(string A_0);
        private void a(object A_0, ScriptErrorEventArgs A_1);
        private void a(object A_0, ScriptProgressEventArgs A_1);
        private void a(byte[] A_0, StringBuilder A_1);
        private string a(string A_0, IDataReader A_1, int A_2);
        private void a(DataTable A_0, string A_1, string A_2, string A_3, TextWriter A_4);
        private void a(string A_0, string A_1, int A_2, long A_3, long A_4);
        private void a(MySqlDataReader A_0, string A_1, string A_2, long A_3, bool A_4, TextWriter A_5);
        private void a(IDbCommand A_0, DataTable A_1, string A_2, string A_3, string A_4, int A_5, string A_6, string A_7, TextWriter A_8);
        private ArrayList b();
        private void b(TextWriter A_0);
        private string b(string A_0);
        private string c();
        private void c(TextWriter A_0);
        private void d(TextWriter A_0);
        private void e(TextWriter A_0);
        private void f(TextWriter A_0);
        private void g(TextWriter A_0);
        private void h(TextWriter A_0);
        private void i(TextWriter A_0);
        protected override void InternalBackup(TextWriter writer);
        protected override void InternalBackupQuery(TextWriter writer, string query);
        protected override void InternalRestore(TextReader reader);
        private void j(TextWriter A_0);

        [Category("Behavior"), aa("DbDump_Connection"), MergableProperty(false), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public MySqlConnection Connection { get; set; }

        [MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlDumpTablesEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Data"), aa("DbDump_Tables"), DefaultValue("")]
        public override string Tables { get; set; }

        [MergableProperty(false), aa("DbDump_DumpText"), Category("Data"), Editor("Devart.Data.MySql.Design.MySqlDumpTextEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), DefaultValue("")]
        public override string DumpText { get; set; }

        [DefaultValue(""), MergableProperty(false), aa("MySqlDump_Database"), Editor("Devart.Data.MySql.Design.MySqlDumpDatabaseEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Data")]
        public string Database { get; set; }

        [DefaultValue(false), aa("MySqlDump_IncludeLock"), Category("Options")]
        public bool IncludeLock { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false), Obsolete("SchemaOnly property is now obsolete. Use Mode property instead.")]
        public bool SchemaOnly { get; set; }

        [DefaultValue(false), Category("Options"), aa("MySqlDump_IncludeUsers"), Obsolete("This property is obsolete. Use ObjectTypes property instead.")]
        public bool IncludeUsers { get; set; }

        [aa("MySqlDump_IncludeDatabase"), Category("Options"), DefaultValue(false), Obsolete("This property is obsolete. Use ObjectTypes property instead.")]
        public bool IncludeDatabase { get; set; }

        [DefaultValue(false), Category("Options"), aa("MySqlDump_DisableKeys")]
        public bool DisableKeys { get; set; }

        [aa("MySqlDump_UseExtSyntax"), Category("Options"), DefaultValue(false)]
        public bool UseExtSyntax { get; set; }

        [aa("MySqlDump_CommitCount"), DefaultValue(0), Category("Options")]
        public int CommitCount { get; set; }

        [DefaultValue(false), aa("MySqlDump_UseDelayedIns"), Category("Options")]
        public bool UseDelayedIns { get; set; }

        [DefaultValue(false), aa("MySqlDump_ExportAll"), Category("Options"), Obsolete("This property is obsolete. Use ObjectTypes property instead.")]
        public bool ExportAll { get; set; }

        [DefaultValue(0x10), aa("MySqlDump_ObjectTypes"), Category("Options"), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Common.Design.EnumListEditor, Devart.Data.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public MySqlDumpObjects ObjectTypes { get; set; }

        [DefaultValue(false), aa("MySqlDump_HexBlob"), Category("Options")]
        public bool HexBlob { get; set; }

        [Category("Options"), aa("MySqlDump_IcludeUse"), DefaultValue(true)]
        public bool IncludeUse { get; set; }

        [aa("MySqlDump_CommitBatchSize"), DefaultValue(0x4000), Category("Options")]
        public int CommitBatchSize { get; set; }

        [Category("Options"), aa("MySqlDump_IncludeDatabaseUsersOnly"), DefaultValue(false)]
        public bool IncludeDatabaseUsersOnly { get; set; }

        protected override System.Text.Encoding Encoding { get; }
    }
}

