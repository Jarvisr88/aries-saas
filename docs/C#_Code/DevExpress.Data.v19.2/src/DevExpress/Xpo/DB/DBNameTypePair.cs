namespace DevExpress.Xpo.DB
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class DBNameTypePair
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public DBColumnType Type;

        public DBNameTypePair()
        {
        }

        public DBNameTypePair(string name, DBColumnType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString() => 
            $"{this.Name} {this.Type}";
    }
}

