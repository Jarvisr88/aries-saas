namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EntityTypeBaseInfo : RuntimeWrapper
    {
        private IEnumerable<EdmMemberInfo> properties;
        private IEnumerable<EdmMemberInfo> navigationProperties;
        private IEnumerable<EdmMemberInfo> keyMembers;

        public EntityTypeBaseInfo(object source) : base(EdmConst.EntityTypeBase, source)
        {
        }

        public object BaseType =>
            base.GetPropertyAccessor("BaseType").Value;

        public string FullName =>
            base.GetPropertyAccessor("FullName").Value as string;

        public string Name =>
            base.GetPropertyAccessor("Name").Value as string;

        public bool Abstract =>
            (bool) base.GetPropertyAccessor("Abstract").Value;

        public string BaseTypeFullName =>
            base.GetPropertyAccessor("BaseType.FullName").Value as string;

        public DevExpress.Entity.Model.Metadata.BuiltInTypeKind BuiltInTypeKind =>
            ConvertEnum<DevExpress.Entity.Model.Metadata.BuiltInTypeKind>(base.GetPropertyAccessor("BuiltInTypeKind").Value);

        public IEnumerable<EdmMemberInfo> Properties
        {
            get
            {
                if (this.properties == null)
                {
                    IEnumerable source = base.GetPropertyAccessor("Properties").Value as IEnumerable;
                    Func<object, EdmMemberInfo> selector = <>c.<>9__15_0;
                    if (<>c.<>9__15_0 == null)
                    {
                        Func<object, EdmMemberInfo> local1 = <>c.<>9__15_0;
                        selector = <>c.<>9__15_0 = x => new EdmMemberInfo(x);
                    }
                    this.properties = source.Cast<object>().Select<object, EdmMemberInfo>(selector);
                }
                return this.properties;
            }
        }

        public IEnumerable<EdmMemberInfo> NavigationProperties
        {
            get
            {
                if (this.navigationProperties == null)
                {
                    IEnumerable source = base.GetPropertyAccessor("NavigationProperties").Value as IEnumerable;
                    Func<object, EdmMemberInfo> selector = <>c.<>9__18_0;
                    if (<>c.<>9__18_0 == null)
                    {
                        Func<object, EdmMemberInfo> local1 = <>c.<>9__18_0;
                        selector = <>c.<>9__18_0 = x => new EdmMemberInfo(x);
                    }
                    this.navigationProperties = source.Cast<object>().Select<object, EdmMemberInfo>(selector);
                }
                return this.navigationProperties;
            }
        }

        public IEnumerable<EdmMemberInfo> KeyMembers
        {
            get
            {
                if (this.keyMembers == null)
                {
                    IEnumerable source = base.GetPropertyAccessor("KeyMembers").Value as IEnumerable;
                    Func<object, EdmMemberInfo> selector = <>c.<>9__21_0;
                    if (<>c.<>9__21_0 == null)
                    {
                        Func<object, EdmMemberInfo> local1 = <>c.<>9__21_0;
                        selector = <>c.<>9__21_0 = x => new EdmMemberInfo(x);
                    }
                    this.keyMembers = source.Cast<object>().Select<object, EdmMemberInfo>(selector);
                }
                return this.keyMembers;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EntityTypeBaseInfo.<>c <>9 = new EntityTypeBaseInfo.<>c();
            public static Func<object, EdmMemberInfo> <>9__15_0;
            public static Func<object, EdmMemberInfo> <>9__18_0;
            public static Func<object, EdmMemberInfo> <>9__21_0;

            internal EdmMemberInfo <get_KeyMembers>b__21_0(object x) => 
                new EdmMemberInfo(x);

            internal EdmMemberInfo <get_NavigationProperties>b__18_0(object x) => 
                new EdmMemberInfo(x);

            internal EdmMemberInfo <get_Properties>b__15_0(object x) => 
                new EdmMemberInfo(x);
        }
    }
}

