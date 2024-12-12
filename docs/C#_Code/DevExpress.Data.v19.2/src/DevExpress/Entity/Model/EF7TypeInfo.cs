namespace DevExpress.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class EF7TypeInfo : IEntityTypeInfo, IEntityProperties
    {
        private readonly System.Type type;
        private List<IEdmPropertyInfo> allProperties;

        public EF7TypeInfo(System.Type type)
        {
            this.type = type;
        }

        public IEdmPropertyInfo GetDependentProperty(IEdmPropertyInfo foreignKey) => 
            null;

        public IEdmPropertyInfo GetForeignKey(IEdmPropertyInfo dependentProperty) => 
            null;

        private void InitProperties()
        {
            PropertyInfo[] properties = this.type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Func<PropertyInfo, EF7PropertyInfo> selector = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<PropertyInfo, EF7PropertyInfo> local1 = <>c.<>9__10_0;
                selector = <>c.<>9__10_0 = p => new EF7PropertyInfo(p);
            }
            this.allProperties = new List<IEdmPropertyInfo>((IEnumerable<IEdmPropertyInfo>) properties.Select<PropertyInfo, EF7PropertyInfo>(selector));
        }

        public IEnumerable<IEdmPropertyInfo> AllProperties
        {
            get
            {
                if (this.allProperties == null)
                {
                    this.InitProperties();
                }
                return this.allProperties;
            }
        }

        public IEnumerable<IEdmPropertyInfo> KeyMembers =>
            null;

        public IEnumerable<IEdmAssociationPropertyInfo> LookupTables =>
            null;

        public System.Type Type =>
            this.type;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EF7TypeInfo.<>c <>9 = new EF7TypeInfo.<>c();
            public static Func<PropertyInfo, EF7PropertyInfo> <>9__10_0;

            internal EF7PropertyInfo <InitProperties>b__10_0(PropertyInfo p) => 
                new EF7PropertyInfo(p);
        }
    }
}

