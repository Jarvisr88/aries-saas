namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Concurrent;

    public static class GenericTypeHelper
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, Type>> _SingleArgGenericTypeDefinitions;
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type[], Type>> _ManyArgsGenericTypeDefinitions;

        static GenericTypeHelper();
        public static Type GetGenericIListType(Type listType);
        public static Type GetGenericIListTypeArgument(Type elementType);
        public static Type GetGenericTypeArgument(Type elementType, Type genericType);
        private static ConcurrentDictionary<Type[], Type> GetManyArgsGenericTypeDefinition(Type genericTypeDefinition);
        private static ConcurrentDictionary<Type, Type> GetSingleArgGenericTypeDefinition(Type genericTypeDefinition);
        public static Type MakeGenericType<GenericTypeDefinition>(Type typeArgument);
        public static Type MakeGenericType<GenericTypeDefinition>(params Type[] typeArguments);
        public static Type MakeGenericType(Type genericTypeDefinition, Type typeArgument);
        public static Type MakeGenericType(Type genericTypeDefinition, params Type[] typeArguments);
        public static Func<Type[], Type> MakeGenericTypeMakerManyArgs(Type genericTypeDefinition);
        public static Func<Type, Type> MakeGenericTypeMakerSingleArg(Type genericTypeDefinition);
        public static Func<Type, Type, Type, Type> MakeGenericTypeMakerThreeArgs(Type genericTypeDefinition);
        public static Func<Type, Type, Type> MakeGenericTypeMakerTwoArgs(Type genericTypeDefinition);
        public static Func<Type[], Type> MakeGenericTypeMakerUnknownArgs(Type genericTypeDefinition);
        private static Type MakeManyArgsGenericTypeCore(ConcurrentDictionary<Type[], Type> dict, Type gt, params Type[] args);
        private static Type MakeSingleArgGenericTypeCore(ConcurrentDictionary<Type, Type> dict, Type gt, Type arg);

        private static class ManyArgsGenericTypeDefinitionStore<GenericTypeDefinition>
        {
            private static readonly Type GenericTypedefinitiontype;
            private static readonly ConcurrentDictionary<Type[], Type> Dictionary;

            static ManyArgsGenericTypeDefinitionStore();
            public static Type MakeGenericType(Type[] arguments);
        }

        private static class SingleArgGenericTypeDefinitionStore<GenericTypeDefinition>
        {
            private static readonly Type GenericTypedefinitiontype;
            private static readonly ConcurrentDictionary<Type, Type> Dictionary;

            static SingleArgGenericTypeDefinitionStore();
            public static Type MakeGenericType(Type argument);
        }
    }
}

