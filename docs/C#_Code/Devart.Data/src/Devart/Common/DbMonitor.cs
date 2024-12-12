namespace Devart.Common
{
    using Devart.DbMonitor;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    public abstract class DbMonitor : Component, IDisposable
    {
        private bool a;
        private bool b = true;
        private static Dictionary<int, long> c = new Dictionary<int, long>();
        private static Devart.Common.m d = new Devart.Common.m();
        private const string f = "ConnectionString = \"";
        private const string g = "Connect: ";
        private const string h = "Creating pool manager";
        private const string i = "Creating pool with connections string: ";
        private const string j = "Creating object";
        private const string k = "Taking connection from connection pool: ";
        private const string l = "Connection is taken from pool.";
        private const string m = "Connection is returning to pool.";
        private const string n = "Connection is returned to pool.";
        private const string o = "Disconnect";
        private const string p = "Begin local transaction";
        private const string q = "Begin distributed transaction";
        private const string r = "Begin TransactionScope local transaction";
        private const string s = "Prepare commit";
        private const string t = "Commit";
        private const string u = "Rollback";
        private const string v = "Open connection: ";
        private const string w = "Close connection";
        private const string x = "Execute: ";
        private const string y = "Prepare: ";

        [y("DbMonitor_TraceEvent")]
        public event MonitorEventHandler TraceEvent;

        protected DbMonitor()
        {
        }

        private static string[] a()
        {
            StackTrace trace = new StackTrace(false);
            int frameCount = trace.FrameCount;
            List<string> list = new List<string>(frameCount);
            for (int i = 0; i < frameCount; i++)
            {
                MethodBase method = trace.GetFrame(i).GetMethod();
                list.Add(a(method));
            }
            list.Reverse();
            return list.ToArray();
        }

        private static void a(object A_0)
        {
            int hashCode = A_0.GetHashCode();
            long num2 = Devart.Common.u.a();
            lock (c)
            {
                if (c.ContainsKey(hashCode))
                {
                    c[hashCode] = num2;
                }
                else
                {
                    c.Add(hashCode, num2);
                }
            }
        }

        private static string a(MethodBase A_0)
        {
            string str = (A_0.DeclaringType != null) ? (A_0.DeclaringType.FullName + "." + A_0.Name) : A_0.Name;
            StringBuilder builder = new StringBuilder(str);
            builder.Append('(');
            foreach (ParameterInfo info in A_0.GetParameters())
            {
                Type parameterType = info.ParameterType;
                string str2 = string.Empty;
                if (parameterType.IsArray)
                {
                    str2 = a(parameterType);
                }
                builder.Append(parameterType.Name + str2 + " " + info.Name);
            }
            builder.Append(')');
            return builder.ToString();
        }

        private static string a(Type A_0)
        {
            int arrayRank = A_0.GetArrayRank();
            StringBuilder builder1 = new StringBuilder();
            builder1.Append('[');
            builder1.Append(',', arrayRank - 1);
            builder1.Append(']');
            return builder1.ToString();
        }

        private Devart.DbMonitor.c a(object A_0, MonitorEventArgs A_1)
        {
            Devart.DbMonitor.c c = new Devart.DbMonitor.c();
            c.d(A_1.Description);
            object parentObject = this.GetParentObject(A_0);
            if (parentObject != null)
            {
                c.c(parentObject.GetHashCode());
                c.a(this.a(parentObject, A_1.CallStack));
            }
            else
            {
                c.c(0);
                c.a(string.Empty);
            }
            if (parentObject is IDbConnection)
            {
                c.a(Devart.DbMonitor.d.b);
            }
            c.b(A_0.GetHashCode());
            c.f(this.a(A_0, A_1.CallStack));
            c.m().AddRange(A_1.CallStack);
            switch (A_1.EventTypeInternal)
            {
                case Devart.Common.c.a:
                    c.a(Devart.DbMonitor.j.c);
                    c.b(Devart.DbMonitor.d.b);
                    break;

                case Devart.Common.c.b:
                    c.a(Devart.DbMonitor.j.d);
                    c.b(Devart.DbMonitor.d.b);
                    break;

                case Devart.Common.c.c:
                case Devart.Common.c.d:
                {
                    if (A_1.EventTypeInternal == Devart.Common.c.c)
                    {
                        c.a(Devart.DbMonitor.j.i);
                    }
                    else
                    {
                        c.a(Devart.DbMonitor.j.k);
                    }
                    IDbCommand command = (IDbCommand) A_0;
                    Devart.DbMonitor.b[] collection = new Devart.DbMonitor.b[command.Parameters.Count];
                    int index = 0;
                    while (true)
                    {
                        string str;
                        string str2;
                        string str3;
                        string str4;
                        if (index >= collection.Length)
                        {
                            int num = -1;
                            if (A_1.ExtraInfo != null)
                            {
                                num = Convert.ToInt32(A_1.ExtraInfo);
                            }
                            c.c().AddRange(collection);
                            c.e(command.CommandText);
                            c.d(num);
                            c.b(Devart.DbMonitor.d.d);
                            break;
                        }
                        this.GetParameterInfo((IDbDataParameter) command.Parameters[index], out str, out str2, out str3, out str4);
                        collection[index].c(str);
                        collection[index].b(str2);
                        collection[index].a(str3);
                        collection[index].d(str4);
                        index++;
                    }
                    break;
                }
                case Devart.Common.c.e:
                    c.a(Devart.DbMonitor.j.e);
                    c.b(Devart.DbMonitor.d.b);
                    break;

                case Devart.Common.c.f:
                    c.a(Devart.DbMonitor.j.f);
                    c.b(Devart.DbMonitor.d.c);
                    break;

                case Devart.Common.c.g:
                    c.a(Devart.DbMonitor.j.g);
                    c.b(Devart.DbMonitor.d.c);
                    break;

                case Devart.Common.c.h:
                    c.d("Error");
                    c.a(Devart.DbMonitor.j.q);
                    c.g(A_1.Description);
                    c.a(true);
                    break;

                case Devart.Common.c.j:
                    c.a(Devart.DbMonitor.j.n);
                    c.b(Devart.DbMonitor.d.f);
                    break;

                case Devart.Common.c.k:
                    c.a(Devart.DbMonitor.j.n);
                    c.b(Devart.DbMonitor.d.f);
                    break;

                case Devart.Common.c.l:
                    c.a(Devart.DbMonitor.j.c);
                    c.b(Devart.DbMonitor.d.f);
                    break;

                case Devart.Common.c.m:
                    c.a(Devart.DbMonitor.j.c);
                    c.b(Devart.DbMonitor.d.b);
                    break;

                case Devart.Common.c.n:
                case Devart.Common.c.o:
                    c.a(Devart.DbMonitor.j.p);
                    c.b(Devart.DbMonitor.d.f);
                    break;

                case Devart.Common.c.p:
                    c.a(Devart.DbMonitor.j.n);
                    if (A_0 is IDbConnection)
                    {
                        c.b(Devart.DbMonitor.d.b);
                    }
                    else if (A_0 is IDbCommand)
                    {
                        c.b(Devart.DbMonitor.d.d);
                    }
                    if (c.q() == A_0.GetType().FullName.ToString())
                    {
                    }
                    break;

                default:
                    c.a(Devart.DbMonitor.j.q);
                    break;
            }
            return c;
        }

        private static double a(object A_0, MonitorTracePoint A_1)
        {
            double num = -1.0;
            if ((A_1 == MonitorTracePoint.AfterEvent) && (A_0 != null))
            {
                int hashCode = A_0.GetHashCode();
                long num3 = 0L;
                lock (c)
                {
                    if (c.ContainsKey(hashCode))
                    {
                        num3 = c[hashCode];
                        c.Remove(hashCode);
                    }
                }
                if (num3 > 0L)
                {
                    num = Devart.Common.u.a(num3);
                }
            }
            return num;
        }

        private string a(object A_0, string[] A_1)
        {
            string objectName = this.GetObjectName(A_0);
            if (!string.IsNullOrEmpty(objectName))
            {
                return objectName;
            }
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(A_0).Find("Name", false);
            if ((descriptor != null) && !Utils.IsEmpty((string) descriptor.GetValue(A_0)))
            {
                object[] objArray1 = new object[] { (string) descriptor.GetValue(A_0), " (", A_0.GetHashCode(), ")" };
                return string.Concat(objArray1);
            }
            if ((A_0 is Component) && (((Component) A_0).Site != null))
            {
                return ((Component) A_0).Site.Name;
            }
            string fullName = A_0.GetType().FullName;
            char[] anyOf = new char[] { '.' };
            int num = fullName.LastIndexOfAny(anyOf);
            string str3 = A_0.GetHashCode().ToString();
            if ((A_1 != null) && (A_1.Length != 0))
            {
                string str4 = null;
                int index = A_1.Length - 1;
                while (true)
                {
                    if (index >= 0)
                    {
                        str4 = A_1[index];
                        if (str4.StartsWith("Devart.") || str4.StartsWith("System.Data."))
                        {
                            index--;
                            continue;
                        }
                    }
                    if (!string.IsNullOrEmpty(str4))
                    {
                        int length = str4.IndexOf('(');
                        if (length >= 0)
                        {
                            str4 = str4.Substring(0, length);
                        }
                        char[] separator = new char[] { '.' };
                        string[] strArray = str4.Split(separator);
                        char[] trimChars = new char[] { ')', '(' };
                        str3 = "in " + strArray[strArray.Length - 1].TrimEnd(trimChars);
                    }
                    break;
                }
            }
            return (fullName.Substring(num + 1, (fullName.Length - num) - 1) + " (" + str3 + ")");
        }

        private static void a(DbMonitor A_0, object A_1, MonitorEventArgs A_2)
        {
            a(A_0, A_1, A_2, false);
        }

        private void a(MonitorTracePoint A_0, Devart.DbMonitor.c A_1, bool A_2)
        {
            if (this.UseApp)
            {
                if (!ReferenceEquals(d.c(), this.ProductName))
                {
                    d.b(this.ProductName);
                }
                d.a(A_1, A_0, A_2);
            }
        }

        private static void a(DbMonitor A_0, object A_1, MonitorEventArgs A_2, bool A_3)
        {
            if ((A_0 != null) && (A_1 != null))
            {
                if (A_2.TracePoint == MonitorTracePoint.BeforeEvent)
                {
                    a(A_1);
                }
                A_0.OnTraceEvent(A_1, A_2, A_3);
            }
        }

        private static void a(DbMonitor A_0, object A_1, object A_2, string A_3, string A_4)
        {
            if (A_0 != null)
            {
                int poolGroupConnectionCount = A_0.GetPoolGroupConnectionCount(A_2);
                if (poolGroupConnectionCount >= 0)
                {
                    object[] objArray1 = new object[] { A_4, " Pool has ", poolGroupConnectionCount, " connection(s)." };
                    string str = string.Concat(objArray1);
                    a(A_0, A_1, new MonitorEventArgs(Devart.Common.c.n, str, MonitorTracePoint.BeforeEvent, A_3, a(), 0.0));
                    a(A_0, A_1, new MonitorEventArgs(Devart.Common.c.n, str, MonitorTracePoint.AfterEvent, A_3, a(), 0.0));
                }
            }
        }

        internal void a(IDbDataParameter A_0, out string A_1, out string A_2, out string A_3, out string A_4)
        {
            this.GetParameterInfo(A_0, out A_1, out A_2, out A_3, out A_4);
        }

        private void b()
        {
            this.IsActive = false;
        }

        internal void c()
        {
            try
            {
                d.b();
            }
            catch
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.b();
            base.Dispose(disposing);
        }

        protected abstract string GetObjectName(object obj);
        protected virtual void GetParameterInfo(IDbDataParameter parameter, out string name, out string dbType, out string direction, out string value)
        {
            name = parameter.ParameterName;
            dbType = parameter.DbType.ToString();
            direction = parameter.Direction.ToString();
            object val = parameter.Value;
            if (Utils.IsNull(val))
            {
                value = "NULL";
            }
            else
            {
                value = val.ToString();
            }
        }

        protected virtual object GetParentObject(object sender)
        {
            IDbCommand command = sender as IDbCommand;
            return command?.Connection;
        }

        protected abstract int GetPoolGroupConnectionCount(object dbConnectionPool);
        protected static void OnActivate(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender, string connectionString)
        {
            if (monitor != null)
            {
                string str = "Open connection: \"" + connectionString + "\"";
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.a, str, tracePoint, connectionString, a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnBeginDistributedTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.e, "Begin distributed transaction", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnBeginLocalTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.e, "Begin local transaction", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnBeginTransactionScopeLocalTransaction(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.e, "Begin TransactionScope local transaction", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnCommit(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.f, "Commit", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected static void OnConnect(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString, bool pooled)
        {
            if (monitor != null)
            {
                string str = "Connect: \"" + connectionString + "\"";
                a(monitor, sender, new MonitorEventArgs(pooled ? Devart.Common.c.l : Devart.Common.c.m, str, tracePoint, connectionString, a(), a(sender, tracePoint)));
            }
        }

        protected static void OnCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, bool isParentMessage)
        {
            if (monitor != null)
            {
                MonitorEventArgs args = new MonitorEventArgs(Devart.Common.c.p, "Creating object", tracePoint, "", a(), a(sender, MonitorTracePoint.AfterEvent));
                a(monitor, sender, args, isParentMessage);
            }
        }

        protected internal static void OnCustomAction(DbMonitor monitor, MonitorTracePoint tracePoint, string description, object sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.i, description, tracePoint, "", a(), a(sender, MonitorTracePoint.AfterEvent)));
            }
        }

        protected static void OnDeactivate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.b, "Close connection", tracePoint, connectionString, a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnDisconnect(DbMonitor monitor, MonitorTracePoint tracePoint, object sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.b, "Disconnect", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnError(DbMonitor monitor, Exception e, object sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.h, e.Message, MonitorTracePoint.AfterEvent, "", a(), a(sender, MonitorTracePoint.AfterEvent)));
            }
        }

        protected internal static void OnExecute(DbMonitor monitor, MonitorTracePoint tracePoint, IDbCommand sender, string sql, int rowsAffected)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.d, "Execute: " + sql, tracePoint, rowsAffected.ToString(), a(), a(sender, tracePoint)));
            }
        }

        protected static void OnPoolGroupCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, string connectionString)
        {
            if (monitor != null)
            {
                MonitorEventArgs args = new MonitorEventArgs(Devart.Common.c.k, "Creating pool with connections string: \"" + connectionString + "\"", tracePoint, connectionString, a(), a(sender, tracePoint));
                a(monitor, sender, args, true);
            }
        }

        protected static void OnPoolManagerCreate(DbMonitor monitor, MonitorTracePoint tracePoint, object sender)
        {
            if (monitor != null)
            {
                MonitorEventArgs args = new MonitorEventArgs(Devart.Common.c.j, "Creating pool manager", tracePoint, null, a(), a(sender, tracePoint));
                a(monitor, sender, args, true);
            }
        }

        protected internal static void OnPrepare(DbMonitor monitor, MonitorTracePoint tracePoint, IDbCommand sender, string sql)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.c, "Prepare: " + sql, tracePoint, null, a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnPrepareCommit(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.f, "Prepare commit", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected static void OnReturnToPool(DbMonitor monitor, MonitorTracePoint tracePoint, object sender, object dbConnectionPool, string connectionString)
        {
            if (monitor != null)
            {
                string str = string.Empty;
                int poolGroupConnectionCount = monitor.GetPoolGroupConnectionCount(dbConnectionPool);
                str = "Connection is returned to pool. Pool has " + poolGroupConnectionCount + " connection(s).";
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.o, str, tracePoint, connectionString, a(), a(sender, tracePoint)));
            }
        }

        protected internal static void OnRollback(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender)
        {
            if (monitor != null)
            {
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.g, "Rollback", tracePoint, "", a(), a(sender, tracePoint)));
            }
        }

        protected static void OnTakeFromPool(DbMonitor monitor, MonitorTracePoint tracePoint, IDbConnection sender, string connectionString, object dbConnectionPool)
        {
            if (monitor != null)
            {
                string str = "Taking connection from connection pool: \"" + connectionString + "\"";
                a(monitor, sender, new MonitorEventArgs(Devart.Common.c.n, str, tracePoint, connectionString, a(), a(sender, tracePoint)));
                if (tracePoint == MonitorTracePoint.AfterEvent)
                {
                    a(monitor, sender, dbConnectionPool, connectionString, "Connection is taken from pool.");
                }
            }
        }

        protected void OnTraceEvent(object sender, MonitorEventArgs e, bool parentMessage)
        {
            if (this.DbMonitorAppAvailable)
            {
                try
                {
                    Devart.DbMonitor.c c = this.a(sender, e);
                    this.a(e.TracePoint, c, parentMessage);
                }
                catch
                {
                }
            }
            try
            {
                if ((e.TracePoint == MonitorTracePoint.AfterEvent) && this.a)
                {
                    Type type = typeof(Trace);
                    Type[] types = new Type[] { typeof(string) };
                    string[] textArray1 = new string[] { this.ProductName, ": ", this.GetObjectName(sender), " - ", e.Description };
                    string str = string.Concat(textArray1);
                    object[] parameters = new object[] { str };
                    type.GetMethod("WriteLine", types).Invoke(type, parameters);
                }
            }
            catch
            {
            }
            try
            {
                if ((this.e != null) && e.IsUserEvent)
                {
                    this.e(sender, e);
                }
            }
            catch
            {
            }
        }

        protected void SetMonitorActive(bool value)
        {
            if (!value)
            {
                this.c();
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        protected abstract string ProductName { get; }

        [DefaultValue(false), Category("Behavior"), y("DbMonitor_IsActive")]
        public abstract bool IsActive { get; set; }

        [y("DbMonitor_UseIdeOutput"), DefaultValue(false), Category("Behavior")]
        public bool UseIdeOutput
        {
            get => 
                this.a;
            set => 
                this.a = value;
        }

        [DefaultValue("localhost"), y("DbMonitor_Host")]
        public string Host
        {
            get => 
                d.e();
            set
            {
                if (!this.DbMonitorAppAvailable)
                {
                    throw new NotSupportedException(Devart.Common.g.a("DbMonitor_NotSupportApp"));
                }
                d.a(value);
            }
        }

        [y("DbMonitor_Port"), DefaultValue(0x3e8)]
        public int Port
        {
            get => 
                d.g();
            set
            {
                if (!this.DbMonitorAppAvailable)
                {
                    throw new NotSupportedException(Devart.Common.g.a("DbMonitor_NotSupportApp"));
                }
                d.c(value);
            }
        }

        [y("DbMonitor_UseApp"), DefaultValue(true)]
        public bool UseApp
        {
            get => 
                this.DbMonitorAppAvailable && this.b;
            set
            {
                if (!this.DbMonitorAppAvailable)
                {
                    throw new NotSupportedException(Devart.Common.g.a("DbMonitor_NotSupportApp"));
                }
                this.b = value;
                if (!this.b)
                {
                    this.c();
                }
            }
        }

        [DefaultValue(0x3e8), y("DbMonitor_EventQueueLimit")]
        public int EventQueueLimit
        {
            get => 
                d.d();
            set
            {
                if (!this.DbMonitorAppAvailable)
                {
                    throw new NotSupportedException(Devart.Common.g.a("DbMonitor_NotSupportApp"));
                }
                d.b(value);
            }
        }

        protected abstract bool DbMonitorAppAvailable { get; }
    }
}

