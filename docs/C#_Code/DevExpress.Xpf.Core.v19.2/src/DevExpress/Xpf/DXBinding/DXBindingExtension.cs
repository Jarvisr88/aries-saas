namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public sealed class DXBindingExtension : DXBindingBase
    {
        internal static System.Windows.Data.UpdateSourceTrigger? DefaultUpdateSourceTrigger;
        private bool isFallbackValueSet;
        private object fallbackValue;

        public DXBindingExtension() : this(string.Empty)
        {
        }

        public DXBindingExtension(string expr)
        {
            this.fallbackValue = DependencyProperty.UnsetValue;
            this.Expr = expr;
            System.Windows.Data.UpdateSourceTrigger? defaultUpdateSourceTrigger = DefaultUpdateSourceTrigger;
            this.UpdateSourceTrigger = (defaultUpdateSourceTrigger != null) ? defaultUpdateSourceTrigger.GetValueOrDefault() : System.Windows.Data.UpdateSourceTrigger.Default;
            this.Mode = BindingMode.Default;
            this.BindingGroupName = string.Empty;
            this.AllowUnsetValue = false;
        }

        private BindingBase CreateBinding()
        {
            if (this.Calculator.Operands.Count<Operand>() == 0)
            {
                Binding binding = base.CreateBinding(null, this.ActualMode);
                this.SetBindingProperties(binding, true);
                binding.Source = this.Calculator.Resolve(null);
                binding.Converter = this.Converter;
                binding.ConverterParameter = this.ConverterParameter;
                binding.ConverterCulture = this.ConverterCulture;
                return binding;
            }
            if (this.Calculator.Operands.Count<Operand>() == 1)
            {
                Binding binding = base.CreateBinding(this.Calculator.Operands.First<Operand>(), this.ActualMode);
                this.SetBindingProperties(binding, true);
                binding.Converter = this.CreateConverter();
                binding.ConverterParameter = this.ConverterParameter;
                binding.ConverterCulture = this.ConverterCulture;
                return binding;
            }
            if (this.Calculator.Operands.Count<Operand>() <= 1)
            {
                throw new NotImplementedException();
            }
            MultiBinding binding1 = new MultiBinding();
            binding1.Mode = this.ActualMode;
            MultiBinding binding3 = binding1;
            this.SetBindingProperties(binding3, true);
            binding3.Converter = this.CreateConverter();
            binding3.ConverterParameter = this.ConverterParameter;
            binding3.ConverterCulture = this.ConverterCulture;
            foreach (Operand operand in this.Calculator.Operands)
            {
                BindingMode mode = (this.ActualMode == BindingMode.OneTime) ? BindingMode.OneTime : BindingMode.OneWay;
                if (operand.IsTwoWay)
                {
                    if (this.ActualMode == BindingMode.Default)
                    {
                        mode = BindingMode.Default;
                    }
                    mode = (this.ActualMode == BindingMode.OneWayToSource) ? BindingMode.OneWayToSource : BindingMode.TwoWay;
                }
                Binding binding = base.CreateBinding(operand, mode);
                this.SetBindingProperties(binding, false);
                binding3.Bindings.Add(binding);
            }
            return binding3;
        }

        private DXBindingBase.DXBindingConverterBase CreateConverter() => 
            (base.ActualResolvingMode != DXBindingResolvingMode.LegacyStaticTyping) ? ((DXBindingBase.DXBindingConverterBase) new DXBindingConverterDynamic(this, (BindingCalculatorDynamic) this.Calculator, this.AllowUnsetValue)) : ((DXBindingBase.DXBindingConverterBase) new DXBindingConverter(this, (BindingCalculator) this.Calculator));

        protected override void Error_Report(string msg)
        {
            DXBindingExceptionBase<DXBindingException, DXBindingExtension>.Report(this, msg);
        }

        protected override void Error_Throw(string msg, Exception innerException)
        {
            DXBindingExceptionBase<DXBindingException, DXBindingExtension>.Throw(this, msg, innerException);
        }

        protected override IEnumerable<Operand> GetOperands() => 
            this.Calculator?.Operands;

        protected override object GetProvidedValue()
        {
            if ((this.Mode == BindingMode.Default) && (this.TreeInfo.IsEmptyBackExpr() && (this.Calculator.Operands.Count<Operand>() == 0)))
            {
                this.ActualMode = BindingMode.OneWay;
            }
            if (((this.ActualMode == BindingMode.TwoWay) || (this.ActualMode == BindingMode.OneWayToSource)) && this.TreeInfo.IsEmptyBackExpr())
            {
                if (!this.TreeInfo.IsSimpleExpr())
                {
                    base.ErrorHandler.Throw(ErrorHelper.Err101_TwoWay(), null);
                }
                else
                {
                    Action<Operand> action = <>c.<>9__84_0;
                    if (<>c.<>9__84_0 == null)
                    {
                        Action<Operand> local1 = <>c.<>9__84_0;
                        action = <>c.<>9__84_0 = delegate (Operand x) {
                            x.SetMode(true);
                        };
                    }
                    this.Calculator.Operands.FirstOrDefault<Operand>().Do<Operand>(action);
                }
            }
            return ((IsInSetter(base.TargetProvider) || (base.TargetPropertyType == typeof(BindingBase))) ? this.CreateBinding() : this.CreateBinding().ProvideValue(base.ServiceProvider));
        }

        protected override void Init()
        {
            this.TreeInfo = new BindingTreeInfo(this.Expr, this.BackExpr, base.ErrorHandler);
            this.Calculator = (base.ActualResolvingMode != DXBindingResolvingMode.LegacyStaticTyping) ? ((IBindingCalculator) new BindingCalculatorDynamic(this.TreeInfo, this.FallbackValue)) : ((IBindingCalculator) new BindingCalculator(this.TreeInfo, this.FallbackValue));
            this.Calculator.Init(base.TypeResolver);
        }

        protected override object ProvideValueCore()
        {
            Func<IProvideValueTarget, DependencyProperty> evaluator = <>c.<>9__82_0;
            if (<>c.<>9__82_0 == null)
            {
                Func<IProvideValueTarget, DependencyProperty> local1 = <>c.<>9__82_0;
                evaluator = <>c.<>9__82_0 = x => x.TargetProperty as DependencyProperty;
            }
            DependencyProperty property = base.TargetProvider.With<IProvideValueTarget, DependencyProperty>(evaluator);
            Func<IProvideValueTarget, DependencyObject> func2 = <>c.<>9__82_1;
            if (<>c.<>9__82_1 == null)
            {
                Func<IProvideValueTarget, DependencyObject> local2 = <>c.<>9__82_1;
                func2 = <>c.<>9__82_1 = x => x.TargetObject as DependencyObject;
            }
            DependencyObject dependencyObject = base.TargetProvider.With<IProvideValueTarget, DependencyObject>(func2);
            PropertyMetadata metadata = ((property == null) || (dependencyObject == null)) ? null : property.GetMetadata(dependencyObject);
            this.ActualMode = ((this.Mode != BindingMode.Default) || (metadata == null)) ? this.Mode : ((metadata is FrameworkPropertyMetadata) ? (!((FrameworkPropertyMetadata) metadata).BindsTwoWayByDefault ? BindingMode.OneWay : BindingMode.TwoWay) : BindingMode.OneWay);
            return base.ProvideValueCore();
        }

        private void SetBindingProperties(BindingBase binding, bool isRootBinding)
        {
            if (isRootBinding)
            {
                if (this.isFallbackValueSet)
                {
                    binding.FallbackValue = this.FallbackValue;
                }
                if (!string.IsNullOrEmpty(this.BindingGroupName))
                {
                    binding.BindingGroupName = this.BindingGroupName;
                }
                if (this.TargetNullValue != null)
                {
                    binding.TargetNullValue = this.TargetNullValue;
                }
            }
            if (binding is Binding)
            {
                Binding binding2 = (Binding) binding;
                binding2.NotifyOnSourceUpdated = this.NotifyOnSourceUpdated;
                binding2.NotifyOnTargetUpdated = this.NotifyOnTargetUpdated;
                binding2.NotifyOnValidationError = this.NotifyOnValidationError;
                binding2.UpdateSourceTrigger = this.UpdateSourceTrigger;
                binding2.ValidatesOnDataErrors = this.ValidatesOnDataErrors;
                binding2.ValidatesOnExceptions = this.ValidatesOnExceptions;
            }
            else
            {
                MultiBinding binding3 = (MultiBinding) binding;
                binding3.NotifyOnSourceUpdated = this.NotifyOnSourceUpdated;
                binding3.NotifyOnTargetUpdated = this.NotifyOnTargetUpdated;
                binding3.NotifyOnValidationError = this.NotifyOnValidationError;
                binding3.UpdateSourceTrigger = this.UpdateSourceTrigger;
                binding3.ValidatesOnDataErrors = this.ValidatesOnDataErrors;
                binding3.ValidatesOnExceptions = this.ValidatesOnExceptions;
            }
        }

        public string BindingGroupName { get; set; }

        public object TargetNullValue { get; set; }

        public bool NotifyOnSourceUpdated { get; set; }

        public bool NotifyOnTargetUpdated { get; set; }

        public bool NotifyOnValidationError { get; set; }

        public System.Windows.Data.UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public bool ValidatesOnDataErrors { get; set; }

        public bool ValidatesOnExceptions { get; set; }

        public string Expr { get; set; }

        public string BackExpr { get; set; }

        public BindingMode Mode { get; set; }

        public object FallbackValue
        {
            get => 
                this.fallbackValue;
            set
            {
                this.fallbackValue = value;
                this.isFallbackValueSet = true;
            }
        }

        public IValueConverter Converter { get; set; }

        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }

        public object ConverterParameter { get; set; }

        public bool AllowUnsetValue { get; set; }

        private BindingMode ActualMode { get; set; }

        private BindingTreeInfo TreeInfo { get; set; }

        private IBindingCalculator Calculator { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXBindingExtension.<>c <>9 = new DXBindingExtension.<>c();
            public static Func<IProvideValueTarget, DependencyProperty> <>9__82_0;
            public static Func<IProvideValueTarget, DependencyObject> <>9__82_1;
            public static Action<Operand> <>9__84_0;

            internal void <GetProvidedValue>b__84_0(Operand x)
            {
                x.SetMode(true);
            }

            internal DependencyProperty <ProvideValueCore>b__82_0(IProvideValueTarget x) => 
                x.TargetProperty as DependencyProperty;

            internal DependencyObject <ProvideValueCore>b__82_1(IProvideValueTarget x) => 
                x.TargetObject as DependencyObject;
        }

        private class DXBindingConverter : DXBindingBase.DXBindingConverterBase
        {
            private readonly BindingTreeInfo treeInfo;
            private readonly BindingCalculator calculator;
            private readonly IValueConverter externalConverter;
            private Type backConversionType;
            private bool isBackConversionInitialized;

            public DXBindingConverter(DXBindingExtension owner, BindingCalculator calculator) : base(owner)
            {
                this.treeInfo = owner.TreeInfo;
                this.calculator = calculator;
                this.backConversionType = owner.TargetPropertyType;
                this.externalConverter = owner.Converter;
            }

            protected override object CoerceAfterConvert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (this.externalConverter != null)
                {
                    return this.externalConverter.Convert(value, targetType, parameter, culture);
                }
                value = ((value != DependencyProperty.UnsetValue) || !(targetType == typeof(string))) ? ObjectToObjectConverter.Coerce(value, targetType, true, false) : null;
                return base.CoerceAfterConvert(value, targetType, parameter, culture);
            }

            protected override object CoerceBeforeConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                if (this.externalConverter == null)
                {
                    return base.CoerceBeforeConvertBack(value, targetTypes, parameter, culture);
                }
                Type targetType = ((targetTypes == null) || (targetTypes.Count<Type>() != 1)) ? this.backConversionType : targetTypes[0];
                return this.externalConverter.ConvertBack(value, targetType, parameter, culture);
            }

            protected override object Convert(object[] values, Type targetType)
            {
                if (this.backConversionType == null)
                {
                    this.backConversionType = targetType;
                }
                base.errorHandler.ClearError();
                return this.calculator.Resolve(values);
            }

            protected override object[] ConvertBack(object value, Type[] targetTypes)
            {
                if (this.treeInfo.IsEmptyBackExpr() && !this.treeInfo.IsSimpleExpr())
                {
                    base.errorHandler.Throw(ErrorHelper.Err101_TwoWay(), null);
                }
                if (!this.isBackConversionInitialized)
                {
                    Func<object, Type> evaluator = <>c.<>9__7_0;
                    if (<>c.<>9__7_0 == null)
                    {
                        Func<object, Type> local1 = <>c.<>9__7_0;
                        evaluator = <>c.<>9__7_0 = x => x.GetType();
                    }
                    Type type = value.Return<object, Type>(evaluator, <>c.<>9__7_1 ??= ((Func<Type>) (() => null)));
                    if ((type ?? this.backConversionType) == null)
                    {
                        base.errorHandler.Throw(ErrorHelper.Err104(), null);
                    }
                    Type backExprType = type;
                    if (type == null)
                    {
                        Type local4 = type;
                        backExprType = this.backConversionType;
                    }
                    this.calculator.InitBack(backExprType);
                    this.isBackConversionInitialized = true;
                }
                List<object> list = new List<object>();
                foreach (Operand operand in this.calculator.Operands)
                {
                    if (!operand.IsTwoWay || (operand.BackConverter == null))
                    {
                        list.Add(value);
                        continue;
                    }
                    object[] arg = new object[] { value };
                    list.Add(operand.BackConverter(arg));
                }
                return list.ToArray();
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXBindingExtension.DXBindingConverter.<>c <>9 = new DXBindingExtension.DXBindingConverter.<>c();
                public static Func<object, Type> <>9__7_0;
                public static Func<Type> <>9__7_1;

                internal Type <ConvertBack>b__7_0(object x) => 
                    x.GetType();

                internal Type <ConvertBack>b__7_1() => 
                    null;
            }
        }

        private class DXBindingConverterDynamic : DXBindingBase.DXBindingConverterBase
        {
            private readonly BindingTreeInfo treeInfo;
            private readonly BindingCalculatorDynamic calculator;
            private readonly IValueConverter externalConverter;
            private readonly bool allowUnsetValue;
            private List<WeakReference> valueRefs;

            public DXBindingConverterDynamic(DXBindingExtension owner, BindingCalculatorDynamic calculator, bool allowUnsetValue) : base(owner)
            {
                this.treeInfo = owner.TreeInfo;
                this.calculator = calculator;
                this.externalConverter = owner.Converter;
                this.allowUnsetValue = allowUnsetValue;
            }

            protected override bool CanConvert(object[] values) => 
                this.allowUnsetValue || !ValuesContainUnsetValue(values);

            protected override object CoerceAfterConvert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (this.externalConverter != null)
                {
                    return this.externalConverter.Convert(value, targetType, parameter, culture);
                }
                if (value == Binding.DoNothing)
                {
                    return value;
                }
                value = ((value != DependencyProperty.UnsetValue) || !(targetType == typeof(string))) ? ObjectToObjectConverter.Coerce(value, targetType, true, false) : null;
                return base.CoerceAfterConvert(value, targetType, parameter, culture);
            }

            protected override object[] CoerceAfterConvertBack(object[] value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                for (int i = 0; i < value.Count<object>(); i++)
                {
                    value[i] = ObjectToObjectConverter.Coerce(value[i], targetTypes[i], true, false);
                }
                return base.CoerceAfterConvertBack(value, targetTypes, parameter, culture);
            }

            protected override object CoerceBeforeConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                if (this.externalConverter == null)
                {
                    return base.CoerceBeforeConvertBack(value, targetTypes, parameter, culture);
                }
                Type targetType = ((targetTypes == null) || (targetTypes.Count<Type>() != 1)) ? null : targetTypes[0];
                return this.externalConverter.ConvertBack(value, targetType, parameter, culture);
            }

            protected override object Convert(object[] values, Type targetType)
            {
                base.errorHandler.ClearError();
                Func<object, WeakReference> selector = <>c.<>9__6_0;
                if (<>c.<>9__6_0 == null)
                {
                    Func<object, WeakReference> local1 = <>c.<>9__6_0;
                    selector = <>c.<>9__6_0 = x => new WeakReference(x);
                }
                this.valueRefs = values.Select<object, WeakReference>(selector).ToList<WeakReference>();
                return this.calculator.Resolve(values);
            }

            protected override object[] ConvertBack(object value, Type[] targetTypes)
            {
                if (this.treeInfo.IsEmptyBackExpr() && !this.treeInfo.IsSimpleExpr())
                {
                    base.errorHandler.Throw(ErrorHelper.Err101_TwoWay(), null);
                }
                if (this.treeInfo.IsEmptyBackExpr())
                {
                    return (from x in Enumerable.Range(0, this.calculator.Operands.Count<Operand>()) select value).ToArray<object>();
                }
                Func<WeakReference, object> selector = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<WeakReference, object> local1 = <>c.<>9__7_1;
                    selector = <>c.<>9__7_1 = x => x.Target;
                }
                object[] values = this.valueRefs.Select<WeakReference, object>(selector).ToArray<object>();
                return this.calculator.ResolveBack(values, value).ToArray<object>();
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXBindingExtension.DXBindingConverterDynamic.<>c <>9 = new DXBindingExtension.DXBindingConverterDynamic.<>c();
                public static Func<object, WeakReference> <>9__6_0;
                public static Func<WeakReference, object> <>9__7_1;

                internal WeakReference <Convert>b__6_0(object x) => 
                    new WeakReference(x);

                internal object <ConvertBack>b__7_1(WeakReference x) => 
                    x.Target;
            }
        }
    }
}

