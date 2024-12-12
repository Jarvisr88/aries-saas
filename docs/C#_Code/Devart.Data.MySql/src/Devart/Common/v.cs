namespace Devart.Common
{
    using System;
    using System.Collections.Generic;
    using System.Transactions;

    internal class v
    {
        private static Dictionary<Transaction, v> a = new Dictionary<Transaction, v>();
        private Dictionary<ac, List<DbConnectionInternal>> b = new Dictionary<ac, List<DbConnectionInternal>>();

        public bool a() => 
            this.b.Count > 0;

        public DbConnectionInternal a(ac A_0)
        {
            DbConnectionInternal internal2;
            lock (this.b)
            {
                List<DbConnectionInternal> list;
                if (this.b.TryGetValue(A_0, out list))
                {
                    using (List<DbConnectionInternal>.Enumerator enumerator = list.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            DbConnectionInternal current = enumerator.Current;
                            DbConnectionInternal internal4 = current;
                            lock (internal4)
                            {
                                if (!current.ConnectionIsClosedAndDeffered)
                                {
                                    continue;
                                }
                                current.ConnectionIsClosedAndDeffered = false;
                                return current;
                            }
                        }
                    }
                    if (A_0.b())
                    {
                        throw new NotSupportedException("Multiple simultaneous connections inside the same local transaction are not currently supported.");
                    }
                    internal2 = null;
                }
                else
                {
                    if (A_0.b() && (this.b.Count > 0))
                    {
                        throw new NotSupportedException("Connections with different connection strings inside the same local transaction are not currently supported.");
                    }
                    internal2 = null;
                }
            }
            return internal2;
        }

        public void a(DbConnectionInternal A_0)
        {
            lock (this.b)
            {
                List<DbConnectionInternal> list;
                ac connectionOptions = ((DbConnectionBase) A_0.Owner).ConnectionOptions;
                if (!this.b.TryGetValue(connectionOptions, out list))
                {
                    list = new List<DbConnectionInternal>();
                    this.b.Add(connectionOptions, list);
                }
                list.Add(A_0);
            }
        }

        private static void a(Transaction A_0)
        {
            lock (a)
            {
                a.Remove(A_0);
            }
        }

        private static void a(object A_0, TransactionEventArgs A_1)
        {
            a(A_1.Transaction);
        }

        public static DbConnectionInternal a(Transaction A_0, ac A_1) => 
            c(A_0)?.a(A_1);

        public static void a(Transaction A_0, DbConnectionInternal A_1)
        {
            b(A_0).a(A_1);
        }

        public static v b(Transaction A_0)
        {
            lock (a)
            {
                v v;
                if (!a.TryGetValue(A_0, out v))
                {
                    v = new v();
                    a.Add(A_0, v);
                    A_0.TransactionCompleted += new TransactionCompletedEventHandler(v.a);
                }
                return v;
            }
        }

        public static v c(Transaction A_0)
        {
            v v;
            a.TryGetValue(A_0, out v);
            return v;
        }

        public static bool d(Transaction A_0)
        {
            v v = c(A_0);
            return ((v != null) ? v.a() : false);
        }
    }
}

