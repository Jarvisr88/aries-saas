namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal abstract class VisitorExpressionBase : VisitorBase<System.Linq.Expressions.Expression>
    {
        private static readonly MethodInfo StringConcatMethod;
        private static readonly Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression> StringConcatExpression;
        private static readonly MethodInfo DependencyObject_GetValueMethod;
        private static readonly Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression> DependencyObject_GetValueExpression;
        private static readonly Dictionary<NBinary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> binaryKindToExpressionMapping;
        private static readonly Dictionary<NUnary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> unaryKindToExpressionMapping;
        private static readonly Dictionary<NCast.NKind, Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>> castKindToExpressionMapping;
        private static readonly Dictionary<NTernary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> ternaryKindToExpressionMapping;
        private List<Tuple<Operand, System.Linq.Expressions.Expression>> assigns;
        private IErrorHandler errorHandler;
        private Func<NType, Type> typeResolver;

        static VisitorExpressionBase()
        {
            Type[] types = new Type[] { typeof(object), typeof(object) };
            StringConcatMethod = typeof(string).GetMethod("Concat", MemberSearcher.StaticBindingFlags, Type.DefaultBinder, types, null);
            StringConcatExpression = (x, y) => System.Linq.Expressions.Expression.Call(null, StringConcatMethod, x, y);
            Type[] typeArray2 = new Type[] { typeof(DependencyProperty) };
            DependencyObject_GetValueMethod = typeof(DependencyObject).GetMethod("GetValue", BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, typeArray2, null);
            DependencyObject_GetValueExpression = (x, y) => System.Linq.Expressions.Expression.Call(x, DependencyObject_GetValueMethod, new System.Linq.Expressions.Expression[] { y });
            Dictionary<NBinary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> dictionary = new Dictionary<NBinary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> {
                { 
                    NBinary.NKind.Mul,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Multiply)
                },
                { 
                    NBinary.NKind.Div,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Divide)
                },
                { 
                    NBinary.NKind.Mod,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Modulo)
                },
                { 
                    NBinary.NKind.Plus,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Add)
                },
                { 
                    NBinary.NKind.Minus,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Subtract)
                },
                { 
                    NBinary.NKind.ShiftLeft,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.LeftShift)
                },
                { 
                    NBinary.NKind.ShiftRight,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.RightShift)
                },
                { 
                    NBinary.NKind.Less,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.LessThan)
                },
                { 
                    NBinary.NKind.Greater,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.GreaterThan)
                },
                { 
                    NBinary.NKind.LessOrEqual,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.LessThanOrEqual)
                },
                { 
                    NBinary.NKind.GreaterOrEqual,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.GreaterThanOrEqual)
                },
                { 
                    NBinary.NKind.Equal,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Equal)
                },
                { 
                    NBinary.NKind.NotEqual,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.NotEqual)
                },
                { 
                    NBinary.NKind.And,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.And)
                },
                { 
                    NBinary.NKind.Or,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Or)
                },
                { 
                    NBinary.NKind.Xor,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.ExclusiveOr)
                },
                { 
                    NBinary.NKind.AndAlso,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.AndAlso)
                },
                { 
                    NBinary.NKind.OrElse,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.OrElse)
                },
                { 
                    NBinary.NKind.Coalesce,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Coalesce)
                }
            };
            binaryKindToExpressionMapping = dictionary;
            Dictionary<NUnary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> dictionary2 = new Dictionary<NUnary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> {
                { 
                    NUnary.NKind.Plus,
                    x => x
                },
                { 
                    NUnary.NKind.Minus,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Negate)
                },
                { 
                    NUnary.NKind.Not,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Not)
                },
                { 
                    NUnary.NKind.NotBitwise,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Not)
                }
            };
            unaryKindToExpressionMapping = dictionary2;
            Dictionary<NCast.NKind, Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>> dictionary3 = new Dictionary<NCast.NKind, Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>> {
                { 
                    NCast.NKind.Cast,
                    new Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Convert)
                },
                { 
                    NCast.NKind.Is,
                    new Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.TypeIs)
                },
                { 
                    NCast.NKind.As,
                    new Func<System.Linq.Expressions.Expression, Type, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.TypeAs)
                }
            };
            castKindToExpressionMapping = dictionary3;
            Dictionary<NTernary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> dictionary4 = new Dictionary<NTernary.NKind, Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>> {
                { 
                    NTernary.NKind.Condition,
                    new Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.Expression>(System.Linq.Expressions.Expression.Condition)
                }
            };
            ternaryKindToExpressionMapping = dictionary4;
        }

        protected VisitorExpressionBase()
        {
        }

        protected override System.Linq.Expressions.Expression Assign(NAssign n, System.Linq.Expressions.Expression value)
        {
            NIdentBase base2;
            if (!(n.Left is NIdentBase))
            {
                throw new InvalidOperationException();
            }
            Operand operand = VisitorOperand.ReduceIdent((NIdentBase) n.Left, x => this.typeResolver(x), out base2, true);
            this.assigns.Add(new Tuple<Operand, System.Linq.Expressions.Expression>(operand, this.Visit(n.Expr)));
            return null;
        }

        protected override System.Linq.Expressions.Expression Binary(NBinary n, System.Linq.Expressions.Expression left, System.Linq.Expressions.Expression right)
        {
            if ((n.Kind == NBinary.NKind.Plus) && ((left.Type == typeof(string)) || (right.Type == typeof(string))))
            {
                return StringConcatExpression(System.Linq.Expressions.Expression.Convert(left, typeof(object)), System.Linq.Expressions.Expression.Convert(right, typeof(object)));
            }
            ParserHelper.CastNumericTypes(ref left, ref right);
            return binaryKindToExpressionMapping[n.Kind](left, right);
        }

        protected override bool CanContinue(NBase n) => 
            this.CanContinue(n, null);

        protected abstract bool CanContinue(NBase n, Operand operand);
        protected override System.Linq.Expressions.Expression Cast(NCast n, System.Linq.Expressions.Expression value) => 
            castKindToExpressionMapping[n.Kind](value, this.typeResolver(n.Type));

        protected override System.Linq.Expressions.Expression Constant(NConstant n) => 
            System.Linq.Expressions.Expression.Constant(n.Value);

        private System.Linq.Expressions.Expression GetInvocationExpression(System.Linq.Expressions.Expression from, Type fromType, MethodInfo[] possibleMethods, string methodName, IEnumerable<System.Linq.Expressions.Expression> methodArgs)
        {
            Type[] typeArray;
            Func<System.Linq.Expressions.Expression, Type> selector = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<System.Linq.Expressions.Expression, Type> local1 = <>c.<>9__12_0;
                selector = <>c.<>9__12_0 = x => x.Type;
            }
            MethodInfo method = MemberSearcher.FindMethod(possibleMethods, methodArgs.Select<System.Linq.Expressions.Expression, Type>(selector).ToArray<Type>(), out typeArray);
            if (method == null)
            {
                Func<System.Linq.Expressions.Expression, Type> func2 = <>c.<>9__12_1;
                if (<>c.<>9__12_1 == null)
                {
                    Func<System.Linq.Expressions.Expression, Type> local2 = <>c.<>9__12_1;
                    func2 = <>c.<>9__12_1 = x => x.Type;
                }
                this.errorHandler.Report(ErrorHelper.Report002(methodName, methodArgs.Select<System.Linq.Expressions.Expression, Type>(func2).ToArray<Type>(), fromType), true);
                return null;
            }
            List<System.Linq.Expressions.Expression> source = new List<System.Linq.Expressions.Expression>();
            if (typeArray != null)
            {
                for (int i = 0; i < typeArray.Count<Type>(); i++)
                {
                    source.Add(System.Linq.Expressions.Expression.Convert(methodArgs.ElementAt<System.Linq.Expressions.Expression>(i), typeArray[i]));
                }
            }
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Any<ParameterInfo>() && MemberSearcher.IsParams(parameters.Last<ParameterInfo>()))
            {
                int num2 = (typeArray.Length - parameters.Length) + 1;
                System.Linq.Expressions.Expression[] initializers = source.Skip<System.Linq.Expressions.Expression>((source.Count - num2)).ToArray<System.Linq.Expressions.Expression>();
                NewArrayExpression[] second = new NewArrayExpression[] { System.Linq.Expressions.Expression.NewArrayInit(typeArray.Last<Type>(), initializers) };
                source = source.Take<System.Linq.Expressions.Expression>((methodArgs.Count<System.Linq.Expressions.Expression>() - num2)).Concat<System.Linq.Expressions.Expression>(second).ToList<System.Linq.Expressions.Expression>();
            }
            if (parameters.Length > typeArray.Length)
            {
                for (int i = typeArray.Length; i < parameters.Length; i++)
                {
                    source.Add(System.Linq.Expressions.Expression.Constant(parameters[i].DefaultValue, parameters[i].ParameterType));
                }
            }
            return System.Linq.Expressions.Expression.Call(from, method, source);
        }

        protected abstract ParameterExpression GetOperandParameter(Operand operand, NRelative.NKind? relativeSource);
        protected override System.Linq.Expressions.Expression Ident(System.Linq.Expressions.Expression from, NIdent n)
        {
            PropertyInfo property = MemberSearcher.FindPropertyCore(from.Type, n.Name, BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                return System.Linq.Expressions.Expression.Property(from, property);
            }
            FieldInfo field = MemberSearcher.FindFieldCore(from.Type, n.Name, BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                return System.Linq.Expressions.Expression.Field(from, field);
            }
            this.errorHandler.Report(ErrorHelper.Report001(n.Name, from.Type), true);
            return null;
        }

        protected override System.Linq.Expressions.Expression Index(System.Linq.Expressions.Expression from, NIndex n, IEnumerable<System.Linq.Expressions.Expression> indexArgs)
        {
            if (from.Type.IsArray)
            {
                return System.Linq.Expressions.Expression.ArrayIndex(from, indexArgs);
            }
            Func<PropertyInfo, bool> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = x => x.GetIndexParameters().Any<ParameterInfo>();
            }
            Func<PropertyInfo, MethodInfo> selector = <>c.<>9__9_1;
            if (<>c.<>9__9_1 == null)
            {
                Func<PropertyInfo, MethodInfo> local2 = <>c.<>9__9_1;
                selector = <>c.<>9__9_1 = x => x.GetGetMethod();
            }
            MethodInfo[] possibleMethods = from.Type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Where<PropertyInfo>(predicate).Select<PropertyInfo, MethodInfo>(selector).ToArray<MethodInfo>();
            return this.GetInvocationExpression(from, from.Type, possibleMethods, n.Name, indexArgs);
        }

        protected override System.Linq.Expressions.Expression Method(System.Linq.Expressions.Expression from, NMethod n, IEnumerable<System.Linq.Expressions.Expression> methodArgs)
        {
            MethodInfo[] possibleMethods = (from x in from.Type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                where x.Name == n.Name
                select x).ToArray<MethodInfo>();
            return this.GetInvocationExpression(from, from.Type, possibleMethods, n.Name, methodArgs);
        }

        protected override System.Linq.Expressions.Expression Relative(System.Linq.Expressions.Expression from, NRelative n)
        {
            switch (n.Kind)
            {
                case NRelative.NKind.Value:
                case NRelative.NKind.Parameter:
                case NRelative.NKind.Sender:
                case NRelative.NKind.Args:
                    return this.GetOperandParameter(null, new NRelative.NKind?(n.Kind));
            }
            throw new NotImplementedException();
        }

        protected IEnumerable<System.Linq.Expressions.Expression> Resolve(NRoot expr, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            IEnumerable<System.Linq.Expressions.Expression> enumerable;
            try
            {
                this.errorHandler = errorHandler;
                this.typeResolver = typeResolver;
                enumerable = base.RootVisit(expr);
            }
            finally
            {
                this.errorHandler = null;
                this.typeResolver = null;
            }
            return enumerable;
        }

        protected IEnumerable<Tuple<Operand, System.Linq.Expressions.Expression>> ResolveBackCore(NRoot expr, Func<NType, Type> typeResolver, IErrorHandler errorHandler)
        {
            this.assigns = new List<Tuple<Operand, System.Linq.Expressions.Expression>>();
            IEnumerable<System.Linq.Expressions.Expression> source = this.Resolve(expr, typeResolver, errorHandler);
            if ((source.Count<System.Linq.Expressions.Expression>() == 1) && (source.First<System.Linq.Expressions.Expression>() != null))
            {
                return new Tuple<Operand, System.Linq.Expressions.Expression>[] { new Tuple<Operand, System.Linq.Expressions.Expression>(null, source.First<System.Linq.Expressions.Expression>()) };
            }
            List<Tuple<Operand, System.Linq.Expressions.Expression>> assigns = this.assigns;
            this.assigns = null;
            return assigns;
        }

        protected override System.Linq.Expressions.Expression RootIdent(NIdentBase n)
        {
            System.Linq.Expressions.Expression operandParameter;
            NIdentBase next;
            Operand operand = VisitorOperand.ReduceIdent(n, this.typeResolver, out next, true);
            if ((operand == null) && (next is NRelative))
            {
                operandParameter = this.Relative(null, (NRelative) next);
                next = next.Next;
            }
            else
            {
                NRelative.NKind? relativeSource = null;
                operandParameter = this.GetOperandParameter(operand, relativeSource);
            }
            n = next;
            while (n != null)
            {
                if (!this.CanContinue(n, operand))
                {
                    return null;
                }
                operandParameter = base.RootIdentCore(operandParameter, n);
                operand = null;
                n = n.Next;
            }
            return operandParameter;
        }

        protected override System.Linq.Expressions.Expression Ternary(NTernary n, System.Linq.Expressions.Expression first, System.Linq.Expressions.Expression second, System.Linq.Expressions.Expression third)
        {
            ParserHelper.CastNumericTypes(ref second, ref third);
            if (second.Type != third.Type)
            {
                bool flag = MemberSearcher.IsImplicitConversion(second.Type, third.Type);
                bool flag2 = MemberSearcher.IsImplicitConversion(third.Type, second.Type);
                if (flag && !flag2)
                {
                    second = System.Linq.Expressions.Expression.Convert(second, third.Type);
                }
                if (flag2 && !flag)
                {
                    third = System.Linq.Expressions.Expression.Convert(third, second.Type);
                }
            }
            return ternaryKindToExpressionMapping[n.Kind](first, second, third);
        }

        protected override System.Linq.Expressions.Expression Type_Attached(System.Linq.Expressions.Expression from, NType n)
        {
            System.Linq.Expressions.Expression expression = this.Type_StaticIdentCore(this.typeResolver(n), n.Ident.Name + "Property");
            return DependencyObject_GetValueExpression(from, expression);
        }

        protected override System.Linq.Expressions.Expression Type_New(System.Linq.Expressions.Expression from, NNew n, IEnumerable<System.Linq.Expressions.Expression> methodArgs)
        {
            throw new NotSupportedException();
        }

        protected override System.Linq.Expressions.Expression Type_StaticIdent(System.Linq.Expressions.Expression from, NType n)
        {
            Type t = this.typeResolver(n);
            return this.Type_StaticIdentCore(t, n.Ident.Name);
        }

        private System.Linq.Expressions.Expression Type_StaticIdentCore(Type t, string nName)
        {
            object obj2 = t.GetProperty(nName, MemberSearcher.StaticBindingFlags) ?? t.GetField(nName, MemberSearcher.StaticBindingFlags);
            if (obj2 is PropertyInfo)
            {
                return System.Linq.Expressions.Expression.Property(null, (PropertyInfo) obj2);
            }
            if (obj2 is FieldInfo)
            {
                return System.Linq.Expressions.Expression.Field(null, (FieldInfo) obj2);
            }
            this.errorHandler.Throw(ErrorHelper.Report001(nName, t), null);
            return System.Linq.Expressions.Expression.Constant(null);
        }

        protected override System.Linq.Expressions.Expression Type_StaticMethod(System.Linq.Expressions.Expression from, NType n, IEnumerable<System.Linq.Expressions.Expression> methodArgs)
        {
            Type fromType = this.typeResolver(n);
            return this.GetInvocationExpression(null, fromType, (from x in fromType.GetMethods(MemberSearcher.StaticBindingFlags)
                where x.Name == n.Ident.Name
                select x).ToArray<MethodInfo>(), n.Ident.Name, methodArgs);
        }

        protected override System.Linq.Expressions.Expression Type_Type(System.Linq.Expressions.Expression from, NType n)
        {
            throw new NotImplementedException();
        }

        protected override System.Linq.Expressions.Expression Type_TypeOf(System.Linq.Expressions.Expression from, NType n) => 
            System.Linq.Expressions.Expression.Constant(this.typeResolver(n), typeof(Type));

        protected override System.Linq.Expressions.Expression Unary(NUnary n, System.Linq.Expressions.Expression value) => 
            unaryKindToExpressionMapping[n.Kind](value);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisitorExpressionBase.<>c <>9 = new VisitorExpressionBase.<>c();
            public static Func<PropertyInfo, bool> <>9__9_0;
            public static Func<PropertyInfo, MethodInfo> <>9__9_1;
            public static Func<System.Linq.Expressions.Expression, Type> <>9__12_0;
            public static Func<System.Linq.Expressions.Expression, Type> <>9__12_1;

            internal System.Linq.Expressions.Expression <.cctor>b__36_0(System.Linq.Expressions.Expression x, System.Linq.Expressions.Expression y) => 
                System.Linq.Expressions.Expression.Call(null, VisitorExpressionBase.StringConcatMethod, x, y);

            internal System.Linq.Expressions.Expression <.cctor>b__36_1(System.Linq.Expressions.Expression x, System.Linq.Expressions.Expression y)
            {
                System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { y };
                return System.Linq.Expressions.Expression.Call(x, VisitorExpressionBase.DependencyObject_GetValueMethod, arguments);
            }

            internal System.Linq.Expressions.Expression <.cctor>b__36_2(System.Linq.Expressions.Expression x) => 
                x;

            internal Type <GetInvocationExpression>b__12_0(System.Linq.Expressions.Expression x) => 
                x.Type;

            internal Type <GetInvocationExpression>b__12_1(System.Linq.Expressions.Expression x) => 
                x.Type;

            internal bool <Index>b__9_0(PropertyInfo x) => 
                x.GetIndexParameters().Any<ParameterInfo>();

            internal MethodInfo <Index>b__9_1(PropertyInfo x) => 
                x.GetGetMethod();
        }
    }
}

