namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class CriteriaCompilerContextDescriptorReflective : CriteriaCompilerContextDescriptorDefaultBase
    {
        public static readonly CriteriaCompilerContextDescriptorReflective Instance;

        static CriteriaCompilerContextDescriptorReflective();
        private CriteriaCompilerContextDescriptorReflective();
        private static object GetThis(object obj);
        protected override Expression MakePropertyAccessCore(Expression baseExpression, string property);
        protected override Expression MakeThisAccess(Expression baseExpression);

        public override Type ObjectType { get; }

        public class ReflectiveAccessor
        {
            public readonly string PropertyName;
            private Func<object, object> CachedAccesor;
            private bool AccessorCached;

            public ReflectiveAccessor(string propertyName);
            private object AccessThroughPropertyDescriptors(PropertyDescriptorCollection props, object row);
            public object GetReflectiveValue(object source);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CriteriaCompilerContextDescriptorReflective.ReflectiveAccessor.<>c <>9;
                public static Func<MemberInfo, Type> <>9__4_1;
                public static Func<Type, Type, Type> <>9__4_2;

                static <>c();
                internal Type <GetReflectiveValue>b__4_1(MemberInfo mi);
                internal Type <GetReflectiveValue>b__4_2(Type acc, Type next);
            }
        }

        public class TypedListUndObjectPair
        {
            public readonly PropertyDescriptorCollection PDs;
            public object Row;

            public TypedListUndObjectPair(PropertyDescriptorCollection _PDs);
        }
    }
}

