namespace DevExpress.Xpf.DXBinding
{
    using DevExpress.DXBinding.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Input;

    public sealed class DXCommandExtension : DXBindingBase
    {
        public DXCommandExtension() : this(string.Empty)
        {
        }

        public DXCommandExtension(string execute)
        {
            this.Execute = execute;
        }

        private BindingBase CreateBinding()
        {
            if (this.Calculator.Operands.Count<Operand>() == 0)
            {
                Binding binding = base.CreateBinding(null, BindingMode.OneWay);
                this.SetBindingProperties(binding, true);
                binding.Source = null;
                binding.Converter = this.CreateConverter(true);
                return binding;
            }
            if (this.Calculator.Operands.Count<Operand>() == 1)
            {
                Binding binding = base.CreateBinding(this.Calculator.Operands.First<Operand>(), BindingMode.OneWay);
                this.SetBindingProperties(binding, true);
                binding.Converter = this.CreateConverter(false);
                return binding;
            }
            if (this.Calculator.Operands.Count<Operand>() <= 1)
            {
                throw new NotImplementedException();
            }
            MultiBinding binding1 = new MultiBinding();
            binding1.Mode = BindingMode.OneWay;
            MultiBinding binding3 = binding1;
            this.SetBindingProperties(binding3, true);
            binding3.Converter = this.CreateConverter(false);
            foreach (Operand operand in this.Calculator.Operands)
            {
                Binding binding = base.CreateBinding(operand, BindingMode.OneWay);
                this.SetBindingProperties(binding, false);
                binding3.Bindings.Add(binding);
            }
            return binding3;
        }

        private DXCommandConverter CreateConverter(bool isEmpty) => 
            new DXCommandConverter(this, isEmpty);

        protected override void Error_Report(string msg)
        {
            DXBindingExceptionBase<DXCommandException, DXCommandExtension>.Report(this, msg);
        }

        protected override void Error_Throw(string msg, Exception innerException)
        {
            DXBindingExceptionBase<DXCommandException, DXCommandExtension>.Throw(this, msg, innerException);
        }

        protected override IEnumerable<Operand> GetOperands() => 
            this.Calculator?.Operands;

        protected override object GetProvidedValue() => 
            !IsInSetter(base.TargetProvider) ? this.CreateBinding().ProvideValue(base.ServiceProvider) : this.CreateBinding();

        protected override void Init()
        {
            this.TreeInfo = new CommandTreeInfo(this.Execute, this.CanExecute, base.ErrorHandler);
            this.Calculator = (base.ActualResolvingMode != DXBindingResolvingMode.LegacyStaticTyping) ? ((ICommandCalculator) new CommandCalculatorDynamic(this.TreeInfo, this.FallbackCanExecute)) : ((ICommandCalculator) new CommandCalculator(this.TreeInfo, this.FallbackCanExecute));
            this.Calculator.Init(base.TypeResolver);
        }

