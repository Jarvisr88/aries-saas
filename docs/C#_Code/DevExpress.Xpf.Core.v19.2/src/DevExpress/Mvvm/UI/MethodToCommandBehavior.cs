namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class MethodToCommandBehavior : FunctionBindingBehaviorBase
    {
        protected const string Error_SourceFunctionShouldBeBool = "The return value of the '{0}.{1}' function should be bool.";
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CanExecuteFunctionProperty;
        public static readonly DependencyProperty MethodProperty;

        static MethodToCommandBehavior()
        {
            CommandProperty = DependencyProperty.Register("Command", typeof(string), typeof(MethodToCommandBehavior), new PropertyMetadata("Command", (d, e) => ((MethodToCommandBehavior) d).OnCommandChanged(e)));
            CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(string), typeof(MethodToCommandBehavior), new PropertyMetadata("CommandParameter", (d, e) => ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged()));
            CanExecuteFunctionProperty = DependencyProperty.Register("CanExecuteFunction", typeof(string), typeof(MethodToCommandBehavior), new PropertyMetadata(null, (d, e) => ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged()));
            MethodProperty = DependencyProperty.Register("Method", typeof(string), typeof(MethodToCommandBehavior), new PropertyMetadata(null, (d, e) => ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged()));
        }

        public MethodToCommandBehavior()
        {
            this.ResultCommand = new DelegateCommand<object>(new Action<object>(this.ExecuteCommand), new Func<object, bool>(this.CanExecuteCommand), false);
        }

        private bool CanExecuteCommand(object parameter)
        {
            if (!this.IsActive)
            {
                return false;
            }
            List<FunctionBindingBehaviorBase.ArgInfo> argsInfo = this.UnpackArgs(parameter);
            object obj2 = InvokeSourceFunction(base.ActualSource, this.ActualCanExecuteFunction, argsInfo, new Func<MethodInfo, Type, string, bool>(MethodToCommandBehavior.CanExecuteMethodInfoChecker));
            return ((obj2 == DependencyProperty.UnsetValue) || ((bool) obj2));
        }

        private static bool CanExecuteMethodInfoChecker(MethodInfo info, Type targetType, string functionName)
        {
            if ((info != null) && (info.ReturnType != typeof(bool)))
            {
                throw new ArgumentException($"The return value of the '{targetType.Name}.{info.Name}' function should be bool.");
            }
            return (info != null);
        }

        private void ExecuteCommand(object parameter)
        {
            if (this.IsActive)
            {
                List<FunctionBindingBehaviorBase.ArgInfo> argsInfo = this.UnpackArgs(parameter);
                InvokeSourceFunction(base.ActualSource, this.ActualFunction, argsInfo, new Func<MethodInfo, Type, string, bool>(MethodToCommandBehavior.ExecuteMethodInfoChecker));
            }
        }

        private static bool ExecuteMethodInfoChecker(MethodInfo info, Type targetType, string functionName)
        {
            if (info == null)
            {
                Trace.WriteLine(string.Format("FunctionBindingBehaviorBase error: Cannot find function with name {1} in the {0} class.", targetType.Name, functionName));
            }
            return (info != null);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.SetTargetProperty(base.ActualTarget, this.Command, this.ResultCommand, true);
        }

        private void OnCommandChanged(DependencyPropertyChangedEventArgs e)
        {
            bool? throwExceptionOnNotFound = null;
            this.SetTargetProperty(base.ActualTarget, (string) e.OldValue, null, throwExceptionOnNotFound);
            this.SetTargetProperty(base.ActualTarget, (string) e.NewValue, this.ResultCommand, true);
        }

        protected override void OnDetaching()
        {
            bool? throwExceptionOnNotFound = null;
            this.SetTargetProperty(base.ActualTarget, this.Command, null, throwExceptionOnNotFound);
            throwExceptionOnNotFound = null;
            this.SetTargetProperty(base.ActualTarget, this.CommandParameter, null, throwExceptionOnNotFound);
            base.OnDetaching();
        }

        protected override void OnResultAffectedPropertyChanged()
        {
            if (this.IsActive && !string.IsNullOrEmpty(this.CommandParameter))
            {
                this.SetTargetProperty(base.ActualTarget, this.CommandParameter, GetArgsInfo(this), true);
                ((IDelegateCommand) this.ResultCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            bool? throwExceptionOnNotFound = null;
            this.SetTargetProperty(e.OldValue, this.Command, null, throwExceptionOnNotFound);
            throwExceptionOnNotFound = null;
            this.SetTargetProperty(e.OldValue, this.CommandParameter, null, throwExceptionOnNotFound);
            this.SetTargetProperty(base.ActualTarget, this.Command, this.ResultCommand, true);
            base.OnTargetChanged(e);
        }

        private void SetTargetProperty(object target, string property, object value, bool? throwExceptionOnNotFound)
        {
            if ((target != null) && (base.IsAttached && !string.IsNullOrEmpty(property)))
            {
                GetObjectPropertySetter(target, property, throwExceptionOnNotFound).Do<Action<object>>(x => x(value));
            }
        }

        protected List<FunctionBindingBehaviorBase.ArgInfo> UnpackArgs(object parameter)
        {
            if (parameter == null)
            {
                return null;
            }
            if (!(parameter is IEnumerable))
            {
                List<FunctionBindingBehaviorBase.ArgInfo> list1 = new List<FunctionBindingBehaviorBase.ArgInfo>();
                list1.Add(new FunctionBindingBehaviorBase.ArgInfo(parameter));
                return list1;
            }
            Func<object, FunctionBindingBehaviorBase.ArgInfo> selector = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<object, FunctionBindingBehaviorBase.ArgInfo> local1 = <>c.<>9__30_0;
                selector = <>c.<>9__30_0 = x => (x is FunctionBindingBehaviorBase.ArgInfo) ? (x as FunctionBindingBehaviorBase.ArgInfo) : new FunctionBindingBehaviorBase.ArgInfo(x);
            }
            return ((IEnumerable) parameter).Cast<object>().Select<object, FunctionBindingBehaviorBase.ArgInfo>(selector).ToList<FunctionBindingBehaviorBase.ArgInfo>();
        }

        public string Command
        {
            get => 
                (string) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public string CommandParameter
        {
            get => 
                (string) base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        public string CanExecuteFunction
        {
            get => 
                (string) base.GetValue(CanExecuteFunctionProperty);
            set => 
                base.SetValue(CanExecuteFunctionProperty, value);
        }

        public string Method
        {
            get => 
                (string) base.GetValue(MethodProperty);
            set => 
                base.SetValue(MethodProperty, value);
        }

        protected ICommand ResultCommand { get; private set; }

        protected override string ActualFunction =>
            this.Method;

        private bool IsActive =>
            base.IsAttached && (!string.IsNullOrEmpty(this.Command) && ((base.ActualSource != null) && !string.IsNullOrEmpty(this.ActualFunction)));

        private string ActualCanExecuteFunction =>
            !string.IsNullOrEmpty(this.CanExecuteFunction) ? this.CanExecuteFunction : $"CanExecute{this.ActualFunction}";

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MethodToCommandBehavior.<>c <>9 = new MethodToCommandBehavior.<>c();
            public static Func<object, FunctionBindingBehaviorBase.ArgInfo> <>9__30_0;

            internal void <.cctor>b__39_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MethodToCommandBehavior) d).OnCommandChanged(e);
            }

            internal void <.cctor>b__39_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__39_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__39_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MethodToCommandBehavior) d).OnResultAffectedPropertyChanged();
            }

            internal FunctionBindingBehaviorBase.ArgInfo <UnpackArgs>b__30_0(object x) => 
                (x is FunctionBindingBehaviorBase.ArgInfo) ? (x as FunctionBindingBehaviorBase.ArgInfo) : new FunctionBindingBehaviorBase.ArgInfo(x);
        }
    }
}

