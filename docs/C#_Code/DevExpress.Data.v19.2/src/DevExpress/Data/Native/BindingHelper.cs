namespace DevExpress.Data.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;

    public class BindingHelper
    {
        public static DataSet ConvertToDataSet(object obj);
        public static IDataAdapter ConvertToIDataAdapter(object obj);
        public static string ConvertToTableName(object obj, string member);
        public static object DetermineDataAdapter(IList typedAdapters, DataTable table);
        public static object DetermineDataAdapter(IList typedAdapters, string tableName);
        private static object DetermineDataAdapter(IList typedAdapters, string tableName, string tableNamespace);
        private static IDataAdapter ExtractDataAdapter(object obj);
        public static string GetDataSourceName(IComponent obj);
        public static bool IsDataAdapter(object obj);
        public static bool IsDataObject(object obj);
        public static bool IsDataSource(object obj);
        public static bool IsEFDbSet(Type type);
        public static bool IsList(PropertyDescriptor property);
        public static bool IsListSource(object obj);
        public static string JoinStrings(string separator, string s1, string s2);
    }
}

