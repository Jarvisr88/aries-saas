namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EdmComplexTypeInfo : RuntimeWrapper
    {
        public EdmComplexTypeInfo(object value) : base(EdmConst.ComplexType, value)
        {
        }

        public EdmComplexTypePropertyInfo[] Properties
        {
            get
            {
                IEnumerable<object> source = base.GetPropertyAccessor("Properties").Value as IEnumerable<object>;
                if (source == null)
                {
                    return new EdmComplexTypePropertyInfo[0];
                }
                Func<object, EdmComplexTypePropertyInfo> selector = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Func<object, EdmComplexTypePropertyInfo> local1 = <>c.<>9__2_0;
                    selector = <>c.<>9__2_0 = p => new EdmComplexTypePropertyInfo(p);
                }
                return source.Select<object, EdmComplexTypePropertyInfo>(selector).ToArray<EdmComplexTypePropertyInfo>();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EdmComplexTypeInfo.<>c <>9 = new EdmComplexTypeInfo.<>c();
            public static Func<object, EdmComplexTypePropertyInfo> <>9__2_0;

            internal EdmComplexTypePropertyInfo <get_Properties>b__2_0(object p) => 
                new EdmComplexTypePropertyInfo(p);
        }
    }
}

