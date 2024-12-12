namespace DevExpress.Mvvm.POCO
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class BuilderCommon
    {
        public static bool CanAccessFromDescendant(MethodBase method) => 
            method.IsPublic || (method.IsFamily || method.IsFamilyOrAssembly);

        public static T GetAttribute<T>(MemberInfo member) where T: Attribute => 
            MetadataHelper.GetAttribute<T>(member, false);

        public static IEnumerable<T> GetAttributes<T>(MemberInfo member, bool inherit) where T: Attribute => 
            MetadataHelper.GetAttributes<T>(member, inherit);

        public static BindablePropertyAttribute GetBindablePropertyAttribute(PropertyInfo propertyInfo) => 
            GetAttribute<BindablePropertyAttribute>(propertyInfo);

        public static POCOViewModelAttribute GetPOCOViewModelAttribute(Type type) => 
            (POCOViewModelAttribute) type.GetCustomAttributes(typeof(POCOViewModelAttribute), false).FirstOrDefault<object>();

        public static bool IsAutoImplemented(PropertyInfo property)
        {
            if (property.GetGetMethod().GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any<object>())
            {
                return true;
            }
            if (property.GetSetMethod(true).GetParameters().Single<ParameterInfo>().Name != "AutoPropertyValue")
            {
                return false;
            }
            FieldInfo field = property.DeclaringType.GetField("_" + property.Name, BindingFlags.NonPublic | BindingFlags.Instance);
            return ((field != null) && ((field.FieldType == property.PropertyType) && field.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any<object>()));
        }

        public static bool IsBindableProperty(PropertyInfo propertyInfo)
        {
            BindablePropertyAttribute bindablePropertyAttribute = GetBindablePropertyAttribute(propertyInfo);
            if ((bindablePropertyAttribute != null) && !bindablePropertyAttribute.IsBindable)
            {
                return false;
            }
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            MethodInfo setMethod = propertyInfo.GetSetMethod(true);
            return ((getMethod != null) ? (getMethod.IsVirtual ? (!getMethod.IsFinal ? ((setMethod != null) ? (!setMethod.IsAssembly ? (IsAutoImplemented(propertyInfo) || ((bindablePropertyAttribute != null) && bindablePropertyAttribute.IsBindable)) : ViewModelSourceException.ReturnFalseOrThrow(bindablePropertyAttribute, "Cannot make property with internal setter bindable: {0}.", propertyInfo)) : ViewModelSourceException.ReturnFalseOrThrow(bindablePropertyAttribute, "Cannot make property without setter bindable: {0}.", propertyInfo)) : ViewModelSourceException.ReturnFalseOrThrow(bindablePropertyAttribute, "Cannot override final property: {0}.", propertyInfo)) : ViewModelSourceException.ReturnFalseOrThrow(bindablePropertyAttribute, "Cannot make non-virtual property bindable: {0}.", propertyInfo)) : ViewModelSourceException.ReturnFalseOrThrow(bindablePropertyAttribute, "Cannot make property without public getter bindable: {0}.", propertyInfo));
        }

        public static bool ShouldImplementIDataErrorInfo(Type type)
        {
            POCOViewModelAttribute pOCOViewModelAttribute = GetPOCOViewModelAttribute(type);
            bool flag = (pOCOViewModelAttribute != null) && pOCOViewModelAttribute.ImplementIDataErrorInfo;
            if (type.GetInterfaces().Contains<Type>(typeof(IDataErrorInfo)) & flag)
            {
                throw new ViewModelSourceException("The IDataErrorInfo interface is already implemented.");
            }
            return flag;
        }
    }
}

