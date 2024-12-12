namespace Devart.Common
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data.Common;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class DbDump : Component
    {
        private DbConnection a;
        private string b = string.Empty;
        private StringCollection c = new StringCollection();
        private bool d = false;
        private bool e = false;
        private DumpMode f = DumpMode.All;
        private bool g = false;
        private Devart.Common.DbDump.b h;

        protected DbDump()
        {
        }

        private string a()
        {
            string str = "";
            foreach (string str2 in this.c)
            {
                if (str2 != string.Empty)
                {
                    str = str + str2 + ";";
                }
            }
            return str;
        }

        private void a(IAsyncResult A_0)
        {
            Utils.CheckArgumentNull(A_0, "result");
            if (this.h != null)
            {
                try
                {
                    this.h.EndInvoke(A_0);
                }
                finally
                {
                    this.h = null;
                }
            }
        }

        private void a(Stream A_0)
        {
            this.a(new StreamReader(A_0, this.Encoding));
        }

        private void a(TextReader A_0)
        {
            this.InternalRestore(A_0);
        }

        private void a(TextWriter A_0)
        {
            try
            {
                this.InternalBackup(A_0);
            }
            finally
            {
                A_0.Flush();
            }
        }

        private void a(string A_0)
        {
            this.c.Clear();
            if ((A_0 != null) && (A_0 != ""))
            {
                char[] separator = new char[] { ';' };
                foreach (string str in A_0.Split(separator))
                {
                    if (str != string.Empty)
                    {
                        this.c.Add(str);
                    }
                }
            }
        }

        private void a(Devart.Common.DbDump.a A_0, params object[] A_1)
        {
            switch (A_0)
            {
                case Devart.Common.DbDump.a.a:
                    this.c();
                    return;

                case Devart.Common.DbDump.a.b:
                    this.d((string) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.c:
                    this.b((Stream) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.d:
                    this.a((TextWriter) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.e:
                    this.c((string) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.f:
                    this.a((string) A_1[0], (string) A_1[1]);
                    return;

                case Devart.Common.DbDump.a.g:
                    this.a((string) A_1[0], (Stream) A_1[1]);
                    return;

                case Devart.Common.DbDump.a.h:
                    this.a((string) A_1[0], (TextWriter) A_1[1]);
                    return;

                case Devart.Common.DbDump.a.i:
                    this.b();
                    return;

                case Devart.Common.DbDump.a.j:
                    this.b((string) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.k:
                    this.a((Stream) A_1[0]);
                    return;

                case Devart.Common.DbDump.a.l:
                    this.a((TextReader) A_1[0]);
                    return;
            }
        }

        private void a(string A_0, bool A_1)
        {
            if (this.h != null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("DbDump_ExecutionInProgress", A_0));
            }
            if (A_1)
            {
                this.h = new Devart.Common.DbDump.b(this.a);
            }
        }

        private void a(string A_0, Stream A_1)
        {
            this.a(A_0, new StreamWriter(A_1, this.Encoding));
        }

        private void a(string A_0, TextWriter A_1)
        {
            this.InternalBackupQuery(A_1, A_0);
        }

        private void a(string A_0, string A_1)
        {
            using (StreamWriter writer = new StreamWriter(A_1, false, this.Encoding))
            {
                this.a(A_0, writer);
            }
        }

        private void b()
        {
            if (Utils.IsEmpty(this.b))
            {
                throw new InvalidOperationException(Devart.Common.g.a("YouMustSetupDumpTextProperty"));
            }
            using (StringReader reader = new StringReader(this.b))
            {
                this.a(reader);
            }
        }

        private void b(Stream A_0)
        {
            this.a(new StreamWriter(A_0, this.Encoding));
        }

        private void b(string A_0)
        {
            using (StreamReader reader = new StreamReader(A_0, this.Encoding))
            {
                this.a(reader);
            }
        }

        public void Backup()
        {
            this.a("Backup", false);
            this.c();
        }

        public void Backup(Stream stream)
        {
            this.a("Backup", false);
            this.b(stream);
        }

        public void Backup(TextWriter writer)
        {
            this.a("Backup", false);
            this.a(writer);
        }

        public void Backup(string fileName)
        {
            this.a("Backup", false);
            this.d(fileName);
        }

        public void BackupQuery(string query)
        {
            this.a("BackupQuery", false);
            this.c(query);
        }

        public void BackupQuery(string query, Stream stream)
        {
            this.a("BackupQuery", false);
            this.a(query, stream);
        }

        public void BackupQuery(string query, TextWriter writer)
        {
            this.a("BackupQuery", false);
            this.a(query, writer);
        }

        public void BackupQuery(string query, string fileName)
        {
            this.a("BackupQuery", false);
            this.a(query, fileName);
        }

        public IAsyncResult BeginBackup() => 
            this.BeginBackup(null, null);

        public IAsyncResult BeginBackup(Stream stream) => 
            this.BeginBackup(stream, null, null);

        public IAsyncResult BeginBackup(TextWriter writer) => 
            this.BeginBackup(writer, null, null);

        public IAsyncResult BeginBackup(string fileName) => 
            this.BeginBackup(fileName, null, null);

        public IAsyncResult BeginBackup(AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackup", true);
            return this.h.BeginInvoke(Devart.Common.DbDump.a.a, null, callback, stateObject);
        }

        public IAsyncResult BeginBackup(Stream stream, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackup", true);
            object[] objArray1 = new object[] { stream };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.c, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackup(TextWriter writer, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackup", true);
            object[] objArray1 = new object[] { writer };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.d, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackup(string fileName, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackup", true);
            object[] objArray1 = new object[] { fileName };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.b, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackupQuery(string query) => 
            this.BeginBackupQuery(query, null, null);

        public IAsyncResult BeginBackupQuery(string query, Stream stream) => 
            this.BeginBackupQuery(query, stream, null, null);

        public IAsyncResult BeginBackupQuery(string query, TextWriter writer) => 
            this.BeginBackupQuery(query, writer, null, null);

        public IAsyncResult BeginBackupQuery(string query, string fileName) => 
            this.BeginBackupQuery(query, fileName, null, null);

        public IAsyncResult BeginBackupQuery(string query, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackupQuery", true);
            object[] objArray1 = new object[] { query };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.e, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackupQuery(string query, Stream stream, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackupQuery", true);
            object[] objArray1 = new object[] { query, stream };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.g, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackupQuery(string query, TextWriter writer, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackupQuery", true);
            object[] objArray1 = new object[] { query, writer };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.h, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginBackupQuery(string query, string fileName, AsyncCallback callback, object stateObject)
        {
            this.a("BeginBackupQuery", true);
            object[] objArray1 = new object[] { query, fileName };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.f, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginRestore() => 
            this.BeginRestore(null, null);

        public IAsyncResult BeginRestore(Stream stream) => 
            this.BeginRestore(stream, null, null);

        public IAsyncResult BeginRestore(TextReader reader) => 
            this.BeginRestore(reader, null, null);

        public IAsyncResult BeginRestore(string fileName) => 
            this.BeginRestore(fileName, null, null);

        public IAsyncResult BeginRestore(AsyncCallback callback, object stateObject)
        {
            this.a("BeginRestore", true);
            return this.h.BeginInvoke(Devart.Common.DbDump.a.i, null, callback, stateObject);
        }

        public IAsyncResult BeginRestore(Stream stream, AsyncCallback callback, object stateObject)
        {
            this.a("BeginRestore", true);
            object[] objArray1 = new object[] { stream };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.j, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginRestore(TextReader reader, AsyncCallback callback, object stateObject)
        {
            this.a("BeginRestore", true);
            object[] objArray1 = new object[] { reader };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.j, objArray1, callback, stateObject);
        }

        public IAsyncResult BeginRestore(string fileName, AsyncCallback callback, object stateObject)
        {
            this.a("BeginRestore", true);
            object[] objArray1 = new object[] { fileName };
            return this.h.BeginInvoke(Devart.Common.DbDump.a.j, objArray1, callback, stateObject);
        }

        private void c()
        {
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    this.a(writer);
                }
                finally
                {
                    this.b = writer.ToString();
                }
            }
        }

        private void c(string A_0)
        {
            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    this.a(A_0, writer);
                }
                finally
                {
                    this.b = writer.ToString();
                }
            }
        }

        protected void CheckConnection()
        {
            Utils.CheckConnectionOpen(this.a);
        }

        private void d(string A_0)
        {
            using (StreamWriter writer = new StreamWriter(A_0, false, this.Encoding))
            {
                this.a(writer);
            }
        }

        public void EndBackup(IAsyncResult result)
        {
            this.a(result);
        }

        public void EndBackupQuery(IAsyncResult result)
        {
            this.a(result);
        }

        public void EndRestore(IAsyncResult result)
        {
            this.a(result);
        }

        protected abstract void InternalBackup(TextWriter writer);
        protected abstract void InternalBackupQuery(TextWriter writer, string query);
        protected abstract void InternalRestore(TextReader reader);
        public void Restore()
        {
            this.a("Restore", false);
            this.b();
        }

        public void Restore(Stream stream)
        {
            this.a("Restore", false);
            this.a(stream);
        }

        public void Restore(TextReader reader)
        {
            this.a("Restore", false);
            this.a(reader);
        }

        public void Restore(string fileName)
        {
            this.a("Restore", false);
            this.b(fileName);
        }

        private bool ShouldSerializeConnection() => 
            this.Connection != null;

        public DbConnection Connection
        {
            get => 
                this.a;
            set => 
                this.a = value;
        }

        public virtual string Tables
        {
            get => 
                this.a();
            set => 
                this.a(value);
        }

        public virtual string DumpText
        {
            get => 
                this.b;
            set => 
                this.b = value;
        }

        [Category("Options"), DefaultValue(false), y("DbDump_QuoteIdentifier")]
        public bool QuoteIdentifier
        {
            get => 
                this.d;
            set => 
                this.d = value;
        }

        [Category("Options"), y("DbDump_IncludeDrop"), DefaultValue(false)]
        public bool IncludeDrop
        {
            get => 
                this.e;
            set => 
                this.e = value;
        }

        [y("DbDump_Mode"), Category("Options"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(3)]
        public DumpMode Mode
        {
            get => 
                this.f;
            set => 
                this.f = value;
        }

        [Category("Options"), DefaultValue(false), y("DbDump_GenerateHeader")]
        public bool GenerateHeader
        {
            get => 
                this.g;
            set => 
                this.g = value;
        }

        protected abstract System.Text.Encoding Encoding { get; }

        protected StringCollection InnerTables =>
            this.c;

        protected bool BackupData =>
            (this.Mode & DumpMode.Data) != 0;

        protected bool BackupSchema =>
            (this.Mode & DumpMode.Schema) != 0;

        private enum a
        {
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j,
            k,
            l
        }

        private delegate void b(DbDump.a A_0, params object[] A_1);
    }
}

