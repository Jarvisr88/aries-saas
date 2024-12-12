namespace DevExpress.Data.Controls.DataAccess
{
    using DevExpress.DataAccess;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Xml.Linq;

    public static class ParameterSerializer
    {
        public const string XML_Parameter = "Parameter";
        private const string XML_ParameterName = "Name";
        private const string XML_ParameterType = "Type";
        private const string XML_StringNull = "IsNull";

        public static void DeserializeParameter(DataSourceParameterBase paramInfo, XElement paramEl, IExtensionsProvider extensionProvider);
        public static void FillParamInfoFromString(DataSourceParameterBase paramInfo, string paramTypeStr, string paramValueStr, IExtensionsProvider extensionProvider);
        private static bool IsPrimitive(Type type);
        public static XElement SerializeParameter(IExtensionsProvider extensionProvider, DataSourceParameterBase parameter);
    }
}

