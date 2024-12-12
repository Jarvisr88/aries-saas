namespace DevExpress.Mvvm.Native
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class ExpressionHelper
    {
        private static void CheckParameterExpression(Expression expression)
        {
            if ((expression.NodeType != ExpressionType.Parameter) && ((expression.NodeType != ExpressionType.Convert) || (((UnaryExpression) expression).Operand.NodeType != ExpressionType.Parameter)))
            {
                throw new ArgumentException("expression");
            }
        }

        internal static MethodInfo GetArgumentFunctionStrict<T, TResult>(Expression<Func<T, TResult>> expression) => 
            GetArgumentMethodStrictCore(expression);

        internal static MethodInfo GetArgumentMethodStrict<T>(Expression<Action<T>> expression) => 
            GetArgumentMethodStrictCore(expression);

        private static MethodInfo GetArgumentMethodStrictCore(LambdaExpression expression)
        {
            MethodCallExpression methodCallExpression = GetMethodCallExpression(expression);
            CheckParameterExpression(methodCallExpression.Object);
            return methodCallExpression.Method;
        }

        internal static PropertyInfo GetArgumentPropertyStrict<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            MemberExpression body = null;
            if (expression.Body is MemberExpression)
            {
                body = (MemberExpression) expression.Body;
            }
            else if (expression.Body is UnaryExpression)
            {
                UnaryExpression body = (UnaryExpression) expression.Body;
                if (body.NodeType == ExpressionType.Convert)
                {
                    body = (MemberExpression) body.Operand;
                }
            }
            if (body == null)
            {
                throw new ArgumentException("expression");
            }
            CheckParameterExpression(body.Expression);
            return (PropertyInfo) body.Member;
        }

        internal static ConstructorInfo GetConstructor<T>(Expression<Func<T>> commandMethodExpression) => 
            GetConstructorCore(commandMethodExpression);

        internal static ConstructorInfo GetConstructorCore(LambdaExpression commandMethodExpression)
        {
            NewExpression body = commandMethodExpression.Body as NewExpression;
            if (body == null)
            {
                throw new ArgumentException("commandMethodExpression");
            }
            return body.Constructor;
        }

        private static MethodInfo GetGetMethod<TInterface>(TInterface _interface, string getMethodName)
        {
            InterfaceMapping interfaceMap = _interface.GetType().GetInterfaceMap(typeof(TInterface));
            var selector = <>c__17<TInterface>.<>9__17_0;
            if (<>c__17<TInterface>.<>9__17_0 == null)
            {
                var local1 = <>c__17<TInterface>.<>9__17_0;
                selector = <>c__17<TInterface>.<>9__17_0 = (m, i) => new { 
                    name = m.Name,
                    index = i
                };
            }
            var func2 = <>c__17<TInterface>.<>9__17_2;
            if (<>c__17<TInterface>.<>9__17_2 == null)
            {
                var local2 = <>c__17<TInterface>.<>9__17_2;
                func2 = <>c__17<TInterface>.<>9__17_2 = m => m.index;
            }
            return interfaceMap.TargetMethods[(from m in interfaceMap.InterfaceMethods.Select(selector)
                where string.Equals(m.name, getMethodName, StringComparison.Ordinal)
                select m).Select(func2).First<int>()];
        }

        private static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            Expression body = expression.Body;
            if (body is UnaryExpression)
            {
                body = ((UnaryExpression) body).Operand;
            }
            MemberExpression expression3 = body as MemberExpression;
            if (expression3 == null)
            {
                throw new ArgumentException("expression");
            }
            return expression3;
        }

        internal static MethodInfo GetMethod(LambdaExpression expression) => 
            GetMethodCallExpression(expression).Method;

        private static MethodCallExpression GetMethodCallExpression(LambdaExpression expression)
        {
            if (expression.Body is InvocationExpression)
            {
                expression = (LambdaExpression) ((InvocationExpression) expression.Body).Expression;
            }
            return (MethodCallExpression) expression.Body;
        }

        public static string GetMethodName(Expression<Action> expression) => 
            GetMethod(expression).Name;

        public static PropertyDescriptor GetProperty<T, TProperty>(Expression<Func<T, TProperty>> expression) => 
            TypeDescriptor.GetProperties(typeof(T))[GetPropertyName<T, TProperty>(expression)];

        public static string GetPropertyName<T>(Expression<Func<T>> expression) => 
            GetPropertyNameCore(expression);

        public static string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression) => 
            GetPropertyNameCore(expression);

        private static string GetPropertyNameCore(LambdaExpression expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression);
            if (IsPropertyExpression(memberExpression.Expression as MemberExpression))
            {
                throw new ArgumentException("expression");
            }
            return memberExpression.Member.Name;
        }

        private static bool IsPropertyExpression(MemberExpression expression) => 
            (expression != null) && (expression.Member.MemberType == MemberTypes.Property);

        public static bool PropertyHasImplicitImplementation<TInterface, TPropertyType>(TInterface _interface, Expression<Func<TInterface, TPropertyType>> property, bool tryInvoke = true) where TInterface: class
        {
            if (_interface == null)
            {
                throw new ArgumentNullException("_interface");
            }
            string name = GetArgumentPropertyStrict<TInterface, TPropertyType>(property).Name;
            string getMethodName = "get_" + name;
            MethodInfo getMethod = GetGetMethod<TInterface>(_interface, getMethodName);
            if (!getMethod.IsPublic || !string.Equals(getMethod.Name, getMethodName))
            {
                return false;
            }
            try
            {
                if (tryInvoke)
                {
                    getMethod.Invoke(_interface, null);
                }
            }
            catch (Exception exception)
            {
                bool flag;
                switch (exception)
                {
                    case (TargetException _):
                        flag = false;
                        break;

                    case (ArgumentException _):
                        flag = false;
                        break;

                    case (TargetParameterCountException _):
                        flag = false;
                        break;

                    case (MethodAccessException _):
                        flag = false;
                        break;

                    case (InvalidOperationException _):
                        break;

                    default:
                        throw;
                        break;
                }
                return flag;
            }
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__17<TInterface>
        {
            public static readonly ExpressionHelper.<>c__17<TInterface> <>9;
            public static Func<MethodInfo, int, <>f__AnonymousType2<string, int>> <>9__17_0;
            public static Func<<>f__AnonymousType2<string, int>, int> <>9__17_2;

            static <>c__17()
            {
                ExpressionHelper.<>c__17<TInterface>.<>9 = new ExpressionHelper.<>c__17<TInterface>();
            }

            internal <>f__AnonymousType2<string, int> <GetGetMethod>b__17_0(MethodInfo m, int i) => 
                new { 
                    name = m.Name,
                    index = i
                };

            internal int <GetGetMethod>b__17_2(<>f__AnonymousType2<string, int> m) => 
                m.index;
        }
    }
}

