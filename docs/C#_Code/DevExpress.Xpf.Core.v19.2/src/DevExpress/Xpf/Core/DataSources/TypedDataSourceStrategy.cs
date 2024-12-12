namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class TypedDataSourceStrategy : DataSourceStrategyBase
    {
        public TypedDataSourceStrategy(ITypedDataSource owner) : base(owner)
        {
        }

        public override bool CanUpdateData() => 
            base.CanUpdateData() && (this.Owner.AdapterType != null);

        public override object CreateContextIstance()
        {
            object contextInstance = base.CreateContextIstance();
            object[] parameters = new object[] { this.GetDataMemberValue(contextInstance) };
            this.Owner.AdapterType.GetMethods().First<MethodInfo>(delegate (MethodInfo m) {
                ParameterInfo[] source = m.GetParameters();
                return ((m.Name == "Fill") && ((m.ReturnType == typeof(int)) && ((source.Count<ParameterInfo>() == 1) && (source[0].ParameterType == this.OwnerDataMemberType))));
            }).Invoke(Activator.CreateInstance(this.Owner.AdapterType), parameters);
            return contextInstance;
        }

        public override object CreateData(object value) => 
            value.GetType().GetProperty("DefaultView").GetValue(value, null);

        public override Type GetDataObjectType() => 
            this.OwnerDataMemberType.BaseType.GetGenericArguments()[0];

        public override List<DesignTimePropertyInfo> GetDesignTimeProperties()
        {
            object obj2 = base.CreateContextIstance();
            Func<PropertyInfo, bool> predicate = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__4_0;
                predicate = <>c.<>9__4_0 = p => p.Name == "Tables";
            }
            PropertyInfo info = obj2.GetType().GetProperties().FirstOrDefault<PropertyInfo>(predicate);
            if (info == null)
            {
                return null;
            }
            DataTableCollection tables = info.GetValue(obj2, null) as DataTableCollection;
            if ((tables == null) || (tables.Count != 1))
            {
                return null;
            }
            List<DesignTimePropertyInfo> list = new List<DesignTimePropertyInfo>();
            foreach (DataColumn column in tables[0].Columns)
            {
                list.Add(new DesignTimePropertyInfo(column.ColumnName, column.DataType, column.ReadOnly));
            }
            return list;
        }

        private ITypedDataSource Owner =>
            (ITypedDataSource) base.owner;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypedDataSourceStrategy.<>c <>9 = new TypedDataSourceStrategy.<>c();
            public static Func<PropertyInfo, bool> <>9__4_0;

            internal bool <GetDesignTimeProperties>b__4_0(PropertyInfo p) => 
                p.Name == "Tables";
        }
    }
}

