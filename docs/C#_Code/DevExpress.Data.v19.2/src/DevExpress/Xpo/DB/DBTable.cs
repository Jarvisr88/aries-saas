namespace DevExpress.Xpo.DB
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(DBProjection))]
    public class DBTable
    {
        private DBColumnCollection columns;
        private DBIndexCollection indexes;
        private DBForeignKeyCollection foreignKeys;
        [XmlAttribute]
        public string Name;
        public DBPrimaryKey PrimaryKey;
        [XmlAttribute, DefaultValue(false)]
        public bool IsView;
        private static int HashSeed = HashCodeHelper.StartGeneric<string>(typeof(DBTable).Name);

        public DBTable()
        {
            this.columns = new DBColumnCollection();
            this.indexes = new DBIndexCollection();
            this.foreignKeys = new DBForeignKeyCollection();
        }

        public DBTable(string name)
        {
            this.columns = new DBColumnCollection();
            this.indexes = new DBIndexCollection();
            this.foreignKeys = new DBForeignKeyCollection();
            this.Name = name;
        }

        public void AddColumn(DBColumn column)
        {
            this.columns.Add(column);
        }

        public void AddForeignKey(DBForeignKey foreignKey)
        {
            this.foreignKeys.Add(foreignKey);
        }

        public void AddIndex(DBIndex index)
        {
            this.indexes.Add(index);
        }

        public override bool Equals(object obj)
        {
            DBTable table = obj as DBTable;
            return ((table != null) ? (this.Name == table.Name) : false);
        }

        public DBColumn GetColumn(string columnName)
        {
            int count = this.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                DBColumn column = this.Columns[i];
                if (column.Name == columnName)
                {
                    return column;
                }
            }
            return null;
        }

        public override int GetHashCode() => 
            HashCodeHelper.FinishGeneric<string>(HashSeed, this.Name);

        public bool IsForeignKeyIncluded(DBForeignKey foreignKey)
        {
            bool flag;
            using (List<DBForeignKey>.Enumerator enumerator = this.ForeignKeys.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DBForeignKey current = enumerator.Current;
                        if (!this.IsGadgetsEqual(current, foreignKey))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsGadgetsEqual(DBTableMultiColumnGadget existingKey, DBTableMultiColumnGadget fk)
        {
            if ((existingKey.Name != fk.Name) || (existingKey.Columns.Count != fk.Columns.Count))
            {
                return false;
            }
            for (int i = 0; i < existingKey.Columns.Count; i++)
            {
                if (existingKey.Columns[i] != fk.Columns[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsIndexIncluded(DBIndex index)
        {
            bool flag;
            using (List<DBIndex>.Enumerator enumerator = this.Indexes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        DBIndex current = enumerator.Current;
                        if (!this.IsGadgetsEqual(current, index))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public override string ToString() => 
            this.Name;

        [XmlArrayItem(typeof(DBColumn))]
        public List<DBColumn> Columns =>
            this.columns;

        [XmlArrayItem(typeof(DBIndex))]
        public List<DBIndex> Indexes =>
            this.indexes;

        [XmlArrayItem(typeof(DBForeignKey))]
        public List<DBForeignKey> ForeignKeys =>
            this.foreignKeys;
    }
}

