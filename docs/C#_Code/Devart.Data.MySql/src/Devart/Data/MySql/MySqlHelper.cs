namespace Devart.Data.MySql
{
    using System;
    using System.Data;

    [Obsolete("This class is designed for compartibility with Connector/Net only")]
    public sealed class MySqlHelper
    {
        private static MySqlDataReader a(MySqlConnection A_0, MySqlTransaction A_1, string A_2, MySqlParameter[] A_3, bool A_4);
        public static string EscapeString(string value);
        public static DataRow ExecuteDataRow(string connectionString, string commandText, params MySqlParameter[] parms);
        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText);
        public static DataSet ExecuteDataset(string connectionString, string commandText);
        public static DataSet ExecuteDataset(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters);
        public static DataSet ExecuteDataset(string connectionString, string commandText, params MySqlParameter[] commandParameters);
        public static int ExecuteNonQuery(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters);
        public static int ExecuteNonQuery(string connectionString, string commandText, params MySqlParameter[] parms);
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText);
        public static MySqlDataReader ExecuteReader(string connectionString, string commandText, params MySqlParameter[] commandParameters);
        public static object ExecuteScalar(MySqlConnection connection, string commandText);
        public static object ExecuteScalar(string connectionString, string commandText);
        public static object ExecuteScalar(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters);
        public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters);
        public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename);
    }
}

