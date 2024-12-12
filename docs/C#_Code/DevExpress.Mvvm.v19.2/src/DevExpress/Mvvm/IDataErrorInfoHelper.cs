namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class IDataErrorInfoHelper
    {
        private static Attribute[] GetAllAttributes(MemberInfo member) => 
            MetadataHelper.GetAllAttributes(member, false);

        public static string GetErrorText(object owner, string propertyName)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            int index = propertyName.IndexOf('.');
            return ((index < 0) ? (GetNonNestedErrorText(owner, propertyName) ?? string.Empty) : GetNestedPropertyErrorText(owner, propertyName, index));
        }

        private static string GetNestedPropertyErrorText(object owner, string path, int pathDelimiterIndex)
        {
            object obj2;
            string propertyName = path.Remove(pathDelimiterIndex);
            if (!TryGetPropertyValue(owner, propertyName, out obj2))
            {
                return string.Empty;
            }
            IDataErrorInfo info = obj2 as IDataErrorInfo;
            return ((info != null) ? info[path.Substring(pathDelimiterIndex + 1, (path.Length - pathDelimiterIndex) - 1)] : string.Empty);
        }

        private static string GetNonNestedErrorText(object obj, string propertyName)
        {
            object obj2;
            Type baseType = obj.GetType();
            if (obj is IPOCOViewModel)
            {
                baseType = baseType.BaseType;
            }
            PropertyValidator propertyValidator = GetPropertyValidator(baseType, propertyName);
            return ((propertyValidator != null) ? (TryGetPropertyValue(obj, propertyName, out obj2) ? propertyValidator.GetErrorText(obj2, obj) : null) : null);
        }

        private static PropertyValidator GetPropertyValidator(Type type, string propertyName)
        {
            MemberInfo property = type.GetProperty(propertyName);
            return ((property != null) ? PropertyValidator.FromAttributes(GetAllAttributes(property), propertyName) : null);
        }

        public static bool HasErrors<TOwner>(TOwner owner, int deep = 2, params Expression<Func<TOwner, object>>[] excludedProperties)
        {
            Func<Expression<Func<TOwner, object>>, string> selector = <>c__0<TOwner>.<>9__0_0;
            if (<>c__0<TOwner>.<>9__0_0 == null)
            {
                Func<Expression<Func<TOwner, object>>, string> local1 = <>c__0<TOwner>.<>9__0_0;
                selector = <>c__0<TOwner>.<>9__0_0 = x => ExpressionHelper.GetPropertyName<TOwner, object>(x);
            }
            List<string> exProperties = excludedProperties.Select<Expression<Func<TOwner, object>>, string>(selector).ToList<string>();
            Func<PropertyDescriptor, bool> propertyFilter = x => !exProperties.Contains(x.Name);
            return HasErrors((IDataErrorInfo) owner, false, deep, propertyFilter);
        }

        public static bool HasErrors(IDataErrorInfo owner, bool ignoreOwnerError, int deep = 2, Func<PropertyDescriptor, bool> propertyFilter = null)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            int num = deep - 1;
            deep = num;
            if (num < 0)
            {
                return false;
            }
            propertyFilter ??= (<>c.<>9__1_0 ??= x => true);
            IEnumerable<PropertyDescriptor> first = TypeDescriptor.GetProperties(owner).Cast<PropertyDescriptor>().Where<PropertyDescriptor>(propertyFilter);
            IEnumerable<PropertyDescriptor> enumerable1 = first;
            if (<>c.<>9__1_1 == null)
            {
                IEnumerable<PropertyDescriptor> local2 = first;
                enumerable1 = (IEnumerable<PropertyDescriptor>) (<>c.<>9__1_1 = p => p.Name == "Error");
            }
            PropertyDescriptor descriptor = ((IEnumerable<PropertyDescriptor>) <>c.<>9__1_1).FirstOrDefault<PropertyDescriptor>((Func<PropertyDescriptor, bool>) enumerable1);
            ParameterExpression expression = Expression.Parameter(typeof(IDataErrorInfo), "o");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            bool flag = ExpressionHelper.PropertyHasImplicitImplementation<IDataErrorInfo, string>(owner, Expression.Lambda<Func<IDataErrorInfo, string>>(Expression.Property(expression, (MethodInfo) methodof(IDataErrorInfo.get_Error)), parameters), false);
            if ((descriptor != null) & flag)
            {
                PropertyDescriptor[] second = new PropertyDescriptor[] { descriptor };
                first = first.Except<PropertyDescriptor>(second);
            }
            return (first.Any<PropertyDescriptor>(p => PropertyHasError(owner, p, deep)) || (!ignoreOwnerError && !string.IsNullOrEmpty(owner.Error)));
        }

        private static bool PropertyHasError(IDataErrorInfo owner, PropertyDescriptor property, int deep)
        {
            object obj2;
            if (!string.IsNullOrEmpty(owner[property.Name]))
            {
                return true;
            }
            if (!TryGetPropertyValue(owner, property.Name, out obj2))
            {
                return false;
            }
            IDataErrorInfo info = obj2 as IDataErrorInfo;
            return ((info != null) && HasErrors<IDataErrorInfo>(info, deep, new Expression<Func<IDataErrorInfo, object>>[0]));
        }

        private static bool TryGetPropertyValue(object owner, string propertyName, out object propertyValue)
        {
            propertyValue = null;
            PropertyInfo property = owner.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }
            MethodInfo getMethod = property.GetGetMethod();
            if (getMethod == null)
            {
                return false;
            }
            propertyValue = getMethod.Invoke(owner, null);
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IDataErrorInfoHelper.<>c <>9 = new IDataErrorInfoHelper.<>c();
            public static Func<PropertyDescriptor, bool> <>9__1_0;
            public static Func<PropertyDescriptor, bool> <>9__1_1;

            internal bool <HasErrors>b__1_0(PropertyDescriptor x) => 
                true;

            internal bool <HasErrors>b__1_1(PropertyDescriptor p) => 
                p.Name == "Error";
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TOwner>
        {
            public static readonly IDataErrorInfoHelper.<>c__0<TOwner> <>9;
            public static Func<Expression<Func<TOwner, object>>, string> <>9__0_0;

            static <>c__0()
            {
                IDataErrorInfoHelper.<>c__0<TOwner>.<>9 = new IDataErrorInfoHelper.<>c__0<TOwner>();
            }

            internal string <HasErrors>b__0_0(Expression<Func<TOwner, object>> x) => 
                ExpressionHelper.GetPropertyName<TOwner, object>(x);
        }
    }
}

