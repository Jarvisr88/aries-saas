namespace DevExpress.Xpf.Core.ReflectionExtensions.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public static class TypeGeneratorHelper
    {
        public static readonly Type TypeofPObject = typeof(object).Assembly.GetType(typeof(object).FullName + "&");
        private static MethodInfo getTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Public | BindingFlags.Static);

        public static void EmitCall(ILGenerator generator, OpCode opCode, MethodInfo info, Type[] types)
        {
            if (info.CallingConvention.HasFlag(CallingConventions.VarArgs))
            {
                generator.EmitCall(opCode, info, types);
            }
            else
            {
                generator.Emit(opCode, info);
            }
        }

        [IteratorStateMachine(typeof(<FlatternType>d__4))]
        public static IEnumerable<Type> FlatternType(Type t, bool flatternInterfaces)
        {
            Type[] interfaces;
            int <>7__wrap2;
            if (t == null)
            {
            }
            if (!flatternInterfaces)
            {
                goto TR_0004;
            }
            else
            {
                interfaces = t.GetInterfaces();
                <>7__wrap2 = 0;
            }
        Label_PostSwitchInIterator:;
            if (<>7__wrap2 < interfaces.Length)
            {
                Type type = interfaces[<>7__wrap2];
                yield return type;
                <>7__wrap2++;
                goto Label_PostSwitchInIterator;
            }
            interfaces = null;
        TR_0004:
            if (t == null)
            {
            }
            yield return t;
            t = t.BaseType;
            goto TR_0004;
        }

        public static MethodInfo GetMethod(Type type, string sourceMethodName, BindingFlags bindingFlags, MethodInfo targetMethodInfo)
        {
            Func<Tuple<int, MethodInfo>, int> keySelector = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<Tuple<int, MethodInfo>, int> local1 = <>c.<>9__5_0;
                keySelector = <>c.<>9__5_0 = x => x.Item1;
            }
            Tuple<int, MethodInfo> tuple = GetMethods(type, sourceMethodName, bindingFlags, targetMethodInfo).OrderBy<Tuple<int, MethodInfo>, int>(keySelector).FirstOrDefault<Tuple<int, MethodInfo>>();
            return tuple?.Item2;
        }

        [IteratorStateMachine(typeof(<GetMethods>d__6))]
        private static IEnumerable<Tuple<int, MethodInfo>> GetMethods(Type type, string sourceMethodName, BindingFlags bindingFlags, MethodInfo targetMethodInfo)
        {
            MethodInfo info;
            int num2;
            ParameterInfo[] parameters;
            int num3;
            string sourceMethodName = sourceMethodName;
            MethodInfo[] <methods>5__1 = (from x in type.GetMethods(bindingFlags)
                where x.Name == sourceMethodName
                select x).ToArray<MethodInfo>();
            if (<methods>5__1.Length == 0)
            {
            }
            if (<methods>5__1.Length == 1)
            {
                yield return new Tuple<int, MethodInfo>(0, <methods>5__1[0]);
            }
            ParameterInfo[] <args>5__2 = targetMethodInfo.GetParameters();
            MethodInfo[] <>7__wrap1 = <methods>5__1;
            int index = 0;
            goto TR_002C;
        TR_0006:
            num3++;
            goto TR_0027;
        TR_0018:
            index++;
            goto TR_002C;
        TR_0027:
            while (true)
            {
                if (num3 >= <args>5__2.Length)
                {
                    yield return new Tuple<int, MethodInfo>(num2, info);
                    break;
                }
                Type currentTargetArg = <args>5__2[num3].ParameterType;
                Type c = parameters[num3].ParameterType;
                if (currentTargetArg != c)
                {
                    if (!currentTargetArg.IsAssignableFrom(c))
                    {
                        if (ShouldWrapType(currentTargetArg))
                        {
                            currentTargetArg = GetNonReferenceType(currentTargetArg);
                            string fullName = GetNonReferenceType(c).FullName;
                            Func<AssignableFromAttribute, bool> predicate = <>c.<>9__6_2;
                            if (<>c.<>9__6_2 == null)
                            {
                                Func<AssignableFromAttribute, bool> local2 = <>c.<>9__6_2;
                                predicate = <>c.<>9__6_2 = x => (x != null) && !x.Inverse;
                            }
                            using (IEnumerator<AssignableFromAttribute> enumerator = currentTargetArg.GetCustomAttributes(typeof(AssignableFromAttribute), true).OfType<AssignableFromAttribute>().Where<AssignableFromAttribute>(predicate).GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator.MoveNext())
                                    {
                                        break;
                                    }
                                    AssignableFromAttribute current = enumerator.Current;
                                    if (fullName == current.GetTypeName())
                                    {
                                        goto TR_0006;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        bool isInterface = currentTargetArg.IsInterface;
                        Type baseType = c;
                        do
                        {
                            if (isInterface)
                            {
                                Func<Type, bool> <>9__1;
                                Func<Type, bool> predicate = <>9__1;
                                if (<>9__1 == null)
                                {
                                    Func<Type, bool> local1 = <>9__1;
                                    predicate = <>9__1 = x => currentTargetArg.IsAssignableFrom(x);
                                }
                                Type baseType = baseType.GetInterfaces().FirstOrDefault<Type>(predicate);
                                if (baseType != null)
                                {
                                    int num4 = 1;
                                    while (true)
                                    {
                                        if (!(baseType != currentTargetArg) || (baseType == null))
                                        {
                                            num2 += num4;
                                            break;
                                        }
                                        num4++;
                                        baseType = baseType.BaseType;
                                    }
                                    break;
                                }
                            }
                            num2++;
                            baseType = baseType.BaseType;
                        }
                        while (baseType != currentTargetArg);
                    }
                }
                goto TR_0006;
            }
            goto TR_0018;
        TR_002C:
            while (true)
            {
                if (index >= <>7__wrap1.Length)
                {
                    <>7__wrap1 = null;
                }
                info = <>7__wrap1[index];
                num2 = 0;
                parameters = info.GetParameters();
                if (parameters.Length != <args>5__2.Length)
                {
                    goto TR_0018;
                }
                else
                {
                    num3 = 0;
                }
                break;
            }
            goto TR_0027;
        }

        public static Type GetNonReferenceType(Type byRef) => 
            byRef.IsByRef ? byRef.GetElementType() : byRef;

        public static void Ldfld(ILGenerator generator, FieldInfo fieldBuilder)
        {
            if (fieldBuilder != null)
            {
                if (!fieldBuilder.IsStatic)
                {
                    generator.Emit(OpCodes.Ldarg_0);
                }
                generator.Emit(OpCodes.Ldfld, fieldBuilder);
            }
        }

        public static void LSTind(ILGenerator generator, Type type, bool stind)
        {
            OpCode opcode = OpCodes.Stind_Ref;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    opcode = stind ? OpCodes.Stind_I1 : OpCodes.Ldind_U1;
                    break;

                case TypeCode.Char:
                    opcode = stind ? OpCodes.Stind_I2 : OpCodes.Ldind_U2;
                    break;

                case TypeCode.SByte:
                case TypeCode.Byte:
                    opcode = stind ? OpCodes.Stind_I1 : OpCodes.Ldind_I1;
                    break;

                case TypeCode.Int16:
                case TypeCode.UInt16:
                    opcode = stind ? OpCodes.Stind_I2 : OpCodes.Ldind_I2;
                    break;

                case TypeCode.Int32:
                case TypeCode.UInt32:
                    opcode = stind ? OpCodes.Stind_I4 : OpCodes.Ldind_I4;
                    break;

                case TypeCode.Int64:
                    opcode = stind ? OpCodes.Stind_I8 : OpCodes.Ldind_I8;
                    break;

                case TypeCode.UInt64:
                    opcode = stind ? OpCodes.Stind_I8 : OpCodes.Ldind_I8;
                    break;

                case TypeCode.Single:
                    opcode = stind ? OpCodes.Stind_R4 : OpCodes.Ldind_R4;
                    break;

                case TypeCode.Double:
                    opcode = stind ? OpCodes.Stind_R8 : OpCodes.Ldind_R8;
                    break;

                default:
                    if (!type.IsClass)
                    {
                        generator.Emit(stind ? OpCodes.Stobj : OpCodes.Ldobj, type);
                        return;
                    }
                    if (!stind)
                    {
                        opcode = OpCodes.Ldind_Ref;
                    }
                    break;
            }
            generator.Emit(opcode);
        }

        public static bool ShouldWrapDelegate(Type currentType, Type desiredType) => 
            (desiredType != null) && (!(desiredType == currentType) && typeof(Delegate).IsAssignableFrom(currentType));

        public static bool ShouldWrapType(Type type) => 
            !type.IsByRef ? type.GetCustomAttributes(typeof(WrapperAttribute), false).Any<object>() : ShouldWrapType(type.GetElementType());

        public static void TypeOf(ILGenerator generator, Type type)
        {
            generator.Emit(OpCodes.Ldtoken, type);
            generator.Emit(OpCodes.Call, getTypeFromHandle);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypeGeneratorHelper.<>c <>9 = new TypeGeneratorHelper.<>c();
            public static Func<Tuple<int, MethodInfo>, int> <>9__5_0;
            public static Func<AssignableFromAttribute, bool> <>9__6_2;

            internal int <GetMethod>b__5_0(Tuple<int, MethodInfo> x) => 
                x.Item1;

            internal bool <GetMethods>b__6_2(AssignableFromAttribute x) => 
                (x != null) && !x.Inverse;
        }


    }
}

