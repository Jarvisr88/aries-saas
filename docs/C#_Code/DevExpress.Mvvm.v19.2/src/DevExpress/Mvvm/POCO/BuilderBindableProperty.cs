namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    internal static class BuilderBindableProperty
    {
        public static PropertyBuilder BuildBindableProperty(Type type, TypeBuilder typeBuilder, PropertyInfo propertyInfo, MethodInfo raisePropertyChangedMethod, MethodInfo raisePropertyChangingMethod, IEnumerable<string> relatedProperties)
        {
            // Unresolved stack state at '0000008B'
        }

        private static MethodBuilder BuildBindablePropertyGetter(TypeBuilder type, MethodInfo originalGetter)
        {
            MethodBuilder builder = type.DefineMethod(originalGetter.Name, MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Public);
            builder.SetReturnType(originalGetter.ReturnType);
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Call, originalGetter);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static MethodBuilder BuildBindablePropertySetter(TypeBuilder type, PropertyInfo property, MethodInfo raisePropertyChangedMethod, MethodInfo raisePropertyChangingMethod, MethodInfo propertyChangedMethod, MethodInfo propertyChangingMethod, IEnumerable<string> relatedProperties, bool onChangedFirst)
        {
            MethodInfo setMethod = property.GetSetMethod(true);
            MethodAttributes attributes = ((setMethod.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.Virtual) | MethodAttributes.HideBySig;
            MethodBuilder builder = type.DefineMethod(setMethod.Name, attributes);
            Expression[] arguments = new Expression[] { Expression.Constant(null, typeof(object)), Expression.Constant(null, typeof(object)) };
            Expression<Action> expression = Expression.Lambda<Action>(Expression.Call(null, (MethodInfo) methodof(object.Equals), arguments), new ParameterExpression[0]);
            builder.SetReturnType(typeof(void));
            Type[] parameterTypes = new Type[] { property.PropertyType };
            builder.SetParameters(parameterTypes);
            bool isValueType = property.PropertyType.IsValueType;
            ParameterBuilder builder2 = builder.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.DeclareLocal(property.PropertyType);
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Call, property.GetGetMethod());
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            if (isValueType)
            {
                iLGenerator.Emit(OpCodes.Box, property.PropertyType);
            }
            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (isValueType)
            {
                iLGenerator.Emit(OpCodes.Box, property.PropertyType);
            }
            iLGenerator.Emit(OpCodes.Call, ExpressionHelper.GetMethod(expression));
            iLGenerator.Emit(OpCodes.Brtrue_S, label);
            if (onChangedFirst)
            {
                EmitPropertyChanging(iLGenerator, propertyChangingMethod);
                EmitRaisePropertyChangedOrChanging(iLGenerator, raisePropertyChangingMethod, property.Name);
            }
            else
            {
                EmitRaisePropertyChangedOrChanging(iLGenerator, raisePropertyChangingMethod, property.Name);
                EmitPropertyChanging(iLGenerator, propertyChangingMethod);
            }
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Call, setMethod);
            if (onChangedFirst)
            {
                EmitPropertyChanged(iLGenerator, propertyChangedMethod);
                EmitRaisePropertyChangedOrChanging(iLGenerator, raisePropertyChangedMethod, property.Name);
            }
            else
            {
                EmitRaisePropertyChangedOrChanging(iLGenerator, raisePropertyChangedMethod, property.Name);
                EmitPropertyChanged(iLGenerator, propertyChangedMethod);
            }
            EmitRaiseRelatedPropertyChanged(iLGenerator, raisePropertyChangedMethod, relatedProperties);
            iLGenerator.MarkLabel(label);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static void CheckOnChangedMethod(MethodInfo method, Type propertyType)
        {
            if (!BuilderCommon.CanAccessFromDescendant(method))
            {
                throw new ViewModelSourceException($"Property changed method should be public or protected: {method.Name}.");
            }
            if (method.GetParameters().Length >= 2)
            {
                throw new ViewModelSourceException($"Property changed method cannot have more than one parameter: {method.Name}.");
            }
            if ((method.GetParameters().Length == 1) && (method.GetParameters()[0].ParameterType != propertyType))
            {
                throw new ViewModelSourceException($"Property changed method argument type should match property type: {method.Name}.");
            }
            if (method.ReturnType != typeof(void))
            {
                throw new ViewModelSourceException($"Property changed method cannot have return type: {method.Name}.");
            }
        }

        private static void EmitPropertyChanged(ILGenerator gen, MethodInfo m)
        {
            if (m != null)
            {
                gen.Emit(OpCodes.Ldarg_0);
                if (m.GetParameters().Length == 1)
                {
                    gen.Emit(OpCodes.Ldloc_0);
                }
                gen.Emit(OpCodes.Call, m);
            }
        }

        private static void EmitPropertyChanging(ILGenerator gen, MethodInfo m)
        {
            if (m != null)
            {
                gen.Emit(OpCodes.Ldarg_0);
                if (m.GetParameters().Length == 1)
                {
                    gen.Emit(OpCodes.Ldarg_1);
                }
                gen.Emit(OpCodes.Call, m);
            }
        }

        private static void EmitRaisePropertyChangedOrChanging(ILGenerator gen, MethodInfo m, string pName)
        {
            if (m != null)
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldstr, pName);
                gen.Emit(OpCodes.Call, m);
            }
        }

        private static void EmitRaiseRelatedPropertyChanged(ILGenerator gen, MethodInfo m, IEnumerable<string> relatedProperties)
        {
            if (relatedProperties != null)
            {
                foreach (string str in relatedProperties)
                {
                    EmitRaisePropertyChangedOrChanging(gen, m, str);
                }
            }
        }

        private static MethodInfo GetPropertyChangedMethod(Type type, PropertyInfo propertyInfo, string methodNameSuffix, Func<BindablePropertyAttribute, string> getMethodName, Func<BindablePropertyAttribute, MethodInfo> getMethod)
        {
            BindablePropertyAttribute bindablePropertyAttribute = BuilderCommon.GetBindablePropertyAttribute(propertyInfo);
            if ((bindablePropertyAttribute != null) && (getMethod(bindablePropertyAttribute) != null))
            {
                CheckOnChangedMethod(getMethod(bindablePropertyAttribute), propertyInfo.PropertyType);
                return getMethod(bindablePropertyAttribute);
            }
            bool flag = (bindablePropertyAttribute != null) && !string.IsNullOrEmpty(getMethodName(bindablePropertyAttribute));
            if (!flag && !BuilderCommon.IsAutoImplemented(propertyInfo))
            {
                return null;
            }
            string onChangedMethodName = flag ? getMethodName(bindablePropertyAttribute) : ("On" + propertyInfo.Name + methodNameSuffix);
            MethodInfo[] source = (from x in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                where x.Name == onChangedMethodName
                select x).ToArray<MethodInfo>();
            if (source.Length > 1)
            {
                throw new ViewModelSourceException($"More than one property changed method: {propertyInfo.Name}.");
            }
            if (flag && !source.Any<MethodInfo>())
            {
                throw new ViewModelSourceException($"Property changed method not found: {onChangedMethodName}.");
            }
            source.FirstOrDefault<MethodInfo>().Do<MethodInfo>(delegate (MethodInfo x) {
                CheckOnChangedMethod(x, propertyInfo.PropertyType);
            });
            return source.FirstOrDefault<MethodInfo>();
        }

        private static bool ShouldInvokeOnPropertyChangedMethodsFirst(Type type)
        {
            POCOViewModelAttribute pOCOViewModelAttribute = BuilderCommon.GetPOCOViewModelAttribute(type);
            return ((pOCOViewModelAttribute != null) && pOCOViewModelAttribute.InvokeOnPropertyChangedMethodBeforeRaisingINPC);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BuilderBindableProperty.<>c <>9 = new BuilderBindableProperty.<>c();
            public static Func<BindablePropertyAttribute, string> <>9__0_0;
            public static Func<BindablePropertyAttribute, MethodInfo> <>9__0_1;
            public static Func<BindablePropertyAttribute, string> <>9__0_2;
            public static Func<BindablePropertyAttribute, MethodInfo> <>9__0_3;

            internal string <BuildBindableProperty>b__0_0(BindablePropertyAttribute x) => 
                x.OnPropertyChangedMethodName;

            internal MethodInfo <BuildBindableProperty>b__0_1(BindablePropertyAttribute x) => 
                x.OnPropertyChangedMethod;

            internal string <BuildBindableProperty>b__0_2(BindablePropertyAttribute x) => 
                x.OnPropertyChangingMethodName;

            internal MethodInfo <BuildBindableProperty>b__0_3(BindablePropertyAttribute x) => 
                x.OnPropertyChangingMethod;
        }
    }
}

