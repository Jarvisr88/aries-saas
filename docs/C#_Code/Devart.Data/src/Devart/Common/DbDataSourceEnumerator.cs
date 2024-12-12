namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Threading;

    public abstract class DbDataSourceEnumerator : System.Data.Common.DbDataSourceEnumerator
    {
        protected int port;
        private string a;
        private string b;
        private static aa[] c;
        private static int d;

        protected DbDataSourceEnumerator(string factoryName, string serverPrefix, int port)
        {
            this.port = port;
            this.a = factoryName;
            this.b = serverPrefix;
        }

        private static void a()
        {
            aa aa;
            bool flag;
            lock (c.SyncRoot)
            {
                d++;
                aa = c[d];
            }
            using (TcpClient client = new TcpClient())
            {
                client.NoDelay = false;
                client.ReceiveTimeout = 50;
                client.SendTimeout = 50;
                try
                {
                    client.Connect(aa.a, aa.b);
                    client.Close();
                    flag = true;
                }
                catch (SocketException)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                lock (aa.a)
                {
                    aa.c.Add(aa.a);
                }
            }
        }

        private IList a(int A_0)
        {
            IList list4;
            IList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            if (!a(list2, IntPtr.Zero))
            {
                return null;
            }
            IList list3 = new ArrayList();
            c = new aa[list2.Count];
            d = 0;
            int num = 0;
            try
            {
                foreach (string str in list2)
                {
                    Thread thread = new Thread(new ThreadStart(Devart.Common.DbDataSourceEnumerator.a)) {
                        Name = "Devart_DbDataSourceEnumerator_" + str
                    };
                    list.Add(thread);
                    aa aa = new aa {
                        a = str,
                        c = list3,
                        b = this.port
                    };
                    thread.IsBackground = true;
                    object syncRoot = c.SyncRoot;
                    lock (syncRoot)
                    {
                        c[num++] = aa;
                    }
                    thread.Start();
                }
                Thread.Sleep(A_0);
                foreach (Thread thread2 in list)
                {
                    if (thread2.ThreadState == ThreadState.Running)
                    {
                        try
                        {
                            thread2.Abort();
                        }
                        catch
                        {
                        }
                    }
                }
                list4 = list3;
            }
            catch (ThreadAbortException)
            {
                foreach (Thread thread3 in list)
                {
                    if (thread3.ThreadState == ThreadState.Running)
                    {
                        try
                        {
                            thread3.Abort();
                        }
                        catch
                        {
                        }
                    }
                }
                throw;
            }
            return list4;
        }

        private static bool a(IList A_0, IntPtr A_1)
        {
            IntPtr ptr;
            int cb = 0x4000;
            int num3 = -1;
            int num = k.WNetOpenEnum(k.c, k.b, 0, A_1, out ptr);
            if (num != k.a)
            {
                return false;
            }
            IntPtr ptr2 = Marshal.AllocHGlobal(cb);
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if ((num = k.WNetEnumResource(ptr, ref num3, ptr2, ref cb)) == k.a)
                        {
                            for (int i = 0; i < num3; i++)
                            {
                                IntPtr ptr3 = (IntPtr.Size != 8) ? ((IntPtr) (ptr2.ToInt32() + (i * Marshal.SizeOf(typeof(Devart.Common.k.a))))) : ((IntPtr) (ptr2.ToInt64() + Convert.ToInt64((int) (i * Marshal.SizeOf(typeof(Devart.Common.k.a))))));
                                Devart.Common.k.a a = (Devart.Common.k.a) Marshal.PtrToStructure(ptr3, typeof(Devart.Common.k.a));
                                if (((a.d & k.e) != 0) && (a.c != k.f))
                                {
                                    Devart.Common.DbDataSourceEnumerator.a(A_0, ptr3);
                                }
                                else
                                {
                                    A_0.Add((a.f.Substring(0, 2) == @"\\") ? a.f.Substring(2) : a.f);
                                }
                            }
                        }
                        if (num != k.d)
                        {
                            break;
                        }
                        return (num == k.a);
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr2);
                    num = k.WNetCloseEnum(ptr);
                }
            }
        }

        public override DataTable GetDataSources()
        {
            DataTable table = new DataTable(this.b + "DataSources") {
                Locale = CultureInfo.InvariantCulture
            };
            table.Columns.Add("ServerName", typeof(string));
            table.Columns.Add("InstanceName", typeof(string));
            table.Columns.Add("IsClustered", typeof(string));
            table.Columns.Add("Version", typeof(string));
            table.Columns.Add("FactoryName", typeof(string));
            IList list = this.a(0x1388);
            if (list != null)
            {
                foreach (string str4 in list)
                {
                    string str;
                    string str2;
                    string str3;
                    if (this.ProcessServer(str4, out str, out str2, out str3))
                    {
                        DataRow row = table.NewRow();
                        row[0] = str4;
                        row[1] = str;
                        row[2] = str2;
                        row[3] = str3;
                        row[4] = this.a;
                        table.Rows.Add(row);
                    }
                }
            }
            using (IEnumerator enumerator = table.Columns.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ((DataColumn) enumerator.Current).ReadOnly = true;
                }
            }
            return table;
        }

        protected virtual bool ProcessServer(string host, out string instanceName, out string isClustered, out string version)
        {
            string text1 = version = string.Empty;
            instanceName = isClustered = text1;
            return true;
        }
    }
}

