namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    internal class DependencyPropertyRegistrator<OwnerType> where OwnerType: class
    {
        private static int lockCounter;

        public DependencyProperty AddOwner<T>(string name, DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null) => 
            property.AddOwner(typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));

        public void OverrideDefaultStyleKey(DependencyProperty propertyKey)
        {
            propertyKey.OverrideMetadata(typeof(OwnerType), new FrameworkPropertyMetadata(typeof(OwnerType)));
        }

        public void OverrideFrameworkMetadata<T>(DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property.OverrideMetadata(typeof(OwnerType), new FrameworkPropertyMetadata(defValue, changed, coerce));
        }

        public DependencyProperty OverrideFrameworkMetadata<T>(string name, DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            DependencyProperty property2 = property;
            property.OverrideMetadata(typeof(OwnerType), new FrameworkPropertyMetadata(defValue, changed, coerce));
            return property2;
        }

        public void OverrideMetadata<T>(DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property.OverrideMetadata(typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
        }

        public void OverrideMetadata<T>(DependencyPropertyKey propertyKey, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            propertyKey.OverrideMetadata(typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
        }

        public void OverrideMetadataNotDataBindable<T>(DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property.OverrideMetadata(typeof(OwnerType), new FrameworkPropertyMetadata(defValue, FrameworkPropertyMetadataOptions.NotDataBindable, changed, coerce));
        }

        public void OverrideUIMetadata<T>(DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property.OverrideMetadata(typeof(OwnerType), new UIPropertyMetadata(defValue, changed, coerce));
        }

        private static void PropertyChanged(DependencyObject dObj, DependencyProperty property, object value, CoerceValueCallback coerce)
        {
            if (DependencyPropertyRegistrator<OwnerType>.lockCounter <= 0)
            {
                DependencyPropertyRegistrator<OwnerType>.lockCounter++;
                object objA = coerce(dObj, value);
                if (!Equals(objA, value))
                {
                    dObj.SetValue(property, objA);
                }
                DependencyPropertyRegistrator<OwnerType>.lockCounter--;
            }
        }

        public void Register<T>(string name, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property = DependencyPropertyManager.Register(name, typeof(T), typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
        }

        public void Register<T>(string name, ref DependencyProperty property, T defValue, FrameworkPropertyMetadataOptions options, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property = DependencyPropertyManager.Register(name, typeof(T), typeof(OwnerType), new FrameworkPropertyMetadata(defValue, options, changed, coerce));
        }

        public void RegisterAttached<T>(string name, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property = DependencyPropertyManager.RegisterAttached(name, typeof(T), typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
        }

        public void RegisterAttached<T>(string name, ref DependencyProperty property, FrameworkPropertyMetadataOptions flags, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property = DependencyPropertyManager.RegisterAttached(name, typeof(T), typeof(OwnerType), new FrameworkPropertyMetadata(defValue, flags, changed, coerce));
        }

        public void RegisterAttachedInherited<T>(string name, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            property = DependencyPropertyManager.RegisterAttached(name, typeof(T), typeof(OwnerType), new FrameworkPropertyMetadata(defValue, FrameworkPropertyMetadataOptions.Inherits, changed, coerce));
        }

        public void RegisterAttachedReadonly<T>(string name, ref DependencyPropertyKey propertyKey, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            propertyKey = DependencyPropertyManager.RegisterAttachedReadOnly(name, typeof(T), typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
            property = propertyKey.DependencyProperty;
        }

        public void RegisterAttachedReadonlyInherited<T>(string name, ref DependencyPropertyKey propertyKey, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            propertyKey = DependencyPropertyManager.RegisterAttachedReadOnly(name, typeof(T), typeof(OwnerType), new FrameworkPropertyMetadata(defValue, FrameworkPropertyMetadataOptions.Inherits, changed, coerce));
            property = propertyKey.DependencyProperty;
        }

        public void RegisterClassCommandBinding(ICommand command, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecute)
        {
            CommandManager.RegisterClassCommandBinding(typeof(OwnerType), new CommandBinding(command, executed, canExecute));
        }

        public void RegisterDirectEvent<THandler>(string name, ref RoutedEvent routedEvent)
        {
            routedEvent = EventManager.RegisterRoutedEvent(name, RoutingStrategy.Direct, typeof(THandler), typeof(OwnerType));
        }

        public void RegisterReadonly<T>(string name, ref DependencyPropertyKey propertyKey, ref DependencyProperty property, T defValue = null, PropertyChangedCallback changed = null, CoerceValueCallback coerce = null)
        {
            propertyKey = DependencyPropertyManager.RegisterReadOnly(name, typeof(T), typeof(OwnerType), new PropertyMetadata(defValue, changed, coerce));
            property = propertyKey.DependencyProperty;
        }
    }
}

