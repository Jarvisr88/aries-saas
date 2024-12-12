namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Runtime.InteropServices;

    internal class be : DbMetaDataFactory
    {
        private readonly int a;
        private bool b;
        private bool c;

        public be(string A_0, string A_1);
        private DataTable a();
        private static DataTable a(DbConnectionBase A_0);
        private DataTable a(DbConnection A_0);
        private static bool a(string A_0);
        private static string[] a(string[] A_0);
        private DataTable a(DbConnectionBase A_0, string[] A_1);
        private DataTable a(DbConnection A_0, string[] A_1);
        private static DataTable a(DataTable A_0, string A_1);
        private static int a(string A_0, int A_1);
        private DataTable a(DbConnectionBase A_0, string[] A_1, bool A_2);
        private void a(DataTable A_0, IDataReader A_1, string[] A_2);
        private DataTable a(string A_0, DbConnectionBase A_1, string[] A_2);
        private DataTable a(string A_0, DbConnection A_1, string[] A_2);
        public override DataTable a(DbConnection A_0, DbConnectionInternal A_1, string A_2, string[] A_3);
        private DataTable a(DbConnection A_0, string[] A_1, string A_2, bool A_3);
        private static IDataReader a(DbConnection A_0, string A_1, string[] A_2, string[] A_3);
        private static DataTable a(DbConnection A_0, string A_1, string A_2, string[] A_3, string[] A_4);
        private static void a(DataTable A_0, string A_1, string A_2, string A_3, string A_4);
        private static void a(string A_0, out string A_1, out int A_2, out int A_3, out int A_4, out bool A_5, out bool A_6, out bool A_7);
        private DataTable b();
        private DataTable b(DbConnectionBase A_0, string[] A_1);
        private DataTable b(DbConnection A_0, string[] A_1);
        private DataTable b(DbConnectionBase A_0, string[] A_1, bool A_2);
        private DataTable c();
        private DataTable c(DbConnectionBase A_0, string[] A_1);
        private DataTable c(DbConnection A_0, string[] A_1);
        private static Stream d();
        private DataTable d(DbConnection A_0, string[] A_1);
        private DataTable e(DbConnection A_0, string[] A_1);
        private DataTable f(DbConnection A_0, string[] A_1);
        private DataTable g(DbConnection A_0, string[] A_1);
        private DataTable h(DbConnection A_0, string[] A_1);
        private DataTable i(DbConnection A_0, string[] A_1);
        private DataTable j(DbConnection A_0, string[] A_1);
        private DataTable k(DbConnection A_0, string[] A_1);
        private DataTable l(DbConnection A_0, string[] A_1);
        private DataTable m(DbConnection A_0, string[] A_1);
        private DataTable n(DbConnection A_0, string[] A_1);
        private DataTable o(DbConnection A_0, string[] A_1);
        private DataTable p(DbConnection A_0, string[] A_1);
        private DataTable q(DbConnection A_0, string[] A_1);
        private DataTable r(DbConnection A_0, string[] A_1);
        private DataTable s(DbConnection A_0, string[] A_1);
        private static DataTable t(DbConnection A_0, string[] A_1);
    }
}

