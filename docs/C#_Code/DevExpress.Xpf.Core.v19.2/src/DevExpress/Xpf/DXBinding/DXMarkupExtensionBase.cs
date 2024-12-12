namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Xaml;

    public abstract class DXMarkupExtensionBase : MarkupExtension
    {
        internal static bool? IsInDesingModeCore;
        private IServiceProvider serviceProvider;
        private IProvideValueTarget targetProvider;
        private IXamlTypeResolver xamlTypeResolver;
        private IXamlSchemaContextProvider xamlSchemaContextProvider;

        protected DXMarkupExtensionBase()
        {
        }

        protected static string GetTargetPropertyName(object targetProperty)
        {
            Func<DependencyProperty, string> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DependencyProperty, string> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.Name;
            }
            string local2 = (targetProperty as DependencyProperty).With<DependencyProperty, string>(evaluator);
            string local11 = local2;
            if (local2 == null)
            {
                string local3 = local2;
                string local5 = (targetProperty as PropertyInfo).With<PropertyInfo, string>(<>c.<>9__2_1 ??= x => x.Name);
                local11 = local5;
                if (local5 == null)
                {
                    string local6 = local5;
                    string local8 = (targetProperty as MethodBase).With<MethodBase, string>(<>c.<>9__2_2 ??= x => x.Name);
                    local11 = local8;
                    if (local8 == null)
                    {
                        string local9 = local8;
                        local11 = (targetProperty as EventInfo).With<EventInfo, string>(<>c.<>9__2_3 ??= x => x.Name);
                    }
                }
            }
            return local11;
        }

        protected static Type GetTargetPropertyType(object targetProperty)
        {
            Func<DependencyProperty, Type> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DependencyProperty, Type> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.PropertyType;
            }
            Type local2 = (targetProperty as DependencyProperty).With<DependencyProperty, Type>(evaluator);
            Type local8 = local2;
            if (local2 == null)
            {
                Type local3 = local2;
                Type local5 = (targetProperty as PropertyInfo).With<PropertyInfo, Type>(<>c.<>9__3_1 ??= x => x.PropertyType);
                local8 = local5;
                if (local5 == null)
                {
                    Type local6 = local5;
                    local8 = (targetProperty as EventInfo).With<EventInfo, Type>(<>c.<>9__3_2 ??= x => x.EventHandlerType);
                }
            }
            return local8;
        }

        protected static bool IsInBinding(IProvideValueTarget targetProvider) => 
            (targetProvider != null) ? (targetProvider.TargetObject is BindingBase) : false;

        protected static bool IsInDesignMode() => 
            (IsInDesingModeCore == null) ? ((bool) DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue) : IsInDesingModeCore.Value;

        protected static bool IsInSetter(IProvideValueTarget targetProvider) => 
            (targetProvider != null) ? (targetProvider.TargetObject is Setter) : false;

        protected static bool IsInTemplate(IProvideValueTarget targetProvider) => 
            (targetProvider != null) ? (targetProvider.TargetObject.GetType().FullName == "System.Windows.SharedDp") : false;

        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            object obj2;
            try
            {
                this.serviceProvider = serviceProvider;
                obj2 = this.ProvideValueCore();
            }
            finally
            {
                this.serviceProvider = null;
                this.targetProvider = null;
                this.xamlTypeResolver = null;
                this.xamlSchemaContextProvider = null;
            }
            return obj2;
        }

        protected abstract object ProvideValueCore();

        protected IServiceProvider ServiceProvider =>
            this.serviceProvider;

        protected IProvideValueTarget TargetProvider
        {
            get
            {
                IProvideValueTarget target;
                if (this.targetProvider != null)
                {
                    return this.targetProvider;
                }
                if (this.serviceProvider == null)
                {
                    return null;
                }
                this.targetProvider = target = (IProvideValueTarget) this.serviceProvider.GetService(typeof(IProvideValueTarget));
                return target;
            }
        }

        protected IXamlTypeResolver XamlTypeResolver
        {
            get
            {
                IXamlTypeResolver resolver;
                if (this.xamlTypeResolver != null)
                {
                    return this.xamlTypeResolver;
                }
                if (this.serviceProvider == null)
                {
                    return null;
                }
                this.xamlTypeResolver = resolver = (IXamlTypeResolver) this.serviceProvider.GetService(typeof(IXamlTypeResolver));
                return resolver;
            }
        }

        protected IXamlSchemaContextProvider XamlSchemaContextProvider
        {
            get
            {
                IXamlSchemaContextProvider provider;
                if (this.xamlSchemaContextProvider != null)
                {
                    return this.xamlSchemaContextProvider;
                }
                if (this.serviceProvider == null)
                {
                    return null;
                }
                this.xamlSchemaContextProvider = provider = (IXamlSchemaContextProvider) this.serviceProvider.GetService(typeof(IXamlSchemaContextProvider));
                return provider;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXMarkupExtensionBase.<>c <>9 = new DXMarkupExtensionBase.<>c();
            public static Func<DependencyProperty, string> <>9__2_0;
            public static Func<PropertyInfo, string> <>9__2_1;
            public static Func<MethodBase, string> <>9__2_2;
            public static Func<EventInfo, string> <>9__2_3;
            public static Func<DependencyProperty, Type> <>9__3_0;
            public static Func<PropertyInfo, Type> <>9__3_1;
            public static Func<EventInfo, Type> <>9__3_2;

            internal string <GetTargetPropertyName>b__2_0(DependencyProperty x) => 
                x.Name;

            internal string <GetTargetPropertyName>b__2_1(PropertyInfo x) => 
                x.Name;

            internal string <GetTargetPropertyName>b__2_2(MethodBase x) => 
                x.Name;

            internal string <GetTargetPropertyName>b__2_3(EventInfo x) => 
                x.Name;

            internal Type <GetTargetPropertyType>b__3_0(DependencyProperty x) => 
                x.PropertyType;

            internal Type <GetTargetPropertyType>b__3_1(PropertyInfo x) => 
                x.PropertyType;

            internal Type <GetTargetPropertyType>b__3_2(EventInfo x) => 
                x.EventHandlerType;
        }
    }
}

