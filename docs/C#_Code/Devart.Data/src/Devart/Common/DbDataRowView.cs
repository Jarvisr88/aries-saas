namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;

    public class DbDataRowView : ICustomTypeDescriptor, IEditableObject, IListSource, IDataErrorInfo
    {
        private DataRowView a;
        private DataRow b;
        internal IList c;
        private int d;
        internal bool e;
        private bool f;
        private PropertyChangedEventHandler g;
        private bool h;
        private Hashtable i;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                this.g += value;
            }
            remove
            {
                this.g -= value;
            }
        }

        internal DbDataRowView(DataRow A_0, IList A_1, DataRowView A_2, int A_3)
        {
            this.b = A_0;
            this.c = A_1;
            this.d = A_3;
            this.a = A_2;
        }

        private object[] a(DataRelation A_0)
        {
            object[] objArray = new object[A_0.ParentColumns.Length];
            if (this.Row != null)
            {
                for (int i = 0; i < A_0.ParentColumns.Length; i++)
                {
                    objArray[i] = this.Row[A_0.ParentColumns[i]];
                }
            }
            return objArray;
        }

        internal object a(DataColumn A_0, Devart.Common.d A_1)
        {
            if (A_1.c() != null)
            {
                return this.CreateChildView(A_1.c());
            }
            if (this.Row == null)
            {
                return null;
            }
            object obj2 = (A_0.Table != null) ? this.Row[A_0] : null;
            obj2 = ((DbDataTable) this.Row.Table).b(obj2, A_0.DataType, this, A_1);
            return ((obj2 == null) ? DBNull.Value : obj2);
        }

        internal static object[] a(DataRow A_0, bool A_1)
        {
            DataColumnCollection columns = A_0.Table.Columns;
            object[] objArray = new object[columns.Count];
            for (int i = 0; i < objArray.Length; i++)
            {
                DataColumn column = columns[i];
                if (A_1 || !column.ReadOnly)
                {
                    objArray[i] = A_0[i];
                }
            }
            return objArray;
        }

        internal static object[] a(DataRow A_0, DataRowVersion A_1)
        {
            DataColumnCollection columns = A_0.Table.Columns;
            object[] objArray = new object[columns.Count];
            for (int i = 0; i < objArray.Length; i++)
            {
                DataColumn column1 = columns[i];
                objArray[i] = A_0[i, A_1];
            }
            return objArray;
        }

        private void a(object A_0, PropertyChangedEventArgs A_1)
        {
            if (this.g != null)
            {
                this.g(A_0, A_1);
            }
            DbDataTableView c = this.c as DbDataTableView;
            if (c != null)
            {
                c.a(this, A_1);
            }
        }

        internal void a(DataColumn A_0, object A_1, PropertyDescriptor A_2)
        {
            if ((this.Row != null) && !this.h)
            {
                DbDataTable table = (DbDataTable) this.Row.Table;
                A_1 = table.a(A_1, A_0.DataType, this, A_2);
                string columnName = A_0.ColumnName;
                object obj2 = table.a(this.Row[columnName], A_0.DataType, this, A_2);
                if ((obj2 != null) || (A_1 != null))
                {
                    Type type = (obj2 != null) ? obj2.GetType() : A_1.GetType();
                    if ((type.IsValueType && ((obj2 == null) || !obj2.Equals(A_1))) || (!type.IsValueType && (obj2 != A_1)))
                    {
                        this.InEditMode = true;
                        DbDataTableView c = (DbDataTableView) this.c;
                        if (c.a(this.Row))
                        {
                            c.a(this.Row, false);
                            this.Row.BeginEdit();
                        }
                        this.Row[columnName] = A_1;
                        this.a(this, new PropertyChangedEventArgs(A_2.Name));
                    }
                }
            }
        }

        private bool a(System.Data.DataView A_0, object[] A_1, System.Data.DataView A_2, object[] A_3)
        {
            if ((A_0 != null) && (A_2 != null))
            {
                if (A_0 == null)
                {
                    return false;
                }
                if (!ReferenceEquals(A_0.GetType(), A_2.GetType()) || !ReferenceEquals(A_0.Table, A_2.Table))
                {
                    return false;
                }
                if (A_1.Length != A_3.Length)
                {
                    return false;
                }
                for (int i = 0; i < A_1.Length; i++)
                {
                    if ((A_1[i] != null) || (A_3[i] != null))
                    {
                        if (A_1[i] == null)
                        {
                            return false;
                        }
                        if (!A_1[i].Equals(A_3[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void BeginEdit()
        {
            DbDataTableView c = this.c as DbDataTableView;
            if (c != null)
            {
                c.d(this);
            }
            if (this.Row != null)
            {
                ((DbDataTableView) this.c).a(this.Row, true);
            }
        }

        public void CancelEdit()
        {
            this.e = false;
            if (this.Row != null)
            {
                DbDataTableView c = this.c as DbDataTableView;
                if (c != null)
                {
                    c.a(this.Row, false);
                }
                if ((c != null) && ReferenceEquals(c.AddNewRow, this))
                {
                    DataRow row = this.Row;
                    c.AddNewRow = null;
                    c.f.disableEvents++;
                    c.f.disableUpdateEvents++;
                    c.f.storeEvents++;
                    try
                    {
                        if (this.a.Row.RowState == DataRowState.Detached)
                        {
                            c.b(this.a.Row);
                        }
                        this.a.CancelEdit();
                        if (this.a.Row.RowState == DataRowState.Added)
                        {
                            this.a.Row.Delete();
                        }
                    }
                    finally
                    {
                        c.f.disableEvents--;
                        c.f.disableUpdateEvents--;
                        c.f.storeEvents--;
                    }
                    this.f = false;
                    c.f();
                }
                else
                {
                    this.InnerRowView.CancelEdit();
                    this.f = false;
                    c.a(this.c, new ListChangedEventArgs(ListChangedType.ItemChanged, this.Index));
                }
            }
        }

        public DbDataTableView CreateChildView(DataRelation dataRelation)
        {
            System.Data.DataView view;
            DbDataTable childTable;
            if (this.a == null)
            {
                view = null;
                childTable = dataRelation.ChildTable as DbDataTable;
            }
            else
            {
                view = this.a.CreateChildView(dataRelation);
                childTable = (DbDataTable) view.Table;
            }
            w objA = (w) this.RelationViews[dataRelation];
            object[] objArray = this.a(dataRelation);
            bool flag = ReferenceEquals(objA, null);
            DbDataTableView a = null;
            if (!flag)
            {
                a = objA.a;
                flag = !this.a(view, objArray, a.g, objA.b);
            }
            if (flag)
            {
                a = new DbDataTableView(childTable, view) {
                    h = this
                };
                this.RelationViews[dataRelation] = new w(a, objArray);
            }
            return a;
        }

        public DbDataTableView CreateChildView(string relationName) => 
            this.CreateChildView(((DbDataTableView) this.c).f.ChildRelations[relationName]);

        public void Delete()
        {
            this.c.Remove(this);
        }

        public void EndEdit()
        {
            if (!this.h)
            {
                this.h = true;
                try
                {
                    if ((this.b != null) && (this.c != null))
                    {
                        DbDataTableView c = this.c as DbDataTableView;
                        if (c != null)
                        {
                            c.a(this.Row, false);
                            if (ReferenceEquals(c.AddNewRow, this))
                            {
                                try
                                {
                                    c.k();
                                    c.b(this.Index);
                                }
                                finally
                                {
                                    c.f();
                                }
                            }
                            else if (this.NeededUpdate)
                            {
                                try
                                {
                                    c.c(this);
                                    c.f();
                                }
                                catch
                                {
                                    if ((c.DataTable == null) || c.DataTable.CancelEditRowIfUpdateFailed)
                                    {
                                        this.Row.CancelEdit();
                                    }
                                    throw;
                                }
                            }
                        }
                        this.Row.EndEdit();
                    }
                    this.f = false;
                }
                finally
                {
                    this.h = false;
                }
            }
        }

        public void Refresh()
        {
            ((DbDataTableView) this.c).RefreshRow(this);
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes() => 
            (this.a == null) ? TypeDescriptor.GetAttributes(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetAttributes();

        string ICustomTypeDescriptor.GetClassName() => 
            (this.a == null) ? TypeDescriptor.GetClassName(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetClassName();

        string ICustomTypeDescriptor.GetComponentName() => 
            (this.a == null) ? TypeDescriptor.GetComponentName(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetComponentName();

        TypeConverter ICustomTypeDescriptor.GetConverter() => 
            (this.a == null) ? TypeDescriptor.GetConverter(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetConverter();

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => 
            (this.a == null) ? TypeDescriptor.GetDefaultEvent(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetDefaultEvent();

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => 
            (this.a == null) ? TypeDescriptor.GetDefaultProperty(this.Row, true) : ((ICustomTypeDescriptor) this.a).GetDefaultProperty();

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => 
            (this.a == null) ? TypeDescriptor.GetEditor(this.Row, editorBaseType, true) : ((ICustomTypeDescriptor) this.a).GetEditor(editorBaseType);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => 
            TypeDescriptor.GetEvents(this.Row, true);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => 
            TypeDescriptor.GetEvents(this.Row, attributes, true);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => 
            ((DbDataTable) this.b.Table).c((PropertyDescriptor[]) null);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => 
            ((DbDataTable) this.b.Table).c((PropertyDescriptor[]) null);

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => 
            this;

        IList IListSource.GetList() => 
            null;

        private bool NeededUpdate =>
            this.f || (((this.b.RowState != DataRowState.Unchanged) && (this.b.RowState != DataRowState.Detached)) || this.b.HasVersion(DataRowVersion.Proposed));

        bool IListSource.ContainsListCollection =>
            false;

        internal DataRowView InnerRowView
        {
            get => 
                this.a;
            set => 
                this.a = value;
        }

        public DataRow Row
        {
            get => 
                this.b;
            set => 
                this.b = value;
        }

        internal bool InEditMode
        {
            get => 
                this.f;
            set => 
                this.f = value;
        }

        public int Index =>
            this.d;

        internal int IndexInternal
        {
            set => 
                this.d = value;
        }

        internal Hashtable RelationViews
        {
            get
            {
                this.i ??= new Hashtable();
                return this.i;
            }
        }

        public object this[string columnName]
        {
            get
            {
                if (this.Row == null)
                {
                    return null;
                }
                PropertyDescriptor descriptor1 = ((ICustomTypeDescriptor) this).GetProperties()[columnName];
                if (descriptor1 == null)
                {
                    throw new InvalidOperationException();
                }
                return descriptor1.GetValue(this);
            }
            set
            {
                PropertyDescriptor descriptor1 = ((ICustomTypeDescriptor) this).GetProperties()[columnName];
                if (descriptor1 == null)
                {
                    throw new InvalidOperationException();
                }
                descriptor1.SetValue(this, value);
            }
        }

        public object this[int index]
        {
            get
            {
                if (this.Row == null)
                {
                    return null;
                }
                PropertyDescriptor descriptor1 = ((ICustomTypeDescriptor) this).GetProperties()[index];
                if (descriptor1 == null)
                {
                    throw new InvalidOperationException();
                }
                return descriptor1.GetValue(this);
            }
            set
            {
                PropertyDescriptor descriptor1 = ((ICustomTypeDescriptor) this).GetProperties()[index];
                if (descriptor1 == null)
                {
                    throw new InvalidOperationException();
                }
                descriptor1.SetValue(this, value);
            }
        }

        public bool IsNew =>
            this.e;

        public bool IsEdit =>
            (this.Row == null) || (this.Row.HasVersion(DataRowVersion.Proposed) || ((DbDataTableView) this.c).a(this.Row));

        public string Error =>
            (this.a != null) ? ((IDataErrorInfo) this.a).Error : string.Empty;

        string IDataErrorInfo.this[string columnName] =>
            ((this.InnerRowView == null) || !this.InnerRowView.DataView.Table.Columns.Contains(columnName)) ? string.Empty : ((IDataErrorInfo) this.InnerRowView)[columnName];

        public DbDataTableView DataView =>
            (DbDataTableView) this.c;
    }
}

