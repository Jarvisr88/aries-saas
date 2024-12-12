namespace Devart.Common
{
    using Devart.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Threading;
    using System.Windows.Forms;

    public abstract class DbDataTable : DataTable, IListSource, ISupportInitializeNotification
    {
        protected internal DataTable dataTable;
        private object a;
        private bool b;
        protected internal DbConnection connection;
        internal DbCommand c;
        protected DbCommand currenSelectCommand;
        protected DbCommand detailSelectCommand;
        internal DbCommand d;
        internal DbCommand e;
        internal DbCommand f;
        protected IDataReader reader;
        private bool g;
        private bool h;
        private bool i;
        private bool j;
        private int k;
        private int l;
        private bool m;
        private DbDataTableView n;
        internal EventHandler o;
        private Devart.Common.DbDataTable.a p;
        internal DataTableMapping q;
        private bool r;
        private bool s;
        private object t;
        private object u;
        private int v;
        private int w;
        private bool x;
        protected internal int disableEvents;
        protected internal int storeEvents;
        protected internal int disableUpdateEvents;
        internal bool y;
        protected PropertyDescriptorCollection propertyDescriptorsCache;
        protected Hashtable readerMappings;
        internal Devart.Common.z z;
        internal ParentDataRelation aa;
        private bool ab;
        private bool ac;
        private bool ad;
        private int ae;
        protected bool hasComplexFields;
        protected bool returnProviderSpecificTypesInternal;
        private bool af;
        private bool ag;
        private bool ah;
        private static readonly object ai = new object();
        private static readonly object aj = new object();
        private static readonly object ak = new object();
        private static readonly object al = new object();
        private static readonly object am = new object();
        private bool an;
        private bool ao;
        private System.Data.MissingSchemaAction ap;
        private int aq;
        protected DataTable schemaTable;
        private static bool ar;
        private bool @as;
        protected int indexOfColumnOnlyOriginalValue;
        private int at;
        private DataRow au;
        protected Devart.Common.DbDataAdapter dataAdapter;
        protected Devart.Common.DbCommandBuilder commandBuilder;
        private string av;
        private const int aw = 0x7fffffff;
        private const int ax = 0x7ffffffe;
        private const System.Data.ConflictOption ay = System.Data.ConflictOption.CompareAllSearchableValues;
        private ArrayList az;
        private const string a0 = "Cannot establish master/detail relation";
        private bool a1;
        private int a2;

        [y("DbDataTable_Disposed")]
        public event EventHandler Disposed
        {
            add
            {
                base.Disposed += value;
            }
            remove
            {
                base.Disposed -= value;
            }
        }

        [y("DbDataTable_FetchFinished"), Category("Fill")]
        public event EventHandler FetchFinished
        {
            add
            {
                this.Events.AddHandler(al, value);
            }
            remove
            {
                this.Events.RemoveHandler(al, value);
            }
        }

        [Category("Fill"), y("DbDataTable_FillError")]
        public event FillErrorEventHandler FillError
        {
            add
            {
                this.Events.AddHandler(ai, value);
            }
            remove
            {
                this.Events.RemoveHandler(ai, value);
            }
        }

        internal event EventHandler ListChanged
        {
            add
            {
                this.Events.AddHandler(aj, A_0);
            }
            remove
            {
                this.Events.RemoveHandler(aj, A_0);
            }
        }

        [Category("Fill"), y("DbDataTable_RowFetched")]
        public event EventHandler RowFetched
        {
            add
            {
                this.Events.AddHandler(ak, value);
            }
            remove
            {
                this.Events.RemoveHandler(ak, value);
            }
        }

        event EventHandler ISupportInitializeNotification.Initialized
        {
            add
            {
                this.Events.AddHandler(am, value);
            }
            remove
            {
                this.Events.RemoveHandler(am, value);
            }
        }

        protected DbDataTable()
        {
            this.h = true;
            this.r = true;
            this.t = new object();
            this.ad = true;
            this.ah = true;
            this.ap = System.Data.MissingSchemaAction.AddWithKey;
            this.@as = true;
            this.indexOfColumnOnlyOriginalValue = -1;
            this.av = string.Empty;
            this.dataTable = this;
            this.dataTable.Columns.CollectionChanged += new CollectionChangeEventHandler(this.a);
            this.dataTable.Constraints.CollectionChanged += new CollectionChangeEventHandler(this.b);
            this.z = new Devart.Common.z(null, (Control) this.Owner, new Devart.Common.o(this.a));
            this.q = new DataTableMapping();
            this.l = -1;
            this.m = true;
            this.o = new EventHandler(this.d);
        }

        protected DbDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.h = true;
            this.r = true;
            this.t = new object();
            this.ad = true;
            this.ah = true;
            this.ap = System.Data.MissingSchemaAction.AddWithKey;
            this.@as = true;
            this.indexOfColumnOnlyOriginalValue = -1;
            this.av = string.Empty;
            this.dataTable = this;
            this.dataTable.Columns.CollectionChanged += new CollectionChangeEventHandler(this.a);
            this.dataTable.Constraints.CollectionChanged += new CollectionChangeEventHandler(this.b);
            this.q = new DataTableMapping();
            this.l = -1;
            this.m = true;
            this.o = new EventHandler(this.d);
            this.z = new Devart.Common.z(null, (Control) this.Owner, new Devart.Common.o(this.a));
        }

        private void a()
        {
            DbConnection connection = this.GetConnection();
            if ((connection == null) && (this.currenSelectCommand != null))
            {
                connection = this.currenSelectCommand.Connection;
            }
            if (connection != null)
            {
                if (this.currenSelectCommand != null)
                {
                    this.currenSelectCommand.Connection ??= connection;
                }
                if (this.d != null)
                {
                    this.d.Connection ??= connection;
                }
                if (this.e != null)
                {
                    this.e.Connection ??= connection;
                }
                if (this.f != null)
                {
                    this.f.Connection ??= connection;
                }
            }
        }

        internal DbDataTable a(DbDataTable A_0)
        {
            A_0.Connection = this.Connection;
            A_0.StartRecord = this.StartRecord;
            A_0.MaxRecords = this.MaxRecords;
            A_0.SelectCommand = this.CloneCommand(this.SelectCommand);
            A_0.InsertCommand = this.CloneCommand(this.InsertCommand);
            A_0.UpdateCommand = this.CloneCommand(this.UpdateCommand);
            A_0.DeleteCommand = this.CloneCommand(this.DeleteCommand);
            if ((A_0.DataSet == null) || ReferenceEquals(A_0.GetType().BaseType.BaseType, typeof(DbDataTable)))
            {
                A_0.TableMapping.DataSetTable = this.TableMapping.DataSetTable;
                A_0.TableMapping.SourceTable = this.TableMapping.SourceTable;
                int count = this.TableMapping.ColumnMappings.Count;
                if (A_0.TableMapping.ColumnMappings.Count != count)
                {
                    for (int i = 0; i < count; i++)
                    {
                        DataColumnMapping mapping = this.TableMapping.ColumnMappings[i];
                        DataColumnMapping mapping2 = new DataColumnMapping(mapping.SourceColumn, mapping.DataSetColumn);
                        A_0.TableMapping.ColumnMappings.Add(mapping2);
                    }
                }
            }
            A_0.ReturnProviderSpecificTypes = this.ReturnProviderSpecificTypes;
            A_0.CachedUpdates = this.CachedUpdates;
            A_0.NonBlocking = this.NonBlocking;
            A_0.QueryRecordCount = this.QueryRecordCount;
            A_0.RefreshMode = this.RefreshMode;
            A_0.RefreshingFields = this.RefreshingFields;
            A_0.Quoted = this.Quoted;
            A_0.UpdatingTable = this.UpdatingTable;
            A_0.FetchAll = this.FetchAll;
            return A_0;
        }

        internal void a(DbDataTableView A_0)
        {
            this.az ??= new ArrayList();
            lock (this.az)
            {
                int index = this.az.Count - 1;
                while (true)
                {
                    if (index < 0)
                    {
                        this.az.Add(new WeakReference(A_0));
                        break;
                    }
                    WeakReference weakReference = (WeakReference) this.az[index];
                    if (!Utils.GetWeakIsAlive(weakReference))
                    {
                        this.az.RemoveAt(index);
                    }
                    else if (Utils.GetWeakTarget(weakReference) == A_0)
                    {
                        break;
                    }
                    index--;
                }
            }
        }

        private void a(bool A_0)
        {
            if (this.n != null)
            {
                this.n.m();
            }
            if (A_0)
            {
                this.CloseReader();
            }
            this.ClearSchemaTableCache();
            if (A_0 && ((base.DataSet != null) && (base.DataSet.Relations != null)))
            {
                int count = base.DataSet.Relations.Count;
                int num2 = 0;
                while (num2 < count)
                {
                    DataRelation relation = base.DataSet.Relations[num2];
                    if (ReferenceEquals(relation.ChildTable, this))
                    {
                        for (int i = 0; i < relation.ChildColumns.Length; i++)
                        {
                            if (this.Columns.IndexOf(relation.ChildColumns[i]) >= 0)
                            {
                                A_0 = false;
                                break;
                            }
                        }
                    }
                    if (A_0)
                    {
                        if (ReferenceEquals(relation.ParentTable, this))
                        {
                            for (int i = 0; i < relation.ParentColumns.Length; i++)
                            {
                                if (this.Columns.IndexOf(relation.ParentColumns[i]) >= 0)
                                {
                                    A_0 = false;
                                    break;
                                }
                            }
                        }
                        if (A_0)
                        {
                            num2++;
                            continue;
                        }
                    }
                    break;
                }
            }
            if (!A_0)
            {
                if (base.DataSet == null)
                {
                    base.Clear();
                }
                else
                {
                    bool enforceConstraints = base.DataSet.EnforceConstraints;
                    base.DataSet.EnforceConstraints = false;
                    base.Clear();
                    base.DataSet.EnforceConstraints = enforceConstraints;
                }
            }
            else
            {
                this.disableEvents++;
                try
                {
                    DataColumn[] array = null;
                    DataColumn[] primaryKey = null;
                    Constraint[] constraintArray = null;
                    if (this.UserDefinedColumns)
                    {
                        primaryKey = this.PrimaryKey;
                        base.PrimaryKey = null;
                        array = new DataColumn[this.Columns.Count];
                        this.Columns.CopyTo(array, 0);
                        constraintArray = new Constraint[this.Constraints.Count];
                        this.Constraints.CopyTo(constraintArray, 0);
                    }
                    this.y = true;
                    if ((this.a != null) && (this.a is Control))
                    {
                        BindingManagerBase base2 = ((Control) this.a).BindingContext[this];
                        FieldInfo field = base2.GetType().GetField("onCurrentItemChangedHandler", BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (field != null)
                        {
                            MulticastDelegate delegate2 = (MulticastDelegate) field.GetValue(base2);
                            if (delegate2 != null)
                            {
                                Delegate[] invocationList = delegate2.GetInvocationList();
                                for (int i = invocationList.Length - 1; i >= 0; i--)
                                {
                                    Delegate delegate3 = (MulticastDelegate) invocationList[i];
                                    if (delegate3.Target.GetType().Name == "RelatedCurrencyManager")
                                    {
                                        delegate2 -= delegate3;
                                    }
                                }
                            }
                            field.SetValue(base2, delegate2);
                        }
                    }
                    base.PrimaryKey = null;
                    this.Constraints.Clear();
                    this.Columns.Clear();
                    base.Clear();
                    this.Reset();
                    if (array != null)
                    {
                        this.Columns.AddRange(array);
                    }
                    if (primaryKey != null)
                    {
                        base.PrimaryKey = primaryKey;
                    }
                    if (constraintArray != null)
                    {
                        int index = 0;
                        while (index < constraintArray.Length)
                        {
                            Constraint constraint = constraintArray[index];
                            bool flag = false;
                            int count = this.Constraints.Count;
                            int num8 = 0;
                            while (true)
                            {
                                if (num8 < count)
                                {
                                    if (!this.Constraints[num8].Equals(constraint))
                                    {
                                        num8++;
                                        continue;
                                    }
                                    flag = true;
                                }
                                if (!flag)
                                {
                                    this.Constraints.Add(constraint);
                                }
                                index++;
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    this.disableEvents--;
                }
                this.UserDefinedColumns = false;
            }
            this.ResetFetchPosition();
            this.currenSelectCommand = null;
            if (this.detailSelectCommand != null)
            {
                this.detailSelectCommand.Dispose();
                this.detailSelectCommand = null;
            }
            this.i = false;
            this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            this.a(new ListChangedEventArgs(ListChangedType.Reset, null));
        }

        internal void a(ListChangedEventArgs A_0)
        {
            if (this.az != null)
            {
                for (int i = this.az.Count - 1; i >= 0; i--)
                {
                    WeakReference weakReference = (WeakReference) this.az[i];
                    if (!Utils.GetWeakIsAlive(weakReference))
                    {
                        this.az.RemoveAt(i);
                    }
                    else
                    {
                        DbDataTableView weakTarget = (DbDataTableView) Utils.GetWeakTarget(weakReference);
                        weakTarget.a(weakTarget, A_0);
                    }
                }
            }
        }

        private DbCommand a(DbConnection A_0)
        {
            string str;
            if (!ParentDataRelation.a(this.aa))
            {
                return null;
            }
            this.aa.a();
            if (this.c.CommandType == CommandType.StoredProcedure)
            {
                return null;
            }
            if (this.c.CommandType == CommandType.TableDirect)
            {
                str = "SELECT * FROM " + this.c.CommandText + " WHERE ";
            }
            else
            {
                char[] trimChars = new char[] { ';' };
                str = this.c.CommandText.TrimEnd(trimChars);
            }
            PropertyDescriptorCollection itemProperties = ((ITypedList) ((IListSource) this.aa.ParentTable).GetList()).GetItemProperties(null);
            if (this.z.j() >= 0)
            {
                DbDataRowView view1 = (DbDataRowView) ((IListSource) this.aa.ParentTable).GetList()[this.z.j()];
            }
            string whereText = "";
            for (int i = 0; i < this.aa.ParentColumnNames.Length; i++)
            {
                PropertyDescriptor descriptor = a(itemProperties, this.aa.ParentColumnNames[i]);
                if (descriptor == null)
                {
                    throw new Exception("Cannot establish master/detail relation");
                }
                if (this.c.Parameters.IndexOf(descriptor.Name) < 0)
                {
                    if (whereText != "")
                    {
                        whereText = whereText + " AND ";
                    }
                    string str3 = this.aa.ChildColumnNames[i];
                    if ((str3 == null) || (str3 == ""))
                    {
                        throw new Exception("Cannot establish master/detail relation");
                    }
                    string[] textArray1 = new string[] { whereText, this.aa.ChildColumnNames[i], " = ", this.GetParameterPlaceholder().ToString(), descriptor.Name };
                    whereText = string.Concat(textArray1);
                }
            }
            DbCommandBase base2 = (DbCommandBase) A_0.CreateCommand();
            if (this.c.CommandType == CommandType.TableDirect)
            {
                str = str + whereText;
            }
            else if (whereText != "")
            {
                str = this.AddWhere(str, whereText);
            }
            base2.CommandText = str;
            base2.CreateParameters();
            if (this.c.Parameters.Count > 0)
            {
                for (int j = 0; j < this.c.Parameters.Count; j++)
                {
                    DbParameter parameter = this.c.Parameters[j];
                    int index = base2.Parameters.IndexOf(parameter.ParameterName);
                    if ((index >= 0) && (parameter is ICloneable))
                    {
                        base2.Parameters[index] = (DbParameter) ((ICloneable) parameter).Clone();
                    }
                }
            }
            return base2;
        }

        private static DataColumn a(DataColumn A_0)
        {
            DataColumn column = new DataColumn {
                AllowDBNull = A_0.AllowDBNull,
                AutoIncrement = A_0.AutoIncrement,
                AutoIncrementStep = A_0.AutoIncrementStep,
                AutoIncrementSeed = A_0.AutoIncrementSeed,
                Caption = A_0.Caption,
                ColumnName = A_0.ColumnName,
                Prefix = A_0.Prefix,
                DataType = A_0.DataType,
                DefaultValue = A_0.DefaultValue,
                ColumnMapping = A_0.ColumnMapping,
                ReadOnly = A_0.ReadOnly,
                MaxLength = A_0.MaxLength,
                DateTimeMode = A_0.DateTimeMode
            };
            if (A_0.ExtendedProperties != null)
            {
                foreach (object obj2 in A_0.ExtendedProperties.Keys)
                {
                    column.ExtendedProperties[obj2] = A_0.ExtendedProperties[obj2];
                }
            }
            return column;
        }

        private int a(DataRow A_0)
        {
            int num;
            this.CommandBuilderInternal.SetAllValues = ((this.n != null) && (this.n.CurrentIndex >= 0)) && this.hasComplexFields;
            this.BeforeUpdatingRow(A_0);
            DataRow[] dataRows = new DataRow[] { A_0 };
            this.a();
            try
            {
                num = this.DataAdapterInternal.Update(dataRows);
            }
            finally
            {
                this.AfterUpdatingRow(A_0);
            }
            return num;
        }

        private static ForeignKeyConstraint a(ForeignKeyConstraint A_0)
        {
            string[] parentColumnNames = new string[A_0.RelatedColumns.Length];
            for (int i = 0; i < A_0.RelatedColumns.Length; i++)
            {
                parentColumnNames[i] = A_0.RelatedColumns[i].ColumnName;
            }
            string[] childColumnNames = new string[A_0.Columns.Length];
            for (int j = 0; j < A_0.Columns.Length; j++)
            {
                parentColumnNames[j] = A_0.Columns[j].ColumnName;
            }
            ForeignKeyConstraint constraint = new ForeignKeyConstraint(A_0.ConstraintName, A_0.RelatedTable.TableName, A_0.RelatedTable.Namespace, parentColumnNames, childColumnNames, A_0.AcceptRejectRule, A_0.DeleteRule, A_0.UpdateRule);
            foreach (object obj2 in A_0.ExtendedProperties.Keys)
            {
                constraint.ExtendedProperties[obj2] = A_0.ExtendedProperties[obj2];
            }
            return constraint;
        }

        private static UniqueConstraint a(UniqueConstraint A_0)
        {
            string[] columnNames = new string[A_0.Columns.Length];
            for (int i = 0; i < A_0.Columns.Length; i++)
            {
                columnNames[i] = A_0.Columns[i].ColumnName;
            }
            UniqueConstraint constraint = new UniqueConstraint(A_0.ConstraintName, columnNames, A_0.IsPrimaryKey);
            foreach (object obj2 in A_0.ExtendedProperties.Keys)
            {
                constraint.ExtendedProperties[obj2] = A_0.ExtendedProperties[obj2];
            }
            return constraint;
        }

        private void a(CultureInfo A_0)
        {
            lock (this.t)
            {
                Thread.CurrentThread.CurrentCulture = A_0;
                this.a(0x7ffffffe, false, false);
                this.p = null;
                this.c(this, null);
            }
        }

        internal DataRow a(int A_0)
        {
            this.FetchToPosition(A_0);
            return ((A_0 < this.dataTable.Rows.Count) ? this.dataTable.Rows[A_0] : null);
        }

        internal System.Type a(System.Type A_0) => 
            this.GetPropertyType(A_0);

        private void a(PropertyDescriptor[] A_0)
        {
            PropertyDescriptorCollection propertyDescriptors = new PropertyDescriptorCollection(null);
            if ((A_0 == null) || (A_0.Length == 0))
            {
                int count = this.Columns.Count;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= count)
                    {
                        int num2 = base.ChildRelations.Count;
                        for (int i = 0; i < num2; i++)
                        {
                            Devart.Common.d d = new Devart.Common.d(base.ChildRelations[i]);
                            propertyDescriptors.Add(d);
                        }
                        break;
                    }
                    DataColumn dataColumn = this.Columns[num3];
                    this.AddPropertyDescriptor(propertyDescriptors, dataColumn);
                    num3++;
                }
            }
            this.propertyDescriptorsCache = propertyDescriptors;
            this.y = false;
        }

        private void a(object[] A_0)
        {
            if (A_0 != null)
            {
                if (this.currenSelectCommand == null)
                {
                    throw new InvalidOperationException(Devart.Common.g.a("SelectCommandNotInit"));
                }
                for (int i = 0; i < A_0.Length; i++)
                {
                    a(this.currenSelectCommand, i).Value = A_0[i];
                }
            }
        }

        internal static void a(DataTable A_0, DbDataTable A_1)
        {
            A_1.TableName = A_0.TableName;
            A_1.Namespace = A_0.Namespace;
            A_1.Prefix = A_0.Prefix;
            A_1.Locale = A_0.Locale;
            A_1.CaseSensitive = A_0.CaseSensitive;
            A_1.DisplayExpression = A_0.DisplayExpression;
            A_1.MinimumCapacity = A_0.MinimumCapacity;
            A_1.RemotingFormat = A_0.RemotingFormat;
            DataColumnCollection columns = A_0.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                A_1.Columns.Add(a(columns[i]));
            }
            for (int j = 0; j < columns.Count; j++)
            {
                A_1.Columns[columns[j].ColumnName].Expression = columns[j].Expression;
            }
            DataColumn[] primaryKey = A_0.PrimaryKey;
            if (primaryKey.Length != 0)
            {
                DataColumn[] columnArray2 = new DataColumn[primaryKey.Length];
                int index = 0;
                while (true)
                {
                    if (index >= primaryKey.Length)
                    {
                        A_1.PrimaryKey = columnArray2;
                        break;
                    }
                    columnArray2[index] = A_1.Columns[primaryKey[index].Ordinal];
                    index++;
                }
            }
            for (int k = 0; k < A_0.Constraints.Count; k++)
            {
                ForeignKeyConstraint constraint = A_0.Constraints[k] as ForeignKeyConstraint;
                UniqueConstraint constraint2 = A_0.Constraints[k] as UniqueConstraint;
                if (constraint != null)
                {
                    if (ReferenceEquals(constraint.Table, constraint.RelatedTable))
                    {
                        A_1.Constraints.Add(a(constraint));
                    }
                }
                else if (constraint2 != null)
                {
                    string[] columnNames = new string[constraint2.Columns.Length];
                    int index = 0;
                    while (true)
                    {
                        if (index >= constraint2.Columns.Length)
                        {
                            UniqueConstraint constraint3 = new UniqueConstraint(constraint2.ConstraintName, columnNames, constraint2.IsPrimaryKey);
                            foreach (object obj2 in constraint2.ExtendedProperties.Keys)
                            {
                                constraint3.ExtendedProperties[obj2] = constraint2.ExtendedProperties[obj2];
                            }
                            A_1.Constraints.Add(a(constraint2));
                            break;
                        }
                        columnNames[index] = constraint2.Columns[index].ColumnName;
                        index++;
                    }
                }
            }
            for (int m = 0; m < A_0.Constraints.Count; m++)
            {
                if (!A_1.Constraints.Contains(A_0.Constraints[m].ConstraintName))
                {
                    ForeignKeyConstraint constraint4 = A_0.Constraints[m] as ForeignKeyConstraint;
                    UniqueConstraint constraint5 = A_0.Constraints[m] as UniqueConstraint;
                    if (constraint4 != null)
                    {
                        if (ReferenceEquals(constraint4.Table, constraint4.RelatedTable))
                        {
                            A_1.Constraints.Add(a(constraint4));
                        }
                    }
                    else if (constraint5 != null)
                    {
                        A_1.Constraints.Add(a(constraint5));
                    }
                }
            }
            if (A_0.ExtendedProperties != null)
            {
                foreach (object obj3 in A_0.ExtendedProperties.Keys)
                {
                    A_1.ExtendedProperties[obj3] = A_0.ExtendedProperties[obj3];
                }
            }
        }

        internal void a(ref DataRow A_0, bool A_1)
        {
            if (A_1)
            {
                this.j();
            }
            if (!this.r)
            {
                this.a(A_0, DataRowAction.Add);
            }
            DataRowChangeEventArgs e = new DataRowChangeEventArgs(A_0, DataRowAction.Add);
            this.OnRowChanging(e);
            this.disableEvents++;
            this.disableUpdateEvents++;
            try
            {
                DataRow row = null;
                object[] objArray = DbDataRowView.a(A_0, true);
                try
                {
                    if (!this.r)
                    {
                        this.dataTable.BeginLoadData();
                    }
                    try
                    {
                        if (this.r)
                        {
                            this.storeEvents++;
                            try
                            {
                                this.dataTable.Rows.Add(A_0);
                            }
                            finally
                            {
                                this.storeEvents--;
                            }
                            goto TR_0016;
                        }
                        else
                        {
                            this.storeEvents++;
                            try
                            {
                                row = this.dataTable.Rows.Add(DbDataRowView.a(A_0, true));
                            }
                            finally
                            {
                                this.storeEvents--;
                            }
                        }
                        try
                        {
                            this.storeEvents++;
                            try
                            {
                                this.a(row);
                            }
                            finally
                            {
                                this.storeEvents--;
                            }
                            b(A_0, row);
                        }
                        catch
                        {
                            throw;
                        }
                        this.dataTable.Rows.Add(A_0);
                    }
                    finally
                    {
                        if (!this.r && (row != null))
                        {
                            row.Delete();
                            if (row.RowState == DataRowState.Deleted)
                            {
                                row.AcceptChanges();
                            }
                            if (base.DefaultView.Count == 0)
                            {
                                DataRowView view1 = base.DefaultView.AddNew();
                                DataRow row2 = view1.Row;
                                view1.BeginEdit();
                                row2.BeginEdit();
                                row2.ItemArray = objArray;
                                A_0 = row2;
                            }
                        }
                    }
                }
                finally
                {
                    if (!this.r)
                    {
                        this.storeEvents++;
                        try
                        {
                            this.dataTable.EndLoadData();
                        }
                        finally
                        {
                            this.storeEvents--;
                        }
                    }
                }
            }
            finally
            {
                this.disableEvents--;
                this.disableUpdateEvents--;
            }
        TR_0016:
            if (!this.r)
            {
                A_0.AcceptChanges();
            }
            this.OnRowChanged(e);
            this.k++;
            if (!this.FetchComplete)
            {
                this.l++;
            }
        }

        private static bool a(ICollection<string> A_0, string A_1)
        {
            bool flag;
            using (IEnumerator<string> enumerator = A_0.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        if (string.Compare(A_1, current, StringComparison.InvariantCultureIgnoreCase) != 0)
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

        private static PropertyDescriptor a(PropertyDescriptorCollection A_0, string A_1)
        {
            PropertyDescriptor descriptor = null;
            if (A_0 != null)
            {
                int num = 0;
                while (true)
                {
                    if (num < A_0.Count)
                    {
                        if (string.Compare(A_0[num].Name, A_1, false) != 0)
                        {
                            num++;
                            continue;
                        }
                        descriptor = A_0[num];
                    }
                    if (descriptor != null)
                    {
                        return descriptor;
                    }
                    for (int i = 0; i < A_0.Count; i++)
                    {
                        if (string.Compare(A_0[i].Name, A_1, true) == 0)
                        {
                            if (descriptor != null)
                            {
                                descriptor = null;
                                break;
                            }
                            descriptor = A_0[i];
                        }
                    }
                    break;
                }
            }
            return descriptor;
        }

        private static IDataParameter a(DbCommand A_0, int A_1)
        {
            if (A_1 < A_0.Parameters.Count)
            {
                return A_0.Parameters[A_1];
            }
            IDataParameter parameter = A_0.CreateParameter();
            A_0.Parameters.Add(parameter);
            return parameter;
        }

        private void a(DataRow A_0, DataRow A_1)
        {
            DbDataTableView weakTarget;
            int num2;
            int num3;
            if (this.az == null)
            {
                return;
            }
            int index = this.az.Count - 1;
            goto TR_0015;
        TR_0002:
            index--;
        TR_0015:
            while (true)
            {
                if (index < 0)
                {
                    return;
                }
                WeakReference weakReference = (WeakReference) this.az[index];
                if (Utils.GetWeakIsAlive(weakReference))
                {
                    weakTarget = (DbDataTableView) Utils.GetWeakTarget(weakReference);
                    num2 = -1;
                    num3 = -1;
                    for (int i = ((weakTarget.g == null) ? 0 : weakTarget.g.Count) - 1; i >= 0; i--)
                    {
                        DataRowView view1 = weakTarget.g[i];
                        if (ReferenceEquals(view1.Row, A_1))
                        {
                            num2 = i;
                        }
                        if (ReferenceEquals(view1.Row, A_0))
                        {
                            num3 = i;
                        }
                        if ((num2 != -1) && (num3 != -1))
                        {
                            break;
                        }
                    }
                }
                else
                {
                    this.az.RemoveAt(index);
                    goto TR_0002;
                }
                break;
            }
            if ((num2 != -1) && (num3 != -1))
            {
                weakTarget.a(num3, num2);
            }
            else if ((num2 != -1) && (num3 == -1))
            {
                weakTarget.c(num2);
            }
            goto TR_0002;
        }

        private void a(DataRow A_0, DataRowAction A_1)
        {
            try
            {
                if (A_0.HasVersion(DataRowVersion.Proposed))
                {
                    int count = this.Columns.Count;
                    if (count > 0)
                    {
                        MethodInfo method = typeof(DataColumn).GetMethod("CheckColumnConstraint", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (method != null)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                DataColumn column = this.Columns[i];
                                if ((column.Expression == "") || (A_1 != DataRowAction.Add))
                                {
                                    object[] parameters = new object[] { A_0, A_1 };
                                    method.Invoke(column, parameters);
                                }
                            }
                        }
                    }
                    int num2 = this.Constraints.Count;
                    if (num2 > 0)
                    {
                        System.Type[] types = new System.Type[] { typeof(DataRow), typeof(DataRowAction) };
                        MethodInfo info2 = typeof(Constraint).GetMethod("CheckConstraint", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
                        if (info2 != null)
                        {
                            for (int i = 0; i < num2; i++)
                            {
                                object[] parameters = new object[] { A_0, A_1 };
                                info2.Invoke(this.Constraints[i], parameters);
                            }
                        }
                    }
                }
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }
                throw;
            }
        }

        private int a(int A_0, int A_1)
        {
            int num;
            ConnectionState open = ConnectionState.Open;
            DbCommand selectCommand = this.dataAdapter.SelectCommand;
            try
            {
                if ((selectCommand.Connection != null) && (selectCommand.Connection.State == ConnectionState.Closed))
                {
                    selectCommand.Connection.Open();
                }
                this.Clear();
                this.i = true;
                if (this.s)
                {
                    this.k = this.GetRecordCount();
                }
                num = this.FillPage(A_0, A_1, null);
            }
            finally
            {
                if (open == ConnectionState.Closed)
                {
                    selectCommand.Connection.Close();
                }
            }
            return num;
        }

        private void a(object A_0, CollectionChangeEventArgs A_1)
        {
            if (this.disableEvents <= 0)
            {
                this.readerMappings = null;
                this.CheckReaderMappings();
                if (A_1.Action == CollectionChangeAction.Add)
                {
                    this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, this.Columns.Count - 1));
                }
                this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, this.Columns.Count - 1));
            }
        }

        private void a(object A_0, FillErrorEventArgs A_1)
        {
            FillErrorEventHandler handler = (FillErrorEventHandler) this.Events[ai];
            if (handler != null)
            {
                handler(this, A_1);
            }
        }

        private void a(object A_0, EventArgs A_1)
        {
            if (!base.fInitInProgress)
            {
                EventHandler handler = (EventHandler) this.Events[aj];
                if (handler != null)
                {
                    handler(A_0, null);
                }
            }
        }

        private void a(object A_0, int A_1)
        {
            if (this.i && !this.j)
            {
                if (!ParentDataRelation.a(this.aa) || ((this.aa.ChildColumnNames == null) || ((this.aa.ChildColumnNames.Length == 0) || ((this.aa.ParentColumnNames == null) || (this.aa.ParentColumnNames.Length == 0)))))
                {
                    this.Close();
                }
                else
                {
                    this.aa.a();
                    this.disableEvents++;
                    try
                    {
                        this.b();
                    }
                    finally
                    {
                        this.disableEvents--;
                    }
                    this.ExecuteCommand();
                    this.CheckReaderMappings();
                    if (this.n != null)
                    {
                        this.n.j();
                    }
                    this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
                    this.c();
                }
            }
        }

        internal void a(int A_0, bool A_1, bool A_2)
        {
            if (A_2 || (this.w <= 0))
            {
                while (A_0 > this.l)
                {
                    if (Monitor.TryEnter(this.t))
                    {
                        if (A_0 > this.l)
                        {
                            try
                            {
                                if (!this.FetchComplete)
                                {
                                    if (this.a2 > 0)
                                    {
                                        throw new InvalidOperationException("Cannot fetch rows in this state.");
                                    }
                                    DataRow row = null;
                                    this.at++;
                                    try
                                    {
                                        bool m = this.m;
                                        Interlocked.Exchange(ref this.ae, 0);
                                        while ((A_0 > this.l) && !this.FetchComplete)
                                        {
                                            lock (this.FetchRowSyncRoot)
                                            {
                                                if (this.m)
                                                {
                                                    bool flag3;
                                                    this.m = false;
                                                    try
                                                    {
                                                        flag3 = this.reader.Read();
                                                    }
                                                    catch
                                                    {
                                                        this.CancelFetch();
                                                        throw;
                                                    }
                                                    if (!flag3)
                                                    {
                                                        DataTable schemaTable = this.GetSchemaTable();
                                                        this.i();
                                                        this.FetchCompleted(schemaTable);
                                                        this.k = this.l + 1;
                                                        if (A_1)
                                                        {
                                                            this.c(this, null);
                                                        }
                                                        break;
                                                    }
                                                }
                                                bool flag2 = false;
                                                try
                                                {
                                                    row = null;
                                                    row = this.dataTable.NewRow();
                                                    this.GetDataRow(row);
                                                }
                                                catch (Exception exception2)
                                                {
                                                    if (!IsCatchableExceptionType(exception2) || !this.RaiseFillError(ref exception2, row?.ItemArray))
                                                    {
                                                        throw;
                                                    }
                                                    flag2 = true;
                                                }
                                                Exception exception = null;
                                                try
                                                {
                                                    if (!this.reader.Read())
                                                    {
                                                        DataTable schemaTable = this.GetSchemaTable();
                                                        try
                                                        {
                                                            this.i();
                                                        }
                                                        catch (Exception exception4)
                                                        {
                                                            exception = exception4;
                                                        }
                                                        this.FetchCompleted(schemaTable);
                                                    }
                                                }
                                                catch
                                                {
                                                    this.CancelFetch();
                                                    throw;
                                                }
                                                if (!flag2)
                                                {
                                                    if (m)
                                                    {
                                                        this.l++;
                                                        if (this.FetchComplete || !this.s)
                                                        {
                                                            this.k = this.l + 1;
                                                        }
                                                    }
                                                    this.disableEvents++;
                                                    try
                                                    {
                                                        int num = 0;
                                                        while (true)
                                                        {
                                                            if (num >= 2)
                                                            {
                                                                if (!flag2)
                                                                {
                                                                    row.AcceptChanges();
                                                                }
                                                                break;
                                                            }
                                                            try
                                                            {
                                                                flag2 = false;
                                                                this.dataTable.Rows.Add(row);
                                                                num = 2;
                                                            }
                                                            catch (Exception exception3)
                                                            {
                                                                num++;
                                                                if (!IsCatchableExceptionType(exception3) || !this.RaiseFillError(ref exception3, row.ItemArray))
                                                                {
                                                                    throw;
                                                                }
                                                                flag2 = true;
                                                                if ((num == 2) & m)
                                                                {
                                                                    this.l--;
                                                                    if (this.FetchComplete || !this.s)
                                                                    {
                                                                        this.k = this.l + 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    finally
                                                    {
                                                        this.disableEvents--;
                                                    }
                                                    if (!flag2)
                                                    {
                                                        if (((!m || this.FetchComplete) & A_1) && ((this.aq == 0) && (!this.s && ((this.n != null) && (this.at < 20)))))
                                                        {
                                                            int count = this.dataTable.Rows.Count;
                                                            this.n.a(this, new ListChangedEventArgs(ListChangedType.ItemAdded, count - 1));
                                                            if ((this.at == 0x13) && (count < this.dataTable.Rows.Count))
                                                            {
                                                                this.n.a(this, new ListChangedEventArgs(ListChangedType.ItemAdded, this.dataTable.Rows.Count - 1));
                                                            }
                                                        }
                                                        if (!m)
                                                        {
                                                            this.l++;
                                                            if (this.FetchComplete || !this.s)
                                                            {
                                                                this.k = this.l + 1;
                                                            }
                                                        }
                                                        this.b(this, (EventArgs) null);
                                                    }
                                                }
                                                if ((this.FetchComplete & A_1) && (this.aq == 0))
                                                {
                                                    this.c(this, null);
                                                }
                                                if (this.ae == 0)
                                                {
                                                    if (exception != null)
                                                    {
                                                        throw exception;
                                                    }
                                                    continue;
                                                }
                                                Interlocked.Exchange(ref this.ae, 0);
                                            }
                                            break;
                                        }
                                    }
                                    finally
                                    {
                                        this.at--;
                                    }
                                }
                            }
                            finally
                            {
                                Monitor.Exit(this.t);
                            }
                        }
                        return;
                    }
                }
            }
        }

        internal static ITypedList a(object A_0, string A_1, out object A_2)
        {
            ITypedList list = null;
            list = !(A_0 is IListSource) ? ((ITypedList) A_0) : ((ITypedList) (A_0 as IListSource).GetList());
            A_2 = null;
            DbDataSet set = A_0 as DbDataSet;
            if (set != null)
            {
                A_2 = set.Owner;
            }
            DbDataTable table = A_0 as DbDataTable;
            if (table != null)
            {
                A_2 = table.Owner;
            }
            DataLink link = A_0 as DataLink;
            if (link != null)
            {
                A_2 = link.Owner;
            }
            ITypedList list1 = a(list, A_1, "", ref A_2, A_0);
            DbDataTableView view = list1 as DbDataTableView;
            if ((view != null) && ((A_2 == null) && (view.f != null)))
            {
                A_2 = view.f.Owner;
            }
            return list1;
        }

        internal object a(object A_0, System.Type A_1, IEditableObject A_2, PropertyDescriptor A_3) => 
            this.GetViewValue(A_0, A_1, A_2, A_3);

        private static ITypedList a(ITypedList A_0, string A_1, string A_2, ref object A_3, object A_4)
        {
            if ((A_1 == null) || ((A_1 == "") || (A_0 == null)))
            {
                return A_0;
            }
            string str = "";
            int index = A_1.IndexOf('.');
            if (index >= 0)
            {
                str = A_1.Substring(index + 1);
                A_1 = A_1.Substring(0, index);
                if (A_1 == "")
                {
                    return null;
                }
            }
            PropertyDescriptorCollection itemProperties = A_0.GetItemProperties(null);
            for (int i = 0; i < itemProperties.Count; i++)
            {
                PropertyDescriptor descriptor = itemProperties[i];
                if (descriptor.Name == A_1)
                {
                    DataViewManagerPropertyDescriptor descriptor2 = descriptor as DataViewManagerPropertyDescriptor;
                    if (descriptor2 != null)
                    {
                        DbDataTableView view = (DbDataTableView) descriptor2.GetValue(((IList) A_0)[0]);
                        if ((A_3 == null) && (view.f != null))
                        {
                            A_3 = view.f.Owner;
                        }
                        return a(view, str, (A_2 == "") ? A_1 : (A_2 + "." + A_1), ref A_3, A_4);
                    }
                    Devart.Common.d d = descriptor as Devart.Common.d;
                    if ((d == null) || (d.c() == null))
                    {
                        return null;
                    }
                    DbDataTable parentTable = d.c().ParentTable as DbDataTable;
                    if ((A_3 == null) && (parentTable != null))
                    {
                        A_3 = parentTable.Owner;
                    }
                    int position = 0;
                    if (A_3 != null)
                    {
                        CurrencyManager manager = (CurrencyManager) ((Control) A_3).BindingContext[A_4, A_2];
                        if (manager != null)
                        {
                            position = manager.Position;
                        }
                    }
                    return a((ITypedList) d.GetValue(((IList) A_0)[position]), str, (A_2 == "") ? A_1 : (A_2 + "." + A_1), ref A_3, A_4);
                }
            }
            return null;
        }

        protected virtual void AddPropertyDescriptor(PropertyDescriptorCollection propertyDescriptors, DataColumn dataColumn)
        {
            Devart.Common.d d = null;
            if (this.propertyDescriptorsCache != null)
            {
                d = this.propertyDescriptorsCache.Find(dataColumn.ColumnName, false) as Devart.Common.d;
                if ((d != null) && !ReferenceEquals(d.a(), dataColumn))
                {
                    d.a(dataColumn);
                }
            }
            d = new Devart.Common.d(dataColumn);
            propertyDescriptors.Add(d);
        }

        protected virtual string AddWhere(string commandText, string whereText)
        {
            throw new Exception();
        }

        protected virtual void AfterUpdatingRow(DataRow row)
        {
        }

        private void b()
        {
            if (this.n != null)
            {
                this.n.m();
            }
            this.CloseReader();
            this.disableEvents++;
            try
            {
                this.y = true;
                base.Clear();
            }
            finally
            {
                this.disableEvents--;
            }
            this.l = -1;
            this.m = true;
            this.k = 0;
            this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            this.a(new ListChangedEventArgs(ListChangedType.Reset, null));
        }

        internal PropertyDescriptorCollection b(PropertyDescriptor[] A_0)
        {
            PropertyDescriptorCollection descriptors = new PropertyDescriptorCollection(null);
            for (int i = 0; i < this.Columns.Count; i++)
            {
                Devart.Common.d d = new Devart.Common.d(this.Columns[i]);
                descriptors.Add(d);
            }
            return descriptors;
        }

        internal void b(bool A_0)
        {
            if (!this.FetchComplete)
            {
                bool enforceConstraints = false;
                if (base.DataSet != null)
                {
                    enforceConstraints = base.DataSet.EnforceConstraints;
                    base.DataSet.EnforceConstraints = false;
                }
                try
                {
                    int count = base.Rows.Count;
                    this.aq++;
                    try
                    {
                        this.a(0x7fffffff, A_0, false);
                    }
                    finally
                    {
                        if ((base.Rows.Count - count) > 0)
                        {
                            this.l();
                        }
                        else
                        {
                            this.aq--;
                        }
                    }
                    if ((base.Rows.Count - count) > 0)
                    {
                        this.d(A_0);
                    }
                }
                catch (ConstraintException)
                {
                    enforceConstraints = false;
                    throw;
                }
                finally
                {
                    if (enforceConstraints)
                    {
                        base.DataSet.EnforceConstraints = true;
                    }
                }
            }
        }

        internal void b(DataRow A_0)
        {
            if (!ReferenceEquals(this.au, A_0))
            {
                this.au = A_0;
                try
                {
                    if (!this.r)
                    {
                        this.a(A_0, DataRowAction.Change);
                        this.disableEvents++;
                        this.disableUpdateEvents++;
                        DataRow row = null;
                        DbDataRowView.a(A_0, true);
                        try
                        {
                            object dependentColumnsInternal = this.DependentColumnsInternal;
                            this.DependentColumnsInternal = null;
                            try
                            {
                                this.dataTable.BeginLoadData();
                                try
                                {
                                    object[] values = !A_0.HasVersion(DataRowVersion.Original) ? DbDataRowView.a(A_0, true) : DbDataRowView.a(A_0, DataRowVersion.Original);
                                    row = this.dataTable.Rows.Add(values);
                                    row.AcceptChanges();
                                    row.BeginEdit();
                                    object[] objArray2 = DbDataRowView.a(A_0, false);
                                    int index = 0;
                                    while (true)
                                    {
                                        if (index >= objArray2.Length)
                                        {
                                            this.storeEvents++;
                                            try
                                            {
                                                row.EndEdit();
                                                this.a(row);
                                            }
                                            finally
                                            {
                                                this.storeEvents--;
                                                this.a(row, A_0);
                                            }
                                            b(A_0, row);
                                            break;
                                        }
                                        object obj3 = row[index];
                                        object obj4 = objArray2[index];
                                        if (((obj3 != obj4) && ((obj3 == null) || ((obj4 == null) || !obj3.Equals(obj4)))) && !this.Columns[index].ReadOnly)
                                        {
                                            row[index] = objArray2[index];
                                        }
                                        index++;
                                    }
                                }
                                finally
                                {
                                    if (row != null)
                                    {
                                        row.Delete();
                                        if (row.RowState == DataRowState.Deleted)
                                        {
                                            row.AcceptChanges();
                                        }
                                    }
                                    this.disableEvents--;
                                    this.disableUpdateEvents--;
                                }
                            }
                            finally
                            {
                                this.DependentColumnsInternal = dependentColumnsInternal;
                            }
                        }
                        finally
                        {
                            this.dataTable.EndLoadData();
                        }
                        this.disableEvents++;
                        A_0.AcceptChanges();
                        this.disableEvents--;
                    }
                }
                finally
                {
                    this.au = null;
                }
            }
        }

        internal void b(int A_0)
        {
            DataRow row = base.Rows[A_0];
            this.j();
            row.Delete();
            if (!this.r)
            {
                this.a(row);
            }
        }

        private static bool b(DataRow A_0, DataRow A_1)
        {
            bool flag = false;
            DataColumnCollection columns = A_0.Table.Columns;
            object[] objArray = new object[columns.Count];
            for (int i = 0; i < objArray.Length; i++)
            {
                DataColumn column = columns[i];
                if ((A_0[i] != A_1[i]) && ((A_0.RowState == DataRowState.Detached) || !column.ReadOnly))
                {
                    A_0[i] = A_1[i];
                    flag = true;
                }
            }
            return flag;
        }

        private void b(object A_0, CollectionChangeEventArgs A_1)
        {
            if (this.af)
            {
                this.UserDefinedColumns = true;
            }
        }

        private void b(object A_0, EventArgs A_1)
        {
            EventHandler handler = (EventHandler) this.Events[ak];
            if (handler != null)
            {
                handler(A_0, null);
            }
        }

        internal object b(object A_0, System.Type A_1, IEditableObject A_2, PropertyDescriptor A_3) => 
            this.GetPropertyValue(A_0, A_1, A_2, A_3);

        protected virtual void BeforeUpdatingRow(DataRow row)
        {
        }

        public IAsyncResult BeginFill(AsyncCallback callback, object stateObject)
        {
            if (this.p != null)
            {
                InvalidOperationException exception1 = new InvalidOperationException(Devart.Common.g.a("FetchInProgress"));
            }
            this.p = new Devart.Common.DbDataTable.a(this.a);
            if (this.FetchComplete)
            {
                if (this.Active)
                {
                    this.Clear();
                }
                this.CloseReader();
                this.ClearSchemaTableCache();
                this.y = true;
                this.CheckSelectCommand();
                this.ExecuteCommand();
                this.CheckColumnsCreated(false);
                if (this.n != null)
                {
                    this.n.j();
                }
                this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            }
            return this.p.BeginInvoke(Thread.CurrentThread.CurrentCulture, callback, stateObject);
        }

        private void c()
        {
            if (!this.ac)
            {
                this.d(true);
                if (base.ParentRelations.Count > 0)
                {
                    this.FetchToPosition(1);
                }
            }
            else if (!this.ab)
            {
                this.j();
            }
            else
            {
                this.d(true);
                this.BeginFill(null, null);
            }
        }

        internal PropertyDescriptorCollection c(PropertyDescriptor[] A_0) => 
            this.GetProperties(A_0);

        internal void c(DataRow A_0)
        {
            if (!this.AllowCruidDuringFetch)
            {
                this.j();
            }
            if (!this.r)
            {
                object[] values = DbDataRowView.a(A_0, DataRowVersion.Original);
                this.disableEvents++;
                this.disableUpdateEvents++;
                try
                {
                    object dependentColumnsInternal = this.DependentColumnsInternal;
                    this.DependentColumnsInternal = null;
                    try
                    {
                        this.dataTable.BeginLoadData();
                        DataRow row = this.dataTable.Rows.Add(values);
                        row.AcceptChanges();
                        row.Delete();
                        try
                        {
                            this.a(row);
                        }
                        finally
                        {
                            if (row.RowState != DataRowState.Detached)
                            {
                                row.AcceptChanges();
                            }
                        }
                    }
                    finally
                    {
                        this.DependentColumnsInternal = dependentColumnsInternal;
                    }
                }
                finally
                {
                    this.dataTable.EndLoadData();
                    this.disableEvents--;
                    this.disableUpdateEvents--;
                }
            }
            this.storeEvents++;
            try
            {
                A_0.Delete();
            }
            finally
            {
                this.storeEvents--;
            }
            if (!this.r)
            {
                A_0.AcceptChanges();
            }
            if (!this.FetchComplete)
            {
                this.l--;
                this.k--;
            }
        }

        private void c(object A_0, EventArgs A_1)
        {
            EventHandler handler = (EventHandler) this.Events[al];
            if (handler != null)
            {
                handler(A_0, null);
            }
        }

        protected virtual void CacheGetSchemaTable()
        {
            bool flag = this.e();
            try
            {
                this.GetSchemaTable();
            }
            catch
            {
                if (flag)
                {
                    this.d();
                }
                throw;
            }
        }

        public void CancelFetch()
        {
            this.SuspendFill(false);
            if (!this.FetchComplete)
            {
                this.k = this.l + 1;
                try
                {
                    this.CloseReader();
                }
                catch
                {
                }
                finally
                {
                    try
                    {
                        this.a(new ListChangedEventArgs(ListChangedType.Reset, base.DefaultView.Count - 1));
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected virtual void CheckColumnsCreated(bool throwOnEmptySchemaTable)
        {
            this.disableEvents++;
            try
            {
                this.x = ((this.dataTable.Columns.Count != 0) || (this.dataTable.Constraints.Count > 0)) || (this.dataTable.PrimaryKey.Length != 0);
                if (!this.UserDefinedColumns)
                {
                    this.CreateColumnsInternal(throwOnEmptySchemaTable);
                }
                this.CheckReaderMappings();
            }
            finally
            {
                this.disableEvents--;
            }
        }

        protected void CheckDataAdapterCreated()
        {
            if (this.dataAdapter == null)
            {
                this.CreateDataAdapter();
            }
        }

        protected virtual void CheckReaderMappings()
        {
            if (this.readerMappings == null)
            {
                if (this.FetchComplete)
                {
                    this.readerMappings = null;
                }
                else
                {
                    int fieldCount = this.reader.FieldCount;
                    this.readerMappings = new Hashtable();
                    ArrayList list = new ArrayList();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        string name = this.reader.GetName(i);
                        string sourceName = name;
                        int num3 = 0;
                        if (Utils.IsEmpty(name))
                        {
                            name = "Column";
                            sourceName = "Column1";
                            num3 = 1;
                        }
                        if (list.IndexOf(sourceName.ToUpper()) >= 0)
                        {
                            bool flag;
                            do
                            {
                                num3++;
                                sourceName = name + num3.ToString();
                                flag = list.IndexOf(sourceName.ToUpper()) >= 0;
                                if (!flag)
                                {
                                    int num5 = this.reader.FieldCount;
                                    for (int j = 0; j < num5; j++)
                                    {
                                        if (this.reader.GetName(j) == sourceName)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            while (flag);
                        }
                        list.Add(sourceName.ToUpper());
                        string columnNameFromMapping = this.GetColumnNameFromMapping(sourceName);
                        int index = this.Columns.IndexOf(columnNameFromMapping);
                        if (index >= 0)
                        {
                            this.readerMappings[i] = index;
                        }
                    }
                }
            }
        }

        protected void CheckSelectCommand()
        {
            if (this.c == null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("SelectCommandNotInit"));
            }
            DbConnection objB = this.GetConnection();
            if ((objB != null) && ((this.c.Connection == null) || !ReferenceEquals(this.c.Connection, objB)))
            {
                this.c.Connection = objB;
            }
            if (this.c.Connection == null)
            {
                throw new InvalidOperationException(Devart.Common.g.a("ConnectionNotInit"));
            }
            this.detailSelectCommand = this.a(this.c.Connection);
            if (this.detailSelectCommand != null)
            {
                this.currenSelectCommand = this.detailSelectCommand;
            }
            else
            {
                this.currenSelectCommand = this.c;
            }
        }

        public void Clear()
        {
            this.a(false);
        }

        protected virtual void ClearSchemaTableCache()
        {
            this.schemaTable = null;
        }

        public override DataTable Clone()
        {
            DataTable table2;
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < this.Columns.Count; i++)
            {
                DataColumn column = this.Columns[i];
                if ((column.Expression != null) && (column.Expression != ""))
                {
                    hashtable[column] = column.Expression;
                    column.Expression = "";
                }
            }
            try
            {
                DbDataTable table = (DbDataTable) base.Clone();
                this.a(table);
                foreach (DictionaryEntry entry in hashtable)
                {
                    try
                    {
                        table.Columns[((DataColumn) entry.Key).ColumnName].Expression = (string) entry.Value;
                    }
                    catch
                    {
                    }
                }
                table2 = table;
            }
            finally
            {
                foreach (DictionaryEntry entry2 in hashtable)
                {
                    ((DataColumn) entry2.Key).Expression = (string) entry2.Value;
                }
            }
            return table2;
        }

        protected virtual DbCommand CloneCommand(DbCommand command) => 
            (command is ICloneable) ? ((DbCommand) ((ICloneable) command).Clone()) : null;

        protected virtual object CloneValue(object oldValue, DataColumn column)
        {
            ICloneable cloneable1 = oldValue as ICloneable;
            if (cloneable1 == null)
            {
                throw new InvalidOperationException();
            }
            return cloneable1.Clone();
        }

        public void Close()
        {
            if (this.i)
            {
                this.a(true);
            }
        }

        protected virtual void CloseReader()
        {
            this.ResetFetchPosition();
            this.readerMappings = null;
            if (this.reader != null)
            {
                try
                {
                    this.i();
                }
                finally
                {
                    this.reader = null;
                    this.g = false;
                    this.h = true;
                }
            }
        }

        protected virtual void ColumnAdded(DataColumn column, DataRow schemaRow, int index)
        {
        }

        protected virtual void CreateColumns()
        {
            bool flag;
            if ((this.reader != null) && !this.reader.IsClosed)
            {
                flag = false;
            }
            else
            {
                flag = true;
                this.y = true;
                this.CheckSelectCommand();
                this.ExecuteCommand();
            }
            this.disableEvents++;
            try
            {
                this.CreateColumnsInternal(true);
            }
            finally
            {
                this.disableEvents--;
                if (flag)
                {
                    this.CloseReader();
                }
            }
        }

        protected virtual void CreateColumnsInternal(bool throwOnEmptySchemaTable)
        {
            DataTable schemaTable = this.GetSchemaTable();
            if (schemaTable == null)
            {
                if (throwOnEmptySchemaTable)
                {
                    throw new ArgumentNullException("Unable to create column set because retrieving of schemaTable for the SELECT command failed.");
                }
            }
            else
            {
                string[] indexedFieldNames;
                bool flag;
                int num2;
                this.indexOfColumnOnlyOriginalValue = -1;
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                string str = null;
                DataColumn column = null;
                if (schemaTable.Columns.Contains(SchemaTableOptionalColumn.IsHidden))
                {
                    column = schemaTable.Columns[SchemaTableOptionalColumn.IsHidden];
                }
                string[] fieldNames = new string[schemaTable.Rows.Count];
                int index = 0;
                while (true)
                {
                    if (index >= schemaTable.Rows.Count)
                    {
                        indexedFieldNames = GetIndexedFieldNames(fieldNames);
                        flag = false;
                        num2 = 0;
                        break;
                    }
                    fieldNames[index] = schemaTable.Rows[index][SchemaTableColumn.ColumnName].ToString();
                    index++;
                }
                while (true)
                {
                    while (true)
                    {
                        if (num2 >= schemaTable.Rows.Count)
                        {
                            if ((list2 != null) && (list2.Count > 0))
                            {
                                if (!flag)
                                {
                                    base.PrimaryKey = (DataColumn[]) list2.ToArray(typeof(DataColumn));
                                    return;
                                }
                                UniqueConstraint constraint = new UniqueConstraint("", (DataColumn[]) list2.ToArray(typeof(DataColumn)));
                                ConstraintCollection constraints = this.Constraints;
                                int count = constraints.Count;
                                int num4 = 0;
                                while (true)
                                {
                                    if (num4 < constraints.Count)
                                    {
                                        if (!constraint.Equals(constraints[num4]))
                                        {
                                            num4++;
                                            continue;
                                        }
                                        constraint = null;
                                    }
                                    if (constraint != null)
                                    {
                                        constraints.Add(constraint);
                                    }
                                    break;
                                }
                            }
                            return;
                        }
                        DataRow schemaRow = schemaTable.Rows[num2];
                        if (column != null)
                        {
                            object obj4 = schemaRow[column];
                            if ((obj4 as bool) && ((bool) obj4))
                            {
                                break;
                            }
                        }
                        string st = indexedFieldNames[num2];
                        int num3 = 0;
                        string sourceName = st;
                        if (Utils.IsEmpty(st))
                        {
                            st = "Column";
                            sourceName = "Column1";
                            num3 = 1;
                        }
                        if (list.IndexOf(sourceName.ToUpper()) >= 0)
                        {
                            bool flag2;
                            do
                            {
                                num3++;
                                sourceName = st + num3.ToString();
                                flag2 = list.IndexOf(sourceName.ToUpper()) >= 0;
                                if (!flag2)
                                {
                                    using (IEnumerator enumerator = schemaTable.Rows.GetEnumerator())
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            if (((DataRow) enumerator.Current)[SchemaTableColumn.ColumnName].ToString() == sourceName)
                                            {
                                                flag2 = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            while (flag2);
                        }
                        list.Add(sourceName.ToUpper());
                        System.Type type = !this.returnProviderSpecificTypesInternal ? ((System.Type) schemaRow[SchemaTableColumn.DataType]) : ((System.Type) schemaRow[SchemaTableOptionalColumn.ProviderSpecificDataType]);
                        DataColumn column2 = this.dataTable.Columns.Add(this.GetColumnNameFromMapping(sourceName), type);
                        string str4 = schemaRow[SchemaTableColumn.BaseTableName] as string;
                        if (str != null)
                        {
                            if ((str4 != null) && (str != str4))
                            {
                                str = "";
                                list2 = null;
                            }
                        }
                        else if (str4 != null)
                        {
                            str = str4;
                        }
                        else
                        {
                            str = "";
                            list2 = null;
                        }
                        object obj2 = schemaRow[SchemaTableColumn.IsKey];
                        if ((list2 != null) && (((this.MissingSchemaAction & System.Data.MissingSchemaAction.AddWithKey) == System.Data.MissingSchemaAction.AddWithKey) && ((obj2 as bool) && ((bool) obj2))))
                        {
                            list2.Add(column2);
                            if (!flag)
                            {
                                object obj5 = schemaRow[SchemaTableColumn.AllowDBNull];
                                if ((obj5 as bool) && ((bool) obj5))
                                {
                                    flag = true;
                                }
                            }
                        }
                        object obj3 = schemaRow[SchemaTableColumn.IsUnique];
                        if (((this.MissingSchemaAction & System.Data.MissingSchemaAction.AddWithKey) == System.Data.MissingSchemaAction.AddWithKey) && ((obj3 as bool) && (((bool) obj3) && (!(obj2 as bool) || !((bool) obj2)))))
                        {
                            column2.Unique = true;
                        }
                        column2.ReadOnly = (bool) schemaRow[SchemaTableOptionalColumn.IsReadOnly];
                        column2.AllowDBNull = (bool) schemaRow[SchemaTableColumn.AllowDBNull];
                        if (schemaRow.Table.Columns.Contains(SchemaTableOptionalColumn.IsAutoIncrement) && (Convert.ToBoolean(schemaRow[SchemaTableOptionalColumn.IsAutoIncrement]) && column2.ReadOnly))
                        {
                            column2.AllowDBNull = true;
                        }
                        this.ColumnAdded(column2, schemaRow, num2);
                        break;
                    }
                    num2++;
                }
            }
        }

        protected virtual void CreateDataAdapter()
        {
            this.dataAdapter.SelectCommand = this.currenSelectCommand;
            this.dataAdapter.InsertCommand = this.d;
            this.dataAdapter.UpdateCommand = this.e;
            this.dataAdapter.DeleteCommand = this.f;
            this.dataAdapter.MissingSchemaAction = this.ap;
            this.dataAdapter.FillError += new FillErrorEventHandler(this.a);
            this.dataAdapter.ReturnProviderSpecificTypes = this.returnProviderSpecificTypesInternal;
            if (this.q != null)
            {
                string sourceTable = this.q.SourceTable;
                this.dataAdapter.TableMappings.Add(this.q);
                this.q.SourceTable = sourceTable;
            }
            this.commandBuilder.ConflictOption = System.Data.ConflictOption.CompareAllSearchableValues;
            this.commandBuilder.DataAdapter = this.dataAdapter;
            this.commandBuilder.RefreshMode = this.RefreshMode;
        }

        private void d()
        {
            this.currenSelectCommand.Connection.Close();
        }

        internal void d(bool A_0)
        {
            DataRelationCollection parentRelations = base.ParentRelations;
            for (int i = 0; i < parentRelations.Count; i++)
            {
                DbDataTable parentTable = parentRelations[i].ParentTable as DbDataTable;
                if (parentTable != null)
                {
                    parentTable.b(A_0);
                }
            }
        }

        internal void d(DataRow A_0)
        {
            this.InitNewRow(A_0);
        }

        internal void d(object A_0, EventArgs A_1)
        {
            this.m();
        }

        protected virtual void DecrementAutoIncrementCurrent(DataRow row)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (this.n != null)
            {
                this.n.Dispose();
                this.n = null;
            }
            if (this.reader != null)
            {
                this.reader.Dispose();
                this.reader = null;
            }
            if (this.schemaTable != null)
            {
                this.schemaTable.Dispose();
                this.schemaTable = null;
            }
            if (this.commandBuilder != null)
            {
                this.commandBuilder.Dispose();
                this.commandBuilder = null;
            }
            if (this.dataAdapter != null)
            {
                this.dataAdapter.Dispose();
                this.dataAdapter = null;
            }
            if (this.z != null)
            {
                ((IDisposable) this.z).Dispose();
                this.z = null;
            }
            this.aa = null;
            this.propertyDescriptorsCache = null;
            this.dataTable = null;
            this.readerMappings = null;
            this.q = null;
            if ((this.Owner != null) && !this.DesignMode)
            {
                GlobalComponentsCache.RemoveFromGlobalList(this);
            }
        }

        private bool e()
        {
            if (this.currenSelectCommand.Connection.State != ConnectionState.Closed)
            {
                return false;
            }
            this.currenSelectCommand.Connection.Open();
            return true;
        }

        internal void e(DataRow A_0)
        {
            this.DecrementAutoIncrementCurrent(A_0);
        }

        public void EndFill(IAsyncResult result)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }
            try
            {
                if (this.p != null)
                {
                    this.p.EndInvoke(result);
                }
            }
            finally
            {
                this.p = null;
            }
        }

        public override void EndInit()
        {
            if (base.fInitInProgress)
            {
                base.EndInit();
                if ((this.ParentRelation.ParentTable == null) || ((ISupportInitializeNotification) this.ParentRelation.ParentTable).IsInitialized)
                {
                    this.m();
                }
                this.k();
            }
        }

        protected virtual void ExecuteCommand()
        {
            this.ag = true;
            bool flag = this.currenSelectCommand.Connection.State == ConnectionState.Closed;
            try
            {
                this.f();
                this.reader = this.currenSelectCommand.ExecuteReader(this.ExecuteCommBehavior);
                this.h = false;
                this.g = false;
            }
            catch
            {
                if (flag)
                {
                    this.currenSelectCommand.Connection.Close();
                }
                throw;
            }
        }

        private void f()
        {
            this.e();
            this.g();
            if (this.s)
            {
                this.k = this.GetRecordCount();
            }
        }

        protected virtual void FetchCompleted(DataTable schemaTable)
        {
        }

        protected void FetchToPosition(int index)
        {
            this.a(index, true, false);
        }

        public int Fill()
        {
            if (!this.i || (!this.g && this.FetchComplete))
            {
                this.CloseReader();
                this.ClearSchemaTableCache();
                this.y = true;
                this.CheckSelectCommand();
                this.ExecuteCommand();
                this.CheckColumnsCreated(false);
                if (this.n != null)
                {
                    this.n.j();
                }
                this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            }
            int count = base.Rows.Count;
            this.j();
            return (base.Rows.Count - count);
        }

        public int Fill(object[] parameterValues)
        {
            this.CheckSelectCommand();
            this.a(parameterValues);
            return this.Fill();
        }

        public int FillPage(int startRecord, int maxRecords) => 
            this.FillPage(startRecord, maxRecords, null);

        protected virtual int FillPage(DbCommand command, int startRecord, int maxRecords)
        {
            throw new Exception();
        }

        protected int FillPage(IDataReader reader, int startRecord, int maxRecords)
        {
            int num3;
            if ((base.DataSet != null) && base.DataSet.EnforceConstraints)
            {
                base.DataSet.EnforceConstraints = false;
            }
            try
            {
                this.reader = reader;
                this.h = false;
                int num = 0;
                try
                {
                    this.CheckColumnsCreated(true);
                    int l = this.l;
                    this.a(0x7fffffff, true, true);
                    num = this.l - l;
                }
                finally
                {
                    this.CloseReader();
                }
                num3 = num;
            }
            finally
            {
                bool flag;
                if (flag)
                {
                    base.DataSet.EnforceConstraints = true;
                }
            }
            return num3;
        }

        public int FillPage(int startRecord, int maxRecords, object[] parameterValues)
        {
            int num;
            this.CloseReader();
            this.ClearSchemaTableCache();
            this.CheckSelectCommand();
            this.a(parameterValues);
            if (this.n != null)
            {
                this.n.j();
            }
            this.disableEvents++;
            ConnectionState open = ConnectionState.Open;
            DbCommand selectCommand = this.dataAdapter.SelectCommand;
            try
            {
                if ((selectCommand.Connection != null) && (selectCommand.Connection.State == ConnectionState.Closed))
                {
                    selectCommand.Connection.Open();
                }
                this.aq++;
                try
                {
                    num = this.FillPage(selectCommand, startRecord, maxRecords);
                }
                finally
                {
                    this.l();
                }
                this.k = base.Rows.Count;
            }
            finally
            {
                this.disableEvents--;
                if (open == ConnectionState.Closed)
                {
                    selectCommand.Connection.Close();
                }
            }
            this.a(new ListChangedEventArgs(ListChangedType.Reset, null));
            this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
            if (this.commandBuilder != null)
            {
                this.commandBuilder.RefreshSchema();
            }
            return num;
        }

        public void FillSchema()
        {
            this.CheckSelectCommand();
            this.DataAdapterInternal.FillSchema(this, SchemaType.Source);
        }

        private void g()
        {
            if (ParentDataRelation.a(this.aa))
            {
                this.aa.a();
                if (this.currenSelectCommand.CommandType == CommandType.TableDirect)
                {
                    throw new Exception("Cannot establish master/detail relation");
                }
                DbDataRowView component = null;
                PropertyDescriptorCollection itemProperties = ((ITypedList) ((IListSource) this.aa.ParentTable).GetList()).GetItemProperties(null);
                if (this.z.j() >= 0)
                {
                    component = (DbDataRowView) ((IListSource) this.aa.ParentTable).GetList()[this.z.j()];
                }
                string[] parentColumnNames = this.aa.ParentColumnNames;
                IDataParameterCollection parameters = this.currenSelectCommand.Parameters;
                if ((this.c.Parameters.Count > 0) && !ReferenceEquals(this.currenSelectCommand, this.c))
                {
                    foreach (DbParameter parameter in this.c.Parameters)
                    {
                        int index = parameters.IndexOf(parameter.ParameterName);
                        if (index >= 0)
                        {
                            DbParameter parameter2 = (DbParameter) parameters[index];
                            try
                            {
                                parameter2.Value = parameter.Value;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                for (int i = 0; i < parentColumnNames.Length; i++)
                {
                    string parameterName = this.GetParameterName(parentColumnNames[i]);
                    int index = parameters.IndexOf(parameterName);
                    if (index < 0)
                    {
                        throw new Exception("Cannot establish master/detail relation");
                    }
                    DbParameter parameter3 = (DbParameter) parameters[index];
                    if (itemProperties == null)
                    {
                        parameter3.Value = null;
                    }
                    else
                    {
                        PropertyDescriptor descriptor = a(itemProperties, parentColumnNames[i]);
                        if (descriptor == null)
                        {
                            throw new Exception("Cannot establish master/detail relation");
                        }
                        parameter3.Value = descriptor.GetValue(component);
                    }
                }
            }
        }

        protected string GetColumnNameFromMapping(string sourceName)
        {
            if (this.q != null)
            {
                int index = this.q.ColumnMappings.IndexOf(sourceName);
                if (index >= 0)
                {
                    sourceName = this.q.ColumnMappings[index].DataSetColumn;
                }
            }
            return sourceName;
        }

        protected DbConnection GetConnection()
        {
            DbConnection connection = this.connection;
            if (connection == null)
            {
                if ((this.c != null) && (this.c.Connection != null))
                {
                    connection = this.c.Connection;
                }
                if (connection != null)
                {
                    return connection;
                }
                DbDataSet dataSet = base.DataSet as DbDataSet;
                if (dataSet != null)
                {
                    connection = dataSet.Connection;
                }
            }
            return connection;
        }

        protected virtual void GetDataRow(DataRow row)
        {
            DbDataReaderBase reader = (DbDataReaderBase) this.reader;
            int visibleFieldCount = reader.VisibleFieldCount;
            if (this.UserDefinedColumns || (this.readerMappings.Count != visibleFieldCount))
            {
                int num2 = visibleFieldCount;
                for (int i = 0; i < num2; i++)
                {
                    object obj2 = this.readerMappings[i];
                    if (obj2 != null)
                    {
                        this.GetField(row, (int) obj2, this.reader, i);
                    }
                }
            }
            else
            {
                object[] values = new object[this.reader.FieldCount];
                bool flag = visibleFieldCount < this.reader.FieldCount;
                if (!this.returnProviderSpecificTypesInternal)
                {
                    this.reader.GetValues(values);
                    if (flag)
                    {
                        object[] destinationArray = new object[visibleFieldCount];
                        Array.Copy(values, 0, destinationArray, 0, visibleFieldCount);
                        values = destinationArray;
                    }
                }
                else
                {
                    reader.GetProviderSpecificValues(values);
                    if (this.indexOfColumnOnlyOriginalValue > -1)
                    {
                        values[this.indexOfColumnOnlyOriginalValue] = this.reader.GetValue(this.indexOfColumnOnlyOriginalValue);
                    }
                    if (flag)
                    {
                        object[] destinationArray = new object[visibleFieldCount];
                        Array.Copy(values, 0, destinationArray, 0, visibleFieldCount);
                        values = destinationArray;
                    }
                }
                row.ItemArray = values;
            }
        }

        protected virtual void GetField(DataRow row, int rowInd, IDataReader reader, int readerInd)
        {
            if (this.returnProviderSpecificTypesInternal)
            {
                row[rowInd] = ((DbDataReaderBase) reader).GetProviderSpecificValue(readerInd);
            }
            else
            {
                row[rowInd] = reader.GetValue(readerInd);
            }
        }

        protected static string[] GetIndexedFieldNames(ICollection<string> fieldNames)
        {
            int count = fieldNames.Count;
            List<string> list = new List<string>(fieldNames);
            string[] strArray = new string[count];
            List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>(count / 2);
            for (int i = 0; i < count; i++)
            {
                string st = list[i];
                strArray[i] = st;
                if (Utils.IsEmpty(st))
                {
                    list2.Add(new KeyValuePair<string, int>(st, i));
                }
                else
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        if (string.Compare(st, list[j], StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            list2.Add(new KeyValuePair<string, int>(st, j));
                            break;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, int> pair in list2)
            {
                string key = pair.Key;
                string str3 = key;
                int num4 = 1;
                if (Utils.IsEmpty(key))
                {
                    key = "Column";
                    str3 = "Column1";
                    num4 = 2;
                }
                int index = pair.Value;
                while (true)
                {
                    if (!a((ICollection<string>) list, str3))
                    {
                        strArray[index] = str3;
                        list.Add(str3);
                        break;
                    }
                    str3 = key + num4;
                    num4++;
                }
            }
            return strArray;
        }

        protected virtual string GetParameterName(string parameterName) => 
            parameterName;

        protected virtual char GetParameterPlaceholder() => 
            ':';

        protected virtual PropertyDescriptorCollection GetProperties(PropertyDescriptor[] listAccessors)
        {
            if ((listAccessors == null) || (listAccessors.Length == 0))
            {
                if ((this.propertyDescriptorsCache == null) || this.y)
                {
                    this.a(listAccessors);
                }
                return this.propertyDescriptorsCache;
            }
            int count = base.ChildRelations.Count;
            if (count != 0)
            {
                PropertyDescriptor descriptor = listAccessors[0];
                for (int i = 0; i < count; i++)
                {
                    DataRelation relation = base.ChildRelations[i];
                    if ((relation.RelationName == descriptor.Name) && (relation.ChildTable is DbDataTable))
                    {
                        PropertyDescriptor[] descriptorArray = null;
                        if (listAccessors.Length > 1)
                        {
                            descriptorArray = new PropertyDescriptor[listAccessors.Length - 1];
                            for (int j = 1; j < listAccessors.Length; j++)
                            {
                                descriptorArray[j - 1] = listAccessors[j];
                            }
                        }
                        return ((DbDataTable) relation.ChildTable).c(descriptorArray);
                    }
                }
            }
            return new PropertyDescriptorCollection(null);
        }

        protected virtual System.Type GetPropertyType(System.Type objType) => 
            objType;

        protected virtual object GetPropertyValue(object obj, System.Type objType, IEditableObject objectItemView, PropertyDescriptor propertyDescriptor) => 
            obj;

        protected int GetRecordCount() => 
            ((DbCommandBase) this.currenSelectCommand).GetRecordCount();

        protected virtual DataTable GetSchemaTable()
        {
            if (this.schemaTable == null)
            {
                if ((this.reader == null) || this.reader.IsClosed)
                {
                    if ((this.DataAdapterInternal.SelectCommand == null) || (this.DataAdapterInternal.SelectCommand.Connection == null))
                    {
                        return null;
                    }
                    using (IDataReader reader = this.DataAdapterInternal.SelectCommand.ExecuteReader((this.ExecuteCommBehavior | CommandBehavior.SingleResult) | CommandBehavior.SchemaOnly))
                    {
                        this.schemaTable = reader.GetSchemaTable();
                    }
                    return this.schemaTable;
                }
                this.schemaTable = this.reader.GetSchemaTable();
            }
            return this.schemaTable;
        }

        protected virtual object GetViewValue(object obj, System.Type objType, IEditableObject objectItemView, PropertyDescriptor propertyDescriptor) => 
            obj;

        private void i()
        {
            if (!this.h)
            {
                if (!this.g)
                {
                    this.reader.Close();
                }
                else if (!this.reader.IsClosed && !this.reader.NextResult())
                {
                    this.reader.Close();
                }
            }
            this.h = true;
        }

        protected virtual void InitNewRow(DataRow newRow)
        {
            if (ParentDataRelation.a(this.aa))
            {
                this.aa.a();
                string[] parentColumnNames = this.aa.ParentColumnNames;
                string[] childColumnNames = this.aa.ChildColumnNames;
                DbDataRowView component = null;
                PropertyDescriptorCollection itemProperties = ((ITypedList) ((IListSource) this.aa.ParentTable).GetList()).GetItemProperties(null);
                if (this.z.j() >= 0)
                {
                    component = (DbDataRowView) ((IListSource) this.aa.ParentTable).GetList()[this.z.j()];
                }
                for (int i = 0; i < parentColumnNames.Length; i++)
                {
                    int index = this.Columns.IndexOf(childColumnNames[i]);
                    if (index >= 0)
                    {
                        DataColumn column = this.Columns[index];
                        if (itemProperties == null)
                        {
                            newRow[column] = DBNull.Value;
                        }
                        else
                        {
                            PropertyDescriptor descriptor1 = a(itemProperties, parentColumnNames[i]);
                            if (descriptor1 == null)
                            {
                                throw new Exception("Cannot establish master/detail relation");
                            }
                            object obj2 = descriptor1.GetValue(component);
                            if (obj2 == null)
                            {
                                obj2 = DBNull.Value;
                            }
                            newRow[column] = obj2;
                        }
                    }
                }
            }
        }

        protected static bool IsCatchableExceptionType(Exception e)
        {
            System.Type objA = e.GetType();
            return (!ReferenceEquals(objA, typeof(StackOverflowException)) && (!ReferenceEquals(objA, typeof(OutOfMemoryException)) && (!ReferenceEquals(objA, typeof(ThreadAbortException)) && (!ReferenceEquals(objA, typeof(NullReferenceException)) && (!ReferenceEquals(objA, typeof(AccessViolationException)) && !typeof(SecurityException).IsAssignableFrom(objA))))));
        }

        private void j()
        {
            this.b(true);
        }

        private void k()
        {
            EventHandler handler = (EventHandler) this.Events[am];
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void l()
        {
            this.aq--;
            if (this.aq == 0)
            {
                if (this.n != null)
                {
                    this.n.a(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
                }
                if (this.FetchComplete)
                {
                    this.c(this, null);
                }
            }
        }

        private void m()
        {
            try
            {
                this.Active = this.b;
            }
            catch
            {
                this.i = this.b;
                throw;
            }
        }

        protected object MakeModifiedObjectTree(DbDataRowView dataRowView, object oldValue, PropertyDescriptor propertyDescriptor)
        {
            dataRowView.InEditMode = true;
            if (dataRowView.Row != null)
            {
                dataRowView.Row.BeginEdit();
                string name = propertyDescriptor.Name;
                int index = name.IndexOf('.');
                if (index >= 0)
                {
                    name = name.Substring(0, index);
                }
                DataColumn column = dataRowView.Row.Table.Columns[name];
                if (column != null)
                {
                    if ((!dataRowView.Row.HasVersion(DataRowVersion.Original) || (dataRowView.Row[column] != dataRowView.Row[column, DataRowVersion.Original])) && !this.NeedCloneColumnValue(oldValue))
                    {
                        return dataRowView.Row[column];
                    }
                    object obj2 = this.CloneValue(oldValue, column);
                    dataRowView.Row[column] = obj2;
                    return obj2;
                }
            }
            return oldValue;
        }

        internal void n()
        {
            if (this.a2 > 0)
            {
                this.a2--;
            }
        }

        protected virtual bool NeedCloneColumnValue(object oldValue) => 
            false;

        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            if (this.disableEvents <= 0)
            {
                base.OnRowChanged(e);
            }
        }

        protected override void OnRowChanging(DataRowChangeEventArgs e)
        {
            if (this.disableEvents <= 0)
            {
                base.OnRowChanging(e);
            }
        }

        protected override void OnRowDeleted(DataRowChangeEventArgs e)
        {
            if (this.disableUpdateEvents <= 0)
            {
                base.OnRowDeleted(e);
            }
        }

        protected override void OnRowDeleting(DataRowChangeEventArgs e)
        {
            if (this.disableUpdateEvents <= 0)
            {
                if (this.disableEvents <= 0)
                {
                    this.a(this, (EventArgs) null);
                }
                base.OnRowDeleting(e);
            }
        }

        protected override void OnTableClearing(DataTableClearEventArgs e)
        {
            this.a(this, (EventArgs) null);
            if (this.disableEvents <= 0)
            {
                base.OnTableClearing(e);
            }
        }

        protected override void OnTableNewRow(DataTableNewRowEventArgs e)
        {
            this.a(this, (EventArgs) null);
            if (this.disableUpdateEvents <= 0)
            {
                base.OnTableNewRow(e);
            }
        }

        public void Open()
        {
            if (!this.i)
            {
                this.i = true;
                this.j = true;
                try
                {
                    this.y = true;
                    if (this.w != 0)
                    {
                        this.a(this.v, this.w);
                    }
                    else
                    {
                        this.CheckSelectCommand();
                        this.CacheGetSchemaTable();
                        this.ExecuteCommand();
                        this.CheckColumnsCreated(true);
                        if (Utils.IsEmpty(base.TableName))
                        {
                            DataTable schemaTable = this.GetSchemaTable();
                            if (schemaTable.Rows.Count <= 0)
                            {
                                base.TableName = "Table";
                            }
                            else
                            {
                                string str = schemaTable.Rows[0][SchemaTableColumn.BaseTableName].ToString();
                                foreach (DataRow row in schemaTable.Rows)
                                {
                                    if (str != row[SchemaTableColumn.BaseTableName].ToString())
                                    {
                                        base.TableName = "Table";
                                        break;
                                    }
                                }
                                if (Utils.IsEmpty(base.TableName))
                                {
                                    base.TableName = str;
                                }
                            }
                        }
                        if (this.n != null)
                        {
                            this.n.j();
                        }
                        this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
                        this.c();
                    }
                }
                catch
                {
                    this.i = false;
                    if (this.reader != null)
                    {
                        this.reader.Dispose();
                    }
                    this.reader = null;
                    this.ResetFetchPosition();
                    throw;
                }
                finally
                {
                    this.j = false;
                }
            }
        }

        protected virtual void Open(IDataReader reader)
        {
            this.OpenInternal(reader);
        }

        protected virtual void OpenInternal(IDataReader reader)
        {
            if (!this.i)
            {
                this.j = true;
                this.i = true;
                try
                {
                    this.y = true;
                    this.g = true;
                    this.h = false;
                    this.reader = reader;
                    if (this.w != 0)
                    {
                        throw new ArgumentException(Devart.Common.g.a("MaxRecordsMustBeZero"));
                    }
                    if (!this.ag)
                    {
                        this.Prepare();
                    }
                    this.ag = false;
                    this.CheckColumnsCreated(true);
                    if (this.n != null)
                    {
                        this.n.j();
                    }
                    this.a(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null));
                    this.c();
                }
                catch
                {
                    this.i = false;
                    this.reader = null;
                    this.ResetFetchPosition();
                    throw;
                }
                finally
                {
                    this.j = false;
                }
            }
        }

        internal void p()
        {
            if ((base.DataSet != null) && base.DataSet.EnforceConstraints)
            {
                DataRelationCollection parentRelations = base.ParentRelations;
                for (int i = 0; i < parentRelations.Count; i++)
                {
                    DbDataTable parentTable = parentRelations[i].ParentTable as DbDataTable;
                    if (parentTable != null)
                    {
                        if (!parentTable.Active && !parentTable.ao)
                        {
                            throw new InvalidOperationException($"The parent table '{parentTable.Name}' used in a relation should be opened before '{this.Name}'");
                        }
                        if (!parentTable.ao)
                        {
                            parentTable.EndInit();
                        }
                    }
                }
            }
        }

        protected void Prepare()
        {
            if (this.s)
            {
                if (this.c == null)
                {
                    throw new QueryRecordCountException(Devart.Common.g.a("SelectCommandNotDefined"));
                }
                this.CheckSelectCommand();
                this.f();
            }
            this.ag = true;
        }

        internal void r()
        {
            this.a2++;
        }

        protected bool RaiseFillError(ref Exception e, object[] dataValues)
        {
            FillErrorEventArgs args = new FillErrorEventArgs(this, dataValues) {
                Errors = e
            };
            this.a(this, args);
            e = args.Errors;
            return args.Continue;
        }

        public bool Read()
        {
            int l = this.l;
            this.a(this.l + 1, true, false);
            return (this.l > l);
        }

        public void ReadComplete(DataRow row)
        {
            if (row.RowState != DataRowState.Unchanged)
            {
                throw new InvalidOperationException(Devart.Common.g.a("ErrorReadModifiedOrDetachedRow"));
            }
            IDbCommand command1 = this.CommandBuilderInternal.a(this.TableMapping, row, true);
            if (command1 == null)
            {
                throw new InvalidOperationException("Cannot create command for loading fields data.");
            }
            IDataReader reader = command1.ExecuteReader(this.ExecuteCommBehavior);
            if (!reader.Read())
            {
                throw new InvalidOperationException(Devart.Common.g.a("RowNotExist"));
            }
            this.disableEvents++;
            this.disableUpdateEvents++;
            try
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    if (this.q != null)
                    {
                        int num4 = this.q.ColumnMappings.IndexOf(name);
                        if (num4 >= 0)
                        {
                            name = this.q.ColumnMappings[num4].DataSetColumn;
                        }
                    }
                    int rowInd = this.Columns.IndexOf(name);
                    if (rowInd != -1)
                    {
                        this.GetField(row, rowInd, reader, i);
                    }
                }
            }
            finally
            {
                this.disableEvents--;
                this.disableUpdateEvents--;
            }
            int index = this.dataTable.Rows.IndexOf(row);
            if ((index >= 0) && (this.n != null))
            {
                this.n.a(this, new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
        }

        public void RefreshRow(DataRow row)
        {
            IDataReader reader = this.CommandBuilderInternal.a(null, this.q, row, false, null).ExecuteReader(this.ExecuteCommBehavior);
            try
            {
                if (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        string name = reader.GetName(i);
                        if (this.q != null)
                        {
                            int num4 = this.q.ColumnMappings.IndexOf(name);
                            if (num4 >= 0)
                            {
                                name = this.q.ColumnMappings[num4].DataSetColumn;
                            }
                        }
                        int index = this.Columns.IndexOf(name);
                        if (index != -1)
                        {
                            this.GetField(row, index, reader, i);
                        }
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        private void ResetColumns()
        {
            if (this.Site != null)
            {
                if (this.Active)
                {
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        DataColumn column = this.Columns[i];
                        if (column != null)
                        {
                            column.Site = null;
                        }
                    }
                }
                else
                {
                    this.Columns.Clear();
                }
            }
        }

        private void ResetFetchPosition()
        {
            this.l = -1;
            this.m = true;
            this.k = 0;
        }

        internal void ResetParentRelation()
        {
            this.ParentRelation = null;
        }

        public DataRow[] Select()
        {
            if (this.i)
            {
                this.FetchToPosition(0x7fffffff);
            }
            return base.Select();
        }

        public DataRow[] Select(string filterExpression)
        {
            if (this.i)
            {
                this.FetchToPosition(0x7fffffff);
            }
            return base.Select(filterExpression);
        }

        public DataRow[] Select(string filterExpression, string sort)
        {
            if (this.i)
            {
                this.FetchToPosition(0x7fffffff);
            }
            return base.Select(filterExpression, sort);
        }

        public DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
        {
            if (this.i)
            {
                this.FetchToPosition(0x7fffffff);
            }
            return base.Select(filterExpression, sort, recordStates);
        }

        protected virtual void SetOwner(object value)
        {
            throw new Exception();
        }

        protected void SetOwnerReal(object value)
        {
            if ((this.a != null) && !this.DesignMode)
            {
                GlobalComponentsCache.RemoveFromGlobalList(this);
            }
            this.a = value;
            if ((this.a != null) && !this.DesignMode)
            {
                GlobalComponentsCache.AddToGlobalList(this);
            }
            this.z.b(this.a);
            if (this.a != value)
            {
                this.a(this, (EventArgs) null);
            }
        }

        protected void SetPrimaryKey(DataColumn[] value)
        {
            base.PrimaryKey = value;
        }

        private bool ShouldSerializeColumns() => 
            (this.Columns.Count > 0) && this.UserDefinedColumns;

        private bool ShouldSerializeConstraints() => 
            (this.Constraints.Count > 0) && this.UserDefinedColumns;

        private bool ShouldSerializeParentRelation() => 
            (this.aa != null) && ((this.aa.ParentTable != null) || ((this.aa.ParentColumnNames != null) || (this.aa.ChildColumnNames != null)));

        private bool ShouldSerializePrimaryKey() => 
            (this.PrimaryKey.Length != 0) && this.UserDefinedColumns;

        public void SuspendFill()
        {
            this.SuspendFill(false);
        }

        public void SuspendFill(bool wait)
        {
            Interlocked.Exchange(ref this.ae, 1);
            if (wait)
            {
                lock (this.t)
                {
                }
            }
        }

        IList IListSource.GetList()
        {
            this.n ??= new DbDataTableView(this, base.DefaultView);
            return this.n;
        }

        internal void t()
        {
            for (int i = this.az.Count - 1; i >= 0; i--)
            {
                WeakReference weakReference = (WeakReference) this.az[i];
                if (!Utils.GetWeakIsAlive(weakReference))
                {
                    this.az.RemoveAt(i);
                }
                else
                {
                    DbDataTableView weakTarget = (DbDataTableView) Utils.GetWeakTarget(weakReference);
                    weakTarget.l++;
                }
            }
            try
            {
                if (this.az != null)
                {
                    bool flag = false;
                    while (!flag)
                    {
                        flag = true;
                        for (int j = this.az.Count - 1; j >= 0; j--)
                        {
                            WeakReference weakReference = (WeakReference) this.az[j];
                            if (!Utils.GetWeakIsAlive(weakReference))
                            {
                                this.az.RemoveAt(j);
                            }
                            else if (((DbDataTableView) Utils.GetWeakTarget(weakReference)).i())
                            {
                                flag = false;
                            }
                        }
                    }
                }
            }
            finally
            {
                for (int j = this.az.Count - 1; j >= 0; j--)
                {
                    WeakReference weakReference = (WeakReference) this.az[j];
                    if (!Utils.GetWeakIsAlive(weakReference))
                    {
                        this.az.RemoveAt(j);
                    }
                    else
                    {
                        DbDataTableView weakTarget = (DbDataTableView) Utils.GetWeakTarget(weakReference);
                        weakTarget.l--;
                        if (weakTarget.l < 0)
                        {
                            weakTarget.l = 0;
                        }
                    }
                }
            }
        }

        public virtual int Update()
        {
            this.a();
            return this.DataAdapterInternal.Update(this);
        }

        public int UpdateRows(DataRow[] datarows)
        {
            this.a();
            return this.DataAdapterInternal.Update(datarows);
        }

        bool ISupportInitializeNotification.IsInitialized =>
            !base.fInitInProgress;

        [Browsable(false)]
        public object SyncRoot =>
            this.t;

        [DefaultValue(false), y("DbDataTable_QueryRecordCount"), Category("Live Data")]
        public bool QueryRecordCount
        {
            get => 
                this.s;
            set => 
                this.s = value;
        }

        bool IListSource.ContainsListCollection =>
            false;

        protected bool UserDefinedColumns
        {
            get
            {
                if ((this.Site == null) || (this.Site.Container is INestedContainer))
                {
                    return this.x;
                }
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataColumn column = this.Columns[i];
                    if ((column != null) && (column.Site != null))
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                if (this.Site == null)
                {
                    this.x = value;
                }
                else if (this.Site.Container != null)
                {
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        DataColumn component = this.Columns[i];
                        if ((component != null) && (component.Site == null))
                        {
                            this.Site.Container.Add(component, this.Site.Name + "_" + component.ColumnName);
                        }
                    }
                }
            }
        }

        [Browsable(false)]
        public DataTable SchemaTable =>
            this.GetSchemaTable();

        [Browsable(false)]
        public int RecordCount =>
            (this.s || !this.FetchComplete) ? this.k : base.Rows.Count;

        internal bool FetchComplete
        {
            get
            {
                if ((this.reader != null) && (this.reader.IsClosed && !this.h))
                {
                    this.i();
                    this.FetchCompleted(this.GetSchemaTable());
                    this.c(this, null);
                }
                return ((this.reader == null) || (this.reader.IsClosed || this.h));
            }
        }

        private object DependentColumnsInternal
        {
            get
            {
                try
                {
                    FieldInfo field = typeof(DbDataTable).GetField("dependentColumns", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        return field.GetValue(this);
                    }
                }
                catch
                {
                }
                return null;
            }
            set
            {
                try
                {
                    FieldInfo field = typeof(DbDataTable).GetField("dependentColumns", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(this, value);
                    }
                }
                catch
                {
                }
            }
        }

        protected internal Devart.Common.DbCommandBuilder CommandBuilderInternal
        {
            get
            {
                this.CheckDataAdapterCreated();
                return this.commandBuilder;
            }
        }

        protected internal Devart.Common.DbDataAdapter DataAdapterInternal
        {
            get
            {
                this.CheckDataAdapterCreated();
                return this.dataAdapter;
            }
        }

        [Category("Live Data"), DefaultValue(false), y("DbTable_Active")]
        public bool Active
        {
            get => 
                !base.fInitInProgress ? this.i : this.b;
            set
            {
                if (base.fInitInProgress)
                {
                    this.b = value;
                }
                else
                {
                    this.ao = value;
                    if (value != this.i)
                    {
                        if (!value)
                        {
                            this.Close();
                        }
                        else
                        {
                            this.p();
                            this.Open();
                        }
                        this.i = value;
                    }
                }
            }
        }

        [Category("Live Data"), y("DbTable_StartRecord"), DefaultValue(0)]
        public int StartRecord
        {
            get => 
                this.v;
            set
            {
                if (this.v != value)
                {
                    if (!base.fInitInProgress && (this.Active && (this.w > 0)))
                    {
                        this.a(value, this.w);
                    }
                    this.v = value;
                }
            }
        }

        [y("DbTable_MaxRecords"), Category("Live Data"), DefaultValue(0)]
        public int MaxRecords
        {
            get => 
                this.w;
            set
            {
                if (this.w != value)
                {
                    this.w = value;
                    if (!base.fInitInProgress && this.Active)
                    {
                        if (value == 0)
                        {
                            this.Close();
                            this.Open();
                        }
                        else
                        {
                            this.a(this.v, value);
                        }
                    }
                }
            }
        }

        public DataTableMapping TableMapping =>
            this.q;

        [Browsable(false), MergableProperty(false)]
        public DbConnection Connection
        {
            get => 
                this.connection;
            set
            {
                if (!ReferenceEquals(this.connection, value))
                {
                    this.connection = value;
                    if (this.SelectCommand != null)
                    {
                        this.SelectCommand.Connection = value;
                    }
                    if (this.InsertCommand != null)
                    {
                        this.InsertCommand.Connection = value;
                    }
                    if (this.UpdateCommand != null)
                    {
                        this.UpdateCommand.Connection = value;
                    }
                    if (this.DeleteCommand != null)
                    {
                        this.DeleteCommand.Connection = value;
                    }
                }
            }
        }

        [Browsable(false), MergableProperty(false)]
        public DbCommand SelectCommand
        {
            get => 
                this.c;
            set
            {
                if (this.DesignMode && ((value == null) && this.Active))
                {
                    this.Active = false;
                }
                this.c = value;
                this.DataAdapterInternal.SelectCommand = value;
            }
        }

        [MergableProperty(false), Browsable(false)]
        public DbCommand InsertCommand
        {
            get => 
                this.d;
            set
            {
                this.d = value;
                this.DataAdapterInternal.InsertCommand = value;
            }
        }

        [Browsable(false), MergableProperty(false)]
        public DbCommand UpdateCommand
        {
            get => 
                this.e;
            set
            {
                this.e = value;
                this.DataAdapterInternal.UpdateCommand = value;
            }
        }

        [MergableProperty(false), Browsable(false)]
        public DbCommand DeleteCommand
        {
            get => 
                this.f;
            set
            {
                this.f = value;
                this.DataAdapterInternal.DeleteCommand = value;
            }
        }

        [Category("Update"), DefaultValue(true), y("DbTable_CachedUpdates")]
        public bool CachedUpdates
        {
            get => 
                this.r;
            set
            {
                if (this.r && !value)
                {
                    if ((this.n != null) && (this.n.CurrentRowView != null))
                    {
                        this.n.CurrentRowView.EndEdit();
                    }
                    this.Update();
                }
                this.r = value;
            }
        }

        [DefaultValue(false), Category("Live Data"), y("DbTable_FetchAll")]
        public virtual bool FetchAll
        {
            get => 
                this.ac;
            set => 
                this.ac = value;
        }

        [DefaultValue(false), y("DbTable_NonBlocking"), Category("Live Data")]
        public bool NonBlocking
        {
            get => 
                this.ab;
            set => 
                this.ab = value;
        }

        protected EventHandlerList Events =>
            base.Events;

        internal string FullName =>
            (ReferenceEquals(base.GetType().BaseType, typeof(DbDataTable)) || !(base.DataSet is DbDataSet)) ? this.Name : ((base.DataSet as DbDataSet).Name + "." + this.Name);

        [Browsable(false), DefaultValue("")]
        public string Name
        {
            get => 
                ((this.Site == null) || (this.Site.Name == null)) ? ((this.av == null) ? string.Empty : this.av) : this.Site.Name;
            set
            {
                if (this.Site == null)
                {
                    this.av = (value == null) ? string.Empty : value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Owner
        {
            get => 
                this.a;
            set => 
                this.SetOwner(value);
        }

        [MergableProperty(false), Browsable(true), y("DbDataTable_ParentRelation"), Category("Data"), RefreshProperties(RefreshProperties.Repaint)]
        public ParentDataRelation ParentRelation
        {
            get
            {
                if (this.aa == null)
                {
                    this.aa = new ParentDataRelation();
                    this.aa.ChildTableInternal = this;
                }
                return this.aa;
            }
            set
            {
                if ((value == null) && (this.aa != null))
                {
                    this.aa.ChildTableInternal = null;
                }
                this.aa = value;
                if (this.aa != null)
                {
                    this.aa.ChildTableInternal = this;
                }
            }
        }

        [DefaultValue(true), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignOnly(true)]
        public bool DesignTimeVisible
        {
            get => 
                this.ad;
            set
            {
                this.ad = value;
                TypeDescriptor.Refresh(this);
            }
        }

        [MergableProperty(false)]
        public DataColumnCollection Columns =>
            base.Columns;

        [MergableProperty(false)]
        public ConstraintCollection Constraints =>
            base.Constraints;

        [MergableProperty(false)]
        public DataColumn[] PrimaryKey
        {
            get => 
                base.PrimaryKey;
            set
            {
                this.af = true;
                try
                {
                    base.PrimaryKey = value;
                }
                finally
                {
                    this.af = false;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected internal bool Reloading
        {
            get => 
                this.an;
            set => 
                this.an = value;
        }

        [Category("Update"), y("DbDataTable_KeyFields"), DefaultValue("")]
        public string UpdatingKeyFields
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.KeyFields : "";
            set
            {
                if ((value != "") || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.KeyFields = value;
                }
            }
        }

        [MergableProperty(false), Category("Update"), y("DbDataTable_UpdatingFields"), DefaultValue("")]
        public string UpdatingFields
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.UpdatingFields : "";
            set
            {
                if ((value != "") || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.UpdatingFields = value;
                }
            }
        }

        [MergableProperty(false)]
        public string UpdatingTable
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.UpdatingTable : "";
            set
            {
                if ((value != "") || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.UpdatingTable = value;
                }
            }
        }

        [Category("Update"), y("DbDataTable_Quoted"), DefaultValue(false)]
        public bool Quoted
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.Quoted : false;
            set
            {
                if (value || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.Quoted = value;
                }
            }
        }

        [y("DbDataTable_RefreshMode"), DefaultValue(0), Category("Update")]
        public RefreshRowMode RefreshMode
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.RefreshMode : RefreshRowMode.None;
            set
            {
                if ((value != RefreshRowMode.None) || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.RefreshMode = value;
                }
            }
        }

        [DefaultValue(""), MergableProperty(false), y("DbDataTable_RefreshingFields"), Category("Update")]
        public string RefreshingFields
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.RefreshingFields : "";
            set
            {
                if ((value != "") || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.RefreshingFields = value;
                }
            }
        }

        [Category("Update"), y("DbDataTable_ConflictOption"), DefaultValue(1)]
        public virtual System.Data.ConflictOption ConflictOption
        {
            get => 
                (this.commandBuilder != null) ? this.commandBuilder.ConflictOption : System.Data.ConflictOption.CompareAllSearchableValues;
            set
            {
                if ((value != System.Data.ConflictOption.CompareAllSearchableValues) || (this.commandBuilder != null))
                {
                    this.CommandBuilderInternal.ConflictOption = value;
                }
            }
        }

        [y("DbDataTable_UpdateBatchSize"), Category("Update"), DefaultValue(1)]
        public int UpdateBatchSize
        {
            get => 
                (this.dataAdapter != null) ? this.dataAdapter.UpdateBatchSize : 1;
            set
            {
                if ((value != 1) || (this.dataAdapter != null))
                {
                    this.DataAdapterInternal.UpdateBatchSize = value;
                }
            }
        }

        [Category("Fill"), y("DbDataTable_ReturnProviderSpecificTypes"), DefaultValue(false)]
        public bool ReturnProviderSpecificTypes
        {
            get => 
                this.returnProviderSpecificTypesInternal;
            set
            {
                this.returnProviderSpecificTypesInternal = value;
                if (this.dataAdapter != null)
                {
                    this.dataAdapter.ReturnProviderSpecificTypes = value;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(true)]
        public bool RetrieveAutoIncrementSeed
        {
            get => 
                this.ah;
            set => 
                this.ah = value;
        }

        [y("DbDataTable_RemotingFormat"), DefaultValue(0)]
        public SerializationFormat RemotingFormat
        {
            get => 
                base.RemotingFormat;
            set => 
                base.RemotingFormat = value;
        }

        [y("DbDataTable_MissingSchemaAction"), DefaultValue(4)]
        public System.Data.MissingSchemaAction MissingSchemaAction
        {
            get => 
                this.ap;
            set
            {
                if ((value != System.Data.MissingSchemaAction.Add) && (value != System.Data.MissingSchemaAction.AddWithKey))
                {
                    throw new NotSupportedException(Devart.Common.g.a("MissingSchemaActionNotSupported"));
                }
                this.ap = value;
            }
        }

        internal object FetchRowSyncRoot
        {
            get
            {
                DbDataSet dataSet = base.DataSet as DbDataSet;
                if (dataSet != null)
                {
                    return dataSet.FetchRowSyncRoot;
                }
                this.u ??= new object();
                return this.u;
            }
        }

        protected virtual CommandBehavior ExecuteCommBehavior =>
            (this.ap == System.Data.MissingSchemaAction.AddWithKey) ? CommandBehavior.KeyInfo : CommandBehavior.Default;

        internal bool AllowCruidDuringFetch
        {
            get => 
                this.a1;
            set => 
                this.a1 = value;
        }

        public static bool DisableListChangedEvents
        {
            get => 
                ar;
            set => 
                ar = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CancelEditRowIfUpdateFailed
        {
            get => 
                this.@as;
            set => 
                this.@as = value;
        }

        private delegate void a(CultureInfo A_0);
    }
}

