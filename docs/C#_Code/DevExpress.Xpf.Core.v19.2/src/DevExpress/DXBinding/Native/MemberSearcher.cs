namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class MemberSearcher
    {
        public static readonly BindingFlags StaticBindingFlags = (BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
        public static readonly BindingFlags InstanceBindingFlags = (BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);

        public static ConstructorInfo FindCtor(IEnumerable<ConstructorInfo> allMethods, Type[] args, out Type[] outArgs) => 
            (ConstructorInfo) FindMethodBase(allMethods, args, out outArgs);

        internal static FieldInfo FindFieldCore(Type instanceType, string fieldName, BindingFlags flags) => 
            instanceType.GetField(fieldName, flags);

        public static FieldInfo FindInstanceField(Type instanceType, string fieldName)
        {
            for (Type type = instanceType; type != null; type = type.BaseType)
            {
                FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (field != null)
                {
                    return field;
                }
            }
            return null;
        }

        public static PropertyInfo FindInstanceProperty(Type instanceType, string propertyName)
        {
            for (Type type = instanceType; type != null; type = type.BaseType)
            {
                PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (property != null)
                {
                    return property;
                }
            }
            return null;
        }

        public static MethodInfo FindMethod(IEnumerable<MethodInfo> allMethods, Type[] args, out Type[] outArgs) => 
            (MethodInfo) FindMethodBase(allMethods, args, out outArgs);

        public static MethodBase FindMethodBase(IEnumerable<MethodBase> allMethods, Type[] args, out Type[] outArgs)
        {
            List<MethodBase> applicable = GetApplicableFunctionMembers(allMethods, args).ToList<MethodBase>();
            outArgs = null;
            if (!applicable.Any<MethodBase>())
            {
                return null;
            }
            MethodBase base2 = null;
            if (applicable.Count<MethodBase>() == 1)
            {
                base2 = applicable.First<MethodBase>();
            }
            else
            {
                base2 = applicable.FirstOrDefault<MethodBase>(x => (from a in applicable
                    where a != x
                    select a).All<MethodBase>(a => IsLeftBetter(args, x, a)));
                if (base2 == null)
                {
                    return null;
                }
            }
            Func<ParameterInfo, Type> selector = <>c.<>9__22_3;
            if (<>c.<>9__22_3 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__22_3;
                selector = <>c.<>9__22_3 = x => x.ParameterType;
            }
            outArgs = GetPreferredForm(args, base2.GetParameters()).Select<ParameterInfo, Type>(selector).ToArray<Type>();
            return base2;
        }

        internal static PropertyInfo FindPropertyCore(Type instanceType, string propertyName, BindingFlags flags) => 
            instanceType.GetProperty(propertyName, flags);

        public static FieldInfo FindStaticField(Type instanceType, string fieldName) => 
            instanceType.GetField(fieldName, StaticBindingFlags);

        public static PropertyInfo FindStaticProperty(Type instanceType, string propertyName) => 
            instanceType.GetProperty(propertyName, StaticBindingFlags);

        public static MethodBase[] GetApplicableFunctionMembers(IEnumerable<MethodBase> allMethods, Type[] args)
        {
            Func<MethodBase, bool> predicate = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<MethodBase, bool> local1 = <>c.<>9__15_0;
                predicate = <>c.<>9__15_0 = x => !x.IsGenericMethodDefinition;
            }
            allMethods = allMethods.Where<MethodBase>(predicate);
            Func<MethodBase, bool> func2 = <>c.<>9__15_1;
            if (<>c.<>9__15_1 == null)
            {
                Func<MethodBase, bool> local2 = <>c.<>9__15_1;
                func2 = <>c.<>9__15_1 = delegate (MethodBase x) {
                    Func<ParameterInfo, bool> func1 = <>c.<>9__15_2;
                    if (<>c.<>9__15_2 == null)
                    {
                        Func<ParameterInfo, bool> local1 = <>c.<>9__15_2;
                        func1 = <>c.<>9__15_2 = p => p.IsOut || p.IsRetval;
                    }
                    return !x.GetParameters().Any<ParameterInfo>(func1);
                };
            }
            allMethods = allMethods.Where<MethodBase>(func2);
            allMethods = from x in allMethods
                where IsApplicableInNormalForm(args, x.GetParameters()) || IsApplicableInExtendedForm(args, x.GetParameters())
                select x;
            return allMethods.ToArray<MethodBase>();
        }

        private static ParameterInfo[] GetExpandedForm(Type[] args, ParameterInfo[] parameters) => 
            (!parameters.Any<ParameterInfo>() || !IsParams(parameters.Last<ParameterInfo>())) ? null : parameters.Take<ParameterInfo>((parameters.Length - 1)).Concat<ParameterInfo>((from _ in Enumerable.Range(0, Math.Max(0, (args.Length - parameters.Length) + 1)) select StripArray(parameters.Last<ParameterInfo>()))).ToArray<ParameterInfo>();

        private static ParameterInfo[] GetPreferredForm(Type[] args, ParameterInfo[] parameters)
        {
            parameters = GetExpandedForm(args, parameters) ?? parameters;
            return parameters.Take<ParameterInfo>(args.Length).ToArray<ParameterInfo>();
        }

        private static bool IsApplicableInExtendedForm(Type[] args, ParameterInfo[] parameters)
        {
            ParameterInfo[] infoArray = GetExpandedForm(args, parameters) ?? parameters;
            return IsApplicableInNormalForm(args, infoArray);
        }

        private static bool IsApplicableInNormalForm(Type[] args, ParameterInfo[] parameters)
        {
            if (args.Length > parameters.Length)
            {
                return false;
            }
            Func<ParameterInfo, bool> predicate = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<ParameterInfo, bool> local1 = <>c.<>9__9_0;
                predicate = <>c.<>9__9_0 = x => x.IsOptional;
            }
            if ((parameters.Length - args.Length) > parameters.Count<ParameterInfo>(predicate))
            {
                return false;
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (!IsImplicitConversion(args[i], parameters[i].ParameterType))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool IsImplicitConversion(Type from, Type to)
        {
            if (from == to)
            {
                return true;
            }
            if ((from == null) && !to.IsValueType)
            {
                return true;
            }
            bool flag = to == typeof(short);
            bool flag2 = to == typeof(int);
            bool flag3 = to == typeof(long);
            bool flag4 = to == typeof(float);
            bool flag5 = to == typeof(double);
            bool flag6 = to == typeof(decimal);
            bool flag7 = to == typeof(ushort);
            bool flag8 = to == typeof(uint);
            bool flag9 = to == typeof(ulong);
            return ((!(from == typeof(sbyte)) || !(((((flag | flag2) | flag3) | flag4) | flag5) | flag6)) ? ((!(from == typeof(byte)) || !((((((((flag | flag7) | flag2) | flag8) | flag3) | flag9) | flag4) | flag5) | flag6)) ? ((!(from == typeof(short)) || !((((flag2 | flag3) | flag4) | flag5) | flag6)) ? ((!(from == typeof(ushort)) || !((((((flag2 | flag8) | flag3) | flag9) | flag4) | flag5) | flag6)) ? ((!(from == typeof(int)) || !(((flag3 | flag4) | flag5) | flag6)) ? ((!(from == typeof(uint)) || !((((flag3 | flag9) | flag4) | flag5) | flag6)) ? ((!(from == typeof(long)) || !((flag4 | flag5) | flag6)) ? ((!(from == typeof(ulong)) || !((flag4 | flag5) | flag6)) ? ((!(from == typeof(char)) || !(((((((flag7 | flag2) | flag8) | flag3) | flag9) | flag4) | flag5) | flag6)) ? (!((from == typeof(float)) & flag5) ? to.IsAssignableFrom(from) : true) : true) : true) : true) : true) : true) : true) : true) : true) : true);
        }

        private static bool IsLeftBetter(Type[] args, MethodBase leftMethod, MethodBase rightMethod)
        {
            ParameterInfo[] preferredForm = GetPreferredForm(args, leftMethod.GetParameters());
            ParameterInfo[] source = GetPreferredForm(args, rightMethod.GetParameters());
            Func<ParameterInfo, Type> selector = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__19_0;
                selector = <>c.<>9__19_0 = x => x.ParameterType;
            }
            if (preferredForm.Select<ParameterInfo, Type>(selector).SequenceEqual<Type>(source.Select<ParameterInfo, Type>(<>c.<>9__19_1 ??= x => x.ParameterType)))
            {
                return IsLeftBetterByTieBreakingRules(args, leftMethod, preferredForm, rightMethod, source);
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (IsLeftConversionBetter(args[i], source[i], preferredForm[i]))
                {
                    return false;
                }
            }
            for (int j = 0; j < args.Length; j++)
            {
                if (IsLeftConversionBetter(args[j], preferredForm[j], source[j]))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsLeftBetterByTieBreakingRules(Type[] args, MethodBase leftMethod, ParameterInfo[] left, MethodBase rightMethod, ParameterInfo[] right) => 
            (leftMethod.IsGenericMethod || !rightMethod.IsGenericMethod) ? (((GetExpandedForm(args, leftMethod.GetParameters()) != null) || (GetExpandedForm(args, rightMethod.GetParameters()) == null)) ? ((leftMethod.GetParameters().Length <= rightMethod.GetParameters().Length) ? ((leftMethod.GetParameters().Length == left.Length) && (rightMethod.GetParameters().Length > args.Length)) : true) : true) : true;

        private static bool IsLeftConversionBetter(Type arg, ParameterInfo left, ParameterInfo right)
        {
            if (arg == null)
            {
                return false;
            }
            if ((arg == left.ParameterType) && (arg != right.ParameterType))
            {
                return true;
            }
            if (IsImplicitConversion(left.ParameterType, right.ParameterType) && !IsImplicitConversion(right.ParameterType, left.ParameterType))
            {
                return true;
            }
            if (left.ParameterType == typeof(sbyte))
            {
                Type[] source = new Type[] { typeof(byte), typeof(ushort), typeof(uint), typeof(ulong) };
                if (source.Contains<Type>(right.ParameterType))
                {
                    return true;
                }
            }
            if (left.ParameterType == typeof(short))
            {
                Type[] source = new Type[] { typeof(ushort), typeof(uint), typeof(ulong) };
                if (source.Contains<Type>(right.ParameterType))
                {
                    return true;
                }
            }
            if (left.ParameterType == typeof(int))
            {
                Type[] source = new Type[] { typeof(uint), typeof(ulong) };
                if (source.Contains<Type>(right.ParameterType))
                {
                    return true;
                }
            }
            return ((left.ParameterType == typeof(long)) && (right.ParameterType == typeof(ulong)));
        }

        public static bool IsParams(ParameterInfo parameter)
        {
            if (!parameter.ParameterType.IsArray)
            {
                return false;
            }
            Func<object, bool> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<object, bool> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = a => a is ParamArrayAttribute;
            }
            return parameter.GetCustomAttributes(true).Any<object>(predicate);
        }

        private static ParameterInfo StripArray(ParameterInfo parameter)
        {
            Type[] typeArguments = new Type[] { parameter.ParameterType.GetElementType() };
            return typeof(Class).MakeGenericType(typeArguments).GetMethod("m").GetParameters().Single<ParameterInfo>();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MemberSearcher.<>c <>9 = new MemberSearcher.<>c();
            public static Func<ParameterInfo, bool> <>9__9_0;
            public static Func<object, bool> <>9__10_0;
            public static Func<MethodBase, bool> <>9__15_0;
            public static Func<ParameterInfo, bool> <>9__15_2;
            public static Func<MethodBase, bool> <>9__15_1;
            public static Func<ParameterInfo, Type> <>9__19_0;
            public static Func<ParameterInfo, Type> <>9__19_1;
            public static Func<ParameterInfo, Type> <>9__22_3;

            internal Type <FindMethodBase>b__22_3(ParameterInfo x) => 
                x.ParameterType;

            internal bool <GetApplicableFunctionMembers>b__15_0(MethodBase x) => 
                !x.IsGenericMethodDefinition;

            internal bool <GetApplicableFunctionMembers>b__15_1(MethodBase x)
            {
                Func<ParameterInfo, bool> predicate = <>9__15_2;
                if (<>9__15_2 == null)
                {
                    Func<ParameterInfo, bool> local1 = <>9__15_2;
                    predicate = <>9__15_2 = p => p.IsOut || p.IsRetval;
                }
                return !x.GetParameters().Any<ParameterInfo>(predicate);
            }

            internal bool <GetApplicableFunctionMembers>b__15_2(ParameterInfo p) => 
                p.IsOut || p.IsRetval;

            internal bool <IsApplicableInNormalForm>b__9_0(ParameterInfo x) => 
                x.IsOptional;

            internal Type <IsLeftBetter>b__19_0(ParameterInfo x) => 
                x.ParameterType;

            internal Type <IsLeftBetter>b__19_1(ParameterInfo x) => 
                x.ParameterType;

            internal bool <IsParams>b__10_0(object a) => 
                a is ParamArrayAttribute;
        }

        private class Class<T>
        {
            public void m(T arg)
            {
            }
        }
    }
}

