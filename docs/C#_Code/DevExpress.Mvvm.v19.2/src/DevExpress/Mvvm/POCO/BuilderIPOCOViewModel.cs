namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;

    internal static class BuilderIPOCOViewModel
    {
        public static void ImplementIPOCOViewModel(Type baseType, TypeBuilder typeBuilder, MethodInfo raisePropertyChangedMethod, MethodInfo raisePropertyChangingMethod)
        {
            Expression[] arguments = new Expression[] { Expression.Constant(null, typeof(string)) };
            Expression<Action> expression = Expression.Lambda<Action>(Expression.Call(Expression.Constant(null, typeof(IPOCOViewModel)), (MethodInfo) methodof(IPOCOViewModel.RaisePropertyChanged), arguments), new ParameterExpression[0]);
            typeBuilder.DefineMethodOverride(RaiseEventMethod(baseType, typeBuilder, raisePropertyChangedMethod, "RaisePropertyChanged"), ExpressionHelper.GetMethod(expression));
            Expression[] expressionArray2 = new Expression[] { Expression.Constant(null, typeof(string)) };
            Expression<Action> expression2 = Expression.Lambda<Action>(Expression.Call(Expression.Constant(null, typeof(IPOCOViewModel)), (MethodInfo) methodof(IPOCOViewModel.RaisePropertyChanging), expressionArray2), new ParameterExpression[0]);
            typeBuilder.DefineMethodOverride(RaiseEventMethod(baseType, typeBuilder, raisePropertyChangingMethod, "RaisePropertyChanging"), ExpressionHelper.GetMethod(expression2));
        }

        private static MethodBuilder RaiseEventMethod(Type baseType, TypeBuilder type, MethodInfo raiseMethod, string methodName)
        {
            MethodBuilder builder = type.DefineMethod("DevExpress.Mvvm.Native.IPOCOViewModel" + methodName, MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private);
            builder.SetReturnType(typeof(void));
            Type[] parameterTypes = new Type[] { typeof(string) };
            builder.SetParameters(parameterTypes);
            ParameterBuilder builder2 = builder.DefineParameter(1, ParameterAttributes.None, "propertyName");
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            if (raiseMethod != null)
            {
                iLGenerator.Emit(OpCodes.Call, raiseMethod);
            }
            else
            {
                Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(string)) };
                ConstructorInfo constructor = ExpressionHelper.GetConstructor<ViewModelSourceException>(Expression.Lambda<Func<ViewModelSourceException>>(Expression.New((ConstructorInfo) methodof(ViewModelSourceException..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]));
                iLGenerator.Emit(OpCodes.Ldstr, $"The INotifyPropertyChanging interface is not implemented or implemented explicitly: {baseType.Name}");
                iLGenerator.Emit(OpCodes.Newobj, constructor);
                iLGenerator.Emit(OpCodes.Throw);
            }
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }
    }
}

