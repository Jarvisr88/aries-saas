namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class Notify
    {
        private static readonly IDictionary<string, Func<object, object>> getPropCache = new Dictionary<string, Func<object, object>>(StringComparer.Ordinal);
        private static readonly IDictionary<Type, Action<object, string>> raisePropertyCache = new Dictionary<Type, Action<object, string>>();
        private static readonly IDictionary<Type, Action<object>> raiseCanExecuteCache = new Dictionary<Type, Action<object>>();

        private static Func<object, object> GetGetPropertyValue(MethodInfo methodInfo, Type type)
        {
            Func<object, object> func;
            string key = type.FullName + "." + methodInfo.Name;
            if (!getPropCache.TryGetValue(key, out func))
            {
                MethodInfo method = MethodInfoHelper.GetMethodInfo(type, "get_" + methodInfo.Name + "Command");
                if (method == null)
                {
                    getPropCache.Add(key, null);
                }
                else
                {
                    ParameterExpression expression = Expression.Parameter(typeof(object), "source");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    func = Expression.Lambda<Func<object, object>>(Expression.Call(Expression.Convert(expression, method.DeclaringType), method), parameters).Compile();
                    getPropCache.Add(key, func);
                }
            }
            return func;
        }

        [DebuggerStepThrough, DebuggerHidden]
        private static Action<object> GetRaiseCanExecuteChanged(object @this)
        {
            Action<object> action;
            Type key = @this.GetType();
            if (!raiseCanExecuteCache.TryGetValue(key, out action))
            {
                MethodInfo methodInfo = MethodInfoHelper.GetMethodInfo(key, "RaiseCanExecuteChanged");
                if (methodInfo == null)
                {
                    raiseCanExecuteCache.Add(key, null);
                }
                else
                {
                    ParameterExpression expression = Expression.Parameter(typeof(object), "source");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    action = Expression.Lambda<Action<object>>(Expression.Call(Expression.Convert(expression, methodInfo.DeclaringType), methodInfo), parameters).Compile();
                    raiseCanExecuteCache.Add(key, action);
                }
            }
            return action;
        }

        private static Action<object, string> GetRaisePropertyChanged(object @this)
        {
            Action<object, string> action;
            Type key = @this.GetType();
            if (!raisePropertyCache.TryGetValue(key, out action))
            {
                Type[] types = new Type[] { typeof(string) };
                MethodInfo method = MethodInfoHelper.GetMethodInfo(key, "RaisePropertyChanged", types, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (method == null)
                {
                    raisePropertyCache.Add(key, null);
                }
                else
                {
                    ParameterExpression expression = Expression.Parameter(typeof(object), "source");
                    ParameterExpression expression2 = Expression.Parameter(typeof(string), "parameter");
                    Expression[] arguments = new Expression[] { expression2 };
                    ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
                    action = Expression.Lambda<Action<object, string>>(Expression.Call(Expression.Convert(expression, method.DeclaringType), method, arguments), parameters).Compile();
                    raisePropertyCache.Add(key, action);
                }
            }
            return action;
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static void RaiseCanExecuteChanged(this object @this, Expression<Action> expression)
        {
            @this.RaiseCanExecuteChanged(((MethodCallExpression) expression.Body).Method);
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static void RaiseCanExecuteChanged<T>(this object @this, Expression<Action<T>> expression)
        {
            @this.RaiseCanExecuteChanged(((MethodCallExpression) expression.Body).Method);
        }

        private static void RaiseCanExecuteChanged(this object @this, MethodInfo methodInfo)
        {
            Action<object> @do = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Action<object> local1 = <>c.<>9__2_2;
                @do = <>c.<>9__2_2 = delegate (object command) {
                    Action<object> raiseCanExecuteChanged = GetRaiseCanExecuteChanged(command);
                    if (raiseCanExecuteChanged != null)
                    {
                        raiseCanExecuteChanged(command);
                    }
                };
            }
            @this.Get<object, Func<object, object>>(x => GetGetPropertyValue(methodInfo, x.GetType()), null).Get<Func<object, object>, object>(getProp => getProp(@this), null).Do<object>(@do);
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static void RaisePropertyChanged<T>(this object @this, Expression<Func<T>> expression)
        {
            @this.RaisePropertyChanged(ExpressionHelper.GetPropertyName<T>(expression));
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static void RaisePropertyChanged(this object @this, LambdaExpression propertySelector)
        {
            @this.RaisePropertyChanged(ExpressionHelper.GetPropertyName(propertySelector));
        }

        internal static void RaisePropertyChanged(this object @this, string propertyName)
        {
            @this.Do<object>(delegate (object x) {
                Action<object, string> raisePropertyChanged = GetRaisePropertyChanged(@this);
                if (raisePropertyChanged != null)
                {
                    raisePropertyChanged(@this, propertyName);
                }
            });
        }

        internal static void Reset()
        {
            getPropCache.Clear();
            raisePropertyCache.Clear();
            raiseCanExecuteCache.Clear();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Notify.<>c <>9 = new Notify.<>c();
            public static Action<object> <>9__2_2;

            internal void <RaiseCanExecuteChanged>b__2_2(object command)
            {
                Action<object> raiseCanExecuteChanged = Notify.GetRaiseCanExecuteChanged(command);
                if (raiseCanExecuteChanged != null)
                {
                    raiseCanExecuteChanged(command);
                }
            }
        }
    }
}

