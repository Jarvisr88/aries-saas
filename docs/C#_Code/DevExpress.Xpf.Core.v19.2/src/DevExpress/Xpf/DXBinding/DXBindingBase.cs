namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public abstract class DXBindingBase : DXMarkupExtensionBase
    {
        private Dictionary<string, object> staticResources;
        internal readonly IErrorHandler ErrorHandler;
        internal readonly ITypeResolver TypeResolver;

        public DXBindingBase()
        {
            this.ErrorHandler = new IErrorHandlerImpl(this);
            this.TypeResolver = new ITypeResolverImpl(this);
        }

        protected virtual void CheckTargetObject()
        {
            if (((!IsInSetter(base.TargetProvider) && (this.TargetPropertyType != typeof(BindingBase))) && (!(base.TargetProvider.TargetObject is Style) || !(base.TargetProvider.TargetObject is ITypedStyle))) && (!(base.TargetProvider.TargetObject is DependencyObject) || !(base.TargetProvider.TargetProperty is DependencyProperty)))
            {
                this.ErrorHandler.Throw(ErrorHelper.Err002(this), null);
            }
        }

        protected virtual void CheckTargetProvider()
        {
            if (base.TargetProvider == null)
            {
                this.ErrorHandler.Throw(ErrorHelper.Err001(this), null);
            }
            if ((base.TargetProvider.TargetObject == null) || (base.TargetProvider.TargetProperty == null))
            {
                this.ErrorHandler.Throw(ErrorHelper.Err002(this), null);
            }
        }

        private void CollectStaticResources()
        {
            if (base.XamlSchemaContextProvider != null)
            {
                IEnumerable<Operand> operands = this.GetOperands();
                if (operands != null)
                {
                    Func<Operand, bool> predicate = <>c.<>9__35_0;
                    if (<>c.<>9__35_0 == null)
                    {
                        Func<Operand, bool> local1 = <>c.<>9__35_0;
                        predicate = <>c.<>9__35_0 = x => x.Source == Operand.RelativeSource.Resource;
                    }
                    Func<Operand, string> selector = <>c.<>9__35_1;
                    if (<>c.<>9__35_1 == null)
                    {
                        Func<Operand, string> local2 = <>c.<>9__35_1;
                        selector = <>c.<>9__35_1 = x => x.ResourceName;
                    }
                    Func<string, string> keySelector = <>c.<>9__35_2;
                    if (<>c.<>9__35_2 == null)
                    {
                        Func<string, string> local3 = <>c.<>9__35_2;
                        keySelector = <>c.<>9__35_2 = x => x;
                    }
                    this.staticResources = operands.Where<Operand>(predicate).Select<Operand, string>(selector).Distinct<string>().ToDictionary<string, string, object>(keySelector, x => new StaticResourceExtension(x).ProvideValue(base.ServiceProvider));
                }
            }
        }

        protected Binding CreateBinding(Operand operand, BindingMode mode)
        {
            string path = ((operand == null) || string.IsNullOrEmpty(operand.Path)) ? "." : operand.Path;
            Binding binding = new Binding {
                Mode = mode
            };
            try
            {
                binding.Path = new PropertyPath(path, new object[0]);
            }
            catch (Exception exception)
            {
                this.ErrorHandler.Report(exception.Message, false);
            }
            if (operand != null)
            {
                try
                {
                    switch (operand.Source)
                    {
                        case Operand.RelativeSource.Context:
                            break;

                        case Operand.RelativeSource.Self:
                            binding.RelativeSource = new System.Windows.Data.RelativeSource(RelativeSourceMode.Self);
                            break;

                        case Operand.RelativeSource.Parent:
                            binding.RelativeSource = new System.Windows.Data.RelativeSource(RelativeSourceMode.TemplatedParent);
                            break;

                        case Operand.RelativeSource.Element:
                            binding.ElementName = operand.ElementName;
                            break;

                        case Operand.RelativeSource.Resource:
                            binding.Source = this.GetStaticResource(operand.ResourceName);
                            break;

                        case Operand.RelativeSource.Reference:
                            binding.Source = new Reference(operand.ReferenceName).ProvideValue(base.ServiceProvider);
                            break;

                        case Operand.RelativeSource.Ancestor:
                        {
                            System.Windows.Data.RelativeSource source1 = new System.Windows.Data.RelativeSource(RelativeSourceMode.FindAncestor);
                            source1.AncestorType = operand.AncestorType;
                            source1.AncestorLevel = operand.AncestorLevel;
                            binding.RelativeSource = source1;
                            break;
                        }
                        default:
                            throw new InvalidOperationException();
                    }
                }
                catch (Exception exception2)
                {
                    this.ErrorHandler.Report(exception2.Message, false);
                }
            }
            return binding;
        }

        protected abstract void Error_Report(string msg);
        protected abstract void Error_Throw(string msg, Exception innerException);
        protected abstract IEnumerable<Operand> GetOperands();
        protected abstract object GetProvidedValue();
        private object GetStaticResource(string resourceName)
        {
            object obj2;
            if (this.staticResources != null)
            {
                this.staticResources.TryGetValue(resourceName, out obj2);
            }
            else
            {
                obj2 = new StaticResourceExtension(resourceName).ProvideValue(base.ServiceProvider);
            }
            return obj2;
        }

        protected abstract void Init();
        private void InitTargetProperties()
        {
            if (base.TargetProvider.TargetObject is Setter)
            {
                Setter targetObject = (Setter) base.TargetProvider.TargetObject;
                if (targetObject.Property != null)
                {
                    this.TargetPropertyName = GetTargetPropertyName(targetObject.Property);
                    this.TargetPropertyType = GetTargetPropertyType(targetObject.Property);
                }
            }
            else
            {
                this.TargetObjectName = base.TargetProvider.TargetObject.GetType().Name;
                this.TargetPropertyName = GetTargetPropertyName(base.TargetProvider.TargetProperty);
                this.TargetPropertyType = GetTargetPropertyType(base.TargetProvider.TargetProperty);
            }
        }

        protected override object ProvideValueCore()
        {
            this.CheckTargetProvider();
            if ((base.XamlTypeResolver != null) && !((ITypeResolverImpl) this.TypeResolver).IsInitialized)
            {
                ((ITypeResolverImpl) this.TypeResolver).SetXamlTypeResolver(base.XamlTypeResolver);
                this.IsInitialized = true;
                this.Init();
            }
            if (IsInTemplate(base.TargetProvider))
            {
                this.CollectStaticResources();
                return this;
            }
            this.InitTargetProperties();
            this.CheckTargetObject();
            if (!this.IsInitialized)
            {
                this.Init();
            }
            object providedValue = this.GetProvidedValue();
            ((ITypeResolverImpl) this.TypeResolver).ClearXamlTypeResolver();
            return providedValue;
        }

        protected internal string TargetPropertyName { get; private set; }

        protected internal string TargetObjectName { get; private set; }

        protected internal Type TargetPropertyType { get; private set; }

        private bool IsInitialized { get; set; }

        public DXBindingResolvingMode? ResolvingMode { get; set; }

        protected DXBindingResolvingMode ActualResolvingMode
        {
            get
            {
                DXBindingResolvingMode? resolvingMode = this.ResolvingMode;
                return ((resolvingMode != null) ? resolvingMode.GetValueOrDefault() : CompatibilitySettings.DXBindingResolvingMode);
            }
        }

        public bool CatchExceptions
        {
            get => 
                this.ErrorHandler.CatchAllExceptions;
            set => 
                this.ErrorHandler.CatchAllExceptions = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXBindingBase.<>c <>9 = new DXBindingBase.<>c();
            public static Func<Operand, bool> <>9__35_0;
            public static Func<Operand, string> <>9__35_1;
            public static Func<string, string> <>9__35_2;

            internal bool <CollectStaticResources>b__35_0(Operand x) => 
                x.Source == Operand.RelativeSource.Resource;

            internal string <CollectStaticResources>b__35_1(Operand x) => 
                x.ResourceName;

            internal string <CollectStaticResources>b__35_2(string x) => 
                x;
        }

        internal class DXBindingConverterBase : IMultiValueConverter, IValueConverter
        {
            protected readonly IErrorHandler errorHandler;

            public DXBindingConverterBase(DXBindingBase owner)
            {
                this.errorHandler = owner.ErrorHandler;
            }

            protected virtual bool CanConvert(object[] values) => 
                !ValuesContainUnsetValue(values);

            protected virtual object CoerceAfterConvert(object value, Type targetType, object parameter, CultureInfo culture) => 
                value;

            protected virtual object[] CoerceAfterConvertBack(object[] value, Type[] targetTypes, object parameter, CultureInfo culture) => 
                value;

            protected virtual object CoerceBeforeConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => 
                value;

            protected virtual object Convert(object[] values, Type targetType) => 
                values;

            protected virtual object[] ConvertBack(object value, Type[] targetTypes)
            {
                throw new NotImplementedException();
            }

            private static object ConvertToTargetType(object value, Type targetType)
            {
                if ((value != Binding.DoNothing) && ((value != null) && ((targetType == typeof(string)) && !(value is string))))
                {
                    value = value.ToString();
                }
                return value;
            }

            object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if (!this.CanConvert(values))
                {
                    return Binding.DoNothing;
                }
                object obj2 = this.Convert(values, targetType);
                return ConvertToTargetType(this.CoerceAfterConvert(obj2, targetType, parameter, culture), targetType);
            }

            object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                value = this.CoerceBeforeConvertBack(value, targetTypes, parameter, culture);
                object[] objArray = this.ConvertBack(value, targetTypes);
                objArray = this.CoerceAfterConvertBack(objArray, targetTypes, parameter, culture);
                for (int i = 0; i < objArray.Count<object>(); i++)
                {
                    objArray[i] = ConvertToTargetType(objArray[i], targetTypes[i]);
                }
                return objArray.ToArray<object>();
            }

            object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                object[] values = new object[] { value };
                return ((IMultiValueConverter) this).Convert(values, targetType, parameter, culture);
            }

            object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                Type[] targetTypes = new Type[] { targetType };
                return ((IMultiValueConverter) this).ConvertBack(value, targetTypes, parameter, culture).First<object>();
            }

            public static bool ValuesContainUnsetValue(object[] values)
            {
                foreach (object obj2 in values)
                {
                    if (obj2 == DependencyProperty.UnsetValue)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private class IErrorHandlerImpl : ErrorHandlerBase
        {
            private readonly DXBindingBase owner;

            public IErrorHandlerImpl(DXBindingBase owner)
            {
                this.owner = owner;
            }

            protected override void ReportCore(string msg)
            {
                if ((msg != null) && !DXMarkupExtensionBase.IsInDesignMode())
                {
                    this.owner.Error_Report(msg);
                }
            }

            protected override void ThrowCore(string msg, Exception innerException)
            {
                this.owner.Error_Throw(msg, innerException);
            }
        }

        private class ITypeResolverImpl : ITypeResolver
        {
            private readonly DXBindingBase owner;
            private IXamlTypeResolver xamlTypeResolver;
            private Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

            public ITypeResolverImpl(DXBindingBase owner)
            {
                this.owner = owner;
            }

            public void ClearXamlTypeResolver()
            {
                this.xamlTypeResolver = null;
            }

            Type ITypeResolver.ResolveType(string type)
            {
                if (this.typeCache.ContainsKey(type))
                {
                    return this.typeCache[type];
                }
                if (DXMarkupExtensionBase.IsInDesignMode())
                {
                    this.owner.ErrorHandler.SetError();
                    return null;
                }
                try
                {
                    Type type2 = this.xamlTypeResolver.Resolve(type);
                    this.typeCache.Add(type, type2);
                    return type2;
                }
                catch (Exception exception)
                {
                    this.owner.ErrorHandler.Throw(ErrorHelper.Err004(type), exception);
                    return null;
                }
            }

            public void SetXamlTypeResolver(IXamlTypeResolver xamlTypeResolver)
            {
                this.xamlTypeResolver = xamlTypeResolver;
            }

            public bool IsInitialized =>
                this.xamlTypeResolver != null;
        }
    }
}

