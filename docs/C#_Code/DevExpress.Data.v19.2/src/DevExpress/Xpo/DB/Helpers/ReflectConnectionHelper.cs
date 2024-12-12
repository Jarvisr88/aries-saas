namespace DevExpress.Xpo.DB.Helpers
{
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ReflectConnectionHelper
    {
        private readonly Type connectionType;
        private readonly ConstructorInfo connectionConstructor;
        private readonly ReadOnlyCollection<Type> exceptionTypes;
        private static object[] emptyList = new object[0];

        public ReflectConnectionHelper(IDbConnection connection, params string[] exceptionTypeNames)
        {
            this.connectionType = connection.GetType();
            this.connectionConstructor = this.connectionType.GetConstructor(new Type[0]);
            this.exceptionTypes = new ReadOnlyCollection<Type>((from name in exceptionTypeNames select this.GetType(name)).ToArray<Type>());
        }

        public static GetPropertyValueDelegate CreateGetPropertyDelegate(Type instanceType, string propertyName)
        {
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo property = instanceType.GetProperty(propertyName, bindingAttr);
            if ((property == null) || !property.CanRead)
            {
                throw new InvalidOperationException("Property not found");
            }
            Type[] typeArguments = new Type[] { instanceType, property.PropertyType };
            return ((GetHelperBase) Activator.CreateInstance(typeof(GetHelper).MakeGenericType(typeArguments))).CreateGetter(property.GetGetMethod());
        }

        public static object CreateInstance(Type objectType, params object[] parameters)
        {
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                types[i] = parameters[i].GetType();
            }
            ConstructorInfo constructor = objectType.GetConstructor(types);
            if (constructor == null)
            {
                throw new InvalidOperationException("Constructor not found.");
            }
            return constructor.Invoke(parameters);
        }

        public static void CreatePropertyDelegates(Type instanceType, string propertyName, out SetPropertyValueDelegate setProperty, out GetPropertyValueDelegate getProperty)
        {
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo property = instanceType.GetProperty(propertyName, bindingAttr);
            if ((property == null) || (!property.CanWrite || !property.CanRead))
            {
                throw new InvalidOperationException("Property not found");
            }
            Type[] typeArguments = new Type[] { instanceType, property.PropertyType };
            GetHelperBase base2 = (GetHelperBase) Activator.CreateInstance(typeof(GetHelper).MakeGenericType(typeArguments));
            setProperty = base2.CreateSetter(property.GetSetMethod());
            getProperty = base2.CreateGetter(property.GetGetMethod());
        }

        public static SetPropertyValueDelegate CreateSetPropertyDelegate(Type instanceType, string propertyName)
        {
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo property = instanceType.GetProperty(propertyName, bindingAttr);
            if ((property == null) || !property.CanWrite)
            {
                throw new InvalidOperationException("Property not found");
            }
            Type[] typeArguments = new Type[] { instanceType, property.PropertyType };
            return ((GetHelperBase) Activator.CreateInstance(typeof(GetHelper).MakeGenericType(typeArguments))).CreateSetter(property.GetSetMethod());
        }

        public static ExecMethodDelegate CreateStaticMethodDelegate(Type type, string methodName)
        {
            MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            if (method == null)
            {
                throw new InvalidOperationException("Method not found");
            }
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length != 1)
            {
                throw new InvalidOperationException("Method not found");
            }
            Type[] typeArguments = new Type[] { parameters[0].ParameterType };
            return ((GetMethodHelperBase) Activator.CreateInstance(typeof(GetMethodHelper).MakeGenericType(typeArguments))).CreateStaticMethodDelegate(method);
        }

        public static object GetCollectionFirstItem(IEnumerable collection)
        {
            IEnumerator enumerator = collection.GetEnumerator();
            return (!enumerator.MoveNext() ? null : enumerator.Current);
        }

        public static ExecMethodDelegate GetCommandBuilderDeriveParametersDelegate(string assemblyName, string typeName) => 
            CreateStaticMethodDelegate(GetTypeFromAssembly(assemblyName, typeName), "DeriveParameters");

        public IDbConnection GetConnection(string connectionString)
        {
            IDbConnection connection = (IDbConnection) this.connectionConstructor.Invoke(new object[0]);
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static IDbConnection GetConnection(string assemblyName, string typeName, bool throwException)
        {
            Type type = GetTypeFromAssembly(assemblyName, typeName, throwException);
            return ((type != null) ? ((IDbConnection) Activator.CreateInstance(type)) : null);
        }

        public static IDbConnection GetConnection(string assemblyName, string typeName, string connectionString) => 
            GetConnection(assemblyName, typeName, connectionString, true);

        public static IDbConnection GetConnection(string[] assemblyNames, string[] typeNames, bool throwException, out int assemblyFoundIndex)
        {
            Type type = GetTypeFromAssembly(assemblyNames, typeNames, throwException, out assemblyFoundIndex);
            return ((type == null) ? null : ((IDbConnection) Activator.CreateInstance(type)));
        }

        public static IDbConnection GetConnection(string assemblyName, string typeName, string connectionString, bool throwException)
        {
            IDbConnection connection = GetConnection(assemblyName, typeName, throwException);
            connection.ConnectionString = connectionString;
            return connection;
        }

        public static object GetPropertyValue(object instance, string propertyName) => 
            GetPropertyValue(instance, propertyName, false);

        public static object GetPropertyValue(object instance, string propertyName, bool declaredOnly)
        {
            Type type = instance.GetType();
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            if (declaredOnly)
            {
                bindingAttr |= BindingFlags.DeclaredOnly;
            }
            PropertyInfo property = type.GetProperty(propertyName, bindingAttr);
            if ((property != null) && property.CanRead)
            {
                MethodInfo getMethod = property.GetGetMethod();
                if (getMethod != null)
                {
                    return getMethod.Invoke(instance, emptyList);
                }
            }
            throw new InvalidOperationException($"Property '{propertyName}' not found");
        }

        public Type GetType(string typeName) => 
            this.connectionType.Assembly.GetType(typeName, true, false);

        public static Type GetTypeFromAssembly(string assemblyName, string typeName) => 
            GetTypeFromAssembly(assemblyName, typeName, true);

        public static Type GetTypeFromAssembly(string assemblyName, string typeName, bool throwException) => 
            XPTypeActivator.GetType(assemblyName, typeName, throwException);

        public static Type GetTypeFromAssembly(string[] assemblyNames, string[] typeNames, bool throwException, out int foundInAssemblyIndex)
        {
            Type type3;
            if (!throwException)
            {
                for (int i = 0; i < assemblyNames.Length; i++)
                {
                    Type type = GetTypeFromAssembly(assemblyNames[i], typeNames[i], false);
                    if (type != null)
                    {
                        foundInAssemblyIndex = i;
                        return type;
                    }
                }
                foundInAssemblyIndex = -1;
                return null;
            }
            Exception exception = null;
            int num = 0;
            int index = 0;
            while (true)
            {
                if (index >= assemblyNames.Length)
                {
                    if (exception == null)
                    {
                        foundInAssemblyIndex = -1;
                        return null;
                    }
                    if (num != assemblyNames.Length)
                    {
                        throw exception;
                    }
                    string str = string.Join("', '", assemblyNames.Distinct<string>());
                    Exception exception4 = new TypeLoadException($"None of the following assemblies found: '{str}'. At least one is required.") {
                        Data = { 
                            ["AssemblyFound"] = false,
                            ["AssemblyLoaded"] = false
                        }
                    };
                    throw exception4;
                }
                try
                {
                    Type type2 = GetTypeFromAssembly(assemblyNames[index], typeNames[index], true);
                    foundInAssemblyIndex = index;
                    type3 = type2;
                    break;
                }
                catch (TypeLoadException exception2)
                {
                    exception = exception2;
                    if (!exception2.Data.Contains("AssemblyFound") || ((bool) exception2.Data["AssemblyFound"]))
                    {
                        exception = exception2;
                    }
                    else
                    {
                        num++;
                        exception ??= exception2;
                    }
                }
                catch (Exception exception5)
                {
                    exception = exception5;
                }
                index++;
            }
            return type3;
        }

        public static object InvokeMethod(object instance, string methodName, object[] parameters, bool declaredOnly)
        {
            Type type = instance.GetType();
            return InvokeMethod(instance, type, methodName, parameters, declaredOnly);
        }

        public static object InvokeMethod(object instance, Type type, string methodName, object[] parameters, bool declaredOnly)
        {
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            if (declaredOnly)
            {
                bindingAttr |= BindingFlags.DeclaredOnly;
            }
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                types[i] = parameters[i].GetType();
            }
            MethodInfo info = type.GetMethod(methodName, bindingAttr, null, types, null);
            if (info == null)
            {
                throw new InvalidOperationException("Method not found");
            }
            return info.Invoke(instance, parameters);
        }

        public static object InvokeStaticMethod(Type type, string methodName, object[] parameters, bool declaredOnly)
        {
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static;
            if (declaredOnly)
            {
                bindingAttr |= BindingFlags.DeclaredOnly;
            }
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                types[i] = parameters[i].GetType();
            }
            MethodInfo info = type.GetMethod(methodName, bindingAttr, null, types, null);
            if (info == null)
            {
                throw new InvalidOperationException("Method not found");
            }
            return info.Invoke(null, parameters);
        }

        public static void SetPropertyValue(object instance, string propertyName, object value)
        {
            SetPropertyValue(instance, propertyName, value, false);
        }

        public static void SetPropertyValue(object instance, string propertyName, object value, bool declaredOnly)
        {
            Type type = instance.GetType();
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
            if (declaredOnly)
            {
                bindingAttr |= BindingFlags.DeclaredOnly;
            }
            PropertyInfo property = type.GetProperty(propertyName, bindingAttr);
            if ((property != null) && property.CanWrite)
            {
                MethodInfo setMethod = property.GetSetMethod();
                if (setMethod != null)
                {
                    object[] parameters = new object[] { value };
                    setMethod.Invoke(instance, parameters);
                    return;
                }
            }
            throw new InvalidOperationException($"Property '{propertyName}' not found");
        }

        public bool TryGetExceptionProperties(Exception e, string[] propertyNameList, out object[] values) => 
            this.TryGetExceptionProperties(e, propertyNameList, null, out values);

        public bool TryGetExceptionProperties(Exception e, string[] propertyNameList, bool[] declaredOnly, out object[] values)
        {
            Type c = e.GetType();
            using (IEnumerator<Type> enumerator = this.exceptionTypes.GetEnumerator())
            {
                bool flag;
                goto TR_0014;
            TR_0006:
                if (!flag)
                {
                    return true;
                }
            TR_0014:
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (!current.IsAssignableFrom(c))
                        {
                            continue;
                        }
                        flag = false;
                        values = new object[propertyNameList.Length];
                        for (int i = 0; i < propertyNameList.Length; i++)
                        {
                            PropertyInfo info = ((declaredOnly == null) || !declaredOnly[i]) ? c.GetProperty(propertyNameList[i], BindingFlags.Public | BindingFlags.Instance) : c.GetProperty(propertyNameList[i], BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                            if ((info != null) && info.CanRead)
                            {
                                MethodInfo getMethod = info.GetGetMethod();
                                if (getMethod == null)
                                {
                                    flag = true;
                                    break;
                                }
                                values[i] = getMethod.Invoke(e, emptyList);
                            }
                        }
                    }
                    else
                    {
                        goto TR_0003;
                    }
                    break;
                }
                goto TR_0006;
            }
        TR_0003:
            values = null;
            return false;
        }

        public bool TryGetExceptionProperty(Exception e, string propertyName, out object value) => 
            this.TryGetExceptionProperty(e, propertyName, false, out value);

        public bool TryGetExceptionProperty(Exception e, string propertyName, bool declaredOnly, out object value)
        {
            bool flag;
            Type type = e.GetType();
            using (IEnumerator<Type> enumerator = this.exceptionTypes.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (!type.IsAssignableFrom(current))
                        {
                            continue;
                        }
                        BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance;
                        if (declaredOnly)
                        {
                            bindingAttr |= BindingFlags.DeclaredOnly;
                        }
                        PropertyInfo property = type.GetProperty(propertyName, bindingAttr);
                        if ((property != null) && property.CanRead)
                        {
                            MethodInfo getMethod = property.GetGetMethod();
                            if (getMethod != null)
                            {
                                value = getMethod.Invoke(e, new object[0]);
                                flag = true;
                                break;
                            }
                        }
                        continue;
                    }
                    value = null;
                    return false;
                }
            }
            return flag;
        }

        public Type ConnectionType =>
            this.connectionType;

        public ReadOnlyCollection<Type> ExceptionTypes =>
            this.exceptionTypes;

        public delegate void CommandBuilderDeriveParametersHandler(IDbCommand command);

        private class GetHelper<T, V> : ReflectConnectionHelper.GetHelperBase
        {
            public override GetPropertyValueDelegate CreateGetter(MethodInfo mi)
            {
                GetPropertyValueTemplate<T, V> d = (GetPropertyValueTemplate<T, V>) mi.CreateDelegate(typeof(GetPropertyValueTemplate<T, V>), null);
                return (GetPropertyValueDelegate) (target => d((T) target));
            }

            public override SetPropertyValueDelegate CreateSetter(MethodInfo mi)
            {
                SetPropertyValueTemplate<T, V> d = (SetPropertyValueTemplate<T, V>) mi.CreateDelegate(typeof(SetPropertyValueTemplate<T, V>), null);
                return delegate (object target, object value) {
                    d((T) target, (V) value);
                };
            }

            private delegate V GetPropertyValueTemplate(T instance);

            private delegate void SetPropertyValueTemplate(T instance, V value);
        }

        private abstract class GetHelperBase
        {
            protected GetHelperBase()
            {
            }

            public abstract GetPropertyValueDelegate CreateGetter(MethodInfo mi);
            public abstract SetPropertyValueDelegate CreateSetter(MethodInfo mi);
        }

        private class GetMethodHelper<T> : ReflectConnectionHelper.GetMethodHelperBase
        {
            public override ExecMethodDelegate CreateStaticMethodDelegate(MethodInfo mi)
            {
                GetMethodDelegateTemplate<T> d = (GetMethodDelegateTemplate<T>) mi.CreateDelegate(typeof(GetMethodDelegateTemplate<T>));
                return delegate (object argument) {
                    d((T) argument);
                };
            }

            private delegate void GetMethodDelegateTemplate(T argument);
        }

        private abstract class GetMethodHelperBase
        {
            protected GetMethodHelperBase()
            {
            }

            public abstract ExecMethodDelegate CreateStaticMethodDelegate(MethodInfo mi);
        }
    }
}

