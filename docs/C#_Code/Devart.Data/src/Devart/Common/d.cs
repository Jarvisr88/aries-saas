namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Data;

    internal class d : PropertyDescriptor
    {
        private DataColumn a;
        private DataRelation b;

        public d(DataColumn A_0) : base(A_0.ColumnName, null)
        {
            this.a = A_0;
        }

        public d(DataRelation A_0) : base(A_0.RelationName, null)
        {
            this.b = A_0;
        }

        internal DataColumn a() => 
            this.a;

        internal void a(DataColumn A_0)
        {
            this.a = A_0;
        }

        public override void a(object A_0)
        {
        }

        public override void a(object A_0, object A_1)
        {
            DbDataRowView view = A_0 as DbDataRowView;
            if (view != null)
            {
                view.a(this.a(), A_1, this);
            }
        }

        public override bool b(object A_0)
        {
            if (!(A_0 is Devart.Common.d))
            {
                return false;
            }
            Devart.Common.d d = (Devart.Common.d) A_0;
            return (ReferenceEquals(d.c(), this.c()) && ReferenceEquals(d.a(), this.a()));
        }

        internal DataRelation c() => 
            this.b;

        public override bool c(object A_0) => 
            false;

        public override bool d(object A_0) => 
            true;

        public override int e() => 
            (this.c() != null) ? (this.c().GetHashCode() ^ this.a().GetHashCode()) : this.a().GetHashCode();

        public override object e(object A_0)
        {
            DbDataRowView view = A_0 as DbDataRowView;
            if (view == null)
            {
                return DBNull.Value;
            }
            if ((this.a != null) && ((view.Row != null) && !ReferenceEquals(view.Row.Table, this.a.Table)))
            {
                this.a = view.Row.Table.Columns[this.a.ColumnName];
            }
            return view.a(this.a, this);
        }

        public override string get_DisplayName() => 
            (this.a != null) ? this.a.Caption : base.DisplayName;

        public override Type System.ComponentModel.PropertyDescriptor.ComponentType =>
            typeof(DbDataRowView);

        public override bool System.ComponentModel.PropertyDescriptor.IsReadOnly =>
            this.a.ReadOnly;

        public override Type System.ComponentModel.PropertyDescriptor.PropertyType =>
            (this.b == null) ? ((DbDataTable) this.a.Table).a(this.a.DataType) : typeof(IBindingList);

        public override TypeConverter System.ComponentModel.PropertyDescriptor.Converter
        {
            get
            {
                object[] customAttributes = this.a.DataType.GetCustomAttributes(typeof(TypeConverterAttribute), true);
                return (((customAttributes == null) || (customAttributes.Length == 0)) ? base.Converter : ((TypeConverter) Activator.CreateInstance(Type.GetType(((TypeConverterAttribute) customAttributes[0]).ConverterTypeName))));
            }
        }
    }
}

