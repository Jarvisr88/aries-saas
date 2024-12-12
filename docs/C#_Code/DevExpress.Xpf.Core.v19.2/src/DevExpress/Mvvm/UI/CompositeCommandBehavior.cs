namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("Commands")]
    public class CompositeCommandBehavior : Behavior<DependencyObject>
    {
        public static readonly DependencyProperty CommandPropertyNameProperty;
        public static readonly DependencyProperty CanExecuteConditionProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty CanExecuteProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty InternalItemsProperty;

        static CompositeCommandBehavior()
        {
            Type ownerType = typeof(CompositeCommandBehavior);
            CommandPropertyNameProperty = DependencyProperty.Register("CommandPropertyName", typeof(string), ownerType, new PropertyMetadata("Command", (d, e) => ((CompositeCommandBehavior) d).OnCommandPropertyNameChanged(e)));
            CanExecuteProperty = DependencyProperty.Register("CanExecute", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((CompositeCommandBehavior) d).OnCanExecuteChanged(e)));
            InternalItemsProperty = DependencyProperty.RegisterAttached("InternalItems", typeof(CommandsCollection), ownerType, new PropertyMetadata(null));
            CanExecuteConditionProperty = DependencyProperty.Register("CanExecuteCondition", typeof(CompositeCommandExecuteCondition), ownerType, new PropertyMetadata(CompositeCommandExecuteCondition.AllCommandsCanBeExecuted, (d, e) => ((CompositeCommandBehavior) d).UpdateCanExecuteBinding()));
        }

        public CompositeCommandBehavior()
        {
            this.CompositeCommand = new DelegateCommand<object>(new Action<object>(this.CompositeCommandExecute), new Func<object, bool>(this.CompositeCommandCanExecute), false);
            this.Commands = new CommandsCollection();
            this.Commands.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCommandsChanged);
        }

        private bool CompositeCommandCanExecute(object parameter) => 
            this.CanExecute;

        private void CompositeCommandExecute(object parameter)
        {
            if (this.CanExecute)
            {
                foreach (CommandItem item in this.Commands)
                {
                    item.ExecuteCommand();
                }
            }
        }

        private DependencyProperty GetCommandDependencyProperty(string propName) => 
            ObjectPropertyHelper.GetDependencyProperty(base.AssociatedObject, propName);

        private PropertyInfo GetCommandProperty(string propName) => 
            ObjectPropertyHelper.GetPropertyInfoSetter(base.AssociatedObject, propName);

        protected override void OnAttached()
        {
            base.OnAttached();
            this.SetAssociatedObjectCommandProperty(this.CommandPropertyName);
            base.AssociatedObject.SetValue(InternalItemsProperty, this.Commands);
        }

        private void OnCanExecuteChanged(DependencyPropertyChangedEventArgs e)
        {
            this.RaiseCompositeCommandCanExecuteChanged();
        }

        private void OnCommandPropertyNameChanged(DependencyPropertyChangedEventArgs e)
        {
            if (base.IsAttached)
            {
                this.ReleaseAssociatedObjectCommandProperty(e.OldValue as string);
                this.SetAssociatedObjectCommandProperty(e.NewValue as string);
            }
        }

        private void OnCommandsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateCanExecuteBinding();
        }

        protected override void OnDetaching()
        {
            this.ReleaseAssociatedObjectCommandProperty(this.CommandPropertyName);
            base.AssociatedObject.SetValue(InternalItemsProperty, null);
            base.OnDetaching();
        }

        private void RaiseCompositeCommandCanExecuteChanged()
        {
            this.CompositeCommand.RaiseCanExecuteChanged();
        }

        private void ReleaseAssociatedObjectCommandProperty(string propName)
        {
            PropertyInfo commandProperty = this.GetCommandProperty(propName);
            if (commandProperty != null)
            {
                if (commandProperty.GetValue(base.AssociatedObject, null) == this.CompositeCommand)
                {
                    commandProperty.SetValue(base.AssociatedObject, null, null);
                }
            }
            else
            {
                DependencyProperty commandDependencyProperty = this.GetCommandDependencyProperty(propName);
                if ((commandDependencyProperty != null) && (base.AssociatedObject.GetValue(commandDependencyProperty) == this.CompositeCommand))
                {
                    base.AssociatedObject.SetValue(commandDependencyProperty, null);
                }
            }
        }

        private void SetAssociatedObjectCommandProperty(string propName)
        {
            PropertyInfo commandProperty = this.GetCommandProperty(propName);
            if (commandProperty != null)
            {
                commandProperty.SetValue(base.AssociatedObject, this.CompositeCommand, null);
            }
            else
            {
                this.GetCommandDependencyProperty(propName).Do<DependencyProperty>(x => base.AssociatedObject.SetValue(x, this.CompositeCommand));
            }
        }

        private void UpdateCanExecuteBinding()
        {
            MultiBinding binding1 = new MultiBinding();
            binding1.Converter = new CompositeCommandCanExecuteConverter(this.CanExecuteCondition);
            MultiBinding binding = binding1;
            for (int i = 0; i < this.Commands.Count; i++)
            {
                Binding item = new Binding("CanExecute");
                item.Mode = BindingMode.OneWay;
                item.Source = this.Commands[i];
                binding.Bindings.Add(item);
            }
            BindingOperations.SetBinding(this, CanExecuteProperty, binding);
        }

        public string CommandPropertyName
        {
            get => 
                (string) base.GetValue(CommandPropertyNameProperty);
            set => 
                base.SetValue(CommandPropertyNameProperty, value);
        }

        private bool CanExecute
        {
            get => 
                (bool) base.GetValue(CanExecuteProperty);
            set => 
                base.SetValue(CanExecuteProperty, value);
        }

        public CompositeCommandExecuteCondition CanExecuteCondition
        {
            get => 
                (CompositeCommandExecuteCondition) base.GetValue(CanExecuteConditionProperty);
            set => 
                base.SetValue(CanExecuteConditionProperty, value);
        }

        public DelegateCommand<object> CompositeCommand { get; private set; }

        public CommandsCollection Commands { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CompositeCommandBehavior.<>c <>9 = new CompositeCommandBehavior.<>c();

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CompositeCommandBehavior) d).OnCommandPropertyNameChanged(e);
            }

            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CompositeCommandBehavior) d).OnCanExecuteChanged(e);
            }

            internal void <.cctor>b__5_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CompositeCommandBehavior) d).UpdateCanExecuteBinding();
            }
        }

        private class CompositeCommandCanExecuteConverter : IMultiValueConverter
        {
            private readonly CompositeCommandExecuteCondition canExecuteCondition;

            public CompositeCommandCanExecuteConverter(CompositeCommandExecuteCondition canExecuteCondition)
            {
                this.canExecuteCondition = canExecuteCondition;
            }

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                if ((values == null) || (values.Length == 0))
                {
                    return false;
                }
                if (this.canExecuteCondition == CompositeCommandExecuteCondition.AllCommandsCanBeExecuted)
                {
                    Func<bool, bool> func1 = <>c.<>9__2_0;
                    if (<>c.<>9__2_0 == null)
                    {
                        Func<bool, bool> local1 = <>c.<>9__2_0;
                        func1 = <>c.<>9__2_0 = x => x;
                    }
                    return values.Cast<bool>().All<bool>(func1);
                }
                if (this.canExecuteCondition != CompositeCommandExecuteCondition.AnyCommandCanBeExecuted)
                {
                    throw new InvalidOperationException();
                }
                Func<bool, bool> predicate = <>c.<>9__2_1;
                if (<>c.<>9__2_1 == null)
                {
                    Func<bool, bool> local2 = <>c.<>9__2_1;
                    predicate = <>c.<>9__2_1 = x => x;
                }
                return values.Cast<bool>().Any<bool>(predicate);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CompositeCommandBehavior.CompositeCommandCanExecuteConverter.<>c <>9 = new CompositeCommandBehavior.CompositeCommandCanExecuteConverter.<>c();
                public static Func<bool, bool> <>9__2_0;
                public static Func<bool, bool> <>9__2_1;

                internal bool <Convert>b__2_0(bool x) => 
                    x;

                internal bool <Convert>b__2_1(bool x) => 
                    x;
            }
        }
    }
}

