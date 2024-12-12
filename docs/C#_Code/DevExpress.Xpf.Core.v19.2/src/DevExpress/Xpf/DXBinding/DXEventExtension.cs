namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public sealed class DXEventExtension : DXBindingBase
    {
        public DXEventExtension() : this(string.Empty)
        {
        }

        public DXEventExtension(string expr)
        {
            this.Handler = expr;
        }

        protected override void CheckTargetObject()
        {
            if (!(base.TargetProvider.TargetObject is DependencyObject))
            {
                base.ErrorHandler.Throw(ErrorHelper.Err002(this), null);
            }
            if (!(base.TargetProvider.TargetProperty is EventInfo))
            {
                if (base.TargetProvider.TargetProperty is MethodInfo)
                {
                    MethodInfo targetProperty = (MethodInfo) base.TargetProvider.TargetProperty;
                    if (targetProperty.Name.StartsWith("Add") && targetProperty.Name.EndsWith("Handler"))
                    {
                        return;
                    }
                }
                base.ErrorHandler.Throw(ErrorHelper.Err002(this), null);
            }
        }

        protected override void CheckTargetProvider()
        {
            base.CheckTargetProvider();
            if (IsInSetter(base.TargetProvider))
            {
                base.ErrorHandler.Throw(ErrorHelper.Err003(this), null);
            }
        }

        private BindingBase CreateBinding()
        {
            if (this.Calculator.Operands.Count<Operand>() == 0)
            {
                Binding binding = base.CreateBinding(null, BindingMode.OneTime);
                binding.Source = null;
                binding.Converter = new EventConverter(this, true);
                return binding;
            }
            if (this.Calculator.Operands.Count<Operand>() == 1)
            {
                Binding binding2 = base.CreateBinding(this.Calculator.Operands.First<Operand>(), BindingMode.OneTime);
                binding2.Converter = new EventConverter(this, false);
                return binding2;
            }
            if (this.Calculator.Operands.Count<Operand>() <= 1)
            {
                throw new NotImplementedException();
            }
            MultiBinding binding1 = new MultiBinding();
            binding1.Mode = BindingMode.OneTime;
            MultiBinding binding3 = binding1;
            foreach (Operand operand in this.Calculator.Operands)
            {
                Binding item = base.CreateBinding(operand, BindingMode.OneTime);
                binding3.Bindings.Add(item);
            }
            binding3.Converter = new EventConverter(this, false);
            return binding3;
        }

        protected override void Error_Report(string msg)
        {
            DXBindingExceptionBase<DXEventException, DXEventExtension>.Report(this, msg);
        }

        protected override void Error_Throw(string msg, Exception innerException)
        {
            DXBindingExceptionBase<DXEventException, DXEventExtension>.Throw(this, msg, innerException);
        }

        private Type GetEventHandlerType() => 
            !(base.TargetProvider.TargetProperty is EventInfo) ? ((MethodInfo) base.TargetProvider.TargetProperty).GetParameters().ElementAt<ParameterInfo>(1).ParameterType : ((EventInfo) base.TargetProvider.TargetProperty).EventHandlerType;

        protected override IEnumerable<Operand> GetOperands() => 
            this.Calculator?.Operands;

        protected override object GetProvidedValue() => 
            new EventBinder(this, this.GetEventHandlerType(), this.CreateBinding()).GetEventHandler();

        protected override void Init()
        {
            this.TreeInfo = new EventTreeInfo(this.Handler, base.ErrorHandler);
            this.Calculator = (base.ActualResolvingMode != DXBindingResolvingMode.LegacyStaticTyping) ? ((IEventCalculator) new EventCalculatorDynamic(this.TreeInfo)) : ((IEventCalculator) new EventCalculator(this.TreeInfo));
            this.Calculator.Init(base.TypeResolver);
        }

        public string Handler { get; set; }

        private EventTreeInfo TreeInfo { get; set; }

        private IEventCalculator Calculator { get; set; }

        private class EventBinder
        {
            private readonly IErrorHandler errorHandler;
            private readonly IEventCalculator calculator;
            private readonly WeakReference targetObject;
            private readonly string handler;
            private readonly Type targetType;
            private readonly string targetPropertyName;
            private readonly Type targetPropertyType;
            private readonly Type eventHandlerType;
            [IgnoreDependencyPropertiesConsistencyChecker]
            private DependencyProperty dataProperty;
            private static object locker = new object();
            private static long dataPropertyIndex = 0L;
            private static Dictionary<Tuple<Type, string>, DependencyProperty> propertiesCache = new Dictionary<Tuple<Type, string>, DependencyProperty>();

            public EventBinder(DXEventExtension owner, Type eventHandlerType, BindingBase binding)
            {
                object locker = DXEventExtension.EventBinder.locker;
                lock (locker)
                {
                    this.errorHandler = owner.ErrorHandler;
                    this.calculator = owner.Calculator;
                    DependencyObject targetObject = (DependencyObject) owner.TargetProvider.TargetObject;
                    this.targetObject = new WeakReference(targetObject);
                    this.handler = owner.Handler;
                    this.targetType = targetObject.GetType();
                    this.targetPropertyName = owner.TargetPropertyName;
                    this.targetPropertyType = owner.TargetPropertyType;
                    this.eventHandlerType = eventHandlerType;
                    Tuple<Type, string> key = Tuple.Create<Type, string>(targetObject.GetType(), owner.Handler);
                    if (!propertiesCache.TryGetValue(key, out this.dataProperty))
                    {
                        long dataPropertyIndex = DXEventExtension.EventBinder.dataPropertyIndex;
                        DXEventExtension.EventBinder.dataPropertyIndex = dataPropertyIndex + 1L;
                        this.dataProperty = DependencyProperty.Register("Tag" + dataPropertyIndex.ToString(), typeof(object), key.Item1);
                        propertiesCache[key] = this.dataProperty;
                    }
                    this.BindCore(binding);
                }
            }

            private void BindCore(BindingBase binding)
            {
                if (this.TargetObject != null)
                {
                    try
                    {
                        BindingOperations.SetBinding(this.TargetObject, this.dataProperty, binding);
                    }
                    catch (Exception exception1)
                    {
                        object[] objArray1 = new object[0x10];
                        objArray1[0] = "DXEvent cannot set binding on data property. ";
                        objArray1[1] = Environment.NewLine;
                        objArray1[2] = "Expr: ";
                        objArray1[3] = this.handler;
                        objArray1[4] = Environment.NewLine;
                        objArray1[5] = "TargetProperty: ";
                        objArray1[6] = this.targetPropertyName;
                        objArray1[7] = Environment.NewLine;
                        objArray1[8] = "TargetPropertyType: ";
                        objArray1[9] = this.targetPropertyType.ToString();
                        objArray1[10] = Environment.NewLine;
                        objArray1[11] = "TargetObjectType: ";
                        objArray1[12] = this.targetType;
                        objArray1[13] = Environment.NewLine;
                        objArray1[14] = "DataProperty: ";
                        objArray1[15] = this.dataProperty.Name;
                        string message = string.Concat(objArray1);
                        throw new DXEventException(this.targetPropertyName, this.targetType.ToString(), this.handler, message, exception1);
                    }
                }
            }

            private object[] GetBoundEventData()
            {
                if (!this.IsAlive)
                {
                    return null;
                }
                IEnumerable<object> source = (IEnumerable<object>) this.TargetObject.GetValue(this.dataProperty);
                if (source == null)
                {
                    BindingExpression bindingExpression = BindingOperations.GetBindingExpression(this.TargetObject, this.dataProperty);
                    if ((bindingExpression != null) && (bindingExpression.Status == BindingStatus.Unattached))
                    {
                        Binding binding = BindingOperations.GetBinding(this.TargetObject, this.dataProperty);
                        this.BindCore(binding);
                        source = (IEnumerable<object>) this.TargetObject.GetValue(this.dataProperty);
                    }
                }
                return ((source == null) ? null : source.ToArray<object>());
            }

            public Delegate GetEventHandler() => 
                new EventTriggerEventSubscriber(new Action<object, object>(this.OnEvent)).CreateEventHandler(this.eventHandlerType);

            private void OnEvent(object sender, object eventArgs)
            {
                object[] boundEventData = this.GetBoundEventData();
                this.errorHandler.ClearError();
                this.calculator.Event(boundEventData, sender, eventArgs);
            }

            private DependencyObject TargetObject =>
                (DependencyObject) this.targetObject.Target;

            private bool IsAlive =>
                this.targetObject.IsAlive;
        }

        private class EventConverter : DXBindingBase.DXBindingConverterBase
        {
            private readonly bool isEmpty;

            public EventConverter(DXEventExtension owner, bool isEmpty) : base(owner)
            {
                this.isEmpty = isEmpty;
            }

            protected override object Convert(object[] values, Type targetType) => 
                this.isEmpty ? null : new List<object>(values ?? new object[0]);
        }
    }
}

