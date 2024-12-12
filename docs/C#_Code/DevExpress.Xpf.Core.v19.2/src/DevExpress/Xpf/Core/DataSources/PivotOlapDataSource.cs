namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;

    public class PivotOlapDataSource : DXDesignTimeControl
    {
        public static readonly DependencyProperty ServerProperty;
        public static readonly DependencyProperty CatalogProperty;
        public static readonly DependencyProperty CubeProperty;
        public static readonly DependencyProperty ConnectionTimeoutProperty;
        public static readonly DependencyProperty LocaleIdentifierProperty;
        public static readonly DependencyProperty QueryTimeoutProperty;
        public static readonly DependencyProperty UserIdProperty;
        public static readonly DependencyProperty PasswordProperty;
        public static readonly DependencyProperty ProviderProperty;
        public static readonly DependencyProperty ConnectionStringProperty;
        private static readonly DependencyPropertyKey ConnectionStringPropertyKey;

        static PivotOlapDataSource()
        {
            Type ownerType = typeof(PivotOlapDataSource);
            ServerProperty = DependencyPropertyManager.Register("Server", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            CatalogProperty = DependencyPropertyManager.Register("Catalog", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            CubeProperty = DependencyPropertyManager.Register("Cube", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            ConnectionTimeoutProperty = DependencyPropertyManager.Register("ConnectionTimeout", typeof(int), ownerType, new UIPropertyMetadata((int) 60, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString(), new CoerceValueCallback(PivotOlapDataSource.CoerceTimeout)));
            LocaleIdentifierProperty = DependencyPropertyManager.Register("LocaleIdentifier", typeof(int), ownerType, new UIPropertyMetadata(DefaultLCID, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            QueryTimeoutProperty = DependencyPropertyManager.Register("QueryTimeout", typeof(int), ownerType, new UIPropertyMetadata((int) 30, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString(), new CoerceValueCallback(PivotOlapDataSource.CoerceTimeout)));
            UserIdProperty = DependencyPropertyManager.Register("UserId", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            PasswordProperty = DependencyPropertyManager.Register("Password", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            ProviderProperty = DependencyPropertyManager.Register("Provider", typeof(string), ownerType, new UIPropertyMetadata(null, (d, e) => ((PivotOlapDataSource) d).UpdateConnectionString()));
            ConnectionStringPropertyKey = DependencyPropertyManager.RegisterReadOnly("ConnectionString", typeof(string), ownerType, new UIPropertyMetadata(null));
            ConnectionStringProperty = ConnectionStringPropertyKey.DependencyProperty;
        }

        private static object CoerceTimeout(DependencyObject d, object baseValue)
        {
            int num = (int) baseValue;
            return ((num < 1) ? 1 : num);
        }

        protected override string GetDesignTimeImageName() => 
            string.Empty;

        protected virtual void UpdateConnectionString()
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(this.Server) || (string.IsNullOrEmpty(this.Catalog) || string.IsNullOrEmpty(this.Cube)))
            {
                this.ConnectionString = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Provider))
                {
                    builder.Append("Provider=").Append(this.Provider).Append(";");
                }
                builder.Append("Data Source=").Append(this.Server).Append(";");
                builder.Append("initial catalog=").Append(this.Catalog).Append(";");
                builder.Append("cube name=").Append(this.Cube).Append(";");
                if (this.ConnectionTimeout != 60)
                {
                    builder.Append("connect timeout=").Append(this.ConnectionTimeout).Append(";");
                }
                if (this.QueryTimeout != 30)
                {
                    builder.Append("query timeout=").Append(this.QueryTimeout).Append(";");
                }
                if (this.LocaleIdentifier != DefaultLCID)
                {
                    builder.Append("locale identifier=").Append(this.LocaleIdentifier).Append(";");
                }
                if (!string.IsNullOrEmpty(this.UserId) && !string.IsNullOrEmpty(this.Password))
                {
                    builder.Append("user id=").Append(this.UserId).Append(";password=").Append(this.Password).Append(";");
                }
                this.ConnectionString = builder.ToString();
            }
        }

        public static int DefaultLCID =>
            CultureInfo.CurrentCulture.LCID;

        public string Provider
        {
            get => 
                (string) base.GetValue(ProviderProperty);
            set => 
                base.SetValue(ProviderProperty, value);
        }

        public string Server
        {
            get => 
                (string) base.GetValue(ServerProperty);
            set => 
                base.SetValue(ServerProperty, value);
        }

        public string Catalog
        {
            get => 
                (string) base.GetValue(CatalogProperty);
            set => 
                base.SetValue(CatalogProperty, value);
        }

        public string Cube
        {
            get => 
                (string) base.GetValue(CubeProperty);
            set => 
                base.SetValue(CubeProperty, value);
        }

        public int ConnectionTimeout
        {
            get => 
                (int) base.GetValue(ConnectionTimeoutProperty);
            set => 
                base.SetValue(ConnectionTimeoutProperty, value);
        }

        public int LocaleIdentifier
        {
            get => 
                (int) base.GetValue(LocaleIdentifierProperty);
            set => 
                base.SetValue(LocaleIdentifierProperty, value);
        }

        public int QueryTimeout
        {
            get => 
                (int) base.GetValue(QueryTimeoutProperty);
            set => 
                base.SetValue(QueryTimeoutProperty, value);
        }

        public string UserId
        {
            get => 
                (string) base.GetValue(UserIdProperty);
            set => 
                base.SetValue(UserIdProperty, value);
        }

        public string Password
        {
            get => 
                (string) base.GetValue(PasswordProperty);
            set => 
                base.SetValue(PasswordProperty, value);
        }

        public string ConnectionString
        {
            get => 
                (string) base.GetValue(ConnectionStringProperty);
            protected set => 
                base.SetValue(ConnectionStringPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PivotOlapDataSource.<>c <>9 = new PivotOlapDataSource.<>c();

            internal void <.cctor>b__13_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }

            internal void <.cctor>b__13_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PivotOlapDataSource) d).UpdateConnectionString();
            }
        }
    }
}

