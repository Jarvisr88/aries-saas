namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class ViewModelSource
    {
        [ThreadStatic]
        private static Dictionary<Type, ICustomAttributeBuilderProvider> attributeBuilderProviders;
        [ThreadStatic]
        private static Dictionary<Type, Type> types;
        [ThreadStatic]
        private static Dictionary<Type, object> factories;

        static ViewModelSource()
        {
            RegisterAttributeBuilderProvider(new CommandParameterAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new DXImageAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new ImageAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new ToolBarItemAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new ContextMenuItemAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new DisplayAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new DisplayNameAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new ScaffoldColumnAttributeBuilderProvider());
            RegisterAttributeBuilderProvider(new BrowsableAttributeBuilderProvider());
        }

        private static void BuildCommandPropertyAttributes(PropertyBuilder commandProperty, MethodInfo commandMethod)
        {
            foreach (Attribute attribute in MetadataHelper.GetAllAttributes(commandMethod, false))
            {
                ICustomAttributeBuilderProvider provider;
                if (AttributeBuilderProviders.TryGetValue(attribute.GetType(), out provider))
                {
                    commandProperty.SetCustomAttribute(provider.CreateAttributeBuilder(attribute));
                }
            }
        }

        private static void BuildCommands(Type type, TypeBuilder typeBuilder)
        {
            MethodInfo[] source = GetCommandMethods(type).ToArray<MethodInfo>();
            foreach (MethodInfo commandMethod in source)
            {
                bool? nullable1;
                CommandAttribute attribute = ViewModelBase.GetAttribute<CommandAttribute>(commandMethod);
                bool isAsyncCommand = commandMethod.ReturnType == typeof(Task);
                string commandName = GetCommandName(commandMethod);
                if (type.GetMember(commandName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Any<MemberInfo>() || source.Any<MethodInfo>(x => ((GetCommandName(x) == commandName) && (x != commandMethod))))
                {
                    throw new ViewModelSourceException($"Member with the same command name already exists: {commandName}.");
                }
                MethodInfo canExecuteMethod = GetCanExecuteMethod(type, commandMethod);
                if (attribute != null)
                {
                    nullable1 = attribute.GetUseCommandManager();
                }
                else
                {
                    nullable1 = null;
                }
                bool? useCommandManager = nullable1;
                MethodBuilder mdBuilder = BuildGetCommandMethod(typeBuilder, commandMethod, canExecuteMethod, commandName, useCommandManager, isAsyncCommand);
                PropertyBuilder commandProperty = typeBuilder.DefineProperty(commandName, PropertyAttributes.None, mdBuilder.ReturnType, null);
                commandProperty.SetGetMethod(mdBuilder);
                BuildCommandPropertyAttributes(commandProperty, commandMethod);
            }
        }

        private static MethodBuilder BuildExplicitStringGetterOverride(TypeBuilder typeBuilder, string propertyName, MethodInfo methodToCall, Type interfaceType, Type argument = null)
        {
            Type[] emptyTypes;
            MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private;
            if (argument == null)
            {
                emptyTypes = Type.EmptyTypes;
            }
            else
            {
                emptyTypes = new Type[] { argument };
            }
            MethodBuilder methodInfoBody = typeBuilder.DefineMethod(interfaceType.FullName + ".get_" + propertyName, attributes, typeof(string), emptyTypes);
            ILGenerator iLGenerator = methodInfoBody.GetILGenerator();
            if (methodToCall == null)
            {
                iLGenerator.Emit(OpCodes.Ldsfld, typeof(string).GetField("Empty"));
            }
            else
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                if (argument != null)
                {
                    iLGenerator.Emit(OpCodes.Ldarg_1);
                }
                iLGenerator.Emit(OpCodes.Call, methodToCall);
            }
            iLGenerator.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodInfoBody, interfaceType.GetProperty(propertyName).GetGetMethod());
            return methodInfoBody;
        }

        private static MethodBuilder BuildGetCommandMethod(TypeBuilder type, MethodInfo commandMethod, MethodInfo canExecuteMethod, string commandName, bool? useCommandManager, bool isAsyncCommand)
        {
            Type type1;
            Type type5;
            Type type6;
            bool flag = commandMethod.GetParameters().Length == 1;
            bool flag2 = commandMethod.ReturnType == typeof(void);
            Type returnType = commandMethod.ReturnType;
            Type parameterType = flag ? commandMethod.GetParameters()[0].ParameterType : null;
            if (!isAsyncCommand)
            {
                if (!flag)
                {
                    type1 = typeof(ICommand);
                }
                else
                {
                    Type[] typeArguments = new Type[] { parameterType };
                    type1 = typeof(DelegateCommand<>).MakeGenericType(typeArguments);
                }
            }
            else if (!flag)
            {
                type1 = typeof(AsyncCommand);
            }
            else
            {
                Type[] typeArguments = new Type[] { parameterType };
                type1 = typeof(AsyncCommand<>).MakeGenericType(typeArguments);
            }
            Type type4 = type1;
            FieldBuilder field = type.DefineField("_" + commandName, type4, FieldAttributes.Private);
            MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public;
            MethodBuilder builder2 = type.DefineMethod("get_" + commandName, attributes);
            if (!flag)
            {
                if (flag2)
                {
                    type5 = typeof(Action);
                }
                else
                {
                    Type[] typeArguments = new Type[] { returnType };
                    type5 = typeof(Func<>).MakeGenericType(typeArguments);
                }
            }
            else if (flag2)
            {
                Type[] typeArguments = new Type[] { parameterType };
                type5 = typeof(Action<>).MakeGenericType(typeArguments);
            }
            else
            {
                Type[] typeArguments = new Type[] { parameterType, returnType };
                type5 = typeof(Func<,>).MakeGenericType(typeArguments);
            }
            Type[] types = new Type[] { typeof(object), typeof(IntPtr) };
            ConstructorInfo con = type5.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, types, null);
            if (!flag)
            {
                type6 = typeof(Func<bool>);
            }
            else
            {
                Type[] typeArguments = new Type[] { parameterType, typeof(bool) };
                type6 = typeof(Func<,>).MakeGenericType(typeArguments);
            }
            Type[] typeArray8 = new Type[] { typeof(object), typeof(IntPtr) };
            ConstructorInfo info2 = type6.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, typeArray8, null);
            MethodInfo meth = !isAsyncCommand ? (flag ? (flag2 ? DelegateCommandFactory.GetGenericMethodWithoutResult(parameterType, useCommandManager != null) : DelegateCommandFactory.GetGenericMethodWithResult(parameterType, returnType, useCommandManager != null)) : (flag2 ? DelegateCommandFactory.GetSimpleMethodWithoutResult(useCommandManager != null) : DelegateCommandFactory.GetSimpleMethodWithResult(returnType, useCommandManager != null))) : (flag ? AsyncCommandFactory.GetGenericMethodWithResult(parameterType, returnType, useCommandManager != null) : AsyncCommandFactory.GetSimpleMethodWithResult(returnType, useCommandManager != null));
            builder2.SetReturnType(type4);
            ILGenerator iLGenerator = builder2.GetILGenerator();
            iLGenerator.DeclareLocal(type4);
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, field);
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Brtrue_S, label);
            iLGenerator.Emit(OpCodes.Pop);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldftn, commandMethod);
            iLGenerator.Emit(OpCodes.Newobj, con);
            if (canExecuteMethod == null)
            {
                iLGenerator.Emit(OpCodes.Ldnull);
            }
            else
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldftn, canExecuteMethod);
                iLGenerator.Emit(OpCodes.Newobj, info2);
            }
            if (isAsyncCommand)
            {
                AsyncCommandAttribute attribute = ViewModelBase.GetAttribute<AsyncCommandAttribute>(commandMethod);
                iLGenerator.Emit(((attribute != null) ? ((OpCode) attribute.AllowMultipleExecution) : ((OpCode) false)) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            }
            if (useCommandManager != null)
            {
                iLGenerator.Emit(useCommandManager.Value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
            }
            iLGenerator.Emit(OpCodes.Call, meth);
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Stfld, field);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.MarkLabel(label);
            iLGenerator.Emit(OpCodes.Ret);
            return builder2;
        }

        private static MethodBuilder BuildGetParentViewModelMethod(TypeBuilder type, FieldBuilder parentViewModelField, string getterName)
        {
            MethodAttributes attributes = MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private;
            MethodBuilder builder = type.DefineMethod(typeof(ISupportParentViewModel).FullName + "." + getterName, attributes);
            builder.SetReturnType(typeof(object));
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, parentViewModelField);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static MethodBuilder BuildGetServiceContainerMethod(TypeBuilder type, FieldInfo serviceContainerField, string getServiceContainerMethodName)
        {
            MethodAttributes attributes = MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private;
            MethodBuilder builder = type.DefineMethod(typeof(ISupportServices).FullName + "." + getServiceContainerMethodName, attributes);
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(object)) };
            Expression<Func<ServiceContainer>> commandMethodExpression = Expression.Lambda<Func<ServiceContainer>>(Expression.New((ConstructorInfo) methodof(ServiceContainer..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]);
            builder.SetReturnType(typeof(IServiceContainer));
            ILGenerator iLGenerator = builder.GetILGenerator();
            iLGenerator.DeclareLocal(typeof(IServiceContainer));
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldfld, serviceContainerField);
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Brtrue_S, label);
            iLGenerator.Emit(OpCodes.Pop);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Newobj, ExpressionHelper.GetConstructor<ServiceContainer>(commandMethodExpression));
            iLGenerator.Emit(OpCodes.Dup);
            iLGenerator.Emit(OpCodes.Stloc_0);
            iLGenerator.Emit(OpCodes.Stfld, serviceContainerField);
            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.MarkLabel(label);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static MethodBuilder BuildGetServicePropertyMethod(TypeBuilder type, PropertyInfo property, string serviceName, ServiceSearchMode searchMode, bool required)
        {
            MethodInfo info1;
            MethodInfo getMethod = property.GetGetMethod(true);
            MethodAttributes attributes = ((getMethod.IsPublic ? MethodAttributes.Public : MethodAttributes.Family) | MethodAttributes.Virtual) | MethodAttributes.HideBySig;
            MethodBuilder builder = type.DefineMethod(getMethod.Name, attributes);
            builder.SetReturnType(property.PropertyType);
            ILGenerator iLGenerator = builder.GetILGenerator();
            ParameterExpression expression2 = Expression.Parameter(typeof(ISupportServices), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression2 };
            Expression<Func<ISupportServices, IServiceContainer>> expression = Expression.Lambda<Func<ISupportServices, IServiceContainer>>(Expression.Property(expression2, (MethodInfo) methodof(ISupportServices.get_ServiceContainer)), parameters);
            Type[] typeArray = new Type[] { typeof(string), typeof(ServiceSearchMode) };
            if (!required)
            {
                Type[] types = new Type[] { typeof(string), typeof(ServiceSearchMode) };
                info1 = typeof(IServiceContainer).GetMethod("GetService", BindingFlags.Public | BindingFlags.Instance, null, types, null);
            }
            else
            {
                Type[] types = new Type[] { typeof(IServiceContainer), typeof(string), typeof(ServiceSearchMode) };
                info1 = typeof(ServiceContainerExtensions).GetMethod("GetRequiredService", BindingFlags.Public | BindingFlags.Static, null, types, null);
            }
            Type[] typeArguments = new Type[] { property.PropertyType };
            MethodInfo meth = info1.MakeGenericMethod(typeArguments);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Callvirt, ExpressionHelper.GetArgumentPropertyStrict<ISupportServices, IServiceContainer>(expression).GetGetMethod());
            if (string.IsNullOrEmpty(serviceName))
            {
                iLGenerator.Emit(OpCodes.Ldnull);
            }
            else
            {
                iLGenerator.Emit(OpCodes.Ldstr, serviceName);
            }
            iLGenerator.Emit(OpCodes.Ldc_I4_S, (int) searchMode);
            iLGenerator.Emit(required ? OpCodes.Call : OpCodes.Callvirt, meth);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static void BuildServiceProperties(Type type, TypeBuilder typeBuilder)
        {
            Func<PropertyInfo, bool> predicate = <>c.<>9__48_0;
            if (<>c.<>9__48_0 == null)
            {
                Func<PropertyInfo, bool> local1 = <>c.<>9__48_0;
                predicate = <>c.<>9__48_0 = x => IsServiceProperty(x);
            }
            foreach (PropertyInfo info in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<PropertyInfo>(predicate).ToArray<PropertyInfo>())
            {
                ServicePropertyAttribute input = BuilderCommon.GetAttribute<ServicePropertyAttribute>(info);
                bool required = DataAnnotationsAttributeHelper.HasRequiredAttribute(info);
                Func<ServicePropertyAttribute, string> evaluator = <>c.<>9__48_1;
                if (<>c.<>9__48_1 == null)
                {
                    Func<ServicePropertyAttribute, string> local2 = <>c.<>9__48_1;
                    evaluator = <>c.<>9__48_1 = x => x.Key;
                }
                MethodBuilder methodInfoBody = BuildGetServicePropertyMethod(typeBuilder, info, input.With<ServicePropertyAttribute, string>(evaluator), input.Return<ServicePropertyAttribute, ServiceSearchMode>(<>c.<>9__48_2 ??= x => x.SearchMode, <>c.<>9__48_3 ??= () => ServiceSearchMode.PreferLocal), required);
                typeBuilder.DefineMethodOverride(methodInfoBody, info.GetGetMethod(true));
                typeBuilder.DefineProperty(info.Name, PropertyAttributes.None, info.PropertyType, Type.EmptyTypes).SetGetMethod(methodInfoBody);
            }
        }

        private static MethodBuilder BuildSetParentViewModelMethod(TypeBuilder type, FieldBuilder parentViewModelField, string setterName)
        {
            MethodAttributes attributes = MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.Private;
            MethodBuilder builder = type.DefineMethod(typeof(ISupportParentViewModel).FullName + "." + setterName, attributes);
            builder.SetReturnType(typeof(void));
            Type[] parameterTypes = new Type[] { typeof(object) };
            builder.SetParameters(parameterTypes);
            ParameterBuilder builder2 = builder.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator iLGenerator = builder.GetILGenerator();
            Label label = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Ceq);
            iLGenerator.Emit(OpCodes.Brfalse_S, label);
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(string)) };
            ConstructorInfo constructor = ExpressionHelper.GetConstructor<InvalidOperationException>(Expression.Lambda<Func<InvalidOperationException>>(Expression.New((ConstructorInfo) methodof(InvalidOperationException..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]));
            iLGenerator.Emit(OpCodes.Ldstr, "ViewModel cannot be parent of itself.");
            iLGenerator.Emit(OpCodes.Newobj, constructor);
            iLGenerator.Emit(OpCodes.Throw);
            iLGenerator.MarkLabel(label);
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Stfld, parentViewModelField);
            iLGenerator.Emit(OpCodes.Ret);
            return builder;
        }

        private static bool CheckType(Type type, bool @throw) => 
            (type.IsPublic || type.IsNestedPublic) ? (!type.IsSealed ? (!typeof(IPOCOViewModel).IsAssignableFrom(type) || ViewModelSourceException.ReturnFalseOrThrow(@throw, "Type cannot implement IPOCOViewModel: {0}.", type)) : ViewModelSourceException.ReturnFalseOrThrow(@throw, "Cannot create dynamic class for the sealed class: {0}.", type)) : ViewModelSourceException.ReturnFalseOrThrow(@throw, "Cannot create a dynamic class for the non-public class: {0}.", type);

        public static T Create<T>() where T: class, new() => 
            Factory<T>(Expression.Lambda<Func<T>>(Expression.New(typeof(T)), new ParameterExpression[0]))();

        public static T Create<T>(Expression<Func<T>> constructorExpression) where T: class
        {
            ValidateCtorExpression(constructorExpression, false);
            return Expression.Lambda<Func<T>>(GetCtorExpression(constructorExpression, typeof(T), false), new ParameterExpression[0]).Compile()();
        }

        internal static object Create(Type type)
        {
            Type pOCOType = GetPOCOType(type, null);
            ConstructorInfo constructor = pOCOType.GetConstructor(new Type[0]);
            if (constructor != null)
            {
                return constructor.Invoke(null);
            }
            constructor = FindConstructorWithAllOptionalParameters(type);
            if (constructor == null)
            {
                return Activator.CreateInstance(GetPOCOType(type, null));
            }
            Func<ParameterInfo, Type> selector = <>c.<>9__30_0;
            if (<>c.<>9__30_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__30_0;
                selector = <>c.<>9__30_0 = x => x.ParameterType;
            }
            Func<ParameterInfo, object> func2 = <>c.<>9__30_1;
            if (<>c.<>9__30_1 == null)
            {
                Func<ParameterInfo, object> local2 = <>c.<>9__30_1;
                func2 = <>c.<>9__30_1 = x => x.DefaultValue;
            }
            return pOCOType.GetConstructor(constructor.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>()).Invoke(constructor.GetParameters().Select<ParameterInfo, object>(func2).ToArray<object>());
        }

        private static TDelegate CreateFactory<TDelegate>(Expression<TDelegate> constructorExpression, Type resultType) => 
            Expression.Lambda<TDelegate>(GetCtorExpression(constructorExpression, resultType, true), constructorExpression.Parameters).Compile();

        private static Type CreateType(Type type, ViewModelSourceBuilderBase builder)
        {
            MethodInfo info;
            MethodInfo info2;
            CheckType(type, true);
            TypeBuilder typeBuilder = BuilderType.CreateTypeBuilder(type);
            BuilderType.BuildConstructors(type, typeBuilder);
            BuilderINPC.ImplementINPC(type, typeBuilder, out info, out info2);
            BuilderIPOCOViewModel.ImplementIPOCOViewModel(type, typeBuilder, info, info2);
            builder.BuildBindableProperties(type, typeBuilder, info, info2);
            BuildCommands(type, typeBuilder);
            ImplementISupportServices(type, typeBuilder);
            ImplementISupportParentViewModel(type, typeBuilder);
            BuildServiceProperties(type, typeBuilder);
            ImplementIDataErrorInfo(type, typeBuilder);
            return typeBuilder.CreateType();
        }

        public static Func<TResult> Factory<TResult>(Expression<Func<TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, TResult> Factory<T1, TResult>(Expression<Func<T1, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, TResult> Factory<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, TResult> Factory<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, TResult> Factory<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, TResult> Factory<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, T6, TResult> Factory<T1, T2, T3, T4, T5, T6, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, T6, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Factory<T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, T6, T7, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Factory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Factory<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>>(constructorExpression, typeof(TResult));

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Factory<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> constructorExpression) where TResult: class => 
            GetFactoryCore<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>>(constructorExpression, typeof(TResult));

        internal static ConstructorInfo FindConstructorWithAllOptionalParameters(Type type)
        {
            Func<ConstructorInfo, bool> predicate = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<ConstructorInfo, bool> local1 = <>c.<>9__31_0;
                predicate = <>c.<>9__31_0 = delegate (ConstructorInfo x) {
                    if (!x.Attributes.HasFlag(MethodAttributes.Public) && !x.Attributes.HasFlag(MethodAttributes.Family))
                    {
                        return false;
                    }
                    Func<ParameterInfo, bool> func1 = <>c.<>9__31_1;
                    if (<>c.<>9__31_1 == null)
                    {
                        Func<ParameterInfo, bool> local1 = <>c.<>9__31_1;
                        func1 = <>c.<>9__31_1 = y => y.IsOptional;
                    }
                    return x.GetParameters().All<ParameterInfo>(func1);
                };
            }
            return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).FirstOrDefault<ConstructorInfo>(predicate);
        }

        private static MethodInfo GetCanExecuteMethod(Type type, MethodInfo method)
        {
            Func<string, Exception> createException = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Func<string, Exception> local1 = <>c.<>9__43_0;
                createException = <>c.<>9__43_0 = x => new ViewModelSourceException(x);
            }
            return ViewModelBase.GetCanExecuteMethod(type, method, ViewModelBase.GetAttribute<CommandAttribute>(method), createException, new Func<MethodInfo, bool>(BuilderCommon.CanAccessFromDescendant));
        }

        private static IEnumerable<MethodInfo> GetCommandMethods(Type type) => 
            from x in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                where IsCommandMethod(type, x)
                select x;

        internal static string GetCommandName(MethodInfo method)
        {
            CommandAttribute attribute = ViewModelBase.GetAttribute<CommandAttribute>(method);
            return (((attribute == null) || string.IsNullOrEmpty(attribute.Name)) ? ViewModelBase.GetCommandName(method) : attribute.Name);
        }

        internal static ConstructorInfo GetConstructor(Type proxyType, Type[] argsTypes)
        {
            Type[] types = argsTypes;
            if (argsTypes == null)
            {
                Type[] local1 = argsTypes;
                types = Type.EmptyTypes;
            }
            ConstructorInfo constructor = proxyType.GetConstructor(types);
            if (constructor == null)
            {
                throw new ViewModelSourceException("Constructor not found.");
            }
            return constructor;
        }

        private static Expression GetCtorExpression(LambdaExpression constructorExpression, Type resultType, bool useOnlyParameters)
        {
            Type pOCOType = GetPOCOType(resultType, null);
            NewExpression body = constructorExpression.Body as NewExpression;
            if (body != null)
            {
                return GetNewExpression(pOCOType, body);
            }
            MemberInitExpression expression2 = constructorExpression.Body as MemberInitExpression;
            if (expression2 == null)
            {
                throw new ArgumentException("constructorExpression");
            }
            return Expression.MemberInit(GetNewExpression(pOCOType, expression2.NewExpression), expression2.Bindings);
        }

        internal static TDelegate GetFactoryCore<TDelegate>(Func<TDelegate> createFactoryDelegate) => 
            (TDelegate) Factories.GetOrAdd<Type, object>(typeof(TDelegate), () => createFactoryDelegate());

        private static TDelegate GetFactoryCore<TDelegate>(Expression<TDelegate> constructorExpression, Type resultType)
        {
            ValidateCtorExpression(constructorExpression, true);
            return GetFactoryCore<TDelegate>(() => CreateFactory<TDelegate>(constructorExpression, resultType));
        }

        private static NewExpression GetNewExpression(Type type, NewExpression newExpression)
        {
            Func<ParameterInfo, Type> selector = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<ParameterInfo, Type> local1 = <>c.<>9__29_0;
                selector = <>c.<>9__29_0 = x => x.ParameterType;
            }
            return Expression.New(GetConstructor(type, newExpression.Constructor.GetParameters().Select<ParameterInfo, Type>(selector).ToArray<Type>()), newExpression.Arguments);
        }

        public static Type GetPOCOType(Type type, ViewModelSourceBuilderBase builder = null)
        {
            Func<Type> createValueDelegate = () => CreateType(type, builder);
            builder ??= ViewModelSourceBuilderBase.Default;
            return (ReferenceEquals(builder, ViewModelSourceBuilderBase.Default) ? Types.GetOrAdd<Type, Type>(type, createValueDelegate) : createValueDelegate());
        }

        private static void ImplementIDataErrorInfo(Type type, TypeBuilder typeBuilder)
        {
            if (BuilderCommon.ShouldImplementIDataErrorInfo(type))
            {
                typeBuilder.DefineProperty("Error", PropertyAttributes.None, typeof(string), new Type[0]).SetGetMethod(BuildExplicitStringGetterOverride(typeBuilder, "Error", null, typeof(IDataErrorInfo), null));
                MethodInfo method = typeof(IDataErrorInfoHelper).GetMethod("GetErrorText", BindingFlags.Public | BindingFlags.Static);
                Type[] parameterTypes = new Type[] { typeof(string) };
                typeBuilder.DefineProperty("Item", PropertyAttributes.None, typeof(string), parameterTypes).SetGetMethod(BuildExplicitStringGetterOverride(typeBuilder, "Item", method, typeof(IDataErrorInfo), typeof(string)));
                Type[] types = new Type[] { typeof(string) };
                object[] constructorArgs = new object[] { "Item" };
                CustomAttributeBuilder customBuilder = new CustomAttributeBuilder(typeof(DefaultMemberAttribute).GetConstructor(types), constructorArgs);
                typeBuilder.SetCustomAttribute(customBuilder);
            }
        }

        private static void ImplementISupportParentViewModel(Type type, TypeBuilder typeBuilder)
        {
            if (!typeof(ISupportParentViewModel).IsAssignableFrom(type))
            {
                ParameterExpression expression2 = Expression.Parameter(typeof(ISupportParentViewModel), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression2 };
                Expression<Func<ISupportParentViewModel, object>> expression = Expression.Lambda<Func<ISupportParentViewModel, object>>(Expression.Property(expression2, (MethodInfo) methodof(ISupportParentViewModel.get_ParentViewModel)), parameters);
                MethodInfo getMethod = ExpressionHelper.GetArgumentPropertyStrict<ISupportParentViewModel, object>(expression).GetGetMethod();
                MethodInfo setMethod = ExpressionHelper.GetArgumentPropertyStrict<ISupportParentViewModel, object>(expression).GetSetMethod();
                FieldBuilder parentViewModelField = typeBuilder.DefineField("parentViewModel", typeof(object), FieldAttributes.Private);
                MethodBuilder methodInfoBody = BuildGetParentViewModelMethod(typeBuilder, parentViewModelField, getMethod.Name);
                MethodBuilder builder3 = BuildSetParentViewModelMethod(typeBuilder, parentViewModelField, setMethod.Name);
                typeBuilder.DefineMethodOverride(methodInfoBody, getMethod);
                typeBuilder.DefineMethodOverride(builder3, setMethod);
            }
        }

        private static void ImplementISupportServices(Type type, TypeBuilder typeBuilder)
        {
            if (!typeof(ISupportServices).IsAssignableFrom(type))
            {
                ParameterExpression expression = Expression.Parameter(typeof(ISupportServices), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                MethodInfo getMethod = ExpressionHelper.GetArgumentPropertyStrict<ISupportServices, IServiceContainer>(Expression.Lambda<Func<ISupportServices, IServiceContainer>>(Expression.Property(expression, (MethodInfo) methodof(ISupportServices.get_ServiceContainer)), parameters)).GetGetMethod();
                FieldBuilder serviceContainerField = typeBuilder.DefineField("serviceContainer", typeof(IServiceContainer), FieldAttributes.Private);
                MethodBuilder methodInfoBody = BuildGetServiceContainerMethod(typeBuilder, serviceContainerField, getMethod.Name);
                typeBuilder.DefineMethodOverride(methodInfoBody, getMethod);
            }
        }

        private static bool IsCommandMethod(Type type, MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            CommandAttribute attribute = ViewModelBase.GetAttribute<CommandAttribute>(method);
            if ((attribute != null) && !attribute.IsCommand)
            {
                return false;
            }
            if (attribute == null)
            {
                if (!method.IsPublic)
                {
                    return false;
                }
            }
            else if (!BuilderCommon.CanAccessFromDescendant(method))
            {
                throw new ViewModelSourceException($"Method should be public: {method.Name}.");
            }
            string commandName = GetCommandName(method);
            if (method.IsSpecialName || ((method.DeclaringType == typeof(object)) || (type.GetProperty(commandName) != null)))
            {
                return false;
            }
            if ((method.ReturnType != typeof(void)) && ((method.ReturnType != typeof(Task)) && (attribute == null)))
            {
                return false;
            }
            Func<string, Exception> createException = <>c.<>9__41_0;
            if (<>c.<>9__41_0 == null)
            {
                Func<string, Exception> local1 = <>c.<>9__41_0;
                createException = <>c.<>9__41_0 = x => new ViewModelSourceException(x);
            }
            return ViewModelBase.ValidateCommandMethodParameters(method, createException);
        }

        internal static bool IsPOCOViewModelType(Type type)
        {
            bool flag;
            try
            {
                if (CheckType(type, false))
                {
                    if (!type.GetCustomAttributes(typeof(POCOViewModelAttribute), true).Any<object>())
                    {
                        if (!typeof(Component).IsAssignableFrom(type))
                        {
                            if (GetCommandMethods(type).Any<MethodInfo>())
                            {
                                Func<PropertyInfo, bool> predicate = <>c.<>9__34_0;
                                if (<>c.<>9__34_0 == null)
                                {
                                    Func<PropertyInfo, bool> local1 = <>c.<>9__34_0;
                                    predicate = <>c.<>9__34_0 = x => typeof(ICommand).IsAssignableFrom(x.PropertyType);
                                }
                                if (!type.GetProperties().Where<PropertyInfo>(predicate).Any<PropertyInfo>())
                                {
                                    return true;
                                }
                            }
                            flag = ViewModelSourceBuilderBase.Default.GetBindableProperties(type).Any<PropertyInfo>() && !typeof(INotifyPropertyChanged).IsAssignableFrom(type);
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private static bool IsServiceProperty(PropertyInfo property)
        {
            ServicePropertyAttribute attribute = BuilderCommon.GetAttribute<ServicePropertyAttribute>(property);
            if ((attribute != null) && !attribute.IsServiceProperty)
            {
                return false;
            }
            string name = property.PropertyType.Name;
            if (name.Contains<char>('`'))
            {
                name = name.Substring(0, name.IndexOf('`'));
            }
            if (!name.EndsWith("Service") && (attribute == null))
            {
                return false;
            }
            if (!property.PropertyType.IsInterface)
            {
                return ViewModelSourceException.ReturnFalseOrThrow(attribute, "Service properties should have an interface type: {0}.", property);
            }
            MethodInfo getMethod = property.GetGetMethod(true);
            return (BuilderCommon.CanAccessFromDescendant(getMethod) ? (getMethod.IsVirtual ? (!getMethod.IsFinal ? ((property.GetSetMethod(true) == null) || ViewModelSourceException.ReturnFalseOrThrow(attribute, "Property with setter cannot be Service Property: {0}.", property)) : ViewModelSourceException.ReturnFalseOrThrow(attribute, "Cannot override final property: {0}.", property)) : ViewModelSourceException.ReturnFalseOrThrow(attribute, "Property is not virtual: {0}.", property)) : ViewModelSourceException.ReturnFalseOrThrow(attribute, "Cannot access property: {0}.", property));
        }

        private static void RegisterAttributeBuilderProvider(ICustomAttributeBuilderProvider provider)
        {
            AttributeBuilderProviders[provider.AttributeType] = provider;
        }

        private static void ValidateCtorExpression(LambdaExpression constructorExpression, bool useOnlyParameters)
        {
            NewExpression body = constructorExpression.Body as NewExpression;
            if (body == null)
            {
                if (useOnlyParameters || !(constructorExpression.Body is MemberInitExpression))
                {
                    throw new ViewModelSourceException("Constructor expression can only be of NewExpression type.");
                }
            }
            else if (useOnlyParameters)
            {
                foreach (Expression expression2 in body.Arguments)
                {
                    if (!(expression2 is ParameterExpression))
                    {
                        throw new ViewModelSourceException("Constructor expression can refer only to its arguments.");
                    }
                }
            }
        }

        private static Dictionary<Type, ICustomAttributeBuilderProvider> AttributeBuilderProviders =>
            attributeBuilderProviders ??= new Dictionary<Type, ICustomAttributeBuilderProvider>();

        private static Dictionary<Type, Type> Types =>
            types ??= new Dictionary<Type, Type>();

        private static Dictionary<Type, object> Factories =>
            factories ??= new Dictionary<Type, object>();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelSource.<>c <>9 = new ViewModelSource.<>c();
            public static Func<ParameterInfo, Type> <>9__29_0;
            public static Func<ParameterInfo, Type> <>9__30_0;
            public static Func<ParameterInfo, object> <>9__30_1;
            public static Func<ParameterInfo, bool> <>9__31_1;
            public static Func<ConstructorInfo, bool> <>9__31_0;
            public static Func<PropertyInfo, bool> <>9__34_0;
            public static Func<string, Exception> <>9__41_0;
            public static Func<string, Exception> <>9__43_0;
            public static Func<PropertyInfo, bool> <>9__48_0;
            public static Func<ServicePropertyAttribute, string> <>9__48_1;
            public static Func<ServicePropertyAttribute, ServiceSearchMode> <>9__48_2;
            public static Func<ServiceSearchMode> <>9__48_3;

            internal bool <BuildServiceProperties>b__48_0(PropertyInfo x) => 
                ViewModelSource.IsServiceProperty(x);

            internal string <BuildServiceProperties>b__48_1(ServicePropertyAttribute x) => 
                x.Key;

            internal ServiceSearchMode <BuildServiceProperties>b__48_2(ServicePropertyAttribute x) => 
                x.SearchMode;

            internal ServiceSearchMode <BuildServiceProperties>b__48_3() => 
                ServiceSearchMode.PreferLocal;

            internal Type <Create>b__30_0(ParameterInfo x) => 
                x.ParameterType;

            internal object <Create>b__30_1(ParameterInfo x) => 
                x.DefaultValue;

            internal bool <FindConstructorWithAllOptionalParameters>b__31_0(ConstructorInfo x)
            {
                if (!x.Attributes.HasFlag(MethodAttributes.Public) && !x.Attributes.HasFlag(MethodAttributes.Family))
                {
                    return false;
                }
                Func<ParameterInfo, bool> predicate = <>9__31_1;
                if (<>9__31_1 == null)
                {
                    Func<ParameterInfo, bool> local1 = <>9__31_1;
                    predicate = <>9__31_1 = y => y.IsOptional;
                }
                return x.GetParameters().All<ParameterInfo>(predicate);
            }

            internal bool <FindConstructorWithAllOptionalParameters>b__31_1(ParameterInfo y) => 
                y.IsOptional;

            internal Exception <GetCanExecuteMethod>b__43_0(string x) => 
                new ViewModelSourceException(x);

            internal Type <GetNewExpression>b__29_0(ParameterInfo x) => 
                x.ParameterType;

            internal Exception <IsCommandMethod>b__41_0(string x) => 
                new ViewModelSourceException(x);

            internal bool <IsPOCOViewModelType>b__34_0(PropertyInfo x) => 
                typeof(ICommand).IsAssignableFrom(x.PropertyType);
        }
    }
}

