namespace DevExpress.XtraReports.Native
{
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Reflection;

    public static class PropInfoAccessorBase
    {
        public static PropertyInfo GetProperty(object obj, string propName, bool throwOnError) => 
            GetProperty(obj.GetType(), propName, throwOnError);

        public static PropertyInfo GetProperty(object obj, string propName, Type propType)
        {
            PropertyInfo info = null;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            int index = 0;
            while (true)
            {
                if (index < properties.Length)
                {
                    PropertyInfo info2 = properties[index];
                    if ((info2.Name != propName) || !(info2.PropertyType == propType))
                    {
                        index++;
                        continue;
                    }
                    info = info2;
                }
                if (info == null)
                {
                    throw new ArgumentException(PreviewLocalizer.GetString(PreviewStringId.Msg_InvPropName), propName);
                }
                return info;
            }
        }

        public static PropertyInfo GetProperty(Type componentType, string propName, bool throwOnError)
        {
            PropertyInfo property = componentType.GetProperty(propName);
            if ((property == null) & throwOnError)
            {
                throw new ArgumentException(PreviewLocalizer.GetString(PreviewStringId.Msg_InvPropName), propName);
            }
            return property;
        }
    }
}

