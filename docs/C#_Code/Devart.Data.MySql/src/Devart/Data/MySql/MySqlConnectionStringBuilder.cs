namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class MySqlConnectionStringBuilder : DbConnectionStringBuilder
    {
        private bool pooling;
        private int minPoolSize;
        private int maxPoolSize;
        private int connectionLifetime;
        private string userId;
        private string password;
        private string host;
        private string database;
        private int port;
        private int connectionTimeout;
        private int defaultCommandTimeout;
        private bool defaultFetchAll;
        private int pingInterval;
        private bool enlist;
        private bool transactionScopeLocal;
        private string pipeName;
        private bool direct;
        private bool compress;
        private bool unicode;
        private MySqlProtocol protocol;
        private string serverParameters;
        private bool persistSecurityInfo;
        private bool clientInteractive;
        private string charset;
        private bool binaryAsGuid;
        private bool charAsGuid;
        private bool tinyAsBoolean;
        private bool validateConnection;
        private bool foundRows;
        private bool disableCharsetSending;
        private bool ignoreFractionalSeconds;
        private int keepAlive;
        private bool ignorePrepare;
        private string sqlModes;
        private bool embedded;
        private string sshHost;
        private int sshPort;
        private string sshUser;
        private string sshPassword;
        private string sshCipherList;
        private string sshPrivateKey;
        private string sshPassphrase;
        private Devart.Common.SshAuthenticationType sshAuthenticationType;
        private string sshHostKey;
        private bool sshStrictHostKeyChecking;
        private string sslCipherList;
        private string sslCACert;
        private string sslCert;
        private string sslKey;
        private string sslTlsProtocol;
        private ProxyOptions proxyOptions;
        private string pluginDir;
        private string defaultAuthPlugin;
        private MySqlHttpOptions httpOptions;
        private static readonly string[] validKeywords;
        private static readonly Hashtable keywords;

        static MySqlConnectionStringBuilder();
        public MySqlConnectionStringBuilder();
        public MySqlConnectionStringBuilder(string connectionString);
        public override void Clear();
        internal void ClearPropertyDescriptors();
        public override bool ContainsKey(string keyword);
        internal static MySqlProtocol ConvertToProtocol(object value);
        internal static Devart.Common.SshAuthenticationType ConvertToSshAuthenticationType(object value);
        public override bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder, bool loginOnly);
        private object GetAt(MySqlConnectionStringBuilder.Keywords index);
        private Attribute[] GetAttributeArrayFromCollection(AttributeCollection collection);
        private MySqlConnectionStringBuilder.Keywords GetIndex(string keyword);
        protected override void GetProperties(Hashtable propertyDescriptors);
        public override bool Remove(string keyword);
        private void Reset(MySqlConnectionStringBuilder.Keywords index);
        public override bool ShouldSerialize(string keyword);
        public override bool TryGetValue(string keyword, out object value);

        public override ICollection Keys { get; }

        public override ICollection Values { get; }

        public override object this[string keyword] { get; set; }

        [Browsable(false)]
        public override bool IsFixedSize { get; }

        [DisplayName("Pooling"), aa("DbConnectionString_Pooling"), RefreshProperties(RefreshProperties.All), Category("Pooling")]
        public bool Pooling { get; set; }

        [DisplayName("Min Pool Size"), aa("DbConnectionString_MinPoolSize"), RefreshProperties(RefreshProperties.All), Category("Pooling")]
        public int MinPoolSize { get; set; }

        [Category("Pooling"), DisplayName("Max Pool Size"), RefreshProperties(RefreshProperties.All), aa("DbConnectionString_MaxPoolSize")]
        public int MaxPoolSize { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("DbConnectionString_ConnectionLifetime"), DisplayName("Connection Lifetime"), Category("Pooling")]
        public int ConnectionLifetime { get; set; }

        [aa("DbConnectionString_UserId"), Category("Security"), RefreshProperties(RefreshProperties.All), DisplayName("User Id")]
        public string UserId { get; set; }

        [PasswordPropertyText(true), DisplayName("Password"), RefreshProperties(RefreshProperties.All), Category("Security"), aa("DbConnectionString_Password")]
        public string Password { get; set; }

        [DisplayName("Host"), Editor("Devart.Data.MySql.Design.MySqlConnectionHostEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Category("Source"), aa("DbConnectionString_Host"), RefreshProperties(RefreshProperties.All)]
        public string Host { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Server { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("DbConnectionString_Database"), Category("Source"), Editor("Devart.Data.MySql.Design.MySqlConnectionDatabaseEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DisplayName("Database")]
        public string Database { get; set; }

        [aa("DbConnectionString_Port"), RefreshProperties(RefreshProperties.All), Category("Source"), DisplayName("Port")]
        public int Port { get; set; }

        [DisplayName("Connection Timeout"), aa("DbConnectionString_ConnectionTimeout"), Category("Initialization"), RefreshProperties(RefreshProperties.All)]
        public int ConnectionTimeout { get; set; }

        [aa("MySqlConnectionString_DefaultCommandTimeout"), Category("Initialization"), RefreshProperties(RefreshProperties.All), DisplayName("Default Command Timeout")]
        public int DefaultCommandTimeout { get; set; }

        [DisplayName("Default FetchAll"), Category("Initialization"), aa("MySqlConnectionString_DefaultFetchAll"), RefreshProperties(RefreshProperties.All)]
        public bool DefaultFetchAll { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("MySqlConnection_PingInterval"), Category("Initialization"), DisplayName("Ping Interval")]
        public int PingInterval { get; set; }

        [Category("Initialization"), aa("DbConnectionString_Enlist"), RefreshProperties(RefreshProperties.All), DisplayName("Enlist")]
        public bool Enlist { get; set; }

        [RefreshProperties(RefreshProperties.All), Category("Initialization"), DisplayName("Transaction Scope Local"), aa("DbConnectionString_TransactionScopeLocal")]
        public bool TransactionScopeLocal { get; set; }

        [Category("Initialization"), aa("MySqlConnectionString_PipeName"), DisplayName("Pipe Name"), RefreshProperties(RefreshProperties.All), DefaultValue("MySQL")]
        public string PipeName { get; set; }

        [DisplayName("Embedded"), aa("DbConnectionString_Embedded"), RefreshProperties(RefreshProperties.All), Category("Initialization")]
        public bool Embedded { get; set; }

        [aa("DbConnectionString_Direct"), DisplayName("Direct"), Category("Initialization"), RefreshProperties(RefreshProperties.All)]
        public bool Direct { get; set; }

        [DisplayName("Compress"), RefreshProperties(RefreshProperties.All), aa("DbConnectionString_Compress"), Category("Initialization")]
        public bool Compress { get; set; }

        [Category("Initialization"), RefreshProperties(RefreshProperties.All), DisplayName("Protocol"), aa("DbConnectionString_Protocol")]
        public MySqlProtocol Protocol { get; set; }

        [aa("DbConnectionString_Unicode"), DisplayName("Unicode"), RefreshProperties(RefreshProperties.All), Category("Initialization")]
        public bool Unicode { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("MySqlConnectionString_CharSet"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderCharSetEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DisplayName("Character Set"), Category("Initialization")]
        public string Charset { get; set; }

        [DisplayName("Binary As Guid"), aa("MySqlConnectionString_BinaryAsGuid"), RefreshProperties(RefreshProperties.All), Category("Initialization")]
        public bool BinaryAsGuid { get; set; }

        [aa("MySqlConnectionString_CharAsGuid"), RefreshProperties(RefreshProperties.All), Category("Initialization"), DisplayName("Char As Guid")]
        public bool CharAsGuid { get; set; }

        [Category("Initialization"), aa("MySqlConnectionString_TinyAsBoolean"), DisplayName("Tiny As Boolean"), RefreshProperties(RefreshProperties.All)]
        public bool TinyAsBoolean { get; set; }

        [DisplayName("Validate Connection"), RefreshProperties(RefreshProperties.All), aa("DbConnectionString_ValidateConnection"), Category("Initialization")]
        public bool ValidateConnection { get; set; }

        [RefreshProperties(RefreshProperties.All), Category("Initialization"), aa("MySqlConnectionString_FoundRows"), DisplayName("Found Rows"), DefaultValue(false)]
        public bool FoundRows { get; set; }

        [DisplayName("Disable Charset Sending"), Category("Initialization"), DefaultValue(false), aa("MySqlConnectionString_DisableCharsetSending"), RefreshProperties(RefreshProperties.All)]
        public bool DisableCharsetSending { get; set; }

        [RefreshProperties(RefreshProperties.All), DefaultValue(false), aa("MySqlConnectionString_IgnoreFractionalSeconds"), DisplayName("Ignore Fractional Seconds"), Category("Initialization")]
        public bool IgnoreFractionalSeconds { get; set; }

        [Category("Initialization"), DisplayName("Keep Alive"), RefreshProperties(RefreshProperties.All), DefaultValue(0), aa("MySqlConnectionString_KeepAlive")]
        public int KeepAlive { get; set; }

        [DisplayName("Ignore Prepare"), RefreshProperties(RefreshProperties.All), DefaultValue(false), aa("MySqlConnectionString_IgnorePrepare"), Category("Initialization")]
        public bool IgnorePrepare { get; set; }

        [DisplayName("Sql Modes"), aa("MySqlConnectionString_SqlModes"), Category("Initialization"), RefreshProperties(RefreshProperties.All), DefaultValue("")]
        public string SqlModes { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("MySqlConnectionString_ServerParameters"), DisplayName("Server Parameters"), Category("Initialization")]
        public string ServerParameters { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("SshOptions_Host"), DisplayName("SSH Host"), Category("SSH")]
        public string SshHost { get; set; }

        [DisplayName("SSH Port"), Category("SSH"), aa("SshOptions_Port"), RefreshProperties(RefreshProperties.All)]
        public int SshPort { get; set; }

        [Category("SSH"), RefreshProperties(RefreshProperties.All), DisplayName("SSH User"), aa("SshOptions_User")]
        public string SshUser { get; set; }

        [DisplayName("SSH Password"), PasswordPropertyText(true), aa("SshOptions_Password"), Category("SSH"), RefreshProperties(RefreshProperties.All)]
        public string SshPassword { get; set; }

        [RefreshProperties(RefreshProperties.All), Category("SSH"), DisplayName("SSH Cipher List"), aa("SshOptions_CipherList")]
        public string SshCipherList { get; set; }

        [Category("SSH"), aa("SshOptions_PrivateKey"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderSshPrivateKeyEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.All), DisplayName("SSH Private Key")]
        public string SshPrivateKey { get; set; }

        [Category("SSH"), DisplayName("SSH Passphrase"), RefreshProperties(RefreshProperties.All), aa("SshOptions_Passphrase"), PasswordPropertyText(true)]
        public string SshPassphrase { get; set; }

        [DisplayName("SSH Authentication Type"), aa("SshOptions_AuthenticationType"), Category("SSH"), RefreshProperties(RefreshProperties.All)]
        public Devart.Common.SshAuthenticationType SshAuthenticationType { get; set; }

        [aa("SshOptions_HostKey"), DisplayName("SSH Host Key"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderSshHostKeyEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.All), Category("SSH")]
        public string SshHostKey { get; set; }

        [aa("SshOptions_Strict_Host_Key_Checking"), DisplayName("SSH Strict Host Key Checking"), RefreshProperties(RefreshProperties.All), Category("SSH")]
        public bool SshStrictHostKeyChecking { get; set; }

        [Category("SSL"), DisplayName("SSL Cipher List"), aa("SslOptions_CipherList"), RefreshProperties(RefreshProperties.All)]
        public string SslCipherList { get; set; }

        [DisplayName("SSL CA Cert"), aa("SslOptions_CACert"), Category("SSL"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderSslCACertEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), RefreshProperties(RefreshProperties.All)]
        public string SslCACert { get; set; }

        [aa("SslOptions_Cert"), Category("SSL"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderSslCertEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DisplayName("SSL Cert"), RefreshProperties(RefreshProperties.All)]
        public string SslCert { get; set; }

        [aa("SslOptions_Key"), RefreshProperties(RefreshProperties.All), Category("SSL"), Editor("Devart.Data.MySql.Design.MySqlConnectionStringBuilderSslKeyEditor, Devart.Data.MySql.Design, Version=8.17.1696.0, Culture=neutral, PublicKeyToken=09af7300eec23701", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DisplayName("SSL Key")]
        public string SslKey { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("SslOptions_TlsProtocol"), Category("SSL"), DisplayName("SSL TLS Protocol")]
        public string SslTlsProtocol { get; set; }

        [Category("Security"), aa("DbConnectionString_PersistSecurityInfo"), RefreshProperties(RefreshProperties.All), DisplayName("Persist Security Info")]
        public bool PersistSecurityInfo { get; set; }

        [RefreshProperties(RefreshProperties.All), Category("Initialization"), DisplayName("Client Interactive"), aa("MySqlConnectionString_ClientInteractive")]
        public bool ClientInteractive { get; set; }

        [Category("Proxy Options"), DisplayName("Proxy Host"), aa("ProxyOptions_Host"), RefreshProperties(RefreshProperties.All)]
        public string ProxyHost { get; set; }

        [aa("ProxyOptions_Port"), RefreshProperties(RefreshProperties.All), Category("Proxy Options"), DisplayName("Proxy Port")]
        public int ProxyPort { get; set; }

        [Category("Proxy Options"), RefreshProperties(RefreshProperties.All), aa("ProxyOptions_User"), DisplayName("Proxy User")]
        public string ProxyUser { get; set; }

        [Category("Proxy Options"), RefreshProperties(RefreshProperties.All), aa("ProxyOptions_Password"), DisplayName("Proxy Password")]
        public string ProxyPassword { get; set; }

        [DisplayName("Plugin Dir"), aa("MySqlConnectionString_PluginDir"), RefreshProperties(RefreshProperties.All), Category("Security")]
        public string PluginDir { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("MySqlConnectionString_DefaultAuthPlugin"), DisplayName("Default Auth Plugin"), Category("Security")]
        public string DefaultAuthPlugin { get; set; }

        [Category("HTTP Options"), aa("HttpOptions_Url"), DisplayName("HTTP Url"), RefreshProperties(RefreshProperties.All)]
        public string HttpUrl { get; set; }

        [DisplayName("HTTP User"), aa("HttpOptions_User"), Category("HTTP Options"), RefreshProperties(RefreshProperties.All)]
        public string HttpUser { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("HttpOptions_Password"), Category("HTTP Options"), DisplayName("HTTP Password")]
        public string HttpPassword { get; set; }

        [Category("HTTP Options"), RefreshProperties(RefreshProperties.All), aa("HttpOptions_Base64"), DisplayName("HTTP Base64")]
        public bool HttpBase64 { get; set; }

        [DisplayName("HTTP Host"), aa("HttpOptions_Host"), RefreshProperties(RefreshProperties.All), Category("HTTP Options")]
        public string HttpHost { get; set; }

        [Category("HTTP Options"), aa("HttpOptions_Port"), RefreshProperties(RefreshProperties.All), DisplayName("HTTP Port")]
        public int HttpPort { get; set; }

        [aa("HttpOptions_StrictContentLength"), Category("HTTP Options"), DisplayName("HTTP Strict Content Length"), RefreshProperties(RefreshProperties.All)]
        public bool HttpStrictContentLength { get; set; }

        [RefreshProperties(RefreshProperties.All), aa("HttpOptions_ContentLength"), DisplayName("HTTP Content Length"), Category("HTTP Options")]
        public int HttpContentLength { get; set; }

        [Category("HTTP Options"), DisplayName("HTTP Keep Alive"), RefreshProperties(RefreshProperties.All), aa("HttpOptions_KeepAlive")]
        public int HttpKeepAlive { get; set; }

        [Category("HTTP Options"), aa("HttpOptions_MaxConnectionAge"), DisplayName("HTTP Max Connection Age"), RefreshProperties(RefreshProperties.All)]
        public int HttpMaxConnectionAge { get; set; }

        private enum Keywords
        {
            public const MySqlConnectionStringBuilder.Keywords UserId = MySqlConnectionStringBuilder.Keywords.UserId;,
            public const MySqlConnectionStringBuilder.Keywords Password = MySqlConnectionStringBuilder.Keywords.Password;,
            public const MySqlConnectionStringBuilder.Keywords Host = MySqlConnectionStringBuilder.Keywords.Host;,
            public const MySqlConnectionStringBuilder.Keywords Port = MySqlConnectionStringBuilder.Keywords.Port;,
            public const MySqlConnectionStringBuilder.Keywords Database = MySqlConnectionStringBuilder.Keywords.Database;,
            public const MySqlConnectionStringBuilder.Keywords Direct = MySqlConnectionStringBuilder.Keywords.Direct;,
            public const MySqlConnectionStringBuilder.Keywords Unicode = MySqlConnectionStringBuilder.Keywords.Unicode;,
            public const MySqlConnectionStringBuilder.Keywords Compress = MySqlConnectionStringBuilder.Keywords.Compress;,
            public const MySqlConnectionStringBuilder.Keywords Protocol = MySqlConnectionStringBuilder.Keywords.Protocol;,
            public const MySqlConnectionStringBuilder.Keywords PersistSecurityInfo = MySqlConnectionStringBuilder.Keywords.PersistSecurityInfo;,
            public const MySqlConnectionStringBuilder.Keywords ClientInteractive = MySqlConnectionStringBuilder.Keywords.ClientInteractive;,
            public const MySqlConnectionStringBuilder.Keywords DefaultCommandTimeout = MySqlConnectionStringBuilder.Keywords.DefaultCommandTimeout;,
            public const MySqlConnectionStringBuilder.Keywords DefaultFetchAll = MySqlConnectionStringBuilder.Keywords.DefaultFetchAll;,
            public const MySqlConnectionStringBuilder.Keywords ConnectionTimeout = MySqlConnectionStringBuilder.Keywords.ConnectionTimeout;,
            public const MySqlConnectionStringBuilder.Keywords PingInterval = MySqlConnectionStringBuilder.Keywords.PingInterval;,
            public const MySqlConnectionStringBuilder.Keywords Enlist = MySqlConnectionStringBuilder.Keywords.Enlist;,
            public const MySqlConnectionStringBuilder.Keywords TransactionScopeLocal = MySqlConnectionStringBuilder.Keywords.TransactionScopeLocal;,
            public const MySqlConnectionStringBuilder.Keywords PipeName = MySqlConnectionStringBuilder.Keywords.PipeName;,
            public const MySqlConnectionStringBuilder.Keywords Pooling = MySqlConnectionStringBuilder.Keywords.Pooling;,
            public const MySqlConnectionStringBuilder.Keywords MinPoolSize = MySqlConnectionStringBuilder.Keywords.MinPoolSize;,
            public const MySqlConnectionStringBuilder.Keywords MaxPoolSize = MySqlConnectionStringBuilder.Keywords.MaxPoolSize;,
            public const MySqlConnectionStringBuilder.Keywords ConnectionLifetime = MySqlConnectionStringBuilder.Keywords.ConnectionLifetime;,
            public const MySqlConnectionStringBuilder.Keywords Charset = MySqlConnectionStringBuilder.Keywords.Charset;,
            public const MySqlConnectionStringBuilder.Keywords BinaryAsGuid = MySqlConnectionStringBuilder.Keywords.BinaryAsGuid;,
            public const MySqlConnectionStringBuilder.Keywords CharAsGuid = MySqlConnectionStringBuilder.Keywords.CharAsGuid;,
            public const MySqlConnectionStringBuilder.Keywords TinyAsBoolean = MySqlConnectionStringBuilder.Keywords.TinyAsBoolean;,
            public const MySqlConnectionStringBuilder.Keywords ValidateConnection = MySqlConnectionStringBuilder.Keywords.ValidateConnection;,
            public const MySqlConnectionStringBuilder.Keywords InitializationCommand = MySqlConnectionStringBuilder.Keywords.InitializationCommand;,
            public const MySqlConnectionStringBuilder.Keywords RunOnceCommand = MySqlConnectionStringBuilder.Keywords.RunOnceCommand;,
            public const MySqlConnectionStringBuilder.Keywords FoundRows = MySqlConnectionStringBuilder.Keywords.FoundRows;,
            public const MySqlConnectionStringBuilder.Keywords DisableCharsetSending = MySqlConnectionStringBuilder.Keywords.DisableCharsetSending;,
            public const MySqlConnectionStringBuilder.Keywords IgnoreFractionalSeconds = MySqlConnectionStringBuilder.Keywords.IgnoreFractionalSeconds;,
            public const MySqlConnectionStringBuilder.Keywords KeepAlive = MySqlConnectionStringBuilder.Keywords.KeepAlive;,
            public const MySqlConnectionStringBuilder.Keywords IgnorePrepare = MySqlConnectionStringBuilder.Keywords.IgnorePrepare;,
            public const MySqlConnectionStringBuilder.Keywords SqlModes = MySqlConnectionStringBuilder.Keywords.SqlModes;,
            public const MySqlConnectionStringBuilder.Keywords Embedded = MySqlConnectionStringBuilder.Keywords.Embedded;,
            public const MySqlConnectionStringBuilder.Keywords SshHost = MySqlConnectionStringBuilder.Keywords.SshHost;,
            public const MySqlConnectionStringBuilder.Keywords SshPort = MySqlConnectionStringBuilder.Keywords.SshPort;,
            public const MySqlConnectionStringBuilder.Keywords SshUser = MySqlConnectionStringBuilder.Keywords.SshUser;,
            public const MySqlConnectionStringBuilder.Keywords SshPassword = MySqlConnectionStringBuilder.Keywords.SshPassword;,
            public const MySqlConnectionStringBuilder.Keywords SshPrivateKey = MySqlConnectionStringBuilder.Keywords.SshPrivateKey;,
            public const MySqlConnectionStringBuilder.Keywords SshPassphrase = MySqlConnectionStringBuilder.Keywords.SshPassphrase;,
            public const MySqlConnectionStringBuilder.Keywords SshAuthenticationType = MySqlConnectionStringBuilder.Keywords.SshAuthenticationType;,
            public const MySqlConnectionStringBuilder.Keywords SshCipherList = MySqlConnectionStringBuilder.Keywords.SshCipherList;,
            public const MySqlConnectionStringBuilder.Keywords SshHostKey = MySqlConnectionStringBuilder.Keywords.SshHostKey;,
            public const MySqlConnectionStringBuilder.Keywords SshStrictHostKeyChecking = MySqlConnectionStringBuilder.Keywords.SshStrictHostKeyChecking;,
            public const MySqlConnectionStringBuilder.Keywords SslCipherList = MySqlConnectionStringBuilder.Keywords.SslCipherList;,
            public const MySqlConnectionStringBuilder.Keywords SslCACert = MySqlConnectionStringBuilder.Keywords.SslCACert;,
            public const MySqlConnectionStringBuilder.Keywords SslCert = MySqlConnectionStringBuilder.Keywords.SslCert;,
            public const MySqlConnectionStringBuilder.Keywords SslKey = MySqlConnectionStringBuilder.Keywords.SslKey;,
            public const MySqlConnectionStringBuilder.Keywords SslTlsProtocol = MySqlConnectionStringBuilder.Keywords.SslTlsProtocol;,
            public const MySqlConnectionStringBuilder.Keywords PluginDir = MySqlConnectionStringBuilder.Keywords.PluginDir;,
            public const MySqlConnectionStringBuilder.Keywords DefaultAuthPlugin = MySqlConnectionStringBuilder.Keywords.DefaultAuthPlugin;,
            public const MySqlConnectionStringBuilder.Keywords ServerParameters = MySqlConnectionStringBuilder.Keywords.ServerParameters;,
            public const MySqlConnectionStringBuilder.Keywords ProxyHost = MySqlConnectionStringBuilder.Keywords.ProxyHost;,
            public const MySqlConnectionStringBuilder.Keywords ProxyPort = MySqlConnectionStringBuilder.Keywords.ProxyPort;,
            public const MySqlConnectionStringBuilder.Keywords ProxyUser = MySqlConnectionStringBuilder.Keywords.ProxyUser;,
            public const MySqlConnectionStringBuilder.Keywords ProxyPassword = MySqlConnectionStringBuilder.Keywords.ProxyPassword;,
            public const MySqlConnectionStringBuilder.Keywords HttpUrl = MySqlConnectionStringBuilder.Keywords.HttpUrl;,
            public const MySqlConnectionStringBuilder.Keywords HttpUser = MySqlConnectionStringBuilder.Keywords.HttpUser;,
            public const MySqlConnectionStringBuilder.Keywords HttpPassword = MySqlConnectionStringBuilder.Keywords.HttpPassword;,
            public const MySqlConnectionStringBuilder.Keywords HttpHost = MySqlConnectionStringBuilder.Keywords.HttpHost;,
            public const MySqlConnectionStringBuilder.Keywords HttpPort = MySqlConnectionStringBuilder.Keywords.HttpPort;,
            public const MySqlConnectionStringBuilder.Keywords HttpStrictContentLength = MySqlConnectionStringBuilder.Keywords.HttpStrictContentLength;,
            public const MySqlConnectionStringBuilder.Keywords HttpMaxConnectionAge = MySqlConnectionStringBuilder.Keywords.HttpMaxConnectionAge;,
            public const MySqlConnectionStringBuilder.Keywords HttpContentLength = MySqlConnectionStringBuilder.Keywords.HttpContentLength;,
            public const MySqlConnectionStringBuilder.Keywords HttpKeepAlive = MySqlConnectionStringBuilder.Keywords.HttpKeepAlive;,
            public const MySqlConnectionStringBuilder.Keywords HttpBase64 = MySqlConnectionStringBuilder.Keywords.HttpBase64;
        }

        internal sealed class MySqlConnectionStringBuilderConverter : ExpandableObjectConverter
        {
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
            private InstanceDescriptor ConvertToInstanceDescriptor(MySqlConnectionStringBuilder options);
        }
    }
}

