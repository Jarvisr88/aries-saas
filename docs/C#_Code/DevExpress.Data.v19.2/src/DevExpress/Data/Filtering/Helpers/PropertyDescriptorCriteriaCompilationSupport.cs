namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public static class PropertyDescriptorCriteriaCompilationSupport
    {
        private const string ReflectPropertyDescriptorTypeName = "System.ComponentModel.ReflectPropertyDescriptor";
        private const string ReflectPropertyDescriptorCompatibilityTypeName = "DevExpress.Compatibility.System.ComponentModel.ReflectPropertyDescriptor";
        private const string ReflectPropertyDescriptorDxTypeName = "DevExpress.Data.Browsing.ReflectPropertyDescriptor";
        private static readonly Dictionary<Tuple<Type, string>, Tuple<Delegate, Type, Type>> fastReflects;

        static PropertyDescriptorCriteriaCompilationSupport();
        public static bool IsReflectPropertyDescriptor(PropertyDescriptor pd);
        private static Tuple<Delegate, Type, Type> MakeFastAccessorCoreCore(PropertyInfo member);
        public static Delegate TryGetFastGetter(PropertyDescriptor pd, out Type rowType, out Type valueType);
        private static Tuple<Delegate, Type, Type> TryMakeFastAccessCore(Type t, string memberName);
        private static Delegate TryMakeFastAccessForReflectPD(PropertyDescriptor pd, out Type rowType, out Type valueType);
        public static Expression TryMakeFastAccessFromDescriptor(Expression baseExpression, PropertyDescriptor pd);

        public interface IHelper
        {
            Delegate TryGetFastGetter(out Type rowType, out Type valueType);
            Expression TryMakeFastExpression(Expression baseExpression);
        }
    }
}

