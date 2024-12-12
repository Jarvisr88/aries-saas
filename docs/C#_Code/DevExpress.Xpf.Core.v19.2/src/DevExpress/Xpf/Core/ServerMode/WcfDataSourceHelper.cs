namespace DevExpress.Xpf.Core.ServerMode
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    internal static class WcfDataSourceHelper
    {
        public static string GetKeyExpressionFromQuery(object queryField)
        {
            string[] keyExpressionsFromQuery = GetKeyExpressionsFromQuery(queryField, "DataServiceKeyAttribute");
            return ((keyExpressionsFromQuery != null) ? (((keyExpressionsFromQuery == null) || (keyExpressionsFromQuery.Length == 0)) ? string.Empty : keyExpressionsFromQuery[0]) : string.Empty);
        }

        public static string[] GetKeyExpressionsFromQuery(object queryField, string keyAttributeName)
        {
            if (queryField == null)
            {
                return null;
            }
            object[] customAttributes = ((Type) queryField.GetType().GetProperty("ElementType").GetValue(queryField, null)).GetCustomAttributes(typeof(object), false);
            if ((customAttributes == null) || (customAttributes.Length == 0))
            {
                return null;
            }
            object obj2 = customAttributes.FirstOrDefault<object>(a => a.GetType().Name == keyAttributeName);
            if (obj2 == null)
            {
                return null;
            }
            ReadOnlyCollection<string> source = obj2.GetType().GetProperty("KeyNames").GetValue(obj2, null) as ReadOnlyCollection<string>;
            return ((source != null) ? source.ToArray<string>() : null);
        }
    }
}

