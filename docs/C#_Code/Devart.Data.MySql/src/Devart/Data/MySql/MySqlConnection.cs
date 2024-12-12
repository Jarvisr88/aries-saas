namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Timers;

    [ToolboxItem(true), Designer("Devart.Data.MySql.Design.MySqlConnectionDesigner, Devart.Data.MySql.Design"), aa("MySqlConnection_Description"), DesignTimeVisible(true), TypeConverter("Devart.Common.Design.ParentTypeConverter, Devart.Data.Design"), LicenseProvider(typeof(CRLicenseProvider)), DesignerSerializer("Devart.Data.MySql.Design.MySqlConnectionSerializer, Devart.Data.MySql.Design", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design")]
    public class MySqlConnection : DbConnectionBase, IDbConnection, ICloneable
    {
        private Devart.Data.MySql.SslOptions a;
        private Devart.Data.MySql.SshOptions b;
        private Devart.Common.ProxyOptions c;
        private MySqlHttpOptions d;
        private static readonly object e;
        private static readonly object f;
        private Timer g;
        private bool h;
        private bool i;
        internal int j;
        private List<string> k;
        private int l;
        private bool m;
        private bool n;
        private string o;
        internal MySqlTransaction p;

        [aa("DbConnection_AuthenticationPrompt")]
        public event MySqlAuthenticationPromptHandler AuthenticationPrompt;

        [aa("MySqlConnection_ConnectionLost")]
        public event ConnectionLostEventHandler ConnectionLost;

        [aa("MySqlConnection_Error")]
        public event MySqlErrorEventHandler Error;

        [aa("DbConnection_InfoMessage")]
        public event MySqlInfoMessageEventHandler InfoMessage;

        [aa("DbConnection_SshHostKeyConfirmation")]
        public event SshHostKeyConfirmationHandler SshHostKeyConfirmation;

        static MySqlConnection();
        public MySqlConnection();
        internal MySqlConnection(MySqlConnection A_0);
        public MySqlConnection(string connectionString);
        private bool a(out string A_0);
        private void a(bb A_0);
        private void a(bool A_0);
        internal void a(Exception A_0);
        private void a(string A_0);
        internal void a(bool A_0, bool A_1);
        private void a(object A_0, ElapsedEventArgs A_1);
        private void a(string A_0, object A_1);
        internal void a(string A_0, string A_1, string[] A_2, string[] A_3);
        internal void a(string A_0, string A_1, int A_2, string A_3, string A_4, string A_5, ref Devart.Common.SshHostKeyConfirmation A_6);
        private int b();
        private void b(bool A_0);
        internal void b(Exception A_0);
        private void b(string A_0);
        private void b(string A_0, object A_1);
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);
        public MySqlTransaction BeginTransaction();
        public MySqlTransaction BeginTransaction(IsolationLevel il);
        private aw c();
        internal MySqlInternalConnection c(bool A_0);
        private void c(string A_0);
        private void c(string A_0, object A_1);
        public override void ChangeDatabase(string value);
        public static void ClearAllPools();
        public static void ClearAllPools(bool forced);
        public static void ClearPool(MySqlConnection connection);
        public object Clone();
        public override void Close();
        public void Commit();
        public MySqlCommand CreateCommand();
        protected override DbCommand CreateDbCommand();
        private void d();
        private void d(string A_0, object A_1);
        protected override void Dispose(bool disposing);
        protected override void DoErrorEvent(Exception ex);
        private void e();
        private void e(string A_0, object A_1);
        private void f();
        private void g();
        private void h();
        protected override bool HasLocalFailoverRestriction();
        private void i();
        protected internal override bool InTransaction();
        protected override bool IsConnectionLostError(Exception e);
        private int j();
        private string k();
        public void Kill(MySqlConnection connection);
        private void l();
        private void m();
        private void n();
        private void o();
        public override void Open();
        internal void p();
        public bool Ping();
        internal void q();
        protected override void Reconnect();
        private void ResetHttpOptions();
        private void ResetLoadBalancingState();
        private void ResetProxyOptions();
        private void ResetSshOptions();
        private void ResetSslOptions();
        public void Rollback();
        internal void s();
        private bool ShouldSerializeHttpOptions();
        private bool ShouldSerializeProxyOptions();
        private bool ShouldSerializeSshOptions();
        private bool ShouldSerializeSslOptions();
        IDbCommand IDbConnection.CreateCommand();
        internal MySqlInternalConnection t();
        internal object v();

        [DefaultValue(false), RefreshProperties(RefreshProperties.Repaint), aa("MySqlConnection_LocalFailover"), Category("Initialization")]
        public bool LocalFailover { get; set; }

        internal aw Session { get; }

        protected override System.Data.Common.DbProviderFactory DbProviderFactory { get; }

        [DefaultValue(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), aa("MySqlConnection_UserId"), RefreshProperties(RefreshProperties.Repaint), Category("Security")]
        public string UserId { get; set; }

        [DefaultValue(""), Obsolete("Use UserId property instead User property"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string User { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), aa("MySqlConnection_Password"), RefreshProperties(RefreshProperties.Repaint), PasswordPropertyText(true), Category("Security"), DefaultValue("")]
        public string Password { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.Repaint), Editor("Devart.Data.MySql.Design.MySqlConnectionHostEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), Category("Source"), DefaultValue(""), aa("MySqlConnection_Host")]
        public string Host { get; set; }

        [Category("Initialization"), aa("MySqlConnection_Embedded"), DefaultValue(false), RefreshProperties(RefreshProperties.Repaint)]
        public bool Embedded { get; set; }

        [DefaultValue(true), Category("Initialization"), RefreshProperties(RefreshProperties.Repaint), aa("MySqlConnection_Direct")]
        public bool Direct { get; set; }

        [Category("Initialization"), DefaultValue(false), aa("MySqlConnection_Compress"), RefreshProperties(RefreshProperties.Repaint)]
        public bool Compress { get; set; }

        [DefaultValue(0), aa("MySqlConnection_Protocol"), Category("Initialization"), RefreshProperties(RefreshProperties.Repaint)]
        public MySqlProtocol Protocol { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Source"), aa("MySqlConnection_Port"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(0xcea)]
        public int Port { get; set; }

        [Category("Initialization"), DefaultValue(false), RefreshProperties(RefreshProperties.Repaint), aa("MySqlConnection_Unicode")]
        public bool Unicode { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), DefaultValue(""), MergableProperty(false), aa("MySqlConnectionString_CharSet"), Category("Initialization"), Editor("Devart.Data.MySql.Design.MySqlConnectionCharSetEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string Charset { get; set; }

        [Browsable(false)]
        public string ClientVersion { get; }

        [Obsolete("Use Host and Protocol properties instead of HostInfo property."), Browsable(false)]
        public string HostInfo { get; }

        [Obsolete("Do not use ProtocolInfo property because it always equals to \"10\"."), Browsable(false)]
        public string ProtocolInfo { get; }

        internal System.Text.Encoding Encoding { get; }

        [RefreshProperties(RefreshProperties.Repaint), aa("MySqlConnection_ConnectionString"), Category("Data"), DefaultValue(""), Editor("Devart.Data.MySql.Design.MySqlConnectionStringEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public override string ConnectionString { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Initialization"), DefaultValue(15), RefreshProperties(RefreshProperties.Repaint), aa("DbConnection_ConnectionTimeout")]
        public int ConnectionTimeout { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), aa("MySqlConnection_PingInterval"), DefaultValue(0), Category("Initialization"), RefreshProperties(RefreshProperties.Repaint)]
        public int PingInterval { get; set; }

        protected override int ConnectionTimeoutInternal { get; }

        protected override string DataSourceInternal { get; set; }

        protected override string DatabaseInternal { get; set; }

        [DefaultValue(""), aa("MySqlConnection_Database"), MergableProperty(false), Category("Source"), RefreshProperties(RefreshProperties.Repaint), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Editor("Devart.Data.MySql.Design.MySqlConnectionDatabaseEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public string Database { get; set; }

        [Category("Source"), aa("MySqlConnection_ServerVersion"), Browsable(true)]
        public string ServerVersion { get; }

        [Category("Design"), Browsable(true), RefreshProperties(RefreshProperties.Repaint), aa("DbConnection_State"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Editor("Devart.Common.Design.ConnectionStateEditor, Devart.Data.MySql.Design", "System.Drawing.Design.UITypeEditor, System.Drawing"), DefaultValue(0)]
        public ConnectionState State { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), aa("MySqlConnection_SshOptions"), Browsable(true), Category("Security")]
        public Devart.Data.MySql.SshOptions SshOptions { get; set; }

        [RefreshProperties(RefreshProperties.Repaint), Category("Security"), aa("MySqlConnection_SslOptions"), Browsable(true)]
        public Devart.Data.MySql.SslOptions SslOptions { get; set; }

        [aa("DbConnection_ProxyOptions"), Browsable(true), Category("Tunneling"), RefreshProperties(RefreshProperties.Repaint)]
        public Devart.Common.ProxyOptions ProxyOptions { get; set; }

        [Category("Tunneling"), aa("DbConnection_HttpOptions"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.Repaint)]
        public MySqlHttpOptions HttpOptions { get; set; }

        private bool EnableLoadBalancing { get; }

        [DefaultValue(""), Browsable(false)]
        public string Name { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Owner { get; set; }
    }
}

