namespace DevExpress.Office.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Data;

    public class DependencyPropertyChangeHandler : DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty TargetPropertyProperty = CreateTargetPropertyProperty();
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty HandlersProperty = CreateHandlersProperty();
        private bool isHandlingLocked;
        private readonly Action action;

        protected DependencyPropertyChangeHandler(Action action)
        {
            this.action = action;
        }

        public static void AddHandler(DependencyObject element, string property, Action action)
        {
            Binding binding = new Binding(property) {
                Source = element
            };
            DependencyPropertyChangeHandler target = new DependencyPropertyChangeHandler(action);
            HandlersTable table = element.GetValue(HandlersProperty) as HandlersTable;
            if (table == null)
            {
                table = new HandlersTable();
                element.SetValue(HandlersProperty, table);
            }
            table[property] = target;
            target.LockHandling();
            BindingOperations.SetBinding(target, TargetPropertyProperty, binding);
            target.UnlockHandling();
        }

        public static void AddHandler(DependencyObject element, DependencyProperty attachedProperty, Action action)
        {
            Binding binding = new Binding();
            object[] pathParameters = new object[] { attachedProperty };
            binding.Path = new PropertyPath("(0)", pathParameters);
            binding.Source = element;
            DependencyPropertyChangeHandler target = new DependencyPropertyChangeHandler(action);
            HandlersTable table = element.GetValue(HandlersProperty) as HandlersTable;
            if (table == null)
            {
                table = new HandlersTable();
                element.SetValue(HandlersProperty, table);
            }
            table[GetPropertyName(attachedProperty)] = target;
            target.LockHandling();
            BindingOperations.SetBinding(target, TargetPropertyProperty, binding);
            target.UnlockHandling();
        }

        private static DependencyProperty CreateHandlersProperty() => 
            DependencyProperty.RegisterAttached("Handlers", typeof(HandlersTable), typeof(DependencyPropertyChangeHandler), new PropertyMetadata(null));

        private static DependencyProperty CreateTargetPropertyProperty() => 
            DependencyProperty.Register("TargetProperty", typeof(object), typeof(DependencyPropertyChangeHandler), new PropertyMetadata(null, new PropertyChangedCallback(DependencyPropertyChangeHandler.OnTargetPropertyChanged)));

        private static string GetPropertyName(DependencyProperty property) => 
            $"{property.OwnerType.Name}.{property.Name}";

        private void LockHandling()
        {
            this.isHandlingLocked = true;
        }

        private void OnTargetPropertyChanged()
        {
            if (!this.isHandlingLocked)
            {
                this.action();
            }
        }

        private static void OnTargetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyPropertyChangeHandler handler = d as DependencyPropertyChangeHandler;
            if (handler != null)
            {
                handler.OnTargetPropertyChanged();
            }
        }

        public static void RemoveHandler(DependencyObject element, string property)
        {
            HandlersTable table = element.GetValue(HandlersProperty) as HandlersTable;
            if ((table != null) && table.ContainsKey(property))
            {
                DependencyPropertyChangeHandler handler = table[property];
                handler.LockHandling();
                handler.ClearValue(TargetPropertyProperty);
                handler.UnlockHandling();
                table.Remove(property);
            }
        }

        public static void RemoveHandler(DependencyObject element, DependencyProperty attachedProperty)
        {
            string propertyName = GetPropertyName(attachedProperty);
            RemoveHandler(element, propertyName);
        }

        private void UnlockHandling()
        {
            this.isHandlingLocked = false;
        }

        private class HandlersTable : Dictionary<string, DependencyPropertyChangeHandler>
        {
        }
    }
}

