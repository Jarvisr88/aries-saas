namespace Dapper
{
    using System;
    using System.Data;
    using System.Data.Common;

    internal static class WrappedReader
    {
        public static DbDataReader Create(IDbCommand cmd, DbDataReader reader)
        {
            if (cmd == null)
            {
                return reader;
            }
            if (reader != null)
            {
                return new DbWrappedReader(cmd, reader);
            }
            cmd.Dispose();
            return null;
        }

        public static IDataReader Create(IDbCommand cmd, IDataReader reader)
        {
            if (cmd == null)
            {
                return reader;
            }
            DbDataReader reader2 = reader as DbDataReader;
            if (reader2 != null)
            {
                return new DbWrappedReader(cmd, reader2);
            }
            if (reader != null)
            {
                return new BasicWrappedReader(cmd, reader);
            }
            cmd.Dispose();
            return null;
        }
    }
}

