namespace Devart.Data.MySql
{
    using Devart.Common;
    using System;
    using System.Reflection;

    public class MySqlStatementCollection : SqlStatementCollection
    {
        public int Add(MySqlStatement value);
        public bool Contains(MySqlStatement value);
        public void CopyTo(MySqlStatement[] array, int index);
        public int IndexOf(MySqlStatement value);
        public void Insert(int index, MySqlStatement value);
        public void Remove(MySqlStatement value);

        public MySqlStatement this[int index] { get; set; }
    }
}

