namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal static class BuilderINPC
    {
        private static void BuildAddRemoveEventHandlers(TypeBuilder type, MethodInfo getHelperMethod, INPCInfo inpcInfo)
        {
            Func<string, Expression<Action>, MethodBuilder> func = delegate (string methodName, Expression<Action> handlerExpression) {
                string[] textArray1 = new string[] { inpcInfo.InterfaceType.FullName, ".", methodName, "_", inpcInfo.EventName };
                MethodBuilder builder = type.DefineMethod(string.Concat(textArray1), MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private);
                builder.SetReturnType(typeof(void));
                Type[] parameterTypes = new Type[] { inpcInfo.EventHandlerType };
                builder.SetParameters(parameterTypes);
                ParameterBuilder builder2 = builder.DefineParameter(1, ParameterAttributes.None, "value");
                ILGenerator iLGenerator = builder.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Call, getHelperMethod);
                iLGenerator.Emit(OpCodes.Ldarg_1);
                iLGenerator.Emit(OpCodes.Callvirt, ExpressionHelper.GetMethod(handlerExpression));
                iLGenerator.Emit(OpCodes.Ret);
                return builder;
            };
            MethodBuilder methodInfoBody = func("add", inpcInfo.AddEventHandlerHelperMethod);
            type.DefineMethodOverride(methodInfoBody, inpcInfo.GetAddEventMethod());
            MethodBuilder builder2 = func("remove", inpcInfo.RemoveEventHandlerHelperMethod);
            type.DefineMethodOverride(builder2, inpcInfo.GetRemoveEventMethod());
        }

        private static MethodBuilder BuildMethod_GetEventHelper(TypeBuilder type)
        {
            FieldBuilder field = type.DefineField("_inpcEventHelper_", typeof(INPCEventHelper), FieldAttributes.Private);
            MethodBuilder builder2 = type.DefineMethod("_GetINPCEventHelper_", MethodAttributes.HideBySig | MethodAttributes.Private);
            ConstructorInfo constructor = ExpressionHelper.GetConstructor<INPCEventHelper>(Expression.Lambda<Func<INPCEventHelper>>(Expression.New(typeof(INPCEventHelper)), new ParameterExpression[0]));
            builder2.SetReturnType(typeof(INPCEventHelper));
            ILGenerator iLGenerator = builder2.GetILGenerator();
            iLGenerator.DeclareLocal(typeof(INPCEventHelper));
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, field);
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Brtrue_S, label);
            iLGenerator.Emit(OpCodes.Pop);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Newobj, constructor);
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Stfld, field);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.MarkLabel(label);
            iLGenerator.Emit(OpCodes.Ret);
            return builder2;
        }

        private static MethodBuilder BuildRaiseEventHandlerMethod(TypeBuilder type, MethodInfo getHelperMethod, INPCInfo inpcInfo)
        {
            MethodBuilder builder = type.DefineMethod(inpcInfo.RaiseEventMethodName, MethodAttributes.HideBySig | MethodAttributes.Family);
            builder.SetReturnType(typeof(void));
            Type[] parameterTypes = new Type[] { typeof(string) };
            builder.SetParameters(parameterTypes);
            ParameterBuilder builder2 = builder.DefineParameter(1, ParameterAttributes.None, "propertyName");
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Call, getHelperMethod);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Callvirt, ExpressionHelper.GetMethod(inpcInfo.RaiseEventHandlerHelperMethod));
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static MethodInfo CheckExistingMethod_RaiseEvent(INPCInfo inpcInfo, Type baseType)
        {
            if (!inpcInfo.InterfaceType.IsAssignableFrom(baseType))
            {
                return null;
            }
            MethodInfo info = baseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault<MethodInfo>(delegate (MethodInfo x) {
                ParameterInfo[] parameters = x.GetParameters();
                return (x.Name == inpcInfo.RaiseEventMethodName) && (BuilderCommon.CanAccessFromDescendant(x) && ((parameters.Length == 1) && ((parameters[0].ParameterType == typeof(string)) && (!parameters[0].IsOut && !parameters[0].ParameterType.IsByRef))));
            });
            if (info == null)
            {
                throw new ViewModelSourceException(string.Format(inpcInfo.RaiseEventMethodNotFoundException, baseType.Name));
            }
            return info;
        }

        public static void ImplementINPC(Type baseType, TypeBuilder typeBuilder, out MethodInfo raisePropertyChanged, out MethodInfo raisePropertyChanging)
        {
            raisePropertyChanged = null;
            raisePropertyChanging = null;
            MethodBuilder _getHelper = null;
            Func<MethodBuilder> getHelper = delegate {
                MethodBuilder builder2 = _getHelper;
                if (_getHelper == null)
                {
                    MethodBuilder local1 = _getHelper;
                    builder2 = _getHelper = BuildMethod_GetEventHelper(typeBuilder);
                }
                return builder2;
            };
            Func<INPCInfo, MethodInfo> func = delegate (INPCInfo inpcInfo) {
                MethodInfo info = CheckExistingMethod_RaiseEvent(inpcInfo, baseType);
                if (info == null)
                {
                    info = BuildRaiseEventHandlerMethod(typeBuilder, getHelper(), inpcInfo);
                    BuildAddRemoveEventHandlers(typeBuilder, getHelper(), inpcInfo);
                }
                return info;
            };
            raisePropertyChanged = func(INPCInfo.INPChangedInfo);
            if (ImplementINPChanging(baseType))
            {
                raisePropertyChanging = func(INPCInfo.INPChangingInfo);
            }
        }

        private static bool ImplementINPChanging(Type type)
        {
            POCOViewModelAttribute pOCOViewModelAttribute = BuilderCommon.GetPOCOViewModelAttribute(type);
            return ((pOCOViewModelAttribute != null) && pOCOViewModelAttribute.ImplementINotifyPropertyChanging);
        }

        private class INPCInfo
        {
            public static readonly BuilderINPC.INPCInfo INPChangedInfo;
            public static readonly BuilderINPC.INPCInfo INPChangingInfo;

            static INPCInfo()
            {
                BuilderINPC.INPCInfo info = new BuilderINPC.INPCInfo {
                    InterfaceType = typeof(INotifyPropertyChanged),
                    EventHandlerType = typeof(PropertyChangedEventHandler),
                    EventName = "PropertyChanged",
                    RaiseEventMethodName = "RaisePropertyChanged",
                    RaiseEventMethodNotFoundException = "Class already supports INotifyPropertyChanged, but RaisePropertyChanged(string) method not found: {0}."
                };
                Expression[] arguments = new Expression[] { Expression.Constant(null, typeof(PropertyChangedEventHandler)) };
                info.AddEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.AddPropertyChangedHandler), arguments), new ParameterExpression[0]);
                Expression[] expressionArray2 = new Expression[] { Expression.Constant(null, typeof(PropertyChangedEventHandler)) };
                info.RemoveEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.RemovePropertyChangedHandler), expressionArray2), new ParameterExpression[0]);
                Expression[] expressionArray3 = new Expression[] { Expression.Constant(null, typeof(INotifyPropertyChanged)), Expression.Constant(null, typeof(string)) };
                info.RaiseEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.OnPropertyChanged), expressionArray3), new ParameterExpression[0]);
                INPChangedInfo = info;
                info = new BuilderINPC.INPCInfo {
                    InterfaceType = typeof(INotifyPropertyChanging),
                    EventHandlerType = typeof(PropertyChangingEventHandler),
                    EventName = "PropertyChanging",
                    RaiseEventMethodName = "RaisePropertyChanging",
                    RaiseEventMethodNotFoundException = "Class already supports INotifyPropertyChanging, but RaisePropertyChanging(string) method not found: {0}."
                };
                Expression[] expressionArray4 = new Expression[] { Expression.Constant(null, typeof(PropertyChangingEventHandler)) };
                info.AddEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.AddPropertyChangingHandler), expressionArray4), new ParameterExpression[0]);
                Expression[] expressionArray5 = new Expression[] { Expression.Constant(null, typeof(PropertyChangingEventHandler)) };
                info.RemoveEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.RemovePropertyChangingHandler), expressionArray5), new ParameterExpression[0]);
                Expression[] expressionArray6 = new Expression[] { Expression.Constant(null, typeof(INotifyPropertyChanging)), Expression.Constant(null, typeof(string)) };
                info.RaiseEventHandlerHelperMethod = Expression.Lambda<Action>(Expression.Call(Expression.New(typeof(INPCEventHelper)), (MethodInfo) methodof(INPCEventHelper.OnPropertyChanging), expressionArray6), new ParameterExpression[0]);
                INPChangingInfo = info;
            }

            private INPCInfo()
            {
            }

            public MethodInfo GetAddEventMethod() => 
                this.InterfaceType.GetMethod("add_" + this.EventName);

            public MethodInfo GetRemoveEventMethod() => 
                this.InterfaceType.GetMethod("remove_" + this.EventName);

            public Type InterfaceType { get; private set; }

            public Type EventHandlerType { get; private set; }

            public string EventName { get; private set; }

            public string RaiseEventMethodName { get; private set; }

            public string RaiseEventMethodNotFoundException { get; private set; }

            public Expression<Action> AddEventHandlerHelperMethod { get; private set; }

            public Expression<Action> RemoveEventHandlerHelperMethod { get; private set; }

            public Expression<Action> RaiseEventHandlerHelperMethod { get; private set; }
        }
    }
}

