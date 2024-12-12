namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class DelegateCommandFactory
    {
        public static DelegateCommand Create(Action executeMethod) => 
            new DelegateCommand(executeMethod);

        public static DelegateCommand<T> Create<T>(Action<T> executeMethod) => 
            new DelegateCommand<T>(executeMethod);

        public static DelegateCommand Create(Action executeMethod, bool useCommandManager) => 
            new DelegateCommand(executeMethod, useCommandManager);

        public static DelegateCommand Create(Action executeMethod, Func<bool> canExecuteMethod) => 
            new DelegateCommand(executeMethod, canExecuteMethod, null);

        public static DelegateCommand<T> Create<T>(Action<T> executeMethod, bool useCommandManager) => 
            new DelegateCommand<T>(executeMethod, useCommandManager);

        public static DelegateCommand<T> Create<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod) => 
            new DelegateCommand<T>(executeMethod, canExecuteMethod, null);

        public static DelegateCommand Create(Action executeMethod, Func<bool> canExecuteMethod, bool useCommandManager) => 
            new DelegateCommand(executeMethod, canExecuteMethod, new bool?(useCommandManager));

        public static DelegateCommand<T> Create<T>(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool useCommandManager) => 
            new DelegateCommand<T>(executeMethod, canExecuteMethod, new bool?(useCommandManager));

        public static DelegateCommand CreateFromFunction<TResult>(Func<TResult> executeMethod, Func<bool> canExecuteMethod) => 
            new DelegateCommand(delegate {
                executeMethod();
            }, canExecuteMethod, null);

        public static DelegateCommand<T> CreateFromFunction<T, TResult>(Func<T, TResult> executeMethod, Func<T, bool> canExecuteMethod) => 
            new DelegateCommand<T>(delegate (T x) {
                executeMethod(x);
            }, canExecuteMethod, null);

        public static DelegateCommand CreateFromFunction<TResult>(Func<TResult> executeMethod, Func<bool> canExecuteMethod, bool useCommandManager) => 
            new DelegateCommand(delegate {
                executeMethod();
            }, canExecuteMethod, new bool?(useCommandManager));

        public static DelegateCommand<T> CreateFromFunction<T, TResult>(Func<T, TResult> executeMethod, Func<T, bool> canExecuteMethod, bool useCommandManager) => 
            new DelegateCommand<T>(delegate (T x) {
                executeMethod(x);
            }, canExecuteMethod, new bool?(useCommandManager));

        internal static MethodInfo GetGenericMethodWithoutResult(Type parameterType, bool withUseCommandManagerParameter)
        {
            Type[] typeArray4;
            if (!withUseCommandManagerParameter)
            {
                typeArray4 = new Type[] { typeof(Action<>), typeof(Func<,>) };
            }
            else
            {
                typeArray4 = new Type[] { typeof(Action<>), typeof(Func<,>), typeof(bool) };
            }
            Type[] typeArguments = new Type[] { parameterType };
            return GetMethodByParameter("Create", typeArray4).MakeGenericMethod(typeArguments);
        }

        internal static MethodInfo GetGenericMethodWithResult(Type parameterType1, Type parameterType2, bool withUseCommandManagerParameter)
        {
            Type[] typeArray4;
            if (!withUseCommandManagerParameter)
            {
                typeArray4 = new Type[] { typeof(Func<,>), typeof(Func<,>) };
            }
            else
            {
                typeArray4 = new Type[] { typeof(Func<,>), typeof(Func<,>), typeof(bool) };
            }
            Type[] typeArguments = new Type[] { parameterType1, parameterType2 };
            return GetMethodByParameter("CreateFromFunction", typeArray4).MakeGenericMethod(typeArguments);
        }

        private static MethodInfo GetMethodByParameter(string methodName, Type[] parameterTypes)
        {
            MethodInfo info2;
            using (IEnumerator<MethodInfo> enumerator = (from m in typeof(DelegateCommandFactory).GetMethods()
                where m.Name == methodName
                select m).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        MethodInfo current = enumerator.Current;
                        ParameterInfo[] parameters = current.GetParameters();
                        if (parameters.Length != parameterTypes.Length)
                        {
                            continue;
                        }
                        bool flag = true;
                        int index = 0;
                        while (true)
                        {
                            if (index < parameters.Length)
                            {
                                if (parameters[index].ParameterType.Name == parameterTypes[index].Name)
                                {
                                    index++;
                                    continue;
                                }
                                flag = false;
                            }
                            if (!flag)
                            {
                                break;
                            }
                            return current;
                        }
                        continue;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return info2;
        }

        internal static MethodInfo GetSimpleMethodWithoutResult(bool withUseCommandManagerParameter)
        {
            Type[] typeArray3;
            if (!withUseCommandManagerParameter)
            {
                typeArray3 = new Type[] { typeof(Action), typeof(Func<bool>) };
            }
            else
            {
                typeArray3 = new Type[] { typeof(Action), typeof(Func<bool>), typeof(bool) };
            }
            return GetMethodByParameter("Create", typeArray3);
        }

        internal static MethodInfo GetSimpleMethodWithResult(Type parameterType, bool withUseCommandManagerParameter)
        {
            Type[] typeArray4;
            if (!withUseCommandManagerParameter)
            {
                typeArray4 = new Type[] { typeof(Func<>), typeof(Func<bool>) };
            }
            else
            {
                typeArray4 = new Type[] { typeof(Func<>), typeof(Func<bool>), typeof(bool) };
            }
            Type[] typeArguments = new Type[] { parameterType };
            return GetMethodByParameter("CreateFromFunction", typeArray4).MakeGenericMethod(typeArguments);
        }
    }
}

