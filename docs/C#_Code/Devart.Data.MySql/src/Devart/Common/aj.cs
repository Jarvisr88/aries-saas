namespace Devart.Common
{
    using Devart.Security;
    using System;
    using System.Collections;
    using System.Text;

    internal class aj
    {
        private static Devart.Common.aj a = new Devart.Common.aj();
        private Hashtable b = new Hashtable();
        private Hashtable c = new Hashtable();
        private Hashtable d = new Hashtable();

        private aj()
        {
        }

        public static void a()
        {
            a.b.Clear();
            a.c.Clear();
            a.d.Clear();
        }

        internal static byte[] a(string A_0)
        {
            if (!a.d.Contains(A_0))
            {
                throw new InvalidOperationException("No private key matching this id can be found in the storage.");
            }
            return (byte[]) a.d[A_0];
        }

        public static void a(string A_0, string A_1)
        {
            a(A_0, Encoding.ASCII.GetBytes(A_1));
        }

        public static void a(string A_0, byte[] A_1)
        {
            a.d.Add(A_0, A_1);
        }

        public static void b()
        {
            a.d.Clear();
        }

        internal static Devart.Security.e b(string A_0)
        {
            if (!a.c.Contains(A_0))
            {
                throw new InvalidOperationException("No certificate matching this id can be found in the storage.");
            }
            return (Devart.Security.e) a.c[A_0];
        }

        public static void b(string A_0, string A_1)
        {
            b(A_0, Encoding.ASCII.GetBytes(A_1));
        }

        public static void b(string A_0, byte[] A_1)
        {
            a.c.Add(A_0, Devart.Security.e.d(A_1));
        }

        public static void c()
        {
            a.c.Clear();
        }

        internal static Devart.Security.e c(string A_0)
        {
            if (!a.b.Contains(A_0))
            {
                throw new InvalidOperationException("No CA certificate matching this id can be found in the storage.");
            }
            return (Devart.Security.e) a.b[A_0];
        }

        public static void c(string A_0, string A_1)
        {
            c(A_0, Encoding.ASCII.GetBytes(A_1));
        }

        public static void c(string A_0, byte[] A_1)
        {
            a.b.Add(A_0, Devart.Security.e.d(A_1));
        }

        public static void d()
        {
            a.b.Clear();
        }

        public static bool d(string A_0) => 
            a.d.Contains(A_0);

        public static bool e(string A_0) => 
            a.c.Contains(A_0);

        public static bool f(string A_0) => 
            a.b.Contains(A_0);

        public static void g(string A_0)
        {
            a.d.Remove(A_0);
        }

        public static void h(string A_0)
        {
            a.c.Remove(A_0);
        }

        public static void i(string A_0)
        {
            a.b.Remove(A_0);
        }
    }
}

