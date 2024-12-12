namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class CompileHelper
    {
        private static readonly ConcurrentDictionary<Type, bool> _isPublicCache;

        static CompileHelper();
        public static bool CanDynamicMethodWithSkipVisibility();
        private static bool DecideIsPublicExposable(Type t);
        private static bool DecideIsPublicExposableCore(Type t);
        private static bool DecideIsPublicExposableGeneric(Type t, Dictionary<Type, object> visited);
        public static MemberInfo FindPropertyOrField(Type typeToSearchFrom, string property, bool includeNonPublic, bool ignoreCase);
        [IteratorStateMachine(typeof(CompileHelper.<FromDescendatsToRoots>d__7))]
        private static IEnumerable<Type> FromDescendatsToRoots(Type t);
        [IteratorStateMachine(typeof(CompileHelper.<GetMembers>d__8))]
        private static IEnumerable<MemberInfo> GetMembers(Type t, string memberName, BindingFlags flags);
        private static bool IsPoisonousTypeBuilder(Type type);
        public static bool IsPublicExposable(Type t);


    }
}

