namespace DevExpress.Internal
{
    using System;
    using System.Data.SqlClient;

    public class SqlConnectionHelper
    {
        public static bool CheckSQLExpressConnection()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=tempdb;Integrated Security=true;User Instance=True;"))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
    }
}

