namespace DevExpress.Xpo.DB
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [Serializable]
    public class DBIndex : DBTableMultiColumnGadget
    {
        [XmlAttribute, DefaultValue(false)]
        public bool IsUnique;

        public DBIndex()
        {
        }

        public DBIndex(ICollection columns, bool isUnique) : this(null, columns, isUnique)
        {
        }

        public DBIndex(string name, ICollection columns, bool isUnique) : base(columns)
        {
            base.Name = name;
            this.IsUnique = isUnique;
        }
    }
}

