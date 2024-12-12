namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class DBTableMultiColumnGadget
    {
        [XmlAttribute]
        public string Name;
        public StringCollection Columns;

        protected DBTableMultiColumnGadget()
        {
            this.Columns = new StringCollection();
        }

        protected DBTableMultiColumnGadget(ICollection columns)
        {
            this.Columns = new StringCollection();
            if (columns.Count < 1)
            {
                throw new ArgumentException(DbRes.GetString("ConnectionProvider_AtLeastOneColumnExpected"), "columns");
            }
            if (columns is StringCollection)
            {
                foreach (string str in columns)
                {
                    this.Columns.Add(str);
                }
            }
            else
            {
                foreach (DBColumn column in columns)
                {
                    this.Columns.Add(column.Name);
                }
            }
        }
    }
}

