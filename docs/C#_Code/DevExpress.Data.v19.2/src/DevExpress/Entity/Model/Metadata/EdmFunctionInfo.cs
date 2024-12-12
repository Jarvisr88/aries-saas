namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class EdmFunctionInfo : RuntimeWrapper
    {
        public EdmFunctionInfo(object source) : base(EdmConst.EdmFunction, source)
        {
        }

        public string Name
        {
            get
            {
                string str = base.GetPropertyAccessor("FunctionName").Value as string;
                return ((str != null) ? str : (base.GetPropertyAccessor("Name").Value as string));
            }
        }

        public FunctionParameterInfo[] Parameters
        {
            get
            {
                Func<object, FunctionParameterInfo> selector = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<object, FunctionParameterInfo> local1 = <>c.<>9__4_0;
                    selector = <>c.<>9__4_0 = p => new FunctionParameterInfo(p);
                }
                return (base.GetPropertyAccessor("Parameters").Value as IEnumerable<object>).Select<object, FunctionParameterInfo>(selector).ToArray<FunctionParameterInfo>();
            }
        }

        public EdmComplexTypePropertyInfo[] ResultTypeProperties
        {
            get
            {
                object source = base.GetPropertyAccessor("ReturnParameter").Value;
                return ((source != null) ? new FunctionParameterInfo(source).ResultTypeProperties : null);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EdmFunctionInfo.<>c <>9 = new EdmFunctionInfo.<>c();
            public static Func<object, FunctionParameterInfo> <>9__4_0;

            internal FunctionParameterInfo <get_Parameters>b__4_0(object p) => 
                new FunctionParameterInfo(p);
        }
    }
}

