namespace DevExpress.DXBinding.Native
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class DynamicHelper
    {
        [ThreadStatic]
        private static Dictionary<Type, Func<object, bool>> isOperators;
        [ThreadStatic]
        private static Dictionary<Type, Func<object, object>> asOperators;
        [ThreadStatic]
        private static Dictionary<Type, Func<object, object>> castOperators;
        private static MethodInfo castMethod;

        public static object Binary(NBinary.NKind kind, [Dynamic] object left, [Dynamic(new bool[] { false, true })] Func<object> right)
        {
            switch (kind)
            {
                case NBinary.NKind.Mul:
                    if (<>o__13.<>p__11 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__11 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__11.Target(<>o__13.<>p__11, left, right());

                case NBinary.NKind.Div:
                    if (<>o__13.<>p__3 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__3 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Divide, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__3.Target(<>o__13.<>p__3, left, right());

                case NBinary.NKind.Mod:
                    if (<>o__13.<>p__10 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__10 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Modulo, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__10.Target(<>o__13.<>p__10, left, right());

                case NBinary.NKind.Plus:
                    if (<>o__13.<>p__17 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__17 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__17.Target(<>o__13.<>p__17, left, right());

                case NBinary.NKind.Minus:
                    if (<>o__13.<>p__9 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__9 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Subtract, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__9.Target(<>o__13.<>p__9, left, right());

                case NBinary.NKind.ShiftLeft:
                    if (<>o__13.<>p__18 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__18 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LeftShift, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__18.Target(<>o__13.<>p__18, left, right());

                case NBinary.NKind.ShiftRight:
                    if (<>o__13.<>p__19 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__19 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.RightShift, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__19.Target(<>o__13.<>p__19, left, right());

                case NBinary.NKind.Less:
                    if (<>o__13.<>p__7 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__7 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThan, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__7.Target(<>o__13.<>p__7, left, right());

                case NBinary.NKind.Greater:
                    if (<>o__13.<>p__5 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.GreaterThan, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__5.Target(<>o__13.<>p__5, left, right());

                case NBinary.NKind.LessOrEqual:
                    if (<>o__13.<>p__8 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__8 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThanOrEqual, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__8.Target(<>o__13.<>p__8, left, right());

                case NBinary.NKind.GreaterOrEqual:
                    if (<>o__13.<>p__6 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__6 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.GreaterThanOrEqual, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__6.Target(<>o__13.<>p__6, left, right());

                case NBinary.NKind.And:
                    if (<>o__13.<>p__0 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.And, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__0.Target(<>o__13.<>p__0, left, right());

                case NBinary.NKind.Or:
                    if (<>o__13.<>p__14 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__14 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Or, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__14.Target(<>o__13.<>p__14, left, right());

                case NBinary.NKind.Xor:
                    if (<>o__13.<>p__20 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__20 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.ExclusiveOr, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__20.Target(<>o__13.<>p__20, left, right());

                case NBinary.NKind.AndAlso:
                    if (<>o__13.<>p__2 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DynamicHelper), argumentInfo));
                    }
                    if (<>o__13.<>p__2.Target(<>o__13.<>p__2, left))
                    {
                        return left;
                    }
                    if (<>o__13.<>p__1 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__1 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__1.Target(<>o__13.<>p__1, left, right());

                case NBinary.NKind.OrElse:
                    if (<>o__13.<>p__16 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__16 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DynamicHelper), argumentInfo));
                    }
                    if (<>o__13.<>p__16.Target(<>o__13.<>p__16, left))
                    {
                        return left;
                    }
                    if (<>o__13.<>p__15 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__15 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__15.Target(<>o__13.<>p__15, left, right());

                case NBinary.NKind.Equal:
                    if (<>o__13.<>p__4 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__4 = CallSite<Func<CallSite, Type, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Equal", null, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__4.Target(<>o__13.<>p__4, typeof(DynamicHelper), left, right());

                case NBinary.NKind.NotEqual:
                    if (<>o__13.<>p__13 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__13 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(DynamicHelper), argumentInfo));
                    }
                    if (<>o__13.<>p__12 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__13.<>p__12 = CallSite<Func<CallSite, Type, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Equal", null, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__13.<>p__13.Target(<>o__13.<>p__13, <>o__13.<>p__12.Target(<>o__13.<>p__12, typeof(DynamicHelper), left, right()));

                case NBinary.NKind.Coalesce:
                    return (left ?? right());
            }
            throw new NotImplementedException();
        }

        private static object Cast<T>(object value)
        {
            if (value == null)
            {
                return null;
            }
            Type nullableType = typeof(T);
            if (!nullableType.IsValueType)
            {
                return (T) value;
            }
            Type underlyingType = Nullable.GetUnderlyingType(nullableType);
            return ((underlyingType == null) ? Convert.ChangeType(value, nullableType) : ((T) Convert.ChangeType(value, underlyingType)));
        }

        public static object Cast(NCast.NKind kind, object value, Type type)
        {
            if (kind == NCast.NKind.Is)
            {
                if (!IsOperators.ContainsKey(type))
                {
                    ParameterExpression expression = Expression.Parameter(typeof(object));
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    Func<object, bool> func = Expression.Lambda<Func<object, bool>>(Expression.TypeIs(expression, type), parameters).Compile();
                    IsOperators.Add(type, func);
                }
                return IsOperators[type](value);
            }
            if (kind == NCast.NKind.As)
            {
                if (!AsOperators.ContainsKey(type))
                {
                    ParameterExpression expression = Expression.Parameter(typeof(object));
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    Func<object, object> func2 = Expression.Lambda<Func<object, object>>(Expression.Convert(Expression.TypeAs(expression, type), typeof(object)), parameters).Compile();
                    AsOperators.Add(type, func2);
                }
                return AsOperators[type](value);
            }
            if (kind != NCast.NKind.Cast)
            {
                throw new NotImplementedException();
            }
            if (!CastOperators.ContainsKey(type))
            {
                ParameterExpression expression5 = Expression.Parameter(typeof(object));
                Type[] typeArguments = new Type[] { type };
                Expression[] arguments = new Expression[] { expression5 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression5 };
                Func<object, object> func3 = Expression.Lambda<Func<object, object>>(Expression.Call(null, CastMethod.MakeGenericMethod(typeArguments), arguments), parameters).Compile();
                CastOperators.Add(type, func3);
            }
            return CastOperators[type](value);
        }

        private static bool Equal([Dynamic] object left, [Dynamic] object right)
        {
            object local1;
            if (<>o__16.<>p__4 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                <>o__16.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DynamicHelper), argumentInfo));
            }
            if (<>o__16.<>p__0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                <>o__16.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(DynamicHelper), argumentInfo));
            }
            object obj2 = <>o__16.<>p__0.Target(<>o__16.<>p__0, left, null);
            if (<>o__16.<>p__3 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                <>o__16.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DynamicHelper), argumentInfo));
            }
            if (<>o__16.<>p__3.Target(<>o__16.<>p__3, obj2))
            {
                local1 = obj2;
            }
            else
            {
                if (<>o__16.<>p__2 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__16.<>p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DynamicHelper), argumentInfo));
                }
                if (<>o__16.<>p__1 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                    <>o__16.<>p__1 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(DynamicHelper), argumentInfo));
                }
                local1 = <>o__16.<>p__2.Target(<>o__16.<>p__2, obj2, <>o__16.<>p__1.Target(<>o__16.<>p__1, right, null));
            }
            if (<>o__16.<>p__4.Target(<>o__16.<>p__4, local1))
            {
                return true;
            }
            if (<>o__16.<>p__5 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                <>o__16.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(DynamicHelper), argumentInfo));
            }
            obj2 = <>o__16.<>p__5.Target(<>o__16.<>p__5, left, null);
            if (<>o__16.<>p__9 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                <>o__16.<>p__9 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DynamicHelper), argumentInfo));
            }
            if (!<>o__16.<>p__9.Target(<>o__16.<>p__9, obj2))
            {
                if (<>o__16.<>p__8 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__16.<>p__8 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DynamicHelper), argumentInfo));
                }
                if (<>o__16.<>p__7 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__16.<>p__7 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(DynamicHelper), argumentInfo));
                }
                if (<>o__16.<>p__6 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                    <>o__16.<>p__6 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(DynamicHelper), argumentInfo));
                }
                if (!<>o__16.<>p__8.Target(<>o__16.<>p__8, <>o__16.<>p__7.Target(<>o__16.<>p__7, obj2, <>o__16.<>p__6.Target(<>o__16.<>p__6, right, null))))
                {
                    <>o__16.<>p__11 ??= CallSite<Func<CallSite, object, Type>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(Type), typeof(DynamicHelper)));
                    if (<>o__16.<>p__10 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__16.<>p__10 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", null, typeof(DynamicHelper), argumentInfo));
                    }
                    Type type = <>o__16.<>p__11(<>o__16.<>p__11.Target, <>o__16.<>p__10.Target(<>o__16.<>p__10, left));
                    if (<>o__16.<>p__13 == null)
                    {
                        <>o__16.<>p__13 = CallSite<Func<CallSite, object, Type>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(Type), typeof(DynamicHelper)));
                    }
                    if (<>o__16.<>p__12 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__16.<>p__12 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetType", null, typeof(DynamicHelper), argumentInfo));
                    }
                    Type type2 = <>o__16.<>p__13.Target(<>o__16.<>p__13, <>o__16.<>p__12.Target(<>o__16.<>p__12, right));
                    if (!ParserHelper.CastNumericTypes(ref type, ref type2))
                    {
                        <>o__16.<>p__17 ??= CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof(bool), typeof(DynamicHelper)));
                        if (<>o__16.<>p__16 == null)
                        {
                            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                            <>o__16.<>p__16 = CallSite<Func<CallSite, Type, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Equals", null, typeof(DynamicHelper), argumentInfo));
                        }
                        return <>o__16.<>p__17(<>o__16.<>p__17.Target, <>o__16.<>p__16.Target(<>o__16.<>p__16, typeof(object), left, right));
                    }
                    if (<>o__16.<>p__14 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                        <>o__16.<>p__14 = CallSite<Func<CallSite, Type, NCast.NKind, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Cast", null, typeof(DynamicHelper), argumentInfo));
                    }
                    object objA = <>o__16.<>p__14.Target(<>o__16.<>p__14, typeof(DynamicHelper), NCast.NKind.Cast, left, type);
                    if (<>o__16.<>p__15 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                        <>o__16.<>p__15 = CallSite<Func<CallSite, Type, NCast.NKind, object, Type, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Cast", null, typeof(DynamicHelper), argumentInfo));
                    }
                    return Equals(objA, <>o__16.<>p__15.Target(<>o__16.<>p__15, typeof(DynamicHelper), NCast.NKind.Cast, right, type2));
                }
            }
            return false;
        }

        public static object Unary(NUnary.NKind kind, [Dynamic] object value)
        {
            switch (kind)
            {
                case NUnary.NKind.Plus:
                    if (<>o__14.<>p__1 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__14.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.UnaryPlus, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__14.<>p__1.Target(<>o__14.<>p__1, value);

                case NUnary.NKind.Minus:
                    if (<>o__14.<>p__0 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__14.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Negate, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__14.<>p__0.Target(<>o__14.<>p__0, value);

                case NUnary.NKind.Not:
                    if (<>o__14.<>p__2 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__14.<>p__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__14.<>p__2.Target(<>o__14.<>p__2, value);

                case NUnary.NKind.NotBitwise:
                    if (<>o__14.<>p__3 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__14.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.OnesComplement, typeof(DynamicHelper), argumentInfo));
                    }
                    return <>o__14.<>p__3.Target(<>o__14.<>p__3, value);
            }
            throw new NotImplementedException();
        }

        private static Dictionary<Type, Func<object, bool>> IsOperators =>
            isOperators ??= new Dictionary<Type, Func<object, bool>>();

        private static Dictionary<Type, Func<object, object>> AsOperators =>
            asOperators ??= new Dictionary<Type, Func<object, object>>();

        private static Dictionary<Type, Func<object, object>> CastOperators =>
            castOperators ??= new Dictionary<Type, Func<object, object>>();

        private static MethodInfo CastMethod =>
            castMethod ??= typeof(DynamicHelper).GetMethod("Cast", BindingFlags.NonPublic | BindingFlags.Static);

        [CompilerGenerated]
        private static class <>o__13
        {
            public static CallSite<Func<CallSite, object, object, object>> <>p__0;
            public static CallSite<Func<CallSite, object, object, object>> <>p__1;
            public static CallSite<Func<CallSite, object, bool>> <>p__2;
            public static CallSite<Func<CallSite, object, object, object>> <>p__3;
            public static CallSite<Func<CallSite, Type, object, object, object>> <>p__4;
            public static CallSite<Func<CallSite, object, object, object>> <>p__5;
            public static CallSite<Func<CallSite, object, object, object>> <>p__6;
            public static CallSite<Func<CallSite, object, object, object>> <>p__7;
            public static CallSite<Func<CallSite, object, object, object>> <>p__8;
            public static CallSite<Func<CallSite, object, object, object>> <>p__9;
            public static CallSite<Func<CallSite, object, object, object>> <>p__10;
            public static CallSite<Func<CallSite, object, object, object>> <>p__11;
            public static CallSite<Func<CallSite, Type, object, object, object>> <>p__12;
            public static CallSite<Func<CallSite, object, object>> <>p__13;
            public static CallSite<Func<CallSite, object, object, object>> <>p__14;
            public static CallSite<Func<CallSite, object, object, object>> <>p__15;
            public static CallSite<Func<CallSite, object, bool>> <>p__16;
            public static CallSite<Func<CallSite, object, object, object>> <>p__17;
            public static CallSite<Func<CallSite, object, object, object>> <>p__18;
            public static CallSite<Func<CallSite, object, object, object>> <>p__19;
            public static CallSite<Func<CallSite, object, object, object>> <>p__20;
        }

        [CompilerGenerated]
        private static class <>o__14
        {
            public static CallSite<Func<CallSite, object, object>> <>p__0;
            public static CallSite<Func<CallSite, object, object>> <>p__1;
            public static CallSite<Func<CallSite, object, object>> <>p__2;
            public static CallSite<Func<CallSite, object, object>> <>p__3;
        }

        [CompilerGenerated]
        private static class <>o__16
        {
            public static CallSite<Func<CallSite, object, object, object>> <>p__0;
            public static CallSite<Func<CallSite, object, object, object>> <>p__1;
            public static CallSite<Func<CallSite, object, object, object>> <>p__2;
            public static CallSite<Func<CallSite, object, bool>> <>p__3;
            public static CallSite<Func<CallSite, object, bool>> <>p__4;
            public static CallSite<Func<CallSite, object, object, object>> <>p__5;
            public static CallSite<Func<CallSite, object, object, object>> <>p__6;
            public static CallSite<Func<CallSite, object, object, object>> <>p__7;
            public static CallSite<Func<CallSite, object, bool>> <>p__8;
            public static CallSite<Func<CallSite, object, bool>> <>p__9;
            public static CallSite<Func<CallSite, object, object>> <>p__10;
            public static CallSite<Func<CallSite, object, Type>> <>p__11;
            public static CallSite<Func<CallSite, object, object>> <>p__12;
            public static CallSite<Func<CallSite, object, Type>> <>p__13;
            public static CallSite<Func<CallSite, Type, NCast.NKind, object, Type, object>> <>p__14;
            public static CallSite<Func<CallSite, Type, NCast.NKind, object, Type, object>> <>p__15;
            public static CallSite<Func<CallSite, Type, object, object, object>> <>p__16;
            public static CallSite<Func<CallSite, object, bool>> <>p__17;
        }
    }
}