        private void SetBindingProperties(BindingBase binding, bool isRootBinding)
        {
            if (binding is Binding)
            {
                ((Binding) binding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            }
            else
            {
                ((MultiBinding) binding).UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            }
        }

        public string Execute { get; set; }

        public string CanExecute { get; set; }

        public bool FallbackCanExecute { get; set; }

        private CommandTreeInfo TreeInfo { get; set; }

        private ICommandCalculator Calculator { get; set; }

        private class Command : ICommand
        {
            private readonly WeakReference errorHandler;
            private readonly WeakReference calculator;
            private readonly WeakReference[] values;
            private readonly bool fallbackCanExecute;
            private readonly bool isEmpty;

            event EventHandler ICommand.CanExecuteChanged
            {
                add
                {
                    CommandManager.RequerySuggested += value;
                }
                remove
                {
                    CommandManager.RequerySuggested -= value;
                }
            }

            public Command(IErrorHandler errorHandler, ICommandCalculator calculator, bool fallbackCanExecute, bool isEmpty, object[] values)
            {
                this.errorHandler = new WeakReference(errorHandler);
                this.calculator = new WeakReference(calculator);
                this.fallbackCanExecute = fallbackCanExecute;
                this.isEmpty = isEmpty;
                Func<object, WeakReference> selector = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<object, WeakReference> local1 = <>c.<>9__13_0;
                    selector = <>c.<>9__13_0 = x => new WeakReference(x);
                }
                this.values = values.Select<object, WeakReference>(selector).ToArray<WeakReference>();
            }

            bool ICommand.CanExecute(object parameter)
            {
                if (!this.IsAlive)
                {
                    return this.fallbackCanExecute;
                }
                this.ErrorHandler.ClearError();
                return ((this.isEmpty || !DXBindingBase.DXBindingConverterBase.ValuesContainUnsetValue(this.Values)) ? this.Calculator.CanExecute(this.Values, parameter) : this.fallbackCanExecute);
            }

            void ICommand.Execute(object parameter)
            {
                if (this.IsAlive)
                {
                    this.ErrorHandler.ClearError();
                    if (this.isEmpty || !DXBindingBase.DXBindingConverterBase.ValuesContainUnsetValue(this.Values))
                    {
                        this.Calculator.Execute(this.Values, parameter);
                    }
                }
            }

            private IErrorHandler ErrorHandler =>
                (IErrorHandler) this.errorHandler.Target;

            private ICommandCalculator Calculator =>
                (ICommandCalculator) this.calculator.Target;

            private object[] Values
            {
                get
                {
                    if (this.isEmpty)
                    {
                        return null;
                    }
                    Func<WeakReference, object> selector = <>c.<>9__10_0;
                    if (<>c.<>9__10_0 == null)
                    {
                        Func<WeakReference, object> local1 = <>c.<>9__10_0;
                        selector = <>c.<>9__10_0 = x => x.Target;
                    }
                    return this.values.Select<WeakReference, object>(selector).ToArray<object>();
                }
            }

            private bool IsAlive
            {
                get
                {
                    bool flag = this.errorHandler.IsAlive && this.calculator.IsAlive;
                    if (this.isEmpty)
                    {
                        return flag;
                    }
                    if (!flag)
                    {
                        return false;
                    }
                    Func<WeakReference, bool> predicate = <>c.<>9__12_0;
                    if (<>c.<>9__12_0 == null)
                    {
                        Func<WeakReference, bool> local1 = <>c.<>9__12_0;
                        predicate = <>c.<>9__12_0 = x => !x.IsAlive;
                    }
                    return !this.values.Any<WeakReference>(predicate);
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DXCommandExtension.Command.<>c <>9 = new DXCommandExtension.Command.<>c();
                public static Func<WeakReference, object> <>9__10_0;
                public static Func<WeakReference, bool> <>9__12_0;
                public static Func<object, WeakReference> <>9__13_0;

                internal WeakReference <.ctor>b__13_0(object x) => 
                    new WeakReference(x);

                internal bool <get_IsAlive>b__12_0(WeakReference x) => 
                    !x.IsAlive;

                internal object <get_Values>b__10_0(WeakReference x) => 
                    x.Target;
            }
        }

        private class DXCommandConverter : DXBindingBase.DXBindingConverterBase
        {
            private readonly ICommandCalculator calculator;
            private readonly bool fallbackCanExecute;
            private readonly bool isEmpty;

            public DXCommandConverter(DXCommandExtension owner, bool isEmpty) : base(owner)
            {
                this.calculator = owner.Calculator;
                this.fallbackCanExecute = owner.FallbackCanExecute;
                this.isEmpty = isEmpty;
            }

            protected override bool CanConvert(object[] values) => 
                true;

            protected override object Convert(object[] values, Type targetType) => 
                new DXCommandExtension.Command(base.errorHandler, this.calculator, this.fallbackCanExecute, this.isEmpty, values);
        }
    }
}

