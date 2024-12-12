namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Navigation;

    public static class UriQualifierHelper
    {
        private static readonly object ResourceContainer;
        private static readonly GetResourceManagerWrapper getResourceManagerWrapper;
        private static volatile Func<object, ResourceManager> getResourceManager;
        private static readonly object olock = new object();
        private static readonly Func<Uri, Package> getResourcePackage;
        public static readonly Dictionary<string, IBaseUriQualifier> registeredQualifiers = new Dictionary<string, IBaseUriQualifier>();
        private static readonly Dictionary<object, UriInfoMap> resourceSetDatas = new Dictionary<object, UriInfoMap>();
        private static Func<object, object> get_DesignerContext;
        private static Func<object, object> get_DocumentViewContext;
        private static Func<object, object> get_ActiveView;
        private static Func<object, object> get_DocumentPath;
        [IgnoreDependencyPropertiesConsistencyChecker]
        internal static readonly DependencyProperty QualifierInfoProperty;

        static UriQualifierHelper()
        {
            int? parametersCount = null;
            getResourcePackage = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateInstanceMethodHandler<Func<Uri, Package>>(null, "GetResourcePackage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, typeof(Application), parametersCount, null, true);
            ResourceContainer = getResourcePackage(new Uri("application://"));
            parametersCount = null;
            getResourceManagerWrapper = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateInstanceMethodHandler<GetResourceManagerWrapper>(ResourceContainer, "GetResourceManagerWrapper", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, ResourceContainer.GetType(), parametersCount, null, true);
            QualifierInfoProperty = DependencyPropertyManager.RegisterAttached("QualifierInfo", typeof(object), typeof(UriQualifierHelper), new FrameworkPropertyMetadata(null));
            RegisterDefaultQualifiers();
        }

        private static void CheckInitializeGetResourceManager(object wrapper)
        {
            if (getResourceManager == null)
            {
                object olock = UriQualifierHelper.olock;
                lock (olock)
                {
                    if (getResourceManager == null)
                    {
                        int? parametersCount = null;
                        getResourceManager = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateInstanceMethodHandler<Func<object, ResourceManager>>(wrapper, "get_ResourceManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, wrapper.GetType(), parametersCount, null, true);
                    }
                }
            }
        }

        public static bool CheckServiceProvider(IServiceProvider serviceProvider, out bool returnSelf, out bool returnBinding, out bool returnBindingExpression)
        {
            returnBinding = false;
            returnBindingExpression = false;
            returnSelf = false;
            if (serviceProvider == null)
            {
                return false;
            }
            IProvideValueTarget service = (IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget));
            if (service != null)
            {
                if (service.TargetObject is ImageSelectorExtension)
                {
                    returnSelf = true;
                    return true;
                }
                if (service.TargetObject is Setter)
                {
                    returnBinding = true;
                    return true;
                }
                if (!(service.TargetProperty is DependencyProperty))
                {
                    return false;
                }
                returnBindingExpression = true;
            }
            return true;
        }

        private static bool CreatePublicGetter(object instance, string getterName, out Func<object, object> result)
        {
            int? parametersCount = null;
            result = DevExpress.Xpf.Core.Internal.ReflectionHelper.CreateInstanceMethodHandler<Func<object, object>>(instance, getterName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, instance.GetType(), parametersCount, null, true);
            return (result != null);
        }

        private static Uri GetBaseUri(IServiceProvider serviceProvider)
        {
            IUriContext service = serviceProvider.GetService(typeof(IUriContext)) as IUriContext;
            if (service != null)
            {
                return service.BaseUri;
            }
            DependencyObject target = GetTarget(serviceProvider);
            return ((target != null) ? ValidateBaseUriInDesignMode(target, BaseUriHelper.GetBaseUri(target)) : null);
        }

        private static object GetDesignTimeHook(IServiceProvider serviceProvider, Uri relativeUri, int ttl)
        {
            DependencyObject dObj = GetTarget(serviceProvider);
            if ((dObj != null) && DesignerProperties.GetIsInDesignMode(dObj))
            {
                dObj.Dispatcher.BeginInvoke(delegate {
                    DependencyProperty targetProperty = ((IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget))).TargetProperty as DependencyProperty;
                    if (targetProperty != null)
                    {
                        dObj.SetValue(targetProperty, ProvideValueOrExpression(null, serviceProvider, relativeUri, ttl - 1));
                    }
                }, new object[0]);
            }
            return DependencyProperty.UnsetValue;
        }

        private static string GetDocumentPathInDesignMode(DependencyObject node)
        {
            string str;
            try
            {
                Func<DependencyObject, bool> predicate = <>c.<>9__26_0;
                if (<>c.<>9__26_0 == null)
                {
                    Func<DependencyObject, bool> local1 = <>c.<>9__26_0;
                    predicate = <>c.<>9__26_0 = x => Convert.ToString(x).Contains("SceneScrollViewer");
                }
                object instance = TreeHelper.GetParent(node, predicate, true, true);
                if (instance == null)
                {
                    str = null;
                }
                else if ((get_DesignerContext == null) && !CreatePublicGetter(instance, "get_DesignerContext", out get_DesignerContext))
                {
                    str = null;
                }
                else
                {
                    instance = get_DesignerContext(instance);
                    if (instance == null)
                    {
                        str = null;
                    }
                    else if ((get_DocumentViewContext == null) && !CreatePublicGetter(instance, "get_DocumentViewContext", out get_DocumentViewContext))
                    {
                        str = null;
                    }
                    else
                    {
                        instance = get_DocumentViewContext(instance);
                        if (instance == null)
                        {
                            str = null;
                        }
                        else if ((get_ActiveView == null) && !CreatePublicGetter(instance, "get_ActiveView", out get_ActiveView))
                        {
                            str = null;
                        }
                        else
                        {
                            instance = get_ActiveView(instance);
                            str = (instance != null) ? (((get_DocumentPath != null) || CreatePublicGetter(instance, "get_DocumentPath", out get_DocumentPath)) ? Convert.ToString(get_DocumentPath(instance)) : null) : null;
                        }
                    }
                }
            }
            catch
            {
                str = null;
            }
            return str;
        }

        internal static object GetQualifierInfo(DependencyObject obj) => 
            obj.GetValue(QualifierInfoProperty);

        private static ResourceManager GetResourceManager(Uri uri)
        {
            string str;
            bool flag;
            object wrapper = getResourceManagerWrapper(ResourceContainer, new Uri(uri.AbsolutePath, UriKind.Relative), out str, out flag);
            CheckInitializeGetResourceManager(wrapper);
            return getResourceManager(wrapper);
        }

        private static DependencyObject GetTarget(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            return ((service != null) ? (service.TargetObject as DependencyObject) : null);
        }

        private static UriInfoMap GetUriMap(Uri baseUri, Uri absoluteUri) => 
            (absoluteUri.Scheme != Uri.UriSchemeFile) ? GetUriMapForPackScheme(baseUri, absoluteUri) : GetUriMapForFileScheme(baseUri, absoluteUri);

        private static UriInfoMap GetUriMapForFileScheme(Uri baseUri, Uri absoluteUri)
        {
            UriInfoMap map;
            if (!resourceSetDatas.TryGetValue(baseUri, out map))
            {
                map = new UriInfoMap(new FileResourceCollection(baseUri, absoluteUri), absoluteUri);
                resourceSetDatas.Add(baseUri, map);
            }
            return map;
        }

        private static UriInfoMap GetUriMapForPackScheme(Uri baseUri, Uri absoluteUri)
        {
            UriInfoMap map;
            ResourceManager resourceManager = GetResourceManager(absoluteUri);
            if (resourceManager == null)
            {
                return null;
            }
            ResourceSet key = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, false, true);
            if (key == null)
            {
                return null;
            }
            if (!resourceSetDatas.TryGetValue(key, out map))
            {
                map = new UriInfoMap(new PackResourceCollection(key), absoluteUri);
                resourceSetDatas.Add(key, map);
            }
            return map;
        }

        public static bool MakeAbsoluteUri(Uri baseUri, Uri relativeUri, out Uri absoluteUri)
        {
            absoluteUri = null;
            if (relativeUri == null)
            {
                return false;
            }
            if (relativeUri.IsAbsoluteUri)
            {
                absoluteUri = relativeUri;
                baseUri = relativeUri;
                return true;
            }
            if (baseUri == null)
            {
                return false;
            }
            absoluteUri = new Uri(baseUri, relativeUri);
            return true;
        }

        public static bool MakeAbsoluteUri(IServiceProvider serviceProvider, Uri relativeUri, out Uri absoluteUri, out Uri baseUri)
        {
            baseUri = null;
            if (serviceProvider != null)
            {
                baseUri = GetBaseUri(serviceProvider);
            }
            return MakeAbsoluteUri(baseUri, relativeUri, out absoluteUri);
        }

        private static object ProvideBindingValue(IServiceProvider serviceProvider, Uri uri, UriInfoMap data, bool provideExpression)
        {
            Func<DevExpress.Xpf.Core.Native.UriInfo, bool> predicate = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                Func<DevExpress.Xpf.Core.Native.UriInfo, bool> local1 = <>c.<>9__28_0;
                predicate = <>c.<>9__28_0 = x => x.BindableQualifier;
            }
            BindingBase input = data.GetValues(uri).All<DevExpress.Xpf.Core.Native.UriInfo>(predicate) ? ProvideSimpleBinding(serviceProvider, uri, () => data.GetValues(uri)) : ProvideComplexBinding(serviceProvider, uri, () => data.GetValues(uri));
            return (!provideExpression ? input : input.With<BindingBase, object>(x => x.ProvideValue(serviceProvider)));
        }

        private static BindingBase ProvideComplexBinding(IServiceProvider serviceProvider, Uri uri, Func<ICollection<DevExpress.Xpf.Core.Native.UriInfo>> uriCandidates) => 
            QualifierListener.CreateBinding(serviceProvider, uri, uriCandidates);

        private static BindingBase ProvideSimpleBinding(IServiceProvider serviceProvider, Uri uri, Func<ICollection<DevExpress.Xpf.Core.Native.UriInfo>> uriCandidates)
        {
            Func<DevExpress.Xpf.Core.Native.UriInfo, IEnumerable<UriQualifierValue>> selector = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<DevExpress.Xpf.Core.Native.UriInfo, IEnumerable<UriQualifierValue>> local1 = <>c.<>9__30_0;
                selector = <>c.<>9__30_0 = x => x.Qualifiers;
            }
            Func<UriQualifierValue, IBaseUriQualifier> func2 = <>c.<>9__30_1;
            if (<>c.<>9__30_1 == null)
            {
                Func<UriQualifierValue, IBaseUriQualifier> local2 = <>c.<>9__30_1;
                func2 = <>c.<>9__30_1 = x => x.Qualifier;
            }
            IBindableUriQualifier[] source = uriCandidates().SelectMany<DevExpress.Xpf.Core.Native.UriInfo, UriQualifierValue>(selector).Select<UriQualifierValue, IBaseUriQualifier>(func2).OfType<IBindableUriQualifier>().Distinct<IBindableUriQualifier>().ToArray<IBindableUriQualifier>();
            if (source.Count<IBindableUriQualifier>() > 1)
            {
                return ProvideComplexBinding(serviceProvider, uri, uriCandidates);
            }
            if (source.Count<IBindableUriQualifier>() == 0)
            {
                Binding binding1 = new Binding();
                binding1.Converter = new SingleUriQualifierConverter(uri);
                return binding1;
            }
            Binding item = source.SingleOrDefault<IBindableUriQualifier>().GetBinding(RelativeSource.Self);
            if (item == null)
            {
                return null;
            }
            MultiBinding binding2 = new MultiBinding();
            Binding binding3 = new Binding();
            binding3.RelativeSource = RelativeSource.Self;
            binding2.Bindings.Add(binding3);
            binding2.Bindings.Add(item);
            binding2.Converter = new ComplexUriQualifierConverter(uriCandidates, uri);
            return binding2;
        }

        private static object ProvideStaticValue(IServiceProvider serviceProvider, Uri uri)
        {
            IProvideValueTarget service = (IProvideValueTarget) serviceProvider.GetService(typeof(IProvideValueTarget));
            if (service == null)
            {
                return uri;
            }
            DependencyProperty targetProperty = service.TargetProperty as DependencyProperty;
            Setter targetObject = service.TargetObject as Setter;
            if (targetObject != null)
            {
                targetProperty = targetObject.Property;
            }
            Func<DependencyProperty, Type> evaluator = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<DependencyProperty, Type> local1 = <>c.<>9__31_0;
                evaluator = <>c.<>9__31_0 = x => x.PropertyType;
            }
            Type targetType = targetProperty.With<DependencyProperty, Type>(evaluator);
            return UriToTypeHelper.GetObject(uri, targetType);
        }

        public static object ProvideValueOrExpression(IServiceProvider serviceProvider, string uriString) => 
            ProvideValueOrExpression(null, serviceProvider, uriString);

        public static object ProvideValueOrExpression(IServiceProvider serviceProvider, Uri relativeUri) => 
            ProvideValueOrExpression(null, serviceProvider, relativeUri, 10);

        public static object ProvideValueOrExpression(MarkupExtension extension, IServiceProvider serviceProvider, string uriString) => 
            ProvideValueOrExpression(extension, serviceProvider, new Uri(uriString, UriKind.RelativeOrAbsolute));

        public static object ProvideValueOrExpression(MarkupExtension extension, IServiceProvider serviceProvider, Uri relativeUri) => 
            ProvideValueOrExpression(extension, serviceProvider, relativeUri, 10);

        private static object ProvideValueOrExpression(MarkupExtension extension, IServiceProvider serviceProvider, Uri relativeUri, int ttl)
        {
            Uri uri;
            Uri uri2;
            bool flag;
            bool flag2;
            bool flag3;
            if (ttl == 0)
            {
                return DependencyProperty.UnsetValue;
            }
            if (!MakeAbsoluteUri(serviceProvider, relativeUri, out uri, out uri2))
            {
                return GetDesignTimeHook(serviceProvider, relativeUri, ttl);
            }
            UriInfoMap uriMap = GetUriMap(uri2, uri);
            return ((CheckServiceProvider(serviceProvider, out flag, out flag2, out flag3) || ((uriMap == null) || !uriMap.GetValues(uri).Any<DevExpress.Xpf.Core.Native.UriInfo>())) ? (!flag ? ProvideBindingValue(serviceProvider, uri, uriMap, flag3) : extension) : ProvideStaticValue(serviceProvider, uri));
        }

        private static void RegisterDefaultQualifiers()
        {
            RegisterQualifier(new InputQualifier(), true);
            RegisterQualifier(new ThemeQualifier(), true);
            RegisterQualifier(new ContrastQualifier(), true);
            RegisterQualifier(new ScaleQualifier(), true);
        }

        public static void RegisterQualifier(IBaseUriQualifier qualifier, bool throwOnError = true)
        {
            object olock = UriQualifierHelper.olock;
            lock (olock)
            {
                if (registeredQualifiers.ContainsKey(qualifier.Name))
                {
                    throw new ArgumentException("duplicate qualifier name, unregister first");
                }
                registeredQualifiers.Add(qualifier.Name, qualifier);
                QualifierListener.ResetInitialization();
            }
        }

        internal static void SetQualifierInfo(DependencyObject obj, object value)
        {
            obj.SetValue(QualifierInfoProperty, value);
        }

        private static Uri TryGetBaseUriInDesignMode(DependencyObject node)
        {
            string documentPathInDesignMode = GetDocumentPathInDesignMode(node);
            return (!string.IsNullOrEmpty(documentPathInDesignMode) ? new Uri(Path.GetDirectoryName(documentPathInDesignMode) + Path.DirectorySeparatorChar.ToString(), UriKind.Absolute) : null);
        }

        public static IBaseUriQualifier UnregisterQualifier(string qualifierName)
        {
            object olock = UriQualifierHelper.olock;
            lock (olock)
            {
                IBaseUriQualifier qualifier = null;
                if (registeredQualifiers.TryGetValue(qualifierName, out qualifier))
                {
                    registeredQualifiers.Remove(qualifierName);
                }
                return qualifier;
            }
        }

        private static Uri ValidateBaseUriInDesignMode(DependencyObject dObj, Uri result) => 
            DesignerProperties.GetIsInDesignMode(dObj) ? ((result.Scheme != Uri.UriSchemeFile) ? TryGetBaseUriInDesignMode(dObj) : result) : result;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UriQualifierHelper.<>c <>9 = new UriQualifierHelper.<>c();
            public static Func<DependencyObject, bool> <>9__26_0;
            public static Func<DevExpress.Xpf.Core.Native.UriInfo, bool> <>9__28_0;
            public static Func<DevExpress.Xpf.Core.Native.UriInfo, IEnumerable<UriQualifierValue>> <>9__30_0;
            public static Func<UriQualifierValue, IBaseUriQualifier> <>9__30_1;
            public static Func<DependencyProperty, Type> <>9__31_0;

            internal bool <GetDocumentPathInDesignMode>b__26_0(DependencyObject x) => 
                Convert.ToString(x).Contains("SceneScrollViewer");

            internal bool <ProvideBindingValue>b__28_0(DevExpress.Xpf.Core.Native.UriInfo x) => 
                x.BindableQualifier;

            internal IEnumerable<UriQualifierValue> <ProvideSimpleBinding>b__30_0(DevExpress.Xpf.Core.Native.UriInfo x) => 
                x.Qualifiers;

            internal IBaseUriQualifier <ProvideSimpleBinding>b__30_1(UriQualifierValue x) => 
                x.Qualifier;

            internal Type <ProvideStaticValue>b__31_0(DependencyProperty x) => 
                x.PropertyType;
        }

        private delegate object GetResourceManagerWrapper(object container, Uri uri, out string partName, out bool isContentFile);
    }
}

