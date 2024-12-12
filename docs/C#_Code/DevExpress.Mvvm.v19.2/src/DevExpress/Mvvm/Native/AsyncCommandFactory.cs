namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class AsyncCommandFactory
    {
        public static AsyncCommand CreateFromFunction<TResult>(Func<Task> executeMethod, Func<bool> canExecuteMethod, bool allowMultipleExecution) => 
            new AsyncCommand(() => executeMethod(), canExecuteMethod, allowMultipleExecution, null);

        public static AsyncCommand<T> CreateFromFunction<T, TResult>(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool allowMultipleExecution) => 
            new AsyncCommand<T>(x => executeMethod(x), canExecuteMethod, allowMultipleExecution, null);

        public static AsyncCommand CreateFromFunction<TResult>(Func<Task> executeMethod, Func<bool> canExecuteMethod, bool allowMultipleExecution, bool useCommandManager) => 
            new AsyncCommand(() => executeMethod(), canExecuteMethod, allowMultipleExecution, new bool?(useCommandManager));

        public static AsyncCommand<T> CreateFromFunction<T, TResult>(Func<T, Task> executeMethod, Func<T, bool> canExecuteMethod, bool allowMultipleExecution, bool useCommandManager) => 
            new AsyncCommand<T>(x => executeMethod(x), canExecuteMethod, allowMultipleExecution, new bool?(useCommandManager));

        internal static MethodInfo GetGenericMethodWithResult(Type parameterType1, Type parameterType2, bool withUseCommandManagerParameter)
        {
            Type[] typeArray4;
            if (!withUseCommandManagerParameter)
            {
                typeArray4 = new Type[] { typeof(Func<,>), typeof(Func<,>), typeof(bool) };
            }
            else
            {
                typeArray4 = new Type[] { typeof(Func<,>), typeof(Func<,>), typeof(bool), typeof(bool) };
            }
            Type[] typeArguments = new Type[] { parameterType1, parameterType2 };
            return GetMethodByParameter("CreateFromFunction", typeArray4).MakeGenericMethod(typeArguments);
        }

        private static MethodInfo GetMethodByParameter(string methodName, Type[] parameterTypes)
        {
            MethodInfo info2;
            using (IEnumerator<MethodInfo> enumerator = (from m in typeof(AsyncCommandFactory).GetMethods()
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

        internal static MethodInfo GetSimpleMethodWithResult(Type parameterType, bool withUseCommandManagerParameter)
        {
            Type[] typeArray4;
            if (!withUseCommandManagerParameter)
            {
                typeArray4 = new Type[] { typeof(Func<>), typeof(Func<bool>), typeof(bool) };
            }
            else
            {
                typeArray4 = new Type[] { typeof(Func<>), typeof(Func<bool>), typeof(bool), typeof(bool) };
            }
            Type[] typeArguments = new Type[] { parameterType };
            return GetMethodByParameter("CreateFromFunction", typeArray4).MakeGenericMethod(typeArguments);
        }
    }
}

