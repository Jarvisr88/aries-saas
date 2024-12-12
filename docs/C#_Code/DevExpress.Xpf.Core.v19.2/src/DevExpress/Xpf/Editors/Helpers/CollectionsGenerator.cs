namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionsGenerator
    {
        public static Type FindGenericType(Type sourceType)
        {
            Type type3;
            using (IEnumerator<Type> enumerator = GetTypeHierarchy(sourceType).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type[] interfaces = enumerator.Current.GetInterfaces();
                        Type collectionLikeGenericTypeFromInterfaces = GetCollectionLikeGenericTypeFromInterfaces(interfaces);
                        if (collectionLikeGenericTypeFromInterfaces == null)
                        {
                            continue;
                        }
                        type3 = collectionLikeGenericTypeFromInterfaces;
                    }
                    else
                    {
                        if (!sourceType.IsGenericType)
                        {
                            return null;
                        }
                        Type[] genericArguments = sourceType.GetGenericArguments();
                        return ((genericArguments.Length == 1) ? genericArguments[0] : null);
                    }
                    break;
                }
            }
            return type3;
        }

        public static IList Generate(Type type)
        {
            if (type.IsAssignableFrom(typeof(IEnumerable)))
            {
                return (IList) Activator.CreateInstance(type);
            }
            Type type2 = FindGenericType(type);
            if (type2 == null)
            {
                return new List<object>();
            }
            Type[] typeArguments = new Type[] { type2 };
            return (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(typeArguments));
        }

        private static Type GetCollectionLikeGenericTypeFromInterfaces(IEnumerable<Type> interfaces)
        {
            Type type2;
            using (IEnumerator<Type> enumerator = interfaces.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (!current.IsGenericType)
                        {
                            continue;
                        }
                        Type[] genericArguments = current.GetGenericArguments();
                        if ((genericArguments.Length > 1) || !(typeof(IEnumerable<>).MakeGenericType(genericArguments) == current))
                        {
                            continue;
                        }
                        type2 = genericArguments[0];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return type2;
        }

        private static IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            IList<Type> source = new List<Type>();
            for (Type type2 = type; type2.BaseType != null; type2 = type2.BaseType)
            {
                source.Add(type2);
            }
            return source.Reverse<Type>();
        }
    }
}

