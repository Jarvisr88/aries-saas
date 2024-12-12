namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DependencyPropertyHelper2
    {
        private static volatile bool methodsCreated = false;
        private static readonly object lockObject = new object();
        private static Func<object, object> getSynchronized;
        private static Func<object, object> get_metadataMap;
        private static Action<object, int, object> set_Item;
        private static Func<object, int, object> get_Item;
        private static Action<object, object> set_propertyChangedCallback;
        private static Action<object, object> set_coerceValueCallback;

        private static void AddCallback(DependencyPropertyKey key, DependencyProperty property, Type ownerType, Delegate callback, Action<object, object> set_Callback, Func<PropertyMetadata, Delegate> get_Callback)
        {
            if (key != null)
            {
                property = key.DependencyProperty;
            }
            if (!property.IsMetadataOverriden(ownerType))
            {
                FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
                set_Callback(metadata, callback);
                if (key != null)
                {
                    key.OverrideMetadata(ownerType, metadata);
                }
                else
                {
                    property.OverrideMetadata(ownerType, metadata);
                }
            }
            else
            {
                PropertyMetadata arg = property.GetMetadata(ownerType) ?? property.DefaultMetadata;
                PropertyMetadata metadata3 = arg;
                lock (metadata3)
                {
                    Delegate delegate2 = get_Callback(arg);
                    set_Callback(arg, (delegate2 == null) ? callback : (delegate2 + callback));
                }
            }
        }

        public static void AddCoerceValueCallback(this DependencyProperty property, Type ownerType, CoerceValueCallback callback)
        {
            CheckMethodsCreated(property);
            Func<PropertyMetadata, Delegate> func1 = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<PropertyMetadata, Delegate> local1 = <>c.<>9__13_0;
                func1 = <>c.<>9__13_0 = x => x.CoerceValueCallback;
            }
            AddCallback(null, property, ownerType, callback, set_coerceValueCallback, func1);
        }

        public static void AddCoerceValueCallback(this DependencyPropertyKey property, Type ownerType, CoerceValueCallback callback)
        {
            CheckMethodsCreated(property.DependencyProperty);
            Func<PropertyMetadata, Delegate> func1 = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<PropertyMetadata, Delegate> local1 = <>c.<>9__14_0;
                func1 = <>c.<>9__14_0 = x => x.CoerceValueCallback;
            }
            AddCallback(property, null, ownerType, callback, set_coerceValueCallback, func1);
        }

        public static void AddPropertyChangedCallback(this DependencyProperty property, Type ownerType, PropertyChangedCallback callback)
        {
            CheckMethodsCreated(property);
            Func<PropertyMetadata, Delegate> func1 = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<PropertyMetadata, Delegate> local1 = <>c.<>9__11_0;
                func1 = <>c.<>9__11_0 = x => x.PropertyChangedCallback;
            }
            AddCallback(null, property, ownerType, callback, set_propertyChangedCallback, func1);
        }

        public static void AddPropertyChangedCallback(this DependencyPropertyKey property, Type ownerType, PropertyChangedCallback callback)
        {
            CheckMethodsCreated(property.DependencyProperty);
            Func<PropertyMetadata, Delegate> func1 = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<PropertyMetadata, Delegate> local1 = <>c.<>9__12_0;
                func1 = <>c.<>9__12_0 = x => x.PropertyChangedCallback;
            }
            AddCallback(property, null, ownerType, callback, set_propertyChangedCallback, func1);
        }

        private static void CheckMethodsCreated(DependencyProperty property)
        {
            if (!methodsCreated)
            {
                object lockObject = DependencyPropertyHelper2.lockObject;
                lock (lockObject)
                {
                    if (!methodsCreated)
                    {
                        Type declaringType = typeof(DependencyProperty);
                        getSynchronized = ReflectionHelper.CreateFieldGetter<object, object>(declaringType, "Synchronized", BindingFlags.NonPublic | BindingFlags.Static);
                        get_metadataMap = ReflectionHelper.CreateFieldGetter<object, object>(declaringType, "_metadataMap", BindingFlags.NonPublic | BindingFlags.Instance);
                        object instance = get_metadataMap(property);
                        int? parametersCount = null;
                        set_Item = ReflectionHelper.CreateInstanceMethodHandler<Action<object, int, object>>(instance, "set_Item", BindingFlags.Public | BindingFlags.Instance, instance.GetType(), parametersCount, null, true);
                        parametersCount = null;
                        get_Item = ReflectionHelper.CreateInstanceMethodHandler<Func<object, int, object>>(instance, "get_Item", BindingFlags.Public | BindingFlags.Instance, instance.GetType(), parametersCount, null, true);
                        set_propertyChangedCallback = ReflectionHelper.CreateFieldSetter<object, object>(typeof(PropertyMetadata), "_propertyChangedCallback", BindingFlags.NonPublic | BindingFlags.Instance);
                        set_coerceValueCallback = ReflectionHelper.CreateFieldSetter<object, object>(typeof(PropertyMetadata), "_coerceValueCallback", BindingFlags.NonPublic | BindingFlags.Instance);
                        methodsCreated = true;
                    }
                }
            }
        }

        public static bool IsMetadataOverriden(this DependencyProperty property, Type ownerType)
        {
            CheckMethodsCreated(property);
            object obj2 = getSynchronized(null);
            lock (obj2)
            {
                return (get_Item(get_metadataMap(property), DependencyObjectType.FromSystemType(ownerType).Id) != DependencyProperty.UnsetValue);
            }
        }

        public static void UnOverrideMetadata(this DependencyProperty property, Type ownerType)
        {
            CheckMethodsCreated(property);
            object obj2 = getSynchronized(null);
            lock (obj2)
            {
                set_Item(get_metadataMap(property), DependencyObjectType.FromSystemType(ownerType).Id, DependencyProperty.UnsetValue);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DependencyPropertyHelper2.<>c <>9 = new DependencyPropertyHelper2.<>c();
            public static Func<PropertyMetadata, Delegate> <>9__11_0;
            public static Func<PropertyMetadata, Delegate> <>9__12_0;
            public static Func<PropertyMetadata, Delegate> <>9__13_0;
            public static Func<PropertyMetadata, Delegate> <>9__14_0;

            internal Delegate <AddCoerceValueCallback>b__13_0(PropertyMetadata x) => 
                x.CoerceValueCallback;

            internal Delegate <AddCoerceValueCallback>b__14_0(PropertyMetadata x) => 
                x.CoerceValueCallback;

            internal Delegate <AddPropertyChangedCallback>b__11_0(PropertyMetadata x) => 
                x.PropertyChangedCallback;

            internal Delegate <AddPropertyChangedCallback>b__12_0(PropertyMetadata x) => 
                x.PropertyChangedCallback;
        }
    }
}

