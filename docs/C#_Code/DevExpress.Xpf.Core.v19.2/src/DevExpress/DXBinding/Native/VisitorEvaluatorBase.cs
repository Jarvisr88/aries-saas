namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal abstract class VisitorEvaluatorBase : VisitorBase<object>
    {
        private readonly bool fullPack;
        protected readonly Func<NType, Type> typeResolver;
        protected readonly IErrorHandler errorHandler;

        protected VisitorEvaluatorBase(Func<NType, Type> typeResolver, IErrorHandler errorHandler, bool fullPack)
        {
            this.fullPack = fullPack;
            this.typeResolver = typeResolver;
            this.errorHandler = errorHandler;
        }

        protected override object Binary(NBinary n, object left, object right)
        {
            throw new NotImplementedException();
        }

        protected override bool CanContinue(NBase n) => 
            !this.errorHandler.HasError;

        protected override object Cast(NCast n, object value) => 
            this.Try(() => DynamicHelper.Cast(n.Kind, value, this.typeResolver(n.Type)));

        protected override object Constant(NConstant n) => 
            n.Value;

        private static Type[] GetArgsTypes(IEnumerable<object> args)
        {
            Func<object, Type> selector = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Func<object, Type> local1 = <>c.<>9__33_0;
                selector = <>c.<>9__33_0 = x => x?.GetType();
            }
            return args.Select<object, Type>(selector).ToArray<Type>();
        }

        private static void GetInvocation<T>(IEnumerable<T> methods, IEnumerable<object> args, out T method, out IEnumerable<object> outArgs) where T: MethodBase
        {
            Type[] typeArray2;
            Type[] argsTypes = GetArgsTypes(args);
            outArgs = new List<object>(args);
            method = (T) MemberSearcher.FindMethodBase((IEnumerable<MethodBase>) methods, argsTypes, out typeArray2);
            if (((T) method) != null)
            {
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Any<ParameterInfo>() && MemberSearcher.IsParams(parameters.Last<ParameterInfo>()))
                {
                    int num = (args.Count<object>() - parameters.Length) + 1;
                    object[] sourceArray = outArgs.Skip<object>((outArgs.Count<object>() - num)).ToArray<object>();
                    outArgs = outArgs.Take<object>((args.Count<object>() - num)).ToList<object>();
                    Type arrayType = parameters.Last<ParameterInfo>().ParameterType.GetElementType();
                    sourceArray = (from x in sourceArray select DynamicHelper.Cast(NCast.NKind.Cast, x, arrayType)).ToArray<object>();
                    Array destinationArray = Array.CreateInstance(arrayType, sourceArray.Length);
                    Array.Copy(sourceArray, destinationArray, sourceArray.Length);
                    ((List<object>) outArgs).Add(destinationArray);
                }
                if (parameters.Length > args.Count<object>())
                {
                    for (int i = args.Count<object>(); i < parameters.Length; i++)
                    {
                        ((List<object>) outArgs).Add(parameters[i].DefaultValue);
                    }
                }
            }
        }

        protected abstract object GetOperandValue(Operand operand, NRelative.NKind? relativeSource);
        protected override object Ident(object from, NIdent n)
        {
            if (!this.IsNull(from))
            {
                Type instanceType = from.GetType();
                PropertyInfo lastMember = MemberSearcher.FindInstanceProperty(instanceType, n.Name);
                if (lastMember != null)
                {
                    this.SetLastMember(lastMember, from);
                    return lastMember.GetValue(from, null);
                }
                FieldInfo info2 = MemberSearcher.FindInstanceField(instanceType, n.Name);
                if (info2 != null)
                {
                    this.SetLastMember(info2, from);
                    return info2.GetValue(from);
                }
                this.errorHandler.Report(ErrorHelper.Report001(n.Name, from.GetType()), true);
            }
            return null;
        }

        protected override object Index(object from, NIndex n, IEnumerable<object> indexArgs)
        {
            if (this.IsNull(from))
            {
                return null;
            }
            Type fromT = from.GetType();
            if (fromT.IsArray)
            {
                return this.Try(() => ((Array) from).GetValue(indexArgs.Cast<int>().ToArray<int>()));
            }
            Func<PropertyInfo, bool> predicate = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__24_1;
                predicate = <>c.<>9__24_1 = x => x.GetIndexParameters().Any<ParameterInfo>();
            }
            Func<PropertyInfo, MethodInfo> selector = <>c.<>9__24_2;
            if (<>c.<>9__24_2 == null)
            {
                Func<PropertyInfo, MethodInfo> local2 = <>c.<>9__24_2;
                selector = <>c.<>9__24_2 = x => x.GetGetMethod();
            }
            MethodInfo[] methods = fromT.GetProperties(MemberSearcher.InstanceBindingFlags).Where<PropertyInfo>(predicate).Select<PropertyInfo, MethodInfo>(selector).ToArray<MethodInfo>();
            return this.InvokeMethod(methods, indexArgs, "Indexer", from, fromT);
        }

        private object InvokeMethod(IEnumerable<MethodInfo> methods, IEnumerable<object> methodArgs, string methodName, object from, Type fromT)
        {
            MethodInfo method;
            IEnumerable<object> outMethodArgs;
            GetInvocation<MethodInfo>(methods, methodArgs, out method, out outMethodArgs);
            if (method != null)
            {
                return this.Try(() => method.Invoke(from, outMethodArgs.ToArray<object>()));
            }
            this.errorHandler.Report(ErrorHelper.Report002(methodName, GetArgsTypes(methodArgs), fromT), true);
            return null;
        }

        private bool IsNull(object obj)
        {
            if (obj != null)
            {
                return false;
            }
            this.errorHandler.SetError();
            return true;
        }

        protected override object Method(object from, NMethod n, IEnumerable<object> methodArgs)
        {
            if (this.IsNull(from))
            {
                return null;
            }
            Type fromT = from.GetType();
            string methodName = n.Name;
            MethodInfo[] methods = (from x in fromT.GetMethods(MemberSearcher.InstanceBindingFlags)
                where x.Name == methodName
                select x).ToArray<MethodInfo>();
            return this.InvokeMethod(methods, methodArgs, methodName, from, fromT);
        }

        protected override object Relative(object from, NRelative n)
        {
            switch (n.Kind)
            {
                case NRelative.NKind.Value:
                case NRelative.NKind.Parameter:
                case NRelative.NKind.Sender:
                case NRelative.NKind.Args:
                    return this.GetOperandValue(null, new NRelative.NKind?(n.Kind));
            }
            throw new NotImplementedException();
        }

        protected override object RootIdent(NIdentBase n)
        {
            object operandValue;
            NIdentBase next;
            Operand operand = VisitorOperand.ReduceIdent(n, x => this.typeResolver(x), out next, this.fullPack);
            if ((operand == null) && (next is NRelative))
            {
                operandValue = this.Relative(null, (NRelative) next);
                next = next.Next;
            }
            else
            {
                NRelative.NKind? relativeSource = null;
                operandValue = this.GetOperandValue(operand, relativeSource);
            }
            n = next;
            while (n != null)
            {
                if (!this.CanContinue(n))
                {
                    return null;
                }
                operandValue = base.RootIdentCore(operandValue, n);
                operand = null;
                n = n.Next;
            }
            return operandValue;
        }

        private void SetLastMember(object lastMember, object lastMemberOwner)
        {
            this.LastMember = lastMember;
            this.LastMemberOwner = lastMemberOwner;
            this.IsLastMember = true;
        }

        protected override object Ternary(NTernary n, object first, object second, object third)
        {
            throw new NotImplementedException();
        }

        private object Try(Func<object> func)
        {
            try
            {
                return func();
            }
            catch (Exception exception)
            {
                this.errorHandler.ReportOrThrow(exception);
                return null;
            }
        }

        protected override object Type_Attached(object from, NType n)
        {
            if (this.IsNull(from))
            {
                return null;
            }
            Type instanceType = this.typeResolver(n);
            FieldInfo info = MemberSearcher.FindStaticField(instanceType, n.Ident.Name + "Property");
            if (info == null)
            {
                this.errorHandler.Report(ErrorHelper.Report001(n.Ident.Name + "Property", instanceType), true);
                return null;
            }
            DependencyProperty dp = (DependencyProperty) info.GetValue(null);
            if (dp == null)
            {
                this.errorHandler.Report(ErrorHelper.Report001(n.Ident.Name + "Property", instanceType), true);
                return null;
            }
            this.SetLastMember(dp, from);
            return this.Try(() => ((DependencyObject) from).GetValue(dp));
        }

        protected override object Type_New(object from, NNew n, IEnumerable<object> args)
        {
            ConstructorInfo method;
            IEnumerable<object> outMethodArgs;
            Type t = this.typeResolver(n.Type);
            if (t.IsPrimitive)
            {
                return this.Try(() => Activator.CreateInstance(t, args.ToArray<object>()));
            }
            GetInvocation<ConstructorInfo>(t.GetConstructors(), args, out method, out outMethodArgs);
            if (method != null)
            {
                return this.Try(() => method.Invoke(outMethodArgs.ToArray<object>()));
            }
            this.errorHandler.Report(ErrorHelper.Report002("Ctor", GetArgsTypes(args), t), true);
            return null;
        }

        protected override object Type_StaticIdent(object from, NType n)
        {
            Type instanceType = this.typeResolver(n);
            PropertyInfo lastMember = MemberSearcher.FindStaticProperty(instanceType, n.Ident.Name);
            if (lastMember != null)
            {
                this.SetLastMember(lastMember, null);
                return lastMember.GetValue(null, null);
            }
            FieldInfo info2 = MemberSearcher.FindStaticField(instanceType, n.Ident.Name);
            if (info2 != null)
            {
                this.SetLastMember(info2, null);
                return info2.GetValue(null);
            }
            this.errorHandler.Report(ErrorHelper.Report001(n.Ident.Name, instanceType), true);
            return null;
        }

        protected override object Type_StaticMethod(object from, NType n, IEnumerable<object> methodArgs)
        {
            Type fromT = this.typeResolver(n);
            string methodName = n.Ident.Name;
            MethodInfo[] methods = (from x in fromT.GetMethods(MemberSearcher.StaticBindingFlags)
                where x.Name == methodName
                select x).ToArray<MethodInfo>();
            return this.InvokeMethod(methods, methodArgs, methodName, null, fromT);
        }

        protected override object Type_Type(object from, NType n)
        {
            throw new NotImplementedException();
        }

        protected override object Type_TypeOf(object from, NType n) => 
            this.typeResolver(n);

        protected override object Unary(NUnary n, object value) => 
            this.Try(() => DynamicHelper.Unary(n.Kind, value));

        protected override object Visit(NBase n)
        {
            this.IsLastMember = false;
            return base.Visit(n);
        }

        protected override object VisitBinary(NBinary n) => 
            this.Try(delegate {
                Func<object> <>9__1;
                Func<object> right = <>9__1;
                if (<>9__1 == null)
                {
                    Func<object> local1 = <>9__1;
                    right = <>9__1 = () => this.Visit(n.Right);
                }
                return DynamicHelper.Binary(n.Kind, this.Visit(n.Left), right);
            });

        protected override object VisitTernary(NTernary n)
        {
            object objA = this.Visit(n.First);
            if (Equals(objA, true))
            {
                return this.Visit(n.Second);
            }
            if (Equals(objA, false))
            {
                return this.Visit(n.Third);
            }
            this.errorHandler.Report($"Cannot convert '{objA}' to boolean.", true);
            return null;
        }

        protected object LastMember { get; private set; }

        protected object LastMemberOwner { get; private set; }

        protected bool IsLastMember { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisitorEvaluatorBase.<>c <>9 = new VisitorEvaluatorBase.<>c();
            public static Func<PropertyInfo, bool> <>9__24_1;
            public static Func<PropertyInfo, MethodInfo> <>9__24_2;
            public static Func<object, Type> <>9__33_0;

            internal Type <GetArgsTypes>b__33_0(object x) => 
                x?.GetType();

            internal bool <Index>b__24_1(PropertyInfo x) => 
                x.GetIndexParameters().Any<ParameterInfo>();

            internal MethodInfo <Index>b__24_2(PropertyInfo x) => 
                x.GetGetMethod();
        }
    }
}

