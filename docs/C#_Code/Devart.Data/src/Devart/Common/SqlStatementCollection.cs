namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class SqlStatementCollection : CollectionBase
    {
        public int Add(SqlStatement value) => 
            base.List.Add(value);

        public bool Contains(SqlStatement value) => 
            base.List.Contains(value);

        public void CopyTo(SqlStatement[] array, int index)
        {
            base.List.CopyTo(array, index);
        }

        public int IndexOf(SqlStatement value) => 
            base.List.IndexOf(value);

        public void Insert(int index, SqlStatement value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(SqlStatement value)
        {
            base.List.Remove(value);
        }

        public SqlStatement this[int index]
        {
            get => 
                (SqlStatement) base.List[index];
            set => 
                base.List[index] = value;
        }
    }
}

