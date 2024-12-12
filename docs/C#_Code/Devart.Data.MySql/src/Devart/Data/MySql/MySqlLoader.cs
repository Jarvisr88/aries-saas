namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.IO;
    using System.Text;

    [DesignerSerializer("Devart.Data.MySql.Design.MySqlLoaderSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design"), Designer("Devart.Data.MySql.Design.MySqlLoaderDesigner, Devart.Data.MySql.Design"), ToolboxItem(true), aa("DbLoader_Description")]
    public sealed class MySqlLoader : DbLoader
    {
        private MemoryStream a;
        private object[] b;
        private Encoding c;
        private int d;
        private bool e;
        private bool f;
        private int g;
        private bool h;
        private bool i;
        private bool shouldSerializeConnection;
        internal const string j = "yyyy-MM-dd";
        internal const string k = "HH:mm:ss";
        internal const string l = "yyyy-MM-dd HH:mm:ss";
        internal const string m = "yyyyMMddHHmmss";

        public MySqlLoader();
        public MySqlLoader(string tableName);
        public MySqlLoader(string tableName, MySqlConnection connection);
        private void a(bool A_0);
        private void a(Exception A_0);
        private void a(string A_0);
        private string a(MySqlLoaderOptions A_0, bool A_1);
        private int a(Stream A_0, MySqlLoaderOptions A_1, bool A_2);
        public override void Close();
        protected override DbLoaderColumn CreateColumn(string name, Type type);
        public override void CreateColumns();
        private void d();
        protected override void Dispose(bool disposing);
        private void e();
        internal void f();
        internal void g();
        protected override DbLoaderColumnCollection InitColumns();
        public void LoadDataPlanText(Stream stream);
        public void LoadDataPlanText(string file);
        public void LoadDataPlanText(Stream stream, MySqlLoaderOptions options);
        public void LoadDataPlanText(string file, MySqlLoaderOptions options);
        public void LoadDataXml(Stream stream);
        public void LoadDataXml(string file);
        public void LoadDataXml(Stream stream, MySqlLoaderOptions options);
        public void LoadDataXml(string file, MySqlLoaderOptions options);
        public override void NextRow();
        public override void Open();
        protected override string QuoteIfNeed(string name);
        private void ResetColumns();
        public void SetDefault(int i);
        public void SetDefault(string name);
        public override void SetValue(int i, object value);
        private bool ShouldSerializeColumns();
        private bool ShouldSerializeConnection();
        protected override string UnQuote(string name);

        private bool BinaryAsGuid { get; }

        private bool CharAsGuid { get; }

        [aa("DbLoader_Columns"), MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")]
        public MySqlLoaderColumnCollection Columns { get; }

        [MergableProperty(false), Editor("Devart.Data.MySql.Design.MySqlConnectionEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Behavior"), aa("DbLoader_Connection"), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), RefreshProperties(RefreshProperties.Repaint)]
        public MySqlConnection Connection { get; set; }

        [DefaultValue(false), Category("Options"), aa("MySqlLoader_Lock")]
        public bool Lock { get; set; }

        [Category("Options"), aa("MySqlLoader_Delayed"), DefaultValue(true)]
        public bool Delayed { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), aa("DbLoader_TableName"), Editor("Devart.Data.MySql.Design.MySqlLoaderTableNameEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Data")]
        public string TableName { get; set; }

        public bool UseTransaction { get; set; }

        private bool CanUseLock { get; }
    }
}

