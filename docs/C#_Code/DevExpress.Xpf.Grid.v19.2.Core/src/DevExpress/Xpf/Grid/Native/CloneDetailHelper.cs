namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Utils;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class CloneDetailHelper
    {
        private static readonly Dictionary<Type, PropertyDescriptorCollection> propertyCache = new Dictionary<Type, PropertyDescriptorCollection>();
        private static readonly Dictionary<Type, DependencyPropertyKey[]> propertyKeys = new Dictionary<Type, DependencyPropertyKey[]>();
        private static readonly HashSet<DependencyProperty> attachedProperties = new HashSet<DependencyProperty>();

        public static void CloneCollection<T>(IList source, IList destination) where T: DependencyObject
        {
            Func<T, T> clone = <>c__18<T>.<>9__18_0;
            if (<>c__18<T>.<>9__18_0 == null)
            {
                Func<T, T> local1 = <>c__18<T>.<>9__18_0;
                clone = <>c__18<T>.<>9__18_0 = item => CloneElement<T>(item, (Action<T>) null, (Func<T, Locker>) null, (object[]) null);
            }
            CloneCollectionCore<T>(source, destination, clone);
        }

        private static void CloneCollectionCore<T>(IList source, IList destination, Func<T, T> clone) where T: class
        {
            if (source.Count != 0)
            {
                ILockable lockable = destination as ILockable;
                if (lockable != null)
                {
                    lockable.BeginUpdate();
                }
                foreach (T local in source)
                {
                    destination.Add(clone(local));
                }
                if (lockable != null)
                {
                    lockable.EndUpdate();
                }
            }
        }

        public static void CloneElement<T>(T source, T destination, Action<T> innerCloneAction = null, Func<T, Locker> getCloneDetailLockerAction = null) where T: DependencyObject
        {
            Action action = () => CopyToElement<T>(source, destination, innerCloneAction);
            if (getCloneDetailLockerAction != null)
            {
                getCloneDetailLockerAction(destination).DoLockedAction(action);
            }
            else
            {
                action();
            }
        }

        public static T CloneElement<T>(T source, Action<T> innerCloneAction = null, Func<T, Locker> getCloneDetailLockerAction = null, object[] args = null) where T: DependencyObject
        {
            T destination = CreateElement<T>(source, args);
            CloneElement<T>(source, destination, innerCloneAction, getCloneDetailLockerAction);
            return destination;
        }

        public static void CloneSimpleCollection<T>(IList source, IList destination, object[] args = null) where T: class
        {
            CloneCollectionCore<T>(source, destination, item => CreateElement<T>(item, args));
        }

        private static object ConvertClonePropertyValue(DependencyObject sourceObject, string propertyName, object sourceValue, DependencyObject destinationObject)
        {
            IConvertClonePropertyValue value2 = sourceObject as IConvertClonePropertyValue;
            return ((value2 != null) ? value2.ConvertClonePropertyValue(propertyName, sourceValue, destinationObject) : sourceValue);
        }

        public static void CopyToCollection<T>(IList source, IList destination) where T: DependencyObject
        {
            if (destination.Count == 0)
            {
                CloneCollection<T>(source, destination);
            }
            else
            {
                if (source.Count != destination.Count)
                {
                    throw new ArgumentException("source.Count != destination.Count");
                }
                int count = source.Count;
                for (int i = 0; i < count; i++)
                {
                    CopyToElement<T>((T) source[i], (T) destination[i], null);
                }
            }
        }

        public static void CopyToElement<T>(T source, T destination, Action<T> innerCloneAction = null) where T: DependencyObject
        {
            ISupportInitialize initialize = destination as ISupportInitialize;
            if (initialize != null)
            {
                initialize.BeginInit();
            }
            foreach (PropertyDescriptor descriptor in GetCloneProperties<T>(destination, destination.GetType(), typeof(T)))
            {
                object objA = descriptor.GetValue(destination);
                object objB = descriptor.GetValue(source);
                if (!Equals(objA, objB))
                {
                    SetClonePropertyValue(source, descriptor, objB, destination);
                }
            }
            if (innerCloneAction != null)
            {
                innerCloneAction(destination);
            }
            if (initialize != null)
            {
                initialize.EndInit();
            }
        }

        public static T CreateElement<T>(T source, object[] args = null) where T: class => 
            ((IDetailElement<T>) source).CreateNewInstance(args);

        private static DependencyPropertyKey FindPropertyKey(Type type, PropertyDescriptor property)
        {
            DependencyPropertyKey key = FindPropertyKeyHelper(type, property);
            return (((key != null) || (type.BaseType == null)) ? key : FindPropertyKey(type.BaseType, property));
        }

        private static DependencyPropertyKey FindPropertyKeyHelper(Type type, PropertyDescriptor property) => 
            propertyKeys.ContainsKey(type) ? propertyKeys[type].FirstOrDefault<DependencyPropertyKey>(key => (key.DependencyProperty.GetName() == property.Name)) : null;

        internal static PropertyDescriptorCollection GetCloneProperties<T>(T component, Type componentType, Type baseComponentType)
        {
            PropertyDescriptorCollection descriptors;
            if (!propertyCache.TryGetValue(componentType, out descriptors))
            {
                List<PropertyDescriptor> list = new List<PropertyDescriptor>();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(componentType))
                {
                    PropertyDescriptor item = GetCopyProperty<T>(component, descriptor, componentType, baseComponentType);
                    if (item != null)
                    {
                        list.Add(item);
                    }
                }
                propertyCache[componentType] = descriptors = new PropertyDescriptorCollection(list.ToArray());
            }
            return descriptors;
        }

        private static PropertyDescriptor GetCopyProperty<T>(T component, PropertyDescriptor property, Type componentType, Type baseComponentType)
        {
            string name = property.Name;
            CloneDetailModeAttribute attribute = property.Attributes[typeof(CloneDetailModeAttribute)] as CloneDetailModeAttribute;
            if (attribute != null)
            {
                if (attribute.Mode == CloneDetailMode.Skip)
                {
                    return null;
                }
                if (attribute.Mode == CloneDetailMode.Force)
                {
                    return new ReadonlyDependencyPropertyDescriptor(property, FindPropertyKey(componentType, property) ?? FindPropertyKey(baseComponentType, property));
                }
            }
            return (!IsReadOnly<T>(component, property) ? ((property.Name != "Resources") ? (baseComponentType.IsAssignableFrom(property.ComponentType) ? property : null) : null) : null);
        }

        public static bool IsKnownAttachedProperty(DependencyProperty property) => 
            attachedProperties.Contains(property);

        private static bool IsReadOnly<T>(T component, PropertyDescriptor property) => 
            ((!NetVersionDetector.IsNetCore3() || (component == null)) || (component.GetType().GetProperty(property.Name, BindingFlags.Public | BindingFlags.Instance).GetSetMethod(false) != null)) ? property.IsReadOnly : true;

        public static void RegisterKnownAttachedProperty(DependencyProperty property)
        {
            attachedProperties.Add(property);
        }

        public static void RegisterKnownPropertyKeys(Type ownerType, params DependencyPropertyKey[] knownKeys)
        {
            propertyKeys.Add(ownerType, knownKeys);
        }

        public static T SafeGetDependentCollectionItem<T>(T item, ISupportGetCachedIndex<T> sourceCollection, IList destinationCollection) where T: DependencyObject
        {
            int cachedIndex = sourceCollection.GetCachedIndex(item);
            if (cachedIndex >= 0)
            {
                return (T) destinationCollection[cachedIndex];
            }
            return default(T);
        }

        public static void SetClonePropertyValue(DependencyObject sourceObject, PropertyDescriptor property, object sourceValue, DependencyObject destinationObject)
        {
            property.SetValue(destinationObject, ConvertClonePropertyValue(sourceObject, property.Name, sourceValue, destinationObject));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__18<T> where T: DependencyObject
        {
            public static readonly CloneDetailHelper.<>c__18<T> <>9;
            public static Func<T, T> <>9__18_0;

            static <>c__18()
            {
                CloneDetailHelper.<>c__18<T>.<>9 = new CloneDetailHelper.<>c__18<T>();
            }

            internal T <CloneCollection>b__18_0(T item) => 
                CloneDetailHelper.CloneElement<T>(item, (Action<T>) null, (Func<T, Locker>) null, (object[]) null);
        }

        public class ReadonlyDependencyPropertyDescriptor : PropertyDescriptor
        {
            private readonly PropertyDescriptor property;
            private readonly DependencyPropertyKey propertyKey;

            public ReadonlyDependencyPropertyDescriptor(PropertyDescriptor property, DependencyPropertyKey propertyKey) : base(property)
            {
                this.property = property;
                this.propertyKey = propertyKey;
            }

            public override bool CanResetValue(object component) => 
                this.property.CanResetValue(component);

            public override object GetValue(object component) => 
                this.property.GetValue(component);

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                ((DependencyObject) component).SetValue(this.propertyKey, value);
            }

            public override bool ShouldSerializeValue(object component)
            {
                throw new NotImplementedException();
            }

            public override Type ComponentType =>
                this.property.ComponentType;

            public override bool IsReadOnly =>
                false;

            public override Type PropertyType =>
                this.property.PropertyType;
        }
    }
}

