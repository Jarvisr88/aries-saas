namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class MetadataHelper
    {
        private static readonly List<Tuple<Type, Type>> internalMetadataProviders = new List<Tuple<Type, Type>>();
        private static readonly List<WeakReference> locators = new List<WeakReference>();
        private const string Exception_MetadataShouldBePublic = "The {0} type should be public";
        private static readonly ConcurrentDictionary<Type, IAttributesProvider> Providers = new ConcurrentDictionary<Type, IAttributesProvider>();
        private static readonly ConcurrentDictionary<Type, IAttributesProvider> FilteringProviders = new ConcurrentDictionary<Type, IAttributesProvider>();
        private const string BuildMetadataMethodName = "BuildMetadata";

        public static void AddMetadata<TMetadata>()
        {
            AddMetadata(typeof(TMetadata));
        }

        public static void AddMetadata<T, TMetadata>()
        {
            AddMetadata(typeof(T), typeof(TMetadata));
        }

        private static void AddMetadata(IEnumerable<Tuple<Type, Type>> newInfoList)
        {
            CheckMetadata(newInfoList);
            List<Tuple<Type, Type>> internalMetadataProviders = MetadataHelper.internalMetadataProviders;
            lock (internalMetadataProviders)
            {
                MetadataHelper.internalMetadataProviders.AddRange(newInfoList);
            }
            UpdateLocators();
            foreach (Tuple<Type, Type> tuple in newInfoList)
            {
                IAttributesProvider provider;
                Providers.TryRemove(tuple.Item1, out provider);
                FilteringProviders.TryRemove(tuple.Item1, out provider);
            }
        }

        public static void AddMetadata(Type metadataType)
        {
            AddMetadata(GetMetadataInfoList(metadataType));
        }

        public static void AddMetadata(Type type, Type metadataType)
        {
            Tuple<Type, Type> tuple = new Tuple<Type, Type>(type, metadataType);
            Tuple<Type, Type>[] newInfoList = new Tuple<Type, Type>[] { tuple };
            AddMetadata(newInfoList);
        }

        internal static void CheckMetadata(IEnumerable<Tuple<Type, Type>> metadataTypes)
        {
            foreach (Tuple<Type, Type> tuple in metadataTypes)
            {
                CheckMetadata(tuple.Item2);
            }
        }

        private static void CheckMetadata(Type metadataType)
        {
            if ((metadataType != null) && (!metadataType.IsPublic && !metadataType.IsNestedPublic))
            {
                throw new InvalidOperationException($"The {metadataType.Name} type should be public");
            }
        }

        public static void ClearMetadata()
        {
            List<Tuple<Type, Type>> internalMetadataProviders = MetadataHelper.internalMetadataProviders;
            lock (internalMetadataProviders)
            {
                MetadataHelper.internalMetadataProviders.Clear();
            }
            UpdateLocators();
            Providers.Clear();
            FilteringProviders.Clear();
        }

        private static IAttributesProvider CreateFilteringMetadataBuilder(Type componentType)
        {
            Type[] typeArguments = new Type[] { componentType };
            return (IAttributesProvider) Activator.CreateInstance(typeof(FilteringMetadataBuilder<>).MakeGenericType(typeArguments));
        }

        private static IAttributesProvider CreateMetadataBuilder(Type componentType)
        {
            Type[] typeArguments = new Type[] { componentType };
            return (IAttributesProvider) Activator.CreateInstance((componentType.IsEnum ? typeof(EnumMetadataBuilder<>) : typeof(MetadataBuilder<>)).MakeGenericType(typeArguments));
        }

        internal static Attribute[] GetAllAttributes(MemberInfo member, bool inherit = false)
        {
            IEnumerable<Attribute> second = GetExternalAndFluentAPIAttributes(member.ReflectedType, member.Name) ?? new Attribute[0];
            return Attribute.GetCustomAttributes(member, inherit).Concat<Attribute>(second).ToArray<Attribute>();
        }

        [IteratorStateMachine(typeof(<GetAllFilteringMetadataAttributes>d__27))]
        private static IEnumerable<IAttributesProvider> GetAllFilteringMetadataAttributes(Type metadataClassType, Type componentType)
        {
            if (componentType.IsGenericType && metadataClassType.IsGenericTypeDefinition)
            {
                metadataClassType = metadataClassType.MakeGenericType(componentType.GetGenericArguments());
            }
            yield return GetExternalMetadataAttributes(metadataClassType, componentType);
            yield return GetFluentAPIAttributes(metadataClassType, componentType);
            yield return GetFluentAPIAttributesFromStaticMethod(metadataClassType, componentType);
            yield return GetFluentAPIFilteringAttributes(metadataClassType, componentType);
            yield return GetFluentAPIFilteringAttributesFromStaticMethod(metadataClassType, componentType);
            yield return GetExternalAndFluentAPIFilteringAttributesCore(metadataClassType);
        }

        [IteratorStateMachine(typeof(<GetAllMetadataAttributes>d__26))]
        private static IEnumerable<IAttributesProvider> GetAllMetadataAttributes(Type metadataClassType, Type componentType)
        {
            if (componentType.IsGenericType && metadataClassType.IsGenericTypeDefinition)
            {
                metadataClassType = metadataClassType.MakeGenericType(componentType.GetGenericArguments());
            }
            yield return GetExternalMetadataAttributes(metadataClassType, componentType);
            yield return GetFluentAPIAttributes(metadataClassType, componentType);
            yield return GetFluentAPIAttributesFromStaticMethod(metadataClassType, componentType);
            yield return GetExternalAndFluentAPIAttributesCore(metadataClassType);
        }

        [Obsolete("Use the GetAttributesProvider method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static IAttributesProvider GetAttrbutesProvider(Type componentType, IMetadataLocator locator) => 
            GetAttributesProvider(componentType, locator);

        internal static T GetAttribute<T>(MemberInfo member, bool inherit = false) where T: Attribute => 
            GetAttributes<T>(member, inherit).FirstOrDefault<T>();

        internal static IEnumerable<T> GetAttributes<T>(MemberInfo member, bool inherit = false) where T: Attribute => 
            GetAllAttributes(member, inherit).OfType<T>();

        public static IAttributesProvider GetAttributesProvider(Type componentType, IMetadataLocator locator) => 
            CompositeMetadataAttributesProvider.Create(GetMetadataTypes(locator, componentType).GetProviders(componentType, false));

        private static IEnumerable<Type> GetAttributeTypes(Attribute attr)
        {
            Func<Type, Type> next = <>c.<>9__68_0;
            if (<>c.<>9__68_0 == null)
            {
                Func<Type, Type> local1 = <>c.<>9__68_0;
                next = <>c.<>9__68_0 = x => x.BaseType;
            }
            return LinqExtensions.Unfold<Type>(attr.GetType(), next, <>c.<>9__68_1 ??= x => (x == typeof(Attribute)));
        }

        private static IEnumerable<MethodInfo> GetBuildMetadataMethodsFromMatadataProvider(Type metadataClassType, Type componentType, Func<Type, Type, bool> isMetadataProviderType)
        {
            bool flag = metadataClassType.IsPublic || metadataClassType.IsNestedPublic;
            if (metadataClassType.IsAbstract || (!flag || (metadataClassType.GetConstructor(new Type[0]) == null)))
            {
                return Enumerable.Empty<MethodInfo>();
            }
            Func<Type, MethodInfo> selector = <>c.<>9__54_1;
            if (<>c.<>9__54_1 == null)
            {
                Func<Type, MethodInfo> local1 = <>c.<>9__54_1;
                selector = <>c.<>9__54_1 = x => x.GetMethod("BuildMetadata", BindingFlags.Public | BindingFlags.Instance);
            }
            return (from x in metadataClassType.GetInterfaces()
                where isMetadataProviderType(x, componentType)
                select x).Select<Type, MethodInfo>(selector);
        }

        private static IEnumerable<MethodInfo> GetBuildMetadataStaticMethods(Type metadataClassType, Type componentType, Func<Type, Type, bool> isMetadataBuilderType) => 
            metadataClassType.GetMethods(BindingFlags.Public | BindingFlags.Static).Where<MethodInfo>(delegate (MethodInfo x) {
                if ((x.Name != "BuildMetadata") || (x.ReturnType != typeof(void)))
                {
                    return false;
                }
                if (x.GetParameters().Length != 1)
                {
                    return false;
                }
                ParameterInfo info = x.GetParameters().Single<ParameterInfo>();
                return isMetadataBuilderType(info.ParameterType, componentType);
            });

        [Obsolete("Use the GetExternalAndFluentAPIAttributes method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<Attribute> GetExternalAndFluentAPIAttrbutes(Type componentType, string propertyName) => 
            GetExternalAndFluentAPIAttributes(componentType, propertyName);

        private static IEnumerable<Attribute> GetExternalAndFluentAPIAttributes(IAttributesProvider attributesProvider, string propertyName)
        {
            IAttributesProvider provider = attributesProvider;
            lock (provider)
            {
                var selector = <>c.<>9__36_0;
                if (<>c.<>9__36_0 == null)
                {
                    var local1 = <>c.<>9__36_0;
                    selector = <>c.<>9__36_0 = attr => from x in GetAttributeTypes(attr) select new { 
                        type = x,
                        value = attr
                    };
                }
                var keySelector = <>c.<>9__36_2;
                if (<>c.<>9__36_2 == null)
                {
                    var local2 = <>c.<>9__36_2;
                    keySelector = <>c.<>9__36_2 = x => x.type;
                }
                var func3 = <>c.<>9__36_3;
                if (<>c.<>9__36_3 == null)
                {
                    var local3 = <>c.<>9__36_3;
                    func3 = <>c.<>9__36_3 = g => g.Key.GetCustomAttributes(typeof(AttributeUsageAttribute), true).Cast<AttributeUsageAttribute>().Single<AttributeUsageAttribute>().AllowMultiple;
                }
                var source = attributesProvider.GetAttributes(propertyName).SelectMany(selector).GroupBy(keySelector).GroupBy(func3).ToList();
                var predicate = <>c.<>9__36_4;
                if (<>c.<>9__36_4 == null)
                {
                    var local4 = <>c.<>9__36_4;
                    predicate = <>c.<>9__36_4 = x => x.Key;
                }
                var func5 = <>c.<>9__36_5;
                if (<>c.<>9__36_5 == null)
                {
                    var local5 = <>c.<>9__36_5;
                    func5 = <>c.<>9__36_5 = x => x;
                }
                var func6 = <>c.<>9__36_6;
                if (<>c.<>9__36_6 == null)
                {
                    var local6 = <>c.<>9__36_6;
                    func6 = <>c.<>9__36_6 = x => x;
                }
                var func7 = <>c.<>9__36_7;
                if (<>c.<>9__36_7 == null)
                {
                    var local7 = <>c.<>9__36_7;
                    func7 = <>c.<>9__36_7 = x => x.value;
                }
                IEnumerable<Attribute> first = source.Where(predicate).SelectMany(func5).SelectMany(func6).Select(func7);
                var func8 = <>c.<>9__36_8;
                if (<>c.<>9__36_8 == null)
                {
                    var local8 = <>c.<>9__36_8;
                    func8 = <>c.<>9__36_8 = x => !x.Key;
                }
                var func9 = <>c.<>9__36_9;
                if (<>c.<>9__36_9 == null)
                {
                    var local9 = <>c.<>9__36_9;
                    func9 = <>c.<>9__36_9 = x => x;
                }
                var func10 = <>c.<>9__36_10;
                if (<>c.<>9__36_10 == null)
                {
                    var local10 = <>c.<>9__36_10;
                    func10 = <>c.<>9__36_10 = x => x.Last();
                }
                var func11 = <>c.<>9__36_11;
                if (<>c.<>9__36_11 == null)
                {
                    var local11 = <>c.<>9__36_11;
                    func11 = <>c.<>9__36_11 = x => x.value;
                }
                Func<Attribute, AttributeReference> func12 = <>c.<>9__36_12;
                if (<>c.<>9__36_12 == null)
                {
                    Func<Attribute, AttributeReference> local12 = <>c.<>9__36_12;
                    func12 = <>c.<>9__36_12 = x => new AttributeReference(x);
                }
                Func<AttributeReference, AttributeReference> func13 = <>c.<>9__36_13;
                if (<>c.<>9__36_13 == null)
                {
                    Func<AttributeReference, AttributeReference> local13 = <>c.<>9__36_13;
                    func13 = <>c.<>9__36_13 = x => x;
                }
                Func<IGrouping<AttributeReference, AttributeReference>, Attribute> func14 = <>c.<>9__36_14;
                if (<>c.<>9__36_14 == null)
                {
                    Func<IGrouping<AttributeReference, AttributeReference>, Attribute> local14 = <>c.<>9__36_14;
                    func14 = <>c.<>9__36_14 = x => x.Key.Attribute;
                }
                return first.Concat<Attribute>(source.Where(func8).SelectMany(func9).Select(func10).Select(func11)).Select<Attribute, AttributeReference>(func12).GroupBy<AttributeReference, AttributeReference>(func13).Select<IGrouping<AttributeReference, AttributeReference>, Attribute>(func14).ToList<Attribute>();
            }
        }

        public static IEnumerable<Attribute> GetExternalAndFluentAPIAttributes(Type componentType, string propertyName)
        {
            Func<Type, IAttributesProvider> valueFactory = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Func<Type, IAttributesProvider> local1 = <>c.<>9__34_0;
                valueFactory = <>c.<>9__34_0 = x => GetExternalAndFluentAPIAttributesCore(x);
            }
            return GetExternalAndFluentAPIAttributes(Providers.GetOrAdd(componentType, valueFactory), propertyName);
        }

        private static IAttributesProvider GetExternalAndFluentAPIAttributesCore(Type componentType) => 
            GetExternalAndFluentAPIAttributesCore(componentType, false);

        private static IAttributesProvider GetExternalAndFluentAPIAttributesCore(Type componentType, bool forFiltering)
        {
            Func<Type, IEnumerable<Type>> getItems = <>c.<>9__39_0;
            if (<>c.<>9__39_0 == null)
            {
                Func<Type, IEnumerable<Type>> local1 = <>c.<>9__39_0;
                getItems = <>c.<>9__39_0 = x => x.BaseType.YieldIfNotNull<Type>();
            }
            IEnumerable<Type> enumerable = componentType.Yield<Type>().Flatten<Type>(getItems).Reverse<Type>();
            IEnumerable<IAttributesProvider> result = new IAttributesProvider[0];
            foreach (Type type in enumerable)
            {
                Action<IAttributesProvider> <>9__3;
                IEnumerable<Type> first = (forFiltering ? GetFilteringMetadataClassType(type) : GetMetadataClassType(type)).Return<Type, IEnumerable<Type>>(<>c.<>9__39_1 ??= ((Func<Type, IEnumerable<Type>>) (x => new Type[] { x })), <>c.<>9__39_2 ??= () => Enumerable.Empty<Type>());
                result = result.Concat<IAttributesProvider>(first.Concat<Type>(GetMetadataTypes(MetadataLocator.Default, type)).GetProviders(type, forFiltering));
                IAttributesProvider provider1 = forFiltering ? GetFluentAPIFilteringAttributesFromStaticMethod(type, type) : GetFluentAPIAttributesFromStaticMethod(type, type);
                if (<>9__3 == null)
                {
                    IAttributesProvider local4 = forFiltering ? GetFluentAPIFilteringAttributesFromStaticMethod(type, type) : GetFluentAPIAttributesFromStaticMethod(type, type);
                    Action<IAttributesProvider> action1 = <>9__3 = delegate (IAttributesProvider x) {
                        IAttributesProvider[] second = new IAttributesProvider[] { x };
                        result = result.Concat<IAttributesProvider>(second);
                    };
                    provider1 = (IAttributesProvider) action1;
                }
                ((IAttributesProvider) <>9__3).Do<IAttributesProvider>((Action<IAttributesProvider>) provider1);
            }
            return CompositeMetadataAttributesProvider.Create(result);
        }

        [Obsolete("Use the GetExternalAndFluentAPIFilteringAttributes method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static IEnumerable<Attribute> GetExternalAndFluentAPIFilteringAttrbutes(Type componentType, string propertyName) => 
            GetExternalAndFluentAPIFilteringAttributes(componentType, propertyName);

        public static IEnumerable<Attribute> GetExternalAndFluentAPIFilteringAttributes(Type componentType, string propertyName)
        {
            Func<Type, IAttributesProvider> valueFactory = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<Type, IAttributesProvider> local1 = <>c.<>9__35_0;
                valueFactory = <>c.<>9__35_0 = x => GetExternalAndFluentAPIAttributesCore(x);
            }
            IAttributesProvider orAdd = Providers.GetOrAdd(componentType, valueFactory);
            Func<Type, IAttributesProvider> func2 = <>c.<>9__35_1;
            if (<>c.<>9__35_1 == null)
            {
                Func<Type, IAttributesProvider> local2 = <>c.<>9__35_1;
                func2 = <>c.<>9__35_1 = x => GetExternalAndFluentAPIFilteringAttributesCore(x);
            }
            IAttributesProvider item = FilteringProviders.GetOrAdd(componentType, func2);
            List<IAttributesProvider> providers = new List<IAttributesProvider>();
            providers.Add(orAdd);
            providers.Add(item);
            return GetExternalAndFluentAPIAttributes(CompositeMetadataAttributesProvider.Create(providers), propertyName);
        }

        private static IAttributesProvider GetExternalAndFluentAPIFilteringAttributesCore(Type componentType)
        {
            List<IAttributesProvider> providers = new List<IAttributesProvider>();
            providers.Add(GetExternalAndFluentAPIAttributesCore(componentType, false));
            providers.Add(GetExternalAndFluentAPIAttributesCore(componentType, true));
            return CompositeMetadataAttributesProvider.Create(providers);
        }

        private static IAttributesProvider GetExternalMetadataAttributes(Type metadataClassType, Type componentType) => 
            metadataClassType.GetMembers(BindingFlags.Public | BindingFlags.Instance).Any<MemberInfo>() ? new ExternalMetadataAttributesProvider(metadataClassType, componentType) : null;

        [Obsolete("Use the GetFilteringAttributesProvider method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public static IAttributesProvider GetFilteringAttrbutesProvider(Type componentType, IMetadataLocator locator) => 
            GetFilteringAttributesProvider(componentType, locator);

        public static IAttributesProvider GetFilteringAttributesProvider(Type componentType, IMetadataLocator locator) => 
            CompositeMetadataAttributesProvider.Create(GetMetadataTypes(locator, componentType).GetProviders(componentType, true));

        private static Type GetFilteringMetadataClassType(Type componentType) => 
            GetFilteringMetadataClassTypeCore(componentType);

        private static Type GetFilteringMetadataClassTypeCore(Type componentType)
        {
            if (componentType.IsEnum)
            {
                return null;
            }
            object[] customAttributes = componentType.GetCustomAttributes(false);
            if ((customAttributes == null) || !customAttributes.Any<object>())
            {
                return null;
            }
            Func<object, bool> predicate = <>c.<>9__52_0;
            if (<>c.<>9__52_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__52_0;
                predicate = <>c.<>9__52_0 = delegate (object x) {
                    Type type = x.GetType();
                    return (type.Name == "FilterMetadataTypeAttribute") && (type.Namespace == "DevExpress.Utils.Filtering");
                };
            }
            object obj2 = customAttributes.SingleOrDefault<object>(predicate);
            return ((obj2 != null) ? ((Type) obj2.GetType().GetProperty("MetadataClassType", BindingFlags.Public | BindingFlags.Instance).GetValue(obj2, null)) : null);
        }

        private static IAttributesProvider GetFluentAPIAttributes(Type metadataClassType, Type componentType) => 
            GetFluentAPIAttributesCore(metadataClassType, componentType, false);

        public static IEnumerable<Attribute> GetFluentAPIAttributes(Type metadataClassType, Type componentType, string propertyName) => 
            GetFluentAPIAttributes(metadataClassType, componentType).With<IAttributesProvider, IEnumerable<Attribute>>(x => x.GetAttributes(propertyName));

        private static IAttributesProvider GetFluentAPIAttributesCore(Type metadataClassType, Type componentType, bool forFiltering)
        {
            MethodInfo method = GetBuildMetadataMethodsFromMatadataProvider(metadataClassType, componentType, forFiltering ? new Func<Type, Type, bool>(MetadataHelper.IsFilteringMetadataProviderType) : new Func<Type, Type, bool>(MetadataHelper.IsMetadataProviderType)).SingleOrDefault<MethodInfo>();
            return ((method != null) ? InvokeBuildMetadataMethodFromMetadataProvider(method, metadataClassType, componentType, forFiltering) : null);
        }

        private static IAttributesProvider GetFluentAPIAttributesFromStaticMethod(Type metadataClassType, Type componentType) => 
            GetFluentAPIAttributesFromStaticMethodCore(metadataClassType, componentType, false);

        private static IAttributesProvider GetFluentAPIAttributesFromStaticMethodCore(Type metadataClassType, Type componentType, bool forFiltering)
        {
            MethodInfo method = GetBuildMetadataStaticMethods(metadataClassType, componentType, forFiltering ? new Func<Type, Type, bool>(MetadataHelper.IsFilteringMetadataBuilderType) : new Func<Type, Type, bool>(MetadataHelper.IsMetadataBuilderType)).SingleOrDefault<MethodInfo>();
            return ((method != null) ? InvokeBuildMetadataStaticMethod(method, componentType, forFiltering) : null);
        }

        private static IAttributesProvider GetFluentAPIFilteringAttributes(Type metadataClassType, Type componentType) => 
            GetFluentAPIAttributesCore(metadataClassType, componentType, true);

        public static IEnumerable<Attribute> GetFluentAPIFilteringAttributes(Type metadataClassType, Type componentType, string propertyName)
        {
            IAttributesProvider fluentAPIAttributes = GetFluentAPIAttributes(metadataClassType, componentType);
            IAttributesProvider fluentAPIFilteringAttributes = GetFluentAPIFilteringAttributes(metadataClassType, componentType);
            List<IAttributesProvider> providers = new List<IAttributesProvider>();
            providers.Add(fluentAPIAttributes);
            providers.Add(fluentAPIFilteringAttributes);
            return CompositeMetadataAttributesProvider.Create(providers).GetAttributes(propertyName);
        }

        private static IAttributesProvider GetFluentAPIFilteringAttributesFromStaticMethod(Type metadataClassType, Type componentType) => 
            GetFluentAPIAttributesFromStaticMethodCore(metadataClassType, componentType, true);

        private static Type GetMetadataClassType(Type componentType) => 
            GetMetadataClassTypeCore(componentType);

        private static Type GetMetadataClassTypeCore(Type componentType)
        {
            Type attributeType = componentType.IsEnum ? typeof(EnumMetadataTypeAttribute) : typeof(MetadataTypeAttribute);
            object[] customAttributes = componentType.GetCustomAttributes(attributeType, false);
            return (((customAttributes == null) || !customAttributes.Any<object>()) ? null : ((Type) customAttributes[0].GetType().GetProperty("MetadataClassType", BindingFlags.Public | BindingFlags.Instance).GetValue(customAttributes[0], null)));
        }

        public static IEnumerable<Tuple<Type, Type>> GetMetadataInfoList(Type metadataType)
        {
            Func<Type, Type, bool> func1 = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<Type, Type, bool> local1 = <>c.<>9__7_0;
                func1 = <>c.<>9__7_0 = (t1, t2) => IsFilteringMetadataProviderType(t1, t2) || IsMetadataProviderType(t1, t2);
            }
            Func<Type, Type, bool> isMetadataProviderType = func1;
            Func<Type, Type, bool> func2 = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<Type, Type, bool> local2 = <>c.<>9__7_1;
                func2 = <>c.<>9__7_1 = (t1, t2) => IsFilteringMetadataBuilderType(t1, t2) || IsMetadataBuilderType(t1, t2);
            }
            Func<Type, Type, bool> isMetadataBuilderType = func2;
            return (from x in metadataType.GetInterfaces()
                where isMetadataProviderType(x, null)
                select new Tuple<Type, Type>(GetTypeOrGenericTypeDefinition(x.GetGenericArguments().Single<Type>()), metadataType)).Concat<Tuple<Type, Type>>((from x in GetBuildMetadataStaticMethods(metadataType, null, isMetadataBuilderType) select new Tuple<Type, Type>(GetTypeOrGenericTypeDefinition(x.GetParameters().Single<ParameterInfo>().ParameterType.GetGenericArguments().Single<Type>()), metadataType)));
        }

        private static IEnumerable<Type> GetMetadataTypes(IMetadataLocator locator, Type type) => 
            ((IMetadataLocator) (locator ?? MetadataLocator.Create())).GetMetadataTypes(GetTypeOrGenericTypeDefinition(type)).Return<Type[], IEnumerable<Type>>(<>c.<>9__66_0 ??= ((Func<Type[], IEnumerable<Type>>) (x => x)), <>c.<>9__66_1 ??= () => Enumerable.Empty<Type>());

        private static IEnumerable<IAttributesProvider> GetProviders(this IEnumerable<Type> metadataTypes, Type type, bool forFiltering) => 
            from x in metadataTypes select forFiltering ? GetAllFilteringMetadataAttributes(x, type) : GetAllMetadataAttributes(x, type);

        private static Type GetTypeOrGenericTypeDefinition(Type componentType) => 
            componentType.IsGenericType ? componentType.GetGenericTypeDefinition() : componentType;

        private static IAttributesProvider InvokeBuildMetadataMethodFromMetadataProvider(MethodInfo method, Type metadataClassType, Type componentType, bool forFiltering)
        {
            IAttributesProvider provider = forFiltering ? CreateFilteringMetadataBuilder(componentType) : CreateMetadataBuilder(componentType);
            IAttributesProvider[] parameters = new IAttributesProvider[] { provider };
            method.Invoke(Activator.CreateInstance(metadataClassType), parameters);
            return provider;
        }

        private static IAttributesProvider InvokeBuildMetadataStaticMethod(MethodInfo method, Type componentType, bool forFiltering)
        {
            IAttributesProvider provider = forFiltering ? CreateFilteringMetadataBuilder(componentType) : CreateMetadataBuilder(componentType);
            IAttributesProvider[] parameters = new IAttributesProvider[] { provider };
            method.Invoke(null, parameters);
            return provider;
        }

        private static bool IsFilteringMetadataBuilderType(Type type, Type componentType) => 
            IsFilteringMetadataBuilderTypeCore(type, componentType, typeof(FilteringMetadataBuilder<>));

        private static bool IsFilteringMetadataBuilderTypeCore(Type type, Type componentType, Type filteringMetadataBuilderOrProviderType)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            Type type2 = type.GetGenericArguments().Single<Type>();
            return (((componentType == null) || !(componentType != type2)) ? (((componentType == null) || !componentType.IsEnum) ? (type.GetGenericTypeDefinition() == filteringMetadataBuilderOrProviderType) : false) : false);
        }

        private static bool IsFilteringMetadataProviderType(Type type, Type componentType) => 
            IsFilteringMetadataBuilderTypeCore(type, componentType, typeof(IFilteringMetadataProvider<>));

        private static bool IsMetadataBuilderType(Type type, Type componentType) => 
            IsMetadataBuilderTypeCore(type, componentType, typeof(MetadataBuilder<>), typeof(EnumMetadataBuilder<>));

        private static bool IsMetadataBuilderTypeCore(Type type, Type componentType, Type metadataBuilderOrProviderType, Type enumMetadataBuilderOrProviderType)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            Type type2 = type.GetGenericArguments().Single<Type>();
            if ((componentType != null) && (componentType != type2))
            {
                return false;
            }
            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            return (!(genericTypeDefinition == metadataBuilderOrProviderType) ? ((genericTypeDefinition == enumMetadataBuilderOrProviderType) && ((componentType == null) || componentType.IsEnum)) : ((componentType == null) || !componentType.IsEnum));
        }

        private static bool IsMetadataProviderType(Type type, Type componentType) => 
            IsMetadataBuilderTypeCore(type, componentType, typeof(IMetadataProvider<>), typeof(IEnumMetadataProvider<>));

        internal static void RegisterLocator(MetadataLocator locator)
        {
            List<WeakReference> locators = MetadataHelper.locators;
            lock (locators)
            {
                MetadataHelper.locators.Add(new WeakReference(locator));
            }
        }

        private static void UpdateLocators()
        {
            List<WeakReference> locators = MetadataHelper.locators;
            lock (locators)
            {
                List<WeakReference> list2 = MetadataHelper.locators.ToList<WeakReference>();
                int count = list2.Count;
                while (--count >= 0)
                {
                    MetadataLocator target = (MetadataLocator) list2[count].Target;
                    if (target == null)
                    {
                        MetadataHelper.locators.RemoveAt(count);
                        continue;
                    }
                    target.Update();
                }
            }
        }

        internal static IEnumerable<Tuple<Type, Type>> InternalMetadataProviders =>
            new ThreadSafeEnumerable<Tuple<Type, Type>>(internalMetadataProviders);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MetadataHelper.<>c <>9 = new MetadataHelper.<>c();
            public static Func<Type, Type, bool> <>9__7_0;
            public static Func<Type, Type, bool> <>9__7_1;
            public static Func<Type, IAttributesProvider> <>9__34_0;
            public static Func<Type, IAttributesProvider> <>9__35_0;
            public static Func<Type, IAttributesProvider> <>9__35_1;
            public static Func<Attribute, IEnumerable<<>f__AnonymousType0<Type, Attribute>>> <>9__36_0;
            public static Func<<>f__AnonymousType0<Type, Attribute>, Type> <>9__36_2;
            public static Func<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>, bool> <>9__36_3;
            public static Func<IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>, bool> <>9__36_4;
            public static Func<IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>, IEnumerable<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>> <>9__36_5;
            public static Func<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>, IEnumerable<<>f__AnonymousType0<Type, Attribute>>> <>9__36_6;
            public static Func<<>f__AnonymousType0<Type, Attribute>, Attribute> <>9__36_7;
            public static Func<IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>, bool> <>9__36_8;
            public static Func<IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>, IEnumerable<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>>> <>9__36_9;
            public static Func<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>, <>f__AnonymousType0<Type, Attribute>> <>9__36_10;
            public static Func<<>f__AnonymousType0<Type, Attribute>, Attribute> <>9__36_11;
            public static Func<Attribute, MetadataHelper.AttributeReference> <>9__36_12;
            public static Func<MetadataHelper.AttributeReference, MetadataHelper.AttributeReference> <>9__36_13;
            public static Func<IGrouping<MetadataHelper.AttributeReference, MetadataHelper.AttributeReference>, Attribute> <>9__36_14;
            public static Func<Type, IEnumerable<Type>> <>9__39_0;
            public static Func<Type, IEnumerable<Type>> <>9__39_1;
            public static Func<IEnumerable<Type>> <>9__39_2;
            public static Func<object, bool> <>9__52_0;
            public static Func<Type, MethodInfo> <>9__54_1;
            public static Func<Type[], IEnumerable<Type>> <>9__66_0;
            public static Func<IEnumerable<Type>> <>9__66_1;
            public static Func<Type, Type> <>9__68_0;
            public static Func<Type, bool> <>9__68_1;

            internal Type <GetAttributeTypes>b__68_0(Type x) => 
                x.BaseType;

            internal bool <GetAttributeTypes>b__68_1(Type x) => 
                x == typeof(Attribute);

            internal MethodInfo <GetBuildMetadataMethodsFromMatadataProvider>b__54_1(Type x) => 
                x.GetMethod("BuildMetadata", BindingFlags.Public | BindingFlags.Instance);

            internal IAttributesProvider <GetExternalAndFluentAPIAttributes>b__34_0(Type x) => 
                MetadataHelper.GetExternalAndFluentAPIAttributesCore(x);

            internal IEnumerable<<>f__AnonymousType0<Type, Attribute>> <GetExternalAndFluentAPIAttributes>b__36_0(Attribute attr) => 
                from x in MetadataHelper.GetAttributeTypes(attr) select new { 
                    type = x,
                    value = attr
                };

            internal <>f__AnonymousType0<Type, Attribute> <GetExternalAndFluentAPIAttributes>b__36_10(IGrouping<Type, <>f__AnonymousType0<Type, Attribute>> x) => 
                x.Last();

            internal Attribute <GetExternalAndFluentAPIAttributes>b__36_11(<>f__AnonymousType0<Type, Attribute> x) => 
                x.value;

            internal MetadataHelper.AttributeReference <GetExternalAndFluentAPIAttributes>b__36_12(Attribute x) => 
                new MetadataHelper.AttributeReference(x);

            internal MetadataHelper.AttributeReference <GetExternalAndFluentAPIAttributes>b__36_13(MetadataHelper.AttributeReference x) => 
                x;

            internal Attribute <GetExternalAndFluentAPIAttributes>b__36_14(IGrouping<MetadataHelper.AttributeReference, MetadataHelper.AttributeReference> x) => 
                x.Key.Attribute;

            internal Type <GetExternalAndFluentAPIAttributes>b__36_2(<>f__AnonymousType0<Type, Attribute> x) => 
                x.type;

            internal bool <GetExternalAndFluentAPIAttributes>b__36_3(IGrouping<Type, <>f__AnonymousType0<Type, Attribute>> g) => 
                g.Key.GetCustomAttributes(typeof(AttributeUsageAttribute), true).Cast<AttributeUsageAttribute>().Single<AttributeUsageAttribute>().AllowMultiple;

            internal bool <GetExternalAndFluentAPIAttributes>b__36_4(IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> x) => 
                x.Key;

            internal IEnumerable<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> <GetExternalAndFluentAPIAttributes>b__36_5(IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> x) => 
                x;

            internal IEnumerable<<>f__AnonymousType0<Type, Attribute>> <GetExternalAndFluentAPIAttributes>b__36_6(IGrouping<Type, <>f__AnonymousType0<Type, Attribute>> x) => 
                x;

            internal Attribute <GetExternalAndFluentAPIAttributes>b__36_7(<>f__AnonymousType0<Type, Attribute> x) => 
                x.value;

            internal bool <GetExternalAndFluentAPIAttributes>b__36_8(IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> x) => 
                !x.Key;

            internal IEnumerable<IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> <GetExternalAndFluentAPIAttributes>b__36_9(IGrouping<bool, IGrouping<Type, <>f__AnonymousType0<Type, Attribute>>> x) => 
                x;

            internal IEnumerable<Type> <GetExternalAndFluentAPIAttributesCore>b__39_0(Type x) => 
                x.BaseType.YieldIfNotNull<Type>();

            internal IEnumerable<Type> <GetExternalAndFluentAPIAttributesCore>b__39_1(Type x) => 
                new Type[] { x };

            internal IEnumerable<Type> <GetExternalAndFluentAPIAttributesCore>b__39_2() => 
                Enumerable.Empty<Type>();

            internal IAttributesProvider <GetExternalAndFluentAPIFilteringAttributes>b__35_0(Type x) => 
                MetadataHelper.GetExternalAndFluentAPIAttributesCore(x);

            internal IAttributesProvider <GetExternalAndFluentAPIFilteringAttributes>b__35_1(Type x) => 
                MetadataHelper.GetExternalAndFluentAPIFilteringAttributesCore(x);

            internal bool <GetFilteringMetadataClassTypeCore>b__52_0(object x)
            {
                Type type = x.GetType();
                return ((type.Name == "FilterMetadataTypeAttribute") && (type.Namespace == "DevExpress.Utils.Filtering"));
            }

            internal bool <GetMetadataInfoList>b__7_0(Type t1, Type t2) => 
                MetadataHelper.IsFilteringMetadataProviderType(t1, t2) || MetadataHelper.IsMetadataProviderType(t1, t2);

            internal bool <GetMetadataInfoList>b__7_1(Type t1, Type t2) => 
                MetadataHelper.IsFilteringMetadataBuilderType(t1, t2) || MetadataHelper.IsMetadataBuilderType(t1, t2);

            internal IEnumerable<Type> <GetMetadataTypes>b__66_0(Type[] x) => 
                x;

            internal IEnumerable<Type> <GetMetadataTypes>b__66_1() => 
                Enumerable.Empty<Type>();
        }



        private class AttributeReference
        {
            public readonly System.Attribute Attribute;

            public AttributeReference(System.Attribute attribute)
            {
                this.Attribute = attribute;
            }

            public override bool Equals(object obj) => 
                this == (obj as MetadataHelper.AttributeReference);

            public override int GetHashCode() => 
                this.Attribute.GetHashCode();

            public static bool operator ==(MetadataHelper.AttributeReference a, MetadataHelper.AttributeReference b)
            {
                bool flag = ReferenceEquals(a, null);
                bool flag2 = ReferenceEquals(b, null);
                return (!(flag & flag2) ? (!(flag | flag2) ? ReferenceEquals(a.Attribute, b.Attribute) : false) : true);
            }

            public static bool operator !=(MetadataHelper.AttributeReference a, MetadataHelper.AttributeReference b) => 
                !(a == b);
        }

        private class CompositeMetadataAttributesProvider : IAttributesProvider
        {
            private readonly IAttributesProvider[] providers;

            private CompositeMetadataAttributesProvider(IAttributesProvider[] providers)
            {
                this.providers = providers;
            }

            public static IAttributesProvider Create(IEnumerable<IAttributesProvider> providers)
            {
                Func<IAttributesProvider, bool> predicate = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<IAttributesProvider, bool> local1 = <>c.<>9__0_0;
                    predicate = <>c.<>9__0_0 = x => x != null;
                }
                return new MetadataHelper.CompositeMetadataAttributesProvider(providers.Where<IAttributesProvider>(predicate).ToArray<IAttributesProvider>());
            }

            IEnumerable<Attribute> IAttributesProvider.GetAttributes(string propertyName)
            {
                Func<IEnumerable<Attribute>, bool> predicate = <>c.<>9__3_1;
                if (<>c.<>9__3_1 == null)
                {
                    Func<IEnumerable<Attribute>, bool> local1 = <>c.<>9__3_1;
                    predicate = <>c.<>9__3_1 = x => x != null;
                }
                Func<IEnumerable<Attribute>, IEnumerable<Attribute>> selector = <>c.<>9__3_2;
                if (<>c.<>9__3_2 == null)
                {
                    Func<IEnumerable<Attribute>, IEnumerable<Attribute>> local2 = <>c.<>9__3_2;
                    selector = <>c.<>9__3_2 = x => x;
                }
                return (from x in this.providers select x.GetAttributes(propertyName)).Where<IEnumerable<Attribute>>(predicate).SelectMany<IEnumerable<Attribute>, Attribute>(selector);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly MetadataHelper.CompositeMetadataAttributesProvider.<>c <>9 = new MetadataHelper.CompositeMetadataAttributesProvider.<>c();
                public static Func<IAttributesProvider, bool> <>9__0_0;
                public static Func<IEnumerable<Attribute>, bool> <>9__3_1;
                public static Func<IEnumerable<Attribute>, IEnumerable<Attribute>> <>9__3_2;

                internal bool <Create>b__0_0(IAttributesProvider x) => 
                    x != null;

                internal bool <DevExpress.Mvvm.Native.IAttributesProvider.GetAttributes>b__3_1(IEnumerable<Attribute> x) => 
                    x != null;

                internal IEnumerable<Attribute> <DevExpress.Mvvm.Native.IAttributesProvider.GetAttributes>b__3_2(IEnumerable<Attribute> x) => 
                    x;
            }
        }

        private class ExternalMetadataAttributesProvider : IAttributesProvider
        {
            private readonly Type metadataClassType;
            private readonly Type componentType;

            public ExternalMetadataAttributesProvider(Type metadataClassType, Type componentType)
            {
                this.metadataClassType = metadataClassType;
                this.componentType = componentType;
            }

            IEnumerable<Attribute> IAttributesProvider.GetAttributes(string propertyName)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    return Enumerable.Empty<Attribute>();
                }
                MemberInfo info = this.metadataClassType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance) ?? this.metadataClassType.GetMethod(propertyName, BindingFlags.Public | BindingFlags.Instance);
                return ((info == null) ? ((IEnumerable<Attribute>) new Attribute[0]) : info.GetCustomAttributes(true).OfType<Attribute>());
            }
        }

        private class ThreadSafeEnumerable<T> : IEnumerable<T>, IEnumerable
        {
            private readonly IEnumerable<T> list;

            public ThreadSafeEnumerable(IEnumerable<T> list)
            {
                this.list = list;
            }

            public IEnumerator<T> GetEnumerator()
            {
                List<T> list;
                IEnumerable<T> enumerable = this.list;
                lock (enumerable)
                {
                    list = this.list.ToList<T>();
                }
                return list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => 
                this.GetEnumerator();
        }
    }
}

