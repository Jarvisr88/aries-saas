namespace DevExpress.Data.WcfLinq.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class WcfDataServiceQueryHelper
    {
        private static readonly Dictionary<Type, WcfDataServiceQueryHelper.QueryableHelper> queryableCache;
        private static readonly Dictionary<Type, WcfDataServiceQueryHelper.ContextHelper> contextCache;
        private static readonly ReaderWriterLockSlim locker;

        static WcfDataServiceQueryHelper();
        public static IQueryable<T> AddQueryOption<T>(IQueryable<T> source, string name, object value);
        public static IEnumerable<string> ContextExecute(object context, Uri uri);
        public static Uri ContextGetBaseUri(object context);
        public static IEnumerable<T> Execute<T>(IQueryable<T> source);
        public static IEnumerable<T> ExecuteWithTotalCount<T>(IQueryable<T> source, out long totalCount);
        public static object GetContext(IQueryable source);
        private static WcfDataServiceQueryHelper.ContextHelper GetContextHelper(object context);
        private static T GetHelperCore<T>(object source, Dictionary<Type, T> cache, Func<Type, T> createNew) where T: class;
        private static WcfDataServiceQueryHelper.QueryableHelper GetQueryableHelper(object source);
        public static Uri GetRequestUri(IQueryable source);
        public static void SubscribeToSendingRequest(object context, Func<Uri, string, string> resolveVersionByUri);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WcfDataServiceQueryHelper.<>c <>9;
            public static Func<Type, WcfDataServiceQueryHelper.QueryableHelper> <>9__3_0;
            public static Func<Type, WcfDataServiceQueryHelper.ContextHelper> <>9__4_0;

            static <>c();
            internal WcfDataServiceQueryHelper.ContextHelper <GetContextHelper>b__4_0(Type t);
            internal WcfDataServiceQueryHelper.QueryableHelper <GetQueryableHelper>b__3_0(Type t);
        }

        private class ContextHelper
        {
            private readonly Func<object, Uri> getBaseUriHandler;
            private readonly Func<object, Uri, IEnumerable<string>> executeHandler;
            private readonly EventInfo sendingRequestEvent;
            private readonly Type sendingRequestEventArgsType;
            private readonly Type sendingRequestEventArgsEventHandlerType;
            private readonly EventInfo sendingRequest2Event;
            private readonly Type sendingRequest2EventArgsType;
            private readonly Type sendingRequest2EventArgsEventHandlerType;

            public ContextHelper(Type instanceType);
            private Delegate CreateSendingRequest2Handler(Func<Uri, string, string> resolveVersionByUri);
            private Delegate CreateSendingRequestHandler(Func<Uri, string, string> resolveVersionByUri);
            public IEnumerable<string> Execute(object context, Uri uri);
            public Uri GetBaseUri(object context);
            public void SubscribeToSendingRequest(object context, Func<Uri, string, string> resolveVersionByUri);

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly WcfDataServiceQueryHelper.ContextHelper.<>c <>9;
                public static Func<ParameterInfo, bool> <>9__8_1;
                public static Func<MethodInfo, bool> <>9__8_0;

                static <>c();
                internal bool <.ctor>b__8_0(MethodInfo mi);
                internal bool <.ctor>b__8_1(ParameterInfo pi);
            }
        }

        private class QueryableHelper
        {
            private readonly Func<object, string, object, object> addQueryOptionHandler;
            private readonly Func<object, object> executeHandler;
            private readonly Func<object, object> includeTotalCountHandler;
            private readonly Func<object, long> getTotalCountHandler;
            private readonly Func<object, Uri> getRequestUriHandler;
            private readonly Func<object, object> getContextHandler;

            public QueryableHelper(Type instanceType);
            public IQueryable<T> AddQueryOption<T>(IQueryable<T> source, string name, object value);
            public IEnumerable<T> Execute<T>(IQueryable<T> source);
            public object GetContext(IQueryable source);
            public Uri GetRequestUri(IQueryable source);
            public long GetTotalCount<T>(IEnumerable<T> executeResult);
            public IQueryable<T> IncludeTotalCount<T>(IQueryable<T> source);
        }
    }
}

