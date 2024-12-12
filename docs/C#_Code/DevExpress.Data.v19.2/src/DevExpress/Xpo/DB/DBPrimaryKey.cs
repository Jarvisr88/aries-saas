namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections;

    [Serializable]
    public class DBPrimaryKey : DBIndex
    {
        public DBPrimaryKey()
        {
        }

        public DBPrimaryKey(ICollection columns) : base(columns, true)
        {
        }

        public DBPrimaryKey(string name, ICollection columns) : base(name, columns, true)
        {
        }
    }
}

