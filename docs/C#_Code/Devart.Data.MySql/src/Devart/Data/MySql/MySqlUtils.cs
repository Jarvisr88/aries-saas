namespace Devart.Data.MySql
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;

    public class MySqlUtils
    {
        private static ArrayList a;
        private static bool b;
        internal const string c = "Scanning the network...";
        internal const string d = "yyyy-MM-dd";
        internal const string e = "HH:mm:ss";
        internal const string f = "yyyy-MM-dd HH:mm:ss";
        internal const string g = "yyyy-MM-dd HH:mm:ss.FFFFFF";
        internal const string h = "yyyyMMddHHmmss";
        internal const string i = "yyyyMMddHHmmss.FFFFFF";
        internal const string j = "yyMMddHHmmss";
        internal const string k = "yyMMddHHmm";
        internal const string l = "yyyyMMdd";
        internal const string m = "yyMMdd";
        internal const string n = "yyMM";
        internal const string o = "yy";
        internal const string p = ":proc";
        internal const string q = ":func";

        static MySqlUtils();
        internal static ArrayList a();
        internal static MySqlType a(a1 A_0);
        internal static string a(int A_0);
        internal static byte[] a(char[] A_0);
        internal static bool a(DbType A_0);
        internal static bool a(string A_0);
        internal static string a(TimeSpan A_0);
        internal static bool a(XmlReader A_0);
        internal static void a(ref string A_0);
        internal static void a(XmlWriter A_0);
        internal static bool a(a1 A_0, bool A_1);
        internal static object a(object A_0, MySqlType A_1);
        internal static byte[] a(byte[] A_0, int A_1);
        internal static void a(string A_0, StringBuilder A_1);
        internal static Type a(MySqlType A_0, bool A_1, int A_2);
        internal static Type a(int A_0, bool A_1, int A_2);
        internal static bool a(int A_0, int A_1, int A_2);
        internal static int a(string A_0, int A_1, int A_2);
        internal static void a(string A_0, out string A_1, out string A_2);
        internal static Type a(MySqlType A_0, bool A_1, int A_2, bool A_3);
        internal static void a(DataColumn A_0, MySqlConnection A_1, string A_2, string A_3);
        internal static MySqlType b(DbType A_0);
        internal static DbType b(int A_0);
        internal static List<string> b(string A_0);
        internal static object b(object A_0, MySqlType A_1);
        internal static bool c();
        internal static byte[] c(string A_0);
        internal static string[] d(string A_0);
        internal static string e(string A_0);
        internal static Devart.Data.MySql.MySqlUtils.a f(string A_0);
        internal static int g(string A_0);
        internal static string h(string A_0);
        internal static DbType MySqlTypeToDbType(MySqlType mySqlDbType);
        public static bool NeedQuote(string name);
        public static string QuoteIfNeed(string name);
        public static string QuoteName(string name);
        internal static MySqlType TypeToMySqlType(Type type);
        public static string UnQuoteName(string name);

        internal static bool DataSourcesObtained { get; }

        internal enum a
        {
            public const Devart.Data.MySql.MySqlUtils.a a = Devart.Data.MySql.MySqlUtils.a.a;,
            public const Devart.Data.MySql.MySqlUtils.a b = Devart.Data.MySql.MySqlUtils.a.b;,
            public const Devart.Data.MySql.MySqlUtils.a c = Devart.Data.MySql.MySqlUtils.a.c;
        }
    }
}

