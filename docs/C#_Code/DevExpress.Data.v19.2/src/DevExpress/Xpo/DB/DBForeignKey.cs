namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Xml.Serialization;

    [Serializable]
    public class DBForeignKey : DBTableMultiColumnGadget
    {
        [XmlAttribute]
        public string PrimaryKeyTable;
        public StringCollection PrimaryKeyTableKeyColumns;

        public DBForeignKey()
        {
            this.PrimaryKeyTableKeyColumns = new StringCollection();
        }

        public DBForeignKey(ICollection columns, string primaryKeyTable, StringCollection primaryKeyTableKeyColumns) : base(columns)
        {
            this.PrimaryKeyTableKeyColumns = new StringCollection();
            this.PrimaryKeyTable = primaryKeyTable;
            this.PrimaryKeyTableKeyColumns = primaryKeyTableKeyColumns;
        }
    }
}

