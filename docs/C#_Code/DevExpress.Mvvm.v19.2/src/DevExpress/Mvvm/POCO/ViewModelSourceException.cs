namespace DevExpress.Mvvm.POCO
{
    using System;
    using System.Reflection;

    [Serializable]
    public class ViewModelSourceException : Exception
    {
        internal const string Error_ObjectDoesntImplementIPOCOViewModel = "Object doesn't implement IPOCOViewModel.";
        internal const string Error_ObjectDoesntImplementISupportServices = "Object doesn't implement ISupportServices.";
        internal const string Error_CommandNotFound = "Command not found: {0}.";
        internal const string Error_CommandNotAsync = "Command is not async";
        internal const string Error_ConstructorNotFound = "Constructor not found.";
        internal const string Error_TypeHasNoCtors = "Type has no accessible constructors: {0}.";
        internal const string Error_SealedClass = "Cannot create dynamic class for the sealed class: {0}.";
        internal const string Error_InternalClass = "Cannot create a dynamic class for the non-public class: {0}.";
        internal const string Error_TypeImplementsIPOCOViewModel = "Type cannot implement IPOCOViewModel: {0}.";
        internal const string Error_RaisePropertyChangedMethodNotFound = "Class already supports INotifyPropertyChanged, but RaisePropertyChanged(string) method not found: {0}.";
        internal const string Error_RaisePropertyChangingMethodNotFound = "Class already supports INotifyPropertyChanging, but RaisePropertyChanging(string) method not found: {0}.";
        internal const string Error_PropertyIsNotVirual = "Cannot make non-virtual property bindable: {0}.";
        internal const string Error_PropertyHasInternalSetter = "Cannot make property with internal setter bindable: {0}.";
        internal const string Error_PropertyHasNoSetter = "Cannot make property without setter bindable: {0}.";
        internal const string Error_PropertyHasNoGetter = "Cannot make property without public getter bindable: {0}.";
        internal const string Error_PropertyIsFinal = "Cannot override final property: {0}.";
        internal const string Error_MoreThanOnePropertyChangedMethod = "More than one property changed method: {0}.";
        internal const string Error_PropertyChangedMethodShouldBePublicOrProtected = "Property changed method should be public or protected: {0}.";
        internal const string Error_PropertyChangedCantHaveMoreThanOneParameter = "Property changed method cannot have more than one parameter: {0}.";
        internal const string Error_PropertyChangedCantHaveReturnType = "Property changed method cannot have return type: {0}.";
        internal const string Error_PropertyChangedMethodArgumentTypeShouldMatchPropertyType = "Property changed method argument type should match property type: {0}.";
        internal const string Error_PropertyChangedMethodNotFound = "Property changed method not found: {0}.";
        internal const string Error_MemberWithSameCommandNameAlreadyExists = "Member with the same command name already exists: {0}.";
        internal const string Error_PropertyTypeShouldBeServiceType = "Service properties should have an interface type: {0}.";
        internal const string Error_CantAccessProperty = "Cannot access property: {0}.";
        internal const string Error_PropertyIsNotVirtual = "Property is not virtual: {0}.";
        internal const string Error_PropertyHasSetter = "Property with setter cannot be Service Property: {0}.";
        internal const string Error_ConstructorExpressionCanReferOnlyToItsArguments = "Constructor expression can refer only to its arguments.";
        internal const string Error_ConstructorExpressionCanOnlyBeOfNewExpressionType = "Constructor expression can only be of NewExpression type.";
        internal const string Error_IDataErrorInfoAlreadyImplemented = "The IDataErrorInfo interface is already implemented.";
        internal const string Error_INotifyPropertyChangingIsNotImplemented = "The INotifyPropertyChanging interface is not implemented or implemented explicitly: {0}";
        internal const string Error_DependsOnNotBindable = "The {0} property cannot depend on the {1} property, because the latter is not bindable.";
        internal const string Error_DependsOnNotExist = "The {0} property cannot depend on the {1} property, because the latter does not exist.";

        public ViewModelSourceException()
        {
        }

        public ViewModelSourceException(string message) : base(message)
        {
        }

        internal static bool ReturnFalseOrThrow(Attribute attribute, string text, PropertyInfo property)
        {
            if (attribute != null)
            {
                throw new ViewModelSourceException(string.Format(text, property.Name));
            }
            return false;
        }

        internal static bool ReturnFalseOrThrow(bool @throw, string text, Type type)
        {
            if (@throw)
            {
                throw new ViewModelSourceException(string.Format(text, type.Name));
            }
            return false;
        }
    }
}

