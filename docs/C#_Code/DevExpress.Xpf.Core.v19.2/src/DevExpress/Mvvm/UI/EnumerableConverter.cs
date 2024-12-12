namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class EnumerableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable == null)
            {
                return null;
            }
            Type targetItemType = this.GetTargetItemType(targetType);
            Func<object, object> func = x => (this.ItemConverter == null) ? x : this.ItemConverter.Convert(x, targetItemType, parameter, culture);
            Type[] typeArguments = new Type[] { targetItemType };
            object[] args = new object[] { enumerable, func };
            IEnumerable enumerable2 = (IEnumerable) Activator.CreateInstance(typeof(EnumerableWrap<>).MakeGenericType(typeArguments), args);
            return (((targetType == null) || targetType.IsAssignableFrom(enumerable2.GetType())) ? enumerable2 : (!targetType.IsInterface ? ((!targetType.IsGenericType || !(targetType.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>))) ? this.CreateCollection(targetType, targetItemType, enumerable2) : this.CreateReadOnlyCollection(targetType, targetItemType, enumerable2)) : this.CreateList(targetType, targetItemType, enumerable2)));
        }

        private object CreateCollection(Type targetType, Type itemType, IEnumerable enumerable)
        {
            Type[] types = new Type[] { typeof(IEnumerable) };
            ConstructorInfo constructor = targetType.GetConstructor(types);
            if (constructor != null)
            {
                object[] objArray1 = new object[] { enumerable };
                return constructor.Invoke(objArray1);
            }
            Type[] typeArguments = new Type[] { itemType };
            Type[] typeArray3 = new Type[] { typeof(IEnumerable<>).MakeGenericType(typeArguments) };
            ConstructorInfo info2 = targetType.GetConstructor(typeArray3);
            if (info2 == null)
            {
                return this.CreateCollectionWithDefaultConstructor(targetType, itemType, enumerable);
            }
            object[] parameters = new object[] { enumerable };
            return info2.Invoke(parameters);
        }

        private object CreateCollectionWithDefaultConstructor(Type targetType, Type itemType, IEnumerable enumerable)
        {
            object obj2;
            MethodInfo method;
            try
            {
                obj2 = Activator.CreateInstance(targetType);
            }
            catch (MissingMethodException exception)
            {
                throw new NotSupportedCollectionException(targetType, null, exception);
            }
            IList list = obj2 as IList;
            if (list != null)
            {
                foreach (object obj3 in enumerable)
                {
                    list.Add(obj3);
                }
                return list;
            }
            Type[] typeArguments = new Type[] { itemType };
            Type genericListType = typeof(IList<>).MakeGenericType(typeArguments);
            if (targetType.GetInterfaces().Any<Type>(t => t == genericListType))
            {
                Type[] types = new Type[] { itemType };
                method = genericListType.GetMethod("Add", types);
            }
            else
            {
                Type[] types = new Type[] { itemType };
                method = targetType.GetMethod("Add", types);
                if (method == null)
                {
                    Func<MethodInfo, bool> predicate = <>c.<>9__13_1;
                    if (<>c.<>9__13_1 == null)
                    {
                        Func<MethodInfo, bool> local1 = <>c.<>9__13_1;
                        predicate = <>c.<>9__13_1 = m => m.GetParameters().Length == 1;
                    }
                    method = (from m in targetType.GetMethods().Where<MethodInfo>(predicate)
                        where m.GetParameters()[0].ParameterType.IsAssignableFrom(itemType)
                        select m).FirstOrDefault<MethodInfo>();
                }
            }
            if (method == null)
            {
                throw new NotSupportedCollectionException(targetType, null, null);
            }
            foreach (object obj4 in enumerable)
            {
                object[] parameters = new object[] { obj4 };
                method.Invoke(obj2, parameters);
            }
            return obj2;
        }

        private object CreateList(Type targetType, Type itemType, IEnumerable enumerable)
        {
            if ((targetType != null) && ((targetType == typeof(IEnumerable)) || (targetType.IsGenericType && (targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))))
            {
                return enumerable;
            }
            Type[] typeArguments = new Type[] { itemType };
            Type c = typeof(List<>).MakeGenericType(typeArguments);
            if ((targetType != null) && !targetType.IsAssignableFrom(c))
            {
                throw new NotSupportedCollectionException(targetType, null, null);
            }
            object[] args = new object[] { enumerable };
            return Activator.CreateInstance(c, args);
        }

        private object CreateReadOnlyCollection(Type targetType, Type itemType, IEnumerable enumerable)
        {
            object obj2 = this.CreateList(null, itemType, enumerable);
            return obj2.GetType().GetMethod("AsReadOnly").Invoke(obj2, new object[0]);
        }

        private Type GetTargetItemType(Type targetType)
        {
            if (this.TargetItemType != null)
            {
                return this.TargetItemType;
            }
            if (targetType == null)
            {
                throw new InvalidOperationException();
            }
            Type[] source = new Type[] { targetType };
            Func<Type, bool> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = t => t.IsInterface;
            }
            Func<Type, bool> func2 = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<Type, bool> local2 = <>c.<>9__9_1;
                func2 = <>c.<>9__9_1 = i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            }
            Func<Type, Type> selector = <>c.<>9__9_2;
            if (<>c.<>9__9_2 == null)
            {
                Func<Type, Type> local3 = <>c.<>9__9_2;
                selector = <>c.<>9__9_2 = i => i.GetGenericArguments()[0];
            }
            Type type = source.Where<Type>(predicate).Concat<Type>(targetType.GetInterfaces()).Where<Type>(func2).Select<Type, Type>(selector).FirstOrDefault<Type>();
            if (type == null)
            {
                throw new InvalidOperationException();
            }
            return type;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public Type TargetItemType { get; set; }

        public IValueConverter ItemConverter { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumerableConverter.<>c <>9 = new EnumerableConverter.<>c();
            public static Func<Type, bool> <>9__9_0;
            public static Func<Type, bool> <>9__9_1;
            public static Func<Type, Type> <>9__9_2;
            public static Func<MethodInfo, bool> <>9__13_1;

            internal bool <CreateCollectionWithDefaultConstructor>b__13_1(MethodInfo m) => 
                m.GetParameters().Length == 1;

            internal bool <GetTargetItemType>b__9_0(Type t) => 
                t.IsInterface;

            internal bool <GetTargetItemType>b__9_1(Type i) => 
                i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            internal Type <GetTargetItemType>b__9_2(Type i) => 
                i.GetGenericArguments()[0];
        }
    }
}

