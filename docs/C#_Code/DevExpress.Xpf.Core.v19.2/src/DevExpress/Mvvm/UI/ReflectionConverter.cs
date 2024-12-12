namespace DevExpress.Mvvm.UI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class ReflectionConverter : IValueConverter
    {
        private Type convertBackMethodOwner = typeof(TypeUnsetValue);
        private static readonly ConvertMethodSignature[] ConvertMethodSignatures;

        static ReflectionConverter()
        {
            ConvertMethodSignature[] signatureArray1 = new ConvertMethodSignature[8];
            signatureArray1[0] = new ConvertMethodSignature(new Type[1], 0, -1, -1, -1);
            Type[] parameterTypes = new Type[2];
            parameterTypes[1] = typeof(CultureInfo);
            signatureArray1[1] = new ConvertMethodSignature(parameterTypes, 0, -1, -1, 1);
            Type[] typeArray2 = new Type[2];
            typeArray2[1] = typeof(Type);
            signatureArray1[2] = new ConvertMethodSignature(typeArray2, 0, 1, -1, -1);
            signatureArray1[3] = new ConvertMethodSignature(new Type[2], 0, -1, 1, -1);
            Type[] typeArray3 = new Type[3];
            typeArray3[1] = typeof(Type);
            signatureArray1[4] = new ConvertMethodSignature(typeArray3, 0, 1, 2, -1);
            Type[] typeArray4 = new Type[3];
            typeArray4[1] = typeof(Type);
            typeArray4[2] = typeof(CultureInfo);
            signatureArray1[5] = new ConvertMethodSignature(typeArray4, 0, 1, -1, 2);
            Type[] typeArray5 = new Type[3];
            typeArray5[2] = typeof(CultureInfo);
            signatureArray1[6] = new ConvertMethodSignature(typeArray5, 0, -1, 1, 2);
            Type[] typeArray6 = new Type[4];
            typeArray6[1] = typeof(Type);
            typeArray6[3] = typeof(CultureInfo);
            signatureArray1[7] = new ConvertMethodSignature(typeArray6, 0, 1, 2, 3);
            ConvertMethodSignatures = signatureArray1;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertCore(value, targetType, parameter, culture, this.ConvertMethodOwner, this.ConvertMethod);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertCore(value, targetType, parameter, culture, this.ConvertBackMethodOwner, this.ConvertBackMethod);

        private static object ConvertByConstructor(object value, Type targetType, object parameter, CultureInfo culture, IEnumerable<ConstructorInfo> methods)
        {
            if ((value == null) && ((targetType == null) || (!targetType.IsValueType || (targetType.IsGenericType && (targetType.GetGenericTypeDefinition() == typeof(Nullable<>))))))
            {
                return null;
            }
            Func<ConstructorInfo, bool> predicate = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__24_0;
                predicate = <>c.<>9__24_0 = c => c.GetParameters().Length == 1;
            }
            ConstructorInfo info = methods.Where<ConstructorInfo>(predicate).FirstOrDefault<ConstructorInfo>();
            if (info == null)
            {
                Func<ConstructorInfo, bool> func2 = <>c.<>9__24_1;
                if (<>c.<>9__24_1 == null)
                {
                    Func<ConstructorInfo, bool> local2 = <>c.<>9__24_1;
                    func2 = <>c.<>9__24_1 = delegate (ConstructorInfo c) {
                        if (c.GetParameters().Length == 0)
                        {
                            return false;
                        }
                        Func<ParameterInfo, bool> func1 = <>c.<>9__24_2;
                        if (<>c.<>9__24_2 == null)
                        {
                            Func<ParameterInfo, bool> local1 = <>c.<>9__24_2;
                            func1 = <>c.<>9__24_2 = p => !p.IsOptional;
                        }
                        return !c.GetParameters().Skip<ParameterInfo>(1).Any<ParameterInfo>(func1);
                    };
                }
                info = methods.Where<ConstructorInfo>(func2).FirstOrDefault<ConstructorInfo>();
            }
            if (info == null)
            {
                throw new InvalidOperationException();
            }
            ParameterInfo[] parameters = info.GetParameters();
            object[] array = new object[] { value };
            Func<ParameterInfo, object> selector = <>c.<>9__24_3;
            if (<>c.<>9__24_3 == null)
            {
                Func<ParameterInfo, object> local3 = <>c.<>9__24_3;
                selector = <>c.<>9__24_3 = p => p.DefaultValue;
            }
            parameters.Skip<ParameterInfo>(1).Select<ParameterInfo, object>(selector).ToArray<object>().CopyTo(array, 1);
            return info.Invoke(array);
        }

        private static object ConvertByConstructor(object value, Type targetType, object parameter, CultureInfo culture, Type convertMethodOwner) => 
            ConvertByConstructor(value, targetType, parameter, culture, convertMethodOwner.GetConstructors());

        private static object ConvertBySourceValueMethod(object value, Type targetType, object parameter, CultureInfo culture, string convertMethodName)
        {
            MethodInfo method = value.GetType().GetMethod(convertMethodName, new Type[0]);
            if (method == null)
            {
                method = value.GetType().GetMethods().Where<MethodInfo>(delegate (MethodInfo c) {
                    if ((c.Name != convertMethodName) || (c.GetParameters().Length == 0))
                    {
                        return false;
                    }
                    Func<ParameterInfo, bool> predicate = <>c.<>9__25_1;
                    if (<>c.<>9__25_1 == null)
                    {
                        Func<ParameterInfo, bool> local1 = <>c.<>9__25_1;
                        predicate = <>c.<>9__25_1 = p => !p.IsOptional;
                    }
                    return !c.GetParameters().Any<ParameterInfo>(predicate);
                }).FirstOrDefault<MethodInfo>();
            }
            if (method == null)
            {
                throw new InvalidOperationException();
            }
            Func<ParameterInfo, object> selector = <>c.<>9__25_2;
            if (<>c.<>9__25_2 == null)
            {
                Func<ParameterInfo, object> local1 = <>c.<>9__25_2;
                selector = <>c.<>9__25_2 = p => p.DefaultValue;
            }
            object[] parameters = method.GetParameters().Select<ParameterInfo, object>(selector).ToArray<object>();
            return method.Invoke(value, parameters);
        }

        private static object ConvertByStaticMethod(object value, Type targetType, object parameter, CultureInfo culture, Type convertMethodOwner, string convertMethodName)
        {
            Tuple<MethodInfo, ConvertMethodSignature> method = GetMethod(from m in convertMethodOwner.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                where m.Name == convertMethodName
                select m);
            if (method == null)
            {
                throw new InvalidOperationException();
            }
            ParameterInfo[] parameters = method.Item1.GetParameters();
            object[] args = new object[parameters.Length];
            method.Item2.AssignArgs(args, value, targetType, parameter, culture);
            for (int i = method.Item2.ParameterTypes.Length; i < args.Length; i++)
            {
                args[i] = parameters[i].DefaultValue;
            }
            return method.Item1.Invoke(null, args);
        }

        private static object ConvertByTargetTypeConstructor(object value, Type targetType, object parameter, CultureInfo culture) => 
            ConvertByConstructor(value, targetType, parameter, culture, targetType.GetConstructors());

        private static object ConvertCore(object value, Type targetType, object parameter, CultureInfo culture, Type convertMethodOwner, string convertMethod) => 
            (convertMethodOwner != null) ? ((convertMethod != null) ? ConvertByStaticMethod(value, targetType, parameter, culture, convertMethodOwner, convertMethod) : ConvertByConstructor(value, targetType, parameter, culture, convertMethodOwner)) : ((convertMethod != null) ? ((value == null) ? null : ConvertBySourceValueMethod(value, targetType, parameter, culture, convertMethod)) : ((targetType == null) ? value : ConvertByTargetTypeConstructor(value, targetType, parameter, culture)));

        private static Tuple<MethodInfo, ConvertMethodSignature> GetMethod(IEnumerable<MethodInfo> methods)
        {
            Tuple<MethodInfo, ConvertMethodSignature> tuple;
            using (IEnumerator<MethodInfo> enumerator = methods.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        MethodInfo current = enumerator.Current;
                        ParameterInfo[] parameters = current.GetParameters();
                        ConvertMethodSignature signature = (from v in ConvertMethodSignatures
                            where Match(parameters, v.ParameterTypes)
                            select v).FirstOrDefault<ConvertMethodSignature>();
                        if (signature == null)
                        {
                            continue;
                        }
                        tuple = new Tuple<MethodInfo, ConvertMethodSignature>(current, signature);
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return tuple;
        }

        private static bool Match(ParameterInfo[] parameterInfo, Type[] parameterTypes)
        {
            if (parameterTypes.Length > parameterInfo.Length)
            {
                return false;
            }
            for (int i = parameterTypes.Length; i < parameterInfo.Length; i++)
            {
                if (!parameterInfo[i].IsOptional)
                {
                    return false;
                }
            }
            for (int j = 0; j < parameterTypes.Length; j++)
            {
                if (!Match(parameterInfo[j].ParameterType, parameterTypes[j]))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool Match(Type actual, Type expected) => 
            (expected == null) || (actual == expected);

        public Type ConvertMethodOwner { get; set; }

        public string ConvertMethod { get; set; }

        public Type ConvertBackMethodOwner
        {
            get => 
                (this.convertBackMethodOwner == typeof(TypeUnsetValue)) ? this.ConvertMethodOwner : this.convertBackMethodOwner;
            set => 
                this.convertBackMethodOwner = value;
        }

        public string ConvertBackMethod { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReflectionConverter.<>c <>9 = new ReflectionConverter.<>c();
            public static Func<ConstructorInfo, bool> <>9__24_0;
            public static Func<ParameterInfo, bool> <>9__24_2;
            public static Func<ConstructorInfo, bool> <>9__24_1;
            public static Func<ParameterInfo, object> <>9__24_3;
            public static Func<ParameterInfo, bool> <>9__25_1;
            public static Func<ParameterInfo, object> <>9__25_2;

            internal bool <ConvertByConstructor>b__24_0(ConstructorInfo c) => 
                c.GetParameters().Length == 1;

            internal bool <ConvertByConstructor>b__24_1(ConstructorInfo c)
            {
                if (c.GetParameters().Length == 0)
                {
                    return false;
                }
                Func<ParameterInfo, bool> predicate = <>9__24_2;
                if (<>9__24_2 == null)
                {
                    Func<ParameterInfo, bool> local1 = <>9__24_2;
                    predicate = <>9__24_2 = p => !p.IsOptional;
                }
                return !c.GetParameters().Skip<ParameterInfo>(1).Any<ParameterInfo>(predicate);
            }

            internal bool <ConvertByConstructor>b__24_2(ParameterInfo p) => 
                !p.IsOptional;

            internal object <ConvertByConstructor>b__24_3(ParameterInfo p) => 
                p.DefaultValue;

            internal bool <ConvertBySourceValueMethod>b__25_1(ParameterInfo p) => 
                !p.IsOptional;

            internal object <ConvertBySourceValueMethod>b__25_2(ParameterInfo p) => 
                p.DefaultValue;
        }

        private class ConvertMethodSignature
        {
            private int valueIndex;
            private int targetTypeIndex;
            private int parameterIndex;
            private int cultureIndex;

            public ConvertMethodSignature(Type[] parameterTypes, int valueIndex, int targetTypeIndex, int parameterIndex, int cultureIndex)
            {
                this.ParameterTypes = parameterTypes;
                this.valueIndex = valueIndex;
                this.targetTypeIndex = targetTypeIndex;
                this.parameterIndex = parameterIndex;
                this.cultureIndex = cultureIndex;
            }

            public void AssignArgs(object[] args, object value, Type targetType, object parameter, CultureInfo culture)
            {
                args[this.valueIndex] = value;
                if (this.targetTypeIndex >= 0)
                {
                    args[this.targetTypeIndex] = targetType;
                }
                if (this.parameterIndex >= 0)
                {
                    args[this.parameterIndex] = parameter;
                }
                if (this.cultureIndex >= 0)
                {
                    args[this.cultureIndex] = culture;
                }
            }

            public Type[] ParameterTypes { get; private set; }
        }

        private class TypeUnsetValue
        {
        }
    }
}

