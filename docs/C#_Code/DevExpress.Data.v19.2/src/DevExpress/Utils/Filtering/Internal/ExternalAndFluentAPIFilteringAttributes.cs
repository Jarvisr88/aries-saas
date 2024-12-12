namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Utils.MVVM;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExternalAndFluentAPIFilteringAttributes
    {
        private static readonly Attribute[] NoAttributes = new Attribute[0];
        private static ConcurrentDictionary<Type, IGetExternalAndFluentAPIAttributesProxy> getExternalAndFluentAPIAttributesCache = new ConcurrentDictionary<Type, IGetExternalAndFluentAPIAttributesProxy>();
        private static ConcurrentDictionary<Type, IGetExternalAndFluentAPIAttributesProxy> getExternalAndFluentAPIFilteringAttributesCache = new ConcurrentDictionary<Type, IGetExternalAndFluentAPIAttributesProxy>();
        private static Type metadataHelperType;

        private static IGetExternalAndFluentAPIAttributesProxy EnsureAttributesGetter(Type type)
        {
            Type[] types = new Type[] { typeof(Type), typeof(string) };
            MethodInfo method = type.GetMethod("GetExternalAndFluentAPIAttributes", types);
            MethodInfo info3 = method;
            if (method == null)
            {
                MethodInfo local1 = method;
                Type[] typeArray2 = new Type[] { typeof(Type), typeof(string) };
                MethodInfo info2 = type.GetMethod("GetExternalAndFluentAPIAttrbutes", typeArray2);
                info3 = info2;
                if (info2 == null)
                {
                    MethodInfo local2 = info2;
                    Type[] typeArray3 = new Type[] { typeof(Type), typeof(string) };
                    info3 = type.GetMethod("GetExtenalAndFluentAPIAttrbutes", typeArray3);
                }
            }
            MethodInfo mInfo = info3;
            return ((mInfo != null) ? new GetExternalAndFluentAPIAttributesProxy(mInfo) : null);
        }

        private static IGetExternalAndFluentAPIAttributesProxy EnsureFilterAttributesGetter(Type type)
        {
            Type[] types = new Type[] { typeof(Type), typeof(string) };
            MethodInfo method = type.GetMethod("GetExternalAndFluentAPIFilteringAttributes", types);
            MethodInfo info2 = method;
            if (method == null)
            {
                MethodInfo local1 = method;
                Type[] typeArray2 = new Type[] { typeof(Type), typeof(string) };
                info2 = type.GetMethod("GetExternalAndFluentAPIFilteringAttrbutes", typeArray2);
            }
            MethodInfo mInfo = info2;
            return ((mInfo != null) ? new GetExternalAndFluentAPIAttributesProxy(mInfo) : null);
        }

        internal static IEnumerable<Attribute> GetAttributes(Type componentType, string memberName)
        {
            object obj1;
            Type metadataHelperType = GetMetadataHelperType();
            if (metadataHelperType == null)
            {
                obj1 = null;
            }
            else
            {
                IEnumerable<Attribute> enumerable1 = GetExternalAndFluentAPIFilteringAttributes(metadataHelperType, componentType, memberName);
                obj1 = enumerable1;
                if (enumerable1 == null)
                {
                    IEnumerable<Attribute> local1 = enumerable1;
                    return GetExternalAndFluentAPIAttributes(metadataHelperType, componentType, memberName);
                }
            }
            return (IEnumerable<Attribute>) obj1;
        }

        internal static IEnumerable<Attribute> GetExternalAndFluentAPIAttributes(Type metadataHelperType, Type componentType, string memberName)
        {
            IGetExternalAndFluentAPIAttributesProxy orAdd = getExternalAndFluentAPIAttributesCache.GetOrAdd(metadataHelperType, new Func<Type, IGetExternalAndFluentAPIAttributesProxy>(ExternalAndFluentAPIFilteringAttributes.EnsureAttributesGetter));
            return ((orAdd != null) ? orAdd.Get(componentType, memberName) : ((IEnumerable<Attribute>) NoAttributes));
        }

        private static IEnumerable<Attribute> GetExternalAndFluentAPIFilteringAttributes(Type metadataHelperType, Type componentType, string memberName)
        {
            IGetExternalAndFluentAPIAttributesProxy orAdd = getExternalAndFluentAPIFilteringAttributesCache.GetOrAdd(metadataHelperType, new Func<Type, IGetExternalAndFluentAPIAttributesProxy>(ExternalAndFluentAPIFilteringAttributes.EnsureFilterAttributesGetter));
            return ((orAdd != null) ? orAdd.Get(componentType, memberName) : ((IEnumerable<Attribute>) NoAttributes));
        }

        internal static Type GetMetadataHelperType() => 
            MVVMAssemblyProxy.GetMvvmType(ref metadataHelperType, "Native.MetadataHelper");

        internal static void Reset()
        {
            metadataHelperType = null;
            getExternalAndFluentAPIAttributesCache.Clear();
            getExternalAndFluentAPIFilteringAttributesCache.Clear();
        }

        private sealed class GetExternalAndFluentAPIAttributesProxy : ExternalAndFluentAPIFilteringAttributes.IGetExternalAndFluentAPIAttributesProxy
        {
            private readonly Func<Type, string, IEnumerable<Attribute>> getAttributes;

            public GetExternalAndFluentAPIAttributesProxy(MethodInfo mInfo)
            {
                ParameterExpression expression = Expression.Parameter(typeof(Type), "componentType");
                ParameterExpression expression2 = Expression.Parameter(typeof(string), "memberName");
                MethodCallExpression body = Expression.Call(mInfo, expression, expression2);
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
                this.getAttributes = Expression.Lambda<Func<Type, string, IEnumerable<Attribute>>>(body, parameters).Compile();
            }

            IEnumerable<Attribute> ExternalAndFluentAPIFilteringAttributes.IGetExternalAndFluentAPIAttributesProxy.Get(Type componentType, string memberName) => 
                this.getAttributes(componentType, memberName);
        }

        private interface IGetExternalAndFluentAPIAttributesProxy
        {
            IEnumerable<Attribute> Get(Type componentType, string memberName);
        }
    }
}

