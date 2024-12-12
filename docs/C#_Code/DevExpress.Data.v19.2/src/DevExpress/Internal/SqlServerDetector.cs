namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class SqlServerDetector : IDisposable
    {
        private readonly RegistryKeyProxy _localMachine;

        internal SqlServerDetector(RegistryKeyProxy localMachine)
        {
            this._localMachine = localMachine;
        }

        public void Dispose()
        {
            this._localMachine.Dispose();
        }

        public virtual string GetLocalDBVersionInstalled()
        {
            RegistryKeyProxy proxy = this.OpenLocalDBInstalledVersions(false);
            if (proxy.SubKeyCount == 0)
            {
                proxy = this.OpenLocalDBInstalledVersions(true);
            }
            List<Tuple<decimal, string>> source = new List<Tuple<decimal, string>>();
            foreach (string str in proxy.GetSubKeyNames())
            {
                decimal num2;
                if (decimal.TryParse(str, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num2))
                {
                    source.Add(Tuple.Create<decimal, string>(num2, str));
                }
            }
            Func<Tuple<decimal, string>, decimal> keySelector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<Tuple<decimal, string>, decimal> local1 = <>c.<>9__3_0;
                keySelector = <>c.<>9__3_0 = v => v.Item1;
            }
            Tuple<decimal, string> tuple = source.OrderByDescending<Tuple<decimal, string>, decimal>(keySelector).FirstOrDefault<Tuple<decimal, string>>();
            return (((tuple == null) || (tuple.Item2 == null)) ? null : ((tuple.Item1 < 12.0M) ? ("v" + tuple.Item2) : "mssqllocaldb"));
        }

        private RegistryKeyProxy OpenLocalDBInstalledVersions(bool useWow6432Node)
        {
            RegistryKeyProxy proxy = this._localMachine.OpenSubKey("SOFTWARE");
            if (useWow6432Node)
            {
                proxy = proxy.OpenSubKey("Wow6432Node");
            }
            return proxy.OpenSubKey("Microsoft").OpenSubKey("Microsoft SQL Server Local DB").OpenSubKey("Installed Versions");
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SqlServerDetector.<>c <>9 = new SqlServerDetector.<>c();
            public static Func<Tuple<decimal, string>, decimal> <>9__3_0;

            internal decimal <GetLocalDBVersionInstalled>b__3_0(Tuple<decimal, string> v) => 
                v.Item1;
        }

        internal class RegistryKeyProxy : IDisposable
        {
            private readonly RegistryKey _key;

            protected RegistryKeyProxy()
            {
            }

            public RegistryKeyProxy(RegistryKey key)
            {
                this._key = key;
            }

            public virtual void Dispose()
            {
                if (this._key != null)
                {
                    this._key.Dispose();
                }
            }

            public virtual string[] GetSubKeyNames() => 
                (this._key == null) ? new string[0] : this._key.GetSubKeyNames();

            public static implicit operator SqlServerDetector.RegistryKeyProxy(RegistryKey key) => 
                new SqlServerDetector.RegistryKeyProxy(key);

            public virtual SqlServerDetector.RegistryKeyProxy OpenSubKey(string name) => 
                new SqlServerDetector.RegistryKeyProxy(this._key?.OpenSubKey(name));

            public virtual int SubKeyCount =>
                (this._key == null) ? 0 : this._key.SubKeyCount;
        }
    }
}

