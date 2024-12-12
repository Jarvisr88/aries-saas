namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Xml;

    public abstract class DbDataSet : DataSet, IListSource, ISupportInitialize
    {
        private x a;
        private object b;
        private bool c;
        private CollectionChangeEventHandler d;
        private string e;
        private DbConnection f;
        private object g;

        public DbDataSet()
        {
            this.e = string.Empty;
            this.d = new CollectionChangeEventHandler(this.a);
            base.Tables.CollectionChanged += this.d;
            base.Relations.CollectionChanged += new CollectionChangeEventHandler(this.b);
        }

        public DbDataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema) : base(info, context, ConstructSchema)
        {
            this.e = string.Empty;
            this.d = new CollectionChangeEventHandler(this.a);
            base.Tables.CollectionChanged += this.d;
            base.Relations.CollectionChanged += new CollectionChangeEventHandler(this.b);
        }

        private void a()
        {
            bool flag = false;
            int num = 0;
            while (true)
            {
                if (num < base.Tables.Count)
                {
                    if (base.Tables[num] is DbDataTable)
                    {
                        num++;
                        continue;
                    }
                    flag = true;
                }
                if (flag)
                {
                    DataTable[] tableArray = new DataTable[base.Tables.Count];
                    DataRelation[] array = new DataRelation[base.Relations.Count];
                    ArrayList[] listArray = new ArrayList[base.Tables.Count];
                    Dictionary<UniqueConstraint, bool> dictionary = new Dictionary<UniqueConstraint, bool>();
                    base.Relations.CopyTo(array, 0);
                    base.Relations.Clear();
                    int index = 0;
                    while (index < base.Tables.Count)
                    {
                        tableArray[index] = base.Tables[index];
                        listArray[index] = new ArrayList();
                        int num3 = 0;
                        while (true)
                        {
                            if (num3 >= base.Tables[index].Constraints.Count)
                            {
                                index++;
                                break;
                            }
                            if (base.Tables[index].Constraints[num3] is ForeignKeyConstraint)
                            {
                                listArray[index].Add(base.Tables[index].Constraints[num3]);
                                base.Tables[index].Constraints.RemoveAt(num3--);
                            }
                            num3++;
                        }
                    }
                    for (int i = 0; i < base.Tables.Count; i++)
                    {
                        foreach (Constraint constraint in base.Tables[i].Constraints)
                        {
                            listArray[i].Add(constraint);
                            if (constraint is UniqueConstraint)
                            {
                                dictionary.Add((UniqueConstraint) constraint, ((UniqueConstraint) constraint).IsPrimaryKey);
                            }
                        }
                        base.Tables[i].Constraints.Clear();
                    }
                    base.Tables.Clear();
                    for (int j = 0; j < tableArray.Length; j++)
                    {
                        DbDataTable table = this.CreateDataTable();
                        DbDataTable.a(tableArray[j], table);
                        table.BeginLoadData();
                        DataTable table2 = tableArray[j];
                        try
                        {
                            foreach (DataRow row in table2.Rows)
                            {
                                table.Rows.Add(row.ItemArray);
                            }
                        }
                        finally
                        {
                            table.EndLoadData();
                        }
                        base.Tables.Add(table);
                    }
                    int num6 = 0;
                    while (num6 < base.Tables.Count)
                    {
                        int num7 = 0;
                        while (true)
                        {
                            if (num7 >= listArray[num6].Count)
                            {
                                num6++;
                                break;
                            }
                            if (listArray[num6][num7] is UniqueConstraint)
                            {
                                UniqueConstraint constraint2 = (UniqueConstraint) listArray[num6][num7];
                                DataColumn[] columns = new DataColumn[constraint2.Columns.Length];
                                int num8 = 0;
                                while (true)
                                {
                                    if (num8 >= columns.Length)
                                    {
                                        UniqueConstraint constraint = new UniqueConstraint(constraint2.ConstraintName, columns, dictionary[constraint2]);
                                        foreach (object obj2 in constraint2.ExtendedProperties.Keys)
                                        {
                                            constraint.ExtendedProperties.Add(obj2, constraint2.ExtendedProperties[obj2]);
                                        }
                                        base.Tables[num6].Constraints.Add(constraint);
                                        listArray[num6].RemoveAt(num7--);
                                        break;
                                    }
                                    columns[num8] = base.Tables[num6].Columns[constraint2.Columns[num8].ColumnName];
                                    num8++;
                                }
                            }
                            num7++;
                        }
                    }
                    for (int k = 0; k < base.Tables.Count; k++)
                    {
                        foreach (Constraint constraint4 in listArray[k])
                        {
                            if (constraint4 is ForeignKeyConstraint)
                            {
                                ForeignKeyConstraint constraint5 = (ForeignKeyConstraint) constraint4;
                                DataColumn[] childColumns = new DataColumn[constraint5.Columns.Length];
                                int num10 = 0;
                                while (true)
                                {
                                    if (num10 >= childColumns.Length)
                                    {
                                        DataColumn[] parentColumns = new DataColumn[constraint5.RelatedColumns.Length];
                                        int num11 = 0;
                                        while (true)
                                        {
                                            if (num11 >= parentColumns.Length)
                                            {
                                                ForeignKeyConstraint constraint = new ForeignKeyConstraint(constraint5.ConstraintName, parentColumns, childColumns) {
                                                    AcceptRejectRule = constraint5.AcceptRejectRule,
                                                    DeleteRule = constraint5.DeleteRule,
                                                    UpdateRule = constraint5.UpdateRule
                                                };
                                                foreach (object obj3 in constraint5.ExtendedProperties.Keys)
                                                {
                                                    constraint.ExtendedProperties.Add(obj3, constraint5.ExtendedProperties[obj3]);
                                                }
                                                base.Tables[k].Constraints.Add(constraint);
                                                break;
                                            }
                                            parentColumns[num11] = base.Tables[constraint5.RelatedTable.TableName].Columns[constraint5.RelatedColumns[num11].ColumnName];
                                            num11++;
                                        }
                                        break;
                                    }
                                    childColumns[num10] = base.Tables[constraint5.Table.TableName].Columns[constraint5.Columns[num10].ColumnName];
                                    num10++;
                                }
                            }
                        }
                    }
                    int num12 = 0;
                    while (num12 < array.Length)
                    {
                        string[] parentColumnNames = new string[array[num12].ParentColumns.Length];
                        int num13 = 0;
                        while (true)
                        {
                            if (num13 >= array[num12].ParentColumns.Length)
                            {
                                string[] childColumnNames = new string[array[num12].ChildColumns.Length];
                                int num14 = 0;
                                while (true)
                                {
                                    if (num14 >= array[num12].ChildColumns.Length)
                                    {
                                        DataRelation relation1 = new DataRelation(array[num12].RelationName, array[num12].ParentTable.TableName, array[num12].ChildTable.TableName, parentColumnNames, childColumnNames, array[num12].Nested);
                                        num12++;
                                        break;
                                    }
                                    childColumnNames[num14] = array[num12].ChildColumns[num14].ColumnName;
                                    num14++;
                                }
                                break;
                            }
                            parentColumnNames[num13] = array[num12].ParentColumns[num13].ColumnName;
                            num13++;
                        }
                    }
                }
                return;
            }
        }

        private void a(object A_0, CollectionChangeEventArgs A_1)
        {
            if (A_1.Action == CollectionChangeAction.Add)
            {
                DbDataTable element = A_1.Element as DbDataTable;
                if (element != null)
                {
                    element.Owner = this.Owner;
                }
            }
            if (this.a != null)
            {
                this.a.a(A_0, new ListChangedEventArgs(ListChangedType.Reset, 0));
            }
        }

        internal DbDataTable a(DbDataTable A_0, PropertyDescriptor[] A_1, int A_2, out int A_3)
        {
            if (A_1.Length < (A_2 + 1))
            {
                A_3 = A_1.Length - 1;
                return A_0;
            }
            PropertyDescriptor descriptor = A_1[A_2];
            if (A_0 == null)
            {
                if (descriptor is DataViewManagerPropertyDescriptor)
                {
                    return this.a(((DataViewManagerPropertyDescriptor) descriptor).DataTable, A_1, A_2 + 1, out A_3);
                }
                A_3 = -1;
                return null;
            }
            if ((descriptor is Devart.Common.d) && (((Devart.Common.d) descriptor).c() != null))
            {
                return this.a((DbDataTable) ((Devart.Common.d) descriptor).c().ChildTable, A_1, A_2 + 1, out A_3);
            }
            if (descriptor is Devart.Common.d)
            {
                A_3 = A_2 - 1;
                return A_0;
            }
            A_3 = -1;
            return null;
        }

        private void b(object A_0, CollectionChangeEventArgs A_1)
        {
            DataRelation element = A_1.Element as DataRelation;
            if ((A_1.Action == CollectionChangeAction.Add) && (element != null))
            {
                DbDataTable childTable = element.ChildTable as DbDataTable;
                if ((childTable != null) && ((childTable.Site != null) && (childTable.Site.Container != null)))
                {
                    for (int i = 0; i < childTable.Columns.Count; i++)
                    {
                        DataColumn component = childTable.Columns[i];
                        if ((component != null) && (component.Site == null))
                        {
                            childTable.Site.Container.Add(component, childTable.Site.Name + "_" + component.ColumnName);
                        }
                    }
                }
                childTable = element.ParentTable as DbDataTable;
                if ((childTable != null) && ((childTable.Site != null) && (childTable.Site.Container != null)))
                {
                    for (int i = 0; i < childTable.Columns.Count; i++)
                    {
                        DataColumn component = childTable.Columns[i];
                        if ((component != null) && (component.Site == null))
                        {
                            childTable.Site.Container.Add(component, childTable.Site.Name + "_" + component.ColumnName);
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            bool enforceConstraints = base.EnforceConstraints;
            try
            {
                base.EnforceConstraints = false;
                for (int i = 0; i < base.Tables.Count; i++)
                {
                    DataTable table = base.Tables[i];
                    DbDataTable table2 = table as DbDataTable;
                    if (table2 != null)
                    {
                        table2.Clear();
                    }
                    else
                    {
                        table.Clear();
                    }
                }
            }
            finally
            {
                base.EnforceConstraints = enforceConstraints;
            }
        }

        public override DataSet Clone()
        {
            DbDataSet set = (DbDataSet) base.Clone();
            for (int i = 0; i < set.Tables.Count; i++)
            {
                DbDataTable table = base.Tables[i] as DbDataTable;
                if (table != null)
                {
                    table.a((DbDataTable) set.Tables[i]);
                }
            }
            set.Connection = this.Connection;
            return set;
        }

        public virtual DbDataTable CreateDataTable()
        {
            throw new Exception();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Tables.CollectionChanged -= this.d;
            }
            base.Dispose(disposing);
            if ((this.Owner != null) && !this.DesignMode)
            {
                GlobalComponentsCache.RemoveFromGlobalList(this);
            }
        }

        public void Fill()
        {
            bool enforceConstraints = base.EnforceConstraints;
            try
            {
                base.EnforceConstraints = false;
                for (int i = base.Tables.Count - 1; i >= 0; i--)
                {
                    DbDataTable table = base.Tables[i] as DbDataTable;
                    if (table != null)
                    {
                        table.Fill();
                    }
                }
            }
            finally
            {
                base.EnforceConstraints = enforceConstraints;
            }
        }

        public XmlReadMode ReadXml(Stream stream)
        {
            this.a();
            return base.ReadXml(stream);
        }

        public XmlReadMode ReadXml(TextReader reader)
        {
            this.a();
            return base.ReadXml(reader);
        }

        public XmlReadMode ReadXml(string fileName)
        {
            this.a();
            return base.ReadXml(fileName);
        }

        public XmlReadMode ReadXml(XmlReader reader)
        {
            this.a();
            return base.ReadXml(reader);
        }

        public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
        {
            this.a();
            return base.ReadXml(stream, mode);
        }

        public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
        {
            this.a();
            return base.ReadXml(reader, mode);
        }

        public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
        {
            this.a();
            return base.ReadXml(fileName, mode);
        }

        public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
        {
            this.a();
            return base.ReadXml(reader, mode);
        }

        protected override bool ShouldSerializeRelations() => 
            (base.Relations != null) ? (base.Relations.Count != 0) : true;

        protected override bool ShouldSerializeTables() => 
            (base.Tables != null) ? (base.Tables.Count != 0) : true;

        IList IListSource.GetList()
        {
            this.a ??= new x(base.DefaultViewManager);
            return this.a;
        }

        void ISupportInitialize.BeginInit()
        {
            base.BeginInit();
            for (int i = 0; i < base.Tables.Count; i++)
            {
                DbDataTable table = base.Tables[i] as DbDataTable;
                if (table != null)
                {
                    table.BeginInit();
                }
            }
        }

        void ISupportInitialize.EndInit()
        {
            for (int i = 0; i < base.Tables.Count; i++)
            {
                DbDataTable table = base.Tables[i] as DbDataTable;
                if (table != null)
                {
                    table.EndInit();
                }
            }
            base.EndInit();
        }

        public void Update()
        {
            bool enforceConstraints = base.EnforceConstraints;
            try
            {
                base.EnforceConstraints = false;
                int count = base.Tables.Count;
                for (int i = 0; i < count; i++)
                {
                    DbDataTable table = base.Tables[i] as DbDataTable;
                    if (table != null)
                    {
                        table.Update();
                    }
                }
            }
            finally
            {
                base.EnforceConstraints = enforceConstraints;
            }
        }

        bool IListSource.ContainsListCollection =>
            true;

        [DefaultValue(""), Browsable(false)]
        public string Name
        {
            get => 
                (this.Site == null) ? ((this.e == null) ? string.Empty : this.e) : this.Site.Name;
            set
            {
                if (this.Site == null)
                {
                    this.e = (value == null) ? string.Empty : value;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object Owner
        {
            get => 
                this.b;
            set
            {
                if (this.b != value)
                {
                    for (int i = 0; i < base.Tables.Count; i++)
                    {
                        DbDataTable table = base.Tables[i] as DbDataTable;
                        if ((table != null) && (table.Owner != value))
                        {
                            table.Owner = value;
                        }
                    }
                }
                if ((this.b != null) && !this.DesignMode)
                {
                    GlobalComponentsCache.RemoveFromGlobalList(this);
                }
                this.b = value;
                if ((this.b != null) && !this.DesignMode)
                {
                    GlobalComponentsCache.AddToGlobalList(this);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected internal bool Reloading
        {
            get => 
                this.c;
            set
            {
                this.c = value;
                for (int i = 0; i < base.Tables.Count; i++)
                {
                    DbDataTable table = base.Tables[i] as DbDataTable;
                    if (table != null)
                    {
                        table.Reloading = value;
                    }
                }
            }
        }

        [Browsable(false), MergableProperty(false)]
        public DbConnection Connection
        {
            get => 
                this.f;
            set => 
                this.f = value;
        }

        [DefaultValue(0), y("DbDataSet_RomotingFormat")]
        public SerializationFormat RemotingFormat
        {
            get => 
                base.RemotingFormat;
            set => 
                base.RemotingFormat = value;
        }

        internal object FetchRowSyncRoot
        {
            get
            {
                this.g ??= new object();
                return this.g;
            }
        }
    }
}

