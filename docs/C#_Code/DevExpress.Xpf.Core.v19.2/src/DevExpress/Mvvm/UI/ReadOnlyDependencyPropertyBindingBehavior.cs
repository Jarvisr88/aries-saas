namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class ReadOnlyDependencyPropertyBindingBehavior : Behavior<DependencyObject>
    {
        public static readonly System.Windows.DependencyProperty IsEnabledProperty;
        public static readonly System.Windows.DependencyProperty PropertyProperty;
        public static readonly System.Windows.DependencyProperty DependencyPropertyProperty;
        public static readonly System.Windows.DependencyProperty BindingProperty;
        public static readonly System.Windows.DependencyProperty CommandProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly System.Windows.DependencyProperty PropertyValueProperty;
        private bool lockBindingChanged;

        static ReadOnlyDependencyPropertyBindingBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior> registrator1 = DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior>.New().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_IsEnabled)), parameters), out IsEnabledProperty, true, (x, oldValue, newValue) => x.OnIsEnabledChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior> registrator2 = registrator1.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_Property)), expressionArray2), out PropertyProperty, null, (x, oldValue, newValue) => x.UpdateBinding(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior> registrator3 = registrator2.Register<System.Windows.DependencyProperty>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, System.Windows.DependencyProperty>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_DependencyProperty)), expressionArray3), out DependencyPropertyProperty, null, (x, oldValue, newValue) => x.UpdateBinding(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior> registrator4 = registrator3.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_Binding)), expressionArray4), out BindingProperty, null, (x, oldValue, newValue) => x.OnBindingChanged(oldValue, newValue), 0x100);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<ReadOnlyDependencyPropertyBindingBehavior> registrator5 = registrator4.Register<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_Command)), expressionArray5), out CommandProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(ReadOnlyDependencyPropertyBindingBehavior), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator5.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<ReadOnlyDependencyPropertyBindingBehavior, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ReadOnlyDependencyPropertyBindingBehavior.get_PropertyValue)), expressionArray6), out PropertyValueProperty, null, (x, oldValue, newValue) => x.OnPropertyValueChanged(oldValue, newValue), frameworkOptions);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.UpdateBinding();
        }

        private void OnBindingChanged(object oldValue, object newValue)
        {
            if (!this.lockBindingChanged)
            {
                base.Dispatcher.BeginInvoke(() => this.OnPropertyValueChanged(this.PropertyValue, this.PropertyValue), new object[0]);
            }
        }

        protected override void OnDetaching()
        {
            BindingOperations.ClearBinding(this, PropertyValueProperty);
            base.OnDetaching();
        }

        private void OnIsEnabledChanged(bool oldValue, bool newValue)
        {
            this.OnPropertyValueChanged(this.PropertyValue, this.PropertyValue);
        }

        private void OnPropertyValueChanged(object oldValue, object newValue)
        {
            if (this.IsEnabled && !Equals(this.Binding, newValue))
            {
                this.lockBindingChanged = true;
                base.SetCurrentValue(BindingProperty, newValue);
                this.lockBindingChanged = false;
                if ((this.Command != null) && this.Command.CanExecute(newValue))
                {
                    this.Command.Execute(newValue);
                }
            }
        }

        private void UpdateBinding()
        {
            if (base.AssociatedObject != null)
            {
                if (this.DependencyProperty != null)
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
                    binding.Path = new PropertyPath(this.DependencyProperty);
                    binding.Source = base.AssociatedObject;
                    BindingOperations.SetBinding(this, PropertyValueProperty, binding);
                }
                else if (!string.IsNullOrEmpty(this.Property))
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(this.Property);
                    binding.Source = base.AssociatedObject;
                    BindingOperations.SetBinding(this, PropertyValueProperty, binding);
                }
            }
        }

        public bool IsEnabled
        {
            get => 
                (bool) base.GetValue(IsEnabledProperty);
            set => 
                base.SetValue(IsEnabledProperty, value);
        }

        public string Property
        {
            get => 
                (string) base.GetValue(PropertyProperty);
            set => 
                base.SetValue(PropertyProperty, value);
        }

        public System.Windows.DependencyProperty DependencyProperty
        {
            get => 
                (System.Windows.DependencyProperty) base.GetValue(DependencyPropertyProperty);
            set => 
                base.SetValue(DependencyPropertyProperty, value);
        }

        public object Binding
        {
            get => 
                base.GetValue(BindingProperty);
            set => 
                base.SetValue(BindingProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        private object PropertyValue
        {
            get => 
                base.GetValue(PropertyValueProperty);
            set => 
                base.SetValue(PropertyValueProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReadOnlyDependencyPropertyBindingBehavior.<>c <>9 = new ReadOnlyDependencyPropertyBindingBehavior.<>c();

            internal void <.cctor>b__6_0(ReadOnlyDependencyPropertyBindingBehavior x, bool oldValue, bool newValue)
            {
                x.OnIsEnabledChanged(oldValue, newValue);
            }

            internal void <.cctor>b__6_1(ReadOnlyDependencyPropertyBindingBehavior x, string oldValue, string newValue)
            {
                x.UpdateBinding();
            }

            internal void <.cctor>b__6_2(ReadOnlyDependencyPropertyBindingBehavior x, DependencyProperty oldValue, DependencyProperty newValue)
            {
                x.UpdateBinding();
            }

            internal void <.cctor>b__6_3(ReadOnlyDependencyPropertyBindingBehavior x, object oldValue, object newValue)
            {
                x.OnBindingChanged(oldValue, newValue);
            }

            internal void <.cctor>b__6_4(ReadOnlyDependencyPropertyBindingBehavior x, object oldValue, object newValue)
            {
                x.OnPropertyValueChanged(oldValue, newValue);
            }
        }
    }
}

