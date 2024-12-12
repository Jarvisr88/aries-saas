namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public abstract class ViewModelBase : BindableBase, ISupportParentViewModel, ISupportServices, ISupportParameter, ICustomTypeDescriptor
    {
        internal const string Error_ParentViewModel = "ViewModel cannot be parent of itself.";
        private static readonly object NotSetParameter = new object();
        private object parameter = NotSetParameter;
        private static bool? isInDesignMode;
        private object parentViewModel;
        private IServiceContainer serviceContainer;
        internal const string CommandNameSuffix = "Command";
        private const string CanExecuteSuffix = "Can";
        private const string Error_PropertyWithSameNameAlreadyExists = "Property with the same name already exists: {0}.";
        internal const string Error_MethodShouldBePublic = "Method should be public: {0}.";
        private const string Error_MethodCannotHaveMoreThanOneParameter = "Method cannot have more than one parameter: {0}.";
        private const string Error_MethodCannotHaveOutORRefParameters = "Method cannot have out or reference parameter: {0}.";
        private const string Error_MethodCannotShouldNotBeGeneric = "Method should not be generic: {0}.";
        private const string Error_CanExecuteMethodHasIncorrectParameters = "Can execute method has incorrect parameters: {0}.";
        private const string Error_MethodNotFound = "Method not found: {0}.";
        private Dictionary<MethodInfo, CommandProperty> commandProperties;
        [ThreadStatic]
        private static Dictionary<Type, Dictionary<MethodInfo, CommandProperty>> propertiesCache;
        private readonly Dictionary<MethodInfo, IDelegateCommand> commands = new Dictionary<MethodInfo, IDelegateCommand>();
        private PropertyDescriptorCollection properties;

        public ViewModelBase()
        {
            this.BuildCommandProperties();
            if (IsInDesignMode)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(this.OnInitializeInDesignMode), new object[0]);
            }
            else
            {
                this.OnInitializeInRuntime();
            }
        }

        private void BuildCommandProperties()
        {
            this.commandProperties = this.IsPOCOViewModel ? new Dictionary<MethodInfo, CommandProperty>() : GetCommandProperties(base.GetType());
        }

        private static void CheckCanExecuteMethod(MethodInfo methodInfo, Func<string, Exception> createException, MethodInfo canExecuteMethod, Func<MethodInfo, bool> canAccessMethod)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();
            ParameterInfo[] infoArray2 = canExecuteMethod.GetParameters();
            if (parameters.Length != infoArray2.Length)
            {
                throw createException($"Can execute method has incorrect parameters: {canExecuteMethod.Name}.");
            }
            if ((parameters.Length == 1) && ((parameters[0].ParameterType != infoArray2[0].ParameterType) || (parameters[0].IsOut != infoArray2[0].IsOut)))
            {
                throw createException($"Can execute method has incorrect parameters: {canExecuteMethod.Name}.");
            }
            if (!canAccessMethod(canExecuteMethod))
            {
                throw createException($"Method should be public: {canExecuteMethod.Name}.");
            }
        }

        private static bool CheckCommandMethodConditionValue(bool value, MethodInfo method, string errorString, Func<string, Exception> createException)
        {
            CommandAttribute attribute = GetAttribute<CommandAttribute>(method);
            if (!value && ((attribute != null) && attribute.IsCommand))
            {
                throw createException(string.Format(errorString, method.Name));
            }
            return !value;
        }

        private IDelegateCommand CreateCommand(MethodInfo method, MethodInfo canExecuteMethod, bool? useCommandManager, bool hasParameter, bool allowMultipleExecution)
        {
            bool flag = method.ReturnType == typeof(Task);
            Type type = hasParameter ? method.GetParameters()[0].ParameterType : typeof(object);
            object[] first = new object[] { this, method, canExecuteMethod, useCommandManager, hasParameter };
            Type[] typeArguments = new Type[] { type };
            return (IDelegateCommand) typeof(CreateCommandHelper).MakeGenericType(typeArguments).GetMethod(flag ? "CreateAsyncCommand" : "CreateCommand", BindingFlags.Public | BindingFlags.Static).Invoke(null, flag ? first.Concat<object>(allowMultipleExecution.Yield<object>()).ToArray<object>() : first);
        }

        private static Dictionary<MethodInfo, CommandProperty> CreateCommandProperties(Type type)
        {
            Func<MethodInfo, bool> predicate = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Func<MethodInfo, bool> local1 = <>c.<>9__54_0;
                predicate = <>c.<>9__54_0 = delegate (MethodInfo x) {
                    CommandAttribute attribute = GetAttribute<CommandAttribute>(x);
                    if (attribute != null)
                    {
                        return attribute.IsCommand;
                    }
                    CommandAttribute local2 = attribute;
                    return false;
                };
            }
            Func<CommandProperty, MethodInfo> keySelector = <>c.<>9__54_4;
            if (<>c.<>9__54_4 == null)
            {
                Func<CommandProperty, MethodInfo> local2 = <>c.<>9__54_4;
                keySelector = <>c.<>9__54_4 = x => x.Method;
            }
            Dictionary<MethodInfo, CommandProperty> dictionary = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where<MethodInfo>(predicate).ToArray<MethodInfo>().Select<MethodInfo, CommandProperty>(delegate (MethodInfo x) {
                CommandAttribute attribute = GetAttribute<CommandAttribute>(x);
                string name = attribute.Name ?? (x.Name.EndsWith("Command") ? x.Name : GetCommandName(x));
                Type type1 = type;
                if (<>c.<>9__54_2 == null)
                {
                    Type local2 = type;
                    type1 = (Type) (<>c.<>9__54_2 = s => new CommandAttributeException(s));
                }
                MethodInfo canExecuteMethod = GetCanExecuteMethod((Type) <>c.<>9__54_2, (MethodInfo) attribute, (CommandAttribute) x, (Func<string, Exception>) type1, <>c.<>9__54_3 ??= m => m.IsPublic);
                return new CommandProperty(x, canExecuteMethod, name, attribute.GetUseCommandManager(), MetadataHelper.GetAllAttributes(x, false), type, attribute.AllowMultipleExecutionCore);
            }).ToDictionary<CommandProperty, MethodInfo>(keySelector);
            foreach (CommandProperty property in dictionary.Values)
            {
                if ((type.GetProperty(property.Name) != null) || dictionary.Values.Any<CommandProperty>(x => ((x.Name == property.Name) && !ReferenceEquals(x, property))))
                {
                    throw new CommandAttributeException($"Property with the same name already exists: {property.Name}.");
                }
                if (!property.Method.IsPublic)
                {
                    throw new CommandAttributeException($"Method should be public: {property.Method.Name}.");
                }
                Func<string, Exception> createException = <>c.<>9__54_6;
                if (<>c.<>9__54_6 == null)
                {
                    Func<string, Exception> local3 = <>c.<>9__54_6;
                    createException = <>c.<>9__54_6 = x => new CommandAttributeException(x);
                }
                ValidateCommandMethodParameters(property.Method, createException);
            }
            return dictionary;
        }

        protected virtual IServiceContainer CreateServiceContainer() => 
            new DevExpress.Mvvm.ServiceContainer(this);

        internal static T GetAttribute<T>(MethodInfo method) => 
            MetadataHelper.GetAllAttributes(method, true).OfType<T>().FirstOrDefault<T>();

        internal static MethodInfo GetCanExecuteMethod(Type type, MethodInfo methodInfo, CommandAttribute commandAttribute, Func<string, Exception> createException, Func<MethodInfo, bool> canAccessMethod)
        {
            if ((commandAttribute != null) && (commandAttribute.CanExecuteMethod != null))
            {
                CheckCanExecuteMethod(methodInfo, createException, commandAttribute.CanExecuteMethod, canAccessMethod);
                return commandAttribute.CanExecuteMethod;
            }
            bool flag = (commandAttribute != null) && !string.IsNullOrEmpty(commandAttribute.CanExecuteMethodName);
            string name = flag ? commandAttribute.CanExecuteMethodName : GetCanExecuteMethodName(methodInfo);
            MethodInfo method = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (flag && (method == null))
            {
                throw createException($"Method not found: {commandAttribute.CanExecuteMethodName}.");
            }
            if (method != null)
            {
                CheckCanExecuteMethod(methodInfo, createException, method, canAccessMethod);
            }
            return method;
        }

        internal static string GetCanExecuteMethodName(MethodInfo commandMethod) => 
            "Can" + commandMethod.Name;

        private IDelegateCommand GetCommand(MethodInfo method, MethodInfo canExecuteMethod, bool? useCommandManager, bool hasParameter, bool allowMultipleExecution) => 
            this.commands.GetOrAdd<MethodInfo, IDelegateCommand>(method, () => this.CreateCommand(method, canExecuteMethod, useCommandManager, hasParameter, allowMultipleExecution));

        internal static string GetCommandName(MethodInfo commandMethod) => 
            commandMethod.Name + "Command";

        private static Dictionary<MethodInfo, CommandProperty> GetCommandProperties(Type type) => 
            PropertiesCache.GetOrAdd<Type, Dictionary<MethodInfo, CommandProperty>>(type, () => CreateCommandProperties(type));

        protected virtual T GetService<T>() where T: class => 
            this.GetService<T>(ServiceSearchMode.PreferLocal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual T GetService<T>(ServiceSearchMode searchMode) where T: class => 
            this.ServiceContainer.GetService<T>(searchMode);

        protected virtual T GetService<T>(string key) where T: class => 
            this.GetService<T>(key, ServiceSearchMode.PreferLocal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected virtual T GetService<T>(string key, ServiceSearchMode searchMode) where T: class => 
            this.ServiceContainer.GetService<T>(key, searchMode);

        protected virtual void OnInitializeInDesignMode()
        {
            this.OnParameterChanged(null);
        }

        protected virtual void OnInitializeInRuntime()
        {
        }

        protected virtual void OnParameterChanged(object parameter)
        {
        }

        protected virtual void OnParentViewModelChanged(object parentViewModel)
        {
        }

        protected internal void RaiseCanExecuteChanged(Expression<Action> commandMethodExpression)
        {
            this.RaiseCanExecuteChangedCore(commandMethodExpression);
        }

        protected internal void RaiseCanExecuteChanged(Expression<Func<Task>> commandMethodExpression)
        {
            this.RaiseCanExecuteChangedCore(commandMethodExpression);
        }

        private void RaiseCanExecuteChangedCore(LambdaExpression commandMethodExpression)
        {
            if (this.IsPOCOViewModel)
            {
                POCOViewModelExtensions.RaiseCanExecuteChangedCore(this, commandMethodExpression);
            }
            else
            {
                ((IDelegateCommand) this.commandProperties[ExpressionHelper.GetMethod(commandMethodExpression)].GetValue(this)).RaiseCanExecuteChanged();
            }
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes() => 
            TypeDescriptor.GetAttributes(this, true);

        string ICustomTypeDescriptor.GetClassName() => 
            TypeDescriptor.GetClassName(this, true);

        string ICustomTypeDescriptor.GetComponentName() => 
            TypeDescriptor.GetComponentName(this, true);

        TypeConverter ICustomTypeDescriptor.GetConverter() => 
            TypeDescriptor.GetConverter(this, true);

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() => 
            TypeDescriptor.GetDefaultEvent(this, true);

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() => 
            TypeDescriptor.GetDefaultProperty(this, true);

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType) => 
            TypeDescriptor.GetEditor(this, editorBaseType, true);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents() => 
            TypeDescriptor.GetEvents(this, true);

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) => 
            TypeDescriptor.GetEvents(this, attributes, true);

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptorCollection properties = this.properties;
            if (this.properties == null)
            {
                PropertyDescriptorCollection local1 = this.properties;
                properties = this.properties = new PropertyDescriptorCollection(TypeDescriptor.GetProperties(this, true).Cast<PropertyDescriptor>().Concat<PropertyDescriptor>(this.commandProperties.Values).ToArray<PropertyDescriptor>());
            }
            return properties;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => 
            TypeDescriptor.GetProperties(this, attributes, true);

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) => 
            this;

        internal static bool ValidateCommandMethodParameters(MethodInfo method, Func<string, Exception> createException)
        {
            ParameterInfo[] parameters = method.GetParameters();
            return (!CheckCommandMethodConditionValue(parameters.Length <= 1, method, "Method cannot have more than one parameter: {0}.", createException) ? (!CheckCommandMethodConditionValue((parameters.Length != 1) || (!parameters[0].IsOut && !parameters[0].ParameterType.IsByRef), method, "Method cannot have out or reference parameter: {0}.", createException) ? !CheckCommandMethodConditionValue(!method.IsGenericMethodDefinition, method, "Method should not be generic: {0}.", createException) : false) : false);
        }

        public static bool IsInDesignMode
        {
            get
            {
                if (ViewModelDesignHelper.IsInDesignModeOverride != null)
                {
                    return ViewModelDesignHelper.IsInDesignModeOverride.Value;
                }
                if (isInDesignMode == null)
                {
                    isInDesignMode = new bool?((bool) DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue);
                }
                return isInDesignMode.Value;
            }
        }

        object ISupportParentViewModel.ParentViewModel
        {
            get => 
                this.parentViewModel;
            set
            {
                if (this.parentViewModel != value)
                {
                    if (value == this)
                    {
                        throw new InvalidOperationException("ViewModel cannot be parent of itself.");
                    }
                    this.parentViewModel = value;
                    this.OnParentViewModelChanged(this.parentViewModel);
                }
            }
        }

        IServiceContainer ISupportServices.ServiceContainer =>
            this.ServiceContainer;

        protected IServiceContainer ServiceContainer
        {
            get
            {
                IServiceContainer serviceContainer = this.serviceContainer;
                if (this.serviceContainer == null)
                {
                    IServiceContainer local1 = this.serviceContainer;
                    serviceContainer = this.serviceContainer = this.CreateServiceContainer();
                }
                return serviceContainer;
            }
        }

        private bool IsPOCOViewModel =>
            this is IPOCOViewModel;

        protected object Parameter
        {
            get => 
                Equals(this.parameter, NotSetParameter) ? null : this.parameter;
            set
            {
                if (this.parameter != value)
                {
                    this.parameter = value;
                    this.OnParameterChanged(value);
                }
            }
        }

        object ISupportParameter.Parameter
        {
            get => 
                this.Parameter;
            set => 
                this.Parameter = value;
        }

        private static Dictionary<Type, Dictionary<MethodInfo, CommandProperty>> PropertiesCache =>
            propertiesCache ??= new Dictionary<Type, Dictionary<MethodInfo, CommandProperty>>();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelBase.<>c <>9 = new ViewModelBase.<>c();
            public static Func<MethodInfo, bool> <>9__54_0;
            public static Func<string, Exception> <>9__54_2;
            public static Func<MethodInfo, bool> <>9__54_3;
            public static Func<ViewModelBase.CommandProperty, MethodInfo> <>9__54_4;
            public static Func<string, Exception> <>9__54_6;

            internal bool <CreateCommandProperties>b__54_0(MethodInfo x)
            {
                CommandAttribute attribute = ViewModelBase.GetAttribute<CommandAttribute>(x);
                if (attribute != null)
                {
                    return attribute.IsCommand;
                }
                CommandAttribute local2 = attribute;
                return false;
            }

            internal Exception <CreateCommandProperties>b__54_2(string s) => 
                new CommandAttributeException(s);

            internal bool <CreateCommandProperties>b__54_3(MethodInfo m) => 
                m.IsPublic;

            internal MethodInfo <CreateCommandProperties>b__54_4(ViewModelBase.CommandProperty x) => 
                x.Method;

            internal Exception <CreateCommandProperties>b__54_6(string x) => 
                new CommandAttributeException(x);
        }

        private class CommandProperty : PropertyDescriptor
        {
            private readonly MethodInfo method;
            private readonly MethodInfo canExecuteMethod;
            private readonly string name;
            private readonly bool? useCommandManager;
            private readonly bool allowMultipleExecution;
            private readonly bool hasParameter;
            private readonly Attribute[] attributes;
            private readonly Type reflectedType;

            public CommandProperty(MethodInfo method, MethodInfo canExecuteMethod, string name, bool? useCommandManager, Attribute[] attributes, Type reflectedType, bool allowMultipleExecution) : base(name, attributes)
            {
                this.method = method;
                this.hasParameter = method.GetParameters().Length == 1;
                this.canExecuteMethod = canExecuteMethod;
                this.name = name;
                this.useCommandManager = useCommandManager;
                this.allowMultipleExecution = allowMultipleExecution;
                this.attributes = attributes;
                this.reflectedType = reflectedType;
            }

            public override bool CanResetValue(object component) => 
                false;

            private IDelegateCommand GetCommand(object component) => 
                ((ViewModelBase) component).GetCommand(this.method, this.canExecuteMethod, this.useCommandManager, this.hasParameter, this.allowMultipleExecution);

            public override object GetValue(object component) => 
                this.GetCommand(component);

            public override void ResetValue(object component)
            {
                throw new NotSupportedException();
            }

            public override void SetValue(object component, object value)
            {
                throw new NotSupportedException();
            }

            public override bool ShouldSerializeValue(object component) => 
                false;

            public MethodInfo Method =>
                this.method;

            public MethodInfo CanExecuteMethod =>
                this.canExecuteMethod;

            public override Type ComponentType =>
                this.method.DeclaringType;

            public override bool IsReadOnly =>
                true;

            public override Type PropertyType =>
                typeof(ICommand);
        }

        public static class CreateCommandHelper<T>
        {
            public static IDelegateCommand CreateAsyncCommand(object owner, MethodInfo method, MethodInfo canExecuteMethod, bool? useCommandManager, bool hasParameter, bool allowMultipleExecution) => 
                new AsyncCommand<T>(x => (Task) method.Invoke(owner, ViewModelBase.CreateCommandHelper<T>.GetInvokeParameters(x, hasParameter)), ViewModelBase.CreateCommandHelper<T>.GetCanExecute(owner, canExecuteMethod, hasParameter), allowMultipleExecution, useCommandManager);

            public static IDelegateCommand CreateCommand(object owner, MethodInfo method, MethodInfo canExecuteMethod, bool? useCommandManager, bool hasParameter) => 
                new DelegateCommand<T>(delegate (T x) {
                    method.Invoke(owner, ViewModelBase.CreateCommandHelper<T>.GetInvokeParameters(x, hasParameter));
                }, ViewModelBase.CreateCommandHelper<T>.GetCanExecute(owner, canExecuteMethod, hasParameter), useCommandManager);

            private static Func<T, bool> GetCanExecute(object owner, MethodInfo canExecuteMethod, bool hasParameter) => 
                x => (canExecuteMethod != null) ? ((bool) canExecuteMethod.Invoke(owner, ViewModelBase.CreateCommandHelper<T>.GetInvokeParameters(x, hasParameter))) : true;

            private static object[] GetInvokeParameters(object parameter, bool hasParameter)
            {
                if (!hasParameter)
                {
                    return new object[0];
                }
                return new object[] { parameter };
            }
        }
    }
}

