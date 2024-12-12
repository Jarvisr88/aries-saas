namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Collections;

    public class MySqlDataSourceEnumerator : DbDataSourceEnumerator
    {
        private static ArrayList a;
        private static object b;

        static MySqlDataSourceEnumerator();
        private MySqlDataSourceEnumerator(int A_0);
        public static MySqlDataSourceEnumerator GetInstance(int port);

        public static MySqlDataSourceEnumerator Instance { get; }
    }
}

