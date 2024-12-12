namespace DevExpress.Xpf.Core.ServerMode
{
    using System;

    internal static class ODataSourceHelper
    {
        public static string[] GetKeyExpressionsFromQuery(object queryField) => 
            WcfDataSourceHelper.GetKeyExpressionsFromQuery(queryField, "KeyAttribute");
    }
}

