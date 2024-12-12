namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public static class FakedListCreator
    {
        public static IList CreateFakedList(object genericList);
        public static IList CreateGenericList(object listSource);
        private static IList CreateGenericListCore(object listSource, Func<Type, IList> create);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FakedListCreator.<>c <>9;
            public static Func<Type, IList> <>9__1_0;
            public static Predicate<Type> <>9__2_0;

            static <>c();
            internal IList <CreateGenericList>b__1_0(Type argumentType);
            internal bool <CreateGenericListCore>b__2_0(Type item);
        }
    }
}

