namespace DMEWorks.Core
{
    using System;
    using System.Collections.Generic;

    public class SqlStringComparer : IComparer<SqlString>, IEqualityComparer<SqlString>, IComparer<string>, IEqualityComparer<string>
    {
        public static readonly SqlStringComparer Default = new SqlStringComparer();

        int IComparer<SqlString>.Compare(SqlString x, SqlString y) => 
            SqlString.Compare(x, y);

        int IComparer<string>.Compare(string x, string y) => 
            SqlString.Compare(x, y);

        bool IEqualityComparer<SqlString>.Equals(SqlString x, SqlString y) => 
            SqlString.Equals(x, y);

        int IEqualityComparer<SqlString>.GetHashCode(SqlString value) => 
            SqlString.GetHashCode(value);

        bool IEqualityComparer<string>.Equals(string x, string y) => 
            SqlString.Equals(x, y);

        int IEqualityComparer<string>.GetHashCode(string value) => 
            SqlString.GetHashCode(value);
    }
}

