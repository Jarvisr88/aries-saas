namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Access;
    using DevExpress.Data.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Security;
    using System.Security.Permissions;

    public class XRAccessor : Accessor
    {
        private XRAccessor()
        {
        }

        public static void ChangeProperty(object obj, string propName, object value)
        {
            PropertyDescriptor propertyDescriptor = GetPropertyDescriptor(obj, propName);
            if ((propertyDescriptor != null) && (propertyDescriptor.GetValue(obj) != value))
            {
                propertyDescriptor.SetValue(obj, value);
            }
        }

        private static FieldInfo[] GetDeclaredFields(object obj, Type fieldType) => 
            GetFields(obj, fieldType, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        public static PropertyDescriptor GetFastPropertyDescriptor(PropertyDescriptor descriptor)
        {
            if (!SecurityHelper.IsPermissionGranted(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess)) || !SecurityHelper.IsPermissionGranted(new SecurityPermission(SecurityPermissionFlag.ControlEvidence)))
            {
                return descriptor;
            }
            try
            {
                return DataListDescriptor.GetFastProperty(descriptor);
            }
            catch (SecurityException)
            {
                return descriptor;
            }
        }

        public static FieldInfo[] GetFields(object obj, Type fieldType) => 
            GetFields(obj, fieldType, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        private static FieldInfo[] GetFields(object obj, Type fieldType, BindingFlags bindingAttr) => 
            (from x in obj.GetType().GetFields(bindingAttr)
                where fieldType.IsAssignableFrom(x.FieldType)
                select x).ToArray<FieldInfo>();

        public static IList<T> GetFieldValues<T>(object obj) => 
            (from x in GetDeclaredFields(obj, typeof(T)) select (T) x.GetValue(obj)).ToList<T>();

        public static IList GetFieldValues(object obj, Type fieldType)
        {
            ArrayList list = new ArrayList();
            foreach (FieldInfo info in GetDeclaredFields(obj, fieldType))
            {
                list.Add(info.GetValue(obj));
            }
            return list;
        }

        public static PropertyDescriptor GetPropertyDescriptor(object obj, string propName)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)[propName];
            if (descriptor == null)
            {
                throw new ArgumentException(PreviewLocalizer.GetString(PreviewStringId.Msg_InvPropName), propName);
            }
            return descriptor;
        }

        public static Type GetPropertyType(object obj, string propName)
        {
            PropertyDescriptor propertyDescriptor = GetPropertyDescriptor(obj, propName);
            return propertyDescriptor?.PropertyType;
        }
    }
}

